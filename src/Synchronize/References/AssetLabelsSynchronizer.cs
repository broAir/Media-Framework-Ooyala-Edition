namespace Sitecore.MediaFramework.Ooyala.Synchronize.References
{
  using System.Collections.Generic;
  using System.Linq;

  using Sitecore.ContentSearch.Linq.Utilities;
  using Sitecore.Integration.Common.Utils;
  using Sitecore.MediaFramework.Ooyala.Security;
  using Sitecore.MediaFramework.Synchronize;
  using Sitecore.RestSharp;

  using global::RestSharp;

  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.MediaFramework.Ooyala.Entities;
  using Sitecore.MediaFramework.Ooyala.Entities.Collections;
  using Sitecore.MediaFramework.Ooyala.Indexing.Entities;
  using Sitecore.MediaFramework.Synchronize.References;

  using Constants = Sitecore.MediaFramework.Ooyala.Constants;

  public class AssetLabelsSynchronizer : IdReferenceSynchronizer<Asset>
  {
    protected override List<ID> GetReference(Asset entity, Item accountItem)
    {
      List<Label> labels = this.GetLabels(entity, accountItem);

      if (labels == null)
      {
        return null;
      }

      if (labels.Count == 0)
      {
        return new List<ID>(0);
      }

      string[] labelIds = labels.Select(i => i.Id).ToArray();

      var expression = ContentSearchUtil.GetAncestorFilter<LabelSearchResult>(accountItem, TemplateIDs.Label);
      var idExp = labelIds.Aggregate(PredicateBuilder.False<LabelSearchResult>(), (current, tmp) => current.Or(i => i.Id == tmp));

      List<LabelSearchResult> searchResults = ContentSearchUtil.FindAll(Constants.IndexName, expression.And(idExp));

      //fallback
      if (searchResults.Count < labelIds.Length)
      {
        IItemSynchronizer synchronizer = MediaFrameworkContext.GetItemSynchronizer(typeof(Label));
        if (synchronizer != null)
        {
          foreach (Label label in labels)
          {
            if (searchResults.Any(i => i.Id == label.Id))
            {
              continue;
            }

            var labelIndex = synchronizer.Fallback(label, accountItem) as LabelSearchResult;
            if (labelIndex != null)
            {
              searchResults.Add(labelIndex);
            }
          }
        }
      }

      return searchResults.Select(i => i.ItemId).ToList();
    }

    protected virtual List<Label> GetLabels(Asset asset, Item accountItem)
    {
      if (asset.Labels != null)
      {
        return asset.Labels;
      }

      var parameters = new List<Parameter> { new Parameter() { Name = "embedcode", Type = ParameterType.UrlSegment, Value = asset.EmbedCode } };

      var context = new RestContext(Constants.SitecoreRestSharpService, new OoyalaAthenticator(accountItem));
      PagedCollection<Label> collection = context.Read<PagedCollection<Label>>("read_asset_labels", parameters).Data;

      return collection != null ? collection.Items : null;
    }
  }
}
