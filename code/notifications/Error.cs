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
	public class Error : NotificationBase
	{
		public Error()
        {
			Title.Text = "Something is creating error";
			Message.Delete(); // To remove an empty space under the title
        }
	}
}
