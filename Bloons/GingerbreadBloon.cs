using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Bloons;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Extensions;
using HarmonyLib;
using Il2CppAssets.Scripts.Models.Bloons;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using MelonLoader;
using UnityEngine;
namespace TemplateMod.Bloons
{
    public class GingerbreadBloon : ModBloon
    {
        public override string BaseBloon => BloonType.sRainbow;

        public override string Icon => "GingerbredBloon-Icon"; // i skilled issued

        public override void ModifyBaseBloonModel(BloonModel bloonModel)
        {
            bloonModel.speed /= 2f;
            bloonModel.maxHealth = 80;

            bloonModel.AddToChildren(ModContent.BloonID<IceBloon>(), 1);
        }


        public class GingerbreadBloonDisplay : ModBloonDisplay<SnowBloon>
        {
            public override string BaseDisplay => GetBloonDisplay("Red");

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                Set2DTexture(node, "GingerbredBloon");
            }
        }
    }
}
