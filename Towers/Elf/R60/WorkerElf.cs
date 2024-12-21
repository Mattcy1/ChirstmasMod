using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using ChristmasMod;
using HarmonyLib;
using Il2CppAssets.Scripts.Models.Audio;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using UnityEngine;

namespace TemplateMod.Towers.Elf.R60
{
    public class WorkerElf : ModTower<ChristmasTowers>
    {
        protected override int Order => 1;

        public static GameObject ShopButton = null;
        
        public override string BaseTower => TowerType.DartMonkey;

        public override string Icon => Portrait;

        public override string Description => "One of Santa's Minions who typically help santa get his money";

        public override int Cost => 10000;

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            towerModel.GetAttackModel().RemoveWeapon(towerModel.GetWeapon());
            towerModel.ApplyDisplay<Elf.R20.Elf.ElfDisplay>();
            
            var bananaFarmAttackModel = Game.instance.model.GetTowerFromId("BananaFarm-003").GetAttackModel().Duplicate();
            bananaFarmAttackModel.name = "ElfWorker_";
            bananaFarmAttackModel.weapons[0].projectile.GetBehavior<CashModel>().maximum = 150;
            bananaFarmAttackModel.weapons[0].projectile.GetBehavior<CashModel>().minimum = 150;
            bananaFarmAttackModel.weapons[0].GetBehavior<EmissionsPerRoundFilterModel>().count = 10;
            bananaFarmAttackModel.weapons[0].rate = 5f;
            towerModel.AddBehavior(bananaFarmAttackModel);
        }
    }

    public class WorkerElfDisplay : ModTowerDisplay<WorkerElf>
    {
        public override string BaseDisplay => MonkeyVillageElfPet;

        public override bool UseForTower(params int[] tiers) => true;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            SetMeshTexture(node, Name);
        }
    }
}
