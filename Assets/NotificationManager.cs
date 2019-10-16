using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;

public class NotificationManager : MonoBehaviour
{

	AndroidNotificationChannel defaultChannel;

	int id;

    // Start is called before the first frame update
    void Start()
    {

		AndroidNotificationCenter.CancelAllNotifications();

		defaultChannel = new AndroidNotificationChannel()
		{
			Id = "default_channel",
			Name = "Default Channel",
			Description = "Generic Notification",
			Importance = Importance.Default,
		};

		AndroidNotificationCenter.RegisterNotificationChannel(defaultChannel);

		AndroidNotification notification = new AndroidNotification()
		{
			Title = "Notificação teste",
			Text = "Descrição de teste",
			SmallIcon = "small",
			LargeIcon = "large",
			FireTime = System.DateTime.Now.AddSeconds(30),
		};

		id = AndroidNotificationCenter.SendNotification(notification, defaultChannel.Id);



	}

	public void CheckNotification()
	{
		if(AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
		{
			AndroidNotification newNotification = new AndroidNotification()
			{
				Title = "Morreu!",
				Text = "Você morreu",
				SmallIcon = "small",
				LargeIcon = "large",
				FireTime = System.DateTime.Now.AddSeconds(5),
			};

			AndroidNotificationCenter.CancelNotification(id);
			AndroidNotificationCenter.SendNotification(newNotification, defaultChannel.Id);

		}
		else if(AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Delivered)
		{
			AndroidNotificationCenter.CancelNotification(id);
		}
		else if(AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Unknown)
		{
			AndroidNotification notification = new AndroidNotification()
			{
				Title = "Nova notificação",
				Text = "Reenvio de notificação",
				SmallIcon = "small",
				LargeIcon = "large",
				FireTime = System.DateTime.Now.AddSeconds(30),
			};

			id = AndroidNotificationCenter.SendNotification(notification, defaultChannel.Id);
		}
	}
    
}
