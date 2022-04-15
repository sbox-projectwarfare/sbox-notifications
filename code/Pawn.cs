using Sandbox;

using Warfare.Notifications;

namespace Warfare
{
	partial class Pawn : AnimEntity
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

			if (!IsClient)
			{
				return;
			}

			NotificationManager notificationManager = (Game.Current as MyGame).NotificationManager;

			if (Input.Released(InputButton.Slot1))
			{
				Log.Info("Pressed Slot 1 key");
				notificationManager.ShowNotification(NotificationType.Generic, "Notification created by player!");
			}

			if (Input.Released(InputButton.Slot2))
			{
				Log.Info("Pressed Slot 2 key");
				notificationManager.ShowNotification(NotificationType.Hint, "Hint created by player!");
			}

			if (Input.Released(InputButton.Slot3))
			{
				Log.Info("Pressed Slot 3 key");
				notificationManager.ShowNotification(NotificationType.Error, "Error created by player!");
			}
		}

		/// <summary>
		/// Called every frame on the client
		/// </summary>
		public override void FrameSimulate(Client cl)
		{
			base.FrameSimulate(cl);
		}
	}
}
