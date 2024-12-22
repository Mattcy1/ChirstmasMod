using BTD_Mod_Helper.Api.Bloons;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Bloons;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppSystem.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChristmasMod.Bloons;

namespace ChristmasMod.Moabs
{
    public class IceMoab : ModBloon
    {
        public override string BaseBloon => BloonType.sMoab;

        public override void ModifyBaseBloonModel(BloonModel bloonModel)
        {
            bloonModel.maxHealth *= 2;
            bloonModel.speed /= 2;
            bloonModel.bloonProperties = Il2Cpp.BloonProperties.White | Il2Cpp.BloonProperties.Frozen;

            bloonModel.RemoveAllChildren();
            bloonModel.AddToChildren<IceBloon>(4);

            StunTowersInRadiusActionModel stunTowersInRadiusActionModel = new("StunTowersInRadiusActionModel", "freeze", 50, 1, 1, CreatePrefabReference<IceCubeOverlay>(), true);

            HealthPercentTriggerModel healthPercentTriggerModel = new("HealthPercentTriggerModel", false, new([0.8f, 0.6f, 0.4f, 0.2f, 0]), new(["freeze"]), false);

            bloonModel.AddBehavior(healthPercentTriggerModel);
            bloonModel.AddBehavior(stunTowersInRadiusActionModel);
        }
    }

    public class IceCubeOverlay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "IceCube");
        }
    }

    public class IceMoabDisplay : ModBloonDisplay<IceMoab>
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

    public class IceMoabDamage1Display : ModBloonDisplay<IceMoab>
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

    public class IceMoabDamage2Display : ModBloonDisplay<IceMoab>
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

    public class IceMoabDamage3Display : ModBloonDisplay<IceMoab>
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

    public class IceMoabDamage4Display : ModBloonDisplay<IceMoab>
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
