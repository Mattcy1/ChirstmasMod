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
using ChirstmasMod;
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
                SetMeshTexture(node, "PoppermintDiffuse", 0);
                SetMeshTexture(node, "PoppermintDiffuse", 1);
                SetMeshTexture(node, "PoppermintDiffuse", 2);
                SetMeshOutlineColor(node, Color.red, 1);
                SetMeshOutlineColor(node, Color.red, 2);
                SetMeshOutlineColor(node, Color.red, 3);
            }

        }


        [HarmonyPatch(typeof(TitleScreen), nameof(TitleScreen.Start))]
        public class TitleScreenInit
        {
            [HarmonyPostfix]

            public static void Postfix()
            {
                BloonModel CandyCaneBoss = CreateBossBase(35000, 1f);
                
                //Poppermint

                CandyCaneBoss.ApplyDisplay<BossDisplay>();


                BossRegisteration candyCaneRegisteration = new BossRegisteration(CandyCaneBoss, "CandyCaneBoss", "Poppermint", true, "PoppermintIcon", 0 ,"The Poppermint: For every 10% of its health lost, it spawns 5 Candy Cane Bloons to swarm the battlefield. Defeating this boss will grant Santa a powerful upgrade, making him stronger for the battles ahead.");
                
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
                
                BloonModel FrostyBoss = CreateBossBase(125000, 1f);
                
                //Frosty the Snowbloon
                
                BossRegisteration frostyBossRegisteration = new BossRegisteration(FrostyBoss, "Frosty", "Frosty The Snowbloon", true, "FrostyIcon", 0 ,"Frosty the Snowbloon is a chilling force. Immune to ice attacks, As it progresses, every time it loses a skull, it stuns all towers, freezing them in place. While Frosty is alive, a relentless snowstorm will rage across the battlefield, Defeating Frosty will bring you one step closer to saving Christmas!");
                
                frostyBossRegisteration.SpawnOnRound(40);
                
                HealthPercentTriggerModel health1 = Game.instance.model.GetBloon("Bloonarius1").GetBehavior<HealthPercentTriggerModel>().Duplicate();
                health1.percentageValues = new float[] { 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1f };
                health1.actionIds = new string[] { "Freeze" };

                StunTowersInRadiusActionModel stun = Game.instance.model.GetBloon("Vortex1").GetBehavior<StunTowersInRadiusActionModel>();
                stun.stunDuration = 5f;
                stun.actionId = "Freeze";
                stun.radius = 999f;
                
                FrostyBoss.AddBehavior(health1);
                FrostyBoss.AddBehavior(stun);
            }
        }
        public static void BossInit(Bloon bloon, BloonModel bloonModel, BossRegisteration registration)
        {
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
                }
                else
                {
                    this.Destroy();
                }
            }
        }
    }
}
