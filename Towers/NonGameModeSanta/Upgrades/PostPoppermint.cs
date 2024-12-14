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

namespace TemplateMod.Towers.NonGameModeSanta.Upgrades
{
    public class PostPoppermint : ModUpgrade<RegularSanta>
    {
        public override int Path => Middle;

        public override int Tier => 1;

        public override int Cost => 515;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetWeapon().rate /= 2;
            towerModel.IncreaseRange(5);
        }
    }
}
