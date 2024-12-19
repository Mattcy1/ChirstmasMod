using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Bloons;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Api.Scenarios;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Difficulty;
using Il2CppAssets.Scripts.Models.Gameplay.Mods;
using Il2CppAssets.Scripts.Models.Rounds;
using Il2CppAssets.Scripts.Simulation;
using TemplateMod.Bloons;
using TemplateMod.Moabs;
using TemplateMod.Towers;

namespace ChristmasMod;

public class Gamemode
{
    internal class CustomRS : ModRoundSet
    {

        public override string BaseRoundSet => RoundSetType.Default;
        public override int DefinedRounds => 100;
        public override string DisplayName => "Christmas Gamemode";

        public override void ModifyEasyRoundModels(RoundModel roundModel, int round)
        {
            switch (round)
            {
                case 4:
                    roundModel.AddBloonGroup(ModContent.BloonID<CandyCaneBloon>(), 3, 0, 10);
                    break;
                case 6:
                    roundModel.AddBloonGroup(ModContent.BloonID<SnowBloon>(), 3, 0, 10);
                    break;
                case 10:
                    roundModel.AddBloonGroup(ModContent.BloonID<CandyCaneBloon>(), 3, 0, 10);
                    roundModel.AddBloonGroup(ModContent.BloonID<SnowBloon>(), 3, 0, 25);
                    break;
                case 13:
                    roundModel.AddBloonGroup(ModContent.BloonID<SnowMoab.WeakSnowMoab>(), 1, 0, 0);
                    roundModel.AddBloonGroup(ModContent.BloonID<IceBloon>(), 2, 0, 10);
                    roundModel.AddBloonGroup(ModContent.BloonID<SnowBloon>(), 5, 0, 40);
                    break;
                case 19:
                    roundModel.AddBloonGroup(ModContent.BloonID<SnowMoab.WeakSnowMoab>(), 5, 0, 25);
                    roundModel.AddBloonGroup(ModContent.BloonID<CandyCaneBloon>(), 2, 0, 20);
                    roundModel.AddBloonGroup(ModContent.BloonID<SnowBloon>(), 5, 0, 30);
                    break;
                case 24:
                    roundModel.AddBloonGroup(ModContent.BloonID<SnowMoab.WeakSnowMoab>(), 3, 0, 10);
                    roundModel.AddBloonGroup(ModContent.BloonID<IceBloon>(), 2, 0, 10);
                    roundModel.AddBloonGroup(ModContent.BloonID<CandyCaneBloon>(), 5, 0, 40);
                    roundModel.AddBloonGroup(ModContent.BloonID<SnowBloon>(), 5, 0, 30);
                    break;
                case 29:
                    roundModel.AddBloonGroup(ModContent.BloonID<GingerbreadBloon>(), 2, 0, 10);
                    break; 
                
                case 34:
                    roundModel.AddBloonGroup(ModContent.BloonID<GingerbreadMoab>(), 1, 0, 10);
                    break; 
                
                case 38:
                    roundModel.AddBloonGroup(ModContent.BloonID<SnowMoab>(), 2, 0, 10);
                    roundModel.AddBloonGroup<PresentBloon>(3, 0, 50);
                    break;
                case 42:
                    roundModel.AddBloonGroup<PresentMoab>(1, 100, 1000);
                    break;
                case 43:
                    roundModel.AddBloonGroup<SnowMoab>(4, 500, 900);
                    roundModel.AddBloonGroup<PresentBloon>(10, 0, 1000);
                    break;
                case 45:
                    roundModel.AddBloonGroup<IceMoab>(1, 100, 101);
                    roundModel.AddBloonGroup<IceBloon>(10, 0, 100);
                    roundModel.AddBloonGroup<SnowMoab>(10, 100, 200);
                    roundModel.AddBloonGroup<PresentBloon>(20, 0, 1000);
                    roundModel.AddBloonGroup<PresentMoab>(2, 0, 1000);
                    break;
                case 54:
                    roundModel.AddBloonGroup<SnowBfb>();
                    roundModel.AddBloonGroup<PresentBloon>(30, 0, 1000);
                    roundModel.AddBloonGroup<PresentMoab>(3, 0, 1000);
                    break;
                case 60:
                    roundModel.AddBloonGroup<GingerbreadBfb>();
                    roundModel.AddBloonGroup<SnowBfb>(1, 100, 100);
                    roundModel.AddBloonGroup<IceMoab>(15, 0, 3000);
                    roundModel.AddBloonGroup<PresentMoab>(10, 0, 1000);
                    break;
            }
        }
        
       public override void ModifyMediumRoundModels(RoundModel roundModel, int round)
        {
            switch (round)
            {
                //Lerp you make mediums stuffs
            }
        }
    }
    
    public class ChristmasGamemode : ModGameMode
    {

        public override string Difficulty => DifficultyType.Hard;
        public override string BaseGameMode => GameModeType.Hard;
        public override string Icon => "Icon";

        public override string DisplayName => "Christmas Gamemode";

        public override void ModifyBaseGameModeModel(ModModel gameModeModel)
        {
            gameModeModel.UseRoundSet<CustomRS>();
            gameModeModel.SetEndingRound(102);
        }
    } 
}