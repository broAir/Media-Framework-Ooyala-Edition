namespace Sitecore.MediaFramework.Ooyala.Import
{
  using System.Collections.Generic;
  using System.Linq;

  using Sitecore.Data.Items;
  using Sitecore.MediaFramework.Ooyala.Entities;

  public class LabelCollectionImporter : EntityCollectionImporter<Label>
  {
    protected static readonly TreeCompare Comparer = new TreeCompare();

    protected override string RequestName
    {
      get
      {
        return "read_labels";
      }
    }

    public override IEnumerable<object> GetData(Item accountItem)
    {
      return base.GetData(accountItem).OfType<Label>().OrderBy(i => i.FullName, Comparer);
    }

    protected  class TreeCompare : IComparer<string>
    {
      public int Compare(string x, string y)
      {
        int countX = x.Count(f => f == '/');
        int countY = y.Count(f => f == '/');

        if (countX > countY)
          return 1;
        if (countX < countY)
          return -1;
        
        return 0;
      }
    }
  }
}