// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VideoToUpload.cs" company="Sitecore A/S">
//   Copyright (C) 2013 by Sitecore A/S
// </copyright>
// <summary>
//   The video to upload.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.MediaFramework.Ooyala.Entities
{
  using Newtonsoft.Json;

  /// <summary>
  /// The video to upload.
  /// </summary>
  public class VideoToUpload
  {
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the file name.
    /// </summary>
    [JsonProperty("file_name", NullValueHandling = NullValueHandling.Ignore)]
    public string FileName { get; set; }

    /// <summary>
    /// Gets or sets the asset type.
    /// </summary>
    [JsonProperty("asset_type", NullValueHandling = NullValueHandling.Ignore)]
    public string AssetType { get; set; }

    /// <summary>
    /// Gets or sets the file size.
    /// </summary>
    [JsonProperty("file_size", NullValueHandling = NullValueHandling.Ignore)]
    public long FileSize { get; set; }

    /// <summary>
    /// Gets or sets the chunk size.
    /// </summary>
    [JsonProperty("chunk_size", NullValueHandling = NullValueHandling.Ignore)]
    public long ChunkSize { get; set; }

    /// <summary>
    /// Gets or sets the post processing status.
    /// </summary>
    [JsonProperty("post_processing_status", NullValueHandling = NullValueHandling.Ignore)]
    public string PostProcessingStatus { get; set; }
  }
}