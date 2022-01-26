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
		}

		[ClientRpc]
		public void ShowNotification(NotificationType type, string text)
        {
			if ( type == NotificationType.Generic )
			{
				Log.Info( "Notifications Library: TODO: Show generic notification!" );
			}
			else if ( type == NotificationType.Hint )
			{
				Log.Info( "Notifications Library: TODO: Show hint notification!" );
			}
			else if ( type == NotificationType.Error )
			{
				Log.Info( "Notifications Library: Activating Error notification '" + text + "'..." );

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
