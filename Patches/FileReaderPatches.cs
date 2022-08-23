using System;
using cohtml;
using System.IO;
using HarmonyLib;

namespace CVR.Lib.CoHTML.Patches {
  internal class FileReaderPatches {

    [HarmonyPatch("cohtml.FileReader, Cohtml.Runtime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", "NormalizePath")]
    class NormalizePath {
      static bool Prefix(ref string path, ref string __result) {
        if(!path.StartsWith(Constants.cohtmlPrefix, StringComparison.CurrentCulture))
          return true;

        __result = Path.Combine(Constants.customCoHTMLDirectory, path.Substring(Constants.cohtmlPrefix.Length));
        ModMain.Instance.Logger.Msg($"__result = {__result}");
        return false;
      }
    }
  }
}
