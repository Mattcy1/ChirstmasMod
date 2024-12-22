using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Components;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Unity.UI_New.Popups;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ChristmasMod.UI
{
    [RegisterTypeInIl2Cpp(false)]
    public class SnowstormIndicator : MonoBehaviour
    {
        public static SnowstormIndicator instance;


        bool prevStatus = false;

        public void Close()
        {
            if(gameObject)
            {
                gameObject.Destroy();
            }
        }

        public static void Create(bool active)
        {
            if(instance != null)
            {
                instance.Close();
            }

            var rect = InGame.instance.uiRect;

            var mainPanel = rect.gameObject.AddModHelperPanel(new("SnowstormIndicator", 1530, 700, 250), VanillaSprites.SnowstormUpgradeIcon);

            instance = mainPanel.AddComponent<SnowstormIndicator>();

            string tex;
            string msg;

            if (active)
            {
                tex = ModContent.GetTextureGUID<ChristmasMod>("SnowstormTrue");
                msg = "Snowstorm active";
            }
            else
            {
                tex = ModContent.GetTextureGUID<ChristmasMod>("SnowstormFalse");
                msg = "Snowstorm inactive";
            }

            mainPanel.AddButton(new("Status", 0, -100, 100), tex, new Action(() => { PopupScreen.instance?.SafelyQueue(screen => screen.ShowOkPopup(msg)); }));

            instance.prevStatus = active;
        }

        void Update()
        {
            if (Values.Snowstorm == prevStatus)
            {
                return;
            }

            Create(Values.Snowstorm);
        }
    }
}
