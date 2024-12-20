using BTD_Mod_Helper.Api.Bloons;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Bloons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMod.Bloons
{
    public class CandyCaneBloon : ModBloon
    {
        public override string BaseBloon => BloonType.sPink;

        public override void ModifyBaseBloonModel(BloonModel bloonModel)
        {
            bloonModel.speed /= 1.5f;
            bloonModel.RemoveAllChildren();

            bloonModel.maxHealth = 3;

            bloonModel.AddToChildren<SnowBloon>(2);
        }
    }
}
