using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using ChristmasMod;
using HarmonyLib;
using Il2CppAssets.Scripts;
using Il2CppAssets.Scripts.Models.Bloons;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Simulation.SMath;
using Il2CppAssets.Scripts.Simulation.Towers.Weapons;
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

namespace ChristmasMod.UI;

public class StartCutscene
{
    [RegisterTypeInIl2Cpp(false)]
    public class StartCutsceneUI : MonoBehaviour
    {
        static bool playingCutscene = false;

        static Dictionary<ObjectId, float> speedCache = [];

        public static StartCutsceneUI instance = null;

        public static GameObject map = null;

        public static GameObject objects = null;

        public static Bloon bloon;
        
        public void Close()
        {
            if (gameObject)
            {
                gameObject.Destroy();
            }
        }
        public static void CreatePanel(bool Angry, bool dying, Bloon boss)
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
                    
                    playingCutscene = true;

                    InGame.instance.mapRect.Hide();
                    InGame.instance.uiRect.Hide();
                    map = GameObject.Find("Map");
                    map.SetActive(false);
                    objects = GameObject.Find("Objects");
                    objects.SetActive(false);

                    foreach (var bloon in InGame.instance.GetBloons())
                    {
                        speedCache.Add(bloon.Id, bloon.bloonModel.speed);
                        bloon.bloonModel.speed = 0;
                        bloon.UpdatedModel(bloon.bloonModel);
                        bloon.UpdateRootModel(bloon.bloonModel);
                    }
                }));
                StartCS.AddText(new("Title_", 0, 0, 300, 150), "Start Cutscene", 60);

                if (dying == true)
                {
                    StartCS.Destroy();
                    
                    instance.Close();
                    
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.localScale = new Vector3(310f, 405, 1f);
                    cube.transform.rotation = Quaternion.Euler(0, 0, 180);
                    cube.name = "CubeDead";
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
                    //objects.SetActive(false);
                    
                    foreach (var bloon in InGame.instance.GetBloons())
                    {
                        speedCache.Add(bloon.Id, bloon.bloonModel.speed);
                        bloon.bloonModel.speed = 0;
                        bloon.UpdatedModel(bloon.bloonModel);
                        bloon.UpdateRootModel(bloon.bloonModel);
                    }
                }
                
                
                if (Angry == true)
                {
                    boss.trackSpeedMultiplier = 0;
                    
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
                    //objects.SetActive(false);
                    
                    foreach (var bloon in InGame.instance.GetBloons())
                    {
                        speedCache.Add(bloon.Id, bloon.bloonModel.speed);
                        bloon.bloonModel.speed = 0;
                        bloon.UpdatedModel(bloon.bloonModel);
                        bloon.UpdateRootModel(bloon.bloonModel);
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
                    //objects.SetActive(true);
                    
                    InGame.instance.SetRound(99);

                    playingCutscene = false;

                    foreach (var bloon in InGame.instance.GetBloons())
                    {
                        if (speedCache.ContainsKey(bloon.Id))
                        {
                            bloon.bloonModel.speed = speedCache[bloon.Id];
                            bloon.UpdatedModel(bloon.bloonModel);
                            bloon.UpdateRootModel(bloon.bloonModel);
                            speedCache.Remove(bloon.Id);
                        }
                    }
                }
            }
        }
        public static void Timer1()
        {
            GameObject cube = GameObject.Find("CubeAngry");
            VideoPlayer vp = cube.gameObject.GetComponent<VideoPlayer>();

            if (vp != null)
            {
                if (vp.time > 132)
                {
                    vp.Destroy();
                    cube.Destroy();
                    InGame.instance.mapRect.Show();
                    InGame.instance.uiRect.Show();
                    map.SetActive(true);
                    //objects.SetActive(true);

                    bloon.trackSpeedMultiplier = 2;
                    
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
                foreach (var bloon in InGame.instance.GetBloons())
                {
                    if (speedCache.ContainsKey(bloon.Id))
                    {
                        bloon.bloonModel.speed = speedCache[bloon.Id];
                        bloon.UpdatedModel(bloon.bloonModel);
                        bloon.UpdateRootModel(bloon.bloonModel);
                        speedCache.Remove(bloon.Id);
                    }
                }
            }
        }
        
        public static void Timer2()
        {
            GameObject cube = GameObject.Find("CubeDead");
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
                    //objects.SetActive(true);
                    
                    InGame.instance.SetRound(99);

                    playingCutscene = false;

                    foreach (var bloon in InGame.instance.GetBloons())
                    {
                        if (speedCache.ContainsKey(bloon.Id))
                        {
                            bloon.bloonModel.speed = speedCache[bloon.Id];
                            bloon.UpdatedModel(bloon.bloonModel);
                            bloon.UpdateRootModel(bloon.bloonModel);
                            speedCache.Remove(bloon.Id);
                        }
                    }
                }
            }
        }
    }
}
