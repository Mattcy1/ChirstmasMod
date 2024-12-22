using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmasMod.Towers.PresentLauncher.Upgrades.Middle
{
    public class WonderfulGifts : ModUpgrade<PresentLauncher>
    {
        public override int Path => Middle;

        public override string Description => "Some primary weapons are now T5, some from other tower sets increased to t4";

        public override int Tier => 4;

        public override int Cost => 105;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetWeapon().projectile.id = "PresentT3";
        }
    }
}
