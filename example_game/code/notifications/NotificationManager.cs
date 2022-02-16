/*
 * Notification manager script code is under MIT License
 * 
 * Copyright (c) 2022 s&box MilitaryRP
 * Author: Val Zubko (5FB5)
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 * 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;
using Sandbox.UI;

namespace Notifications
{

	/// <summary>
	/// Types of available notifications
	/// </summary>
	public enum NotificationType
	{
		Generic,
		Hint,
		Error
	}

	/// <summary>
	/// Main class for working with notifications
	/// </summary>
	[Library]
	public partial class NotificationManager : HudEntity<RootPanel>
	{
		private const string stylePath = "/notifications/styles/NotificationsStyle.scss";
		private List<NotificationBase> NotificationList = null;

		/// <summary>
		/// How much panels will be shown on the screen
		/// </summary>
		private const int notificationLimit = 10;

		/// <summary>
		/// Indend between panels
		/// </summary>
		private const int positionIndend = -30;

		public NotificationManager()
		{
			if ( !IsClient )
				return;

			RootPanel.StyleSheet.Load( stylePath );

			NotificationList = new List<NotificationBase>();

			Log.Info( "Notification Library: Client NotificationManager Initialized" );
		}

		// TODO: FIFO stuff
		private void CheckList()
		{
			Log.Warning( "Notification Library: TODO: CheckList()" );
			return;
		}

		[Event( "NotificationManager.DeleteNotification" )]
		private void OnDeleteNotification( NotificationBase _Notification )
		{
			if ( _Notification == null )
			{
				Log.Error( "Notification Library: OnDeleteNotification() - Notification is null" );
				return;
			}

			foreach ( NotificationBase notificationFromList in NotificationList )
			{
				Log.Info( "Notification Library: Deleting notification..." );

				NotificationList.Remove( notificationFromList ); // delete it from list and itself 
				_Notification.Delete();

				Log.Info( "Notification Library: Notification deleted!" );

				CheckList();
				return;

			}
		}

		/// <summary>
		/// Returns an instance of panel from enum.
		/// Should be updated for custom panel type
		/// </summary>
		private NotificationBase GetTypeFromEnum( NotificationType type )
		{
			switch ( type )
			{
				case NotificationType.Generic: return new Notifications.Generic();
				case NotificationType.Hint: return new Notifications.Hint();
				case NotificationType.Error: return new Notifications.Error();
				default: { Log.Error( "Notificaiton Library: GetTypeFromEnum() - Type isn't exists! (" + type + ")" ); return null; }
			}
		} 

		[ClientRpc]
		public void ShowNotification( NotificationType type, string text )
		{
			NotificationBase NewPanel = GetTypeFromEnum( type );

			if ( NewPanel == null )
			{
				Log.Error( "Notification Library: ShowNotification() - Notification based on " + type + " type is null!" );
				return;
			}

			NewPanel.Message.Text = text;

			// If count of panels isn't more than limit, we add it to screen
			// in other case we just add it to the list, so we can draw the panel later
			if ( NotificationList.Count <= notificationLimit - 1 ) // -1 because it counting from 0
			{
				RootPanel.AddChild( NewPanel );

				// If there is more than 1 panel on the screen, perform repositioning
				if ( NotificationList.Count > 0 )
				{
					var lastPosition = NotificationList.Last().Box.Rect.bottom; // get position from last panel
					var newPosition = NotificationList.Last().ScaleFromScreen * ( lastPosition + positionIndend );

					NewPanel.Style.Top = newPosition; // update panel style
					NewPanel.Box.Rect.bottom = newPosition; // save value for extracting it in the next call
				}

				NotificationList.Add( NewPanel );
			}
			//else if ( NotificationList.Count > notificationLimit )
			//{
			//	Log.Info( "Notification Library: Count of panels is more than limit. Notification added to queue" );
			//	NotificationList.Add( NewPanel );
			//}
		}
	}
}
