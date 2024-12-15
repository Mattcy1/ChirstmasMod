using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using MelonLoader;
using System;
using System.Linq;
using UnityEngine;

namespace TemplateMod.UI;

public class SantaStory
{
    public struct SantaMessage(string message, SantaEmotion emotion, Action onMessage = null)
    {
        public string Message = message;
        public SantaEmotion Emotion = emotion;
        public Action OnMessage = onMessage;
    }

    public enum SantaEmotion
    {
        SantaHappy,
        SantaWorry,
        SantaSalute,
        SantaDisapointed,
        ElfLordThumbsUp,
        ElfLordWant,
        SnowMoab
    }


    [RegisterTypeInIl2Cpp(false)]
    public class SantaStoryUI : MonoBehaviour
    {
        public static SantaStoryUI instance = null;
        public void Close()
        {
            if (gameObject)
            {
                gameObject.Destroy();
            }
        }

        public static Action LastCloseAction;

        public static void CreatePanel(SantaEmotion emotion, string text, Action closeAction = null, bool runLastCloseAction = true)
        {
            CreatePanel(new SantaMessage(text, emotion), closeAction, runLastCloseAction);
        }

        public static void CreatePanel(SantaMessage msg, Action closeAction = null, bool runLastCloseAction = true)
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
                instance = panel.AddComponent<SantaStoryUI>();
                var image = panel.AddImage(new("Image_", 1000, 0, 750, 750), ModContent.GetTextureGUID<ChristmasMod.ChristmasMod>(msg.Emotion.ToString()));
                var text_ = panel.AddText(new("Title_", 0, 0, 1150, 500), $"{msg.Message}");
                text_.Text.enableAutoSizing = text_;

                var btn = panel.AddButton(new("CloseBtn", 625, 300, 100), VanillaSprites.CloseBtn, new Action(() => { instance.Close(); closeAction?.Invoke(); }));
            }
        }

        public static void CreatePanel(SantaEmotion[] emotions, string[] texts, Action closeAction = null, bool runLastCloseAction = true)
        {
            SantaMessage[] msgs = new SantaMessage[texts.Length];

            for (int i = 0; i < texts.Length; i++)
            {
                SantaEmotion? emotionToUse = emotions[i];
                emotionToUse ??= SantaEmotion.SantaHappy;

                msgs[i] = new(texts[i], (SantaEmotion)emotionToUse);
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

        public static void CreatePanel(SantaMessage[] msgs, Action closeAction = null, bool runLastCloseAction = true)
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
            instance = panel.AddComponent<SantaStoryUI>();
            var image = panel.AddImage(new("Image_", 1000, 0, 750, 750), ModContent.GetTextureGUID<ChristmasMod.ChristmasMod>(msgs[0].Emotion.ToString()));
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
