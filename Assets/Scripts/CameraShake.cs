using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FirstGearGames.SmoothCameraShaker;

public class CameraShake : MonoBehaviour
{
    public ShakeData MyShake;
    public void Shake()
    {
        CameraShakerHandler.Shake(MyShake);
    }
}
