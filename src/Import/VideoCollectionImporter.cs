namespace Sitecore.MediaFramework.Ooyala.Import
{
  using Sitecore.MediaFramework.Ooyala.Entities;

  public class VideoCollectionImporter : EntityCollectionImporter<Video>
  {
    protected override string RequestName
    {
      get
      {
        return "read_videos";
      }
    }
  }
}
