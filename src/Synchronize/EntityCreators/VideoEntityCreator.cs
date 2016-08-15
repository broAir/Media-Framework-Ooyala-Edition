namespace Sitecore.MediaFramework.Ooyala.Synchronize.EntityCreators
{
  using Sitecore.Data.Items;
  using Sitecore.MediaFramework.Ooyala.Entities;

  public class VideoEntityCreator : AssetEntityCreator<Video>
  {
    public override object CreateEntity(Item item)
    {
      var video = (Video)base.CreateEntity(item);

      video.AssetType = "video";
      video.OriginalFileName = item[FieldIDs.MediaElement.Video.OriginalFileName]; 

      return video;
    }
  }
}