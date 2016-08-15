namespace Sitecore.MediaFramework.Ooyala.Synchronize
{
  using System.Globalization;

  using Sitecore.Data.Items;
  using Sitecore.MediaFramework.Entities;
  using Sitecore.MediaFramework.Export;
  using Sitecore.MediaFramework.Ooyala.Entities;
  using Sitecore.MediaFramework.Ooyala.Indexing.Entities;
  using Sitecore.MediaFramework.Synchronize;

  using Integration.Common.Utils;

  /// <summary>
  /// The asset synchronizer.
  /// </summary>
  public abstract class AssetSynchronizer : SynchronizerBase
  {
    public override Item SyncItem(object entity, Item accountItem)
    {
      var assetEntity = (Asset)entity;

      if(ExportQueueManager.IsExist(accountItem, FieldIDs.MediaElement.EmbedCode, assetEntity.EmbedCode))
      {
        return null;
      }

      return base.SyncItem(entity, accountItem);
    }

    public override Item GetRootItem(object entity, Item accountItem)
    {
      return accountItem.Children["Media Content"];
    }

    public override Item UpdateItem(object entity, Item accountItem, Item item)
    {
      var asset = (Asset)entity;
      using (new EditContext(item))
      {
        this.UpdateProperties(item, accountItem, asset);
      }

      return item;
    }

    public override bool NeedUpdate(object entity, Item accountItem, MediaServiceSearchResult searchResult)
    {
      var asset = (Asset)entity;
      var assetIndex = (AssetSearchResult)searchResult;

      return !StringUtil.EqualsIgnoreNullEmpty(asset.UpdatedAt,assetIndex.UpdatedAt);
    }

    public override MediaServiceSearchResult GetSearchResult(object entity, Item accountItem)
    {
      var asset = (Asset)entity;

      var searchResult = base.GetSearchResult<VideoSearchResult>(Constants.IndexName, accountItem,
        i => (i.TemplateId == TemplateIDs.Video || i.TemplateId == TemplateIDs.Channel) && i.EmbedCode == asset.EmbedCode.Replace('-', ' '));

      //workaround for lowercased embed codes
      return searchResult != null && searchResult.EmbedCode == asset.EmbedCode ? searchResult : null;
    }

    protected virtual void UpdateProperties(Item item, Item accountItem, Asset asset)
    {
      item.Name = ItemUtil.ProposeValidItemName(asset.Name);

      item[FieldIDs.MediaElement.AssetType] = asset.AssetType;
      item[FieldIDs.MediaElement.Name] = asset.Name;
      item[FieldIDs.MediaElement.EmbedCode] = asset.EmbedCode;
      item[FieldIDs.MediaElement.Description] = asset.Description;
      item[FieldIDs.MediaElement.Status] = asset.Status;

      item[FieldIDs.MediaElement.Duration] = asset.Duration.ToString(CultureInfo.InvariantCulture);

      item[FieldIDs.MediaElement.PreviewImageUrl] = asset.PreviewImageUrl;

      item[FieldIDs.MediaElement.CreatedAt] = asset.CreatedAt;
      item[FieldIDs.MediaElement.UpdatedAt] = asset.UpdatedAt;
    }

    public override MediaServiceEntityData GetMediaData(object entity)
    {
      var asset = (Asset)entity;

      return new MediaServiceEntityData { EntityId = asset.EmbedCode, EntityName = asset.Name };
    }
  }
}