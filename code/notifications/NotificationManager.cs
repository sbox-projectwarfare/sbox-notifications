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

		private const int positionIndend = 50;

		public NotificationManager()
		{
			if ( !IsClient )
				return;

			RootPanel.StyleSheet.Load( stylePath );
			_NotificationList = new List<NotificationBase>();

			Log.Info( "Notification Library: NotificationManager Initialized" );
		}

		[Event("NotificationManager.DeleteNotification")]
		private void OnDeleteNotification(NotificationBase _Notification)
		{
			if ( _Notification == null )
			{
				Log.Error( "Notification Library: OnDeleteNotification() - Notification is null" );
				return;
			}

			_NotificationList.Remove( _Notification ); // TODO: Correct NotificationList management
			_Notification.Delete();

			Log.Info( "Notification Library: Notification deleted!" );
		}

		/// <summary>
		/// Returns an instance of needed panel from enum.
		/// Should be updated for custom panel type
		/// </summary>
		private NotificationBase GetTypeFromEnum(NotificationType type)
		{
			switch ( type )
			{
				case NotificationType.Generic: return new Generic();
				case NotificationType.Hint: return new Hint();
				case NotificationType: return new Error();
				default: { Log.Error( "Notificaiton Library: GetTypeFromEnum() - Type isn't exists!" ); return null; }
			}
		}

		[ClientRpc]
		public void ShowNotification( NotificationType type, string text )
		{
			NotificationBase newPanel = GetTypeFromEnum( type );

			newPanel.Message.Text = text;

			RootPanel.AddChild( newPanel );

			if ( _NotificationList.Count > 0 )
			{
				// FIXME: set indend from bottom coordinate
				var lastPosition = _NotificationList.Last().Box.Rect.top; // get position from last panel
				var newPosition = lastPosition + positionIndend;
				newPanel.Style.Top = newPosition; // update panel style
				newPanel.Box.Rect.top = newPosition; // just to save a new position to panel
			}

			_NotificationList.Add( newPanel );
		}
	}
}
