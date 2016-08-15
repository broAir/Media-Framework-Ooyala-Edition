namespace Sitecore.MediaFramework.Ooyala.Security
{
  using System;
  using System.Linq;
  using System.Security.Cryptography;
  using System.Text;
  using System.Web;

  using Sitecore.Data.Items;

  using global::RestSharp;
  using global::RestSharp.Extensions;

  public class OoyalaAthenticator : IAuthenticator
  {
    public OoyalaAthenticator(Item accountItem)
    {
      this.ApiKey = accountItem[FieldIDs.Account.ApiKey];
      this.ApiSecret = accountItem[FieldIDs.Account.ApiSecret];
    }

    /// <summary>
    /// The api secret.
    /// </summary>
    protected readonly string ApiSecret;

    /// <summary>
    /// The api key.
    /// </summary>
    protected readonly string ApiKey;

    public void Authenticate(IRestClient client, IRestRequest request)
    {
      request.AddUrlSegment("apiKey", this.ApiKey);
      request.AddUrlSegment("signature", this.GetSignature(client, request));
    }

    protected virtual string GetSignature(IRestClient client, IRestRequest request)
    {
      Uri uri = client.BuildUri(request);
      var builder = new StringBuilder(this.ApiSecret + request.Method + uri.AbsolutePath, 128);

      this.AddParameters(builder, uri);

      Parameter parameter = request.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);

      if (parameter != null)
      {
        builder.Append(parameter.Value);
      }

      var sha256 = new SHA256Managed();
      byte[] digest = sha256.ComputeHash(Encoding.ASCII.GetBytes(builder.ToString()));
      string signedInput = Convert.ToBase64String(digest);

      var signature = signedInput.TrimEnd(new[] { '=' }).Substring(0, 43);
      return signature;
    }

    protected virtual void AddParameters(StringBuilder builder, Uri uri)
    {
      var collection = HttpUtility.ParseQueryString(uri.Query.UrlDecode());

      string[] keys = collection.AllKeys;
      Array.Sort(keys);

      foreach (var key in keys)
      {
        if (key == "signature")
        {
          continue;
        }

        string value = collection[key];

        builder.Append(key + "=" + value);
      }
    }
  }
}
