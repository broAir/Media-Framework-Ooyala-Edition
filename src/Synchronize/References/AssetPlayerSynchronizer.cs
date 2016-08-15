namespace Sitecore.MediaFramework.Ooyala.Synchronize.References
{
  using System.Collections.Generic;
  using System.Linq;

  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Integration.Common.Utils;
  using Sitecore.MediaFramework.Ooyala.Entities;
  using Sitecore.MediaFramework.Ooyala.Entities.Assets;
  using Sitecore.MediaFramework.Ooyala.Indexing.Entities;
  using Sitecore.MediaFramework.Ooyala.Security;
  using Sitecore.MediaFramework.Synchronize;
  using Sitecore.MediaFramework.Synchronize.References;
  using Sitecore.RestSharp;

  using global::RestSharp;

  public class AssetPlayerSynchronizer : IdReferenceSynchronizer<Asset>
  {
    protected override List<ID> GetReference(Asset entity, Item accountItem)
    {
      var parameters = new List<Parameter> { new Parameter() { Name = "embedcode", Type = ParameterType.UrlSegment, Value = entity.EmbedCode } };

      var context = new RestContext(Constants.SitecoreRestSharpService, new OoyalaAthenticator(accountItem));
      
      var referencedPlayer = context.Read<ReferencedPlayer>("read_asset_player", parameters).Data;
      if (referencedPlayer == null)
      {
        return new List<ID>(0);
      }

      var playerIndex = ContentSearchUtil.FindOne<PlayerSearchResult>(Constants.IndexName,
        i => i.Paths.Contains(accountItem.ID) && i.TemplateId == TemplateIDs.Player && i.Id == referencedPlayer.ID);

      if (playerIndex == null)
      {
        IItemSynchronizer synchronizer = MediaFrameworkContext.GetItemSynchronizer(typeof(Player));
        if (synchronizer != null)
        {
          Player player = new Player { Id = referencedPlayer.ID };

          playerIndex = synchronizer.Fallback(player, accountItem) as PlayerSearchResult;
        }
      }

      return playerIndex != null ? new List<ID> { playerIndex.ItemId } : new List<ID>(0);
    }
  }
}