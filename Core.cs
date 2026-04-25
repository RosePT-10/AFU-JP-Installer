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
using System.Runtime.CompilerServices;
using Il2CppQuantum_Game;

[assembly: MelonInfo(typeof(JPInstaller.Core), "JPInstaller", "1.0.0", "RosePT-10", null)]
[assembly: MelonGame("Videocult", "Airframe")]

namespace JPInstaller
{
    public class Core : MelonMod
    {
        //          the great post-syntax hell purge:
        /*
        [HarmonyPatch(typeof(PhotonController), "SetCurrentRoomProperties")]
        private static class AddToRoomProperties
        {
            public static void Postfix(PhotonController __instance, PhotonHashtable properties)
            {   
                Melon<Core>.Logger.Msg("detected SetCurrentRoomProperties");
                
                //Il2CppSystem.Object test_key_boxed = new Il2CppSystem.String("buh");
                Il2CppSystem.String buh = "buh";
                Il2CppSystem.Object test_key_boxed_2 = buh;

                Il2CppSystem.Int32 test_value = default;
                System.Runtime.CompilerServices.Unsafe.AsRef(in test_value.m_value) = 99999;
                Il2CppSystem.Object test_value_boxed = test_value.BoxIl2CppObject();
            
                properties.Add(test_key_boxed_2, test_value_boxed);
                
                foreach (Il2CppSystem.Collections.DictionaryEntry dict in properties)
                {
                    string converted_string = dict.Key.ToString();
                    Melon<Core>.Logger.Msg(converted_string);

                    switch(dict.Value.GetIl2CppType().Name){
                        case "Int32":
                            Melon<Core>.Logger.Msg(dict.Value.Unbox<int>());
                            break;
                        case "Boolean":
                            Melon<Core>.Logger.Msg(dict.Value.Unbox<bool>());
                            break;
                    }

                    Melon<Core>.Logger.Msg("-----------------");
                    
                    
                }
                //Melon<Core>.Logger.Msg(__instance.FrameData.Count);
                
            }
        }

        [HarmonyPatch(typeof(PhotonController), "GetCurrentRoomProperties")]
        private static class ReadRoomProperties
        {
            public static void Postfix(PhotonController __instance, PhotonHashtable __result)
            {
                Melon<Core>.Logger.Msg("detected GetCurrentRoomProperties");
                
                foreach (Il2CppSystem.Collections.DictionaryEntry dict in __result)
                {
                    string converted_string = dict.Key.ToString();
                    Melon<Core>.Logger.Msg(converted_string);

                    switch(dict.Value.GetIl2CppType().Name){
                        case "Int32":
                            Melon<Core>.Logger.Msg(dict.Value.Unbox<int>());
                            break;
                        case "Boolean":
                            Melon<Core>.Logger.Msg(dict.Value.Unbox<bool>());
                            break;
                    }

                    Melon<Core>.Logger.Msg("-----------------");
                
                    
                }
            }
        }
        */
        [HarmonyPatch(typeof(BikeRespawnSystem), "SpawnBike")]
        private class DisableAllRacingInhibitors
        {
            public static void Postfix(Frame f, EntityRef playerEntity, PlayerRef playerRef, Transform3D spawnTransform)
            {   
                //                          ***** sharing data through a plane *****

                // first check if plane exists before doing anything
                // grab all entity refs
                Il2CppSystem.Collections.Generic.List<EntityRef> all_Erefs = new Il2CppSystem.Collections.Generic.List<EntityRef>();
                f.GetAllEntityRefs(all_Erefs);
                // grab all objects
                UnityEngine.Object airplane_object = UnityEngine.Object.FindAnyObjectByType<Airplane_View>();
                
                // check each Eref against all game objects to filter Erefs to just airplanes
                if (airplane_object != null)
                {
                    foreach (EntityRef eref in all_Erefs)
                    {
                        if (eref.ToString() == airplane_object.name)
                        {
                            Melon<Core>.Logger.Msg("airplane already exisits! not making a new airplane!");
                        }
                    }
                }
                else
                {
                    Melon<Core>.Logger.Msg("no airplane yet, making one now!");

                    // now create if it does not exist
                    EntityRef airPlane_ref = f.Create(f.GameConfig().airPlane);
                    Melon<Core>.Logger.Msg("Plane created with ID: " + airPlane_ref);
                    Airplane airPlane_comp = f.Get<Airplane>(airPlane_ref);
                    airPlane_comp.pathIndex = 69696969;
                    f.Set(airPlane_ref, airPlane_comp);
                }
                

                //                      ***** reading the shared data through a plane *****

                Airplane discovered_airplane;

                // check each Eref against all game objects to filter Erefs to just airplanes
                if (airplane_object != null)
                {
                    foreach (EntityRef eref in all_Erefs)
                    {
                        if (eref.ToString() == airplane_object.name)
                        {
                            // read airplane data
                            Melon<Core>.Logger.Msg("airplane exisits!");
                            discovered_airplane = f.Get<Airplane>(eref);
                            Melon<Core>.Logger.Msg("Plane found with id: " + eref.ToString());
                            Melon<Core>.Logger.Msg(discovered_airplane.pathIndex);
                        }
                    }
                }
                else
                {
                    Melon<Core>.Logger.Msg("airplane_object was null :[");
                }
            }
        }
        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Initialized.");
        }
    }
}