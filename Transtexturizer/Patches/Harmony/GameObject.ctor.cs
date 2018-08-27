using System;
using System.Security.Cryptography;
using System.Text;
using Harmony;
using UnityEngine;

namespace Transtexturizer
{
    public partial class Photon
    {
        [HarmonyPatch(typeof(GameObject))]
        [HarmonyPatch(new Type[] { })]
        internal partial class Transform_ctor__null__Patch
        {
            public static void Postfix(GameObject __instance)
            {
                GameObject_Generic.Postfix(__instance);
            }
        }

        [HarmonyPatch(typeof(GameObject))]
        [HarmonyPatch(new Type[] { typeof(string) })]
        internal partial class Transform_ctor__string__Patch
        {
            public static void Postfix(GameObject __instance)
            {
                GameObject_Generic.Postfix(__instance);
            }
        }

        [HarmonyPatch(typeof(GameObject))]
        [HarmonyPatch(new Type[] { typeof(string), typeof(Type[]) })]
        internal partial class Transform_ctor__string__Type_arr__Patch
        {
            public static void Postfix(GameObject __instance)
            {
                GameObject_Generic.Postfix(__instance);
            }
        }

        internal static partial class GameObject_Generic
        {
            public static void Postfix(GameObject __instance)
            {
                if (__instance.HasComponent<Renderer>())
                {
                    Photon.SetRendererTexture(__instance.GetComponent<Renderer>());
                }
            }
        }
    }
}
