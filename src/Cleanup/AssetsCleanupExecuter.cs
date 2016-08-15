namespace Sitecore.MediaFramework.Ooyala.Cleanup
{
  using Sitecore.MediaFramework.Cleanup;
  using Sitecore.MediaFramework.Ooyala.Entities;
  using Sitecore.MediaFramework.Ooyala.Indexing.Entities;

  public class AssetsCleanupExecuter : CleanupExecuterBase<Asset, AssetSearchResult>
  {
    protected override string GetEntityId(Asset entity)
    {
      return entity.EmbedCode;
    }

    protected override string GetSearchResultId(AssetSearchResult searchResult)
    {
      return searchResult.EmbedCode;
    }
  }
}