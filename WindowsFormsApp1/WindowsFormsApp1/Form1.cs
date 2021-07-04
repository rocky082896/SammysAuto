using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput.Native;
using WindowsInput;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        // Import the user32.dll
        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        // Declare some keyboard keys as constants with its respective code
        public const int KEYEVENTF_EXTENDEDKEY = 0x0001; //Key down flag
        public const int KEYEVENTF_KEYUP = 0x0002; //Key up flag
        public const int VK_RCONTROL = 0xA3; //Right Control key code

        // Simulate a key press event
        
       


        public Form1()
        {
            InitializeComponent();
            keybd_event(VK_RCONTROL, 0, KEYEVENTF_EXTENDEDKEY, 0);
            keybd_event(VK_RCONTROL, 0, KEYEVENTF_KEYUP, 0);
            Thread.Sleep(2000);
            InputSimulator sim = new InputSimulator();
            for (int i = 0; i < 20; i++)
            {
                 sim.Keyboard.TextEntry("HELLO " + i);

                //SendKeys.SendWait("Hello" + i + " ");
               
            }
           

        }
    }
}
