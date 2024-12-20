global using StoryPortrait = TemplateMod.UI.Story.StoryPortrait;
global using StoryMessage = TemplateMod.UI.Story.StoryMessage;
using System;
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
using System.Threading.Tasks;
using BTD_Mod_Helper.Api.ModOptions;
using Il2CppAssets.Scripts.Models.Effects;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppNinjaKiwi.LiNK.Lobbies;
using TemplateMod.Moabs;
using TemplateMod.Towers;
using TemplateMod.Towers.Elf.R20;
using TemplateMod.Towers.Elf.R60;
using TemplateMod.Towers.Elf.R80;
using TemplateMod.Towers.PresentLauncher;
using TemplateMod.UI;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;
using Input = UnityEngine.Windows.Input;
using Vector3 = Il2CppAssets.Scripts.Simulation.SMath.Vector3;
using TemplateMod.Bloons;

[assembly: MelonInfo(typeof(ChristmasMod.ChristmasMod), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace ChristmasMod;

[HarmonyPatch(typeof(ShopMenu), nameof(ShopMenu.CreateTowerButton))]
static class ShopMenu_CreateTowerButton
{
    [HarmonyPostfix]
    public static void Postfix(ITowerPurchaseButton __result)
    {
        string[] ids = [ModContent.TowerID<PresentLauncher>(), ModContent.TowerID<ElfLord>(), ModContent.TowerID<PresentTower>()];

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
                else if (__result.TowerModel.baseId == ModContent.TowerID<PresentTower>())
                {
                    PresentTower.ShopButton = __result.GameObject.transform.parent.gameObject;
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

    
    private static int Cookiedefeated = 0;

    public static int DefeatedCounterCookie
    {
        get { return Cookiedefeated; }
        set { Cookiedefeated = value; }
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
    
    private static bool StoryExecuted = false;

    public static bool storyExecuted
    {
        get { return StoryExecuted; }
        set { StoryExecuted = value; }
    }
    
    private static bool Tsunami = false;

    public static bool tsunami
    {
        get { return Tsunami; }
        set { Tsunami = value; }
    }
    
    private static bool Disableprojectile = false;
    public static bool disableprojectile
    {
        get { return Disableprojectile; }
        set { Disableprojectile = value; }
    }
    
    private static bool CookieAngry = false;
    public static bool cookieAngry
    {
        get { return CookieAngry; }
        set { CookieAngry = value; }
    }
    
    private static bool IconAdded = false;
    public static bool iconAdded
    {
        get { return IconAdded; }
        set { IconAdded = value; }
    }
    
    private static bool grinchAngry = false;
    public static bool GrinchAngry
    {
        get { return grinchAngry; }
        set { grinchAngry = value; }
    }

    public static PrefabReference SnowstormPrefab;
}

public class ChristmasMod : BloonsTD6Mod
{
    private static readonly System.Random random = new System.Random();

    internal static GameObject PresentLauncherButton = null;
    
    private VideoPlayer player;

    public override void OnRestart()
    {
        PresentLauncher.AddedToShop = false;
        ElfLord.AddedToShop = false;
        Values.Snowstorm = false;
        Values.DefeatedCounter = 0;
        Values.disableprojectile = false;
        Values.trivia1 = false;
        Values.gift = 0;
        Values.bossDead = false;
        Values.tsunami = false; 
        Values.snowflake = 0;
        Values.DefeatedCounterCookie = 0;
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
                Gift.GiftUI.CreatePanel(2000, 50);

                Values.trivia1 = false;
                
                if (Story.StoryUI.instance != null)
                {
                    Story.StoryUI.instance.Close();
                }
                
                var text = "The correct answer was Mermonkey! Enjoy your gift!";
                Story.StoryUI.CreatePanel(StoryPortrait.SantaHappy, text);
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
        //StartCutscene.StartCutsceneUI.CreatePanel();
        
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
        Values.snowflake += 100;
    }
    public override void OnUpdate()
    {
        if (Values.Snowstorm == true)
        {
            InGame.instance.bridge.Simulation.SpawnEffect(ModContent.CreatePrefabReference<SnowstormEffect>(), new Vector3(0, 0, 0), 0, 1.1f, isFullscreen: (Fullscreen)1);
        }
    }
    public override void OnRoundEnd()
    {
        if (random.Next(10) == 0 && Values.Snowstorm == false)
        {
            PopupScreen.instance?.ShowOkPopup("Snowstorm started");
            Values.Snowstorm = true;
            Values.SnowstormRound = 3;

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
            
            MelonLogger.Msg(Values.SnowstormRound);
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
    internal static GameObject ShopButton;
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
        towerModel.ApplyDisplay<SantaDisplay>();
        towerModel.range += 30;
        towerModel.GetAttackModel().range += 30;
        towerModel.GetWeapon().projectile.GetDamageModel().damage += 1;
        towerModel.dontDisplayUpgrades = true;
        towerModel.canAlwaysBeSold = false;
        towerModel.blockSelling = true;
        towerModel.GetWeapon().projectile.ApplyDisplay<Present>();
    }
}

public class SantaDisplay : ModDisplay
{
    public override string BaseDisplay => Generic2dDisplay;

    public override float Scale => 0.5f;

    public override Il2CppAssets.Scripts.Simulation.SMath.Vector3 PositionOffset => new(0, 0, 100);
    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        Set2DTexture(node,"SantaDisplay");
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
            buff.speedBoost = 1.3f;
            var mutator = buff.Mutator;
            __instance.AddMutator(mutator, 99999);
        }

        if (Values.storyExecuted == true)
        {
            Values.storyExecuted = false;
            
            if (Story.StoryUI.instance != null)
            {
                Story.StoryUI.instance.Close();
            }
                
            StoryMessage[] messages = [
                new("This is really bad—all the bosses are here! But my power is stronger than ever. Take $1,000,000 to help you defend!", StoryPortrait.SantaWorry),
                new("I'm sorry, Santa, but this battle is over! Your little player can't save you!", StoryPortrait.GrinchAngryIcon)
            ];

            Story.StoryUI.CreatePanel(messages);
            InGame.instance.AddCash(1000000);
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
                if (bloon.bloonModel.isBoss) { }
                else if (!bloon.bloonModel.isBoss)
                {
                    InGame.instance.bridge.simulation.SpawnEffect(ModContent.CreatePrefabReference<GiftEffect>(), bloon.Position, 0, 2);
                    bloon.Damage(50, null, true, true,false, tower: null);
                }
            }
            
            if (__instance.abilityModel.displayName == "SantaAbilityT2")
            {
                InGame.instance.bridge.simulation.SpawnEffect(ModContent.CreatePrefabReference<GiftEffect>(), bloon.Position, 0, 2);
                bloon.Damage(10000, null, true, true,false);
            }

            if(__instance.abilityModel.name == "giftsForAll")
            {
                InGame.instance.bridge.simulation.SpawnEffect(ModContent.CreatePrefabReference<GiftEffect>(), bloon.Position, 0, 2);
                InGame.instance.bridge.simulation.SpawnEffect(ModContent.CreatePrefabReference<ExplodeEffect>(), bloon.Position, 0, 2);
                bloon.Damage(5000, null, true, true, false);
            }
        }
    }
}

public class ExplodeEffect : ModDisplay
{
    public override string BaseDisplay => "6d84b13b7622d2744b8e8369565bc058";
    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        string explosionGuid = Game.instance.model.GetTowerFromId("BombShooter-500").GetDescendant<CreateEffectOnContactModel>().effectModel.assetId.AssetGUID;

        RendererExt.SetMainTexture(((Il2CppArrayBase<Renderer>)node.genericRenderers)[0], ModContent.GetTexture<ChristmasMod>("blank"));
        RendererExt.SetMainTexture(((Il2CppArrayBase<Renderer>)node.genericRenderers)[3], GetTexture<ChristmasMod>("Explode"));
        RendererExt.SetMainTexture(((Il2CppArrayBase<Renderer>)node.genericRenderers)[2], GetTexture<ChristmasMod>("Explode"));
        RendererExt.SetMainTexture(((Il2CppArrayBase<Renderer>)node.genericRenderers)[1], ModContent.GetTexture<ChristmasMod>("blank"));
        ((Component)((Il2CppArrayBase<Renderer>)node.genericRenderers)[2]).GetComponent<ParticleSystem>().startSpeed *= 0.1f;
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
public class SnowstormEffect : ModDisplay
{
    public override string BaseDisplay => "06928d3ec6e91854d99859c4f1dac91d";

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
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
            StoryMessage[] messages = [
                new("Help <b>Santa</b> defeat 5 different bosses sent by the <b>Grinch</b> to save Christmas!\nAfter the <b>Grinch</b> stole all the presents, you are the only one who can save Christmas! Each boss you face gets stronger and stronger, but so do you with every victory.\n\nDefeating all 5 bosses and collecting the 5 gifts will reward you with the ultimate prize: 10,000 Monkey Money.\n\nAre you ready for the challenge? The fate of Christmas is in your hands!", StoryPortrait.SantaHappy),
                new("Hello soldier! We must stop the <b>Grinch</b> from ruining Christmas! If Christmas gets ruined who knows what people might start to think of me...", StoryPortrait.SantaWorry),
                new("Do I have a choice?", StoryPortrait.Player),
                new("No.", StoryPortrait.SantaHappy),
            ];

            Story.StoryUI.CreatePanel(messages, new(() => {
                bool towerPlaced = false;
                Il2CppSystem.Action<bool> something = (Il2CppSystem.Action<bool>)delegate (bool s)
                {
                    towerPlaced = s;
                };
                Il2CppSystem.Action<bool> spawn = something;

                InGame.instance.bridge.CreateTowerAt(new Vector2(0, 0), ModContent.GetTowerModel<Santa>(), ObjectId.Create(9999, 0), false, something, true, true, false, 0);
                
                //Santa.ShopButton.SetActive(false);
            }));
        }

        if (__instance.GetCurrentRound() == 9)
        {
            Story.StoryUI.CreatePanel([new StoryMessage("I don't really pay the elves all to much, I'm sure that won't be a problem. W-what is that??!", StoryPortrait.SantaWorry), new("Oh, hi guys. I was told to come here and run through the defenses for some reason, I think it was something to do with me being weaker than the others. Wait... was I supposed to say that?", StoryPortrait.SnowMoab, new(() => InGame.instance.SpawnBloons(ModContent.BloonID<SnowMoab.WeakSnowMoab>(), 1, 0)))]);
        }

        if(__instance.GetCurrentRound() == 14)
        {
            Story.StoryUI.CreatePanel([new StoryMessage("Hehehehe! Have a gift! It's not bad at all!", StoryPortrait.PresentBloonIcon), new("A present? For me? Oh golly goo! I haven't been gifted a present in years!", StoryPortrait.PlayerNoWay, new(() => InGame.instance.SpawnBloons(ModContent.BloonID<PresentBloon>(), 1, 0))), new("Yeah, that's right, just pop me open. Nothing bad will happen...", StoryPortrait.PresentBloonTroll)]);
        }

        if (__instance.GetCurrentRound() == 18)
        {
            StoryMessage[] messages = [
                new("The Poppermint is approaching next round! Prepare yourself for the fight. Here’s $1000 to help your defenses spend it wisely!", StoryPortrait.SantaWorry),
                new("Thanks, santa!", StoryPortrait.Player),
            ];

            Story.StoryUI.CreatePanel(messages, new(() =>
            InGame.instance.AddCash(1000)));
        }

        if (__instance.GetCurrentRound() == 20)
        {
            StoryMessage[] messages = [new("You truly are the hero Christmas needs. Keep pushing forward only you can save Christmas! I can feel it... I’ve grown stronger, and so have you!", StoryPortrait.SantaSalute), new("I have added the present launcher to the shop! This tower uses snowflakes so be sure to not sell all of them.", StoryPortrait.SantaHappy), new ("Alright, sounds good, Santa!", StoryPortrait.Player)];

            Story.StoryUI.CreatePanel(messages, new(() => {

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
        
        if (__instance.GetCurrentRound() == 24)
        {
            StoryMessage[] messages = [
                new("Oh... hey... Uhm, we were all sent by the Grinch to destroy YOU.", StoryPortrait.GroupOfBloon),
                new("GO AWAY BLOONS!", StoryPortrait.SantaWorry),
                new("Oh, Christmas, what did I do?", StoryPortrait.Player),
            ];
            
            Story.StoryUI.CreatePanel(messages);
        }

        if (__instance.GetCurrentRound() == 29)
        {
            StoryMessage[] messages = [
                new("Can you guess the tower? Which tower becomes less effective when placed in specific map locations, despite not having any direct penalties or debuffs listed", StoryPortrait.SantaWorry),
                new("Sure! (Place down the tower)", StoryPortrait.Player),
            ];
            Values.trivia1 = true;
            Story.StoryUI.CreatePanel(messages);
        }

        if (__instance.GetCurrentRound() == 38)
        {
            StoryMessage[] messages = [
                new("Watch out! I’ve heard the next boss is incredibly tough. Not only is he immune to ice attacks, but he also creates a devastating snowstorm while he’s on the battlefield!", StoryPortrait.SantaWorry),
                new("I'm worried now!", StoryPortrait.Player),
            ];

            Story.StoryUI.CreatePanel(messages);
        }

        if (__instance.GetCurrentRound() == 39)
        {
            Values.Snowstorm = true;
            Values.SnowstormRound = 1;
            PopupScreen.instance?.ShowOkPopup("Snowstorm started");
            
            Story.StoryUI.CreatePanel(StoryPortrait.Player, "This cant be good!");
        }
        
        if (__instance.GetCurrentRound() == 34)
        {
            StoryMessage[] messages =
            [
                new("Did you know that Christmas was officially established in the 4th century?", StoryPortrait.SantaHappy),
                new("Did you know that the tradition of Christmas trees originated in Germany? The custom of decorating evergreen trees began in the 16th century when devout Christians in Germany brought trees into their homes and adorned them with candles. This tradition later spread across Europe and became popular worldwide", StoryPortrait.SantaHappy),
            ];

            Story.StoryUI.CreatePanel(messages);
        }

        if (__instance.GetCurrentRound() == 40)
        {
            StoryMessage[] messages = [
                new("Good job, Soldier! The boss has been defeated, and now there are only 3 more to go! You're making great progress! And by the way, my workers have arrived to help you out things are looking even better now. Keep going, you're almost there!", StoryPortrait.SantaSalute),
                new("2 Down 3 To Go!", StoryPortrait.Player),
            ];

            Story.StoryUI.CreatePanel(messages, new(() => {
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
        
        if (__instance.GetCurrentRound() == 54)
        {
            StoryMessage[] messages = [
                new("I've sent a massive gift box! Make sure to pop it for a reward and DO NOT let it leak!", StoryPortrait.SantaHappy),
                new("Wow, I love gifts when I can open them and not pop them!", StoryPortrait.Player)
            ];

            Story.StoryUI.CreatePanel(messages);
        }

        if (__instance.GetCurrentRound() == 55)
        {
            Gift.GiftUI.CreatePanel(5000, 100);
        }
        
        if (__instance.GetCurrentRound() == 58)
        {
            StoryMessage[] messages = [
                new("I have some bad news... This next boss is SUPER STRONG! It has 5 lives, and with each life, its HP doubles. On top of that, it gets faster every second! This will be your toughest fight yet, soldier.\n\nHere’s 7500 cash to help.", StoryPortrait.SantaWorry),
                new("That is, in fact, bad news.", StoryPortrait.Player)
            ];  
            Story.StoryUI.CreatePanel(messages, new(() =>
            InGame.instance.AddCash(7500)));
        }
        
        if (__instance.GetCurrentRound() == 60)
        {
            var text = "Wow, that boss was insanely tough!  But we’ve come out stronger than ever. I’ve unlocked a new ability check it out!\n\nGet ready, soldier. The next battle is right around the corner, and we’ll need everything we’ve got to win. Let’s keep pushing forward!";

            Story.StoryUI.CreatePanel(StoryPortrait.SantaSalute, text, new(() => {
                foreach (TowerToSimulation tower in InGame.instance.bridge.GetAllTowers().ToList())
                {
                    if (tower.tower.towerModel.baseId == ModContent.TowerID<Santa>())
                    {
                        var tm = tower.tower.rootModel.Cast<TowerModel>().Duplicate();


                        tm.GetWeapon().projectile.GetDamageModel().damage = 5;
                        tm.GetWeapon().rate = 0.6f;
                        var ability = Game.instance.model.GetTowerFromId("DartlingGunner-040").GetAbility().Duplicate();
                        ability.RemoveBehavior<ActivateAttackModel>();
                        ability.cooldown = 60;
                        ability.Cooldown = 60;
                        ability.SetName("SantaAbility");
                        ability.displayName = "SantaAbility";
                        ability.name = "SantaAbility";
                        ability.icon = ModContent.GetSpriteReference<ChristmasMod>("GiftsParticle");
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
        
        if (__instance.GetCurrentRound() == 78)
        {
            StoryMessage[] messages =
            [
                new("I'm kind of worried about this next boss. So far, they were all pretty easy, but I was told this one is way stronger than the others.", StoryPortrait.SantaWorry),
                new("As you should be! I am the Grinch's strongest worker Employee of the Month for three years straight.", StoryPortrait.AngryCookieMonsterIcon),
                new("That is not reassuring. take 50k$ to help defend", StoryPortrait.SantaWorry),
                new("Where do you get all this money?", StoryPortrait.Player),
                new("It's Christmas magic.", StoryPortrait.SantaHappy)
            ];

            Story.StoryUI.CreatePanel(messages, new(() => {
                InGame.instance.AddCash(50000);
            }));
        }
        
        if (__instance.GetCurrentRound() == 80)
        {
            Story.StoryUI.instance.Close();
            
            StoryMessage[] messages =
            [
                new StoryMessage("I guess I'm not strong enough, But I've got one last trick up my sleeve, Cookie Monster proceeded to steal half your money... and then died.", StoryPortrait.AngryCookieMonsterIcon),
                new StoryMessage("Ez", StoryPortrait.Player),
                new StoryMessage("Rude!", StoryPortrait.AngryCookieMonsterIcon),
                new StoryMessage("Krill issue", StoryPortrait.Player),
                new StoryMessage("Man that boss was really tough! I'm not sure what we're going to do for this last boss! If only there was something else or rather <b>someone</b> else who could help us...", StoryPortrait.SantaWorry),
                new StoryMessage("Aha! I know just who to call. He's one of my strongest elves so he'll hopefully help us.", StoryPortrait.SantaHappy),
                new StoryMessage("You want my help to stop the <b>Grinch</b> from ruining christmas? Uh... fine I'll help, but you need to pay me lots of <color=#00ff00ff>money</color>, if you don't then no help from me! Afterall, <i><b>you already barely pay me for what I do at the North Pole</b></i>...", StoryPortrait.ElfLordWant),
                new StoryMessage("Seriously?? Fine, but you have to promise to not abandon us and actually help us.", StoryPortrait.SantaDisapointed),
                new StoryMessage("OK I'll help you now. After all I suppose a lot of work would be for waste and I'd be out of a job if the <b>Grinch</b> ruined christmas.", StoryPortrait.ElfLordThumbsUp),
                new StoryMessage("Soldier, this is great news! Don't expect me to pay the <b>Elf Lord</b> though, I have already payed out too much money today, ho ho ho. However the <b>Elf Lord</b> shouldn't leave until we win! Unlike those other elves.", StoryPortrait.SantaHappy)
            ];

            Story.StoryUI.CreatePanel(messages, new(() => {
                if (ElfLord.ShopButton != null)
                {
                    ElfLord.AddedToShop = true;
                    ElfLord.ShopButton.SetActive(true);
                }
                
                Gift.GiftUI.CreatePanel(100000, 50);
                
                foreach (TowerToSimulation tower in InGame.instance.bridge.GetAllTowers().ToList())
                {
                    //BUFFS
                    
                    if (tower.tower.towerModel.baseId == ModContent.TowerID<Santa>())
                    {
                        var tm = tower.tower.rootModel.Cast<TowerModel>().Duplicate();


                        tm.GetWeapon().projectile.GetDamageModel().damage = 50;
                        tm.GetWeapon().rate = 0.3f;
                        var ability = tm.GetAbility();
                        ability.SetName("SantaAbilityT2");
                        ability.displayName = "SantaAbilityT2";
                        ability.name = "SantaAbilityT2";

                        tm.GetAttackModel(1).weapons[0].rate /= 2f;
                        tm.GetAttackModel(2).weapons[0].rate /= 2f;

                        tower.tower.UpdateRootModel(tm);
                    }
                }
                AbilityMenu.instance.AbilitiesChanged();
            }));
        }

        if (__instance.GetCurrentRound() == 84)
        {
            StoryMessage[] messages =
            [
                new StoryMessage("I'm really, really scared about the Grinch. He alone has more HP than all the bosses combined.", StoryPortrait.SantaWorry),
                new StoryMessage("But not to worry, I just stole 100k from you to upgrade my damage and range!", StoryPortrait.SantaHappy, new(() => InGame.instance.AddCash(-100000))),
                new StoryMessage("That's kinda rude, Santa.", StoryPortrait.Player),
                new StoryMessage("Didn't I already give you enough for all the Christmases before?", StoryPortrait.SantaHappy),
                new StoryMessage("Really...", StoryPortrait.Player),
            ];
            
            Story.StoryUI.CreatePanel(messages, new(() =>
            {
                foreach (TowerToSimulation tower in InGame.instance.bridge.GetAllTowers().ToList())
                {
                    //BUFFS
                    
                    if (tower.tower.towerModel.baseId == ModContent.TowerID<Santa>())
                    {
                        var tm = tower.tower.rootModel.Cast<TowerModel>().Duplicate();


                        tm.GetWeapon().projectile.GetDamageModel().damage = 80;
                        tm.range += 20;
                        tm.GetAttackModel().range = tm.range;

                        tower.tower.UpdateRootModel(tm);
                    }
                }
            }));
        }

        if (__instance.GetCurrentRound() == 85)
        {

            StoryMessage[] messages =
            [
                new StoryMessage("Okay, I feel bad about taking your monkey...", StoryPortrait.SantaWorry),
                new StoryMessage("I'll give it back to you.", StoryPortrait.SantaDisapointed, new(() => InGame.instance.AddCash(100000))),
                new StoryMessage("<i>Phew</i> I thought you weren't going to pay me back...", StoryPortrait.Player),
                new StoryMessage("Oooh! Gimme some! (x200)", StoryPortrait.GroupOfPresentBloons, new(() => {
                    InGame.instance.SpawnBloons(ModContent.BloonID<PresentBloon>(), 200, 1);
                })),
                new StoryMessage("Uhh... I believe it's in your best intrest to kill those 200 present bloons!", StoryPortrait.SantaWorry)
            ];
        }

        if (__instance.GetCurrentRound() == 98)
        {
            //StartCutscene.StartCutsceneUI.CreatePanel();
        }

        if (__instance.GetCurrentRound() == 100)
        {
            StoryMessage[] messages = [
                new("At long last, I was defeated... sigh. Don’t close the game yet! There’s a secret cutscene waiting for you once all the bosses are defeated.", StoryPortrait.GrinchAngryIcon),
                new("Ha! You didn't win this time Grinch!", StoryPortrait.SantaHappy),
                new("I guess I never stood a chance again you all anyway...", StoryPortrait.GrinchAngryIcon),
                new("Guys I obviously carried the game, y'know? Anyway thanks for the money, you gave me a lot! Can't wait to spend it!", StoryPortrait.ElfLordWant),
                new("It was all of us, Elf Lord don't even get ahead of yourself.", StoryPortrait.Player),
                new("Throughout North, and South, I alone am the Jolly one.", StoryPortrait.SantaGojo),
                new("Oh you haven't won yet Go- Santa! There's always next year!", StoryPortrait.GrinchAngryIcon),
                new("Christmas Mod 2025 Confirmed?????", StoryPortrait.PlayerNoWay, new(() => Gift.GiftUI.CreatePanel(1000000, 50, true)))
            ];

            Story.StoryUI.CreatePanel(messages);
        }
    }
}
