namespace Sitecore.MediaFramework.Ooyala.Synchronize
{
  using Sitecore.Data.Items;
  using Sitecore.Integration.Common.Utils;
  using Sitecore.MediaFramework.Diagnostics;
  using Sitecore.MediaFramework.Entities;
  using Sitecore.MediaFramework.Export;
  using Sitecore.MediaFramework.Ooyala.Entities;
  using Sitecore.MediaFramework.Ooyala.Indexing.Entities;
  using Sitecore.MediaFramework.Synchronize;

  public class PlayerSynchronizer : SynchronizerBase
  {
    public override Item SyncItem(object entity, Item accountItem)
    {
      var playerEntity = (Player)entity;

      if (ExportQueueManager.IsExist(accountItem, FieldIDs.Player.Id, playerEntity.Id))
      {
        return null;
      }

      return base.SyncItem(entity, accountItem);
    }

    public override Item UpdateItem(object entity, Item accountItem, Item item)
    {
      var player = (Player)entity;
      using (new EditContext(item))
      {
        item.Name = ItemUtil.ProposeValidItemName(player.Name);

        item[FieldIDs.Player.Id] = player.Id;
        item[FieldIDs.Player.IsDefault] = player.IsDefault ? "1" : "0";
        item[FieldIDs.Player.Name] = player.Name;
        item[FieldIDs.Player.ProviderHomepageUrl] = player.ProviderHomepageUrl;
        item[FieldIDs.Player.DefaultLanguage] = player.DefaultClosedCaptionLanguage;

        // Branding 
        item[FieldIDs.Player.AccentColor] = player.Branding.AccentColor;
        item[FieldIDs.Player.Chromeless] = player.Branding.Chromeless ? "1" : "0";
        item[FieldIDs.Player.DiggSharing] = player.Branding.DiggSharing ? "1" : "0";
        item[FieldIDs.Player.EmailSharing] = player.Branding.EmailSharing ? "1" : "0";
        item[FieldIDs.Player.EnableErrorScreen] = player.Branding.EnableErrorScreen ? "1" : "0";
        item[FieldIDs.Player.FacebookSharing] = player.Branding.FacebookSharing ? "1" : "0";
        item[FieldIDs.Player.ShowAdCountdown] = player.Branding.ShowAdCountdown ? "1" : "0";
        item[FieldIDs.Player.ShowBitrateButton] = player.Branding.ShowBitrateButton ? "1" : "0";
        item[FieldIDs.Player.ShowChannelButton] = player.Branding.ShowChannelButton ? "1" : "0";
        item[FieldIDs.Player.ShowEmbedButton] = player.Branding.ShowEmbedButton ? "1" : "0";
        item[FieldIDs.Player.ShowEndScreenReplayButton] = player.Branding.ShowEndScreenReplayButton ? "1" : "0";
        item[FieldIDs.Player.ShowInfoButton] = player.Branding.ShowInfoButton ? "1" : "0";
        item[FieldIDs.Player.ShowInfoScreenDescription] = player.Branding.ShowInfoScreenDescription ? "1" : "0";
        item[FieldIDs.Player.ShowInfoScreenHomepageLink] = player.Branding.ShowInfoScreenHomepageLink ? "1" : "0";
        item[FieldIDs.Player.ShowInfoScreenTitle] = player.Branding.ShowInfoScreenTitle ? "1" : "0";
        item[FieldIDs.Player.ShowShareButton] = player.Branding.ShowShareButton ? "1" : "0";
        item[FieldIDs.Player.ShowVolumeButton] = player.Branding.ShowVolumeButton ? "1" : "0";
        item[FieldIDs.Player.TwitterSharing] = player.Branding.TwitterSharing ? "1" : "0";
        item[FieldIDs.Player.UrlSharing] = player.Branding.UrlSharing ? "1" : "0";

        // Watermark
        item[FieldIDs.Player.Alpha] = player.Watermark.Alpha;
        item[FieldIDs.Player.ClickUrl] = player.Watermark.ClickUrl;
        item[FieldIDs.Player.WatermarkImageUrl] = player.Watermark.ImageUrl;

        // Playback
        item[FieldIDs.Player.BufferOnPause] = player.Playback.BufferOnPause ? "1" : "0";

        // Scrubber
        item[FieldIDs.Player.AlwaysShow] = player.Scrubber.AlwaysShow ? "1" : "0";
        item[FieldIDs.Player.ScrubberImageUrl] = player.Scrubber.ImageUrl;
      }

      return item;
    }

    public override Item GetRootItem(object entity, Item accountItem)
    {
      return accountItem.Children["Players"];
    }

