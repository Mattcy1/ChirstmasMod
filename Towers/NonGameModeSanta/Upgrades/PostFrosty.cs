using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using HarmonyLib;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Simulation.Bloons.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTD_Mod_Helper.Api;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;

namespace TemplateMod.Towers.NonGameModeSanta.Upgrades
{
    public class PostFrosty : ModUpgrade<RegularSanta>
    {
        public override int Path => Middle;

        public override string Icon => "FrostyIcon";

        public override int Tier => 2;

        public override int Cost => 1000;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            //AttackModel[] Avatarspawner = { Game.instance.model.GetTowerFromId("EngineerMonkey-200").GetAttackModels().First(a => a.name == "AttackModel_Spawner_").Duplicate() };
            //Avatarspawner[0].weapons[0].rate = 5f;
            //Avatarspawner[0].weapons[0].projectile.RemoveBehavior<CreateTowerModel>();
            //Avatarspawner[0].name = "ElfSpawner";
            //Avatarspawner[0].weapons[0].projectile.AddBehavior(new CreateTowerModel("CreateTower", ModContent.GetTowerModel<Elf.R20.Elf>(), 0, false, false, false, false, false));
            //towerModel.AddBehavior(Avatarspawner[0]);
            
            //lot of errors here idk i tried to change the loading order but that didnt fix it
        }
    }
}
