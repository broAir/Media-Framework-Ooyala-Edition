namespace Sitecore.MediaFramework.Ooyala.Import
{
  using Sitecore.MediaFramework.Ooyala.Entities;

  public class PlayerCollectionImporter : EntityCollectionImporter<Player>
  {
    protected override string RequestName
    {
      get
      {
        return "read_players";
      }
    }
  }
}
