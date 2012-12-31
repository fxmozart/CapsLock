using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CapsLocks
{
    using System.Reactive.Linq;
    using Properties;

    public partial class HiddenForm : Form
    {
        public HiddenForm()
        {
          
            InitializeComponent();

            Opacity = 0;


            this.notifyIcon1.MouseClick += (sender, args) =>
                                               {
                                                   if (args.Button == MouseButtons.Right)
                                                   {
                                                       this.Close();

                                                   }
                                               };


            this.notifyIcon1.Text = "Right click to kill the software.";
          
           
            this.ShowInTaskbar = false;

            this.Width = 0;
            this.Height = 0;
            Visible = false;

            this.Hide();
         

          //  CapsLock();




            Observable.Interval(TimeSpan.FromMilliseconds(500)).Subscribe(l => ChangeIconIfCaps(l));



        }

        private void ChangeIconIfCaps(long l)
        {


            if (Control.IsKeyLocked(Keys.CapsLock))
            {
                notifyIcon1.Text = "ON";
                this.notifyIcon1.BalloonTipTitle = "ON";
                this.notifyIcon1.Icon = Resources.RUN;
            }
            else if (!Control.IsKeyLocked(Keys.CapsLock))
            {
                notifyIcon1.Text = "OFF";
                this.notifyIcon1.BalloonTipTitle = "OFF";
                this.notifyIcon1.Icon = Resources.login;
            }
        }

         

        [DllImport("user32.dll")]
            private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

            private const int KEYEVENTF_EXTENDEDKEY = 0x1;
            private const int KEYEVENTF_KEYUP = 0x2;

            public void CapsLock()
            {

                this.KeyDown += (sender, args) =>
                                    {
                                        if (Control.IsKeyLocked(Keys.CapsLock))
                                        {
                                            //  Console.WriteLine("Caps Lock key is ON.  We'll turn it off");
                                            //keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr) 0);
                                            //keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP,
                                            //            (UIntPtr) 0);

                                         
                                            notifyIcon1.Text = "ON";
                                            this.notifyIcon1.BalloonTipTitle = "ON";
                                            this.notifyIcon1.Icon = Resources.RUN;
                                        }
                                        else
                                        {
                                            
                                             
                                            notifyIcon1.Text = "OFF";
                                            this.notifyIcon1.BalloonTipTitle = "OFF";
                                            this.notifyIcon1.Icon = Resources.login;
                                        }
                                    };

            }
        }
    
}