namespace Sitecore.MediaFramework.Ooyala.Indexing.Entities
{
  using System.ComponentModel;

  using Sitecore.ContentSearch;
  using Sitecore.ContentSearch.Converters;
  using Sitecore.Data;
  using Sitecore.MediaFramework.Entities;

  public class AssetSearchResult : MediaServiceSearchResult
  {
    /// <summary>
    /// Gets or sets the embed code.
    /// </summary>
    [IndexField("embedcode")]
    public string EmbedCode { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    [IndexField("name")]
    public string AssetName { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    [IndexField("description")]
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the asset type.
    /// </summary>
    [IndexField("assettype")]
    public string AssetType { get; set; }

    /// <summary>
    /// Gets or sets the created at.
    /// </summary>
    [IndexField("createdat")]
    public string CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the updated at.
    /// </summary>
    [IndexField("updatedat")]
    public string UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    [IndexField("status")]
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets the duration.
    /// </summary>                   
    [IndexField("duration")]
    public string Duration { get; set; }

    /// <summary>
    /// Gets or sets the preview image url.
    /// </summary>
    [IndexField("previewimageurl")]
    public string PreviewImageUrl { get; set; }

    /// <summary>
    /// Gets or sets the custom metadata.
    /// </summary> 
    [IndexField("custommetadata")]
    public string CustomMetadata { get; set; }

    /// <summary>
    /// Gets or sets the labels list.
    /// </summary>
    [IndexField("labelslist")]
    [TypeConverter(typeof(IndexFieldEnumerableConverter))]
    public ID[] LabelsList { get; set; }

    /// <summary>
    /// Gets or sets the player.
    /// </summary>
    [IndexField("player")]
    public string Player { get; set; }
  }
}
