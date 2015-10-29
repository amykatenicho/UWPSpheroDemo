using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigWhiteBall.ViewModels
{
  // This should all really be replaced with some convention that would
  // then make these names unnecessary.
  class Constants
  {
    static Constants()
    {
      value = new Constants();
    }
    public string MainViewModelName
    {
      get
      {
        return ("main");
      }
    }
    public string MainViewModelXamlPathIndexer
    {
      get
      {
        return (MakeIndexerName(this.MainViewModelName));
      }
    }
    public string LookingForSpheroViewModelName
    {
      get
      {
        return ("looking");
      }
    }
    public string DriveSpheroViewModelName
    {
      get
      {
        return ("drive");
      }
    }
    public string LookingForSpheroViewModelXamlPathIndexer
    {
      get
      {
        return (MakeIndexerName(LookingForSpheroViewModelName));
      }
    }
    public string DriveSpheroViewModelXamlPathIndexer
    {
      get
      {
        return (MakeIndexerName(DriveSpheroViewModelName));
      }
    }
    static string MakeIndexerName(string name)
    {
      return ($"[{name}]");
    }
    public static Constants Values
    {
      get
      {
        return (value);
      }
    }
    static Constants value;
  }
}
