using System;
using Harmony;
using Spectrum.API.Interfaces.Plugins;
using Spectrum.API.Interfaces.Systems;
using System.Reflection;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace Transtexturizer
{
    public partial class Photon : IPlugin, IUpdatable
    {
        public void Initialize(IManager manager, string ipcIdentifier)
        {
            Console.WriteLine($"Initializing ... ({ipcIdentifier})");
            CurrentPlugin.Initialize();
            MaterialUtil.Initialize();
            RendererManager.Initialize();
            try
            {
                CurrentPlugin.Log.Info("Instantiating Harmony Patcher ...");
                HarmonyInstance Harmony = HarmonyInstance.Create("com.REHERC.Transtexturizer");
                CurrentPlugin.Log.Info("Harmony patcher instantiated!");
                CurrentPlugin.Log.Info("Patching assemblies ...");
                Harmony.PatchAll(Assembly.GetExecutingAssembly());
                CurrentPlugin.Log.Info("Assemblies patched!");
            }
            catch (Exception VirusSpirit)
            {
                CurrentPlugin.Log.Error("Failed to patch the assemblies!");
                CurrentPlugin.Log.Exception(VirusSpirit);
            }

            CurrentPlugin.Log.Info("Subscribing to Events ...");
            Events.Scene.StartLoad.Subscribe((data) =>
            {
                RendererManager.Renderers.Clear();
                levelloadcomplete = false;
            });
            Events.Scene.LoadFinish.Subscribe((data) =>
            {
                RendererManager.Renderers.Clear();
                levelloadcomplete = true;
                framecount = 0;
                currentframe = 0;
            });
            CurrentPlugin.Log.Info("Subscribed to Events!");
        }

        public bool levelloadcomplete;
        public int framecount = 0;
        public int currentframe = 0;
        
        public void Update()
        {
            

            framecount++;
            if (levelloadcomplete && framecount < 128)
            {
                currentframe++;
                if (currentframe >= 4)
                {
                    Scene scene = SceneManager.GetActiveScene();

                    renderers = new List<Renderer>(Resources.FindObjectsOfTypeAll<Renderer>());
                    foreach (GameObject go in scene.GetRootGameObjects())
                    {
                        renderers.AddRange(go.GetComponentsInChildren<Renderer>());
                    }

                    foreach (Renderer renderer in renderers)
                    {
                        SetRendererTexture(renderer);
                    }

                    currentframe = 0;
                    framecount++;
                }
            }
            
        }

        public List<Renderer> renderers = null;

        public static void SetRendererTexture(Renderer renderer)
        {
            foreach (string item in RendererManager.BlackList)
            {
                if (renderer.name.Contains(item) || item.Contains(renderer.name))
                {
                    return;
                }
            }

            // Generate a number for the rng but make sure it's the same each frame.
            var MD5Input = renderer.name + Util.GameObjectPath(renderer.transform) + renderer.transform.position.ToString() + renderer.transform.rotation.ToString() + renderer.transform.lossyScale.ToString();
            MD5 MD5Hasher = MD5.Create();
            var Hashed = MD5Hasher.ComputeHash(Encoding.UTF8.GetBytes(MD5Input));
            var RNGSeed = BitConverter.ToInt32(Hashed, 0);


            // Select a material from the list.
            Material SelectedMat = MaterialUtil.GetMaterial(RNGSeed);


            // Apply the selected material    E V E R Y W H E R E
            renderer.material = SelectedMat;
            renderer.sharedMaterial = SelectedMat;

            for (int i = 0; i < renderer.materials.Length; i++)
            {
                renderer.materials[i] = SelectedMat;
            }

            for (int i = 0; i < renderer.sharedMaterials.Length; i++)
            {
                renderer.materials[i] = SelectedMat;
            }
        }
    }
}
