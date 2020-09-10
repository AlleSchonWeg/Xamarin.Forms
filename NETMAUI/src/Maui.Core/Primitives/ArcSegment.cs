﻿using System.ComponentModel;

namespace System.Maui
{
    public class ArcSegment : PathSegment
    {
        public ArcSegment()
        {

        }

        public ArcSegment(Point point, Size size, double rotationAngle, SweepDirection sweepDirection, bool isLargeArc)
        {
            Point = point;
            Size = size;
            RotationAngle = rotationAngle;
            SweepDirection = sweepDirection;
            IsLargeArc = isLargeArc;
        }

        public Point Point { get; set; }

        [TypeConverter(typeof(Xaml.SizeTypeConverter))]
        public Size Size { get; set; }

        public double RotationAngle { get; set; }

        public SweepDirection SweepDirection { get; set; }

        public bool IsLargeArc { get; set; }
    }
}