using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Unity.Display;
using UnityEngine;

namespace TemplateMod.Towers.Elf.R60
{
    public class StronkElf : ModTower
    {
        protected override int Order => 1;

        public override TowerSet TowerSet => TowerSet.Primary;

        public override string BaseTower => TowerID<R20.Elf>();

        //public override bool DontAddToShop => true;

        public override string Icon => Portrait;

        public override string Description => "One of Santa's Minions, throws balls of ice.";

        public override int Cost => 0;

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            var wpn = towerModel.GetWeapon();
            var proj = wpn.projectile;

            proj.GetDamageModel().damage += 2;
            proj.ApplyDisplay<IceBall>();
        }

        public class IceBall : ModDisplay
        {
            public override string BaseDisplay => Generic2dDisplay;

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                Set2DTexture(node, Name);

                var trailRenderer = node.gameObject.AddComponent<TrailRenderer>();
                var gradient = new Gradient();
                gradient.colorKeys = new Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppStructArray<GradientColorKey>([new(new(1, 1, 1, 1), 0), new(new(1, 1, 1, 0), 1)]);
                trailRenderer.colorGradient = gradient;
                trailRenderer.widthCurve = new(new Keyframe(0, 10), new(1, 0));

                var trail = node.gameObject.AddComponent<ProjectileTrailEffect>();
                trail.trailRenderer = trailRenderer;
            }
        }

        public class StronkElfDisplay : ModTowerDisplay<StronkElf>
        {
            public override string BaseDisplay => MonkeyVillageElfPet;

            public override bool UseForTower(params int[] tiers) => true;

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {

                foreach (var rend in node.GetMeshRenderers())
                {
                    rend.SetMainTexture(GetTexture("StronkElf"));
                    rend.ApplyOutlineShader();
                }
            }
        }
    }
}
