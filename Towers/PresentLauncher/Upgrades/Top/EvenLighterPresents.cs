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
    public class EvenLighterPresents : ModUpgrade<PresentLauncher>
    {
        public override int Path => TOP;

        public override int Tier => 2;

        public override string Description =>"Even Lighter Present allows for more attack speed";

        public override int Cost => 10;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetWeapon().rate -= 0.1f;
        }
    }
}
