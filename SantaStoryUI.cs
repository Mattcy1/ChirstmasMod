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

        public static void CreateNormalSantaPanel(string text, int size)
        {
            if (InGame.instance != null)
            {
                RectTransform rect = InGame.instance.uiRect;
                var panel = rect.gameObject.AddModHelperPanel(new("Panel_", 0, -1000, 1250, 600), VanillaSprites.BrownPanel);
                instance = panel.AddComponent<SantaStoryUI>();
                var image = panel.AddImage(new("Image_", 1000, 0, 750, 750), ModContent.GetSpriteReference<ChirstmasMod>("Santa").GetGUID());
                panel.AddText(new("Title_", 0, 0, 1150, 500), $"{text}", size);
            }
        }
        
        public static void CreateWorriedSantaPanel(string text, int size)
        {
            if (InGame.instance != null)
            {
                RectTransform rect = InGame.instance.uiRect;
                var panel = rect.gameObject.AddModHelperPanel(new("Panel_", 0, -1000, 1250, 600), VanillaSprites.BrownPanel);
                instance = panel.AddComponent<SantaStoryUI>();
                var image = panel.AddImage(new("Image_", 1000, 0, 750, 750), ModContent.GetSpriteReference<ChirstmasMod>("worrysanta").GetGUID());
                panel.AddText(new("Title_", 0, 0, 1150, 500), $"{text}", size);
            }
        }
        
        public static void CreateSalutingSantaPanel(string text, int size)
        {
            if (InGame.instance != null)
            {
                RectTransform rect = InGame.instance.uiRect;
                var panel = rect.gameObject.AddModHelperPanel(new("Panel_", 0, -1000, 1250, 600), VanillaSprites.BrownPanel);
                instance = panel.AddComponent<SantaStoryUI>();
                var image = panel.AddImage(new("Image_", 1000, 0, 750, 750), ModContent.GetSpriteReference<ChirstmasMod>("santaSalute").GetGUID());
                panel.AddText(new("Title_", 0, 0, 1150, 500), $"{text}", size);
            }
        }
    }
}
