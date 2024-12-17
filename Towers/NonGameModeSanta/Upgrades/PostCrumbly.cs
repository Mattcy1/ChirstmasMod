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
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;
using TemplateMod.Towers.Elf.R60;

namespace TemplateMod.Towers.NonGameModeSanta.Upgrades
{
    public class PostCrumbly : ModUpgrade<RegularSanta>
    {
        public override int Path => Middle;

        public override string Icon => "CrumblyIcon";

        public override int Tier => 3;

        public override int Cost => 10000;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetWeapon().projectile.GetDamageModel().damage = 5;
            towerModel.GetWeapon().rate = 0.6f;
            var ability = Game.instance.model.GetTowerFromId("DartlingGunner-040").GetAbility().Duplicate();
            ability.RemoveBehavior<ActivateAttackModel>();
            ability.cooldown = 60;
            ability.Cooldown = 60;
            ability.SetName("SantaAbility");
            ability.displayName = "SantaAbility";
            ability.name = "SantaAbility";
            ability.icon = ModContent.GetSpriteReference<ChristmasMod.ChristmasMod>("GiftsParticle");
            towerModel.AddBehavior(ability);
            AttackModel[] Avatarspawner = { Game.instance.model.GetTowerFromId("EngineerMonkey-200").GetAttackModels().First(a => a.name == "AttackModel_Spawner_").Duplicate() };
            Avatarspawner[0].weapons[0].rate = 10f;
            Avatarspawner[0].weapons[0].projectile.RemoveBehavior<CreateTowerModel>();
            Avatarspawner[0].name = "ElfSpawner";
            Avatarspawner[0].weapons[0].projectile.AddBehavior(new CreateTowerModel("CreateTower", ModContent.GetTowerModel<StronkElf>(), 0, false, false, false, false, false));
            towerModel.AddBehavior(Avatarspawner[0]);
        }
    }
}
