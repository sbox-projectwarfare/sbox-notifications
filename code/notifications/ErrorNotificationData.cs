using System;

namespace ProjectWarfare.Notifications
{
    public partial class ErrorNotificationData : NotificationData
    {
        public override Type NotificationType => typeof(UI.Notifications.ErrorNotification);

        public ErrorNotificationData() : base()
        {
            Title = "Error";
            Message = "Something is creating error";
        }
    }
}
