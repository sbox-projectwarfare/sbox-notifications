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
		private List<NotificationBase> _NotificationList = null;

		public NotificationManager()
		{
			if ( !IsClient )
				return;

			RootPanel.StyleSheet.Load( "/notifications/styles/NotificationsStyle.scss" );

			_NotificationList = new List<NotificationBase>();

			Log.Info( "Notifications Library: NotificationManager Initialized" );
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

			_Notification.SetClass( "unactive", true );
			_NotificationList.Remove( _Notification ); // TODO: NotificationList management
			_Notification.Delete();

			Log.Info( "Notification Library: Notification deleted!" );
		}

		[ClientRpc]
		public void ShowNotification(NotificationType type, string text)
        {
			if ( type == NotificationType.Generic )
			{
				Log.Warning( "Notification Library: TODO: Show generic notification!" );
			}
			else if ( type == NotificationType.Hint )
			{
				Log.Warning( "Notification Library: TODO: Show hint notification!" );
			}
			else if ( type == NotificationType.Error )
			{
				Log.Info( "Notification Library: Activating Error notification '" + text + "'..." );

				var m_Error = new Error();
				m_Error.Title.Text = text;

				// TODO: it should be something like event manager
				// TODO: update panel position if element count in list more than 1

				_NotificationList.Add( m_Error );
				RootPanel.AddChild( m_Error );

			}
		}
	}
}
