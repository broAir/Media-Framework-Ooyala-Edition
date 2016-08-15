namespace Sitecore.MediaFramework.Ooyala.RestSharp.Tokens
{
  using Sitecore.RestSharp.Tokens;

  public class OoyalaTimestampToken : UnixTimestampToken
  {
    /// <summary>
    /// Gets the seconds to expires.
    /// </summary>
    protected virtual int SecondsToExpires
    {
      get
      {
        return 86400;
      }
    }

    /// <summary>
    /// The get timestamp.
    /// </summary>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    protected override int GetTimestamp()
    {
      return base.GetTimestamp() + this.SecondsToExpires;
    } 
  }
}

