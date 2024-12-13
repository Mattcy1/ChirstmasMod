using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Data;
using Il2CppAssets.Scripts.Data.Cosmetics.Pets;
using Il2CppAssets.Scripts.Data.TrophyStore;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Unity.Display;
using System.Linq;
using UnityEngine;

namespace TemplateMod.Towers.Elf.R20
{
    public class Elf : ModTower
    {
        public override TowerSet TowerSet => TowerSet.Primary;

        public override string BaseTower => TowerType.DartMonkey;

        //public override bool DontAddToShop => true;

        public override int Cost => 0;

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            towerModel.isSubTower = true;
            towerModel.range = 20;

            towerModel.AddBehavior(new TowerExpireModel("TowerExpireModel", 40, 3, false, false));
        }

        public class Snowball : ModDisplay
        {
            public override string BaseDisplay => Generic2dDisplay;

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                Set2DTexture(node, "Snowball");

                var trail = node.gameObject.AddComponent<ProjectileTrailEffect>();
                var gradient = new Gradient();
                gradient.colorKeys = new Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppStructArray<GradientColorKey>([new(new(1, 1, 1, 1), 0), new(new(1, 1, 1, 0), 1)]);
                trail.trailRenderer.colorGradient = gradient;
            }
        }

        public class ElfDisplay : ModTowerDisplay<Elf>
        {
            public override string BaseDisplay => MonkeyVillageElfPet;

            public override bool UseForTower(params int[] tiers) => true;

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {

                foreach (var rend in node.GetMeshRenderers())
                {
                    rend.SetMainTexture(GetTexture("Elf"));
                    rend.ApplyOutlineShader();
                }
            }
        }
    }
}
