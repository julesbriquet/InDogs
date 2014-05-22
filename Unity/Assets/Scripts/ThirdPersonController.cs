using UnityEngine;
using System.Collections;

public class ThirdPersonController : MonoBehaviour {

    public static CharacterController CharacterController;

    // Class to handle all player movements
    public static ThirdPersonMotor TPMotor;

    Transform cameraTransform;

	// Use this for initialization
	void Awake () {
        CharacterController = gameObject.GetComponent("CharacterController") as CharacterController;
        TPMotor = gameObject.GetComponent("ThirdPersonMotor") as ThirdPersonMotor;

        cameraTransform = ThirdPersonCamera.UseExistingOrCreateMainCamera();
    }
	
	// Update is called once per frame
	void Update () {
        // Verify that a camera exist
        if (Camera.main == null)
            return;

        GetGamePadInput();
        TPMotor.UpdateMotor(cameraTransform);
	}

    void GetGamePadInput() {
             
        // Get LeftPad Input From Gamepad
        float LeftVerticalAxe = Input.GetAxis("LeftVertical");
        float LeftHorizontallAxe = Input.GetAxis("LeftHorizontal");


        // Verify that we are outside of the dead space
        TPMotor.ControlAxes = Vector3.zero;
        TPMotor.ControlAxes += new Vector3(0, 0, LeftVerticalAxe);
        TPMotor.ControlAxes += new Vector3(LeftHorizontallAxe, 0, 0);

    }
}
