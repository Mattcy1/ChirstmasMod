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
using Il2CppAssets.Scripts.Models.Towers.Weapons;
using Il2CppAssets.Scripts.Simulation;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Simulation.Towers.Weapons;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Bridge;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using MelonLoader;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChristmasMod.Towers.PresentLauncher
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
            proj.AddBehavior<DamageModel>(new("DamageModel_", 1, -1, true, false, true, Il2Cpp.BloonProperties.Lead, Il2Cpp.BloonProperties.Lead, false));
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
                int tiers = tm.tiers[pathIndex];
                int tier = tiers + 1;

                int cost = !isParagon ? __instance.tower.towerModel.GetUpgrade(pathIndex, tier).cost : 1150;



                if (Values.snowflake >= cost)
                {
                    t.worth = tm.cost + cost;
                    InGame.instance.AddCash(cost);
                    Values.snowflake -= cost;
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
                var num = random.Next(4);

                string[] ids = ["DartMonkey", "BoomerangMonkey", "BombShooter", "BananaFarm"];

                model.projectile = Game.instance.model.GetTowerFromId(ids[num]).GetWeapon().projectile.Duplicate();
            }
            else if (proj.id == "PresentT1")
            {
                CreateProjectileOnContactModel model = proj.GetBehavior<CreateProjectileOnContactModel>();

                System.Random random = new();

                string[] ids = ["DartMonkey-003", "DartMonkey-302", "BoomerangMonkey-300", "BombShooter-003", "BombShooter-300", "BombShooter-030", "GlueGunner-003", "GlueGunner-300", "TackShooter-030", "BananaFarm-020"];


                var num = random.Next(ids.Length);

                model.projectile = Game.instance.model.GetTowerFromId(ids[num]).GetWeapon().projectile.Duplicate();
            }
            else if (proj.id == "PresentT2")
            {
                CreateProjectileOnContactModel model = proj.GetBehavior<CreateProjectileOnContactModel>();

                System.Random random = new();

                Dictionary<string, int> weapons = new Dictionary<string, int>()
                {
                    ["DartMonkey-204"] = 0,
                    ["DartMonkey-502"] = 0,
                    ["BoomerangMonkey-402"] = 0,
                    ["BombShooter-204"] = 0,
                    ["BombShooter-402"] = 0,
                    ["BombShooter-240"] = 0,
                    ["GlueGunner-024"] = 0,
                    ["GlueGunner-420"] = 0,
                    ["TackShooter-040"] = 0,
                    ["TackShooter-400"] = 1,
                    ["IceMonkey-004"] = 0,
                    ["BoomerangMonkey-004"] = 0,
                    ["SniperMonkey-300"] = 0,
                    ["DartlingGunner-030"] = 0,
                    ["MonkeySub-003"] = 0,
                    ["MonkeyAce-300"] = 0,
                    ["SuperMonkey-302"] = 0,
                    ["SuperMonkey-204"] = 0,
                    ["NinjaMonkey-003"] = 2,
                    ["WizardMonkey-300"] = 0,
                    ["WizardMonkey-030"] = 2,
                    ["Alchemist-030"] = 0,
                    ["Alchemist-003"] = 2,
                    ["Druid-300"] = 1,
                    ["Druid-300"] = 2,
                    ["BananaFarm-320"] = 0,
                    ["EngineerMonkey-300"] = 1,
                };

                var num = random.Next(weapons.Count);

                var key = weapons.Keys.ToList()[num];

                model.projectile = Game.instance.model.GetTowerFromId(key).GetWeapon(weapons[key]).projectile.Duplicate();
            }
            else if (proj.id == "PresentT3")
            {
                CreateProjectileOnContactModel model = proj.GetBehavior<CreateProjectileOnContactModel>();

                System.Random random = new();


                Dictionary<string, int> weapons = new Dictionary<string, int>()
                {
                    ["DartMonkey-204"] = 0,
                    ["DartMonkey-502"] = 0,
                    ["BoomerangMonkey-502"] = 0,
                    ["BombShooter-204"] = 0,
                    ["BombShooter-502"] = 0,
                    ["BombShooter-240"] = 0,
                    ["GlueGunner-025"] = 0,
                    ["GlueGunner-420"] = 0,
                    ["TackShooter-040"] = 0,
                    ["TackShooter-500"] = 1,
                    ["IceMonkey-004"] = 0,
                    ["BoomerangMonkey-004"] = 0,
                    ["SniperMonkey-400"] = 0,
                    ["DartlingGunner-040"] = 0,
                    ["MonkeySub-004"] = 0,
                    ["MonkeyAce-300"] = 0,
                    ["SuperMonkey-402"] = 0,
                    ["SuperMonkey-204"] = 0,
                    ["NinjaMonkey-003"] = 2,
                    ["WizardMonkey-300"] = 0,
                    ["WizardMonkey-040"] = 2,
                    ["Alchemist-030"] = 0,
                    ["Alchemist-003"] = 2,
                    ["Druid-300"] = 1,
                    ["Druid-300"] = 2,
                    ["BananaFarm-420"] = 0,
                    ["EngineerMonkey-400"] = 1,
                    ["EngineerMonkey-004"] = 1
                };

                var num = random.Next(weapons.Count);

                var key = weapons.Keys.ToList()[num];

                model.projectile = Game.instance.model.GetTowerFromId(key).GetWeapon(weapons[key]).projectile.Duplicate();
            }
            else if (proj.id == "PresentT4")
            {
                CreateProjectileOnContactModel model = proj.GetBehavior<CreateProjectileOnContactModel>();

                System.Random random = new();

                Dictionary<string, int> weapons = new Dictionary<string, int>()
                {
                    ["DartMonkey-205"] = 0,
                    ["DartMonkey-502"] = 0,
                    ["BoomerangMonkey-502"] = 0,
                    ["BombShooter-205"] = 0,
                    ["BombShooter-502"] = 0,
                    ["BombShooter-250"] = 0,
                    ["GlueGunner-025"] = 0,
                    ["GlueGunner-520"] = 0,
                    ["TackShooter-050"] = 0,
                    ["TackShooter-500"] = 1,
                    ["IceMonkey-005"] = 0,
                    ["BoomerangMonkey-005"] = 0,
                    ["SniperMonkey-500"] = 0,
                    ["DartlingGunner-050"] = 0,
                    ["MonkeySub-005"] = 0,
                    ["MonkeyAce-500"] = 0,
                    ["SuperMonkey-502"] = 0,
                    ["SuperMonkey-205"] = 0,
                    ["NinjaMonkey-005"] = 2,
                    ["WizardMonkey-500"] = 0,
                    ["WizardMonkey-050"] = 2,
                    ["Alchemist-050"] = 0,
                    ["Alchemist-005"] = 1,
                    ["Druid-500"] = 1,
                    ["Druid-500"] = 2,
                    ["BananaFarm-520"] = 0,
                    ["EngineerMonkey-500"] = 1,
                    ["EngineerMonkey-005"] = 1
                };

                var num = random.Next(weapons.Count);

                var key = weapons.Keys.ToList()[num];

                model.projectile = Game.instance.model.GetTowerFromId(key).GetWeapon(weapons[key]).projectile.Duplicate();
            }
            else if (proj.id == "PresentParagon")
            {
                List<TowerModel> paragons = Game.instance.model.towers.ToList().FindAll(tm => tm.isParagon);
                List<WeaponModel> weapons = [];

                System.Random random = new();

                foreach (var weps in paragons.Select(tm => tm.GetWeapons()))
                {
                    foreach(var wep in weps)
                    {
                        weapons.Add(wep);
                    }
                }

                CreateProjectileOnContactModel model = proj.GetBehavior<CreateProjectileOnContactModel>();

                int num = random.Next(weapons.Count);
                model.projectile = weapons[num].projectile.Duplicate();
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

                renderer.SetMainTexture(GetTexture("PresentLauncher000Texture"));
            }
        }
    }

    public class PresentLauncherDisplay : ModTowerCustomDisplay<PresentLauncher>
    {
        public override string AssetBundleName => "christmas2024";

        public override string PrefabName => "PresentLauncher2024";

        public override bool UseForTower(params int[] tiers) => !IsParagon(tiers);

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
