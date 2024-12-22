using BTD_Mod_Helper.Api.Bloons;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Bloons;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Unity.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmasMod.Bloons
{
    public class IceBloon : ModBloon
    {
        public override string BaseBloon => BloonType.sPink;

        public override string Icon => base.Icon + "-Icon";

        public override void ModifyBaseBloonModel(BloonModel bloonModel)
        {
            bloonModel.speed /= 4;
            bloonModel.maxHealth = 40;
            bloonModel.danger += 2;
            bloonModel.bloonProperties = Il2Cpp.BloonProperties.White | Il2Cpp.BloonProperties.Frozen;

            var popEffect = bloonModel.GetBehavior<PopEffectModel>();
            popEffect.soundEffectGroupID = "None";
            popEffect.soundEffect1Id = new("");
            popEffect.soundEffect2Id = new("");
            popEffect.soundEffect3Id = new("");
            popEffect.soundEffect4Id = new("");

            bloonModel.RemoveAllChildren();
            bloonModel.AddToChildren<SnowBloon>();
        }
    }

    public class IceBloonDisplay : ModBloonDisplay<IceBloon>
    {
        public override string BaseDisplay => GetBloonDisplay("Red");

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "IceBloon");
        }
    }

    public class IceBloonDamage1 : ModBloonDisplay<IceBloon>
    {
        public override string BaseDisplay => GetBloonDisplay("Red");

        public override int Damage => 1;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, Name);
        }
    }

    public class IceBloonDamage2 : ModBloonDisplay<IceBloon>
    {
        public override string BaseDisplay => GetBloonDisplay("Red");

        public override int Damage => 2;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, Name);
        }
    }

    public class IceBloonDamage3 : ModBloonDisplay<IceBloon>
    {
        public override string BaseDisplay => GetBloonDisplay("Red");

        public override int Damage => 3;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, Name);
        }
    }

    public class IceBloonDamage4 : ModBloonDisplay<IceBloon>
    {
        public override string BaseDisplay => GetBloonDisplay("Red");

        public override int Damage => 4;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, Name);
        }
    }
}
