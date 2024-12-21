using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMod.Towers.PresentLauncher.Upgrades.Top
{
    public class BigPresents : ModUpgrade<PresentLauncher>
    {
        public override int Path => TOP;

        public override int Tier => 3;

        public override int Cost => 15;

        public override string Description => "Presents are 25% larger and do 1 more damage.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var proj = towerModel.GetWeapon().projectile;
            proj.scale *= 1.25f;
            proj.radius *= 1.25f;

            proj.GetDamageModel().damage += 1;
        }
    }
}
