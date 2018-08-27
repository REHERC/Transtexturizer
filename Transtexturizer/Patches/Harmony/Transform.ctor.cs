using System;
using System.Security.Cryptography;
using System.Text;
using Harmony;
using UnityEngine;

namespace Transtexturizer
{
    public partial class Photon
    {
        [HarmonyPatch(typeof(Transform))]
        [HarmonyPatch(new Type[] { })]
        internal partial class Transform_ctor__null__Patch
        {
            public static void Postfix(Transform __instance)
            {
                Transform_Generic.Postfix(__instance);
            }
        }

        internal static partial class Transform_Generic
        {
            public static void Postfix(Transform __instance)
            {
                if (__instance.HasComponent<Renderer>())
                {
                    Photon.SetRendererTexture(__instance.GetComponent<Renderer>());
                }
            }
        }
    }
}
