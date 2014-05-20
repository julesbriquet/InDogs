using UnityEngine;
using System.Collections;

public class ThirdPersonController : MonoBehaviour {

    public static CharacterController CharacterController;
    //public static ThirdPersonController Instance;
    public static ThirdPersonMotor TPMotor;

	// Use this for initialization
	void Awake () {
        CharacterController = gameObject.GetComponent("CharacterController") as CharacterController;
        TPMotor = gameObject.GetComponent("ThirdPersonMotor") as ThirdPersonMotor;

        ThirdPersonCamera.UseExistingOrCreateMainCamera();
    }
	
	// Update is called once per frame
	void Update () {
        // Verify that a camera exist
        if (Camera.main == null)
            return;

        GetGamePadInput();
        TPMotor.UpdateMotor();
	}

    void GetGamePadInput() {

        TPMotor.MoveVector = Vector3.zero;
     
        // Get LeftPad Input From Gamepad
        float LeftVerticalAxe = Input.GetAxis("LeftVertical");
        float LeftHorizontallAxe = Input.GetAxis("LeftHorizontal");

        Debug.Log("Left Vetical Axe: " + LeftVerticalAxe);
        Debug.Log("Left Horizontal Axe: " + LeftHorizontallAxe);

        // Verify that we are outside of the dead space
        TPMotor.MoveVector += new Vector3(0, 0, LeftVerticalAxe);
        TPMotor.MoveVector += new Vector3(LeftHorizontallAxe, 0, 0);

    }
}
