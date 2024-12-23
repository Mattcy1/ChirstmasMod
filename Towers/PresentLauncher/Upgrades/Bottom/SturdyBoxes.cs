﻿using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmasMod.Towers.PresentLauncher.Upgrades.Middle
{
    public class SturdyBoxes : ModUpgrade<PresentLauncher>
    {
        public override int Path => Bottom;

        public override int Tier => 1;

        public override string Description =>"Sturdy boxes allow for the present to hit more bloons and travel further.";

        public override int Cost => 5;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetWeapon().projectile.pierce += 2;
            towerModel.GetWeapon().projectile.GetBehavior<TravelStraitModel>().lifespan *= 1.1f;
            towerModel.IncreaseRange(5);
        }
    }
}
