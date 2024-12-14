using BTD_Mod_Helper.Api.Bloons;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Extensions;
using HarmonyLib;
using Il2CppAssets.Scripts.Models.Bloons;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
namespace TemplateMod.Bloons
{
    public class SnowBloon : ModBloon
    {
        public override string BaseBloon => BloonType.sRed;

        public override string Icon => Name + "-Icon";

        public override void ModifyBaseBloonModel(BloonModel bloonModel)
        {
            bloonModel.speed *= 1.5f;
            bloonModel.maxHealth = 5;

            bloonModel.bloonProperties = Il2Cpp.BloonProperties.White;

            bloonModel.AddBehavior(Game.instance.model.GetBloon("Ceramic").GetBehavior<CreateSoundOnDamageBloonModel>().Duplicate());
        }


        public class SnowBloonDisplay : ModBloonDisplay<SnowBloon>
        {
            public override string BaseDisplay => GetBloonDisplay("Red");

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                Set2DTexture(node, "SnowBloon");
            }
        }

        [HarmonyPatch(typeof(Bloon), nameof(Bloon.Damage))]
        static class Bloon_Damage
        {
            [HarmonyPrefix]
            public static void Prefix(Bloon __instance, ref float totalAmount, Tower tower)
            {
                if (tower != null)
                {
                    var towerModel = tower.towerModel;

                    if (towerModel.baseId == "BombShooter" || towerModel.baseId == "MortarMonkey")
                    {
                        totalAmount = 999;
                    }
                }
            }
        }
    }
}
