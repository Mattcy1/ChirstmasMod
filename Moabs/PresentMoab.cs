using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Bloons;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Extensions;
using HarmonyLib;
using Il2CppAssets.Scripts.Models.Bloons;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using System;
using System.Collections.Generic;
using System.Linq;
using ChristmasMod.Bloons;
using Il2CppAssets.Scripts.Unity.Display;

namespace ChristmasMod.Moabs
{
    public class PresentMoab : ModBloon
    {
        public override string BaseBloon => BloonType.sMoab;

        public override IEnumerable<string> DamageStates => [];

        public override void ModifyBaseBloonModel(BloonModel bloonModel)
        {
            bloonModel.maxHealth = 50;
            bloonModel.AddToChildren<PresentBloon>(3);
        }
    }

    public class PresentMoabDisplay : ModBloonCustomDisplay<PresentMoab>
    {
        public override string AssetBundleName => "christmas2024";

        public override string PrefabName => "PresentMoabAnimated";

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            foreach(var renderer in node.GetMeshRenderers())
            {
                renderer.ApplyOutlineShader();
                renderer.SetOutlineColor(UnityEngine.Color.green);
            }
        }

    }

    [HarmonyPatch(typeof(Bloon), nameof(Bloon.OnDestroy))]
    static class Bloon_OnDestroy
    {
        public static void Postfix(Bloon __instance)
        {
            var bm = __instance.bloonModel;
            if (bm.baseId == ModContent.BloonID<PresentMoab>())
            {
                var bloons = Game.instance.model.bloons.ToList().FindAll(bloon => bloon.isMoab && !bloon.isBoss);
                Random rand = new();

                var bloon = bloons[rand.Next(bloons.Count)];
                var countRand = rand.Next(1, 4);

                string[] unallowedIds = [];

                int rnd = InGame.instance.bridge.GetCurrentRound();

                if (rnd < 49)
                {
                    unallowedIds = ["Bfb", "Zomg", "Ddt", "Bad"];
                }
                else if (rnd < 59)
                {
                    unallowedIds = ["Zomg", "Ddt", "Bad"];
                }
                else if (rnd < 69)
                {
                    unallowedIds = ["Zomg", "Bad"];
                }
                else if (rnd < 93)
                {
                    unallowedIds = ["Bad"];
                }
                
                string[] BossID = ["Lych", "Phayze", "Bloonarius", "Dreadbloon", "Blastapopoulos", "Vortex", "Test"];

                if (!unallowedIds.Contains(bloon.baseId) && !BossID.Contains(bloon.id))
                {
                    InGame.instance.SpawnBloons(bloon.id, countRand, 10);
                }
            }
        }
    }
}
