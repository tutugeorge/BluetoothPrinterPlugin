using System.Threading.Tasks;

namespace XF.Bluetooth.Printer.Plugin.Abstractions
{
    public interface IPrint
    {
        /// <summary>
        /// Print the input stirng to blue tooth printer
        /// </summary>
        /// <param name="input">input data in string format</param>
        Task PrintText(string input, string printerName);
    }
}
