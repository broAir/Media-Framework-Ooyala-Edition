// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OoyalaPlayerMarkupGenerator.cs" company="Sitecore A/S">
//   Copyright (C) 2013 by Sitecore A/S
// </copyright>
// <summary>
//   The ooyala player markup provider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.MediaFramework.Ooyala.Players
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Web;

  using Newtonsoft.Json;
  using Newtonsoft.Json.Linq;

  using Sitecore.Data.Fields;
  using Sitecore.Data.Items;
  using Sitecore.MediaFramework.Account;
  using Sitecore.MediaFramework.Pipelines.MediaGenerateMarkup;
  using Sitecore.MediaFramework.Players;

  /// <summary>
  /// The ooyala player markup provider.
  /// </summary>
  public class OoyalaPlayerMarkupGenerator : PlayerMarkupGeneratorBase
  {
    private string scriptUrl;

    public string ScriptUrl
    {
      get
      {
        return scriptUrl;
      }
      set
      {
        scriptUrl = HttpUtility.UrlDecode(value);
      }
    }

    /// <summary>
    /// Generate a player markup.
    /// </summary>
    /// <param name="args">
    /// The args.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public override PlayerMarkupResult Generate(MediaGenerateMarkupArgs args)
    {
      string playerId = args.PlayerItem[FieldIDs.Player.Id];

      string id = "ooyalaplayer_" +Guid.NewGuid().ToString("N");

      var result = new PlayerMarkupResult
        {
          Html =
            string.Format(
              "<div id='{0}' style='width:{1}px;height:{2}px;display:inline-block;'></div><noscript><div>Please enable Javascript to watch this video</div></noscript>",
              id,
              args.Properties.Width,
              args.Properties.Height)
        };

      result.ScriptUrls.Add(string.Format(ScriptUrl, playerId, id));
      result.ScriptUrls.Add(this.AnalyticsScriptUrl);

      string parameters = !Context.PageMode.IsPageEditorEditing ? this.GetAdditionalParametersStr(args) : "{}";

      result.BottomScripts.Add(id, string.Format("{0}.ready(function() {{ {0}.Player.create('{0}', '{1}',{2}); }});", id, args.MediaItem[FieldIDs.MediaElement.EmbedCode], parameters));
      return result;
    }

    /// <summary>
    /// Generate a player markup.
    /// </summary>
    /// <param name="args">
    /// The properties.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public override string GetPreviewImage(MediaGenerateMarkupArgs args)
    {
      return PlayerManager.GetPreviewImage(args, FieldIDs.MediaElement.PreviewImageUrl); 
    }

    public override Item GetDefaultPlayer(MediaGenerateMarkupArgs args)
    {
      return AccountManager.GetPlayers(args.AccountItem).FirstOrDefault(player => player[FieldIDs.Player.IsDefault] == "1");
    }

    public override string GetMediaId(Item item)
    {
      return item[FieldIDs.MediaElement.EmbedCode];
    }

    protected virtual string GetAdditionalParametersStr(MediaGenerateMarkupArgs args)
    {
      string result;

      var parameters = this.GetAdditionalParameters(args);

      var serializer = new JsonSerializer();
      using (var textWriter = new StringWriter())
      {
        using (var jsonWriter = new JsonTextWriter(textWriter){ QuoteName = false })
        {
          serializer.Serialize(jsonWriter, parameters);
        }
        result = textWriter.ToString();
      }

      return result;
    }

    protected virtual Dictionary<string, object> GetAdditionalParameters(MediaGenerateMarkupArgs args)
    {
      var parameters = new Dictionary<string, object>
        {
          { "onCreate", new JRaw("ooyalaListener.subscribeEvents") }
        };

      NameValueListField field = args.PlayerItem.Fields[FieldIDs.Player.Parameters];
      if (field != null)
      {
        var collection = field.NameValues;

        foreach (string key in collection)
        {
          string value = HttpUtility.UrlDecode(collection[key]);

          parameters[key] = new JRaw(value);
        }
      }

      return parameters;
    }
  }
}