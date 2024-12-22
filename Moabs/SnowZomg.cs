﻿using BTD_Mod_Helper.Api.Bloons;
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
using Il2CppAssets.Scripts.Unity;
using ChristmasMod.Bloons;
using UnityEngine;

namespace ChristmasMod.Moabs
{
    public class SnowZomg : ModBloon
    {
        public override string BaseBloon => BloonType.sZomg;

        public override IEnumerable<string> DamageStates => [];

        public override void ModifyBaseBloonModel(BloonModel bloonModel)
        {
            bloonModel.RemoveAllChildren();
            bloonModel.AddToChildren<SnowBfb>(4);
            bloonModel.maxHealth = 3500;
            bloonModel.bloonProperties = Il2Cpp.BloonProperties.White;
        }

        public class SnowZomg0 : ModBloonDisplay<SnowZomg>
        {
            public override string BaseDisplay => Game.instance.model.GetBloon("Zomg").GetDisplayGUID();

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
