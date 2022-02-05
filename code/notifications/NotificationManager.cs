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
		private const string stylePath = "/notifications/styles/NotificationsStyle.scss";
		private List<NotificationBase> _NotificationList = null;

		private const int positionIndend = 20;

		public NotificationManager()
		{
			if ( !IsClient )
				return;

			RootPanel.StyleSheet.Load( stylePath );

			_NotificationList = new List<NotificationBase>();

			Log.Info( "Notification Library: Client NotificationManager Initialized" );
		}

		[Event("NotificationManager.DeleteNotification")]
		private void OnDeleteNotification(NotificationBase _Notification)
		{
			if ( _Notification == null )
			{
				Log.Error( "Notification Library: OnDeleteNotification() - Notification is null" );
				return;
			}

			foreach ( NotificationBase notificationFromList in _NotificationList )
			{
				if ( _Notification.GetHashCode() == notificationFromList.GetHashCode() ) // if notification in list equal to notification we need
				{
					Log.Info( "Notification Library: Deleting notification..." );
					
					_NotificationList.Remove( notificationFromList ); // delete it from list and itself 
					_Notification.Delete(); // TODO: Correct NotificationList management
					
					Log.Info( "Notification Library: Notification deleted!" );
					
					return;
				}
			}
		}

		/// <summary>
		/// Returns an instance of needed panel from enum.
		/// Should be updated for custom panel type
		/// </summary>
		private NotificationBase GetTypeFromEnum(NotificationType type)
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

			RootPanel.AddChild( NewPanel );

			if ( _NotificationList.Count > 0 )
			{
				var lastPosition = _NotificationList.Last().Box.Rect.bottom; // get position from last panel
				var newPosition = _NotificationList.Last().ScaleFromScreen  * ( lastPosition + positionIndend );
				
				NewPanel.Style.Top = newPosition; // update panel style
				NewPanel.Box.Rect.top = newPosition;
			}

			_NotificationList.Add( NewPanel );
		}
	}
}
