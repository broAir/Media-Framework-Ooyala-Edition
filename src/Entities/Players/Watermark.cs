namespace Sitecore.MediaFramework.Ooyala.Entities.Players
{
  using Newtonsoft.Json;

  public class Watermark
  {
    [JsonProperty("image_url")]
    public string ImageUrl { get; set; }

    [JsonProperty("click_url")]
    public string ClickUrl { get; set; }

    [JsonProperty("Alpha")]
    public string Alpha { get; set; }
  }
}
