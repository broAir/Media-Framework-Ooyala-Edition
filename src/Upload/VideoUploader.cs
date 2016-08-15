namespace Sitecore.MediaFramework.Ooyala.Upload
{
  using System;
  using System.Collections.Generic;
  using System.Collections.Specialized;
  using System.IO;
  using System.Net;
  using System.Threading;

  using Sitecore.RestSharp.Data;

  using global::RestSharp;

  using Sitecore.Data.Items;
  using Sitecore.MediaFramework.Ooyala.Entities;
  using Sitecore.MediaFramework.Ooyala.Security;
  using Sitecore.MediaFramework.Upload;
  using Sitecore.RestSharp;
  using Sitecore.Text;

  public class VideoUploader : UploadExecuterBase
  {
    public string ChunkSize { get; set; }
    public string PostProcessingStatus { get; set; }

    protected override object UploadInternal(NameValueCollection parameters, byte[] fileBytes, Item accountItem)
    {
      string fileName = this.GetFileName(parameters);

      var videoToUpload = new VideoToUpload
      {
        AssetType = "video",
        ChunkSize = MainUtil.GetLong(this.ChunkSize,5000000),
        FileSize = fileBytes.LongLength,
        FileName = fileName,
        Name = Path.GetFileNameWithoutExtension(fileName),
        PostProcessingStatus = this.PostProcessingStatus
      };

      Video video = this.CreateVideo(accountItem, videoToUpload);

      if (video != null)
      {
        try
        {
          var sended = this.SendContent(parameters, fileBytes, accountItem, video);
          if (sended)
          {
            this.MarkAs(accountItem, video, "uploaded");
            video.Status = "processing";
            return video;
          }

          this.DeleteVideo(accountItem, video);
          return new CanceledVideo();
        }
        catch (Exception)
        {
          this.UpdateStatus(Guid.Empty, this.GetFileId(parameters), accountItem.ID.Guid, 0, "Uploading process failed. Please read logs for more details!");
          this.MarkAs(accountItem, video, "failed");
        }
      }

      return null;
    }

    protected virtual bool SendContent(NameValueCollection parameters, byte[] fileBytes, Item accountItem, Video video)
    {
      Guid fileId = this.GetFileId(parameters);

      foreach (string url in this.GetUploadingUrls(accountItem, video))
      { 
        UrlString urlString = new UrlString(url);

        string[] tmp = StringUtil.GetLastPostfix(urlString["filename"], '/')
                                 .Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

        int startIndex = int.Parse(tmp[0]);
        int lastIndex = int.Parse(tmp[1]);

        byte[] chunk = new byte[lastIndex - startIndex + 1];

        Array.Copy(fileBytes, startIndex, chunk, 0, lastIndex - startIndex + 1);

        for (int i = 0; i < 5; i++)
        {
          try
          {
            this.SendChunk(url, chunk);
            break;
          }
          catch
          {
            continue;
          }
        }

        byte progress = Convert.ToByte(((lastIndex + 1) / (float)fileBytes.Length) * 100);

        if (this.IsCanceled(parameters))
        {
          this.Cancel(fileId, accountItem.ID.Guid);
          return false;
        }

        if (progress != 100)
        { 
          this.UpdateStatus(Guid.Empty, fileId, accountItem.ID.Guid, progress);
        }                                                                                                       
      }

      return true;
    }    

    protected virtual void SendChunk(string url, byte[] bytes)
    {
      WebRequest request = WebRequest.Create(url);
      request.ContentType = "application/x-www-form-urlencoded";
      request.Method = "PUT";
      request.Timeout = 1800000; // 30m
      request.ContentLength = bytes.Length;
      Stream stream = request.GetRequestStream();                                              
      
      stream.Write(bytes, 0, bytes.Length);
      stream.Flush();
      stream.Close();
      var response = request.GetResponse();
    }
                                                                                                          
    protected virtual void MarkAs(Item accountItem, Video video, string mark)
    {
      var authenticator = new OoyalaAthenticator(accountItem);
      var context = new RestContext(Constants.SitecoreRestSharpService, authenticator);
      var i = 0;
      while (i < 5)
      {
        var res = context.Update<Video, Video>(
          "update_video_upload_status",
          new Video { Status = mark },
          new List<Parameter>
            {
              new Parameter
                {
                  Type = ParameterType.UrlSegment,
                  Name = "embedcode",
                  Value = video.EmbedCode
                }
            });

        if (res.StatusCode == HttpStatusCode.OK)
        {
          return;
        }

        Thread.Sleep(5000);
        i++;
      }
    }

    protected virtual void DeleteVideo(Item accountItem, Video video)
    {
      var authenticator = new OoyalaAthenticator(accountItem);
      var context = new RestContext(Constants.SitecoreRestSharpService, authenticator);
      context.Delete<Video, RestEmptyType>(
      "delete_video",
      parameters:
      new List<Parameter>
            {
              new Parameter
                {
                  Name = "embedcode",
                  Type = ParameterType.UrlSegment,
                  Value = video.EmbedCode
                }
            });
    }

    protected virtual Video CreateVideo(Item accountItem, VideoToUpload video)
    {
      var authenticator = new OoyalaAthenticator(accountItem);
      var context = new RestContext(Constants.SitecoreRestSharpService, authenticator);

      return context.Create<VideoToUpload, Video>("upload_video_create", video).Data;
    }

    protected virtual List<string> GetUploadingUrls(Item accountItem, Video video)
    {
      var authenticator = new OoyalaAthenticator(accountItem);
      var context = new RestContext(Constants.SitecoreRestSharpService, authenticator);

      return context.Read<List<string>>(
        "get_uploading_URLs",
          new List<Parameter>
                {
                  new Parameter
                    {
                      Type = ParameterType.UrlSegment, 
                      Name = "embedcode", 
                      Value = video.EmbedCode
                    }
                }).Data;
    }
  }
}
