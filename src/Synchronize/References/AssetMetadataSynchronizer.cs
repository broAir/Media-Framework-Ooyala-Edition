namespace Sitecore.MediaFramework.Ooyala.Synchronize.References
{
  using System.Collections.Generic;
  using global::RestSharp;
  using Sitecore.Data.Items;
  using Sitecore.Integration.Common.Utils;
  using Sitecore.MediaFramework.Ooyala.Entities;
  using Sitecore.MediaFramework.Ooyala.Security;
  using Sitecore.MediaFramework.Synchronize.References;
  using Sitecore.RestSharp;

  public class AssetMetadataSynchronizer : StringReferenceSynchronizer<Asset>
  {
    protected override string GetReference(Asset entity, Item accountItem)
    {
      Dictionary<string, string> medatadata;

      if (entity.Metadata != null)
      {
        medatadata = entity.Metadata;
      }
      else
      {
        var parameters = new List<Parameter> { new Parameter { Name = "embedcode", Type = ParameterType.UrlSegment, Value = entity.EmbedCode } };

        var context = new RestContext(Constants.SitecoreRestSharpService, new OoyalaAthenticator(accountItem));

        medatadata = context.Read<Dictionary<string, string>>("read_asset_metadata", parameters).Data;
      }

      return medatadata != null ? StringUtil.DictionaryToString(entity.Metadata, '=', '&') : null;
    }
  }
}