namespace Sitecore.MediaFramework.Ooyala.Synchronize.Fallback
{
  using Sitecore.Data.Items;
  using Sitecore.MediaFramework.Entities;
  using Sitecore.MediaFramework.Ooyala.Entities;
  using Sitecore.MediaFramework.Ooyala.Indexing.Entities;

  public class VideoFallback : AssetFallback<VideoSearchResult>
  {
    protected override Item GetItem(object entity, Item accountItem)
    {
      Video video = (Video)entity;

      return accountItem.Axes.SelectSingleItem(string.Format("./Media Content//*[@@templateid='{0}' and @embedcode='{1}']", TemplateIDs.Video, video.EmbedCode));
    }

    protected override MediaServiceSearchResult GetSearchResult(Item item)
    {
      VideoSearchResult videoSearchResult = (VideoSearchResult)base.GetSearchResult(item);

      videoSearchResult.OriginalFileName = item[FieldIDs.MediaElement.Video.OriginalFileName];
      
      return videoSearchResult;
    }
  }
}