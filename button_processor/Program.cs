﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Media;
using System.Threading;

namespace button_processor
{
    class Program
    {
        static bool s_continue = true;
        static SerialPort s_sp;

        static readonly Dictionary<string, SoundPlayer> s_keySounds = new Dictionary<string, SoundPlayer>()
        {
            {"FFA25D", new SoundPlayer("res/unknown.wav")},
            {"FF629D", new SoundPlayer("res/unknown.wav")},
            {"FFE21D", new SoundPlayer("res/unknown.wav")},
            {"FF22DD", new SoundPlayer("res/unknown.wav")},
            {"FF02FD", new SoundPlayer("res/unknown.wav")},
            {"FFC23D", new SoundPlayer("res/unknown.wav")},
            {"FFE01F", new SoundPlayer("res/unknown.wav")},
            {"FFA857", new SoundPlayer("res/unknown.wav")},
            {"FF906F", new SoundPlayer("res/unknown.wav")},
            {"FF6897", new SoundPlayer("res/unknown.wav")},
            {"FF9867", new SoundPlayer("res/unknown.wav")},
            {"FFB04F", new SoundPlayer("res/unknown.wav")},
            {"FF30CF", new SoundPlayer("res/unknown.wav")},
            {"FF18E7", new SoundPlayer("res/unknown.wav")},
            {"FF7A85", new SoundPlayer("res/unknown.wav")},
            {"FF10EF", new SoundPlayer("res/unknown.wav")},
            {"FF38C7", new SoundPlayer("res/meow.wav")},
            {"FF5AA5", new SoundPlayer("res/bark.wav")},
            {"FF42BD", new SoundPlayer("res/unknown.wav")},
            {"FF4AB5", new SoundPlayer("res/unknown.wav")},
            {"FF52AD", new SoundPlayer("res/unknown.wav")}
        };

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
        

        private static void Read()
        {
            while (s_continue)
            {
                try { processKey(s_sp.ReadLine().TrimEnd()); }
                catch (TimeoutException) { }
            }
        }

        private static void processKey(string key)
        {
            SoundPlayer sp;
            if (s_keySounds.TryGetValue(key, out sp)) sp.Play();
        }
    }
}
