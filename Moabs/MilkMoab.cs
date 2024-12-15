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
using TemplateMod.Bloons;
using UnityEngine;

namespace TemplateMod.Moabs
{
    public class MilkMoab : ModBloon
    {
        public override string BaseBloon => BloonType.sMoab;

        public override void ModifyBaseBloonModel(BloonModel bloonModel)
        {
            bloonModel.RemoveAllChildren();
            bloonModel.AddToChildren<MilkBloon>(5);
            bloonModel.maxHealth = 1000;
            bloonModel.speed *= 1.25f;
            bloonModel.bloonProperties = Il2Cpp.BloonProperties.White;
        }
    }
}
