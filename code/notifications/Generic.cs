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
	public class Generic : NotificationsHud.NotificationBase
    {
		public Generic()
        {
			Title = Add.Label("Notification", "title");
			Message = Add.Label("Test notification message\nAnd of course it should support a transition in a new line", "message");
        }
    }
}
