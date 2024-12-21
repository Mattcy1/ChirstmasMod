using System.Linq;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using ChristmasMod;
using Il2CppAssets.Scripts.Simulation.SMath;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Unity.UI_New.Popups;
using MelonLoader;
using UnityEngine;
using UnityEngine.Video;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace TemplateMod.UI;

public class Gift
{
    [RegisterTypeInIl2Cpp(false)]
    public class GiftUI : MonoBehaviour
    {
        public static GiftUI instance = null;

        public static GameObject map = null;

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
                        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        cube.transform.localScale = new Vector3(310f, 405, 1f);
                        cube.transform.rotation = Quaternion.Euler(0, 0, 180);
                    
                        var videoPlayer = cube.AddComponent<UnityEngine.Video.VideoPlayer>();

                        RenderTexture renderTexture = new RenderTexture(1920, 1080, 0);
                        renderTexture.Create();

                        videoPlayer.renderMode = VideoRenderMode.RenderTexture;
                        videoPlayer.targetTexture = renderTexture;

                        Renderer renderer = cube.GetComponent<Renderer>();
                        renderer.material = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
                        renderer.material.mainTexture = renderTexture;
                    
                        string videoPath = "https://mattcy1.github.io/VideoHosting/The_Grinch_has_Arived.mp4";
                        videoPlayer.url = videoPath;

                        videoPlayer.SetDirectAudioVolume(0, 0.5f);
                        videoPlayer.Play();
                    
                        InGame.instance.mapRect.Hide();
                        InGame.instance.uiRect.Hide();
                        map = GameObject.Find("Map");
                        map.SetActive(false);

                        foreach (var tower in InGame.instance.GetTowers())
                        {
                            tower.Scale = new Vector3Boxed(0f, 0f, 0f);
                        }

                        foreach (var bloon in InGame.instance.GetAllBloonToSim().ToList())
                        {
                            bloon.GetBloon().Scale = new Vector3Boxed(0f, 0f, 0f);
                        }
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
