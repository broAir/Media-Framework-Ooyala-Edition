namespace Sitecore.MediaFramework.Ooyala.Synchronize.EntityCreators
{
  using Sitecore.Data.Items;
  using Sitecore.MediaFramework.Entities;
  using Sitecore.MediaFramework.Ooyala.Entities;
  using Sitecore.MediaFramework.Ooyala.Entities.Players;
  using Sitecore.MediaFramework.Ooyala.Indexing.Entities;
  using Sitecore.MediaFramework.Synchronize;

  public class PlayerEntityCreator : IMediaServiceEntityCreator
  {
    public object CreateEntity(Item item)
    {
      return new Player
        {
          Id = item[FieldIDs.Player.Id],
          DefaultClosedCaptionLanguage = item[FieldIDs.Player.DefaultLanguage],
          Name = item[FieldIDs.Player.Name],
          IsDefault = item[FieldIDs.Player.IsDefault] == "1",
          ProviderHomepageUrl = item[FieldIDs.Player.ProviderHomepageUrl],

          Branding = new Branding
            {
              AccentColor = item[FieldIDs.Player.AccentColor],
              Chromeless = item[FieldIDs.Player.Chromeless] == "1",
              DiggSharing = item[FieldIDs.Player.DiggSharing] == "1",
              EmailSharing = item[FieldIDs.Player.EmailSharing] == "1",
              EnableErrorScreen = item[FieldIDs.Player.EnableErrorScreen] == "1",
              FacebookSharing = item[FieldIDs.Player.FacebookSharing] == "1",
              ShowAdCountdown = item[FieldIDs.Player.ShowAdCountdown] == "1",
              ShowBitrateButton = item[FieldIDs.Player.ShowBitrateButton] == "1",
              ShowChannelButton = item[FieldIDs.Player.ShowChannelButton] == "1",
              ShowEmbedButton = item[FieldIDs.Player.ShowEmbedButton] == "1",
              ShowEndScreenReplayButton = item[FieldIDs.Player.ShowEndScreenReplayButton] == "1",
              ShowInfoButton = item[FieldIDs.Player.ShowInfoButton] == "1",
              ShowInfoScreenDescription = item[FieldIDs.Player.ShowInfoScreenDescription] == "1",
              ShowInfoScreenHomepageLink = item[FieldIDs.Player.ShowInfoScreenHomepageLink] == "1",
              ShowInfoScreenTitle = item[FieldIDs.Player.ShowInfoScreenTitle] == "1",
              ShowShareButton = item[FieldIDs.Player.ShowShareButton] == "1",
              ShowVolumeButton = item[FieldIDs.Player.ShowVolumeButton] == "1",
              TwitterSharing = item[FieldIDs.Player.TwitterSharing] == "1",
              UrlSharing = item[FieldIDs.Player.UrlSharing] == "1",
            },

          Playback = new Playback
            {
              BufferOnPause = item[FieldIDs.Player.BufferOnPause] == "1"
            },

          Scrubber = new Scrubber
            {
              AlwaysShow = item[FieldIDs.Player.AlwaysShow] == "1",
              ImageUrl = item[FieldIDs.Player.ScrubberImageUrl]
            },

          Watermark = new Watermark
            {
              Alpha = item[FieldIDs.Player.Alpha],
              ImageUrl = item[FieldIDs.Player.WatermarkImageUrl],
              ClickUrl = item[FieldIDs.Player.ClickUrl]
            }
        };
    }
  }
}