using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.Towers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMod.Towers.PresentLauncher.Upgrades.Paragon
{
    public class PresentGawd : ModParagonUpgrade<PresentLauncher>
    {
        public override int Cost => 1150;

        public override string Description => "Shooting presents at an unbelievable rate, this tower shreads with what lays inside the presents...";

        public override void ApplyUpgrade(TowerModel towerModel)
        {

        }
    }
}
