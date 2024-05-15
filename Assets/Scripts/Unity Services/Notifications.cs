using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif
#if UNITY_IOS
using Unity.Notifications.iOS;
#endif

public class Notifications
{

    private const string channel_id = "channel_id";
    public void ScheduleNotification(DateTime dateTime)
    {
#if UNITY_EDITOR
        Debug.Log("Notification scheduled for: " + dateTime.ToString());
#endif
#if UNITY_ANDROID
        AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel()
        {
            Id = channel_id,
            Name = "Channel Name",
            Importance = Importance.High,
            Description = "Channel Descripton"
        };
        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);
        //print("register not");
        AndroidNotification notification = new AndroidNotification()
        {
            Title = "Nice Fight!!",
            Text = "iyisin",
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = dateTime
        };

        AndroidNotificationCenter.SendNotification(notification, channel_id);
        //print("send not");
#endif
#if UNITY_IOS
iOSNotification notification = new iOSNotification()
        {
            Title = "Nice Fight!!",
            Body = "iyisin",
            ShowInForeground = true,
            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
            CategoryIdentifier = "category_a",
            ThreadIdentifier = "thread1",
            Trigger = new iOSNotificationTimeIntervalTrigger()
            {
                TimeInterval = new TimeSpan(0, 0, 5),
                Repeats = false
            }
            
        };
        iOSNotificationCenter.ScheduleNotification(notification);
    
#endif

    }



}