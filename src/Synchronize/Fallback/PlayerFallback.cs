namespace Sitecore.MediaFramework.Ooyala.Synchronize.Fallback
{
  using Sitecore.Data.Items;
  using Sitecore.MediaFramework.Entities;
  using Sitecore.MediaFramework.Ooyala.Entities;
  using Sitecore.MediaFramework.Ooyala.Indexing.Entities;
  using Sitecore.MediaFramework.Synchronize.Fallback;

  public class PlayerFallback : DatabaseFallbackBase
  {
    protected override Item GetItem(object entity, Item accountItem)
    {
      Player player = (Player)entity;

      return accountItem.Axes.SelectSingleItem(string.Format("./Players/*[@@templateid='{0}' and @Id='{1}']", TemplateIDs.Player, player.Id));
    }

    protected override MediaServiceSearchResult GetSearchResult(Item item)
    {
      return new PlayerSearchResult
        {
          Id = item[FieldIDs.Player.Id],
          IsDefault = item[FieldIDs.Player.IsDefault] == "1",
          PlayerName = item[FieldIDs.Player.Name],
          ProviderHomepageUrl = item[FieldIDs.Player.ProviderHomepageUrl],
          DefaultLanguage = item[FieldIDs.Player.DefaultLanguage],

          // Branding
          AccentColor = item[FieldIDs.Player.AccentColor],
          ShowShareButton = item[FieldIDs.Player.ShowShareButton] == "1",
          ShowInfoScreenHomepageLink = item[FieldIDs.Player.ShowInfoScreenHomepageLink] == "1",
          TwitterSharing = item[FieldIDs.Player.TwitterSharing] == "1",
          ShowAdCountdown = item[FieldIDs.Player.ShowAdCountdown] == "1",
          ShowInfoScreenTitle = item[FieldIDs.Player.Id] == "1",
          ShowInfoButton = item[FieldIDs.Player.ShowInfoButton] == "1",
          FacebookSharing = item[FieldIDs.Player.FacebookSharing] == "1",
          EmailSharing = item[FieldIDs.Player.EmailSharing] == "1",
          ShowInfoScreenDescription = item[FieldIDs.Player.ShowInfoScreenDescription] == "1",
          ShowBitrateButton = item[FieldIDs.Player.ShowBitrateButton] == "1",
          DiggSharing = item[FieldIDs.Player.DiggSharing] == "1",
          ShowEmbedButton = item[FieldIDs.Player.ShowEmbedButton] == "1",
          ShowChannelButton = item[FieldIDs.Player.ShowChannelButton] == "1",
          ShowEndScreenReplayButton = item[FieldIDs.Player.ShowEndScreenReplayButton] == "1",
          EnableErrorScreen = item[FieldIDs.Player.EnableErrorScreen] == "1",
          ShowVolumeButton = item[FieldIDs.Player.ShowVolumeButton] == "1",
          Chromeless = item[FieldIDs.Player.Chromeless] == "1",

          // Scrubber
          AlwaysShow = item[FieldIDs.Player.AlwaysShow] == "1",
          ScrubberImageUrl = item[FieldIDs.Player.ScrubberImageUrl],

          // Playback
          BufferOnPause = item[FieldIDs.Player.BufferOnPause] == "1",

          // Watermark
          WatermarkImageUrl = item[FieldIDs.Player.WatermarkImageUrl],
          ClickUrl = item[FieldIDs.Player.ClickUrl],
          Alpha = item[FieldIDs.Player.Alpha],
        };
    }
  }
}