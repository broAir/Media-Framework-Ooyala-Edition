namespace Sitecore.MediaFramework.Ooyala.Synchronize
{
  using Sitecore.ContentSearch.Linq.Utilities;
  using Sitecore.Data.Items;
  using Sitecore.Integration.Common.Utils;
  using Sitecore.MediaFramework.Entities;
  using Sitecore.MediaFramework.Export;
  using Sitecore.MediaFramework.Ooyala.Entities;
  using Sitecore.MediaFramework.Ooyala.Indexing.Entities;
  using Sitecore.MediaFramework.Synchronize;

  public class LabelSynchronizer : SynchronizerBase
  {
    public override Item SyncItem(object entity, Item accountItem)
    {
      var labelEntity = (Label)entity;

      if (ExportQueueManager.IsExist(accountItem, FieldIDs.Label.Id, labelEntity.Id))
      {
        return null;
      }

      return base.SyncItem(entity, accountItem);
    }

    public override Item UpdateItem(object entity, Item accountItem, Item item)
    {
      var labelEntity = (Label)entity;

      if (item.Parent[FieldIDs.Label.Id] != labelEntity.ParentId)
      {
        Item parentItem = this.GetParentItem(labelEntity, accountItem);
        if (parentItem != null)
        {
          item.MoveTo(parentItem);
        }
      }

      using (new EditContext(item))
      {
        item.Name = ItemUtil.ProposeValidItemName(labelEntity.Name);

        item[FieldIDs.Label.Id] = labelEntity.Id;
        item[FieldIDs.Label.FullName] = labelEntity.FullName;
        item[FieldIDs.Label.Name] = labelEntity.Name;
      }

      return item;
    }

    public override Item GetRootItem(object entity, Item accountItem)
    {
      return this.GetParentItem((Label)entity, accountItem);
    }

    public override bool NeedUpdate(object entity, Item accountItem, MediaServiceSearchResult searchResult)
    {
      var label = (Label)entity;
      var labelIndex = (LabelSearchResult)searchResult;

      return
        !(StringUtil.EqualsIgnoreNullEmpty(label.Id, labelIndex.Id)
          && StringUtil.EqualsIgnoreNullEmpty(label.Name, labelIndex.LabelName)
          && StringUtil.EqualsIgnoreNullEmpty(label.FullName, labelIndex.FullName)
          && StringUtil.EqualsIgnoreNullEmpty(label.ParentId, labelIndex.ParentId));
    }

    public override MediaServiceSearchResult GetSearchResult(object entity, Item accountItem)
    {
      var label = (Label)entity;
      return base.GetSearchResult<LabelSearchResult>(Constants.IndexName, accountItem, i => i.TemplateId == TemplateIDs.Label && i.Id == label.Id);
    }

    public override MediaServiceEntityData GetMediaData(object entity)
    {
      var label = (Label)entity;

      return new MediaServiceEntityData { EntityId = label.Id, EntityName = label.Name, TemplateId = TemplateIDs.Label };
    }

    protected virtual Item GetParentItem(Label label, Item accountItem)
    {
      if (string.IsNullOrEmpty(label.ParentId))
      {
        return accountItem.Children["Labels"];
      }

      var expression = ContentSearchUtil.GetAncestorFilter<LabelSearchResult>(accountItem, TemplateIDs.Label);
      expression.And(i => i.Id == label.ParentId);

      LabelSearchResult searchResults = ContentSearchUtil.FindOne(Constants.IndexName, expression.And(i => i.Id == label.ParentId));
      if (searchResults != null)
      {
        Item item = searchResults.GetItem();
        if (item != null)
        {
          return item;
        }
      }

      return accountItem.Axes.SelectSingleItem(string.Format("./Labels//*[@@templateid='{0}' and @Id='{1}']", TemplateIDs.Label, label.ParentId));
    }
  }
}
