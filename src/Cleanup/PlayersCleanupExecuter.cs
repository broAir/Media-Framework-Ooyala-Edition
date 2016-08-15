namespace Sitecore.MediaFramework.Ooyala.Cleanup
{
  using Sitecore.MediaFramework.Cleanup;
  using Sitecore.MediaFramework.Ooyala.Entities;
  using Sitecore.MediaFramework.Ooyala.Indexing.Entities;

  public class PlayersCleanupExecuter : CleanupExecuterBase<Player, PlayerSearchResult>
  {
    protected override string GetEntityId(Player entity)
    {
      return entity.Id;
    }

    protected override string GetSearchResultId(PlayerSearchResult searchResult)
    {
      return searchResult.Id;
    }
  }
}