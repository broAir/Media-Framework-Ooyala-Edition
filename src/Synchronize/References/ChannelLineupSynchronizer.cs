namespace Sitecore.MediaFramework.Ooyala.Synchronize.References
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Linq.Expressions;

  using Sitecore.ContentSearch.Linq.Utilities;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Integration.Common.Utils;
  using Sitecore.MediaFramework.Ooyala.Entities;
  using Sitecore.MediaFramework.Ooyala.Indexing.Entities;
  using Sitecore.MediaFramework.Ooyala.Security;
  using Sitecore.MediaFramework.Synchronize;
  using Sitecore.MediaFramework.Synchronize.References;
  using Sitecore.RestSharp;

  using global::RestSharp;

  using Constants = Sitecore.MediaFramework.Ooyala.Constants;

  public class ChannelLineupSynchronizer : IdReferenceSynchronizer<Channel>
  {
    protected override List<ID> GetReference(Channel entity, Item accountItem)
    {
      var parameters = new List<Parameter> { new Parameter() { Name = "embedcode", Type = ParameterType.UrlSegment, Value = entity.EmbedCode } };

      var context = new RestContext(Constants.SitecoreRestSharpService, new OoyalaAthenticator(accountItem));
      List<string> embedCodes = context.Read<List<string>>("read_channel_lineup", parameters).Data;

      if (embedCodes == null)
      {
        return null;
      }

      Expression<Func<VideoSearchResult, bool>> expression = ContentSearchUtil.GetAncestorFilter<VideoSearchResult>(accountItem, TemplateIDs.Video);

      var embedCodesExp = embedCodes.Aggregate(PredicateBuilder.False<VideoSearchResult>(), (current, tmp) => current.Or(i => i.EmbedCode == tmp.Replace('-', ' ')));

      List<VideoSearchResult> searchResults = ContentSearchUtil.FindAll(Constants.IndexName, expression.And(embedCodesExp));

      //fallback
      if (searchResults.Count < embedCodes.Count)
      {
        IItemSynchronizer synchronizer = MediaFrameworkContext.GetItemSynchronizer(typeof(Video));
        if (synchronizer != null)
        {
          foreach (string embedCode in embedCodes)
          {
            if (searchResults.Any(i => i.EmbedCode == embedCode))
            {
              continue;
            }

            Video video = new Video { EmbedCode = embedCode };
            var videoIndex = synchronizer.Fallback(video, accountItem) as VideoSearchResult;

            if (videoIndex != null)
            {
              searchResults.Add(videoIndex);
            }
          }
        }
      }

      return searchResults.Select(i => i.ItemId).ToList();
    }
  }
}