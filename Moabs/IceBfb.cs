using BTD_Mod_Helper.Api.Bloons;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Bloons;
using Il2CppAssets.Scripts.Unity.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using TemplateMod.Bloons;
using UnityEngine;

namespace TemplateMod.Moabs
{
    public class IceBfb : ModBloon
    {
        public override string BaseBloon => BloonType.sBfb;

        public override void ModifyBaseBloonModel(BloonModel bloonModel)
        {
            bloonModel.RemoveAllChildren();
            bloonModel.AddToChildren<IceMoab>(4);
            bloonModel.maxHealth = 575;
            bloonModel.isImmuneToSlow = true;
            bloonModel.bloonProperties = Il2Cpp.BloonProperties.White;
            
            StunTowersInRadiusActionModel stunTowersInRadiusActionModel = new("StunTowersInRadiusActionModel", "freeze", 50, 1, 1, CreatePrefabReference<IceCubeOverlay>(), true);

            HealthPercentTriggerModel healthPercentTriggerModel = new("HealthPercentTriggerModel", false, new([0.8f, 0.6f, 0.4f, 0.2f, 0]), new(["freeze"]), false);

            bloonModel.AddBehavior(healthPercentTriggerModel);
            bloonModel.AddBehavior(stunTowersInRadiusActionModel);
        }

        public class IceBfb0 : ModBloonDisplay<IceBfb>
        {
            public override string BaseDisplay => GetBloonDisplay(BloonType.sBfb);

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                foreach(var renderer in node.GetMeshRenderers())
                {
                    renderer.SetMainTexture(GetTexture(Name)); 
                }
            }
        }
    }
}
