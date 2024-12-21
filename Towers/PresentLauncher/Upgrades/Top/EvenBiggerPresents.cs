using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMod.Towers.PresentLauncher.Upgrades.Top
{
    public class EvenBiggerPresents : ModUpgrade<PresentLauncher>
    {
        public override int Path => TOP;

        public override int Tier => 4;

        public override int Cost => 35;

        public override string Description => "Presents are even larger and do more damage and now explode.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var proj = towerModel.GetWeapon().projectile;
            proj.scale *= 1.3f;
            proj.radius *= 1.3f;

            proj.GetDamageModel().damage += 2;

            var boom = Game.instance.model.GetTower("BombShooter", 3).GetDescendant<CreateProjectileOnContactModel>().projectile.Duplicate();
            var boomFx = Game.instance.model.GetTower("BombShooter", 3).GetDescendant<CreateEffectOnContactModel>().effectModel.Duplicate();


            proj.AddBehavior(new CreateEffectOnExhaustFractionModel("CreateEffectOnExhaustFractionModel", boomFx, boomFx.lifespan, Il2CppAssets.Scripts.Models.Effects.Fullscreen.No, 1, 1, true));

            proj.AddBehavior(new CreateProjectileOnExhaustFractionModel("CreateProjectileOnExhaustFractionModel", boom, new SingleEmissionModel("SingleEmissionModel", null), 1, 1, false, true, false));
        }
    }
}
