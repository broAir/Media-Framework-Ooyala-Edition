namespace Sitecore.MediaFramework.Ooyala.Entities
{
  using Newtonsoft.Json;

  using Sitecore.Data;

  public class Label
  {
    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the full name.
    /// </summary>
    [JsonProperty("full_name", NullValueHandling = NullValueHandling.Ignore)]
    public string FullName { get; set; }

    /// <summary>
    /// Gets or sets the parent id.
    /// </summary>
    [JsonProperty("parent_id", NullValueHandling = NullValueHandling.Ignore)]
    public string ParentId { get; set; }

    public override string ToString()
    {
      return string.Format("(type:{0},id:{1},name:{2})", this.GetType().Name,this.Id, this.Name);
    }
  }
}
