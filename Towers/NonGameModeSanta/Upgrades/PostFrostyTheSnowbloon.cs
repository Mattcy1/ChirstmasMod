using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using ChristmasMod;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;
using System.Linq;

namespace TemplateMod.Towers.NonGameModeSanta.Upgrades
{
    internal class PostFrostyTheSnowbloon : ModUpgrade<Santa>
    {
        public override int Path => Middle;

        public override int Tier => 1;

        public override int Cost => 530;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            AttackModel[] Avatarspawner = { Game.instance.model.GetTowerFromId("EngineerMonkey-200").GetAttackModels().First(a => a.name == "AttackModel_Spawner_").Duplicate() };
            Avatarspawner[0].weapons[0].rate = 5f;
            Avatarspawner[0].weapons[0].projectile.RemoveBehavior<CreateTowerModel>();
            Avatarspawner[0].name = "ElfSpawner";
            Avatarspawner[0].weapons[0].projectile.AddBehavior(new CreateTowerModel("CreateTower", ModContent.GetTowerModel<Elf>(), 0, false, false, false, false, false));
            towerModel.AddBehavior(Avatarspawner[0]);
        }
    }
}
