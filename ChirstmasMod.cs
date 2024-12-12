using MelonLoader;
using BTD_Mod_Helper;
using ChirstmasMod;

[assembly: MelonInfo(typeof(ChirstmasMod.ChirstmasMod), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace ChirstmasMod;

public class ChirstmasMod : BloonsTD6Mod
{
    public override void OnApplicationStart()
    {
        ModHelper.Msg<ChirstmasMod>("ChirstmasMod loaded!");
    }
}