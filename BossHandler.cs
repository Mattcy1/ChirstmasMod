
using MelonLoader;


using Harmony;

using System.Linq;

using System;

using BTD_Mod_Helper;
using BTD_Mod_Helper.Extensions;
using System.Collections.Generic;

using BTD_Mod_Helper.Api.Display;

using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppAssets.Scripts.Unity;
using UnityEngine;
using Il2CppAssets.Scripts.Models.Effects;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Unity.Bridge;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Models.Map;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles;
using Il2Cpp;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors.Actions;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Simulation.Towers.Weapons;
using Il2CppAssets.Scripts.Models.Towers.Weapons;
using Il2CppAssets.Scripts.Models.Towers.TowerFilters;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles;
using Il2CppAssets.Scripts.Utils;
using Il2CppAssets.Scripts.Models.Bloons;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;

using static BossHandlerNamespace.Bosses;

using Il2CppAssets.Scripts.Models.Rounds;
using Il2CppAssets.Scripts.Unity.UI_New.InGame.Stats;
using Il2CppAssets.Scripts.Simulation.Map.Triggers;
using Il2CppAssets.Scripts.Simulation.Track;
using Il2CppAssets.Scripts.Simulation.Bloons.Behaviors;
using Il2CppAssets.Scripts.Unity.Map.Triggers;
using MapEvent = Il2CppAssets.Scripts.Simulation.Map.Triggers.MapEvent;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Simulation.SMath;
using Vector2 = Il2CppAssets.Scripts.Simulation.SMath.Vector2;
using CreateEffectOnExpireModel = Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors.CreateEffectOnExpireModel;
using Random = System.Random;
using static Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors.MorphTowerModel;
using CreateEffectOnExpire = Il2CppAssets.Scripts.Simulation.Towers.Projectiles.Behaviors.CreateEffectOnExpire;
using Il2CppAssets.Scripts.Simulation.SimulationBehaviors;
using UnityEngine.Windows;
using Il2CppAssets.Scripts.Simulation;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Simulation.Objects;
using static Il2CppAssets.Scripts.Models.Towers.Behaviors.ParagonTowerModel;
using Il2CppAssets.Scripts;
using static Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors.AddBehaviorToBloonModel;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Quaternion = Il2CppAssets.Scripts.Simulation.SMath.Quaternion;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;
using Math = Il2CppAssets.Scripts.Simulation.SMath.Math;
using Il2CppTMPro;
using UnityEngine.Assertions;
using Il2CppAssets.Scripts.Unity.Scenes;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Attack.Behaviors;
//using NinjaKiwi.Common.ResourceUtils;
using BTD_Mod_Helper.Api.Components;
using Il2CppAssets.Scripts.Unity.UI_New.Utils;
using static MelonLoader.MelonLogger;
using Il2CppAssets.Scripts.Unity.Towers;
using Tower = Il2CppAssets.Scripts.Simulation.Towers.Tower;
using TowerBehavior = Il2CppAssets.Scripts.Simulation.Towers.TowerBehavior;
using Il2CppAssets.Scripts.Simulation.Towers.Weapons.Behaviors;
using Vector3 = UnityEngine.Vector3;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Abilities.Behaviors;
using static BossHandlerNamespace.BossHandler;
using Il2CppAssets.Scripts.Data.Boss;
using Il2CppAssets.Scripts.SimulationTests;
using BTD_Mod_Helper.Api.ModOptions;
using Il2CppAssets.Scripts.Data;
using Il2CppAssets.Scripts.Unity.Achievements.List;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using UnityEngine.Rendering;
using Il2CppAssets.Scripts.Unity.UI_New;
using Il2CppAssets.Scripts.Models.Towers.Mods;

namespace BossHandlerNamespace
{

    public class BossHandler : BloonsTD6Mod
    {

        public static ModHelperPanel panel;
     

        public static readonly ModSettingInt HealthPercentMultiplier = 100;
        public static readonly ModSettingInt SpeedPercentMultiplier = 100;  

        public static Dictionary<string, BossRegisteration> bossRegisterations = new Dictionary<string, BossRegisteration>();

        public static BossPanel bossPanel;
        public static ModHelperPanel mainBossPanel;

        public const string PHAYZEBAR = "phayzeBar";
        public const string DREADBAR = "dreadHealth";

        public class BossRegisteration
        {
            // Size of the description box
            public int sizeX = 900;
            public int sizeY = 300;
            public int continueRounds = 0;
            // The BloonModel of the boss
            public BloonModel boss;

            // The name of the png you want to use as the boss icon
            public string icon = "";

