using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVR.Lib.CoHTML.Helpers {
  internal static class CustomCoHTMLDirectory {
    public static void EnsureDirectoryExists() {
      if(Directory.Exists(Constants.customCoHTMLDirectory + "ui/"))
        return;

      Directory.CreateDirectory(Constants.customCoHTMLDirectory + "ui/");
    }
  }
}
