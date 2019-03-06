
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// Vibration intensity for android (Look like black magic....but hey...it's working fine)
/// </summary>
public class VibrationController {

#if UNITY_ANDROID
    private static readonly AndroidJavaObject Vibrator =
    new AndroidJavaClass("com.unity3d.player.UnityPlayer")// Get the Unity Player.
    .GetStatic<AndroidJavaObject>("currentActivity")// Get the Current Activity from the Unity Player.
    .Call<AndroidJavaObject>("getSystemService", "vibrator");// Then get the Vibration Service from the Current Activity.



    public static void Vibrate(long milliseconds) {
        
        Vibrator.Call("vibrate", milliseconds);
    }

    public static void Vibrate(long[] pattern, int repeat) {
        Vibrator.Call("vibrate", pattern, repeat);
    }
#endif

#if UNITY_IOS
     public static void Vibrate(long milliseconds) {

        Handheld.Vibrate();
    }

    public static void Vibrate(long[] pattern, int repeat) {
        Handheld.Vibrate();
    }
#endif
}

