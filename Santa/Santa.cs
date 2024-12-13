using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.TowerSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMod.Santa
{
    public class Santa : ModTower
    {
        public override TowerSet TowerSet => throw new NotImplementedException();

        public override string BaseTower => throw new NotImplementedException();

        public override int Cost => throw new NotImplementedException();

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            throw new NotImplementedException();
        }
    }
}
