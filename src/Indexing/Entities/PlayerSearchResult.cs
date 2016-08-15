namespace Sitecore.MediaFramework.Ooyala.Indexing.Entities
{
  using Sitecore.ContentSearch;
  using Sitecore.MediaFramework.Entities;     

  public class PlayerSearchResult : MediaServiceSearchResult
  {                                        
    [IndexField("id")]
    public string Id { get; set; }

    [IndexField("isdefault")]
    public bool IsDefault { get; set; }

    [IndexField("name")]
    public string PlayerName { get; set; }

    [IndexField("providerhomepageurl")]
    public string ProviderHomepageUrl { get; set; }

    [IndexField("defaultlanguage")]
    public string DefaultLanguage { get; set; }

    // Branding


    [IndexField("accentcolor")]
    public string AccentColor { get; set; }

    [IndexField("showsharebutton")]
    public bool ShowShareButton { get; set; }

    [IndexField("showinfoscreenhomepagelink")]
    public bool ShowInfoScreenHomepageLink { get; set; }
       
    [IndexField("twittersharing")]
    public bool TwitterSharing { get; set; }

    [IndexField("showadcountdown")]
    public bool ShowAdCountdown { get; set; }

    [IndexField("showinfoscreentitle")]
    public bool ShowInfoScreenTitle { get; set; }

    [IndexField("showinfobutton")]
    public bool ShowInfoButton { get; set; }

    [IndexField("urlsharing")]
    public bool UrlSharing { get; set; }

    [IndexField("facebooksharing")]
    public bool FacebookSharing { get; set; }

    [IndexField("emailsharing")]
    public bool EmailSharing { get; set; }

    [IndexField("showinfoscreendescription")]
    public bool ShowInfoScreenDescription { get; set; }

    [IndexField("showbitratebutton")]
    public bool ShowBitrateButton { get; set; }

    [IndexField("diggsharing")]
    public bool DiggSharing { get; set; }

    [IndexField("showembedbutton")]
    public bool ShowEmbedButton { get; set; }

    [IndexField("showchannelbutton")]
    public bool ShowChannelButton { get; set; }

    [IndexField("showendscreenreplaybutton")]
    public bool ShowEndScreenReplayButton { get; set; }
        
    [IndexField("enableerrorscreen")]
    public bool EnableErrorScreen { get; set; }

    [IndexField("showvolumebutton")]
    public bool ShowVolumeButton { get; set; }

    [IndexField("chromeless")]
    public bool Chromeless { get; set; }


    // Scrubber
    [IndexField("alwaysshow")]
    public bool AlwaysShow { get; set; }

    [IndexField("scrubberimageurl")]
    public string ScrubberImageUrl { get; set; }

    // Playback
    [IndexField("bufferonpause")]
    public bool BufferOnPause { get; set; }

    // Watermark
    [IndexField("watermarkimageurl")]
    public string WatermarkImageUrl { get; set; }

    [IndexField("clickurl")]
    public string ClickUrl { get; set; }

    [IndexField("alpha")]
    public string Alpha { get; set; }
  }
}
