namespace Sitecore.MediaFramework.Ooyala.Indexing.Entities
{
  using Sitecore.ContentSearch;

  public class VideoSearchResult : AssetSearchResult
  {
    /// <summary>
    /// Gets or sets the original file name.
    /// </summary>
    [IndexField("originalfilename")]
    public string OriginalFileName { get; set; }
  }
}
