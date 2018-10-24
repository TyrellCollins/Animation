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
using Windows.UI.Xaml.Shapes;
using static Windows.UI.Color;

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

        //Create structs required for dispatch timer to function
        DispatcherTimer dispatcherTimer;
        DateTimeOffset startTime;
        DateTimeOffset lastTime;
        //DateTimeOffset stopTime; //Stop time not needed in this scope

        int timesTicked = 1;
        //int timesToTick = 10; //Only necessary if stopping time

        int positionX = 50;
        int positionY = 50;
        int radius = 50;
        int speedX = 5;
        int speedY = 5;
        byte a = 0; //brightness value
        byte r = 50; //red value
        byte g = 50; //green value
        byte b = 50; //blue value

        public void DispatcherTimerSetup()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            //IsEnabled defaults to false
            //TimerLog.Text += "dispatcherTimer.IsEnabled = " + dispatcherTimer.IsEnabled + "\n";  //for debugging
            startTime = DateTimeOffset.Now;
            lastTime = startTime;
            // TimerLog.Text += "Calling dispatcherTimer.Start()\n";  //for debugging purposes
            dispatcherTimer.Start();
            //IsEnabled should now be true after calling start
            //TimerLog.Text += "dispatcherTimer.IsEnabled = " + dispatcherTimer.IsEnabled + "\n"; //for debugging
        }

        void dispatcherTimer_Tick(object sender, object e)
        {

            DateTimeOffset time = DateTimeOffset.Now;
            TimeSpan span = time - lastTime;
            lastTime = time;
            //Time since last tick should be very very close to Interval
            //TimerLog.Text += timesTicked + "\t time since last tick: " + span.ToString() + "\n";  for debugging
            timesTicked++;

            if (a > 255)
            {
                a = 0;
            }
            else a+=51;

            if (r > 255)
            {
                r = 0;
            }
            else r+=5;

            if (g < 0)
            {
                g = 255;
            }
            else g--;

            if (b > 255)
            {
                b = 0;
            }
            else b++;

            /*
            if (timesTicked > timesToTick)
            {
                stopTime = time;
                //TimerLog.Text += "Calling dispatcherTimer.Stop()\n"; for debugging end dispatch timer
                dispatcherTimer.Stop();
                //IsEnabled should now be false after calling stop
                // TimerLog.Text += "dispatcherTimer.IsEnabled = " + dispatcherTimer.IsEnabled + "\n";  for debugging
                span = stopTime - startTime;
                //TimerLog.Text += "Total Time Start-Stop: " + span.ToString() + "\n";  for debugging
            }
            */
            var path1 = new Windows.UI.Xaml.Shapes.Path();


            /*
             * public static Windows.UI.Color.FromArgb(byte a, byte r, byte g, byte b);
             * The above line of code allows us to adjust specific values of: Brightness, Red, Green and Blue
            */

            path1.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(a, r, g, b));

            var geometryGroup1 = new GeometryGroup();

            var ellipseGeometry1 = new EllipseGeometry();
            ellipseGeometry1.Center = new Point(positionX, positionY);
            ellipseGeometry1.RadiusX = radius;
            ellipseGeometry1.RadiusY = radius;
            geometryGroup1.Children.Add(ellipseGeometry1);

            var pathGeometry1 = new PathGeometry();


            geometryGroup1.Children.Add(pathGeometry1);
            path1.Data = geometryGroup1;

            layoutRoot.Children.Clear();
            layoutRoot.Children.Add(path1);

            positionX += speedX;
            positionY += speedY;

            if (positionY + radius > layoutRoot.ActualHeight)
            {
                speedY *= -1;
            }

            if (positionX + radius > layoutRoot.ActualWidth)
            {
                speedX *= -1;
            }
            if (positionY - radius < 0)
            {
                speedY *= -1;
            }

            if (positionX - radius < 0)
            {
                speedX *= -1;
            }
        }

        private void Page_Loading(FrameworkElement sender, object args)
        {
            DispatcherTimerSetup();
        }

    }
}//animation demo
