namespace Sitecore.MediaFramework.Ooyala.Entities
{
  using Newtonsoft.Json;

  using Sitecore.MediaFramework.Ooyala.Entities.Players;

  /// <summary>
  /// The player.
  /// </summary>
  public class Player
  {  
    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
    public string Id { get; set; }

    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }

    [JsonProperty("is_default", NullValueHandling = NullValueHandling.Ignore)]
    public bool IsDefault { get; set; }

    [JsonProperty("provider_homepage_url", NullValueHandling = NullValueHandling.Ignore)]
    public string ProviderHomepageUrl { get; set; }

    [JsonProperty("default_closed_caption_language", NullValueHandling = NullValueHandling.Ignore)]
    public string DefaultClosedCaptionLanguage { get; set; }

    [JsonProperty("ooyala_branding", NullValueHandling = NullValueHandling.Ignore)]
    public Branding Branding { get; set; }

    [JsonProperty("scrubber", NullValueHandling = NullValueHandling.Ignore)]
    public Scrubber Scrubber { get; set; }

    [JsonProperty("watermark", NullValueHandling = NullValueHandling.Ignore)]
    public Watermark Watermark { get; set; }

    [JsonProperty("playback", NullValueHandling = NullValueHandling.Ignore)]
    public Playback Playback { get; set; }

    public override string ToString()
    {
      return string.Format("(type:{0},id:{1},name:{2})", this.GetType().Name, this.Id, this.Name);
    }
  }
}
