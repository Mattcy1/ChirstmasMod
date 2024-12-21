using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using ChristmasMod;
using Il2CppAssets.Scripts.Models.Bloons;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Simulation.SMath;
using Il2CppAssets.Scripts.Unity.Bridge;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Utils;
using Il2CppMono.Unity;
using MelonLoader;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using Quaternion = UnityEngine.Quaternion;
using Scene = Il2CppAssets.Scripts.Unity.Display.Scene;
using Vector3 = UnityEngine.Vector3;

namespace TemplateMod.UI;

public class StartCutscene
{
    [RegisterTypeInIl2Cpp(false)]
    public class StartCutsceneUI : MonoBehaviour
    {
        public static StartCutsceneUI instance = null;

        public static GameObject map = null;

        public static Bloon bloon;
        
        public void Close()
        {
            if (gameObject)
            {
                gameObject.Destroy();
            }
        }
        public static void CreatePanel(bool Angry, Bloon boss)
        {
            if (InGame.instance != null)
            {
                RectTransform rect = InGame.instance.uiRect;
                var panel = rect.gameObject.AddModHelperPanel(new("Panel_", 0, 0, 0, 0), VanillaSprites.BrownPanel);
                instance = panel.AddComponent<StartCutsceneUI>();

                if (boss != null)
                {
                    bloon = boss;
                }
                
                var StartCS = panel.AddButton(new("Button_", 0, 0, 450, 450 / 2), VanillaSprites.GreenBtnLong, new System.Action(() =>
                {
                    instance.Close();
                    
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
                }));
                StartCS.AddText(new("Title_", 0, 0, 300, 150), "Start Cutscene", 60);

                if (Angry == true)
                {
                    StartCS.Destroy();
                    
                    instance.Close();
                    
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.localScale = new Vector3(310f, 405, 1f);
                    cube.transform.rotation = Quaternion.Euler(0, 0, 180);
                    cube.name = "CubeAngry";
                    MelonLogger.Msg("Renammed Cube: " + cube.name);
                    
                    var videoPlayer = cube.AddComponent<UnityEngine.Video.VideoPlayer>();

                    RenderTexture renderTexture = new RenderTexture(1920, 1080, 0);
                    renderTexture.Create();

                    videoPlayer.renderMode = VideoRenderMode.RenderTexture;
                    videoPlayer.targetTexture = renderTexture;

                    Renderer renderer = cube.GetComponent<Renderer>();
                    renderer.material = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
                    renderer.material.mainTexture = renderTexture;
                    
                    string videoPath = "https://mattcy1.github.io/VideoHosting/The_Grinch-o-Matic_2.0.mp4";
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
                }
            }
        }

        public static void Timer()
        {
            GameObject cube = GameObject.Find("Cube");
            VideoPlayer vp = cube.gameObject.GetComponent<VideoPlayer>();

            if (vp != null)
            {
                if (vp.time > 86)
                {
                    vp.Destroy();
                    cube.Destroy();
                    InGame.instance.mapRect.Show();
                    InGame.instance.uiRect.Show();
                    map.SetActive(true);
                    
                    foreach (var tower in InGame.instance.GetTowers())
                    {
                        tower.Scale = new Vector3Boxed(1f, 1f, 1f);
                    }
                    
                    InGame.instance.SetRound(99);
                }
            }
        }
        public static void Timer1()
        {
            GameObject cube = GameObject.Find("CubeAngry");
            VideoPlayer vp = cube.gameObject.GetComponent<VideoPlayer>();

            if (vp != null)
            {
                if (vp.time > 1000)
                {
                    vp.Destroy();
                    cube.Destroy();
                    InGame.instance.mapRect.Show();
                    InGame.instance.uiRect.Show();
                    map.SetActive(true);
                    
                    bloon.trackSpeedMultiplier = 2;

                    foreach (var tower in InGame.instance.GetTowers())
                    {
                        tower.Scale = new Vector3Boxed(1f, 1f, 1f);
                    }
                    
                    Task.Run(async () =>
                    {
                        await Task.Delay(2000);

                        InGame.instance.SpawnBloons("CandyCaneBossNHB", 1, 0);
                    });
                    Task.Run(async () =>
                    {
                        await Task.Delay(4000);

                        Values.storyExecuted = true;

                        InGame.instance.SpawnBloons("FrostyNHB", 1, 0);
                    });
                    Task.Run(async () =>
                    {
                        await Task.Delay(8000);

                        InGame.instance.SpawnBloons("CookieMonsterNHB", 1, 0);
                    });
                }
            }
        }
    }
}
