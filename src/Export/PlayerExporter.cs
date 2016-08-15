// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerExporter.cs" company="Sitecore A/S">
//   Copyright (C) 2013 by Sitecore A/S
// </copyright>
// <summary>
//   The player exporter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.MediaFramework.Ooyala.Export
{
  using System.Collections.Generic;

  using Sitecore.Data.Items;
  using Sitecore.MediaFramework.Ooyala.Security;
  using Sitecore.RestSharp;
  using Sitecore.RestSharp.Data;

  using global::RestSharp;

  using Sitecore.MediaFramework.Export;
  using Sitecore.MediaFramework.Ooyala.Entities;

  /// <summary>
  /// The player exporter.
  /// </summary>
  public class PlayerExporter : ExportExecuterBase
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
      return item[FieldIDs.Player.Id].Length == 0;
    }

    /// <summary>
    /// Creates a player.
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

      var player = (Player)synchronizer.CreateEntity(operation.Item);

      player.Id = null;
      player.IsDefault = false;

      return context.Create<Player, Player>("create_player", player).Data;
    }

    /// <summary>
    /// Deletes a player.
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
      var player = (Player)synchronizer.CreateEntity(operation.Item);

      context.Delete<Player, RestEmptyType>(
        "delete_player",
        parameters:
        new List<Parameter>
            {
              new Parameter
                {
                  Type = ParameterType.UrlSegment, 
                  Name = "id", 
                  Value = player.Id
                }
            });
    }

    /// <summary>
    /// Updates a player.
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

      var player = (Player)synchronizer.CreateEntity(operation.Item);

      string playerId = player.Id;

      player.Id = null;

      return context.Update<Player, Player>(
        "update_player",
        player,
        new List<Parameter>
            {
              new Parameter
                {
                  Type = ParameterType.UrlSegment, 
                  Name = "id", 
                  Value = playerId
                }
            }).Data;
    }
  }
}