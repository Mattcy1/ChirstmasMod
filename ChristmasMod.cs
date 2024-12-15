global using SantaEmotion = TemplateMod.UI.SantaStory.SantaEmotion;
global using SantaMessage = TemplateMod.UI.SantaStory.SantaMessage;

using BTD_Mod_Helper;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using ChristmasMod;
using HarmonyLib;
using Il2CppAssets.Scripts;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Map;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Simulation;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Simulation.Objects;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Bridge;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Unity.UI_New.InGame.AbilitiesMenu;
using Il2CppAssets.Scripts.Unity.UI_New.InGame.RightMenu;
using Il2CppAssets.Scripts.Unity.UI_New.InGame.StoreMenu;
using Il2CppAssets.Scripts.Unity.UI_New.Popups;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppNinjaKiwi.Common.ResourceUtils;
using MelonLoader;
using MelonLoader.Utils;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using TemplateMod.Moabs;
using TemplateMod.Towers;
using TemplateMod.Towers.Elf.R20;
using TemplateMod.Towers.Elf.R60;
using TemplateMod.Towers.Elf.R80;
using TemplateMod.Towers.NonGameModeSanta;
using TemplateMod.Towers.PresentLauncher;
using TemplateMod.UI;
using UnityEngine;

[assembly: MelonInfo(typeof(ChristmasMod.ChristmasMod), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace ChristmasMod;

[HarmonyPatch(typeof(ShopMenu), nameof(ShopMenu.CreateTowerButton))]
static class ShopMenu_CreateTowerButton
{
    [HarmonyPostfix]
    public static void Postfix(ITowerPurchaseButton __result)
    {
        string[] ids = [ModContent.TowerID<PresentLauncher>(), ModContent.TowerID<RegularSanta>(), ModContent.TowerID<ElfLord>()];

        if (InGame.instance.GetGameModel().gameMode == ModContent.GetInstance<Gamemode.ChristmasGamemode>().Id)
        {
            if (ids.Contains(__result.TowerModel.baseId))
            {
                __result.GameObject.transform.parent.gameObject.SetActive(false);

                if (__result.TowerModel.baseId == ModContent.TowerID<PresentLauncher>() && !PresentLauncher.AddedToShop)
                {
                    ChristmasMod.PresentLauncherButton = __result.GameObject.transform.parent.gameObject;
                }
                else if (__result.TowerModel.baseId == ModContent.TowerID<ElfLord>() && !ElfLord.AddedToShop)
                {
                    ElfLord.ShopButton = __result.GameObject.transform.parent.gameObject;
                }
            }
        }
        //else if (__result.TowerModel.baseId == ModContent.TowerID<Santa>())
        //{
        //    __result.GameObject.transform.parent.gameObject.SetActive(false);
        //}
    }
}

public class Values
{

    private static bool snowstorm = false;

    public static bool Snowstorm
    {
        get { return snowstorm; }
        set { snowstorm = value; }
    }
    
    private static bool Trivia1 = false;

    public static bool trivia1
    {
        get { return Trivia1; }
        set { Trivia1 = value; }
    }

    private static int snowstormRound = 0;

    public static int SnowstormRound
    {
        get { return snowstormRound; }
        set { snowstormRound = value; }
    }
    
    private static int defeated = 0;

    public static int DefeatedCounter
    {
        get { return defeated; }
        set { defeated = value; }
    }

    private static bool BossDead = false;

    public static bool bossDead
    {
        get { return BossDead; }
        set { BossDead = value; }
    }
    
    private static int Snowflake = 0;

    public static int snowflake
    {
        get { return Snowflake; }
        set { Snowflake = value; }
    }

    private static int Gift = 0;

    public static int gift
    {
        get { return Gift; }
        set { Gift = value; }
    }

    public static PrefabReference SnowstormPrefab;
}

public class ChristmasMod : BloonsTD6Mod
{
    private static readonly System.Random random = new System.Random();

    internal static GameObject PresentLauncherButton = null;

    public override void OnRestart()
    {
        PresentLauncher.AddedToShop = false;
        ElfLord.AddedToShop = false;
    }

    private class SaveData(string mapName = "Tutorial")
    {
        public bool PresentLauncher = false;
        public bool ElfLord = false;

        public string Map = mapName;
    }

    public override void OnMatchEnd()
    {
        PresentLauncher.AddedToShop = false;
        ElfLord.AddedToShop = false;
    }

    public override void OnApplicationStart()
    {
        ModHelper.Msg<ChristmasMod>("ChirstmasMod loaded!");
    }

    public override void OnTowerCreated(Tower tower, Entity target, Model modelToUse)
    {
        if (Values.trivia1 == true)
        {
            if (tower.towerModel.baseId == "Mermonkey")
            {
                Gift.GiftUI.CreatePanel(2000, 50, false);

                Values.trivia1 = false;
                
                if (SantaStory.SantaStoryUI.instance != null)
                {
                    SantaStory.SantaStoryUI.instance.Close();
                }
                
                var text = "The correct answer was Mermonkey! Enjoy your gift!";
                SantaStory.SantaStoryUI.CreatePanel(SantaEmotion.SantaHappy, text);
            }
        }
        
        if (Values.Snowstorm == true)
        {
            var towerModel = tower.rootModel.Cast<TowerModel>().Duplicate();

            towerModel.range -= 10;

            foreach (var attack in towerModel.GetAttackModels())
            {
                attack.range -= 10;
            }
            tower.UpdateRootModel(towerModel);
        }
    }

    public override void OnTowerDestroyed(Tower tower)
    {
        if (tower.towerModel.baseId == ModContent.TowerID<Santa>())
        {
            bool towerPlaced = false;
            Il2CppSystem.Action<bool> something = (Il2CppSystem.Action<bool>)delegate (bool s)
            {
                towerPlaced = s;
            };
            Il2CppSystem.Action<bool> spawn = something;

            InGame.instance.bridge.CreateTowerAt(new Vector2(0, 0), ModContent.GetTowerModel<Santa>(), ObjectId.Create(9999, 0), false, something, true, true, false, 0);
        }
    }

    //public override void OnUpdate()
    //{
    //    if (Values.Snowstorm == true)
    //    {
    //        Values.SnowstormPrefab = Game.instance.model.GetTowerFromId("IceMonkey-040").GetDescendant<ActivateAttackModel>().GetDescendant<CreateEffectOnAbilityModel>().effectModel.assetId;
    //    
    //        InGame.instance.bridge.Simulation.SpawnEffect(Values.SnowstormPrefab, new Vector3(0, 140, 0), 0, 1, 0.1f);
    //    }
    //}

    public override void OnTowerUpgraded(Tower tower, string upgradeName, TowerModel newBaseTowerModel)
    {
        if (Values.Snowstorm == true)
        {
            var towerModel = tower.rootModel.Cast<TowerModel>().Duplicate();

            towerModel.range -= 10;

            foreach (var attack in towerModel.GetAttackModels())
            {
                attack.range -= 10;
            }
            tower.UpdateRootModel(towerModel);
        }
    }

    public override void OnNewGameModel(GameModel result, MapModel map)
    {
        OpenerUI.CreatePanel();
    }

    public override void OnRoundEnd()
    {
        if (random.Next(10) == 0 && Values.Snowstorm == false)
        {
            PopupScreen.instance?.ShowOkPopup("Snowstorm started");
            Values.Snowstorm = true;
            Values.SnowstormRound = 3;

            MelonLogger.Msg("Snowstorm Starting" + Values.Snowstorm + Values.SnowstormRound);

            foreach (var tower in InGame.instance.GetTowers())
            {
                var towerModel = tower.rootModel.Cast<TowerModel>().Duplicate();

                towerModel.range -= 10;

                foreach (var attack in towerModel.GetAttackModels())
                {
                    attack.range -= 10;
                }
                tower.UpdateRootModel(towerModel);
            }

        }

        if (Values.Snowstorm == true && Values.SnowstormRound > 0)
        {
            Values.SnowstormRound -= 1;
            MelonLogger.Msg("Removing 1 from snowstorm round" + Values.SnowstormRound);
        }

        if (Values.Snowstorm == true && Values.SnowstormRound == 0)
        {
            Values.Snowstorm = false;

            MelonLogger.Msg("Snowstorm finished" + Values.Snowstorm);

            foreach (var tower in InGame.instance.GetTowers())
            {
                var towerModel = tower.rootModel.Cast<TowerModel>().Duplicate();

                towerModel.range += 10;

                foreach (var attack in towerModel.GetAttackModels())
                {
                    attack.range += 10;
                }
                tower.UpdateRootModel(towerModel);
            }
        }
    }
}

public class Santa : ModTower<ChristmasTowers>
{
    public override string Portrait => "Santa";
    public override string Icon => "Santa";
    public override string BaseTower => TowerType.DartMonkey;
    public override int Cost => 0;

    public override int TopPathUpgrades => 0;
    public override int MiddlePathUpgrades => 0;
    public override int BottomPathUpgrades => 0;
    public override string Description => "Santa has come to help us save christmas! after the Grinch stole all the gifts.";

    public override int ShopTowerCount => 1;

    public override string DisplayName => "Santa";

    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        towerModel.range += 30;
        towerModel.GetAttackModel().range += 30;
        towerModel.GetWeapon().projectile.GetDamageModel().damage += 1;
        towerModel.dontDisplayUpgrades = true;
        towerModel.canAlwaysBeSold = false;
        towerModel.blockSelling = true;
    }
}

[HarmonyPatch(typeof(Bloon), nameof(Bloon.OnSpawn))]
static class SnowstormPatch
{
    [HarmonyPostfix]
    public static void Postfix(Bloon __instance)
    {
        if (Values.Snowstorm == true)
        {
            BuffBloonSpeedModel buff = Game.instance.model.GetBloon("Vortex1").GetBehavior<BuffBloonSpeedModel>();
            buff.speedBoost = 1.5f;
            var mutator = buff.Mutator;
            __instance.AddMutator(mutator, 99999);
        }
    }
}

[HarmonyPatch(typeof(Bloon), nameof(Bloon.Damage))]
static class SnowflakePatch
{
    private static readonly System.Random random = new System.Random();

    [HarmonyPostfix]
    public static void Postfix(Bloon __instance)
    {
        if (random.Next(75) == 0)
        {
            Values.snowflake++;
        }
    }
}

[HarmonyPatch(typeof(Ability), nameof(Ability.Activate))]
static class GiftAbility
{
    private static readonly System.Random random = new System.Random();

    [HarmonyPostfix]
    public static void Postfix(Ability __instance)
    {
        foreach (var bloon in InGame.instance.GetBloons())
        {
            if (__instance.abilityModel.displayName == "SantaAbility")
            {
                if (bloon.bloonModel.isBoss)
                {
                    //Do nothing
                }
                else if (!bloon.bloonModel.isBoss)
                {
                    InGame.instance.bridge.simulation.SpawnEffect(ModContent.CreatePrefabReference<GiftEffect>(), bloon.Position, 0, 2);
                    bloon.Damage(50, null, true, true,false);
                    MelonLogger.Msg("Casted");
                }
            }
        }
    }
}

public class GiftEffect : ModDisplay
{
    public override string BaseDisplay => "6d84b13b7622d2744b8e8369565bc058";

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        RendererExt.SetMainTexture(((Il2CppArrayBase<Renderer>)node.genericRenderers)[0], ModContent.GetTexture<ChristmasMod>("blank"));
        RendererExt.SetMainTexture(((Il2CppArrayBase<Renderer>)node.genericRenderers)[3], ModContent.GetTexture<ChristmasMod>("GiftsParticle"));
        RendererExt.SetMainTexture(((Il2CppArrayBase<Renderer>)node.genericRenderers)[2], ModContent.GetTexture<ChristmasMod>("GiftsParticle"));
        RendererExt.SetMainTexture(((Il2CppArrayBase<Renderer>)node.genericRenderers)[1], ModContent.GetTexture<ChristmasMod>("blank"));
        ((Component)((Il2CppArrayBase<Renderer>)node.genericRenderers)[2]).GetComponent<ParticleSystem>().startSpeed *= 0.2f;
    }
}

