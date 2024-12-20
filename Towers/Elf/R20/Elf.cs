using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using HarmonyLib;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles;
using Il2CppAssets.Scripts.Unity.Display;
using TemplateMod.Bloons;
using UnityEngine;

namespace TemplateMod.Towers.Elf.R20
{
    public class Elf : ModTower<ChristmasTowers>
    {
        public override string BaseTower => TowerType.DartMonkey;

        public override bool DontAddToShop => true;

        public override string Icon => Portrait;

        public override string Description => "One of Santa's Minions, throws snow balls.";

        public override int Cost => 0;

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            towerModel.isSubTower = true;
            towerModel.range = 40;

            var proj = towerModel.GetWeapon().projectile;
            proj.GetDamageModel().damage = 2;
            towerModel.GetWeapon().rate = 0.5f;
            proj.ApplyDisplay<Snowball>();
            proj.GetBehavior<TravelStraitModel>().speed /= 4;
            proj.pierce = 1;
            proj.id = "Snowball_Elf";
            towerModel.AddBehavior(new TowerExpireModel("TowerExpireModel", 40, 3, false, false));
        }

        [HarmonyPatch(typeof(Projectile), nameof(Projectile.CollideBloon))]
        static class Projectile_CollideBloon
        {
            [HarmonyPostfix]
            public static void Postfix(Projectile __instance, Bloon bloon)
            {
                if(__instance.projectileModel.id == "Snowball_Elf" && bloon.bloonModel.baseId != BloonID<SnowBloon>())
                {
                    System.Random rand = new();

                    GetAudioClip<ChristmasMod.ChristmasMod>("SnowBloon_" + rand.Next(4)).Play();
                }
            }
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
