using System;
using System.Linq;
using Microsoft.Devices.Sensors;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Reactive;

namespace Rx_Compass_Smoothing
{
    public partial class MainPage : PhoneApplicationPage
    {
        Compass compass = new Compass();

        public MainPage()
        {
            InitializeComponent();

            Observable.FromEvent<SensorReadingEventArgs<CompassReading>>(compass, "CurrentValueChanged")
                .BufferWithTime(TimeSpan.FromSeconds(2))
                .Select(headings => headings.Select(e => e.EventArgs.SensorReading.TrueHeading).Sum() / headings.Count())
                .ObserveOnDispatcher()
                .Subscribe(heading => this.ArrowRotation.Angle = 360 - heading);

            compass.Start();
        }
    }
}