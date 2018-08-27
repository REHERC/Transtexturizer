using System.IO;
using Spectrum.API.Storage;
using UnityEngine;

namespace Transtexturizer
{
    public static class CurrentPlugin
    {
        public static void Initialize()
        {
            Log = new Spectrum.API.Logging.Logger("Transtexturizer.log");
            Log.WriteToConsole = true;
            Files = new FileSystem();


            
        }

        

        public static FileSystem Files;
        public static Spectrum.API.Logging.Logger Log;
    }
}
