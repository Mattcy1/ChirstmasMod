using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Unity.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Il2CppAssets.Scripts.Unity;
using BTD_Mod_Helper.Extensions;

namespace TemplateMod.Towers.PresentLauncher.Upgrades.Bottom
{
    public class StrongerPresentTurret : ModUpgrade<PresentLauncher>
    {
        public override int Path => Bottom;

        public override int Tier => 5;

        public override string Icon => Portrait;

        public override string Description => "Presents now contain Present Turrets, which shoot presents of their own.";

        public override string Portrait => "PresentLauncher-Portrait";

        public override int Cost => 205;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetWeapon(1).projectile.RemoveBehavior<CreateTowerModel>();

            var ctm = Game.instance.model.GetTowerFromId("EngineerMonkey-200").GetDescendant<CreateTowerModel>().Duplicate();
            var tm = ctm.tower;

            tm.name = "PresentTurret";
            tm.portrait = GetSpriteReference(Portrait);

            tm.GetWeapon().rate /= 6;
            tm.GetWeapon().projectile.GetDamageModel().damage += 5;

            tm.GetBehavior<TowerExpireModel>().lifespan /= 2;

            tm.GetWeapon().projectile.id = "PresentT3";
            tm.GetWeapon().projectile.ApplyDisplay<Present>();

            tm.GetAttackModel().ApplyDisplay<StrongerPresentTurretAttackDisplay>();
            tm.ApplyDisplay<StrongerPresentTurretDisplay>();

            towerModel.GetWeapon(1).projectile.AddBehavior(ctm);
        }
    }

    public class StrongerPresentTurretDisplay : ModDisplay
    {
        public override string BaseDisplay => GetDisplay(TowerType.Sentry);

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            SetMeshTexture(node, "PresentLauncherColours");
        }
    }
    public class StrongerPresentTurretAttackDisplay : ModDisplay
    {
        public override string BaseDisplay => Game.instance.model.GetTower(TowerType.Sentry).GetAttackModel().GetBehavior<DisplayModel>().display.AssetGUID;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            SetMeshTexture(node, "PresentLauncherColours");
        }
    }
}
