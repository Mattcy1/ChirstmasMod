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

namespace TemplateMod.Moabs
{
    public class SnowMoab : ModBloon
    {
        public override string BaseBloon => BloonType.sMoab;

        public override void ModifyBaseBloonModel(BloonModel bloonModel)
        {
            bloonModel.RemoveAllChildren();
            bloonModel.AddToChildren<SnowBloon>(5);
            bloonModel.maxHealth = 5;
            bloonModel.isImmuneToSlow = true;
            bloonModel.bloonProperties = Il2Cpp.BloonProperties.Frozen | Il2Cpp.BloonProperties.White;
        }

        public class SnowMoabDisplay : ModBloonDisplay<SnowMoab>
        {
            public override string BaseDisplay => GetBloonDisplay(BloonType.sMoab);

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                node.DumpTextures("SnowMoab");
            }
        }

        public class SnowMoabDamage1Display : ModBloonDisplay<SnowMoab>
        {
            public override string BaseDisplay => GetBloonDisplay(BloonType.sMoab, 1);

            public override int Damage => 1;

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                node.DumpTextures("SnowMoab1");
            }
        }

        public class SnowMoabDamage2Display : ModBloonDisplay<SnowMoab>
        {
            public override string BaseDisplay => GetBloonDisplay(BloonType.sMoab, 2);

            public override int Damage => 2;

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                node.DumpTextures("SnowMoab2");
            }
        }

        public class SnowMoabDamage3Display : ModBloonDisplay<SnowMoab>
        {
            public override string BaseDisplay => GetBloonDisplay(BloonType.sMoab, 3);

            public override int Damage => 3;

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                node.DumpTextures("SnowMoab3");
            }
        }

        public class SnowMoabDamage4Display : ModBloonDisplay<SnowMoab>
        {
            public override string BaseDisplay => GetBloonDisplay(BloonType.sMoab, 4);

            public override int Damage => 4;

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                node.DumpTextures("SnowMoab4");
            }
        }
    }
}
