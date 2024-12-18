using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Bloons;
using BTD_Mod_Helper.Extensions;
using HarmonyLib;
using Il2CppAssets.Scripts.Models.Bloons;
using Il2CppAssets.Scripts.Models.Rounds;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Bridge;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMod.Bloons
{
    public class PresentBloon : ModBloon
    {
        public override string BaseBloon => BloonType.sPink;

        public override void ModifyBaseBloonModel(BloonModel bloonModel)
        {
            bloonModel.bloonProperties = Il2Cpp.BloonProperties.None;
            bloonModel.maxHealth = 3;
            bloonModel.RemoveAllChildren();
        }
    }

    [HarmonyPatch(typeof(Bloon), nameof(Bloon.OnSpawn))]
    static class Bloon_OnSpawn
    {
        public static void Postfix(Bloon __instance)
        {
            var bm = __instance.bloonModel;
            if(bm.baseId == ModContent.BloonID<PresentBloon>())
            {
                var bloons = Game.instance.model.bloons.ToList().FindAll(bloon => !bloon.isMoab);
                Random rand = new();

                var bloon = bloons[rand.Next(bloons.Count)];
                var count = rand.Next(60) / 6;

                if(count < 1)
                {
                    count = 1;
                }

                bm.AddToChildren(bloon.id, count);
            }
        }
    }
}
