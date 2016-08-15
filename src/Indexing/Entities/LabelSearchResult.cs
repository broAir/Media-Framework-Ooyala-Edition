namespace Sitecore.MediaFramework.Ooyala.Indexing.Entities
{
  using Sitecore.ContentSearch;
  using Sitecore.MediaFramework.Entities;

  public class LabelSearchResult : MediaServiceSearchResult
  {
    [IndexField("id")]
    public string Id { get; set; }

    [IndexField("name")]
    public string LabelName { get; set; }

    [IndexField("fullname")]
    public string FullName { get; set; }

    [IndexField("label_parentid")]
    public string ParentId { get; set; }
  }
}
