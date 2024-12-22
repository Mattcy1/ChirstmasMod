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
using ChristmasMod.Bloons;
using UnityEngine;

namespace ChristmasMod.Moabs
{
    public class GingerbreadBfb : ModBloon
    {
        public override string BaseBloon => BloonType.sBfb;
        public override string Icon => "GingerbreadBfb-Icon";

        public override IEnumerable<string> DamageStates => [];
        public override void ModifyBaseBloonModel(BloonModel bloonModel)
        {
            bloonModel.RemoveAllChildren();
            bloonModel.AddToChildren<GingerbreadMoab>(4);
            bloonModel.maxHealth = 500;
            bloonModel.speed *= 1.25f;
        }
    }

    public class GingerbreadBfb0 : ModBloonDisplay<GingerbreadBfb>
    {
        public override string BaseDisplay => GetBloonDisplay(BloonType.sBfb);

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            foreach (var renderer in node.GetMeshRenderers())
            {
                renderer.SetMainTexture(GetTexture(Name));
            }
        }
    }
}
