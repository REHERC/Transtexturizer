using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Transtexturizer
{
    public static class MaterialUtil
    {
        private static System.Random RNG;

        public static List<string> Properties = new List<string>() {
            "_BackTex",
            "_BumpMap",
            "_BumpSpecMap",
            "_Control",
            "_DecalTex",
            "_Detail",
            "_DownTex",
            "_FrontTex",
            "_GlossMap",
            "_Illum",
            "_LeftTex",
            "_LightMap",
            "_LightTextureB0",
            "_MainTex",
            "_ParallaxMap",
            "_RightTex",
            "_ShadowOffset",
            "_Splat0",
            "_Splat1",
            "_Splat2",
            "_Splat3",
            "_TranslucencyMap",
            "_UpTex",
            "_Tex",
            "_Cube"
        };

        public static void Initialize()
        {
            RNG = new System.Random();

            string dirPath = $@"{CurrentPlugin.Files.RootDirectory}\Data";

            byte[] fileData;
            if (Directory.Exists(dirPath))
            {
                UniMaterial = new List<Material>();
                UniMaterial.Clear();
                UniTexture = new List<Texture>();
                UniTexture.Clear();

                foreach (string filePath in Directory.GetFiles(dirPath,"*.png"))
                {
                    Material UniMat = null;
                    Texture UniTex = null;

                    fileData = File.ReadAllBytes(filePath);
                    Texture2D Tex2D = new Texture2D(128, 128);
                    Tex2D.LoadImage(fileData);
                    UniTex = Tex2D as Texture;
                    UniTexture.Add(UniTex);
                    UniMat = new Material(Shader.Find("Unlit/Texture"));
                    foreach (string property in Properties)
                    {
                        if (UniMat.HasProperty(property))
                        {
                            UniMat.SetTexture(property, UniTex);
                        }
                    }

                    UniMaterial.Add(UniMat);
                }
            }
            else
            {
                UniMaterial = null;
            }
        }
        
        public static List<Material> UniMaterial;
        public static List<Texture> UniTexture;

        public static Material GetMaterial(int seed)
        {
            RNG = new System.Random(seed);
            return GetMaterial();
        }

        public static Material GetMaterial()
        {
            if (UniMaterial != null)
            {
                return UniMaterial[RNG.Next(0, UniMaterial.Count - 1)];
            }
            else
            {
                Material ErrorMat = new Material(Shader.Find("Unlit/Color"));
                ErrorMat.SetColor("_Color", new Color(0, 255, 255, 255));
                return ErrorMat;
            }
        }

        public static Texture GetTexture(int seed)
        {
            RNG = new System.Random(seed);
            return GetTexture();
        }

        public static Texture GetTexture()
        {
            if (UniMaterial != null)
            {
                return UniTexture[RNG.Next(0, UniTexture.Count - 1)];
            }
            else
            {
                return null;
            }
        }
    }
}
