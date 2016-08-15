namespace Sitecore.MediaFramework.Ooyala.Entities.Players
{
  using Newtonsoft.Json;

  public class Scrubber
  {
    [JsonProperty("always_show")]
    public bool AlwaysShow { get; set; }

    [JsonProperty("image_url")]
    public string ImageUrl { get; set; }
  }
}