            // The id of the boss Bloon. This can used for spawning the boss via code
            public string id = "Boss";
            public string displayName = "NA";
            public string description = "";


            public bool isMainBoss = true;

            // If the boss uses the extra info panel, it sets the icon and text according to InfoIcon and InfoText.
            public bool usesExtraInfo = false;
            public string extraInfoIcon = "";
            public string extraInfoText = "";

            // If you want the health bar to display values other than the bosses actual health. This is useful for phasing mechanics
            // Example: Boss with 1M HP goes into phase 2 at 500K HP, so you can make the health bar say X/500K for phase 1 and phase 2
            public bool usesHealthOverride = false;
            public int fakeHealth = 0;
            public int fakeMaxHealth = 0;
            public BossRegisteration(BloonModel boss, string id, string displayName, bool isMainBoss = true, string icon = "defaultIcon", int continueRounds = 0, string description = "")
            {
                this.icon = icon;
                this.id = id;
                this.displayName = displayName;
                this.description = description;
                this.continueRounds = continueRounds;

                this.boss = boss;
                this.isMainBoss = isMainBoss;
              
                // The inputted boss health is divided by 100, but then multiplied by 100 which is the default
                // HP multiplier under mod settings. That way the user can adjust the health to their preference.


                boss.id = boss.name = boss._name = id;

               
                Game.instance.model.bloonsByName[id] = boss;
                Game.instance.model.bloons = Game.instance.model.bloons.Take(0).Append(boss).Concat(Game.instance.model.bloons.Skip(0)).ToArray();


                bossRegisterations[boss.id] = this;

                
            }


            public void SpawnOnRound(int round, bool clearOtherSpawns = true)
            {

                // When adding a boss via SpawnOnRound, makes every round set spawn that boss on that corrosponding round.
                // By default it prevents any other Bloons from spawning that round, but this can be disabled.
                foreach (RoundSetModel roundSet in GameData.Instance.roundSets)
                {
                    try
                    {
                        if (clearOtherSpawns)
                        {

                            roundSet.rounds[round - 1].ClearBloonGroups();
                        }

                        roundSet.rounds[round - 1].AddBloonGroup(this.boss.id, 1);
                    }
                    catch
                    {

                    }
                }
            }
        }

        [RegisterTypeInIl2Cpp]
        public class BossPanel : MonoBehaviour
        {
            public BossPanel() : base() { }
            public ObjectId bloon;


            public int healthOverride = -1;
            public int maxHealthOverride = -1;

            public ModHelperImage barImage;
            public ModHelperImage healthBlockBar;
            public ModHelperText nameText;
            public ModHelperText textBox;
     
            public ModHelperText description;
            public ModHelperImage descriptionBox;
            public BossType bossType;
            public ModHelperImage bossIcon;
          
            public BossRegisteration registeration;
            public bool showDescription = false;

            public string healthBar = "healthBar";
            public static string healthBarInUse = "healthBar";

            public ModHelperPanel extraPanel;
         
            public ModHelperImage iconGlow;
            public ModHelperImage extraIcon;
            
            public ModHelperText extraTextObject;
            public string extraText = "";
            public ModHelperButton descriptionToggle;

