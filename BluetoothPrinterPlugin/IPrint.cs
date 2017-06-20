using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XF.Plugin.Abstractions
{
    public interface IPrint
    {
        /// <summary>
        /// Print the input stirng to blue tooth printer
        /// </summary>
        /// <param name="input">input data in string format</param>
        void Print(string input);
    }
}
