using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RDG;

public class VibrationMethods : MonoBehaviour
{
    private void Start()
    {
        
    }

    public static void ShortLowVibration()
    {
        Vibration.Vibrate(40, 50, true);
        //Handheld.Vibrate();
    }

    public void ButtonVibrate()
    {
        Vibration.Vibrate(40, 50, true);
        //Vibration.Vibrate(50, 80, true);
        //Handheld.Vibrate();
    }

    public void ToolVibration()
    {
        Vibration.Vibrate(40, 50, true);
        //Vibration.Vibrate(300, 150, true);
        //Handheld.Vibrate();
    }
}
