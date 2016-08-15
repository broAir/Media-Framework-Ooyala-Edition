
namespace Sitecore.MediaFramework.Ooyala.Synchronize.Fallback
{
  using Sitecore.Data.Items;
  using Sitecore.MediaFramework.Entities;
  using Sitecore.MediaFramework.Ooyala.Entities;
  using Sitecore.MediaFramework.Ooyala.Indexing.Entities;
  using Sitecore.MediaFramework.Synchronize.Fallback;

  public class LabelFallback : DatabaseFallbackBase
  {
    protected override Item GetItem(object entity, Item accountItem)
    {
      Label label = (Label)entity;

      return accountItem.Axes.SelectSingleItem(string.Format("./Labels//*[@@templateid='{0}' and @Id='{1}']", TemplateIDs.Label, label.Id));
    }

    protected override MediaServiceSearchResult GetSearchResult(Item item)
    {
      Item parent = item.Parent;

      return new LabelSearchResult
        {
          Id = item[FieldIDs.Label.Id],
          LabelName = item[FieldIDs.Label.Name],
          FullName = item[FieldIDs.Label.FullName],
          ParentId = parent.TemplateID == TemplateIDs.Label ? parent[FieldIDs.Label.Id] : string.Empty
        };
    }
  }
}
