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

		private const int positionIndent = 100;

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

			_NotificationList.Remove( _Notification ); // TODO: NotificationList management
			_Notification.Delete();

			Log.Info( "Notification Library: Notification deleted!" );
		}

		[ClientRpc]
		public void ShowNotification( NotificationType type, string text )
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

				RootPanel.AddChild( m_Error );

				if ( _NotificationList.Count > 0 )
				{
					var lastPosition = _NotificationList.Last().Box.Rect.top; // get position from last panel
					var newPosition = lastPosition + positionIndent;
					m_Error.Style.Top = newPosition; // update panel style
					m_Error.Box.Rect.top = newPosition; // just to save a new position to panel
				}
				_NotificationList.Add( m_Error );
			}
		}
	}
}
