using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmasMod.Towers.PresentLauncher.Upgrades.Bottom
{
    public class MultiGift : ModUpgrade<PresentLauncher>
    {
        public override int Path => Bottom;

        public override int Tier => 3;

        public override int Cost => 16;

        public override string Description => "Present launcher shoots 3 presents instead of one.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetWeapon().emission = new ArcEmissionModel("ArcEmissionModel", 3, 0, 45, null, false, false);
        }
    }
}
