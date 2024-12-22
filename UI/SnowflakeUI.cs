using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using ChristmasMod;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Unity.UI_New.Popups;
using MelonLoader;
using UnityEngine;

namespace ChristmasMod.UI;

public class Snowflake
{
    [RegisterTypeInIl2Cpp(false)]
    public class SnowflakeUI : MonoBehaviour
    {
        public static SnowflakeUI instance = null;


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
                var image = panel.AddImage(new("Image_", -400, 150, 275), VanillaSprites.NinjaMonkeySnowflakesIcon);
                image.AddText(new("Title_", 0, -200, 400, 100), "Snowflakes:", 60);
                image.AddText(new("Snowflakes", 0, -300, 400, 100), Values.snowflake.ToString(), 80);
                var image1 = panel.AddImage(new("Image_", 450, 150, 275), VanillaSprites.GiftRed);
                image1.AddText(new("Title_", 0, -200, 350, 200), "Gift: " + Values.gift + " / 7", 60);
                var Sell = panel.AddButton(new("Button_", 0, 0, 300, 150), VanillaSprites.GreenBtnLong, new System.Action(() =>
                {
                    InGame.instance.AddCash(25 * Values.snowflake);
                    PopupScreen.instance?.ShowOkPopup($"You sold all your snowflakes for: {25 * Values.snowflake}$");
                    Values.snowflake = 0;
                }));
                Sell.AddText(new("Title_", 0, 0, 300, 150), "Sell snowflakes", 40);
            }
        }
    }
}
