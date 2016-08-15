namespace Sitecore.MediaFramework.Ooyala.Synchronize.Fallback
{
  using Sitecore.Data.Items;
  using Sitecore.MediaFramework.Ooyala.Entities;
  using Sitecore.MediaFramework.Ooyala.Indexing.Entities;

  public class ChannelFallback : AssetFallback<ChannelSearchResult>
  {
    protected override Item GetItem(object entity, Item accountItem)
    {
      Channel video = (Channel)entity;

      return accountItem.Axes.SelectSingleItem(string.Format("./Media Content//*[@@templateid='{0}' and @embedcode='{1}']", TemplateIDs.Channel, video.EmbedCode));
    }
  }
}