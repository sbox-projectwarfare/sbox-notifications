using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Notifications
{
	[Library]
	public partial class NotificationsHud : HudEntity<RootPanel>
	{
		public class NotificationBase : Panel
		{
			// Title of your notification
			public Label Title;

			// Notification's message
			public Label Message;

			// Just to draw a UI shape in left from text
			private Label NotificationShape;

			public NotificationBase()
			{
				NotificationShape = Add.Label( " ", "shape" );
				Title = Add.Label( "Notification Title", "title" );
				Message = Add.Label( "Notification text here", "message" );
			}
		}

		public NotificationsHud()
		{
			if (!IsClient)
				return;

			RootPanel.StyleSheet.Load("/notifications/styles/NotificationsStyle.scss");

			RootPanel.AddChild<Generic>();
			//RootPanel.AddChild<Hint>();
			//RootPanel.AddChild<Error>();
		}
	}
}
