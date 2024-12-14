using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using MelonLoader;
using UnityEngine;
using UnityEngine.UI;

namespace TemplateMod.UI
{
    [RegisterTypeInIl2Cpp(false)]
    public class OpenerUI : MonoBehaviour
    {
        public static OpenerUI _instance;
        public bool uiOpen;
        public void Close()
        {
            if (gameObject)
            {
                gameObject.Destroy();
            }
        }

        public static void CreatePanel()
        {
            if (InGame.instance == null) return;

            RectTransform rect = InGame.instance.uiRect;
            var panel = rect.gameObject.AddModHelperPanel(new("Panel_", 0, 0, 0), VanillaSprites.BrownInsertPanel);
            _instance = panel.AddComponent<OpenerUI>();
            var image = panel.AddImage(new("OpenerImage_", 1530, 1000, 250), VanillaSprites.NinjaMonkeySnowflakesIcon);
            var button = image.gameObject.AddComponent<Button>();
            button.onClick.AddListener(_instance.OpenUI);
        }

        private void OpenUI()
        {
            if (uiOpen == false)
            {
                Snowflake.SnowflakeUI.CreatePanel();
                uiOpen = true;
            }
            else if (uiOpen == true)
            {
                Snowflake.SnowflakeUI.instance.Close();
                uiOpen = false;
            }
        }
    }
}