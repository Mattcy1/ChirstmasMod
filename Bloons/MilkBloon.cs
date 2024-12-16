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
using UnityEngine;
namespace TemplateMod.Bloons
{
    public class MilkBloon : ModBloon
    {
        public override string BaseBloon => BloonType.sRainbow;

        public override string Icon => Name + "-Icon";

        public override void ModifyBaseBloonModel(BloonModel bloonModel)
        {
            bloonModel.speed = Game.instance.model.GetBloon("Pink").speed;
            bloonModel.maxHealth = 200;

            bloonModel.AddToChildren(BloonType.sWhite, 1);

            bloonModel.bloonProperties = Il2Cpp.BloonProperties.White;
        }


        public class MilkBloonDisplay : ModBloonDisplay<MilkBloon>
        {
            public override string BaseDisplay => GetBloonDisplay("Red");
        
            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
               Set2DTexture(node, "MilkBloon");
            }
        }
    }
}
