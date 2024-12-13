using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Bloons;
using Il2CppAssets.Scripts.Unity.Scenes;
using Il2CppAssets.Scripts.Unity;
using static BossHandlerNamespace.BossHandler;
using Harmony;
using BTD_Mod_Helper.Extensions;
using MelonLoader;
using UnityEngine;
using BTD_Mod_Helper.Api.Display;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppSystem;
using Il2CppAssets.Scripts.Simulation.Bloons;
using System.Runtime.InteropServices;
using BTD_Mod_Helper.Api;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Abilities.Behaviors;

namespace BossHandlerNamespace
{


    internal class Bosses
    {

        class BossDisplay : ModDisplay
        {
            public override string BaseDisplay => Game.instance.model.GetBloon("Bad").display.guidRef;


            public override void ModifyDisplayNode(UnityDisplayNode node)
            { 
                SetMeshTexture(node, "FirstTexture", 0);
                SetMeshTexture(node, "FirstTexture", 1);
                SetMeshTexture(node, "FirstTexture", 2);
            }

        }


        [HarmonyPatch(typeof(TitleScreen), nameof(TitleScreen.Start))]
        public class TitleScreenInit
        {
            [HarmonyPostfix]

            public static void Postfix()
            {
                BloonModel CandyCaneBoss = CreateBossBase(35000, 1f);

                CandyCaneBoss.ApplyDisplay<BossDisplay>();


                BossRegisteration candyCaneRegisteration = new BossRegisteration(CandyCaneBoss, "CandyCaneBoss", "The Poppermint", true, "CandyCaneIcon", 0 ,"The Poppermint: For every 10% of its health lost, it spawns 5 Candy Cane Bloons to swarm the battlefield. Defeating this boss will grant Santa a powerful upgrade, making him stronger for the battles ahead.");
                
                candyCaneRegisteration.SpawnOnRound(20);

                
                HealthPercentTriggerModel health = Game.instance.model.GetBloon("Bloonarius1").GetBehavior<HealthPercentTriggerModel>().Duplicate();
                health.percentageValues = new float[] { 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1f };
                health.actionIds = new string[] { "SpawnBloons" };
                
                SpawnBloonsActionModel spawn = Game.instance.model.GetBloon("Bloonarius1").GetBehavior<SpawnBloonsActionModel>().Duplicate();
                spawn.bloonType = "Rainbow";
                spawn.actionId = "SpawnBloons";
                spawn.spawnCount = 5;
                spawn.spawnTrackMin = 0.3f;
                spawn.spawnTrackMax = 0.5f;
                spawn.bossName = "Phayze";
                
                CandyCaneBoss.AddBehavior(health);
                CandyCaneBoss.AddBehavior(spawn);
            }
        }
        public static void BossInit(Bloon bloon, BloonModel bloonModel, BossRegisteration registration)
        {
            // This function runs when a boss is spawned. The parameters include the boss and its registration info
            // You can put code here to spawn minions, effects,  start a monobehavior, etc


      
        }

        [RegisterTypeInIl2Cpp]
        public class MonoBehaviorTemplate : MonoBehaviour
        {
            public Bloon boss;
     
            public MonoBehaviorTemplate() : base()
            {

            }
            public void Start()
            {



            }

            public void Update()
            {
             

                if(boss != null)
                {
                    // If the boss is active, do stuff
                }
                else
                {
                    // If the bloon is destroyed, end this Monobehavior. 
                    this.Destroy();
                }
            }


        }


    }


    
}
