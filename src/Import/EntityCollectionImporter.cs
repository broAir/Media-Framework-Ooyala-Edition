namespace Sitecore.MediaFramework.Ooyala.Import
{
  using System.Collections.Generic;
  using System.Collections.Specialized;
  using System.Web;

  using Sitecore.Data.Items;
  using Sitecore.MediaFramework.Diagnostics;
  using Sitecore.MediaFramework.Import;
  using Sitecore.MediaFramework.Ooyala.Entities.Collections;
  using Sitecore.MediaFramework.Ooyala.Security;
  using Sitecore.RestSharp;

  using global::RestSharp;

  using global::RestSharp.Extensions;

  public abstract class EntityCollectionImporter<TEntity> : IImportExecuter
  {
    protected abstract string RequestName { get; }

    public virtual IEnumerable<object> GetData(Item accountItem)
    {
      var authenticator = new OoyalaAthenticator(accountItem);

      return this.GetWithPaging(authenticator);
    }

    protected virtual IEnumerable<object> GetWithPaging(IAuthenticator authenticator)
    {
      var context = new RestContext(Constants.SitecoreRestSharpService, authenticator);
      
      string nextPage = null;

      var parameters = new List<Parameter>();

      var pageToken = new Parameter { Type = ParameterType.UrlSegment, Name = "page_token", Value = string.Empty };

      parameters.Add(pageToken);

      string requestName = this.RequestName;
      do
      {
        if (!string.IsNullOrEmpty(nextPage))
        {
          NameValueCollection qscoll = HttpUtility.ParseQueryString(nextPage.Split('?')[1]);
          pageToken.Value = string.Format(qscoll["page_token"].UrlDecode().UrlEncode());
        }
        else
        {
          pageToken.Value = string.Empty;
        }

        var temp = context.Read<PagedCollection<TEntity>>(requestName, parameters);
       
        if (temp == null || temp.Data == null || temp.Data.Items == null)
        {
          LogHelper.Warn("Null Result during importing", this);

          throw new HttpException("Http null result");
        }

        nextPage = temp.Data.NextPage != nextPage ? temp.Data.NextPage : null;

        foreach (TEntity entity in temp.Data.Items)
        {
          yield return entity;
        }
      }
      while (!string.IsNullOrEmpty(nextPage));
    }
  }
}
