
namespace Sitecore.MediaFramework.Ooyala.Synchronize.EntityCreators
{
  using Sitecore.Data.Items;
  using Sitecore.MediaFramework.Ooyala.Entities;
  using Sitecore.MediaFramework.Synchronize;

  public class LabelEntityCreator : IMediaServiceEntityCreator
  {
    public virtual object CreateEntity(Item item)
    {
      Item parent = item.Parent;
     
      return new Label
        {
          Id = item[FieldIDs.Label.Id],
          Name = item[FieldIDs.Label.Name],
          ParentId = parent != null && parent.TemplateID == TemplateIDs.Label ? parent[FieldIDs.Label.Id] : string.Empty,
          FullName = item[FieldIDs.Label.FullName]
        };
    }
  }
}
