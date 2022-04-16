using Sandbox;
using Sandbox.UI;

using Warfare.Notifications;

namespace Warfare.UI.Notifications
{
    public partial class NotificationWrapper : Panel
    {
        /// <summary>
        /// How much panels will be shown on the screen
        /// </summary>
        public const int NOTIFICATION_LIMIT = 6;

        public NotificationWrapper()
        {
            AddClass("notification-wrapper");
        }

        public override void Tick()
        {
            base.Tick();

            NotificationStack notificationStack = NotificationStack.Instance;

            for (int i = ChildrenCount - 1; i < NOTIFICATION_LIMIT - 1; i++)
            {
                if (notificationStack.Count == 0)
                {
                    break;
                }

                NotificationData notificationData = notificationStack.Pop();

                Notification baseNotification = Library.Create<Notification>(notificationData.NotificationName);
                baseNotification.Data = notificationData;

                AddChild(baseNotification);

                baseNotification.IsDisplayed = true;
            }
        }
    }
}
