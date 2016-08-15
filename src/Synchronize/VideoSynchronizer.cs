namespace Sitecore.MediaFramework.Ooyala.Synchronize
{
  using Sitecore.Data.Items;
  using Sitecore.MediaFramework.Entities;
  using Sitecore.MediaFramework.Ooyala.Entities;

  public class VideoSynchronizer : AssetSynchronizer
  {
    protected override void UpdateProperties(Item item, Item accountItem, Asset asset)
    {
      base.UpdateProperties(item, accountItem, asset);

      item[FieldIDs.MediaElement.Video.OriginalFileName] = ((Video)asset).OriginalFileName;
    }

    public override MediaServiceEntityData GetMediaData(object entity)
    {
      var mediaData = base.GetMediaData(entity);

      mediaData.TemplateId = TemplateIDs.Video;

      return mediaData;
    }
  }
}
