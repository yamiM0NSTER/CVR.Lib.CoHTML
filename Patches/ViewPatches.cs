using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVR.Lib.CoHTML.Patches {
  internal class ViewPatches {

    [HarmonyPatch("cohtml.Net.View, cohtml.Net, Version=1.17.0.1, Culture=neutral, PublicKeyToken=06c400a284f0f0fd", "LoadURL")]
    class LoadURL {
      static bool Prefix(ref string url) {
        if(!url.StartsWith(Constants.cohtmlPrefix, StringComparison.CurrentCulture))
          return true;

        url = Path.Combine(Constants.customCoHTMLDirectory, url.Substring(Constants.cohtmlPrefix.Length));
        return true;
      }
    }
  }
}
