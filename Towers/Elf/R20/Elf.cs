using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Unity.Display;
using UnityEngine;

namespace TemplateMod.Towers.Elf.R20
{
    public class Elf : ModTower
    {
        public override TowerSet TowerSet => TowerSet.Primary;

        public override string BaseTower => TowerType.DartMonkey;

        //public override bool DontAddToShop => true;

        public override string Icon => Portrait;

        public override int Cost => 0;

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            towerModel.isSubTower = true;
            towerModel.range = 20;

            var proj = towerModel.GetWeapon().projectile;
            proj.ApplyDisplay<Snowball>();
            proj.GetBehavior<TravelStraitModel>().speed /= 2;
            proj.GetBehavior<TravelStraitModel>().lifespan /= 2;

            towerModel.AddBehavior(new TowerExpireModel("TowerExpireModel", 40, 3, false, false));
        }

        public class Snowball : ModDisplay
        {
            public override string BaseDisplay => Generic2dDisplay;

            public override float Scale => 0.85f;

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                Set2DTexture(node, "Snowball");

                var trailRenderer = node.gameObject.AddComponent<TrailRenderer>();
                var gradient = new Gradient();
                gradient.colorKeys = new Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppStructArray<GradientColorKey>([new(new(1, 1, 1, 1), 0), new(new(1, 1, 1, 0), 1)]);
                trailRenderer.colorGradient = gradient;
                trailRenderer.widthCurve = new(new Keyframe(0, 10), new(1, 0));

                var trail = node.gameObject.AddComponent<ProjectileTrailEffect>();
                trail.trailRenderer = trailRenderer;
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
