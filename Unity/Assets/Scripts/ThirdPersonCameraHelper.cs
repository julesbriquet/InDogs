using UnityEngine;
using System.Collections;

public static class ThirdPersonCameraHelper {

    public static float ClampAngle(float angle, float min, float max)
    {
        Debug.Log("angle:" + angle);

        do
        {
            if (angle < -360)
                angle = -360;
            if (angle > 360)
                angle = 360;

        } while (angle < -360 || angle > 360);

        return Mathf.Clamp(angle, min, max);
    }
}
