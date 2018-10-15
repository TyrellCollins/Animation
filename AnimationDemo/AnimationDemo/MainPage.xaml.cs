using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AnimationDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

        }

            DispatcherTimer dispatcherTimer;
            DateTimeOffset startTime;
            DateTimeOffset lastTime;
            DateTimeOffset stopTime;
            int timesTicked = 1;
            int timesToTick = 10;

            int positionX = 100; //track position x
            int positionY = 100; //track position y
            int speedX = 5;
            int speedY = 5;
            int radius = 20;
               

          
                }//main page


            public void DispatcherTimerSetup()
            {
                dispatcherTimer = new DispatcherTimer();
                dispatcherTimer.Tick += dispatcherTimer_Tick;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                //IsEnabled defaults to false
                TimerLog.Text += "dispatcherTimer.IsEnabled = " + dispatcherTimer.IsEnabled + "\n";
                startTime = DateTimeOffset.Now;
                lastTime = startTime;
                TimerLog.Text += "Calling dispatcherTimer.Start()\n";
                dispatcherTimer.Start();
                //IsEnabled should now be true after calling start
                TimerLog.Text += "dispatcherTimer.IsEnabled = " + dispatcherTimer.IsEnabled + "\n";
            }

            void dispatcherTimer_Tick(object sender, object e)
            {
                DateTimeOffset time = DateTimeOffset.Now;
                TimeSpan span = time - lastTime;
                lastTime = time;
                //Time since last tick should be very very close to Interval
                TimerLog.Text += timesTicked + "\t time since last tick: " + span.ToString() + "\n";
                timesTicked++;
                if (timesTicked > timesToTick)
                {
                    stopTime = time;
                    TimerLog.Text += "Calling dispatcherTimer.Stop()\n";
                    dispatcherTimer.Stop();
                    //IsEnabled should now be false after calling stop
                    TimerLog.Text += "dispatcherTimer.IsEnabled = " + dispatcherTimer.IsEnabled + "\n";
                    span = stopTime - startTime;
                    TimerLog.Text += "Total Time Start-Stop: " + span.ToString() + "\n";
                }
            }
            private void Page_Loaded_1(object sender, RoutedEventArgs e)
            {
                DispatcherTimerSetup();
            }

        

        
    }//animation demo

