namespace Sitecore.MediaFramework.Ooyala.Entities
{
  using Newtonsoft.Json;

  public class Video : Asset
  {
    /// <summary>
    /// Gets or sets the original file name.
    /// </summary>
    [JsonProperty("original_file_name", NullValueHandling = NullValueHandling.Ignore)]
    public string OriginalFileName { get; set; }
  }
}
