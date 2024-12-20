using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using ChristmasMod;
using HarmonyLib;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Simulation;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Simulation.Towers.Weapons;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Bridge;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using MelonLoader;
using UnityEngine;

namespace TemplateMod.Towers.PresentLauncher
{
    public class PresentLauncher : ModTower<ChristmasTowers>
    {

        public static bool AddedToShop = false;

        public override ParagonMode ParagonMode => ParagonMode.Base000;

        public override string BaseTower => TowerType.BombShooter;

        public override int Cost => 10;

        public override string Portrait => Icon;

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            var proj = towerModel.GetWeapon().projectile;
            proj.ApplyDisplay<Present>();
            proj.id = "BasePresent";
            proj.RemoveBehavior<CreateEffectOnContactModel>();
            proj.GetBehavior<CreateProjectileOnContactModel>().emission = new ArcEmissionModel("ArcEmissionModel", 3, 0, 360, null, true, false);
        }



        [HarmonyPatch(typeof(Tower), nameof(Tower.Initialise))]
        static class Tower_Initialise
        {
            [HarmonyPostfix]
            public static void Postfix(Tower __instance, Model modelToUse)
            {
                if (modelToUse.Cast<TowerModel>().baseId != TowerID<PresentLauncher>())
                {
                    return;
                }
                if (Values.snowflake >= modelToUse.Cast<TowerModel>().cost)
                {
                    Values.snowflake -= (int)modelToUse.Cast<TowerModel>().cost;
                    return;
                }

                InGame.instance.AddCash(1);

                sellingPresentLauncher = true;

                __instance.SellTower();
            }
        }

        static bool sellingPresentLauncher = false;

        [HarmonyPatch(typeof(TowerToSimulation), nameof(TowerToSimulation.Upgrade))]
        static class TowerToSimulation_Upgrade
        {
            private static float upgradeCost;
            
