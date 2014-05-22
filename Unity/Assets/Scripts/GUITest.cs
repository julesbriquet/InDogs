using UnityEngine;
using System.Collections;

public class GUITest : MonoBehaviour
{

    public static Vector3 MoveDirection;
    public static Vector3 cameraDirection;
    public static Vector3 targetDirection;


    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 175, 50), "targetDirection: " + targetDirection))
        {
            print("You clicked the button!");
        }

        if (GUI.Button(new Rect(10, 70, 175, 50), "CamTrans: " + cameraDirection))
        {
            print("You clicked the button!");
        }

        if (GUI.Button(new Rect(10, 120, 175, 50), "MovDir: " + MoveDirection))
        {
            print("You clicked the button!");
        }
    }
}
