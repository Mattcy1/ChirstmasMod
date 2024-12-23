﻿using BTD_Mod_Helper.Api.Bloons;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Extensions;
using HarmonyLib;
using Il2CppAssets.Scripts.Models.Bloons;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using MelonLoader;
using UnityEngine;
namespace ChristmasMod.Bloons
{
    public class SnowBloon : ModBloon
    {
        public override string BaseBloon => BloonType.sRed;

        public override string Icon => Name + "-Icon";

        public override void ModifyBaseBloonModel(BloonModel bloonModel)
        {
            bloonModel.speed *= 1.5f;
            bloonModel.maxHealth = 5;

            bloonModel.AddToChildren(BloonType.sWhite, 1);

            bloonModel.bloonProperties = Il2Cpp.BloonProperties.White;
        }


        public class SnowBloonDisplay : ModBloonDisplay<SnowBloon>
        {
            public override string BaseDisplay => GetBloonDisplay("Red");

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                Set2DTexture(node, "SnowBloon");
            }
        }

        [HarmonyPatch(typeof(Bloon), nameof(Bloon.Damage))]
        static class Bloon_Damage
        {
            public static System.Random rand = new();
            
            [HarmonyPostfix]
            public static void Prefix(Bloon __instance, ref float totalAmount, Projectile projectile, Tower tower)
            {
                if (tower != null)
                {
                    var towerModel = tower.towerModel;

                    if ((towerModel.baseId == "BombShooter" || towerModel.baseId == "MortarMonkey") && __instance.bloonModel.name == BloonID<SnowBloon>())
                    {
                        __instance.Destroy();
                    }
                    
                    if (__instance.bloonModel.baseId == BloonID<SnowBloon>())
                    {
                        GetAudioClip<ChristmasMod>("SnowBloon_" + rand.Next(4)).Play();
                        MelonLogger.Msg("Playing SnowBloon_");
                    }
                    
                    if (__instance.health <= 0)
                    {
                        if (__instance.bloonModel.baseId == BloonID<IceBloon>())
                        {
                            GetAudioClip<ChristmasMod>("IceShatter" + rand.Next(4)).Play();
                            
                            MelonLogger.Msg("Playing IceShatter_");
                        }
                    }
                }
            }
        }
    }
}
