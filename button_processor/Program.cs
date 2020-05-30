using System;
using System.IO.Ports;
using System.Media;
using System.Threading;

namespace button_processor
{
    class Program
    {
        static bool s_continue = true;
        static SerialPort s_sp;

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        private static SerialPort getSerialPort()
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
            var startupSoundPlayer = new SoundPlayer("res/startup.wav");
            startupSoundPlayer.Play();

            var readThread = new Thread(Read);

            s_sp = getSerialPort();
            s_sp.Open();
            s_continue = true;
            readThread.Start();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            s_continue = false;
            readThread.Join();
            s_sp.Close();
        }
        /*
        const char* keyToString(const unsigned long& key)
{
  if (key == 0xFFA25D) return "CH-";
  if (key == 0xFF629D) return "CH";
  if (key == 0xFFE21D) return "CH+";
  if (key == 0xFF22DD) return "PREV";
  if (key == 0xFF02FD) return "NEXT";
  if (key == 0xFFC23D) return "PLAY/PAUSE";
  if (key == 0xFFE01F) return "VOL-";
  if (key == 0xFFA857) return "VOL+";
  if (key == 0xFF906F) return "EQ";
  if (key == 0xFF6897) return "0";
  if (key == 0xFF9867) return "100+";
  if (key == 0xFFB04F) return "200+";
  if (key == 0xFF30CF) return "1";
  if (key == 0xFF18E7) return "2";
  if (key == 0xFF7A85) return "3";
  if (key == 0xFF10EF) return "4";
  if (key == 0xFF38C7) return "5";
  if (key == 0xFF5AA5) return "6";
  if (key == 0xFF42BD) return "7";
  if (key == 0xFF4AB5) return "8";
  if (key == 0xFF52AD) return "9";
}*/

    private static void Read()
        {
            while (s_continue)
            {
                try
                {
                    processKey(s_sp.ReadLine());
                }
                catch (TimeoutException) { }
            }
        }

        private static void processKey(string key)
        {
            if (key == "FF38C7\r")
            {
                var startupSoundPlayer = new SoundPlayer("res/meow.wav");
                startupSoundPlayer.Play();
            }

            if (key == "FF5AA5\r")
            {
                var startupSoundPlayer = new SoundPlayer("res/bark.wav");
                startupSoundPlayer.Play();
            }
        }
    }
}