            public void Start()
            {

              // Adds various components for the boss UI

                LayoutGroup group = InGame.instance.GetInGameUI().GetComponentInChildrenByName<LayoutGroup>("LayoutGroup");


                mainBossPanel = group.gameObject.AddModHelperPanel(new BTD_Mod_Helper.Api.Components.Info("MainBoss", 2000, 300));

               


                barImage = mainBossPanel.AddImage(new BTD_Mod_Helper.Api.Components.Info("HealthBar", 0, 0, 1000, 100), ModContent.GetSpriteReference<BossHandler>("healthBar").GetGUID());
                healthBlockBar = mainBossPanel.AddImage(new BTD_Mod_Helper.Api.Components.Info("Bar", 0, 0, 1000, 100), ModContent.GetSpriteReference<BossHandler>("fillBar").GetGUID());

                textBox = mainBossPanel.AddText(new BTD_Mod_Helper.Api.Components.Info("TextBox", 0, 0, 2000, 200), "Text", 70);

                nameText = mainBossPanel.AddText(new BTD_Mod_Helper.Api.Components.Info("NameBox", 0, 100, 2000, 200), registeration.displayName, 80);

                extraPanel = mainBossPanel.gameObject.AddModHelperPanel(new BTD_Mod_Helper.Api.Components.Info("ExtraIconPanel", 0, -100, 700, 100), ModContent.GetSpriteReference<BossHandler>("iconBG").GetGUID());
                //extraPanel.Background.sprite = ModContent.GetSprite<BossHandler>("iconBGglow");
                extraPanel.AddImage(new BTD_Mod_Helper.Api.Components.Info("Glow", -350, 0, 120), ModContent.GetSpriteReference<BossHandler>("iconBGglow").GetGUID());

                extraIcon = extraPanel.AddImage(new BTD_Mod_Helper.Api.Components.Info("ExtraIcon", -350, 0, 120), "descriptionButton");

                extraTextObject = extraPanel.AddText(new BTD_Mod_Helper.Api.Components.Info("ExtraIconText", 0, 0, 1000, 100), "Nothing", 40);
                extraTextObject.Text.alignment = Il2CppTMPro.TextAlignmentOptions.Center;

                extraPanel.Hide();


                bossIcon = mainBossPanel.AddImage(new BTD_Mod_Helper.Api.Components.Info("BossIcon", -550, 0, 150), ModContent.GetSpriteReference<BossHandler>("blank").GetGUID());

                mainBossPanel.transform.localPosition = new Vector3(1000, 1080, 0);
                mainBossPanel.Show();

                
                    // If the description property in the registration has text, adds a toggle
                    // to show and hide the description.
                    Il2CppSystem.Action descriptionToggleAction = (Il2CppSystem.Action)delegate ()
                    {


                        if (showDescription)
                        {
                            showDescription = false;
                            descriptionBox.Hide();
                        }
                        else
                        {
                            showDescription = true;
                            
                            descriptionBox.Show();
                            
                          
                        }
                    };

               
                    descriptionToggle = mainBossPanel.AddButton(new BTD_Mod_Helper.Api.Components.Info("ShowDescription", 550, 0, 100), ModContent.GetSpriteReference<BossHandler>("descriptionButton").GetGUID(), descriptionToggleAction);


                    descriptionBox = mainBossPanel.AddImage(new BTD_Mod_Helper.Api.Components.Info("DescriptionBox", 0, 0, registeration.sizeX, registeration.sizeY), ModContent.GetSpriteReference<BossHandler>("descriptionBox").GetGUID());
                    description = descriptionBox.AddText(new BTD_Mod_Helper.Api.Components.Info("Description", 0, 0, registeration.sizeX, registeration.sizeY), registeration.description, 40, Il2CppTMPro.TextAlignmentOptions.Top);



              
                    descriptionBox.Hide();
               

                UpdateInfo();
            }

            public void Update()
            {
                if (InGame.instance.bridge != null)
                {
                    Bloon boss = InGame.instance.bridge.GetBloonFromId(bloon);

                    // Checks if the boss exists and updates the UI accordingly. 

                    if (boss != null)
                    {
                        // Uses the default boss health bar, but you can add custom ones by setting bossPanels healthBar property
                        // to the name of the health bar image you'd like to use.
                        // Image is only updated when the property is altered.
                        if (healthBar != healthBarInUse)
                        {
                            healthBarInUse = healthBar;
                            barImage.AddImage(new BTD_Mod_Helper.Api.Components.Info("HealthBar", 0, 0, 1000, 100), ModContent.GetSpriteReference<BossHandler>(healthBarInUse).GetGUID());
                        }

                        if (registeration.usesExtraInfo)
                        {
                            extraTextObject.Text.SetText(extraText);
                        }
                        int health = 0;
                        int maxHealth = 0;
                        if(registeration.usesHealthOverride)
                        {
                            health = registeration.fakeHealth;
                            maxHealth = registeration.fakeMaxHealth;
                        }
                        else
                        {
                            health = boss.health;
                            maxHealth = boss.bloonModel.maxHealth;
                        }

                        float hpScale = (float)health / maxHealth;
                        hpScale = Math.Min(hpScale, 1);
                        hpScale = 1 - hpScale;

                        healthBlockBar.transform.localScale = new Vector3(1f, 0.6f, 1);
                        healthBlockBar.transform.localScale = new Vector3(1f * hpScale, 1f, 1);

                        healthBlockBar.transform.localPosition = new Vector3(500 + (-500 * hpScale), 0, 0);



                        textBox.Text.text = $"{health:n0}" + "/" + $"{maxHealth:n0}";
                        nameText.Show();



                    }
                    else
                    {

                        // If the Bloon was destroyed, hides the UI

                        mainBossPanel.Hide();


                    }
                }
                else
                {
                    mainBossPanel.Destroy();
                }
               
            }

