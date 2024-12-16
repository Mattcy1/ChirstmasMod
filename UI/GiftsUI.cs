using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using ChristmasMod;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Unity.UI_New.Popups;
using MelonLoader;
using UnityEngine;

namespace TemplateMod.UI;

public class Gift
{
    [RegisterTypeInIl2Cpp(false)]
    public class GiftUI : MonoBehaviour
    {
        public static GiftUI instance = null;


        public void Close()
        {
            if (gameObject)
            {
                gameObject.Destroy();
            }
        }

        public static void CreatePanel(int Cash, double Lives, bool storyGift = false)
        {
            if (InGame.instance != null)
            {
                RectTransform rect = InGame.instance.uiRect;
                var panel = rect.gameObject.AddModHelperPanel(new("Panel_", 0, 0, 0, 0), VanillaSprites.BrownPanel);
                instance = panel.AddComponent<GiftUI>();
                var image = panel.AddImage(new("Image_", 0, 0, 1000), VanillaSprites.GiftRed);
                var Claim = image.AddButton(new("Button_", 0, -500, 450, 450 / 2), VanillaSprites.GreenBtnLong, new System.Action(() =>
                {
                    if (storyGift)
                    {
                        InGame.instance.SetRound(101); //Start the cutscene
                    }
                    
                    Values.gift += 1;
                    InGame.instance.AddCash(Cash);
                    InGame.instance.AddHealth(Lives);
                    instance.Close();
                    PopupScreen.instance?.ShowOkPopup($"You opened the gift! Inside, you found {Cash} Cashs and {Lives} Lives to help you on your journey. Keep going Christmas depends on you!");
                }));
                Claim.AddText(new("Title_", 0, 0, 300, 150), "CLAIM GIFT!", 70);
            }
        }
    }
}
