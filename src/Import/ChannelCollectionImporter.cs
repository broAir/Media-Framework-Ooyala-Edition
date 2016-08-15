namespace Sitecore.MediaFramework.Ooyala.Import
{
  using Sitecore.MediaFramework.Ooyala.Entities;

  public class ChannelCollectionImporter : EntityCollectionImporter<Channel>
  {
    protected override string RequestName
    {
      get
      {
        return "read_channels";
      }
    }
  }
}
