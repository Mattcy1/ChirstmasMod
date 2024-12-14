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
    public struct SantaMessage(string message, SantaEmotion emotion)
    {
        public string Message = message;
        public SantaEmotion Emotion = emotion;
    }

    public enum SantaEmotion
    {
        SantaHappy,
        SantaWorry,
        SantaSalute
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

        public static void CreatePanel(SantaEmotion emotion, string text)
        {
            CreatePanel(new SantaMessage(text, emotion));
        }

        public static void CreatePanel(SantaMessage msg)
        {
            if (InGame.instance != null)
            {
                if(instance != null)
                {
                    instance.Close();
                }

                RectTransform rect = InGame.instance.uiRect;
                var panel = rect.gameObject.AddModHelperPanel(new("Panel_", 0, -1000, 1250, 600), VanillaSprites.BrownPanel);
                instance = panel.AddComponent<SantaStoryUI>();
                var image = panel.AddImage(new("Image_", 1000, 0, 750, 750), ModContent.GetTextureGUID<ChristmasMod.ChristmasMod>(msg.Emotion.ToString()));
                var text_ = panel.AddText(new("Title_", 0, 0, 1150, 500), $"{msg.Message}");
                text_.Text.enableAutoSizing = text_;

                var btn = panel.AddButton(new("CloseBtn", 625, 300, 100), VanillaSprites.CloseBtn, new Action(instance.Close));
            }
        }

        public static void CreatePanel(SantaEmotion[] emotions, string[] texts)
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
                CreatePanel(msgs[0]);
            }
            else
            {
                CreatePanel(msgs);
            }
        }

        public static void CreatePanel(SantaMessage[] msgs)
        {
            if (instance != null)
            {
                instance.Close();
            }

            RectTransform rect = InGame.instance.uiRect;
            var panel = rect.gameObject.AddModHelperPanel(new("Panel_", 0, -1000, 1250, 600), VanillaSprites.BrownPanel);
            instance = panel.AddComponent<SantaStoryUI>();
            var image = panel.AddImage(new("Image_", 1000, 0, 750, 750), ModContent.GetTextureGUID<ChristmasMod.ChristmasMod>(msgs[0].Emotion.ToString()));
            var text_ = panel.AddText(new("Title_", 0, 0, 1150, 500), $"{msgs[0].Message}");
            text_.Text.enableAutoSizing = text_;

            var newMsgs = msgs.Skip(1).ToArray();

            if (newMsgs.Length <= 1)
            {
                var btn = panel.AddButton(new("NextBtn", 625, 300, 100), VanillaSprites.ContinueBtn, new Action(() => CreatePanel(newMsgs[0])));
            }
            else
            {
                var btn = panel.AddButton(new("NextBtn", 625, 300, 100), VanillaSprites.ContinueBtn, new Action(() => CreatePanel(newMsgs)));
            }
        }
    }
}
