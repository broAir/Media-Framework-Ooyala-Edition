namespace Sitecore.MediaFramework.Ooyala.Entities.Players
{
  using Newtonsoft.Json;

  public class Branding
  {
    [JsonProperty("show_info_screen_homepage_link")]
    public bool ShowInfoScreenHomepageLink { get; set; }

    [JsonProperty("show_share_button")]
    public bool ShowShareButton { get; set; }

    [JsonProperty("twitter_sharing")]
    public bool TwitterSharing { get; set; }

    [JsonProperty("show_ad_countdown")]
    public bool ShowAdCountdown { get; set; }

    [JsonProperty("show_info_screen_title")]
    public bool ShowInfoScreenTitle { get; set; }

    [JsonProperty("show_info_button")]
    public bool ShowInfoButton { get; set; }

    [JsonProperty("url_sharing")]
    public bool UrlSharing { get; set; }

    [JsonProperty("facebook_sharing")]
    public bool FacebookSharing { get; set; }

    [JsonProperty("email_sharing")]
    public bool EmailSharing { get; set; }

    [JsonProperty("show_info_screen_description")]
    public bool ShowInfoScreenDescription { get; set; }

    [JsonProperty("show_bitrate_button")]
    public bool ShowBitrateButton { get; set; }

    [JsonProperty("digg_sharing")]
    public bool DiggSharing { get; set; }

    [JsonProperty("show_embed_button")]
    public bool ShowEmbedButton { get; set; }

    [JsonProperty("show_channel_button")]
    public bool ShowChannelButton { get; set; }

    [JsonProperty("show_end_screen_replay_button")]
    public bool ShowEndScreenReplayButton { get; set; }

    [JsonProperty("accent_color")]
    public string AccentColor { get; set; }

    [JsonProperty("enable_error_screen")]
    public bool EnableErrorScreen { get; set; }

    [JsonProperty("show_volume_button")]
    public bool ShowVolumeButton { get; set; }

    [JsonProperty("chromeless")]
    public bool Chromeless { get; set; }
  }
}
