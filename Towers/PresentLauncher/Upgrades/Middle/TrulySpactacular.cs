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
    public class TrulySpactacular : ModUpgrade<PresentLauncher>
    {
        public override int Path => Middle;

        public override string Description => "All weapons are now only T5. Also shoots 2x faster cuz y not";
        public override int Tier => 5;

        public override int Cost => 300;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetWeapon().rate /= 2;
            towerModel.GetWeapon().projectile.id = "PresentT4";
        }
    }
}
