namespace Sitecore.MediaFramework.Ooyala.Entities.Players
{
  using Newtonsoft.Json;

  public class Playback
  {
    [JsonProperty("buffer_on_pause")]
    public bool BufferOnPause { get; set; }
  }
}
