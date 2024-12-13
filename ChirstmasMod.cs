using MelonLoader;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using ChirstmasMod;
using HarmonyLib;
using Il2CppAssets.Scripts;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Map;
using Il2CppAssets.Scripts.Models.Rounds;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Simulation;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Simulation.Objects;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Unity.UI_New.Popups;
using Il2CppSystem;
using Il2CppSystem.Linq.Expressions.Interpreter;
using UnityEngine;

[assembly: MelonInfo(typeof(ChirstmasMod.ChirstmasMod), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace ChirstmasMod;

public class Values
{
    private static bool snowstorm = false;

    public static bool Snowstorm
    {
        get { return snowstorm; }
        set { snowstorm = value; }
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
}

public class ChirstmasMod : BloonsTD6Mod
{
    private static readonly System.Random random = new System.Random();
    
    public override void OnApplicationStart()
    {
        ModHelper.Msg<ChirstmasMod>("ChirstmasMod loaded!");
    }

    public override void OnTowerCreated(Tower tower, Entity target, Model modelToUse)
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
static class SnowstormPacth
{
    [HarmonyPostfix]
    public static void Postfix(Bloon __instance)
    {
        if (Values.Snowstorm == true)
        {
            BuffBloonSpeedModel buff = Game.instance.model.GetBloon("Vortex1").GetBehavior<BuffBloonSpeedModel>();
            buff.speedBoost = 1.5f;
            var mutator = buff.Mutator;
            __instance.AddMutator(mutator, 480);
        }
    }
}

[HarmonyPatch(typeof(Bloon), nameof(Bloon.Damage))]
static class SnowflakePacth
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
static class RoundPacth
{
    
    [HarmonyPostfix]
    public static void Postfix(Simulation __instance)
    {
        if (__instance.GetCurrentRound() == 0)
        {
            var text = "Help Santa defeat 5 different bosses sent by the Grinch to save Christmas!\nAfter the Grinch stole all the presents, you are the only one who can save Christmas! Each boss you face gets stronger and stronger, but so do you with every victory.\n\nDefeating all 5 bosses and collecting the 5 gifts will reward you with the ultimate prize: 10,000 Monkey Money.\n\nAre you ready for the challenge? The fate of Christmas is in your hands!";
            
            SantaStory.SantaStoryUI.CreateNormalSantaPanel(text, 37);
            
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
            
            SantaStory.SantaStoryUI.CreateWorriedSantaPanel(text, 50);
            InGame.instance.AddCash(1000);
        }
        
        if (__instance.GetCurrentRound() == 20)
        {
            var text = "You truly are the hero Christmas needs. Keep pushing forward only you can save Christmas! I can feel it... I’ve grown stronger, and so have you!";
            
            Gift.GiftUI.CreatePanel(1000, 10);
            SantaStory.SantaStoryUI.CreateSalutingSantaPanel(text, 50);
        }
        
        if (__instance.GetCurrentRound() == 21)
        {
            SantaStory.SantaStoryUI.instance.Close();
        }
        
        if (__instance.GetCurrentRound() == 19)
        {
            SantaStory.SantaStoryUI.instance.Close();
        }
        
        if (__instance.GetCurrentRound() == 1)
        {
            SantaStory.SantaStoryUI.instance.Close();
        }
    }
}




