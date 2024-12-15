using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Unity.Display;

namespace TemplateMod.Towers.PresentLauncher
{
    public class PresentLauncher : ModTower<ChristmasTowers>
    {

        public static bool AddedToShop = false;

        public override string BaseTower => TowerType.BombShooter;

        public override int Cost => 0;

        public override string Portrait => Icon;

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            var proj = towerModel.GetWeapon().projectile;
            proj.ApplyDisplay<Present>();
        }
    }

    public class Present : ModCustomDisplay
    {
        public override string AssetBundleName => "christmas2024";

        public override string PrefabName => "Present";

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            foreach (var renderer in node.GetMeshRenderers())
            {
                renderer.ApplyOutlineShader();
                if (renderer.name == "Present")
                {
                    renderer.SetOutlineColor(new(0f, 0.7f, 0f));
                }
                else if (renderer.name == "Bow")
                {
                    renderer.SetOutlineColor(new(0.7f, 0.5f, 0f));
                }
            }
        }
    }

    public class PresentLauncherDisplay : ModTowerCustomDisplay<PresentLauncher>
    {
        public override string AssetBundleName => "christmas2024";

        public override string PrefabName => "PresentLauncher2024";

        public override bool UseForTower(params int[] tiers) => true;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            foreach (var renderer in node.GetMeshRenderers())
            {
                renderer.ApplyOutlineShader();
                if (renderer.name == "Barrel_Green")
                {
                    renderer.SetOutlineColor(new(0f, 0.7f, 0f));
                }
                else if (renderer.name == "Barrel_Yellow")
                {
                    renderer.SetOutlineColor(new(0.7f, 0.7f, 0f));
                }
                else if (renderer.name == "Barrel_Red")
                {
                    renderer.SetOutlineColor(new(0.7f, 0f, 0f));
                }
                else if (renderer.name == "Wheels")
                {
                    renderer.SetOutlineColor(new(0, 0, 0.05f));
                }
                else if (renderer.name == "Axis")
                {
                    renderer.SetOutlineColor(new(0.1f, 0.1f, 0.1f));
                }
            }
        }
    }
}
