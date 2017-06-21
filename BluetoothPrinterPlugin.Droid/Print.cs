using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using XF.Printer.Plugin.Abstractions;
using Android.Bluetooth;
using Java.Util;
using System.Threading.Tasks;

namespace XF.Printer.Plugin
{
    public class Print : IPrint
    {
        public async Task PrintText(string input, string printerName)
        {
            try
            {
                using (BluetoothAdapter bluetoothAdapter = BluetoothAdapter.DefaultAdapter)
                {
                    if (bluetoothAdapter == null)
                    {
                        return;
                    }

                    if (!bluetoothAdapter.IsEnabled)
                    {
                        Intent enableIntent = new Intent(BluetoothAdapter.ActionRequestEnable);
                        //StartActivityForResult(enableIntent, REQUEST_ENABLE_BT);
                        // Otherwise, setup the chat session
                    }

                    BluetoothDevice device = (from bd in bluetoothAdapter.BondedDevices
                                              where bd.Name == printerName
                                              select bd).FirstOrDefault();

                    using (BluetoothSocket _socket = device.CreateRfcommSocketToServiceRecord(UUID.FromString("00001101-0000-1000-8000-00805f9b34fb")))
                    {
                        await _socket.ConnectAsync();

                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(input);
                        await Task.Delay(3000);
                        // Write data to the device
                        await _socket.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                        _socket.Close();
                    }

                    if (device == null)
                        throw new Exception("Named device not found.");
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}