**Rx Compass Smoothing Example for Windows Phone**


Companion blog post: http://factormystic.net/blog/using-reactive-extensions-to-smooth-compass-data-in-windows-phone

Reactive Extensions at work:

    Observable.FromEvent<SensorReadingEventArgs<CompassReading>>(compass, "CurrentValueChanged")
              .BufferWithTime(TimeSpan.FromSeconds(2))
              .Select(headings => headings.Select(e => e.EventArgs.SensorReading.TrueHeading).Sum() / headings.Count())
              .ObserveOnDispatcher()
              .Subscribe(heading => this.ArrowRotation.Angle = 360 - heading);


Rotates the compass arrow:

    <Image Stretch="None" Source="compass.arrow.dark.png" RenderTransformOrigin="0.5,0.5">
        <Image.RenderTransform>
            <RotateTransform x:Name="ArrowRotation" CenterX="0.5" CenterY="0.5" />
        </Image.RenderTransform>
    </Image>