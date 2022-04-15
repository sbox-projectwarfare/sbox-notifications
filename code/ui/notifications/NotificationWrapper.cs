using Sandbox;
using Sandbox.UI;

using Warfare.Notifications;

namespace Warfare.UI.Notifications
{
    public partial class NotifcationWrapper : Panel
    {
        /// <summary>
        /// How much panels will be shown on the screen
        /// </summary>
        public const int NOTIFICATION_LIMIT = 6;

        public NotifcationWrapper()
        {
            AddClass("wrapper");
        }

        public override void Tick()
        {
            base.Tick();

            NotificationManager notificationManager = NotificationManager.Instance;

            for (int i = ChildrenCount - 1; i < NOTIFICATION_LIMIT - 1; i++)
            {
                if (notificationManager.NotificationList.Count == 0)
                {
                    break;
                }

                NotificationData notificationData = notificationManager.NotificationList[0];
                notificationManager.NotificationList.RemoveAt(0);

                BaseNotification baseNotification = Library.Create<BaseNotification>(notificationData.NotificationName);
                baseNotification.Data = notificationData;

                AddChild(baseNotification);

                baseNotification.IsDisplayed = true;
            }
        }
    }
}
