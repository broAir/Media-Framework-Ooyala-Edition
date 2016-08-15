namespace Sitecore.MediaFramework.Ooyala.Import
{
  using Sitecore.MediaFramework.Ooyala.Entities;

  public class AssetCollectionImporter : EntityCollectionImporter<Asset>
  {
    protected override string RequestName
    {
      get
      {
        return "read_assets";
      }
    }
  }
}
