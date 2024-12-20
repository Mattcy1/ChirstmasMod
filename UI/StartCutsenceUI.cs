using System.Collections.Generic;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using MelonLoader;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using Scene = Il2CppAssets.Scripts.Unity.Display.Scene;

namespace TemplateMod.UI;

public class StartCutscene
{
    [RegisterTypeInIl2Cpp(false)]
    public class StartCutsceneUI : MonoBehaviour
    {
        public static StartCutsceneUI instance = null;


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
                var panel = rect.gameObject.AddModHelperPanel(new("Panel_", 0, 0, 0, 0), VanillaSprites.BrownPanel);
                instance = panel.AddComponent<StartCutsceneUI>();
                var StartCS = panel.AddButton(new("Button_", 0, 0, 450, 450 / 2), VanillaSprites.GreenBtnLong, new System.Action(() =>
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    cube.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                    cube.transform.localScale = new Vector3(1920f, 1080f, 1);

                    var videoPlayer = cube.AddComponent<UnityEngine.Video.VideoPlayer>();

                    RenderTexture renderTexture = new RenderTexture(1920, 1080, 0);
                    renderTexture.Create();

                    videoPlayer.renderMode = VideoRenderMode.RenderTexture;
                    videoPlayer.targetTexture = renderTexture;

                    Renderer renderer = cube.GetComponent<Renderer>();
                    renderer.material = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
                    renderer.material.mainTexture = renderTexture;
                    
                    string videoPath = "C:\\Users\\Mattheo\\OneDrive\\Documents\\BTD6 Mod Sources\\ChirstmasMod\\The_Grinch_has_Arived.mp4";
                    videoPlayer.url = videoPath;

                    videoPlayer.SetDirectAudioVolume(0, 0.7f);
                    videoPlayer.Play();
                    
                    instance.Close();
                }));
                StartCS.AddText(new("Title_", 0, 0, 300, 150), "Start Cutscene", 60);
            }
        }
    }
}