[HarmonyPatch(typeof(Simulation), nameof(Simulation.RoundStart))]
static class RoundPatch
{
    [HarmonyPostfix]
    public static void Postfix(Simulation __instance)
    {
        if (__instance.GetCurrentRound() == 2)
        {
            SantaMessage[] messages = [
                new("Help <b>Santa</b> defeat 5 different bosses sent by the <b>Grinch</b> to save Christmas!\nAfter the <b>Grinch</b> stole all the presents, you are the only one who can save Christmas! Each boss you face gets stronger and stronger, but so do you with every victory.\n\nDefeating all 5 bosses and collecting the 5 gifts will reward you with the ultimate prize: 10,000 Monkey Money.\n\nAre you ready for the challenge? The fate of Christmas is in your hands!", SantaEmotion.SantaHappy),
                new("Hello soldier! We must stop the <b>Grinch</b> from ruining Christmas! If Christmas gets ruined who knows what people might start to think of me...", SantaEmotion.SantaWorry)
            ];

            SantaStory.SantaStoryUI.CreatePanel(messages, new(() => {
                bool towerPlaced = false;
                Il2CppSystem.Action<bool> something = (Il2CppSystem.Action<bool>)delegate (bool s)
                {
                    towerPlaced = s;
                };
                Il2CppSystem.Action<bool> spawn = something;

                InGame.instance.bridge.CreateTowerAt(new Vector2(0, 0), ModContent.GetTowerModel<Santa>(), ObjectId.Create(9999, 0), false, something, true, true, false, 0);
            }));
        }

        if (__instance.GetCurrentRound() == 9)
        {
            //SantaStory.SantaStoryUI.CreatePanel([new SantaMessage("I don't really pay the elves all to much, I'm sure that won't be a problem. W-what is that??!", SantaEmotion.SantaWorry), new("Oh, hi guys. I was told to come here and run through the defenses for some reason, I think it was something to do with me being weaker than the others. Wait... was I supposed to say that?", SantaEmotion.SnowMoab, new(() => InGame.instance.SpawnBloons(ModContent.BloonID<SnowMoab.WeakSnowMoab>(), 1, 0)))]);
        }

        if (__instance.GetCurrentRound() == 18)
        {
            var text = "The Poppermint is approaching next round! Prepare yourself for the fight. Here’s $1000 to help your defenses spend it wisely!";

            SantaStory.SantaStoryUI.CreatePanel(SantaEmotion.SantaWorry, text, new(() =>
            InGame.instance.AddCash(1000)));
        }

        if (__instance.GetCurrentRound() == 20)
        {
            SantaMessage[] messages = [new("You truly are the hero Christmas needs. Keep pushing forward only you can save Christmas! I can feel it... I’ve grown stronger, and so have you!", SantaEmotion.SantaSalute), new("I have added the present launcher to the shop! This tower uses snowflakes so be sure to not sell all of them.", SantaEmotion.SantaHappy)];

            SantaStory.SantaStoryUI.CreatePanel(messages, new(() => {

                if (ChristmasMod.PresentLauncherButton != null)
                {
                    ChristmasMod.PresentLauncherButton.SetActive(true);
                }

                Gift.GiftUI.CreatePanel(1000, 10);

                foreach (TowerToSimulation tower in InGame.instance.bridge.GetAllTowers().ToList())
                {
                    if (tower.tower.towerModel.baseId == ModContent.TowerID<Santa>())
                    {
                        var tm = tower.tower.rootModel.Cast<TowerModel>().Duplicate();

                        tm.GetWeapon().rate = 0.5f;
                        tm.range += 5;
                        tm.GetAttackModel().range += 5;

                        tower.tower.UpdateRootModel(tm);
                    }
                }
            }));
        }

        if (__instance.GetCurrentRound() == 29)
        {
            var text = "Can you guess the tower? Which tower becomes less effective when placed in specific map locations, despite not having any direct penalties or debuffs listed?";
            Values.trivia1 = true;
            SantaStory.SantaStoryUI.CreatePanel(SantaEmotion.SantaHappy, text);
        }

        if (__instance.GetCurrentRound() == 38)
        {
            var text = "Watch out! I’ve heard the next boss is incredibly tough. Not only is he immune to ice attacks, but he also creates a devastating snowstorm while he’s on the battlefield!";

            SantaStory.SantaStoryUI.CreatePanel(SantaEmotion.SantaWorry, text);
        }

        if (__instance.GetCurrentRound() == 39)
        {
            Values.Snowstorm = true;
            Values.SnowstormRound = 1;
            PopupScreen.instance?.ShowOkPopup("Snowstorm started");
        }

        if (__instance.GetCurrentRound() == 40)
        {
            var text = "Good job, Soldier! The boss has been defeated, and now there are only 3 more to go! You're making great progress! And by the way, my workers have arrived to help you out things are looking even better now. Keep going, you're almost there!";

            SantaStory.SantaStoryUI.CreatePanel(SantaEmotion.SantaSalute, text, new(() => {
                foreach (TowerToSimulation tower in InGame.instance.bridge.GetAllTowers().ToList())
                {
                    if (tower.tower.towerModel.baseId == ModContent.TowerID<Santa>())
                    {
                        var tm = tower.tower.rootModel.Cast<TowerModel>().Duplicate();

                        AttackModel[] Avatarspawner = { Game.instance.model.GetTowerFromId("EngineerMonkey-200").GetAttackModels().First(a => a.name == "AttackModel_Spawner_").Duplicate() };
                        Avatarspawner[0].weapons[0].rate = 5f;
                        Avatarspawner[0].weapons[0].projectile.RemoveBehavior<CreateTowerModel>();
                        Avatarspawner[0].name = "ElfSpawner";
                        Avatarspawner[0].weapons[0].projectile.AddBehavior(new CreateTowerModel("CreateTower", ModContent.GetTowerModel<Elf>(), 0, false, false, false, false, false));
                        tm.AddBehavior(Avatarspawner[0]);

                        tower.tower.UpdateRootModel(tm);
                    }
                }
            }));

            Gift.GiftUI.CreatePanel(5000, 100);
        }
        
        if (__instance.GetCurrentRound() == 58)
        {
            var text = "I have some bad news... This next boss is SUPER STRONG! It has 5 lives, and with each life, its HP doubles. On top of that, it gets faster every second! This will be your toughest fight yet, soldier.\n\nHere’s 7500 cash to help.";

            SantaStory.SantaStoryUI.CreatePanel(SantaEmotion.SantaWorry, text, new(() =>
            InGame.instance.AddCash(7500)));
        }
        
        if (__instance.GetCurrentRound() == 60)
        {
            var text = "Wow, that boss was insanely tough!  But we’ve come out stronger than ever. I’ve unlocked a new ability check it out!\n\nGet ready, soldier. The next battle is right around the corner, and we’ll need everything we’ve got to win. Let’s keep pushing forward!";

            SantaStory.SantaStoryUI.CreatePanel(SantaEmotion.SantaSalute, text, new(() => {
                foreach (TowerToSimulation tower in InGame.instance.bridge.GetAllTowers().ToList())
                {
                    if (tower.tower.towerModel.baseId == ModContent.TowerID<Santa>())
                    {
                        var tm = tower.tower.rootModel.Cast<TowerModel>().Duplicate();

                        var ability = Game.instance.model.GetTowerFromId("DartlingGunner-040").GetAbility().Duplicate();
                        ability.RemoveBehavior<ActivateAttackModel>();
                        ability.cooldown = 60;
                        ability.Cooldown = 60;
                        ability.SetName("SantaAbility");
                        ability.displayName = "SantaAbility";
                        ability.name = "SantaAbility";
                        tm.AddBehavior(ability);
                        AttackModel[] Avatarspawner = { Game.instance.model.GetTowerFromId("EngineerMonkey-200").GetAttackModels().First(a => a.name == "AttackModel_Spawner_").Duplicate() };
                        Avatarspawner[0].weapons[0].rate = 10f;
                        Avatarspawner[0].weapons[0].projectile.RemoveBehavior<CreateTowerModel>();
                        Avatarspawner[0].name = "ElfSpawner";
                        Avatarspawner[0].weapons[0].projectile.AddBehavior(new CreateTowerModel("CreateTower", ModContent.GetTowerModel<StronkElf>(), 0, false, false, false, false, false));
                        tm.AddBehavior(Avatarspawner[0]);

                        tower.tower.UpdateRootModel(tm);
                    }
                }
                AbilityMenu.instance.AbilitiesChanged();
            }));
            
            Gift.GiftUI.CreatePanel(10000, 50);
        }
        if (__instance.GetCurrentRound() == 80)
        {
            SantaMessage[] messages =
            [
                new SantaMessage("Man that boss was really tough! I'm not sure what we're going to do for this last boss! If only there was something else or <b>someone</b> else we could have to help us...", SantaEmotion.SantaWorry),
                    new SantaMessage("Aha! I know just who to call. He's one of my strongest elves so he'll hopefully help us.", SantaEmotion.SantaHappy),
                    new SantaMessage("You want my help to stop the <b>Grinch</b> from ruining christmas? Uh... fine I'll help, but you need to pay me <color='#00ff00'>money</color>, if you don't then no help from me! Afterall, <i>you already barely pay me for what I do at the North Pole</i>...", SantaEmotion.ElfLordWant),
                    new SantaMessage("Seriously?? Fine, but you have to promise to not abandon us and actually help us.", SantaEmotion.SantaDisapointed),
                    new SantaMessage("OK I'll help you now. After all I suppose a lot of work would be for waste and I'd be out of a job if the <b>Grinch</b> ruined christmas.", SantaEmotion.ElfLordThumbsUp),
                    new SantaMessage("Soilder, this is great news! Don't expect me to pay the <b>Elf Lord</b> though, I have already payed out too much money today. Ho ho ho... However the <b>Elf Lord</b> shouldn't leave until we win!", SantaEmotion.SantaHappy)
            ];

            SantaStory.SantaStoryUI.CreatePanel(messages, new(() => {
                if (ElfLord.ShopButton != null)
                {
                    ElfLord.AddedToShop = true;
                    ElfLord.ShopButton.SetActive(true);
                }
            }));
        }
    }
}
