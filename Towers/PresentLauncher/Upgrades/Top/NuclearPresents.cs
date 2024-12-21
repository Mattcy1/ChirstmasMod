using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMod.Towers.PresentLauncher.Upgrades.Top
{
    public class NuclearPresents : ModUpgrade<PresentLauncher>
    {
        public override int Path => TOP;

        public override int Tier => 5;

        public override int Cost => 145;

        public override string Description => "Presents do more damage and have even more devastating explosions.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var proj = towerModel.GetWeapon().projectile;

            proj.GetDamageModel().damage += 2;

            var boom = proj.GetDescendant<CreateProjectileOnExhaustFractionModel>();
            var boomFx = proj.GetBehavior<CreateEffectOnExhaustFractionModel>();
            boomFx.effectModel.scale *= 8;
            boom.projectile.scale *= 8;
            boom.projectile.GetDamageModel().damage *= 3;
        }
    }

    public class NuclearPresent : ModCustomDisplay
    {
        public override string AssetBundleName => "christmas2024";

        public override string PrefabName => "NuclearPresent";

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            foreach(var renderer in node.GetMeshRenderers())
            {
                renderer.ApplyOutlineShader();
                renderer.SetOutlineColor(new(0.5f, 1f, 0));
            }
        }
    }
}
