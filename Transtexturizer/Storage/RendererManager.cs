using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transtexturizer
{
    public static class RendererManager
    {
        public static void Initialize()
        {
            Renderers = new List<string>();
            Renderers.Clear();
        }

        public static List<string> Renderers;


        public static List<string> BlackList = new List<string>()
        {
            "KillGrid",
            "Sky",
            "Fog",
            "Light"
        };
    }
}
