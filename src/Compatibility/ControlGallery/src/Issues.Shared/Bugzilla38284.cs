﻿using System;
using Microsoft.Maui.Controls.CustomAttributes;
using Microsoft.Maui.Controls.Internals;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Maps;

namespace Microsoft.Maui.Controls.Compatibility.ControlGallery.Issues
{
#if UITEST
	[NUnit.Framework.Category(Compatibility.UITests.UITestCategories.Bugzilla)]
#endif
	[Preserve(AllMembers = true)]
	[Issue(IssueTracker.Bugzilla, 38284, "when creating a map in iOS, if the map is not visible when the page is created the zoom level is offn")]
	public class Bugzilla38284 : TestContentPage // or TestFlyoutPage, etc ...
	{
		Map map1;
		Map map2;
		double Latitude = 28.032005;
		double Longitude = -81.948931;
		string LocationTitle = "Someplace Cool";
		string StreetAddress = "";

		protected override void Init()
		{
			var stack = new StackLayout();

			map1 = new Maps.Map
			{
				IsShowingUser = false,
				WidthRequest = 320,
				HeightRequest = 200
			};

			map2 = new Maps.Map
			{
				IsShowingUser = false,
				WidthRequest = 320,
				HeightRequest = 200
			};


			var btn = new Button { Text = "Show" };
			btn.Clicked += (sender, e) =>
			{
				map2.IsVisible = !map2.IsVisible;
			};

			stack.Children.Add(map1);
			stack.Children.Add(map2);
			stack.Children.Add(btn);
			DisplayMaps();
			Content = stack;
		}

		public void DisplayMaps()
		{
			map2.IsVisible = false;
			var mapPinPosition = new Location(Latitude, Longitude);

			var type = MapType.Satellite;
			map1.MapType = type;
			map2.MapType = type;
			var pin = new Pin
			{
				Type = PinType.Place,
				Location = mapPinPosition,
				Label = LocationTitle,
				Address = StreetAddress
			};
			map1.Pins.Add(pin);
			map2.Pins.Add(pin);

			// Move the map to center on the map location with the proper zoom level
			var lldegrees = 360 / (Math.Pow(2, 16));
			map1.MoveToRegion(new MapSpan(map1.Pins[0].Location, lldegrees, lldegrees));
			map2.MoveToRegion(new MapSpan(map2.Pins[0].Location, lldegrees, lldegrees));
		}
	}
}
