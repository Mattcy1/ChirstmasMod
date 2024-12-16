using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using MelonLoader;
using System;
using System.Linq;
using UnityEngine;

namespace TemplateMod.UI;

public class Story
{
    public struct StoryMessage(string message, StoryPortrait portrait, Action onMessage = null)
    {
        public string Message = message;
        public StoryPortrait Portrait = portrait;
        public Action OnMessage = onMessage;
    }

    public enum StoryPortrait
    {
        SantaHappy,
        SantaWorry,
        SantaSalute,
        SantaDisapointed,
        SantaSad,
        ElfLordThumbsUp,
        ElfLordWant,
        SnowMoab,
        CrumblyIcon,
        CookieMonsterIcon,
        GrinchAngryIcon,
    }


    [RegisterTypeInIl2Cpp(false)]
    public class StoryUI : MonoBehaviour
    {
        public static StoryUI instance = null;
        public void Close()
        {
            if (gameObject)
            {
                gameObject.Destroy();
            }
        }

        public static Action LastCloseAction;

        public static void CreatePanel(StoryPortrait portrait, string text, Action closeAction = null, bool runLastCloseAction = true)
        {
            CreatePanel(new StoryMessage(text, portrait), closeAction, runLastCloseAction);
        }

        public static void CreatePanel(StoryMessage msg, Action closeAction = null, bool runLastCloseAction = true)
        {
            if (InGame.instance != null)
            {
                if(instance != null)
                {
                    instance.Close();
                    if(runLastCloseAction)
                        LastCloseAction?.Invoke();
                }
                LastCloseAction = closeAction;

                msg.OnMessage?.Invoke();

                RectTransform rect = InGame.instance.uiRect;
                var panel = rect.gameObject.AddModHelperPanel(new("Panel_", 0, -1000, 1250, 600), VanillaSprites.BrownPanel);
                instance = panel.AddComponent<StoryUI>();
                var image = panel.AddImage(new("Image_", 1000, 0, 750, 750), ModContent.GetTextureGUID<ChristmasMod.ChristmasMod>(msg.Portrait.ToString()));
                var text_ = panel.AddText(new("Title_", 0, 0, 1150, 500), $"{msg.Message}");
                text_.Text.enableAutoSizing = text_;

                var btn = panel.AddButton(new("CloseBtn", 625, 300, 100), VanillaSprites.CloseBtn, new Action(() => { instance.Close(); closeAction?.Invoke(); }));
            }
        }

        public static void CreatePanel(StoryPortrait[] portraits, string[] texts, Action closeAction = null, bool runLastCloseAction = true)
        {
            StoryMessage[] msgs = new StoryMessage[texts.Length];

            for (int i = 0; i < texts.Length; i++)
            {
                StoryPortrait? emotionToUse = portraits[i];
                emotionToUse ??= StoryPortrait.SantaHappy;

                msgs[i] = new(texts[i], (StoryPortrait)emotionToUse);
            }

            if (msgs.Length == 1)
            {
                CreatePanel(msgs[0], closeAction, runLastCloseAction);
            }
            else
            {
                CreatePanel(msgs, closeAction, runLastCloseAction);
            }
        }

        public static void CreatePanel(StoryMessage[] msgs, Action closeAction = null, bool runLastCloseAction = true)
        {
            if (instance != null)
            {
                instance.Close();
                if (runLastCloseAction)
                    LastCloseAction?.Invoke();
            }

            LastCloseAction = closeAction;

            RectTransform rect = InGame.instance.uiRect;
            var panel = rect.gameObject.AddModHelperPanel(new("Panel_", 0, -1000, 1250, 600), VanillaSprites.BrownPanel);
            instance = panel.AddComponent<StoryUI>();
            var image = panel.AddImage(new("Image_", 1000, 0, 750, 750), ModContent.GetTextureGUID<ChristmasMod.ChristmasMod>(msgs[0].Portrait.ToString()));
            var text_ = panel.AddText(new("Title_", 0, 0, 1150, 500), $"{msgs[0].Message}");
            text_.Text.enableAutoSizing = text_;

            msgs[0].OnMessage?.Invoke();

            var newMsgs = msgs.Skip(1).ToArray();

            if (newMsgs.Length == 1)
            {
                var btn = panel.AddButton(new("NextBtn", 625, 300, 100), VanillaSprites.ContinueBtn, new Action(() => { CreatePanel(newMsgs[0], closeAction, false); }));
            }
            else
            {
                var btn = panel.AddButton(new("NextBtn", 625, 300, 100), VanillaSprites.ContinueBtn, new Action(() => { CreatePanel(newMsgs, closeAction, false); }));
            }
        }
    }
}
