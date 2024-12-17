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
    public class PostCookieMonster : ModUpgrade<RegularSanta>
    {
        public override int Path => Middle;

        public override string Icon => "CookieMonsterIcon";

        public override int Tier => 4;

        public override int Cost => 20000;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetWeapon().projectile.GetDamageModel().damage = 80;
            towerModel.range += 20;
            towerModel.GetAttackModel().range = towerModel.range;
            towerModel.GetWeapon().rate = 0.3f;
            var ability = towerModel.GetAbility();
            ability.SetName("SantaAbilityT2");
            ability.displayName = "SantaAbilityT2";
            ability.name = "SantaAbilityT2";

            towerModel.GetAttackModel(1).weapons[0].rate /= 2f;
            //towerModel.GetAttackModel(2).weapons[0].rate /= 2f;
        }
    }
}
