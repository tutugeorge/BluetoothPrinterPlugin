using System;
using System.Linq;
using Android.Content;
using XF.Bluetooth.Printer.Plugin.Abstractions;
using Android.Bluetooth;
using Java.Util;
using System.Threading.Tasks;

namespace XF.Bluetooth.Printer.Plugin
{
    /// <summary>
    /// Print funtion
    /// </summary>
    public class Print : IPrint
    {
        /// <summary>
        /// This method prints data to the bluetooth printer
        /// </summary>
        /// <param name="input"></param>
        /// <param name="printerName"></param>
        /// <returns></returns>
        public async Task PrintText(string input, string printerName)
        {
            
                using (BluetoothAdapter bluetoothAdapter = BluetoothAdapter.DefaultAdapter)
                {
                    if (bluetoothAdapter == null)
                    {
                        throw new Exception("No default adapter");
                        //return;
                    }

                    if (!bluetoothAdapter.IsEnabled)
                    {
                        throw new Exception("Bluetooth not enabled");
                        Intent enableIntent = new Intent(BluetoothAdapter.ActionRequestEnable);
                        //StartActivityForResult(enableIntent, REQUEST_ENABLE_BT);
                        // Otherwise, setup the chat session
                    }

                    BluetoothDevice device = (from bd in bluetoothAdapter.BondedDevices
                                              where bd.Name == printerName
                                              select bd).FirstOrDefault();
                    if (device == null)
                        throw new Exception(printerName + " device not found.");

                try
                {
                    using (BluetoothSocket _socket = device.CreateRfcommSocketToServiceRecord(UUID.FromString("00001101-0000-1000-8000-00805f9b34fb")))
                    {
                        await _socket.ConnectAsync();

                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(input);
                        await Task.Delay(3000);
                        // Write data to the device
                        await _socket.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                        _socket.Close();
                    }
                }
                catch (Exception exp)
                {
                    
                    throw exp;
                }

                    
                }
            
        }
    }
}