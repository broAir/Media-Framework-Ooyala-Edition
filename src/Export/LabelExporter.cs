// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelExporter.cs" company="Sitecore A/S">
//   Copyright (C) 2013 by Sitecore A/S
// </copyright>
// <summary>
//   The label exporter.
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
  /// The label exporter.
  /// </summary>
  public class LabelExporter : ExportExecuterBase
  {
    /// <summary>
    /// Updates a label item on sitecore.
    /// </summary>
    /// <param name="item">
    /// The item.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public override bool IsNew(Item item)
    {
      return item[FieldIDs.Label.Id].Length == 0;
    }

    /// <summary>
    /// Creates a label.
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

      var label = (Label)synchronizer.CreateEntity(operation.Item);

      return context.Create<Label, Label>(
        "create_label",
        new Label
        {
          Name = label.Name,
          ParentId = !string.IsNullOrEmpty(label.ParentId) ? label.ParentId : "root" 
        }).Data;
    }

    /// <summary>
    /// Deletes a label.
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

      var label = (Label)synchronizer.CreateEntity(operation.Item);

      context.Delete<Label, RestEmptyType>(
        "delete_label",
        parameters:
        new List<Parameter>
            {
              new Parameter
                {
                  Type = ParameterType.UrlSegment, 
                  Name = "id", 
                  Value = label.Id
                }
            });
    }

    /// <summary>
    /// Update a label.
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
      
      var lab = (Label)synchronizer.CreateEntity(operation.Item);

      return context.Update<Label, Label>(
        "update_label",
        new Label { Name = lab.Name, ParentId = !string.IsNullOrEmpty(lab.ParentId) ? lab.ParentId : "root" },
        new List<Parameter>
            {
              new Parameter
                {
                  Type = ParameterType.UrlSegment, 
                  Name = "id", 
                  Value = lab.Id
                }
            }).Data;
    }

    protected override object Move(ExportOperation operation)
    {
      return this.Update(operation);
    }
  }
}