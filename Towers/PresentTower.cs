using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using ChristmasMod;
using HarmonyLib;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using UnityEngine;

namespace TemplateMod.Towers
{
    public class PresentTower : ModTower<ChristmasTowers>
    {
        public override string BaseTower => TowerType.DartMonkey;

        public static GameObject ShopButton = null;

        public override int Cost => 4999999;

        public override string DisplayName => "Present of Doom";

        public override string Description => "What's contained inside is truely dangerous...";

        public override string Icon => "GiftsForAll";

        public override string Portrait => Icon;

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            towerModel.ApplyDisplay<PresentLauncher.Present>();

            towerModel.AddBehavior(new AbilityModel("present_nuke", "Present Nuke", "Present Nuke: 10 million damage to ALL bloons.", 0, 0, GetSpriteReference(Icon), 0, null, false, false, "", 1, 0, -1, false, false));
        }
    }

    [HarmonyPatch(typeof(Ability), nameof(Ability.Activate))]
    static class Ability_Activate
    {
        [HarmonyPostfix]
        public static void Postfix(Ability __instance)
        {
            var twr = __instance.tower;

            if(twr.towerModel.baseId == ModContent.TowerID<PresentTower>())
            {
                foreach (var bloon in InGame.instance.GetBloons())
                {
                    InGame.instance.bridge.simulation.SpawnEffect(ModContent.CreatePrefabReference<GiftEffect>(), bloon.Position, 0, 2);
                    InGame.instance.bridge.simulation.SpawnEffect(ModContent.CreatePrefabReference<GiftEffect>(), bloon.Position, 0, 2);
                    InGame.instance.bridge.simulation.SpawnEffect(ModContent.CreatePrefabReference<GiftEffect>(), bloon.Position, 0, 2);
                    bloon.Damage(10000000, null, true, true, false, tower: null);
                }

                twr.worth = 0;
                twr.SellTower();
            }
        }
    }
}
