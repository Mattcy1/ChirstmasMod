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
    public class MilkMoab : ModBloon
    {
        public override string BaseBloon => BloonType.sMoab;

        public override void ModifyBaseBloonModel(BloonModel bloonModel)
        {
            bloonModel.RemoveAllChildren();
            bloonModel.AddToChildren<MilkBloon>(5);
            bloonModel.maxHealth = 1000;
            bloonModel.speed *= 1.25f;
        }
    }

    public class MilkMoabDisplay : ModBloonDisplay<MilkMoab>
    {
        public override string BaseDisplay => GetBloonDisplay(BloonType.sMoab);

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            foreach (var renderer in node.GetMeshRenderers())
            {
                renderer.SetMainTexture(GetTexture(Name));
            }
        }
    }

    public class MilkMoabDamage1Display : ModBloonDisplay<MilkMoab>
    {
        public override string BaseDisplay => GetBloonDisplay(BloonType.sMoab);

        public override int Damage => 1;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            foreach (var renderer in node.GetMeshRenderers())
            {
                renderer.SetMainTexture(GetTexture(Name));
            }
        }
    }

    public class MilkMoabDamage2Display : ModBloonDisplay<MilkMoab>
    {
        public override string BaseDisplay => GetBloonDisplay(BloonType.sMoab);

        public override int Damage => 2;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            foreach (var renderer in node.GetMeshRenderers())
            {
                renderer.SetMainTexture(GetTexture(Name));
            }
        }
    }

    public class MilkMoabDamage3Display : ModBloonDisplay<MilkMoab>
    {
        public override string BaseDisplay => GetBloonDisplay(BloonType.sMoab);

        public override int Damage => 3;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            foreach (var renderer in node.GetMeshRenderers())
            {
                renderer.SetMainTexture(GetTexture(Name));
            }
        }
    }

    public class MilkMoabDamage4Display : ModBloonDisplay<MilkMoab>
    {
        public override string BaseDisplay => GetBloonDisplay(BloonType.sMoab);

        public override int Damage => 4;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            foreach (var renderer in node.GetMeshRenderers())
            {
                renderer.SetMainTexture(GetTexture(Name));
            }
        }
    }
}
