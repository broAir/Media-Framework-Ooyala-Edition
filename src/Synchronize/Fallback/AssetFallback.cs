namespace Sitecore.MediaFramework.Ooyala.Synchronize.Fallback
{
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.MediaFramework.Entities;
  using Sitecore.MediaFramework.Ooyala.Indexing.Entities;
  using Sitecore.MediaFramework.Synchronize.Fallback;

  public abstract class AssetFallback<T> : DatabaseFallbackBase
    where T : AssetSearchResult, new()
  {
    protected override MediaServiceSearchResult GetSearchResult(Item item)
    {
      return new T
        {
          EmbedCode = item[FieldIDs.MediaElement.EmbedCode],
          AssetName = item[FieldIDs.MediaElement.Name],
          Description = item[FieldIDs.MediaElement.Description],
          AssetType = item[FieldIDs.MediaElement.AssetType],
          CreatedAt = item[FieldIDs.MediaElement.CreatedAt],
          UpdatedAt = item[FieldIDs.MediaElement.UpdatedAt],
          Status = item[FieldIDs.MediaElement.Status],
          Duration = item[FieldIDs.MediaElement.Duration],
          PreviewImageUrl = item[FieldIDs.MediaElement.PreviewImageUrl],
          CustomMetadata = item[FieldIDs.MediaElement.CustomMetadata],
          LabelsList = ID.ParseArray(item[FieldIDs.MediaElement.LabelsList]),
          Player = item[FieldIDs.MediaElement.Player]
        };
    }
  }
}