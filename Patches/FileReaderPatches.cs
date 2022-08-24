using System;
using cohtml;
using System.IO;
using HarmonyLib;

namespace CVR.Lib.CoHTML.Patches {
  internal class FileReaderPatches {

    [HarmonyPatch(typeof(cohtml.FileReader), "NormalizePath")]
    class NormalizePath {
      static bool Prefix(ref string path, ref string __result) {
        if(!path.StartsWith(Constants.cohtmlPrefix, StringComparison.CurrentCulture))
          return true;

        __result = Path.Combine(Constants.customCoHTMLDirectory, path.Substring(Constants.cohtmlPrefix.Length));
        return false;
      }
    }
  }
}
