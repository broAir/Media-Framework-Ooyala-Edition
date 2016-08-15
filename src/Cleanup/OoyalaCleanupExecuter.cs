namespace Sitecore.MediaFramework.Ooyala.Cleanup
{
  using System;

  using Sitecore.MediaFramework.Cleanup;
  using Sitecore.MediaFramework.Entities;

  [Obsolete("Use Sitecore.MediaFramework.Cleanup.CleanupExecuterBase class")]
  public abstract class OoyalaCleanupExecuter<TEntity, TSearchResult> : CleanupExecuterBase<TEntity, TSearchResult>
    where TSearchResult : MediaServiceSearchResult, new()
  {
  }
}