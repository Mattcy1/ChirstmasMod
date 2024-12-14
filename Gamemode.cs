using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Bloons;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Api.Scenarios;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Difficulty;
using Il2CppAssets.Scripts.Models.Gameplay.Mods;
using Il2CppAssets.Scripts.Models.Rounds;

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
                case 0:
                    roundModel.AddBloonGroup(BloonType.BlueRegrow, 1, 0, 0);
                    break;
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
            gameModeModel.AddMutator(new LockTowerModModel("LockTowerModModel_", ModContent.TowerID<Santa>()));
        }
    } 
}