            public void UpdateInfo()
            {
                // Updates text and icons for different bosses

                InGame.instance.bridge.GetBloonFromId(bloon).spawnRound += registeration.continueRounds;

                nameText.SetText( registeration.displayName);
                bossIcon.AddImage(new BTD_Mod_Helper.Api.Components.Info("BossIcon", 0, 0, 150), ModContent.GetSpriteReference<BossHandler>(registeration.icon).GetGUID());
                //bossIcon.Image.sprite = ModContent.GetSprite<BossHandler>(registeration.icon);
                mainBossPanel.Show();

                showDescription = false;
                descriptionBox.Hide();
                if (registeration.usesExtraInfo)
                {

                    extraIcon.Image.sprite = ModContent.GetSprite<BossHandler>(registeration.extraInfoIcon);
                    extraTextObject.SetText(extraText);
                  

                    extraPanel.Show();
                }
                else
                {
                    extraPanel.Hide();
                }





                description.SetText("\n" + registeration.description);

                descriptionBox.Image.rectTransform.sizeDelta = new UnityEngine.Vector2(registeration.sizeX, registeration.sizeY);
                    description.Text.rectTransform.sizeDelta = new UnityEngine.Vector2(registeration.sizeX, registeration.sizeY);

                    if (registeration.usesExtraInfo)
                    {
                        descriptionBox.gameObject.transform.localPosition = new Vector3(0, (-registeration.sizeY / 2) - 200);
                    }
                    else
                    {
                        descriptionBox.gameObject.transform.localPosition = new Vector3(0, (-registeration.sizeY / 2) - 50);
                    }

                // If there is no description, removes the toggle from view

                if (registeration.description != "")
                {
                    descriptionToggle.gameObject.transform.localPosition = new Vector3(550, 0, 0);
                }
                else
                {
                    descriptionToggle.gameObject.transform.localPosition = new Vector3(1000, 1000, 0);
                }
            }


        }
        /// <summary>
        /// Returns a copy of a Bad's BloonModel with various properties changed to match vanilla bosses. The health and speed you input here can be adjusted under mod helper settings and default to 1000 and 1 respectively. 
        /// </summary>
        public static BloonModel CreateBossBase(int health = 1000, float speed = 1)
        {
            BloonModel bossBase = Game.instance.model.GetBloon("Bad").Duplicate();


            bossBase.maxHealth =  (int)(health * 0.01f * HealthPercentMultiplier);

            bossBase.speed = (float)(speed * 0.01f * SpeedPercentMultiplier);

            bossBase.isBoss = true;
            bossBase.tags = new string[] { "Moabs", "Bad", "Boss" };

            bossBase.damageDisplayStates = new DamageStateModel[] { };

            bossBase.RemoveBehaviors<DamageStateModel>();
            
            bossBase.RemoveBehavior<SpawnChildrenModel>();

            bossBase.mods = new ApplyModModel[] { };
            return bossBase;
        }

        /// <summary>
        ///  Creates and attachs a Monobehavior to the InGame UI.
        /// </summary>
        public static T StartMonobehavior<T>() where T : MonoBehaviour
        {

            var obj = InGame.instance.GetInGameUI().AddComponent<T>();

            return obj as T;

        }
            [HarmonyPatch(typeof(Bloon), nameof(Bloon.Initialise))]
        public class BloonSpawn
        {
            [HarmonyPostfix]
            public static void Postfix(Bloon __instance)
            {
               
              // When a Bloon spawns, checks if it was registered as a boss.
              
                if (bossRegisterations.ContainsKey(__instance.bloonModel.id))
                {

                    BossRegisteration registration = bossRegisterations[__instance.bloonModel.id];


                    // If the boss is a main boss, it will create the boss UI.
                    if (registration.isMainBoss)
                    {

                        if (bossPanel == null)
                        {
                       
                            /*
                             If there is already a bossPanel, it wont create another one.
                             You should not have multiple main bosses at once as the UIs would just block eachother

                            Any additional bosses or minions should have "isMainBoss" set to false. It wont occupy
                            the UI panel but will still run BossInit

                            */
                            bossPanel = InGame.instance.GetInGameUI().AddComponent<BossPanel>();
                            bossPanel.bloon = __instance.Id;
                            bossPanel.registeration = registration;
                        }
                        else
                        {
                            bossPanel.bloon = __instance.Id;
                            bossPanel.registeration = registration;
                            bossPanel.UpdateInfo();
                        }

                       

                        
                    }
                    
                 
                    
                    
                    /*
                     Runs Boss Init and passes through the Bloon along with tis registration info, so the modder
                     can alter the boss further from there.

                     Non-main bosses will still run BossInit incase you want minions to run code when spawned.

                    */
                    


                    BossInit(__instance, __instance.bloonModel, registration);
            
                }
            }
        }
       



    }
}