using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.TowerSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmasMod.Towers
{
    public class ChristmasTowers : ModTowerSet
    {
        public override string Portrait => Name + "-Portrait";
        public override string Container => Name + "-Container";
        public override string ContainerLarge => Name + "-ContainerLarge";
        public override string Button => Name + "-Button";

        public override int GetTowerSetIndex(List<TowerSet> towerSets)
        {
            return towerSets.IndexOf(TowerSet.Primary) + 1;
        }
    }
}
