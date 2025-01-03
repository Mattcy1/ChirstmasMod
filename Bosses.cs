using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Extensions;
using ChristmasMod;
using HarmonyLib;
using Il2CppAssets.Scripts.Data.Behaviors.Weapons;
using Il2CppAssets.Scripts.Models.Bloons;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Simulation;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Simulation.Bloons.Behaviors;
using Il2CppAssets.Scripts.Simulation.SMath;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppAssets.Scripts.Unity.Scenes;
using Il2CppAssets.Scripts.Unity.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using MelonLoader;
using ChristmasMod.Moabs;
using ChristmasMod.UI;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using static BossHandlerNamespace.BossHandler;
using AddBerserkerBrewToProjectile = Il2CppAssets.Scripts.Data.Behaviors.Projectiles.AddBerserkerBrewToProjectile;
using Color = UnityEngine.Color;
using Math = Il2CppAssets.Scripts.Simulation.SMath.Math;
using Vector2 = Il2CppAssets.Scripts.Simulation.SMath.Vector2;
using Vector3 = Il2CppAssets.Scripts.Simulation.SMath.Vector3;
using BTD_Mod_Helper;
using Il2CppInterop.Runtime.Attributes;

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
            }

        }

        class FrostyDisplay : ModCustomDisplay
        {
            public override string AssetBundleName => "christmas2024";

            public override string PrefabName => "FrostyBoss";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                foreach(var renderer in node.GetMeshRenderers())
                {
                    renderer.ApplyOutlineShader();

                    renderer.SetMainTexture(GetTexture("FrostyBossBody"));

                    renderer.SetOutlineColor(new(0.4f, 0.4f, 0.4f));

                    switch (renderer.name)
                    {
                        case "Hat" or "Stones":
                            renderer.SetOutlineColor(Color.black);
                            renderer.SetMainTexture(GetTexture("FrostyParts"));
                            break;
                        case "Carrot":
                            renderer.SetOutlineColor(new(0.65f, 0.25f, 0));
                            renderer.SetMainTexture(GetTexture("FrostyParts"));
                            break;
                        case "Arms" or "Arms.001":
                            renderer.SetOutlineColor(new(0.25f, 0.12f, 0));
                            renderer.SetMainTexture(GetTexture("FrostyParts"));
                            break;
                        case "Scarf":
                            renderer.SetOutlineColor(new(0.8f, 0, 0));
                            renderer.SetMainTexture(GetTexture("FrostyParts"));
                            break;
                    }
                }
            }
        }

        [HarmonyPatch(typeof(HealthPercentTrigger), nameof(HealthPercentTrigger.Trigger))]
        static class HealthPercentTrigger_Trigger
        {
            [HarmonyPostfix]
            public static void Postfix(HealthPercentTrigger __instance)
            {
                var bloon = __instance.bloon;
                var bm = bloon.bloonModel;

                string[] acceptedIds = ["Frosty"];

                if (acceptedIds.Contains(bm.baseId))
                {
                    bloon.GetUnityDisplayNode().GetComponent<Animator>().SetTrigger("Skull");
                    ModHelper.Log<ChristmasMod.ChristmasMod>("Skull");
                }
            }
        }

        class CookieMonsterDisplay : ModCustomDisplay
        {
            public override string AssetBundleName => "christmas2024";

            public override string PrefabName => "CookieMonsterNormalAnimated";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {

                foreach (var renderer in node.GetMeshRenderers())
                {
                    renderer.ApplyOutlineShader();
                    renderer.SetOutlineColor(new(0.34f, .21f, .11f));
                }
            }
        }

        class CookieMonsterAngryDisplay : ModCustomDisplay
        {
            public override string AssetBundleName => "christmas2024";

            public override string PrefabName => "CookieMonsterAngry";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {

                foreach (var renderer in node.GetMeshRenderers())
                {
                    renderer.ApplyOutlineShader();
                    renderer.SetOutlineColor(new(0.34f, .21f, .11f));
                }
            }
        }

        class GrinchDisplay : ModCustomDisplay
        {
            public override string AssetBundleName => "christmas2024";

            public override string PrefabName => "Grinch-o-MaticAnimated";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {

                foreach (var renderer in node.GetMeshRenderers())
                {
                    renderer.ApplyOutlineShader();
                    renderer.SetOutlineColor(Color.green);
                }
            }
        }
        
        class AngryGrinchDisplay : ModCustomDisplay
        {
            public override string AssetBundleName => "christmas2024";

            public override string PrefabName => "Grinch-O-Mega-MaticAnimated";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {

                foreach (var renderer in node.GetMeshRenderers())
                {
                    node.transform.localScale *= 0.3f;
                    renderer.ApplyOutlineShader();
                    renderer.SetOutlineColor(Color.green);
                }
            }
        }
        
        class CrumblyDisplay : ModCustomDisplay
        {
            public override string AssetBundleName => "christmas2024";

            public override string PrefabName => "CrumblyAnimated";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                foreach (var renderer in node.GetMeshRenderers())
                {
                    if (renderer.name == "Body_Body.003")
                    {
                        renderer.SetMainTexture(GetTexture("CrumblyPartsDamage0"));
                    }
                    else
                    {
                        renderer.SetMainTexture(GetTexture("CrumblyBodyDamage0"));
                    }
                    renderer.ApplyOutlineShader();
                    renderer.SetOutlineColor(new(0.63f, .36f, .15f));
                }
            }
        }
        
        class CrumblyDisplay1 : ModCustomDisplay
        {
            public override string AssetBundleName => "christmas2024";

            public override string PrefabName => "CrumblyAnimated";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                foreach (var renderer in node.GetMeshRenderers())
                {
                    if (renderer.name == "Body_Body.003")
                    {
                        renderer.SetMainTexture(GetTexture("CrumblyPartsDamage1"));
                    }
                    else
                    {
                        renderer.SetMainTexture(GetTexture("CrumblyBodyDamage1"));
                    }
                    renderer.ApplyOutlineShader();
                    renderer.SetOutlineColor(new(0.63f, .36f, .15f));
                }
            }
        }
        
        class CrumblyDisplay2 : ModCustomDisplay
        {
            public override string AssetBundleName => "christmas2024";

            public override string PrefabName => "CrumblyAnimated";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                foreach (var renderer in node.GetMeshRenderers())
                {
                    if (renderer.name == "Body_Body.003")
                    {
                        renderer.SetMainTexture(GetTexture("CrumblyPartsDamage2"));
                    }
                    else
                    {
                        renderer.SetMainTexture(GetTexture("CrumblyBodyDamage2"));
                    }
                    renderer.ApplyOutlineShader();
                    renderer.SetOutlineColor(new(0.63f, .36f, .15f));
                }
            }
        }
        
        class CrumblyDisplay3 : ModCustomDisplay
        {
            public override string AssetBundleName => "christmas2024";

            public override string PrefabName => "CrumblyAnimated";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                foreach (var renderer in node.GetMeshRenderers())
                {
                    if (renderer.name == "Body_Body.003")
                    {
                        renderer.SetMainTexture(GetTexture("CrumblyPartsDamage3"));
                    }
                    else
                    {
                        renderer.SetMainTexture(GetTexture("CrumblyBodyDamage3"));
                    }
                    renderer.ApplyOutlineShader();
                    renderer.SetOutlineColor(new(0.63f, .36f, .15f));
                }
            }
        }
        
        class CrumblyDisplay4 : ModCustomDisplay
        {
            public override string AssetBundleName => "christmas2024";

            public override string PrefabName => "CrumblyAnimated";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                foreach (var renderer in node.GetMeshRenderers())
                {
                    if (renderer.name == "Body_Body.003")
                    {
                        renderer.SetMainTexture(GetTexture("CrumblyPartsDamage4"));
                    }
                    else
                    {
                        renderer.SetMainTexture(GetTexture("CrumblyBodyDamage4"));
                    }
                    renderer.ApplyOutlineShader();
                    renderer.SetOutlineColor(new(0.63f, .36f, .15f));
                }
            }
        }

        [HarmonyPatch(typeof(TitleScreen), nameof(TitleScreen.Start))]
        public class TitleScreenInit
        {
            [HarmonyPostfix]

            public static void Postfix()
            {
                BloonModel CandyCaneBoss = CreateBossBase(30000, 1f);

                //Poppermint

                CandyCaneBoss.ApplyDisplay<BossDisplay>();


                BossRegisteration candyCaneRegisteration = new BossRegisteration(CandyCaneBoss, "CandyCaneBoss",
                    "Poppermint", true, "PoppermintIcon", 0,
                    "The Poppermint: For every 10% of its health lost, it spawns 5 Candy Cane Bloons to swarm the battlefield. Defeating this boss will grant Santa a powerful upgrade, making him stronger for the battles ahead.");

                candyCaneRegisteration.SpawnOnRound(20);


                HealthPercentTriggerModel health = Game.instance.model.GetBloon("Bloonarius1")
                    .GetBehavior<HealthPercentTriggerModel>().Duplicate();
                health.percentageValues = new float[] { 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1f };
                health.actionIds = new string[] { "SpawnBloons" };

                SpawnBloonsActionModel spawn = Game.instance.model.GetBloon("Bloonarius1")
                    .GetBehavior<SpawnBloonsActionModel>().Duplicate();
                spawn.bloonType = "Rainbow";
                spawn.actionId = "SpawnBloons";
                spawn.spawnCount = 5;
                spawn.spawnTrackMin = 0.3f;
                spawn.spawnTrackMax = 0.5f;
                spawn.bossName = "Phayze";

                CandyCaneBoss.AddBehavior(health);
                CandyCaneBoss.AddBehavior(spawn);

                //Frosty the Snowbloon

                BloonModel FrostyBoss = CreateBossBase(70000, 1f);
                FrostyBoss.ApplyDisplay<FrostyDisplay>();

                BossRegisteration frostyBossRegisteration = new BossRegisteration(FrostyBoss, "Frosty",
                    "Frosty The Snowbloon", true, "FrostyIcon", 0,
                    "Frosty the Snowbloon is a chilling force. Immune to ice attacks, As it progresses, every time it loses a skull, it stuns all towers, freezing them in place. While Frosty is alive, a relentless snowstorm will rage across the battlefield, Defeating Frosty will bring you one step closer to saving Christmas!");

                frostyBossRegisteration.SpawnOnRound(40);

                HealthPercentTriggerModel health1 = Game.instance.model.GetBloon("Bloonarius1")
                    .GetBehavior<HealthPercentTriggerModel>().Duplicate();
                health1.percentageValues = new float[] { 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1f };
                health1.actionIds = new string[] { "Freeze" };

                StunTowersInRadiusActionModel stun = Game.instance.model.GetBloon("Vortex1")
                    .GetBehavior<StunTowersInRadiusActionModel>();
                stun.stunDuration = 5f;
                stun.actionId = "Freeze";
                stun.radius = 999f;

                FrostyBoss.AddBehavior(health1);
                FrostyBoss.AddBehavior(stun);

                // Crumbly 

                BloonModel Crumbly = CreateBossBase(10000000, 1f);

                Crumbly.ApplyDisplay<CrumblyDisplay>();

                BossRegisteration crumblyBossRegisteration = new BossRegisteration(Crumbly, "Crumbly", "Crumbly", true,
                    "CrumblyIcon", 0,
                    "I’m Crumbly, the Gingerbread Boss! I’ve got 5 lives, and with each one, my HP doubles. To make things even sweeter, my speed increases every second—I’ve gotta go fast! Catch me if you can!");

                crumblyBossRegisteration.usesExtraInfo = true;
                crumblyBossRegisteration.extraInfoText = "Test";
                crumblyBossRegisteration.extraInfoIcon = "descriptionButton";
                crumblyBossRegisteration.fakeHealth = 15000;
                crumblyBossRegisteration.fakeMaxHealth = 15000;
                crumblyBossRegisteration.SpawnOnRound(60);
                crumblyBossRegisteration.usesHealthOverride = true;

                // Cookie Monster 
                
                BloonModel CookieMonster = CreateBossBase(10000000, 1f);

                CookieMonster.ApplyDisplay<CookieMonsterDisplay>();
                
                BossRegisteration cookieMonsterBossRegisteration = new BossRegisteration(CookieMonster, "CookieMonster",
                    "Cookie Monster", true, "CookieMonsterIcon", 0, "");

                cookieMonsterBossRegisteration.usesHealthOverride = true;
                cookieMonsterBossRegisteration.fakeHealth = 2500000;
                cookieMonsterBossRegisteration.fakeMaxHealth = 2500000;

                cookieMonsterBossRegisteration.SpawnOnRound(80);

                // Grinch (Oh boy)

                BloonModel Grinch = CreateBossBase(10000000, 1f);

                Grinch.ApplyDisplay<GrinchDisplay>();
                
                BossRegisteration grinchRegisteration = new BossRegisteration(Grinch, "GrinchBoss",
                    "The Grinch", true, "GrinchIcon", 0, "The Grinch, the strongest boss yet, contains abilities from all bosses like snowstorm, stun towers, and more. Prepare for the craziest battle.");
                
                HealthPercentTriggerModel healthGrinch = Game.instance.model.GetBloon("Bloonarius1")
                    .GetBehavior<HealthPercentTriggerModel>().Duplicate();
                health.percentageValues = new float[] { 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1f };
                health.actionIds = new string[] { "SpawnBloons" };

                SpawnBloonsActionModel spawnGrinch = Game.instance.model.GetBloon("Bloonarius1")
                    .GetBehavior<SpawnBloonsActionModel>().Duplicate();
                spawnGrinch.bloonType = "Zomg";
                spawnGrinch.actionId = "SpawnBloons";
                spawnGrinch.spawnCount = 10;
                spawnGrinch.spawnTrackMin = 0.5f;
                spawnGrinch.spawnTrackMax = 0.5f;
                spawnGrinch.bossName = "Phayze";
                
                TimeTriggerModel time = new TimeTriggerModel("time", 20, false, new string[] { "Freeze" } );

                
                StunTowersInRadiusActionModel stunGrinch = Game.instance.model.GetBloon("Vortex1").GetBehavior<StunTowersInRadiusActionModel>();
                stunGrinch.stunDuration = 5;
                stunGrinch.actionId = "Freeze";
                stunGrinch.radius = 999f;
                
                
                Grinch.AddBehavior(time);
                Grinch.AddBehavior(stunGrinch);
                Grinch.AddBehavior(healthGrinch);
                Grinch.AddBehavior(spawnGrinch);
                
                grinchRegisteration.usesHealthOverride = true;
                grinchRegisteration.fakeHealth = 20000000;
                grinchRegisteration.fakeMaxHealth = 20000000;
                
                

                grinchRegisteration.SpawnOnRound(100);

                //R55 Gift Box

                BloonModel Giftbox = CreateBossBase(100000, 0.7f);

                BossRegisteration giftboxBossRegisteration = new BossRegisteration(Giftbox, "Giftbox",
                    "Massive Giftbox", true, "GiftsParticle", 0,
                    "A massive gift box that runs through the map. Beating it will let you open it!");

                giftboxBossRegisteration.SpawnOnRound(55);

                Giftbox.disallowCosmetics = true;
                Giftbox.ApplyDisplay<MassivePresent>();

                CandyCaneBoss.disallowCosmetics = true;
                FrostyBoss.disallowCosmetics = true;
                Crumbly.disallowCosmetics = true;
                CookieMonster.disallowCosmetics = true;
                Grinch.disallowCosmetics = true;
                
                // NONE HEALTH BAR BOSSES
                
                BloonModel CandyCaneBossNHB = CreateBossBase(35000, 1f);

                //Poppermint

                CandyCaneBossNHB.ApplyDisplay<BossDisplay>();


                BossRegisteration candyCaneRegisterationNHB = new BossRegisteration(CandyCaneBossNHB, "CandyCaneBossNHB",
                    "Poppermint", false, "PoppermintIcon", 0,
                    "The Poppermint: For every 10% of its health lost, it spawns 5 Candy Cane Bloons to swarm the battlefield. Defeating this boss will grant Santa a powerful upgrade, making him stronger for the battles ahead.");

                HealthPercentTriggerModel healthNHB = Game.instance.model.GetBloon("Bloonarius1")
                    .GetBehavior<HealthPercentTriggerModel>().Duplicate();
                healthNHB.percentageValues = new float[] { 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1f };
                healthNHB.actionIds = new string[] { "SpawnBloons" };

                SpawnBloonsActionModel spawnNHB = Game.instance.model.GetBloon("Bloonarius1")
                    .GetBehavior<SpawnBloonsActionModel>().Duplicate();
                spawnNHB.bloonType = "Rainbow";
                spawnNHB.actionId = "SpawnBloons";
                spawnNHB.spawnCount = 5;
                spawnNHB.spawnTrackMin = 0.3f;
                spawnNHB.spawnTrackMax = 0.5f;
                spawnNHB.bossName = "Phayze";

                CandyCaneBossNHB.AddBehavior(healthNHB);
                CandyCaneBossNHB.AddBehavior(spawnNHB);

                //Frosty the Snowbloon

                BloonModel FrostyBossNHB = CreateBossBase(90000, 1f);
                FrostyBossNHB.ApplyDisplay<FrostyDisplay>();

                BossRegisteration frostyBossRegisterationNHB = new BossRegisteration(FrostyBossNHB, "FrostyNHB",
                    "Frosty The Snowbloon", false, "FrostyIcon", 0,
                    "Frosty the Snowbloon is a chilling force. Immune to ice attacks, As it progresses, every time it loses a skull, it stuns all towers, freezing them in place. While Frosty is alive, a relentless snowstorm will rage across the battlefield, Defeating Frosty will bring you one step closer to saving Christmas!");

                HealthPercentTriggerModel health1NHB = Game.instance.model.GetBloon("Bloonarius1")
                    .GetBehavior<HealthPercentTriggerModel>().Duplicate();
                health1NHB.percentageValues = new float[] { 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1f };
                health1NHB.actionIds = new string[] { "Freeze" };

                StunTowersInRadiusActionModel stunNHB = Game.instance.model.GetBloon("Vortex1")
                    .GetBehavior<StunTowersInRadiusActionModel>();
                stunNHB.stunDuration = 5f;
                stunNHB.actionId = "Freeze";
                stunNHB.radius = 999f;

                FrostyBossNHB.AddBehavior(health1NHB);
                FrostyBossNHB.AddBehavior(stunNHB);
                FrostyBossNHB.ApplyDisplay<FrostyDisplay>();
                
                // Cookie Monster 

                BloonModel CookieMonsterNHB = CreateBossBase(1500000, 1f);

                BossRegisteration cookieMonsterBossRegisterationNHB = new BossRegisteration(CookieMonsterNHB, "CookieMonsterNHB",
                    "Cookie Monster", false, "CookieMonsterIcon", 0, "");

                CookieMonsterNHB.ApplyDisplay<CookieMonsterDisplay>();
                cookieMonsterBossRegisterationNHB.usesHealthOverride = true;
                cookieMonsterBossRegisterationNHB.fakeHealth = 1500000;
                cookieMonsterBossRegisterationNHB.fakeMaxHealth = 1500000;
            }
        }

        public class MassivePresent : ModDisplay
        {
            public override string BaseDisplay => Generic2dDisplay;

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                Set2DTexture(node, "MassiveGiftbox");
            }
        }

        public static void BossInit(Bloon bloon, BloonModel bloonModel, BossRegisteration registration)
        {
            if (Values.GrinchAngry == true)
            {
                registration.isMainBoss = false;
            }
            
            if (bloonModel.id.Contains("Crumbly"))
            {
                MonoBehaviorCrumbly mono = StartMonobehavior<MonoBehaviorCrumbly>();

                mono.boss = bloon;
                mono.registration = registration;
            }

            if (bloonModel.id.Contains("Cookie"))
            {
                MonoBehaviorCookieMonster mono =
                    StartMonobehavior<MonoBehaviorCookieMonster>();

                mono.boss = bloon;
                mono.registration = registration;
            }
            
            if (bloonModel.id.Contains("GrinchBoss"))
            {
                MonoBehaviorGrinch mono =
                    StartMonobehavior<MonoBehaviorGrinch>();

                mono.boss = bloon;
                mono.registration = registration;
            }
        }
        
        [RegisterTypeInIl2Cpp(false)]
        public class MonoBehaviorCrumbly : MonoBehaviour
        {
            public Bloon boss;
            public double SpeedMultiplier = 1f;
            public BossRegisteration registration;
            public int fakeHealth = 15000;
            public int fakeMaxHealth = 15000;
            private static readonly System.Random random = new System.Random();
            public MonoBehaviorCrumbly() : base()
            {

            }

            public void Start()
            {
                Values.DefeatedCounter = 0;
            }
            
            public void Update()
            {
                if (boss != null)
                {
                    if (TimeManager.FastForwardActive == true)
                    {
                        SpeedMultiplier += 3 * 0.0001f;
                    }
                    else
                    {
                        SpeedMultiplier += 1 * 0.0001f;
                    }


                    boss.trackSpeedMultiplier = (float)SpeedMultiplier;

                    registration.usesExtraInfo = true;
                    registration.extraInfoText = "Speed: " + (SpeedMultiplier - 1) * 100 + "%";

                    registration.fakeHealth = fakeHealth;
                    registration.fakeMaxHealth = fakeMaxHealth;

                    if (boss.health < boss.bloonModel.maxHealth)
                    {
                        fakeHealth -= (boss.bloonModel.maxHealth - boss.health);
                    }

                    boss.health = boss.bloonModel.maxHealth;

                    fakeHealth = Math.Max(0, fakeHealth);

                    if (fakeHealth == 0 && Values.bossDead == false)
                    {
                        Values.bossDead = true;
                       
                        boss.trackSpeedMultiplier = -10;
                        
                        Task.Run(async () =>
                        {
                            await Task.Delay(500);

                            if (Values.DefeatedCounter <= 6)
                            {
                                fakeMaxHealth *= 2;
                                fakeHealth = fakeMaxHealth;
                                boss.trackSpeedMultiplier = 1;
                                SpeedMultiplier = 1f;
                                boss.Rotation = boss.prevRot;
                                Values.DefeatedCounter += 1;
                            }

                            Values.bossDead = false;
                        });

                        if (Values.DefeatedCounter == 0 && Values.GrinchAngry == false)
                        {
                            var text = "Im not that tasty! Crumbly Stole 10% of your cash";
                            Story.StoryUI.CreatePanel(StoryPortrait.CrumblyIcon, text);
                            InGame.instance.AddCash(-InGame.instance.GetCash() * 0.1f);
                        }

                        if (Values.DefeatedCounter == 1 && Values.GrinchAngry == false)
                        {
                            
                            var text = "Ouch! Crumbly Stole 10% of your cash";
                            Story.StoryUI.CreatePanel(StoryPortrait.CrumblyIcon, text);
                            InGame.instance.AddCash(-InGame.instance.GetCash() * 0.1f);
                        }

                        if (Values.DefeatedCounter == 2 && Values.GrinchAngry == false)
                        {

                            var text = "Im not eatable! Crumbly Stole 10% of your cash";
                            Story.StoryUI.CreatePanel(StoryPortrait.CrumblyIcon, text);
                            InGame.instance.AddCash(-InGame.instance.GetCash() * 0.1f);
                        }

                        if (Values.DefeatedCounter == 3 && Values.GrinchAngry == false)
                        {
                            var text = "Stop it! Crumbly Stole 10% of your cash";
                            Story.StoryUI.CreatePanel(StoryPortrait.CrumblyIcon, text);
                            InGame.instance.AddCash(-InGame.instance.GetCash() * 0.1f);
                        }

                        if (Values.DefeatedCounter == 4 && Values.GrinchAngry == false)
                        {
                            InGame.instance.AddCash(-InGame.instance.GetCash() * 0.1f);
                        }
                    }

                    if (Values.DefeatedCounter == 5)
                    {
                        boss.Destroy();
                    }
                }
                else
                {
                    this.Destroy();
                }
            }
        }
        [RegisterTypeInIl2Cpp(false)]
        public class MonoBehaviorCookieMonster : MonoBehaviour
        {
            public Bloon boss;
            public BossRegisteration registration;
            public int fakeHealth = 1500000;
            public int fakeMaxHealth = 1500000;
            public MonoBehaviorCookieMonster() : base()
            {

            }

            public void Start()
            {
                Values.DefeatedCounter = 0;
                Values.DefeatedCounterCookie = 0;
            }
            
            public void Update()
            {
                if (boss != null)
                {
                    registration.fakeHealth = fakeHealth;
                    registration.fakeMaxHealth = fakeMaxHealth;

                    if (boss.health < boss.bloonModel.maxHealth)
                    {
                        fakeHealth -= (boss.bloonModel.maxHealth - boss.health);
                    }

                    boss.health = boss.bloonModel.maxHealth;

                    fakeHealth = Math.Max(0, fakeHealth);

                    if (Values.cookieAngry == true)
                    {
                        boss.bloonModel.ApplyDisplay<CookieMonsterAngryDisplay>();
                        boss.UpdateDisplay();
                    }

                    if (fakeHealth <= fakeMaxHealth * 0.5f && Values.tsunami == false && Values.GrinchAngry == false)
                    {
                        Values.disableprojectile = true;
                        Values.tsunami = true;
                        InGame.instance.bridge.simulation.SpawnEffect(
                            Game.instance.model.GetTowerFromId("Mermonkey-050").GetAbility()
                                .GetBehavior<CreateEffectOnAbilityModel>().effectModel.assetId,
                            new Vector3(0, 0, 0), 0, 1);
                        boss.trackSpeedMultiplier = 2;
                        Task.Run(async () =>
                        {
                            await Task.Delay(10000);
                            
                            Values.disableprojectile = false;
                            boss.trackSpeedMultiplier = 1;
                        });
                    }

                    if (fakeHealth == 0 && Values.bossDead == false)
                    {
                        Values.bossDead = true;

                        if (Values.DefeatedCounterCookie < 2)
                        {
                            Values.cookieAngry = true;
                            
                            StoryMessage messages = new StoryMessage(
                                "NO! IMPOSSIBLE!I'M THE STRONGEST BOSS YET, I WON'T LET THIS SLIDE!, IT'S TIME TO SHOW YOU MY TRUE POWER!",
                                StoryPortrait.AngryCookieMonsterIcon, new(() =>
                                {
                                    fakeMaxHealth *= 3;
                                    fakeHealth = fakeMaxHealth;
                                    boss.trackSpeedMultiplier = 2;
                                    Values.DefeatedCounterCookie += 1;
                                    Values.tsunami = false;
                                    InGame.instance.SpawnBloons(ModContent.BloonID<MilkMoab>(), 20, 50);
                                }));

                            if (Values.GrinchAngry == true && Values.DefeatedCounterCookie < 2)
                            {
                                boss.bloonModel.ApplyDisplay<CookieMonsterAngryDisplay>();
                                boss.UpdateDisplay();
                                
                                
                                Values.cookieAngry = true;
                                
                                fakeMaxHealth *= 3;
                                fakeHealth = fakeMaxHealth;
                                boss.trackSpeedMultiplier = 2;
                                Values.DefeatedCounter += 1;
                                Values.tsunami = false;
                                InGame.instance.SpawnBloons(ModContent.BloonID<MilkMoab>(), 20, 50);
                            }
                            else
                            {
                                if (Values.cookieAngry == false)
                                {
                                    Story.StoryUI.CreatePanel(messages);
                                }
                                else if (Values.cookieAngry == true && Values.DefeatedCounterCookie <= 2)
                                {
                                    fakeMaxHealth *= 2;
                                    fakeHealth = fakeMaxHealth;
                                    boss.trackSpeedMultiplier *= 5;
                                    Values.DefeatedCounterCookie += 1;
                                    Values.tsunami = false;
                                    InGame.instance.SpawnBloons(ModContent.BloonID<MilkMoab>(), 20, 50);
                                }
                            }

                            Values.bossDead = false;
                        }
                        if (Values.DefeatedCounterCookie == 2 && Values.GrinchAngry == false)
                        {
                            boss.trackSpeedMultiplier = -40;
                            boss.Rotation = boss.PercThroughMap() * 20000;
                            boss.prevRot = boss.Rotation;

                            Task.Run(async () =>
                            {
                                await Task.Delay(4000);

                                boss.Destroy();
                            });
                            InGame.instance.AddCash(-InGame.instance.GetCash() * 0.5f);
                        }
                        else if (Values.DefeatedCounterCookie == 2 && Values.GrinchAngry == true)
                        {
                            boss.trackSpeedMultiplier = -40;
                            boss.Rotation = boss.PercThroughMap() * 20000;
                            boss.prevRot = boss.Rotation;

                            Task.Run(async () =>
                            {
                                await Task.Delay(4000);

                                boss.Destroy();
                            });
                        }

                        Values.bossDead = false;
                    }
                }
                else
                {
                    this.Destroy();
                }
            }
        }

        [RegisterTypeInIl2Cpp(false)]
        public class MonoBehaviorGrinch : MonoBehaviour
        {
            public Bloon boss;
            public BossRegisteration registration;
            public int fakeHealth = 20000000;
            public int fakeMaxHealth = 20000000;

            public MonoBehaviorGrinch() : base()
            {

            }

            public void Start()
            {
                Values.DefeatedCounter = 0;
                Values.DefeatedCounterCookie = 0;
                Values.Snowstorm = true;
                Values.SnowstormRound = 5;
            }
            
            public void Update()
            {
                if (boss != null)
                {
                    registration.fakeHealth = fakeHealth;
                    registration.fakeMaxHealth = fakeMaxHealth;

                    if (boss.health < boss.bloonModel.maxHealth)
                    {
                        fakeHealth -= (boss.bloonModel.maxHealth - boss.health);
                    }

                    if (Values.GrinchAngry == true)
                    {
                        boss.bloonModel.ApplyDisplay<AngryGrinchDisplay>();
                        boss.UpdateDisplay();
                    }
                    
                    boss.health = boss.bloonModel.maxHealth;

                    fakeHealth = Math.Max(0, fakeHealth);

                    if (fakeHealth <= fakeMaxHealth * 0.5f && Values.tsunami == false && Values.GrinchAngry == true)
                    {
                        foreach (var tower in InGame.instance.GetTowers())
                        {
                            CalculateNewSpot(tower, 114, 145, false);
                        }

                        Values.disableprojectile = true;
                        Values.tsunami = true;
                        InGame.instance.bridge.simulation.SpawnEffect(
                            Game.instance.model.GetTowerFromId("Mermonkey-050").GetAbility()
                                .GetBehavior<CreateEffectOnAbilityModel>().effectModel.assetId,
                            new Vector3(0, 0, 0), 0, 1);
                        boss.trackSpeedMultiplier = 3;

                        Task.Run(async () =>
                        {
                            await Task.Delay(7500);
                                
                            Values.disableprojectile = false;
                            boss.trackSpeedMultiplier = 2;
                        });
                    }

                    if (fakeHealth == 0 && Values.bossDead == false)
                    {
                        Values.bossDead = true;

                        Values.grinchAngryIcon = true;

                        if (Values.GrinchAngry == false)
                        {
                            Values.GrinchAngry = true;
                            fakeMaxHealth = 40000000;
                            fakeHealth = fakeMaxHealth;
                            boss.trackSpeedMultiplier = 2;
                            Values.tsunami = false;
                            
                            StartCutscene.StartCutsceneUI.CreatePanel(true, false, boss);
                            
                            Values.bossDead = false;
                        }
                        else if (Values.GrinchAngry == true)
                        {
                            boss.trackSpeedMultiplier = -80;
                            boss.Rotation = boss.PercThroughMap() * 200000;
                            boss.prevRot = boss.Rotation;

                            Task.Run(async () =>
                            {
                                await Task.Delay(4000);

                                boss.Destroy();
                            });

                            Values.bossDead = false;
                        }
                    }
                }
                else
                {
                    this.Destroy();
                }
            }
        }

        public static int RandomInt(int min, int max)
        {
            System.Random rand = new();
            return rand.Next(min, max);
        }
        public static void CalculateNewSpot(Tower tower, int maxDistance, int minDistance, bool limitDistance)
        {
            Vector2 oldPos = tower.Position.ToVector2();

            Vector2 newPos = new(RandomInt(-145, 145), RandomInt(-114 + 5, 114));

            if (limitDistance)
            {
                float travelledDistance = newPos.Distance(oldPos);

                while(travelledDistance > maxDistance || travelledDistance < minDistance)
                {
                    newPos = new(RandomInt(-145, 145), RandomInt(-114 + 5, 114));
                    travelledDistance = newPos.Distance(oldPos);
                }
            }

            tower.PositionTower(newPos);
            tower.Position.Z = 100;
        }


        [HarmonyPatch(typeof(Il2CppAssets.Scripts.Simulation.Towers.Weapons.Weapon), nameof(Il2CppAssets.Scripts.Simulation.Towers.Weapons.Weapon.SpawnDart))]
        public static class StopProjectile
        {
            [HarmonyPostfix]
            public static void Postfix(Weapon __instance)
            {
            }

            [HarmonyPrefix]
            public static bool Prefix()
            {
                if (Values.disableprojectile == true)
                {
                    return false;
                }

                return true;
            }
        }
    }
}

