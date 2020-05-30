using System;
using System.IO.Ports;
using System.Media;

namespace button_processor
{
    class Program
    {
        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        SerialPort getSerialPort()
        {
            SerialPort sp = new SerialPort();
            sp.PortName = "COM3";
            sp.BaudRate = 9600;
            sp.Parity = Parity.None;
            sp.DataBits = 8;
            sp.StopBits = StopBits.One;
            sp.Handshake = Handshake.None;
            sp.ReadTimeout = 500;
            sp.WriteTimeout = 500;
            return sp;
        }

        static void Main(string[] args)
        {
            var startupSoundPlayer = new SoundPlayer("startup.wav");
            startupSoundPlayer.Play();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
