// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VideoExporter.cs" company="Sitecore A/S">
//   Copyright (C) 2013 by Sitecore A/S
// </copyright>
// <summary>
//   The video exporter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.MediaFramework.Ooyala.Export
{
  using System.Collections.Generic;

  using Sitecore.MediaFramework.Ooyala.Security;
  using Sitecore.RestSharp.Data;

  using global::RestSharp;

  using Sitecore.MediaFramework.Export;
  using Sitecore.MediaFramework.Ooyala.Entities;
  using Sitecore.RestSharp;

  /// <summary>
  /// The video exporter.
  /// </summary>
  public class VideoExporter : AssetExporter
  {
    /// <summary>
    /// Creates a video befor the upload.
    /// </summary>
    /// <param name="operation">
    /// The operation.
    /// </param>
    /// <returns>
    /// The <see cref="object"/>.
    /// </returns>
    protected override object Create(ExportOperation operation)
    {
      return null;
    }

    /// <summary>
    /// Deletes a video.
    /// </summary>
    /// <param name="operation">
    /// The operation.
    /// </param>
    protected override void Delete(ExportOperation operation)
    {
      var synchronizer = MediaFrameworkContext.GetItemSynchronizer(operation.Item);
      if (synchronizer == null)
      {
        return;
      }

      var authenticator = new OoyalaAthenticator(operation.AccountItem);
      var context = new RestContext(Constants.SitecoreRestSharpService, authenticator);

      var video = (Video)synchronizer.CreateEntity(operation.Item);

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

    /// <summary>
    /// Updates a video.
    /// </summary>
    /// <param name="operation">
    /// The operation.
    /// </param>
    /// <returns>
    /// The <see cref="object"/>.
    /// </returns>
    protected override object Update(ExportOperation operation)
    {
      var synchronizer = MediaFrameworkContext.GetItemSynchronizer(operation.Item);
      if (synchronizer == null)
      {
        return null;
      }

      var authenticator = new OoyalaAthenticator(operation.AccountItem);
      var context = new RestContext(Constants.SitecoreRestSharpService, authenticator);

      var video = (Video)synchronizer.CreateEntity(operation.Item);

      string embedCode = video.EmbedCode;
      
      video.EmbedCode = null;
      video.CreatedAt = null;
      video.UpdatedAt = null;
      video.Duration = null;
      video.PostProcessingStatus = null;
      video.PreviewImageUrl = null;
      video.Status = null;
      video.AssetType = null;
      video.Metadata = null;
      video.OriginalFileName = null;

      this.UpdateLabels(operation, embedCode);
      this.UpdatePlayer(operation, embedCode);
      this.UpdateMetadata(operation, embedCode);

      return context.Update<Video, Video>(
        "update_video",
        video,
        new List<Parameter>
            {
              new Parameter
                {
                  Type = ParameterType.UrlSegment, 
                  Name = "embedcode", 
                  Value = embedCode
                }
            }).Data;
    }
  }
}