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
    public class Overload : ModUpgrade<PresentLauncher>
    {
        public override int Path => Middle;

        public override int Tier => 2;

        public override string Description =>"Presents contain 8 projectiles and can now have projectiles from a wider variaty of projectiles. (Most T3 Primary)";

        public override int Cost => 12;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetDescendant<CreateProjectileOnContactModel>().emission.Cast<ArcEmissionModel>().count = 8;

            towerModel.GetWeapon().projectile.id = "PresentT1";
        }
    }
}
