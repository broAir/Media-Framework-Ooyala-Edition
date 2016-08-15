namespace Sitecore.MediaFramework.Ooyala.Synchronize
{
  using Sitecore.MediaFramework.Entities;

  public class ChannelSynchronizer : AssetSynchronizer
  {
    public override MediaServiceEntityData GetMediaData(object entity)
    {
      var mediaData = base.GetMediaData(entity);

      mediaData.TemplateId = TemplateIDs.Channel;

      return mediaData;
    }
  }
}
