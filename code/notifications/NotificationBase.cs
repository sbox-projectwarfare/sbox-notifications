using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Notifications
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
}
