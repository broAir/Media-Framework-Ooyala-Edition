// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssetExporter.cs" company="Sitecore A/S">
//   Copyright (C) 2013 by Sitecore A/S
// </copyright>
// <summary>
//   The asset exporter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.MediaFramework.Ooyala.Export
{
  using System.Collections.Generic;
  using System.Linq;

  using Sitecore.Data.Fields;
  using Sitecore.MediaFramework.Ooyala.Entities;
  using Sitecore.MediaFramework.Ooyala.Entities.Collections;
  using Sitecore.MediaFramework.Ooyala.Security;
  using Sitecore.RestSharp.Data;

  using global::RestSharp;

  using Sitecore.Data.Items;
  using Sitecore.MediaFramework.Export;
  using Sitecore.RestSharp;

  using Sitecore.Integration.Common.Utils;

  /// <summary>
  /// The asset exporter.
  /// </summary>
  public abstract class AssetExporter : ExportExecuterBase
  {
    /// <summary>
    /// Checks if an item is new for export.
    /// </summary>
    /// <param name="item">
    /// The item.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public override bool IsNew(Item item)
    {
      return item[FieldIDs.MediaElement.EmbedCode].Length == 0;
    }

    /// <summary>
    /// Gets labels which are assigned to the asset.
    /// </summary>
    /// <param name="asset">
    /// The asset.
    /// </param>
    /// <returns>
    /// The <see cref="List"/>.
    /// </returns>
    protected virtual List<string> GetLabels(Item asset)
    {
      MultilistField field = asset.Fields[FieldIDs.MediaElement.LabelsList];

      return field.GetItems().Select(item => item[FieldIDs.Label.Id]).ToList();
    }

    /// <summary>
    /// Gets player ID.
    /// </summary>
    /// <param name="asset">
    /// The asset.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    protected virtual string GetPlayerId(Item asset)
    {
      ReferenceField field = asset.Fields[FieldIDs.MediaElement.Player];

      Item targetItem = field.TargetItem;

      return targetItem != null ? targetItem[FieldIDs.Player.Id] : string.Empty;
    }

    /// <summary>
    /// Gets metadata.
    /// </summary>
    /// <param name="asset">
    /// The asset.
    /// </param>
    /// <returns>
    /// The <see cref="Dictionary"/>.
    /// </returns>
    protected virtual Dictionary<string, string> GetMetadata(Item asset)
    {
      return StringUtil.GetDictionary(asset[FieldIDs.MediaElement.CustomMetadata], '=', '&');
    }

    /// <summary>
    /// Updates list of the assigned labels.
    /// </summary>
    /// <param name="operation">
    /// The operation.
    /// </param>
    /// <param name="assetEmbedCode">
    /// The asset embed code.
    /// </param>
    protected virtual void UpdateLabels(ExportOperation operation, string assetEmbedCode)
    {
      var authenticator = new OoyalaAthenticator(operation.AccountItem);
      var context = new RestContext(Constants.SitecoreRestSharpService, authenticator);

      List<string> labels = this.GetLabels(operation.Item);

      context.Update<List<string>, PagedCollection<Label>>(
      "update_labels_of_asset",
      labels,
      new List<Parameter>
          {
            new Parameter { Type = ParameterType.UrlSegment, Name = "embedcode", Value = assetEmbedCode }
          });
    }

    /// <summary>
    /// Updates player for an asset.
    /// </summary>
    /// <param name="operation">
    /// The operation.
    /// </param>
    /// <param name="assetEmbedCode">
    /// The asset embed code.
    /// </param>
    protected virtual void UpdatePlayer(ExportOperation operation, string assetEmbedCode)
    {
      var authenticator = new OoyalaAthenticator(operation.AccountItem);
      var context = new RestContext(Constants.SitecoreRestSharpService, authenticator);

      string playerId = this.GetPlayerId(operation.Item);

      context.Update<RestEmptyType, RestEmptyType>(
        "update_player_of_asset",
        null,
        new List<Parameter>
            {
              new Parameter
                {
                  Type = ParameterType.UrlSegment,
                  Name = "embedcode",
                  Value = assetEmbedCode
                },

                new Parameter
                {
                  Type = ParameterType.UrlSegment,
                  Name = "id",
                  Value = playerId
                }
            });
    }

    /// <summary>
    /// Updates metadata for an asset.
    /// </summary>
    /// <param name="operation">
    /// The operation.
    /// </param>
    /// <param name="assetEmbedCode">
    /// The asset embed code.
    /// </param>
    /// <returns>
    /// The <see cref="Dictionary"/>.
    /// </returns>
    protected virtual Dictionary<string, string> UpdateMetadata(ExportOperation operation, string assetEmbedCode)
    {
      var authenticator = new OoyalaAthenticator(operation.AccountItem);
      var context = new RestContext(Constants.SitecoreRestSharpService, authenticator);

      Dictionary<string, string> metadata = this.GetMetadata(operation.Item);

      return context.Update<Dictionary<string, string>, Dictionary<string, string>>(
        "update_metadata_of_asset",
        metadata,
        new List<Parameter>
            {
              new Parameter
                {
                  Type = ParameterType.UrlSegment, 
                  Name = "embedcode", 
                  Value = assetEmbedCode
                }
            }).Data;
    }
  }
}