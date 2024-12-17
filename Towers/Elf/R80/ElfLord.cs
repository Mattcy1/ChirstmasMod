using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using HarmonyLib;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Simulation.Towers.Weapons;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using TemplateMod.Towers.Elf.R60;
using TemplateMod.Towers.PresentLauncher;
using UnityEngine;

namespace TemplateMod.Towers.Elf.R80
{
    public class ElfLord : ModTower<ChristmasTowers>
    {
        internal static GameObject ShopButton;

        internal static bool AddedToShop = false;

        protected override int Order => 2;

        public override string Icon => Portrait;

        public override string Description => "Santa's strongest elf, throws presents full of... <b>BOMBS??</b>";

        public override string BaseTower => TowerID<StronkElf>();

        public override int Cost => 25000;

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            var wpn = towerModel.GetWeapon();
            var proj = wpn.projectile;

            proj.ApplyDisplay<Present>();
            proj.id = "ElfPresent";
            proj.GetBehavior<CreateProjectileOnContactModel>().emission = new ArcEmissionModel("ArcEmissionModel", 6, 0, 360, null, true, false);

            towerModel.RemoveBehavior<TowerExpireModel>();

            var ability = new AbilityModel("giftsForAll", "Gifts For All", "Gifts For All: Explodes every bloon for 5000 damage.", 1, 0, GetSpriteReference("GiftsForAll"), 120, null, true, false, "", 1, 0, -1, false, false);
            towerModel.AddBehavior(ability);
        }

        public class ElfLordDisplay : ModTowerDisplay<ElfLord>
        {
            public override string BaseDisplay => MonkeyVillageElfPet;

            public override bool UseForTower(params int[] tiers) => true;

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                foreach (var rend in node.GetMeshRenderers())
                {
                    rend.SetMainTexture(GetTexture("ElfLord"));
                    rend.ApplyOutlineShader();
                }
            }
        }

        [HarmonyPatch(typeof(Weapon), nameof(Weapon.Emit))]
        static class Weapon_Emit
        {
            [HarmonyPostfix]
            public static void Postfix(Weapon __instance)
            {
                var proj = __instance.weaponModel.projectile;

                if(proj.id == "ElfPresent")
                {
                    CreateProjectileOnContactModel model = proj.GetBehavior<CreateProjectileOnContactModel>();

                    System.Random random = new();
                    var num = random.Next(101);

                    if (num < 5)
                    {
                        model.projectile = Game.instance.model.GetTowerFromId("BombShooter-502").GetWeapon().projectile.Duplicate();
                    }
                    else if (num < 25)
                    {
                        model.projectile = Game.instance.model.GetTowerFromId("BombShooter-402").GetWeapon().projectile.Duplicate();
                    }
                    else if (num <= 60)
                    {
                        model.projectile = Game.instance.model.GetTowerFromId("BombShooter-302").GetWeapon().projectile.Duplicate();
                    }
                    else if (num <= 100)
                    {
                        model.projectile = Game.instance.model.GetTowerFromId("BombShooter-202").GetWeapon().projectile.Duplicate();
                    }
                }
            }
        }
    }
}
