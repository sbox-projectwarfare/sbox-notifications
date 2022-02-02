using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Notifications {

	/// <summary>
	/// Base class of notification panel
	/// </summary>
	public class NotificationBase : Panel
	{
		/// <summary>
		/// How long notification will active
		/// by default notification will shown for 6 seconds
		/// </summary>
		private float showTime = 4.7f;

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
		
		public NotificationBase()
		{
			this.Style.Dirty();
			this.NotificationShape = Add.Label( " ", "shape" );
			this.Title = Add.Label( "Notification Title", "title" );
			this.Message = Add.Label( "Notification text here", "message" );
		}

		public override void Tick()
		{
			base.Tick();

			if ( showTime > 0 )
			{
				showTime -= Sandbox.Time.Delta;
			}
			else
			{
				Sandbox.Event.Run( "NotificationManager.DeleteNotification", this );
			}
		}
	}
}
