namespace Sitecore.MediaFramework.Ooyala.Entities.Collections
{
  using System.Collections.Generic;
  using Newtonsoft.Json;

  public class PagedCollection<T> 
  {
    [JsonProperty("next_page")]
    public string NextPage { get; set; }

    [JsonProperty("items")]
    public List<T> Items { get; set; }
  }
}