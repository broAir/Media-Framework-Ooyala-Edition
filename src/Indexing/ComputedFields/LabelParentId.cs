
namespace Sitecore.MediaFramework.Ooyala.Indexing.ComputedFields
{
  using Sitecore.ContentSearch;
  using Sitecore.ContentSearch.ComputedFields;
  using Sitecore.Data.Items;

  public class LabelParentId : IComputedIndexField
  {
    public object ComputeFieldValue(IIndexable indexable)
    {
      Item item = (Item)(indexable as SitecoreIndexableItem);
      if (item.TemplateID == TemplateIDs.Label)
      {
        Item parent = item.Parent;
        if (parent != null && parent.TemplateID == TemplateIDs.Label)
        {
          return parent[FieldIDs.Label.Id];
        }
      }

      return null;
    }

    public string FieldName { get; set; }

    public string ReturnType { get; set; }
  }
}
