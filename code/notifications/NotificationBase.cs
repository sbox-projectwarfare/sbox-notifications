using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Notifications {

	/// <summary>
	/// Base class of notification panel
	/// </summary>
	public class NotificationBase : Panel
	{
		/// <summary>
		/// Title for your notification
		/// </summary>
		public Label Title;

		/// <summary>
		/// Notification message under the title
		/// </summary>
		public Label Message;

		// Just to draw a UI shape in left from text
		private Label NotificationShape;

		// How long notification will active
		// by default notification will shown for 4.7 seconds
		private const float showTime = 4.7f;
		private uint timeStamp = (uint)Sandbox.Time.Tick;

		public NotificationBase()
		{
			NotificationShape = Add.Label( " ", "shape" );
			Title = Add.Label( "Notification Title", "title" );
			Message = Add.Label( "Notification text here", "message" );
		}

		public void show()
		{
			SetClass( "active", true );
			//while ( Sandbox.Time.Now - timeStamp <= showTime )
			//{
			//	SetClass( "active", true );
			//	timeStamp = (uint)Sandbox.Time.Tick; // update ticks
			//}

			//// When time is over we tell to scss set class "unable" to hide notification
			//SetClass( "unactive", true );
		}
	}
}
