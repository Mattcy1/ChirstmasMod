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
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using MelonLoader;

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
    
    [HarmonyPatch(typeof(Bloon), nameof(Bloon.OnDestroy))]
    static class Bloon_OnDamage
    {
        private static int Count = 1;
        public static void Postfix(Bloon __instance)
        {
            var bm = __instance.bloonModel;
            if(bm.baseId == ModContent.BloonID<PresentBloon>())
            {
                var bloons = Game.instance.model.bloons.ToList().FindAll(bloon => !bloon.isMoab && !bloon.isBoss);
                Random rand = new();
                
                var bloon = bloons[rand.Next(bloons.Count)];
                var countRand = rand.Next(1, 6);
                Count = countRand;


                if (!bloon.baseId.Contains("Rock") && !bloon.baseId.Contains("TestBloon") && !bloon.baseId.Contains(ModContent.BloonID<PresentBloon>()))
                {
                    InGame.instance.SpawnBloons(bloon.id, Count, 10);
                    MelonLogger.Msg($"Added {Count} bloon to {bloon.id}");
                }
            }
        }
    }
}
