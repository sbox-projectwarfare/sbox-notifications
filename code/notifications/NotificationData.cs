using System;
using System.IO;

using Sandbox;

using ProjectWarfare.UI.Notifications;

namespace ProjectWarfare.Notifications
{
    public partial class NotificationData : LibraryClass
    {
        public string Title { get; set; }

        public string Message { get; set; }

        public string Name { get; private set; }

        public string NotificationName { get; set; }

        public virtual Type NotificationType => typeof(Notification);

        public NotificationData() : base()
        {
            Name = Library.GetAttribute(GetType()).Name;
            NotificationName = Library.GetAttribute(NotificationType).Name;
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
