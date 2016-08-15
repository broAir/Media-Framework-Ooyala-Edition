namespace Sitecore.MediaFramework.Ooyala.Entities.Players
{
  using System.Collections.Generic;

  using Newtonsoft.Json;

  public class RelatedVideos
  {
    [JsonProperty("source")]
    public bool Source { get; set; }

    [JsonProperty("click_behavior")]
    public bool ClickBehavior { get; set; }

    [JsonProperty("order")]
    public string Order { get; set; }

    [JsonProperty("sort")]
    public string Sort { get; set; }

    [JsonProperty("labels")]
    public List<Label> Labels { get; set; }
  }
}
