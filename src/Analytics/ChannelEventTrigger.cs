namespace Sitecore.MediaFramework.Ooyala.Analytics
{
  using Sitecore.MediaFramework.Analytics;

  public class ChannelEventTrigger : EventTrigger
  {
    /// <summary>
    /// Inits events.
    /// </summary>
    public override void InitEvents()
    {
      this.AddEvent(TemplateIDs.Channel, PlaybackEvents.PlaybackStarted.ToString(), "Ooyala video is started.");
      this.AddEvent(TemplateIDs.Channel, PlaybackEvents.PlaybackCompleted.ToString(), "Ooyala video is completed.");
      this.AddEvent(TemplateIDs.Channel, PlaybackEvents.PlaybackChanged.ToString(), "Ooyala video progress is changed.");
      this.AddEvent(TemplateIDs.Channel, PlaybackEvents.PlaybackError.ToString(), "Ooyala video playback error.");
    }
  }
}