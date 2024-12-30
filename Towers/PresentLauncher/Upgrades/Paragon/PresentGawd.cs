using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChristmasMod.Towers.PresentLauncher.Upgrades.Bottom;

namespace ChristmasMod.Towers.PresentLauncher.Upgrades.Paragon
{
    public class PresentGawd : ModParagonUpgrade<PresentLauncher>
    {
        public override int Cost => 1150;

        public override string Description => "Shooting presents at an unbelievable rate, this tower shreads with what lays inside the presents...";

        public override string DisplayName => "Present Gawd";

        public override string Icon => "PresentGawd-Icon";

        public override string Portrait => Icon;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var weapon = towerModel.GetWeapon();
            var proj = weapon.projectile;

            proj.ApplyDisplay<ParagonalPresent>();
            proj.id = "PresentParagon";
            proj.scale *= 2;
            proj.radius *= 2;
            proj.pierce = 10;

            weapon.rate /= 5;

            var ctm = Game.instance.model.GetTowerFromId("EngineerMonkey-200").GetDescendant<CreateTowerModel>().Duplicate();
            var tm = ctm.tower;

            tm.name = "PresentTurret";
            tm.portrait = GetSpriteReference("PresentTurretParagon-Portrait");

            tm.GetWeapon().rate = 0.7f; //Nerfing this shit oneshotting final boss
            tm.GetWeapon().projectile.GetDamageModel().damage += 10;

            tm.GetBehavior<TowerExpireModel>().lifespan /= 2;

            tm.GetWeapon().projectile.id = "PresentParargon";
            tm.GetWeapon().projectile.ApplyDisplay<ParagonalPresent>();

            tm.GetAttackModel().ApplyDisplay<PresentTurretParagonAttackDisplay>();
            tm.ApplyDisplay<PresentTurretParagonDisplay>();

            proj.AddBehavior(ctm);
        }
    }

    public class PresentGawdDisplay : ModTowerCustomDisplay<PresentLauncher>
    {
        public override string AssetBundleName => "christmas2024";

        public override string PrefabName => "PresentLauncher2024";

        public override bool UseForTower(params int[] tiers) => IsParagon(tiers);

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            foreach (var renderer in node.GetMeshRenderers())
            {
                renderer.ApplyOutlineShader();
                if (renderer.name == "Barrel_Green")
                {
                    renderer.SetMainTexture(GetTexture("PresentLauncherParagonTexture"));
                    renderer.SetOutlineColor(new(0f, 0f, 0.7f));
                }
                else if (renderer.name == "Barrel_Yellow")
                {
                    renderer.SetMainTexture(GetTexture("PresentLauncherParagonTexture"));
                    renderer.SetOutlineColor(new(0.7f, 0f, 0.25f));
                }
                else if (renderer.name == "Barrel_Red")
                {
                    renderer.SetMainTexture(GetTexture("PresentLauncherParagonTexture"));
                    renderer.SetOutlineColor(new(0.7f, 0f, 0.7f));
                }
                else if (renderer.name == "Wheels")
                {
                    renderer.SetMainTexture(GetTexture("PresentLauncherParagonTexture"));
                    renderer.SetOutlineColor(new(0, 0, 0.05f));
                }
                else if (renderer.name == "Axis")
                {
                    renderer.SetOutlineColor(new(0.1f, 0.1f, 0.1f));
                }
            }
        }
    }

    public class PresentTurretParagonDisplay : ModDisplay
    {
        public override string BaseDisplay => GetDisplay(TowerType.Sentry);

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            SetMeshTexture(node, "PresentTurretParagonColours");
        }
    }
    public class PresentTurretParagonAttackDisplay : ModDisplay
    {
        public override string BaseDisplay => Game.instance.model.GetTower(TowerType.Sentry).GetAttackModel().GetBehavior<DisplayModel>().display.AssetGUID;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            SetMeshTexture(node, "PresentTurretParagonColours");
        }
    }
    public class ParagonalPresent : ModCustomDisplay
    {

        public override string AssetBundleName => "christmas2024";

        public override string PrefabName => "Present";

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            foreach (var renderer in node.GetMeshRenderers())
            {
                renderer.ApplyOutlineShader();
                if (renderer.name == "Present")
                {
                    renderer.SetOutlineColor(new(0f, 0.0f, .7f));
                }
                else if (renderer.name == "Bow")
                {
                    renderer.SetOutlineColor(new(0.7f, 0.0f, 0.25f));
                }

                renderer.SetMainTexture(GetTexture("PresentLauncherParagonTexture"));
            }
        }
    }
}
