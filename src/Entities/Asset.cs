namespace Sitecore.MediaFramework.Ooyala.Entities
{
  using System.Collections.Generic;

  using Newtonsoft.Json;

  public class Asset
  {  
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the embed code.
    /// </summary>
    [JsonProperty("embed_code", NullValueHandling = NullValueHandling.Ignore)]
    public string EmbedCode { get; set; }

    /// <summary>
    /// Gets or sets the asset type.
    /// </summary>
    [JsonProperty("asset_type", NullValueHandling = NullValueHandling.Ignore)]
    public string AssetType { get; set; }

    /// <summary>
    /// Gets or sets the created at.
    /// </summary>
    [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
    public string CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the updated at.
    /// </summary>
    [JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore)]
    public string UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    [JsonProperty("post_processing_status", NullValueHandling = NullValueHandling.Ignore)]
    public string PostProcessingStatus { get; set; }

    /// <summary>
    /// Gets or sets the duration.
    /// </summary>
    [JsonProperty("duration", NullValueHandling = NullValueHandling.Ignore)]
    public string Duration { get; set; }

    /// <summary>
    /// Gets or sets the preview image url.
    /// </summary>
    [JsonProperty("preview_image_url", NullValueHandling = NullValueHandling.Ignore)]
    public string PreviewImageUrl { get; set; }

    [JsonProperty("labels")]
    public List<Label> Labels { get; set; }

    /// <summary>
    /// Gets or sets the ooyala metadata.
    /// </summary>
    [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
    public Dictionary<string, string> Metadata { get; set; }

    public override string ToString()
    {
      return string.Format("(type:{0},embed code:{1},name:{2})", this.GetType().Name, this.EmbedCode, this.Name);
    }
  }
}