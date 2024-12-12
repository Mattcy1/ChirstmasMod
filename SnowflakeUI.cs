using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using MelonLoader;
using UnityEngine;

namespace ChirstmasMod;

public class Snowflake
{
    [RegisterTypeInIl2Cpp(false)]
    public class SnowflakeUI : MonoBehaviour
    {
        public static SnowflakeUI? instance = null;


        public void Close()
        {
            if (gameObject)
            {
                gameObject.Destroy();
            }
        }

        public static void CreatePanel()
        {
            if (InGame.instance != null)
            {
                RectTransform rect = InGame.instance.uiRect;
                var panel = rect.gameObject.AddModHelperPanel(new("Panel_", 0, 1000, 1250, 600), VanillaSprites.BrownPanel);
                instance = panel.AddComponent<SnowflakeUI>();
                var image = panel.AddImage(new("Image_", -450, 150, 275), VanillaSprites.NinjaMonkeySnowflakesIcon);
            }
        }
    }
}