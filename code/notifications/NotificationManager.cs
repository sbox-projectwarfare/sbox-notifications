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
	/// Types of all available notifications
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
		public NotificationManager()
		{
			if ( !IsClient )
				return;

			RootPanel.StyleSheet.Load( "/notifications/styles/NotificationsStyle.scss" );

			Log.Info( "Notifications Library: NotificationManager Initialized" );
		}

		private int notificationCounter = 0;

		[ClientRpc]
		public void show_notification(NotificationType type, string text)
        {
			notificationCounter++;

			if ( type == NotificationType.Generic )
			{
				Log.Info( "Notifications Library (Generic): TODO: Show generic notification!" );
			}
			else if ( type == NotificationType.Hint )
			{
				Log.Info( "Notifications Library (Hint): TODO: Show hint notification!" );
			}
			else if ( type == NotificationType.Error )
			{
				Log.Info( "Notifications Library (Error): Activating Error notification '" + text + "'..." );

				var error = RootPanel.AddChild<Error>();
				error.Title.Text = text;

				//if ( notificationCounter > 1 )
				//{
				//	// TODO: Change position
				//}

				error.show();

				//notificationCounter--;

				//error.Delete();
				Log.Info( "Notifications Library (Error): End of Error notification" );
			}
			else
			{
				notificationCounter -= notificationCounter > 0 ? 1 : 0;
			}
		}
	}
}
