using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppAssets.Scripts.Unity.UI_New.InGame.EditorMenus;

namespace TemplateMod.Towers.PresentLauncher.Upgrades.Bottom
{
    public class PresentTurret : ModUpgrade<PresentLauncher>
    {
        public override int Path => Bottom;

        public override int Tier => 4;

        public override string Icon => Portrait;

        public override string Description => "Presents now contain Present Turrets, which shoot presents of their own.";

        public override int Cost => 35;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var ctm = Game.instance.model.GetTowerFromId("EngineerMonkey-200").GetDescendant<CreateTowerModel>().Duplicate();
            var tm = ctm.tower;

            tm.name = "PresentTurret";
            tm.portrait = GetSpriteReference(Portrait);

            tm.GetWeapon().rate /= 3;
            tm.GetWeapon().projectile.GetDamageModel().damage += 1;

            tm.GetBehavior<TowerExpireModel>().lifespan /= 2;

            tm.GetWeapon().projectile.id = "PresentT2";
            tm.GetWeapon().projectile.ApplyDisplay<Present>();

            tm.GetAttackModel().ApplyDisplay<PresentTurretAttackDisplay>();
            tm.ApplyDisplay<PresentTurretDisplay>();

            var newWep = towerModel.GetWeapon().Duplicate();
            newWep.projectile.AddBehavior(ctm);
            newWep.projectile.pierce = 0;
            newWep.projectile.id = "PresentTurretPresent";

            towerModel.GetAttackModel().AddWeapon(newWep);
        }
    }

    public class PresentTurretDisplay : ModDisplay
    {
        public override string BaseDisplay => GetDisplay(TowerType.Sentry);

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            SetMeshTexture(node, "PresentTurretColours");
        }
    }
    public class PresentTurretAttackDisplay : ModDisplay
    {
        public override string BaseDisplay => Game.instance.model.GetTower(TowerType.Sentry).GetAttackModel().GetBehavior<DisplayModel>().display.AssetGUID;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            SetMeshTexture(node, "PresentTurretColours");
        }
    }
}
