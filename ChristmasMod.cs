global using SantaEmotion = TemplateMod.UI.SantaStory.SantaEmotion;
global using SantaMessage = TemplateMod.UI.SantaStory.SantaMessage;

using BTD_Mod_Helper;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using ChristmasMod;
using HarmonyLib;
using Il2CppAssets.Scripts;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Map;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Simulation;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Simulation.Objects;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Bridge;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Unity.UI_New.InGame.RightMenu;
using Il2CppAssets.Scripts.Unity.UI_New.InGame.StoreMenu;
using Il2CppAssets.Scripts.Unity.UI_New.Popups;
using Il2CppNinjaKiwi.Common.ResourceUtils;
using MelonLoader;
using System.Linq;
using TemplateMod.Towers.Elf.R20;
using TemplateMod.Towers.PresentLauncher;
using TemplateMod.UI;
using UnityEngine;

[assembly: MelonInfo(typeof(ChristmasMod.ChristmasMod), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace ChristmasMod;

[HarmonyPatch(typeof(ShopMenu), nameof(ShopMenu.CreateTowerButton))]
static class ShopMenu_CreateTowerButton
{
    [HarmonyPostfix]
    public static void Postfix(ITowerPurchaseButton __result)
    {
        if (__result.TowerModel.baseId == ModContent.GetInstance<PresentLauncher>().Id && !PresentLauncher.AddedToShop)
        {
            ChristmasMod.PresentLauncherButton = __result.GameObject.transform.parent.gameObject;
            __result.GameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}

public class Values
{

    private static bool snowstorm = false;

    public static bool Snowstorm
    {
        get { return snowstorm; }
        set { snowstorm = value; }
    }
    
    private static bool Trivia1 = false;

    public static bool trivia1
    {
        get { return Trivia1; }
        set { Trivia1 = value; }
    }

    private static int snowstormRound = 0;

    public static int SnowstormRound
    {
        get { return snowstormRound; }
        set { snowstormRound = value; }
    }

    private static int Snowflake = 0;

    public static int snowflake
    {
        get { return Snowflake; }
        set { Snowflake = value; }
    }

    private static int Gift = 0;

    public static int gift
    {
        get { return Gift; }
        set { Gift = value; }
    }

    public static PrefabReference SnowstormPrefab;
}

public class ChristmasMod : BloonsTD6Mod
{
    private static readonly System.Random random = new System.Random();

    internal static GameObject PresentLauncherButton = null;

    public override void OnApplicationStart()
    {
        ModHelper.Msg<ChristmasMod>("ChirstmasMod loaded!");
    }

    public override void OnTowerCreated(Tower tower, Entity target, Model modelToUse)
    {
        if (Values.trivia1 == true)
        {
            if (tower.towerModel.baseId == "Mermonkey")
            {
                Gift.GiftUI.CreatePanel(2000, 50);

                Values.trivia1 = false;
                
                if (SantaStory.SantaStoryUI.instance != null)
                {
                    SantaStory.SantaStoryUI.instance.Close();
                }
                
                var text = "The correct answer was Mermonkey! Enjoy your gift!";
                SantaStory.SantaStoryUI.CreatePanel(SantaEmotion.SantaHappy, text);
            }
        }
        
        if (Values.Snowstorm == true)
        {
            var towerModel = tower.rootModel.Cast<TowerModel>().Duplicate();

            towerModel.range -= 10;

            foreach (var attack in towerModel.GetAttackModels())
            {
                attack.range -= 10;
            }
            tower.UpdateRootModel(towerModel);
        }
    }

    public override void OnTowerDestroyed(Tower tower)
    {
        if (tower.towerModel.baseId == ModContent.TowerID<Santa>())
        {
            bool towerPlaced = false;
            Il2CppSystem.Action<bool> something = (Il2CppSystem.Action<bool>)delegate (bool s)
            {
                towerPlaced = s;
            };
            Il2CppSystem.Action<bool> spawn = something;

            InGame.instance.bridge.CreateTowerAt(new Vector2(0, 0), ModContent.GetTowerModel<Santa>(), ObjectId.Create(9999, 0), false, something, true, true, false, 0);
        }
    }

    //public override void OnUpdate()
    //{
    //    if (Values.Snowstorm == true)
    //    {
    //        Values.SnowstormPrefab = Game.instance.model.GetTowerFromId("IceMonkey-040").GetDescendant<ActivateAttackModel>().GetDescendant<CreateEffectOnAbilityModel>().effectModel.assetId;
    //    
    //        InGame.instance.bridge.Simulation.SpawnEffect(Values.SnowstormPrefab, new Vector3(0, 140, 0), 0, 1, 0.1f);
    //    }
    //}

    public override void OnTowerUpgraded(Tower tower, string upgradeName, TowerModel newBaseTowerModel)
    {
        if (Values.Snowstorm == true)
        {
            var towerModel = tower.rootModel.Cast<TowerModel>().Duplicate();

            towerModel.range -= 10;

            foreach (var attack in towerModel.GetAttackModels())
            {
                attack.range -= 10;
            }
            tower.UpdateRootModel(towerModel);
        }
    }

    public override void OnNewGameModel(GameModel result, MapModel map)
    {
        OpenerUI.CreatePanel();
    }

    public override void OnRoundEnd()
    {
        if (random.Next(10) == 0)
        {
            PopupScreen.instance?.ShowOkPopup("Snowstorm started");
            Values.Snowstorm = true;
            Values.SnowstormRound = 3;

            MelonLogger.Msg("Snowstorm Starting" + Values.Snowstorm + Values.SnowstormRound);

            foreach (var tower in InGame.instance.GetTowers())
            {
                var towerModel = tower.rootModel.Cast<TowerModel>().Duplicate();

                towerModel.range -= 10;

                foreach (var attack in towerModel.GetAttackModels())
                {
                    attack.range -= 10;
                }
                tower.UpdateRootModel(towerModel);
            }

        }

        if (Values.Snowstorm == true && Values.SnowstormRound > 0)
        {
            Values.SnowstormRound -= 1;
            MelonLogger.Msg("Removing 1 from snowstorm round" + Values.SnowstormRound);
        }

        if (Values.Snowstorm == true && Values.SnowstormRound == 0)
        {
            Values.Snowstorm = false;

            MelonLogger.Msg("Snowstorm finished" + Values.Snowstorm);

            foreach (var tower in InGame.instance.GetTowers())
            {
                var towerModel = tower.rootModel.Cast<TowerModel>().Duplicate();

                towerModel.range += 10;

                foreach (var attack in towerModel.GetAttackModels())
                {
                    attack.range += 10;
                }
                tower.UpdateRootModel(towerModel);
            }
        }
    }
}

public class Santa : ModTower
{
    public override string Portrait => "Santa";
    public override string Icon => "Santa";

    public override TowerSet TowerSet => TowerSet.Primary;
    public override string BaseTower => TowerType.DartMonkey;
    public override int Cost => 0;
    public override bool DontAddToShop => false;

    public override int TopPathUpgrades => 0;
    public override int MiddlePathUpgrades => 0;
    public override int BottomPathUpgrades => 0;
    public override string Description => "Sante has come to help us save christmas! after the grinch stole all the gits";

    public override string DisplayName => "Santa";

    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        towerModel.range += 30;
        towerModel.GetAttackModel().range += 30;
        towerModel.GetWeapon().projectile.GetDamageModel().damage += 1;
        towerModel.dontDisplayUpgrades = true;
        towerModel.canAlwaysBeSold = false;
        towerModel.blockSelling = true;
    }
}

[HarmonyPatch(typeof(Bloon), nameof(Bloon.OnSpawn))]
static class SnowstormPatch
{
    [HarmonyPostfix]
    public static void Postfix(Bloon __instance)
    {
        if (Values.Snowstorm == true)
        {
            BuffBloonSpeedModel buff = Game.instance.model.GetBloon("Vortex1").GetBehavior<BuffBloonSpeedModel>();
            buff.speedBoost = 1.5f;
            var mutator = buff.Mutator;
            __instance.AddMutator(mutator, 99999);
        }
    }
}

[HarmonyPatch(typeof(Bloon), nameof(Bloon.Damage))]
static class SnowflakePatch
{
    private static readonly System.Random random = new System.Random();

    [HarmonyPostfix]
    public static void Postfix(Bloon __instance)
    {
        if (random.Next(75) == 0)
        {
            Values.snowflake++;
            MelonLogger.Msg("Snowflakes: " + Values.snowflake);
        }
    }
}

[HarmonyPatch(typeof(Simulation), nameof(Simulation.RoundStart))]
static class RoundPatch
{

    [HarmonyPostfix]
    public static void Postfix(Simulation __instance)
    {
        if (__instance.GetCurrentRound() == 2)
        {
            var text = "Help Santa defeat 5 different bosses sent by the Grinch to save Christmas!\nAfter the Grinch stole all the presents, you are the only one who can save Christmas! Each boss you face gets stronger and stronger, but so do you with every victory.\n\nDefeating all 5 bosses and collecting the 5 gifts will reward you with the ultimate prize: 10,000 Monkey Money.\n\nAre you ready for the challenge? The fate of Christmas is in your hands!";

            SantaStory.SantaStoryUI.CreatePanel(SantaEmotion.SantaHappy, text);

            bool towerPlaced = false;
            Il2CppSystem.Action<bool> something = (Il2CppSystem.Action<bool>)delegate (bool s)
            {
                towerPlaced = s;
            };
            Il2CppSystem.Action<bool> spawn = something;

            InGame.instance.bridge.CreateTowerAt(new Vector2(0, 0), ModContent.GetTowerModel<Santa>(), ObjectId.Create(9999, 0), false, something, true, true, false, 0);
        }

        if (__instance.GetCurrentRound() == 18)
        {
            var text = "The Poppermint is approaching next round! Prepare yourself for the fight. Here’s $1000 to help your defenses spend it wisely!";

            SantaStory.SantaStoryUI.CreatePanel(SantaEmotion.SantaWorry, text);
            InGame.instance.AddCash(1000);
        }

        if (__instance.GetCurrentRound() == 20)
        {
            SantaMessage[] messages = [new("You truly are the hero Christmas needs. Keep pushing forward only you can save Christmas! I can feel it... I’ve grown stronger, and so have you!", SantaEmotion.SantaSalute), new("I have added the present launcher to the shop! This tower uses snowflakes so be sure to not sell all of them.", SantaEmotion.SantaHappy)];

            SantaStory.SantaStoryUI.CreatePanel(messages);

            ChristmasMod.PresentLauncherButton.SetActive(true);

            Gift.GiftUI.CreatePanel(1000, 10);

            foreach (TowerToSimulation tower in InGame.instance.bridge.GetAllTowers().ToList())
            {
                if (tower.tower.towerModel.baseId == ModContent.TowerID<Santa>())
                {
                    var tm = tower.tower.rootModel.Cast<TowerModel>().Duplicate();

                    tm.GetWeapon().rate = 0.5f;
                    tm.range += 5;
                    tm.GetAttackModel().range += 5;

                    tower.tower.UpdateRootModel(tm);
                }
            }
        }

        if (__instance.GetCurrentRound() == 29)
        {
            var text = "Can you guess the tower? Which tower becomes less effective when placed in specific map locations, despite not having any direct penalties or debuffs listed?";
            Values.trivia1 = true;
            SantaStory.SantaStoryUI.CreatePanel(SantaEmotion.SantaHappy, text);
        }

        if (__instance.GetCurrentRound() == 38)
        {
            var text = "Watch out! I’ve heard the next boss is incredibly tough. Not only is he immune to ice attacks, but he also creates a devastating snowstorm while he’s on the battlefield!";

            SantaStory.SantaStoryUI.CreatePanel(SantaEmotion.SantaWorry, text);
        }

        if (__instance.GetCurrentRound() == 39)
        {
            Values.Snowstorm = true;
            Values.SnowstormRound = 1;
            PopupScreen.instance?.ShowOkPopup("Snowstorm started");
        }

        if (__instance.GetCurrentRound() == 40)
        {
            var text = "Good job, Soldier! The boss has been defeated, and now there are only 3 more to go! You're making great progress! And by the way, my workers have arrived to help you out things are looking even better now. Keep going, you're almost there!";

            SantaStory.SantaStoryUI.CreatePanel(SantaEmotion.SantaSalute, text);
            Gift.GiftUI.CreatePanel(5000, 100);

            foreach (TowerToSimulation tower in InGame.instance.bridge.GetAllTowers().ToList())
            {
                if (tower.tower.towerModel.baseId == ModContent.TowerID<Santa>())
                {
                    var tm = tower.tower.rootModel.Cast<TowerModel>().Duplicate();

                    AttackModel[] Avatarspawner = { Game.instance.model.GetTowerFromId("EngineerMonkey-200").GetAttackModels().First(a => a.name == "AttackModel_Spawner_").Duplicate() };
                    Avatarspawner[0].weapons[0].rate = 5f;
                    Avatarspawner[0].weapons[0].projectile.RemoveBehavior<CreateTowerModel>();
                    Avatarspawner[0].name = "ElfSpawner";
                    Avatarspawner[0].weapons[0].projectile.AddBehavior(new CreateTowerModel("CreateTower", ModContent.GetTowerModel<Elf>(), 0, false, false, false, false, false));
                    tm.AddBehavior(Avatarspawner[0]);

                    tower.tower.UpdateRootModel(tm);
                }
            }
        }
    }
}
