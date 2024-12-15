using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using ChristmasMod;
using HarmonyLib;
using Il2CppAssets.Scripts.Models.Audio;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using UnityEngine;

namespace TemplateMod.Towers.Elf.R60
{
    public class StronkElf : ModTower<ChristmasTowers>
    {
        protected override int Order => 1;

        public override string BaseTower => TowerID<R20.Elf>();

        public override bool DontAddToShop => true;

        public override string Icon => Portrait;

        public override string Description => "One of Santa's Minions who typically protects the North Pole against weak threats, throws balls of ice.";

        public override int Cost => 500;

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            var wpn = towerModel.GetWeapon();
            var proj = wpn.projectile;

            wpn.rate *= 0.8f;

            proj.GetDamageModel().damage += 2;
            proj.pierce = 1;
            proj.ApplyDisplay<IceBall>();
            proj.id = "StronkElfIceBall";

            var iceShard = Game.instance.model.GetTower("DartMonkey").GetWeapon().projectile.Duplicate();
            iceShard.ApplyDisplay<IceShard>();
            iceShard.pierce = 1;

            var createProjectileOnContactModel = new CreateProjectileOnContactModel("CreateProjectileOnContactModel", iceShard, new ArcEmissionModel("ArcEmissionModel", 1, 0, 90, null, true, false), true, true, true);
            proj.AddBehavior(createProjectileOnContactModel);
        }

        [HarmonyPatch(typeof(Projectile), nameof(Projectile.CollideBloon))]
        static class Projectile_CollideBloon
        {
            [HarmonyPostfix]
            public static void Postfix(Projectile __instance)
            {
                if (__instance.projectileModel.id == "StronkElfIceBall")
                {
                    System.Random rand = new();

                    GetAudioClip<ChristmasMod.ChristmasMod>("IceShatter" + rand.Next(4)).Play();
                }
            }
        }

        public class IceShard : IceBall 
        {
            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                Set2DTexture(node, Name);
            }
        }

        public class IceBall : ModDisplay
        {
            public override string BaseDisplay => Generic2dDisplay;

            public override float Scale => 0.85f;

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
