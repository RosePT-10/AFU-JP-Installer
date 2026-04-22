using MelonLoader;
using HarmonyLib;
using UnityEngine;

[assembly: MelonInfo(typeof(JPInstaller.Core), "JPInstaller", "1.0.0", "taldo", null)]
[assembly: MelonGame("Videocult", "Airframe")]

namespace JPInstaller
{
    public class Core : MelonMod
    {
        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Initialized.");
        }
    }
}