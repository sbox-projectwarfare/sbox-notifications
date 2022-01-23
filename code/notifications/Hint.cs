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
	public class Hint : NotificationsHud.NotificationBase
	{
		public Hint()
		{
			Title.Text = "HINT:";
			Message.Text = "This is a hint message that must notify you about something!";
		}
	}
}
