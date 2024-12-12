using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using MelonLoader;
using UnityEngine;

namespace ChirstmasMod;

public class SantaStory
{
    [RegisterTypeInIl2Cpp(false)]
    public class SantaStoryUI : MonoBehaviour
    {
        public static SantaStoryUI? instance = null;


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
                var panel = rect.gameObject.AddModHelperPanel(new("Panel_", 0, -1000, 1250, 600), VanillaSprites.BrownPanel);
                instance = panel.AddComponent<SantaStoryUI>();
                var image = panel.AddImage(new("Image_", 800, 0, 325, 750), ModContent.GetSpriteReference<ChirstmasMod>("Santa").GetGUID());
                panel.AddText(new("Title_", 0, 0, 1150, 500), $"Help Santa defeat 5 different bosses sent by the Grinch to save Christmas!\nAfter the Grinch stole all the presents, you are the only one who can save Christmas! Each boss you face gets stronger and stronger, but so do you with every victory.\n\nDefeating all 5 bosses and collecting the 5 gifts will reward you with the ultimate prize: 10,000 Monkey Money.\n\nAre you ready for the challenge? The fate of Christmas is in your hands!", 37);
            }
        }
    }
}