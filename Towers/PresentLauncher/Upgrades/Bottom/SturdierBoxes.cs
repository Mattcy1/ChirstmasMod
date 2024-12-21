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

namespace TemplateMod.Towers.PresentLauncher.Upgrades.Middle
{
    public class SturdierBoxes : ModUpgrade<PresentLauncher>
    {
        public override int Path => Bottom;

        public override int Tier => 2;

        public override string Description =>"Sturdier boxes allow the presents to hit even more bloons, and travel even further.";

        public override int Cost => 8;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.IncreaseRange(5);
            towerModel.GetWeapon().projectile.GetBehavior<TravelStraitModel>().lifespan *= 1.1f;
            towerModel.GetWeapon().projectile.pierce += 2;
        }
    }
}
