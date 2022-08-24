using MelonLoader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVR.Lib.CoHTML {
  public class ModMain : MelonMod {
    public static ModMain Instance { get; private set; }

    public MelonLogger.Instance Logger => Instance.LoggerInstance;

    public override void OnApplicationStart() {
      Instance = this;
      CoHTMLWrapper.OnApplicationStarted();
    }

    public override void OnPreSupportModule() {
      Helpers.CustomCoHTMLDirectory.EnsureDirectoryExists();
    }
  }
}
