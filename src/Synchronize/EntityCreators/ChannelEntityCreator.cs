namespace Sitecore.MediaFramework.Ooyala.Synchronize.EntityCreators
{
  using Sitecore.Data.Items;
  using Sitecore.MediaFramework.Ooyala.Entities;

  public class ChannelEntityCreator : AssetEntityCreator<Channel>
  {
    public override object CreateEntity(Item item)
    {
      var channel = (Channel)base.CreateEntity(item);

      channel.AssetType = "channel";

      return channel;
    }
  }
}