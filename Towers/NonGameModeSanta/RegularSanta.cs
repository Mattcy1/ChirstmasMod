using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.TowerSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMod.Towers.NonGameModeSanta
{
    public class RegularSanta : ModTower
    {
        public override string Portrait => "Santa";
        public override string Icon => "Santa";

        public override TowerSet TowerSet => TowerSet.Primary;
        public override string BaseTower => TowerType.DartMonkey;
        public override int Cost => 300;

        public override string Description => "Santa has come to help us save christmas! After the grinch stole all the gifts";

        public override int ShopTowerCount => 1;

        public override string DisplayName => "Santa";

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            towerModel.range += 30;
            towerModel.GetAttackModel().range += 30;
            towerModel.GetWeapon().projectile.GetDamageModel().damage += 1;
        }
    }
}
