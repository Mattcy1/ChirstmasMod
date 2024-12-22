using BTD_Mod_Helper.Api.Bloons;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Extensions;
using ChristmasMod.Bloons;
using Il2CppAssets.Scripts.Models.Bloons;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Unity.Display;
using System.Collections.Generic;

namespace ChristmasMod.Moabs
{
    public class IceSpeedster : ModBloon
    {
        public override string BaseBloon => BloonType.sDdt;

        public override string Icon => Name + "-Icon";

        public override IEnumerable<string> DamageStates => [];

        public override void ModifyBaseBloonModel(BloonModel bloonModel)
        {
            bloonModel.isCamo = false;

            bloonModel.RemoveAllChildren();
            bloonModel.AddToChildren<IceBloon>(3);

            bloonModel.speed *= 1.2f;
            bloonModel.maxHealth *= 2;
            bloonModel.maxHealth += 20;

            bloonModel.bloonProperties = Il2Cpp.BloonProperties.White | Il2Cpp.BloonProperties.Frozen;

            StunTowersInRadiusActionModel stunTowersInRadiusActionModel = new("StunTowersInRadiusActionModel", "freeze", 50, 1, 1, CreatePrefabReference<IceCubeOverlay>(), true);

            HealthPercentTriggerModel healthPercentTriggerModel = new("HealthPercentTriggerModel", false, new([2 / 3f, 1 / 3f, 0]), new(["freeze"]), false);

            bloonModel.AddBehavior(healthPercentTriggerModel);
            bloonModel.AddBehavior(stunTowersInRadiusActionModel);
        }
    }

    public class IceSpeedsterDisplay : ModBloonDisplay<IceSpeedster>
    {
        public override string BaseDisplay => GetBloonDisplay(BloonType.sDdt);

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            foreach(var renderer in node.GetMeshRenderers())
            {
                renderer.SetMainTexture(GetTexture("IceSpeedster"));
            }
        }
    }
}
