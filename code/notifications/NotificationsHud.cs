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
			public Label Title;
			public Label Message;
			public Label NotificationShape;

			public NotificationBase()
            {
				NotificationShape = Add.Label(" ", "shape");
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
