using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmasMod.Towers.PresentLauncher.Upgrades.Middle
{
    public class LighterPresents : ModUpgrade<PresentLauncher>
    {
        public override int Path => TOP;

        public override int Tier => 1;

        public override string Description =>"Lighter Present allows for more attack speed";

        public override int Cost => 5;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetWeapon().rate -= 0.1f;
        }
    }
}
