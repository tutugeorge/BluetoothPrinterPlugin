using System.Threading.Tasks;

namespace XF.Bluetooth.Printer.Plugin.Abstractions
{
    /// <summary>
    /// Interface for Bluetooth printer
    /// </summary>
    public interface IPrint
    {
        /// <summary>
        /// Print the input stirng to blue tooth printer
        /// </summary>
        /// <param name="input">input data in string format</param>
        /// <param name="printerName">name of the paired bluetooth printer</param>
        Task PrintText(string input, string printerName);
    }
}
