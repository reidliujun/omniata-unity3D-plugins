// ------------------------------------------------------------------------------
//  <Omniata>
//		Unity3D project example to use Omniata Android plugin
//		Omniata Android SDK version: 1.1.1
//      Target version of Android: android-21
//		created by Jun, 23-10-2014.
//  </Omniata>
// ------------------------------------------------------------------------------

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using omniata;

namespace test
{

    public class Test : MonoBehaviour
    {
		bool initialized;
		void Start()
		{
			Omniata.Log ("Start");
			initialized = false;
		}

		void OnApplicationPause(bool pause)
		{
			if (pause)
			{
				// we are in background
			}
			else
			{
				// we are in foreground again.
				if (initialized) 
				{
					// Automatically send om_load
					Omniata.TrackLoad();
				}
			}
		}

        void OnGUI()
        {

			int buttonCount = 5;
			int screenWidth = Screen.width;
			int screenHeight = Screen.height;
			
			int xSize = screenWidth / 3;
			int yMargin = Convert.ToInt32(screenHeight * 0.10);
			int ySize = (screenHeight - 2 * yMargin) / (buttonCount + (buttonCount - 1));
			
			int buttonXLeft = (screenWidth / 2) - (xSize / 2);
			
			// Make a background box
			int buttonIndex = 0;

			
			// Make the first button. If it is pressed, Omniata object will be initialized
			int buttonYTop = yMargin + (buttonIndex * ySize) + (buttonIndex * ySize);
			if (GUI.Button(new Rect(buttonXLeft, buttonYTop, xSize, ySize), "Initialize"))
			{		

				Omniata.Log ("Initialize");
				Omniata.Initialize(Omniata.api_key, Omniata.uid, Omniata.debug);
				initialized = true;
			}
			
			// Make the second button, send track load events to Omniata
			buttonIndex++;
			buttonYTop = yMargin + (buttonIndex * ySize) + (buttonIndex * ySize);
			if (GUI.Button(new Rect(buttonXLeft, buttonYTop, xSize, ySize), "om_load"))
			{
				Omniata.Log("track load");
				Omniata.TrackLoad(); 
			}
			
			// Make the third button, send track revenue events to Omniata
			buttonIndex++;
			buttonYTop = yMargin + (buttonIndex * ySize) + (buttonIndex * ySize);
			if (GUI.Button(new Rect(buttonXLeft, buttonYTop, xSize, ySize), "om_revenue "))
			{
				double total = 99.9;
				string currency_code = "EUR";
				Omniata.Log("track revenue");
				Omniata.TrackRevenue(total,currency_code);
			}

			// Make the fourth button, customed the sending events to Omniata
			buttonIndex++;
			buttonYTop = yMargin + (buttonIndex * ySize) + (buttonIndex * ySize);
			if (GUI.Button(new Rect(buttonXLeft, buttonYTop, xSize, ySize), "om_event"))
			{
				Dictionary<string, string> parameters = new Dictionary<string, string>();
				parameters.Add("app", "testapp");
				parameters.Add("attack.attacker_won", "0");
				string eventType = "testing_event_type";
				Omniata.Log("track custom event");
				Omniata.Track(eventType,parameters);
			}

			// Make the fifth button.
			/** Get the test message, only work for iOS build now
			 *  uncomment for Android build will return error
			 *  return value when debug mode is set to false, check the url here
			 *  http://api.omniata.com/channel?uid=uidtest&api_key=a514370d&channel_id=40
			 */
			buttonIndex++;
			buttonYTop = yMargin + (buttonIndex * ySize) + (buttonIndex * ySize);
			if (GUI.Button(new Rect(buttonXLeft, buttonYTop, xSize, ySize), "channel_info"))
			{
//				string message = Omniata.GetChannelMessage(40);
//				Omniata.Log (message);
			}
       }
    }
}
