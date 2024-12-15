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
using Il2CppAssets.Scripts.Simulation;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Simulation.Bloons.Behaviors;
using Il2CppAssets.Scripts.Simulation.SMath;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppAssets.Scripts.Unity.Scenes;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using MelonLoader;
using TemplateMod.Moabs;
using TemplateMod.UI;
using UnityEngine;
using static BossHandlerNamespace.BossHandler;
using Color = UnityEngine.Color;
using Math = Il2CppAssets.Scripts.Simulation.SMath.Math;
using Vector3 = Il2CppAssets.Scripts.Simulation.SMath.Vector3;

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


        [HarmonyPatch(typeof(TitleScreen), nameof(TitleScreen.Start))]
        public class TitleScreenInit
        {
            [HarmonyPostfix]

            public static void Postfix()
            {
                BloonModel CandyCaneBoss = CreateBossBase(35000, 1f);

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

                BloonModel FrostyBoss = CreateBossBase(90000, 1f);

                //Frosty the Snowbloon

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

                BloonModel Crumbly = CreateBossBase(50000, 1f);

                BossRegisteration crumblyBossRegisteration = new BossRegisteration(Crumbly, "Crumbly", "Crumbly", true,
                    "CrumblyIcon", 0,
                    "I’m Crumbly, the Gingerbread Boss! I’ve got 5 lives, and with each one, my HP doubles. To make things even sweeter, my speed increases every second—I’ve gotta go fast! Catch me if you can!");

                crumblyBossRegisteration.usesExtraInfo = true;
                crumblyBossRegisteration.extraInfoText = "Test";
                crumblyBossRegisteration.extraInfoIcon = "descriptionButton";
                crumblyBossRegisteration.fakeHealth = 50000;
                crumblyBossRegisteration.fakeMaxHealth = 50000;
                crumblyBossRegisteration.SpawnOnRound(60);
                crumblyBossRegisteration.usesHealthOverride = true;

                // Cookie Monster 

                BloonModel CookieMonster = CreateBossBase(1500000, 1f);

                BossRegisteration cookieMonsterBossRegisteration = new BossRegisteration(CookieMonster, "CookieMonster",
                    "Cookie Monster", true, "CookieMonsterIcon", 0, "");

                cookieMonsterBossRegisteration.usesHealthOverride = true;
                cookieMonsterBossRegisteration.fakeHealth = 1500000;
                cookieMonsterBossRegisteration.fakeMaxHealth = 1500000;
                
                cookieMonsterBossRegisteration.SpawnOnRound(80);
            }
        }

        public static void BossInit(Bloon bloon, BloonModel bloonModel, BossRegisteration registration)
        {
            if (bloonModel.id == "Crumbly")
            {
                MonoBehaviorTemplate mono = StartMonobehavior<MonoBehaviorTemplate>();

                mono.boss = bloon;
                mono.registration = registration;
            }
            
            if (bloonModel.id == "CookieMonster")
            {
                MonoBehaviorTemplate.MonoBehaviorCookieMonster mono = StartMonobehavior<MonoBehaviorTemplate.MonoBehaviorCookieMonster>();

                mono.boss = bloon;
                mono.registration = registration;
            }
        }

        [RegisterTypeInIl2Cpp(false)]
        public class MonoBehaviorTemplate : MonoBehaviour
        {
            public Bloon boss;
            public double SpeedMultiplier = 1f;
            public BossRegisteration registration;
            public int fakeHealth = 50000;
            public int fakeMaxHealth = 50000;
            private static readonly System.Random random = new System.Random();

            public MonoBehaviorTemplate() : base()
            {

            }

            public void Start()
            {
            }

            public void Update()
            {
                if (boss != null)
                {
                    if (TimeManager.FastForwardActive == true)
                    {
                        SpeedMultiplier += 3 * 0.001f;
                    }
                    else
                    {
                        SpeedMultiplier += 1 * 0.001f;
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
                        boss.Rotation = boss.PercThroughMap() * 20000;
                        boss.prevRot = boss.Rotation;

                        Task.Run(async () =>
                        {
                            await Task.Delay(2000);

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

                        if (Values.DefeatedCounter == 0)
                        {
                            var text = "Im not that tasty! Crumbly Stole 10% of your cash";
                            SantaStory.SantaStoryUI.CreatePanel(SantaEmotion.CrumblyIcon, text);
                            InGame.instance.AddCash(-InGame.instance.GetCash() * 0.1f);
                        }

                        if (Values.DefeatedCounter == 1)
                        {
                            var text = "Ouch! Crumbly Stole 10% of your cash";
                            SantaStory.SantaStoryUI.CreatePanel(SantaEmotion.CrumblyIcon, text);
                            InGame.instance.AddCash(-InGame.instance.GetCash() * 0.1f);
                        }

                        if (Values.DefeatedCounter == 2)
                        {
                            var text = "Im not eatable! Crumbly Stole 10% of your cash";
                            SantaStory.SantaStoryUI.CreatePanel(SantaEmotion.CrumblyIcon, text);
                            InGame.instance.AddCash(-InGame.instance.GetCash() * 0.1f);
                        }

                        if (Values.DefeatedCounter == 3)
                        {
                            var text = "Stop it! Crumbly Stole 10% of your cash";
                            SantaStory.SantaStoryUI.CreatePanel(SantaEmotion.CrumblyIcon, text);
                            InGame.instance.AddCash(-InGame.instance.GetCash() * 0.1f);
                        }

                        if (Values.DefeatedCounter == 4)
                        {
                            var text = "NOOOOO! Crumbly Stole 10% of your cash before dying";
                            SantaStory.SantaStoryUI.CreatePanel(SantaEmotion.CrumblyIcon, text);
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

                        if (fakeHealth <= fakeMaxHealth * 0.5f)
                        {
                            Values.tsunami = true;
                            InGame.instance.bridge.simulation.SpawnEffect(Game.instance.model.GetTowerFromId("Mermonkey-050").GetAbility().GetBehavior<CreateEffectOnAbilityModel>().effectModel.assetId, new Vector3(0, 0, 0), 0, 1);
                            boss.trackSpeedMultiplier = 2;
                            
                            Task.Run(async () =>
                            {
                                await Task.Delay(10000);
                                boss.trackSpeedMultiplier = 1;
                                Values.tsunami = false;
                            });
                        }

                        if (fakeHealth == 0 && Values.bossDead == false)
                        {
                            Values.bossDead = true;
                            
                            if (Values.DefeatedCounter == 0)
                            {
                                SantaMessage messages = new SantaMessage("NO! IMPOSSIBLE!I'M THE STRONGEST BOSS YET, I WON'T LET THIS SLIDE!, IT'S TIME TO SHOW YOU MY TRUE POWER!", SantaEmotion.CookieMonsterIcon, new(() =>
                                {
                                    fakeMaxHealth *= 3;
                                    fakeHealth = fakeMaxHealth;
                                    boss.trackSpeedMultiplier *= 5;
                                    Values.DefeatedCounter += 1;
                                    InGame.instance.SpawnBloons(ModContent.BloonID<MilkMoab>(), 20, 50);
                                }));

                                SantaStory.SantaStoryUI.CreatePanel(messages); 
                                
                                Values.bossDead = false;
                            }
                            else if (Values.DefeatedCounter == 1)
                            {
                                SantaMessage messages = new SantaMessage("I guess I'm not strong enough, But I've got one last trick up my sleeve, Cookie Monster proceeded to steal half your money... and then died.", SantaEmotion.CookieMonsterIcon, new(() =>
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
                                }));
                                
                                SantaStory.SantaStoryUI.CreatePanel(messages);
                                
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
        }
    }
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
        if (Values.tsunami == true)
        {
            return false;
        }
        return true;
    }
}

