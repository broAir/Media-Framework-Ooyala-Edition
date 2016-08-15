namespace Sitecore.MediaFramework.Ooyala.Cleanup
{
  using Sitecore.MediaFramework.Cleanup;
  using Sitecore.MediaFramework.Ooyala.Entities;
  using Sitecore.MediaFramework.Ooyala.Indexing.Entities;

  public class LabelsCleanupExecuter : CleanupExecuterBase<Label, LabelSearchResult> 
  {
    protected override string GetEntityId(Label entity)
    {
      return entity.Id;
    }

    protected override string GetSearchResultId(LabelSearchResult searchResult)
    {
      return searchResult.Id;
    }
  }
}
