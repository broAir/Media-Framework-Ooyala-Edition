// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChannelExporter.cs" company="Sitecore A/S">
//   Copyright (C) 2013 by Sitecore A/S
// </copyright>
// <summary>
//   The channel exporter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.MediaFramework.Ooyala.Export
{
  using System.Collections.Generic;
  using System.Linq;

  using Sitecore.Data.Fields;
  using Sitecore.MediaFramework.Ooyala.Security;
  using Sitecore.RestSharp.Data;

  using global::RestSharp;

  using Sitecore.Data.Items;
  using Sitecore.MediaFramework.Export;
  using Sitecore.MediaFramework.Ooyala.Entities;
  using Sitecore.RestSharp;

  /// <summary>
  /// The channel exporter.
  /// </summary>
  public class ChannelExporter : AssetExporter
  {
    /// <summary>
    /// Creates a channel.
    /// </summary>
    /// <param name="operation">
    /// The operation.
    /// </param>
    /// <returns>
    /// The <see cref="object"/>.
    /// </returns>
    protected override object Create(ExportOperation operation)
    {
      var synchronizer = MediaFrameworkContext.GetItemSynchronizer(operation.Item);
      if (synchronizer == null)
      {
        return null;
      }

      var authenticator = new OoyalaAthenticator(operation.AccountItem);

      var context = new RestContext(Constants.SitecoreRestSharpService, authenticator);

      var channel = (Channel)synchronizer.CreateEntity(operation.Item);

      channel.EmbedCode = null;
      channel.CreatedAt = null;
      channel.UpdatedAt = null;
      channel.Duration = null;
      channel.PostProcessingStatus = null;
      channel.Metadata = null;
      channel.PreviewImageUrl = null;
      channel.Status = null;

      var createdChannel = context.Create<Channel, Channel>("create_channel", channel).Data;

      this.UpdateLineup(operation, createdChannel.EmbedCode);
      this.UpdateLabels(operation, createdChannel.EmbedCode);
      this.UpdatePlayer(operation, createdChannel.EmbedCode);
      this.UpdateMetadata(operation, createdChannel.EmbedCode);

      return createdChannel;
    }

    /// <summary>
    /// Deletes a channel.
    /// </summary>
    /// <param name="operation">
    /// The operation.
    /// </param>
    protected override void Delete(ExportOperation operation)
    {
      var synchronizer = MediaFrameworkContext.GetItemSynchronizer(operation.Item);
      if (synchronizer == null)
      {
        return;
      }

      var authenticator = new OoyalaAthenticator(operation.AccountItem);
      var context = new RestContext(Constants.SitecoreRestSharpService, authenticator);

      var channel = (Channel)synchronizer.CreateEntity(operation.Item);

      context.Delete<Channel, RestEmptyType>(
        "delete_channel",
        parameters:
          new List<Parameter>
            {
              new Parameter
                {
                  Name = "embedcode",
                  Type = ParameterType.UrlSegment,
                  Value = channel.EmbedCode
                }
            });
    }

    /// <summary>
    /// Updates a channel.
    /// </summary>
    /// <param name="operation">
    /// The operation.
    /// </param>
    /// <returns>
    /// The <see cref="object"/>.
    /// </returns>
    protected override object Update(ExportOperation operation)
    {
      var synchronizer = MediaFrameworkContext.GetItemSynchronizer(operation.Item);
      if (synchronizer == null)
      {
        return null;
      }

      var authenticator = new OoyalaAthenticator(operation.AccountItem);
      var context = new RestContext(Constants.SitecoreRestSharpService, authenticator);

      var channel = (Channel)synchronizer.CreateEntity(operation.Item);

      string embedCode = channel.EmbedCode;

      channel.EmbedCode = null;
      channel.CreatedAt = null;
      channel.UpdatedAt = null;
      channel.Duration = null;
      channel.PostProcessingStatus = null;
      channel.PreviewImageUrl = null;
      channel.Status = null;
      channel.AssetType = null;
      channel.Metadata = null;

      this.UpdateLineup(operation, embedCode);
      this.UpdateLabels(operation, embedCode);
      this.UpdatePlayer(operation, embedCode);
      this.UpdateMetadata(operation, embedCode);

      return context.Update<Channel, Channel>(
        "update_channel",
        channel,
        new List<Parameter>
            {
              new Parameter
                {
                  Type = ParameterType.UrlSegment, 
                  Name = "embedcode", 
                  Value = embedCode
                }
            }).Data;
    }

    /// <summary>
    /// Updates a lineup.
    /// </summary>
    /// <param name="operation">
    /// The operation.
    /// </param>
    /// <param name="channelEmbedCode">
    /// The channel Embed Code.
    /// </param>
    /// <returns>
    /// The <see cref="List"/>.
    /// </returns>
    protected virtual List<string> UpdateLineup(ExportOperation operation, string channelEmbedCode)
    {
      var authenticator = new OoyalaAthenticator(operation.AccountItem);
      var context = new RestContext(Constants.SitecoreRestSharpService, authenticator);

      List<string> lineup = this.GetLineup(operation.Item);

      return context.Update<List<string>, List<string>>(
        "update_channel_lineup",
        lineup,
        new List<Parameter>
            {
              new Parameter
                {
                  Type = ParameterType.UrlSegment, 
                  Name = "embedcode", 
                  Value = channelEmbedCode
                }
            }).Data;
    }

    /// <summary>
    /// Gets a lineup for a channel item.
    /// </summary>
    /// <param name="channel">
    /// The channel.
    /// </param>
    /// <returns>
    /// The <see cref="Lineup"/>.
    /// </returns>
    protected virtual List<string> GetLineup(Item channel)
    {
      MultilistField field = channel.Fields[FieldIDs.MediaElement.Channel.VideoList];
      
      return field.GetItems().Select(item => item[FieldIDs.MediaElement.EmbedCode]).ToList();
    }
  }
}