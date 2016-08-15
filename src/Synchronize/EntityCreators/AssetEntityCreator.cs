namespace Sitecore.MediaFramework.Ooyala.Synchronize.EntityCreators
{
  using Sitecore.Data.Items;
  using Sitecore.MediaFramework.Ooyala.Entities;
  using Sitecore.MediaFramework.Synchronize;

  public abstract class AssetEntityCreator<T> : IMediaServiceEntityCreator
    where T: Asset, new()
  {
    public virtual object CreateEntity(Item item)
    {
      return new T
        {
          PreviewImageUrl = item[FieldIDs.MediaElement.PreviewImageUrl],
          Name = item[FieldIDs.MediaElement.Name],
          EmbedCode = item[FieldIDs.MediaElement.EmbedCode],
          Description = item[FieldIDs.MediaElement.Description],
          Status = item[FieldIDs.MediaElement.Status],
          Duration = item[FieldIDs.MediaElement.Duration],
          CreatedAt = item[FieldIDs.MediaElement.CreatedAt],
          UpdatedAt = item[FieldIDs.MediaElement.UpdatedAt],
          Metadata = Integration.Common.Utils.StringUtil.GetDictionary(item[FieldIDs.MediaElement.CustomMetadata], '=', '&')
        };
    }
  }
}