    public override bool NeedUpdate(object entity, Item accountItem, MediaServiceSearchResult searchResult)
    {
      var player = (Player)entity;
      var playerIndex = (PlayerSearchResult)searchResult;
      
      if (!this.IsCommonEquals(player, playerIndex))
      {
        return true;
      }

      if (!this.IsBrandingEquals(player, playerIndex))
      {
        return true;
      }

      if (!this.IsPlaybackEquals(player, playerIndex))
      {
        return true;
      }
      
      if (!this.IsScrubberEquals(player, playerIndex))
      {
        return true;
      }
      
      if (!this.IsWatermarkEquals(player, playerIndex))
      {
        return true;
      }

      return false;
    }

    public override MediaServiceSearchResult GetSearchResult(object entity, Item accountItem)
    {
      var player = (Player)entity;
      return base.GetSearchResult<PlayerSearchResult>(Constants.IndexName, accountItem, i => i.TemplateId == TemplateIDs.Player && i.Id == player.Id);
    }

    public override MediaServiceEntityData GetMediaData(object entity)
    {
      var player = (Player)entity;

      return new MediaServiceEntityData
        {
          EntityId = player.Id,
          EntityName = player.Name,
          TemplateId = TemplateIDs.Player
        };
    }

    protected virtual bool IsCommonEquals(Player player, PlayerSearchResult playerIndex)
    {
      return StringUtil.EqualsIgnoreNullEmpty(player.Id, playerIndex.Id)
             && StringUtil.EqualsIgnoreNullEmpty(player.DefaultClosedCaptionLanguage, playerIndex.DefaultLanguage)
             && StringUtil.EqualsIgnoreNullEmpty(player.Name, playerIndex.PlayerName)
             && StringUtil.EqualsIgnoreNullEmpty(player.ProviderHomepageUrl, playerIndex.ProviderHomepageUrl);
    }

    protected virtual bool IsBrandingEquals(Player player, PlayerSearchResult playerIndex)
    {
      if (player.Branding == null)
      {
        LogHelper.Debug("player.Branding is null. Entity:" + player, this);
        return true;
      }

      return StringUtil.EqualsIgnoreNullEmpty(player.Branding.AccentColor, playerIndex.AccentColor)
             && player.Branding.Chromeless == playerIndex.Chromeless
             && player.Branding.DiggSharing == playerIndex.DiggSharing
             && player.Branding.EmailSharing == playerIndex.EmailSharing
             && player.Branding.EnableErrorScreen == playerIndex.EnableErrorScreen
             && player.Branding.FacebookSharing == playerIndex.FacebookSharing
             && player.Branding.ShowAdCountdown == playerIndex.ShowAdCountdown
             && player.Branding.ShowBitrateButton == playerIndex.ShowBitrateButton
             && player.Branding.ShowChannelButton == playerIndex.ShowChannelButton
             && player.Branding.ShowEmbedButton == playerIndex.ShowEmbedButton
             && player.Branding.ShowEndScreenReplayButton == playerIndex.ShowEndScreenReplayButton
             && player.Branding.ShowInfoButton == playerIndex.ShowInfoButton
             && player.Branding.ShowInfoScreenDescription == playerIndex.ShowInfoScreenDescription
             && player.Branding.ShowInfoScreenHomepageLink == playerIndex.ShowInfoScreenHomepageLink
             && player.Branding.ShowInfoScreenTitle == playerIndex.ShowInfoScreenTitle
             && player.Branding.ShowShareButton == playerIndex.ShowShareButton
             && player.Branding.ShowVolumeButton == playerIndex.ShowVolumeButton
             && player.Branding.TwitterSharing == playerIndex.TwitterSharing
             && player.Branding.UrlSharing == playerIndex.UrlSharing;
    }

    protected virtual bool IsPlaybackEquals(Player player, PlayerSearchResult playerIndex)
    {
      if (player.Playback == null)
      {
        LogHelper.Debug("player.Playback is null. Entity:" + player, this);
        return true;
      }

      return player.Playback.BufferOnPause == playerIndex.BufferOnPause;
    }

    protected virtual bool IsScrubberEquals(Player player, PlayerSearchResult playerIndex)
    {
      if (player.Scrubber == null)
      {
        LogHelper.Debug("player.Scrubber is null. Entity:" + player, this);
        return true;
      }

      return player.Scrubber.AlwaysShow == playerIndex.AlwaysShow
             && StringUtil.EqualsIgnoreNullEmpty(player.Scrubber.ImageUrl, playerIndex.ScrubberImageUrl);
    }

    protected virtual bool IsWatermarkEquals(Player player, PlayerSearchResult playerIndex)
    {
      if (player.Watermark == null)
      {
        LogHelper.Debug("player.Watermark is null. Entity:" + player, this);
        return true;
      }

      return StringUtil.EqualsIgnoreNullEmpty(player.Watermark.Alpha, playerIndex.Alpha)
             && StringUtil.EqualsIgnoreNullEmpty(player.Watermark.ImageUrl, playerIndex.WatermarkImageUrl)
             && StringUtil.EqualsIgnoreNullEmpty(player.Watermark.ClickUrl, playerIndex.ClickUrl);
    }
  }
}
