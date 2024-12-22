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
    public class PrimaryPresents : ModUpgrade<PresentLauncher>
    {
        public override int Path => Middle;

        public override int Tier => 3;

        public override int Cost => 30;

        public override string Description => "Presents now have T4 primary projectiles and t3 projectiles from millitary, magic and support";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetWeapon().projectile.id = "PresentT2";
        }
    }
}
