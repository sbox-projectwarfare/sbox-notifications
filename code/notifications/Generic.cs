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
	public class Generic : NotificationBase
	{
		public Generic()
		{
			Title.Text = "Notification";
			Message.Text = "Test notification message\nAnd of course it should support a transition in a new line";
		}
	}
}
