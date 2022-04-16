namespace Warfare.Notifications
{
    [NotificationData("error", typeof(UI.Notifications.ErrorNotification)), Hammer.Skip]
    public partial class ErrorNotificationData : NotificationData
    {
        public ErrorNotificationData() : base()
        {
            Title = "Error";
            Message = "Something is creating error";
        }
    }
}
