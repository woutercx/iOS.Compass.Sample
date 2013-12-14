using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreLocation;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;

namespace Compass
{
	public partial class Compass_ViewController : UIViewController
	{
		//Translated from Objective C from this example:
	    //https://github.com/kiichi/CompassExample/tree/master/CompassExample
	    //With help from this other article:
		//https://github.com/xamarin/monotouch-samples/blob/master/CoreLocation/MainScreen/MainViewController.cs
		CLLocationManager _iPhoneLocationManager = null;
		
		public Compass_ViewController () : base ("Compass_ViewController", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// initialize our location manager and callback handler
            _iPhoneLocationManager = new CLLocationManager ();
            _iPhoneLocationManager.DesiredAccuracy = CLLocation.AccuracyBest;
            _iPhoneLocationManager.HeadingFilter = 1;
            
            _iPhoneLocationManager.UpdatedHeading += HandleUpdatedHeading;
            _iPhoneLocationManager.StartUpdatingHeading();
		}

		void HandleUpdatedHeading (object sender, CLHeadingUpdatedEventArgs e)
		{
			double oldRad = -_iPhoneLocationManager.Heading.TrueHeading * Math.PI / 180D;
			double newRad = -e.NewHeading.TrueHeading * Math.PI / 180D;
			
			CABasicAnimation theAnimation;
			theAnimation = CABasicAnimation.FromKeyPath("transform.rotation");
			theAnimation.From = NSNumber.FromDouble(oldRad);
			theAnimation.To = NSNumber.FromDouble(newRad);
			theAnimation.Duration = 0.5;
			compassImage.Layer.AddAnimation(theAnimation, "rotationAnimation");
			compassImage.Transform = CGAffineTransform.MakeRotation((float)newRad);

		}
	}
}

