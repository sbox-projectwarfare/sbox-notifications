using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications
{
	public class Error : NotificationBase
	{
		public Error()
		{
			Title.Delete();
			Message.Text = "Something is creating error";
		}
	}
}
