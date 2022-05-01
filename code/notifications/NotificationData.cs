using System;
using System.IO;

using Sandbox;

using ProjectWarfare.UI.Notifications;

namespace ProjectWarfare.Notifications
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class NotificationDataAttribute : LibraryAttribute
    {
        public Type NotificationType { get; set; }

        public NotificationDataAttribute(string name, Type notificationType) : base("pw_notificationdata_" + name)
        {
            NotificationType = notificationType;
        }
    }

    [NotificationData("base", typeof(Notification)), Hammer.Skip]
    public partial class NotificationData
    {
        public string Title { get; set; }

        public string Message { get; set; }

        public string Name { get; set; }

        public string NotificationName { get; set; }

        public NotificationData()
        {
            LibraryAttribute libraryAttribute = Library.GetAttribute(GetType());

            Name = libraryAttribute.Name;

            if (libraryAttribute is NotificationDataAttribute notificationDataAttribute)
            {
                LibraryAttribute attribute = Library.GetAttribute(notificationDataAttribute.NotificationType);

                if (attribute is NotificationAttribute)
                {
                    NotificationName = attribute.Name;
                }
            }
        }

        protected virtual void WriteData(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(Name);
            binaryWriter.Write(NotificationName);
            binaryWriter.Write(Title ?? "");
            binaryWriter.Write(Message ?? "");
        }

        public byte[] Write()
        {
            using MemoryStream memoryStream = new();

            using (BinaryWriter binaryWriter = new(memoryStream))
            {
                WriteData(binaryWriter);
            }

            memoryStream.Flush();

            return memoryStream.GetBuffer();
        }

        protected virtual void ReadData(BinaryReader binaryReader)
        {
            Name = binaryReader.ReadString();
            NotificationName = binaryReader.ReadString();

            Title = binaryReader.ReadString();

            if (string.IsNullOrEmpty(Title))
            {
                Title = null;
            }

            Message = binaryReader.ReadString();

            if (string.IsNullOrEmpty(Message))
            {
                Message = null;
            }
        }

        public void Read(byte[] bytes)
        {
            using MemoryStream memoryStream = new(bytes);
            using BinaryReader binaryReader = new(memoryStream);

            ReadData(binaryReader);
        }
    }
}
