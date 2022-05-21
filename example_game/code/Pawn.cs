using Sandbox;

using Warfare.Notifications;

namespace Warfare
{
    public partial class Pawn : AnimatedEntity
    {
        public static Model PlayerModel { get; set; } = Model.Load("models/sbox_props/watermelon/watermelon.vmdl");

        /// <summary>
        /// Called when the entity is first created
        /// </summary>
        ///
        public override void Spawn()
        {
            base.Spawn();

            Model = PlayerModel;
            EnableDrawing = true;
            EnableHideInFirstPerson = true;
            EnableShadowInFirstPerson = true;
        }

        /// <summary>
        /// Called every tick, clientside and serverside.
        /// </summary>
        public override void Simulate(Client cl)
        {
            base.Simulate(cl);

            if (IsServer)
            {
                using (Prediction.Off())
                {
                    if (Input.Released(InputButton.Slot1))
                    {
                        Log.Info("Pressed Slot 1 key on server");

                        NotificationStack.Push(new NotificationData()
                        {
                            NotificationName = TypeLibrary.GetDescription(typeof(UI.Notifications.GenericNotification)).Name,
                            Message = "Notification created by player on server!"
                        });
                    }
                }

                return;
            }

            if (Input.Released(InputButton.Slot2))
            {
                Log.Info("Pressed Slot 2 key on client");
                NotificationStack.Push(new NotificationData()
                {
                    NotificationName = TypeLibrary.GetDescription(typeof(UI.Notifications.HintNotification)).Name,
                    Title = "HINT",
                    Message = "Hint created by player on client!"
                });
            }

            if (Input.Released(InputButton.Slot3))
            {
                Log.Info("Pressed Slot 3 key");
                NotificationStack.Push(new NotificationData()
                {
                    NotificationName = TypeLibrary.GetDescription(typeof(UI.Notifications.ErrorNotification)).Name,
                    Title = "OMG! New Notification!!!",
                    Message = "Custom notification created by player on client!"
                });
            }
        }
    }
}
