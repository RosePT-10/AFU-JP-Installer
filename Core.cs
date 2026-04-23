using MelonLoader;
using HarmonyLib;
using UnityEngine;
using Il2CppPhoton.Deterministic.Protocol;
using Il2CppPhoton.Deterministic;
using Il2CppQuantum;
using Il2Cpp;
using Il2CppPhoton.Client;
using System.Reflection.PortableExecutable;
using System.Collections;
using Il2CppPhoton.Realtime;
using Unity.Collections;
using AssetsTools.NET.Extra;
using Il2CppSystem.Threading.Tasks;
using Il2CppInterop.Runtime.Runtime;

[assembly: MelonInfo(typeof(JPInstaller.Core), "JPInstaller", "1.0.0", "RosePT-10", null)]
[assembly: MelonGame("Videocult", "Airframe")]

namespace JPInstaller
{
    public class Core : MelonMod
    {
        [HarmonyPatch(typeof(PhotonController), "SetCurrentRoomProperties")]
        private static class AddToRoomProperties
        {
            public static void Postfix(PhotonController __instance, PhotonHashtable properties)
            {   
                Melon<Core>.Logger.Msg("detected SetCurrentRoomProperties");

                string test_key = "buh";
                int test_value = 999;
                Il2CppSystem.Object test_key_boxed = test_key;
                Il2CppSystem.Object test_value_boxed = test_value;
                
                new Il2CppSystem.Int32 {m_value = 999}.BoxIl2CppObject();
                
            
                properties.Add(new Il2CppSystem.Int32 {m_value = 999}.BoxIl2CppObject(), test_value_boxed);
                //Melon<Core>.Logger.Msg(__instance.FrameData.Count);
                
            }
        }

        [HarmonyPatch(typeof(PhotonController), "GetCurrentRoomProperties")]
        private static class ReadRoomProperties
        {
            public static void Postfix(PhotonController __instance, PhotonHashtable properties)
            {
                Melon<Core>.Logger.Msg("detected GetCurrentRoomProperties");
                //Melon<Core>.Logger.Msg(__instance.FrameData.Count);
                foreach (Il2Cpp.RoomPlayerInfo dict in __instance.currentRoomPlayerInfosStorage)
                {
                    //Melon<Core>.Logger.Msg(dict.name);
                    //Melon<Core>.Logger.Msg($"{dict.name} : {dict.minValue} - {dict.maxValue}");
                    //Melon<Core>.Logger.Msg($"{dict.player} : {dict}");
                    
                }
                foreach (Il2CppSystem.Collections.DictionaryEntry dict in properties)
                {
                    Melon<Core>.Logger.Msg(dict.Value.Unbox<int>());
                    
                }
            }
        }

        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Initialized.");
        }
    }
}