            [HarmonyPrefix]
            public static bool Prefix(TowerToSimulation __instance, int pathIndex, bool isParagon)
            {
                if (__instance.tower.towerModel.baseId != TowerID<PresentLauncher>())
                {
                    return true;
                }

                var t = __instance.tower;
                var tm = t.towerModel;
                int tiers = tm.tiers[pathIndex];
                int tier = tiers + 1;

                if (pathIndex == 0 && tier == 1)
                {
                    upgradeCost = 5;
                }
                else if (pathIndex == 0 && tier == 2)
                {
                    upgradeCost = 8;
                }
                
                if (pathIndex == 1 && tier == 1)
                {
                    upgradeCost = 10;
                }
                else if (pathIndex == 1 && tier == 2)
                {
                    upgradeCost = 12;
                }
                else if (pathIndex == 1 && tier == 3)
                {
                    upgradeCost = 30;
                }
                else if (pathIndex == 1 && tier == 4)
                {
                    upgradeCost = 105;
                }
                else if (pathIndex == 1 && tier == 5)
                {
                    upgradeCost = 467;
                }
                
                if (pathIndex == 2 && tier == 1)
                {
                    upgradeCost = 5;
                }
                else if (pathIndex == 2 && tier == 2)
                {
                    upgradeCost = 10;
                }
                
                MelonLogger.Msg(upgradeCost);

                if (Values.snowflake >= upgradeCost)
                {
                    t.worth = tm.cost + upgradeCost;
                    InGame.instance.AddCash(upgradeCost);
                    Values.snowflake -= (int)upgradeCost;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [HarmonyPatch(typeof(InGame), nameof(InGame.SellTower))]
        static class InGame_SellTower
        {
            [HarmonyPostfix]
            public static void Postfix(InGame __instance, TowerToSimulation tower)
            {
                var t = tower.tower;
                var tm = t.towerModel;
                if(tm.baseId != TowerID<PresentLauncher>())
                {
                    return;
                }
                if(sellingPresentLauncher)
                {
                    return;
                }
                
                MelonLogger.Msg(t.SaleValue);

                __instance.AddCash(t.SaleValue);

                Values.snowflake += (int)t.SaleValue;
            }
        }
    }

    [HarmonyPatch(typeof(Weapon), nameof(Weapon.Emit))]
    static class Weapon_Emit
    {
        [HarmonyPostfix]
        public static void Postfix(Weapon __instance)
        {
            var proj = __instance.weaponModel.projectile;

            if (proj.id == "BasePresent")
            {
                CreateProjectileOnContactModel model = proj.GetBehavior<CreateProjectileOnContactModel>();

                System.Random random = new();
                var num = random.Next(3);

                string[] ids = ["DartMonkey", "BoomerangMonkey", "BombShooter"];

                model.projectile = Game.instance.model.GetTowerFromId(ids[num]).GetWeapon().projectile.Duplicate();
            }
            else if (proj.id == "PresentT1")
            {
                CreateProjectileOnContactModel model = proj.GetBehavior<CreateProjectileOnContactModel>();

                System.Random random = new();
                var num = random.Next(3);

                string[] ids = ["DartMonkey-003", "DartMonkey-302", "BoomerangMonkey-300", "BombShooter-003", "BombShooter-300", "BombShooter-030", "GlueGunner-003", "GlueGunner-300", "TackShooter-030"];


                num = random.Next(ids.Length);

                model.projectile = Game.instance.model.GetTowerFromId(ids[num]).GetWeapon().projectile.Duplicate();
            }
            else if (proj.id == "PresentT2")
            {
                CreateProjectileOnContactModel model = proj.GetBehavior<CreateProjectileOnContactModel>();

                System.Random random = new();
                var num = random.Next(3);

                string[] ids = ["DartMonkey-004,0", "DartMonkey-402,0", "BoomerangMonkey-402,0", "BombShooter-204,0", "BombShooter-402,0", "BombShooter-240,0", "GlueGunner-024,0", "GlueGunner-420,0", "TackShooter-040,0", "TackShooter-400,0", "IceMonkey-004,0", "BoomerangMonkey-004,0",
                    "SniperMonkey-300,0", "SniperMonkey-030,0", "DartlingGunner-030,0", "MonkeySub-003,0", "MonkeyAce-300,0", "SuperMonkey-300,0", "SuperMonkey-003,0", "NinjaMonkey-003,0", "WizardMonkey-300,0", "WizardMonkey-030,0", "WizardMonkey-030,1", "Alchemist-030,0", "Alchemist-003,1", "Druid-300,1", "Druid-300,2", "BananaFarm-320", "EngineerMonkey-300,1"];


                num = random.Next(ids.Length);

                //model.projectile = Game.instance.model.GetTowerFromId(ids[num].Split(',')[0]).GetWeapon(int.Parse(ids[num].Split(',')[1])).projectile.Duplicate();
            }
            else if (proj.id == "PresentT3")
            {
                CreateProjectileOnContactModel model = proj.GetBehavior<CreateProjectileOnContactModel>();

                System.Random random = new();
                var num = random.Next(3);

                string[] ids = ["DartMonkey-004,0", "DartMonkey-502,0", "BoomerangMonkey-502,0", "BombShooter-204,0", "BombShooter-502,0", "BombShooter-240,0", "GlueGunner-025,0", "GlueGunner-420,0", "TackShooter-040,0", "TackShooter-500,1", "IceMonkey-004,0", "BoomerangMonkey-004,0",
                    "SniperMonkey-400,0", "SniperMonkey-030,0", "DartlingGunner-040,0", "MonkeySub-004,0", "MonkeyAce-300,0", "SuperMonkey-400,0", "SuperMonkey-004,0", "NinjaMonkey-003,0", "WizardMonkey-300,0", "WizardMonkey-040,0", "WizardMonkey-030,1", "Alchemist-030,0", "Alchemist-003,1", "Druid-300,1", "Druid-300,2", "BananaFarm-420", "EngineerMonkey-400,1", "EngineerMonkey-004,1"];


                num = random.Next(ids.Length);

                //model.projectile = Game.instance.model.GetTowerFromId(ids[num].Split(',')[0]).GetWeapon(int.Parse(ids[num].Split(',')[1])).projectile.Duplicate();
            }
            else if (proj.id == "PresentT4")
            {
                CreateProjectileOnContactModel model = proj.GetBehavior<CreateProjectileOnContactModel>();

                System.Random random = new();
                var num = random.Next(3);

                string[] ids = ["DartMonkey-205,0", "DartMonkey-502,0", "BoomerangMonkey-502,0", "BombShooter-205,0", "BombShooter-502,0", "BombShooter-250,0", "GlueGunner-025,0", "GlueGunner-520,0", "TackShooter-050,0", "TackShooter-500,1", "IceMonkey-005,0", "BoomerangMonkey-005,0",
                    "SniperMonkey-500,0", "SniperMonkey-050,0", "DartlingGunner-050,0", "MonkeySub-005,0", "MonkeyAce-500,0", "SuperMonkey-502,0", "SuperMonkey-205,0", "NinjaMonkey-005,2", "WizardMonkey-500,0", "WizardMonkey-050,0", "WizardMonkey-050,1", "Alchemist-050,0", "Alchemist-005,1", "Druid-500,1", "Druid-500,2", "BananaFarm-520", "EngineerMonkey-500,1", "EngineerMonkey-005,1"];


                num = random.Next(ids.Length);
                //model.projectile = Game.instance.model.GetTowerFromId(ids[num].Split(',')[0]).GetWeapon(int.Parse(ids[num].Split(',')[1])).projectile.Duplicate();
            }
        }
    }

    public class Present : ModCustomDisplay
    {
        public override string AssetBundleName => "christmas2024";

        public override string PrefabName => "Present";

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            foreach (var renderer in node.GetMeshRenderers())
            {
                renderer.ApplyOutlineShader();
                if (renderer.name == "Present")
                {
                    renderer.SetOutlineColor(new(0f, 0.7f, 0f));
                }
                else if (renderer.name == "Bow")
                {
                    renderer.SetOutlineColor(new(0.7f, 0.5f, 0f));
                }
            }
        }
    }

    public class PresentLauncherDisplay : ModTowerCustomDisplay<PresentLauncher>
    {
        public override string AssetBundleName => "christmas2024";

        public override string PrefabName => "PresentLauncher2024";

        public override bool UseForTower(params int[] tiers) => true;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            foreach (var renderer in node.GetMeshRenderers())
            {
                renderer.ApplyOutlineShader();
                if (renderer.name == "Barrel_Green")
                {
                    renderer.SetOutlineColor(new(0f, 0.7f, 0f));
                }
                else if (renderer.name == "Barrel_Yellow")
                {
                    renderer.SetOutlineColor(new(0.7f, 0.7f, 0f));
                }
                else if (renderer.name == "Barrel_Red")
                {
                    renderer.SetOutlineColor(new(0.7f, 0f, 0f));
                }
                else if (renderer.name == "Wheels")
                {
                    renderer.SetOutlineColor(new(0, 0, 0.05f));
                }
                else if (renderer.name == "Axis")
                {
                    renderer.SetOutlineColor(new(0.1f, 0.1f, 0.1f));
                }
            }
        }
    }
}
