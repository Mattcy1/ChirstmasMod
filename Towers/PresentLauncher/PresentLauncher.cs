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
using NAudio.Utils;

namespace TemplateMod.Towers.PresentLauncher
{
    public class PresentLauncher : ModTower<ChristmasTowers>
    {

        public static bool AddedToShop = false;

        public override string BaseTower => TowerType.BombShooter;

        public override int Cost => 5;

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
            [HarmonyPrefix]
            public static bool Prefix(TowerToSimulation __instance, int pathIndex, bool isParagon)
            {
                if (__instance.tower.towerModel.baseId != TowerID<PresentLauncher>())
                {
                    return true;
                }

                var t = __instance.tower;
                var tm = t.towerModel;
                int tier = tm.tiers[pathIndex];

                float upgradeCost = __instance.GetUpgradeCost(pathIndex, tier + 1, 0, isParagon);

                if (upgradeCost < Values.snowflake)
                {
                    return false;
                }

                InGame.instance.AddCash(upgradeCost);
                Values.snowflake -= (int)upgradeCost;

                return true;
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

                __instance.AddCash(-t.SaleValue);

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
