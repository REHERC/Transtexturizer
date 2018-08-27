using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Harmony;
using UnityEngine;

namespace Transtexturizer
{
    public partial class Photon
    {
        [HarmonyPatch(typeof(Material))]
        [HarmonyPatch(new Type[] { typeof(string) })]
        internal partial class Material_ctor__string__Patch
        {
            public static void Postfix(Material __instance)
            {
                Material_ctor_Generic.Postfix(__instance);
            }
        }

        [HarmonyPatch(typeof(Material))]
        [HarmonyPatch(new Type[] { typeof(Material) })]
        internal partial class Material_ctor__Material__Patch
        {
            public static void Postfix(Material __instance)
            {
                Material_ctor_Generic.Postfix(__instance);
            }
        }

        [HarmonyPatch(typeof(Material))]
        [HarmonyPatch(new Type[] { typeof(Shader) })]
        internal partial class Material_ctor__Shader__Patch
        {
            public static void Postfix(Material __instance)
            {
                Material_ctor_Generic.Postfix(__instance);
            }
        }

        internal static partial class Material_ctor_Generic
        {
            public static void Postfix(Material __instance)
            {
                // Generate a number for the rng but make sure it's the same each frame.
                var MD5Input = __instance.name + __instance.color.ToString();
                MD5 MD5Hasher = MD5.Create();
                var Hashed = MD5Hasher.ComputeHash(Encoding.UTF8.GetBytes(MD5Input));
                var RNGSeed = BitConverter.ToInt32(Hashed, 0);


                // Select a material from the list.
                Texture Tex = MaterialUtil.GetTexture(RNGSeed);


                foreach(string property in MaterialUtil.Properties)
                {
                    if (__instance.HasProperty(property))
                    {
                        __instance.SetTexture(property, Tex);
                    }
                }
            }
        }
    }
}
