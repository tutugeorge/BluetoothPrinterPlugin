using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XF.Printer.Plugin.Abstractions;

namespace XF.Printer.Plugin
{
    public static class CrossPrint
    {
        static Lazy<IPrint> TTS = new Lazy<IPrint>(
            () => CreatePrint(), 
            System.Threading.LazyThreadSafetyMode.PublicationOnly);

        public static IPrint Current
        {
            get
            {
                var ret = TTS.Value;
                if (ret == null)
                    throw NotImplementedInReferenceAssembly();
                return ret; 
            }
        }

        static IPrint CreatePrint()
        {
#if PORTABLE
            return null;
#else
            return new Print();
#endif
        }

        internal static Exception NotImplementedInReferenceAssembly()
        {
            return new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the Xam.Plugins.Vibrate NuGet package from your main application project in order to reference the platform-specific implementation.");
        }
    }
}
