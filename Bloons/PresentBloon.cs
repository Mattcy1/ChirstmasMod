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
using BTD_Mod_Helper.Api.Display;
using Il2CppAssets.Scripts.Unity.Display;

namespace ChristmasMod.Bloons
{
    public class PresentBloon : ModBloon
    {
        public override string BaseBloon => BloonType.sPink;

        public override string Icon => Name + "Icon";

        public override void ModifyBaseBloonModel(BloonModel bloonModel)
        {
            bloonModel.bloonProperties = Il2Cpp.BloonProperties.None;
            bloonModel.maxHealth = 10;
            bloonModel.RemoveAllChildren();
        }
    }

    public class PresentBloonDisplay : ModBloonDisplay<PresentBloon>
    {
        public override string BaseDisplay => GetBloonDisplay("Pink");

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "PresentBloon");
        }
    }

    public class PresentBloonDamage1 : ModBloonDisplay<PresentBloon>
    {
        public override string BaseDisplay => GetBloonDisplay("Pink");

        public override int Damage => 1;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, Name);
        }
    }

    public class PresentBloonDamage2 : ModBloonDisplay<PresentBloon>
    {
        public override string BaseDisplay => GetBloonDisplay("Pink");

        public override int Damage => 2;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, Name);
        }
    }
    public class PresentBloonDamage3 : ModBloonDisplay<PresentBloon>
    {
        public override string BaseDisplay => GetBloonDisplay("Pink");

        public override int Damage => 3;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, Name);
        }
    }

    [HarmonyPatch(typeof(Bloon), nameof(Bloon.OnDestroy))]
    static class Bloon_OnDestroy
    {
        public static void Postfix(Bloon __instance)
        {
            var bm = __instance.bloonModel;
            if(bm.baseId == ModContent.BloonID<PresentBloon>())
            {
                var bloons = Game.instance.model.bloons.ToList().FindAll(bloon => !bloon.isMoab && !bloon.isBoss);
                Random rand = new();
                
                var bloon = bloons[rand.Next(bloons.Count)];
                var countRand = rand.Next(1, 5);


                if (!bloon.baseId.Contains("Rock") && !bloon.baseId.Contains("TestBloon") && !bloon.baseId.Contains("Gold"))
                {
                    InGame.instance.SpawnBloons(bloon.id, countRand, 10);
                }
            }
        }
    }
}
