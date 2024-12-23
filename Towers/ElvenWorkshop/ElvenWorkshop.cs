using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using HarmonyLib;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;
using Il2CppAssets.Scripts.Simulation.Towers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ChristmasMod.Towers.ElvenWorkshop
{
    /*public class ElvenWorkshop : ModTower<ChristmasTowers>
    {
        public override string BaseTower => TowerType.BananaFarm;

        public override bool DontAddToShop => !ChristmasMod.saveData.unlockedTower;

        public override int Cost => 1200;

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            towerModel.GetWeapon().projectile.RemoveBehavior<CashModel>();
            towerModel.GetWeapon().projectile.AddBehavior<LivesModel>(new("LivesModel", 1, 1, 0));
            towerModel.GetWeapon().GetBehavior<EmissionsPerRoundFilterModel>().count = 5;
            towerModel.GetWeapon().rate = 4;
            towerModel.GetWeapon().projectile.ApplyDisplay<LifeCrate>();
        }

    }

    [HarmonyPatch(typeof(Tower), nameof(Tower.UpdatedModel))]
    static class Tower_UpdatedModel
    {
        [HarmonyPostfix]
        public static void Postfix(Tower __instance, Model modelToUse)
        {
            if(modelToUse.Cast<TowerModel>().baseId == ModContent.TowerID<ElvenWorkshop>())
            {
                __instance.GetUnityDisplayNode().animationController.SetSpeed(4 / modelToUse.Cast<TowerModel>().GetWeapon().rate);
            }
        }
    }

    public class LifeCrate : ModDisplay
    {
        public override string BaseDisplay => "6134c9e7bc875b04383c89476b6f2aff";
    }*/
}
