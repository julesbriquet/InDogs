using UnityEngine;
using System.Collections;

/*
 * Fonctionalities:
 *      Primary: Process character motion
 *      Secondary: Rotate character to match camera rotation
 */
public class ThirdPersonMotor : MonoBehaviour {



    public float WalkSpeed = 10f;
    public float TurnSmooth = 15f;
    public float SpeedSmoothing = 5f;

    private float MoveSpeed = 0.0f;

    public float RotateSpeed = 500f;

    public Vector3 ControlAxes = Vector3.zero;

    public Vector3 MoveDirection { get; set; }

	// Use this for initialization
	void Start () 
    {
        MoveDirection = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
    public void UpdateMotor(Transform cameraTransform)
    {
        AlignCharacterWithCamera(cameraTransform);
        ProcessMotion(cameraTransform);
	}

    void AlignCharacterWithCamera(Transform cameraTransform) {

        

    }

    void ProcessMotion(Transform cameraTransform)
    {

        
        // Forward vector relative to the camera along the x-z plane    
        Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
        forward.y = 0;
        forward = forward.normalized;

        // Right vector relative to the camera
        // Always orthogonal to the forward vector
        Vector3 right = new Vector3(forward.z, 0, -forward.x);

        Vector3 targetDirection = ControlAxes.x * right + ControlAxes.z * forward;

        //MoveDirection = transform.TransformDirection(MoveDirection);

        // Normalize MoveVector if Magnitude > 1 (to limit the MoveVector)
        if (MoveDirection.magnitude > 1)
            MoveDirection = MoveDirection.normalized;

        MoveDirection = Vector3.RotateTowards(MoveDirection, targetDirection, RotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000);
        MoveDirection = MoveDirection.normalized;



        // Smooth the speed based on the current target direction
        float curSpeedSmooth = SpeedSmoothing * Time.deltaTime;
        float targetSpeed = Mathf.Min(targetDirection.magnitude, 1.0f);
        targetSpeed *= WalkSpeed;

        MoveSpeed = Mathf.Lerp(MoveSpeed, targetSpeed, curSpeedSmooth);

        if (Debug.isDebugBuild)
        {
            GUITest.cameraDirection = right + forward;
            GUITest.MoveDirection = MoveDirection;
            GUITest.targetDirection = targetDirection;
        }

        
        Vector3 Movements = MoveDirection * MoveSpeed;

        // Multiply MoveVector with the DeltaTimeUpdate (Avoid speed depending on processor speed)
        Movements *= Time.deltaTime;

        GUITest.MoveDirection = Movements;
        ThirdPersonController.CharacterController.Move(Movements);
        

        ThirdPersonController.CharacterController.transform.rotation = Quaternion.LookRotation(MoveDirection);
    }
}
