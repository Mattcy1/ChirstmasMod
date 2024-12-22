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
    internal class PackedPresents : ModUpgrade<PresentLauncher>
    {
        public override int Path => Middle;

        public override int Tier => 1;

        public override string Description => "Presents now contain 6 items instead of one.";

        public override int Cost => 10;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var proj = towerModel.GetWeapon().projectile;
            proj.GetBehavior<CreateProjectileOnContactModel>().emission = new ArcEmissionModel("ArcEmissionModel", 6, 0, 360, null, true, false);
        }
    }
}
