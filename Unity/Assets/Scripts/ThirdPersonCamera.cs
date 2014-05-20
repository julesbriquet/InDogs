using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

    // All data position of what the camera will look at
    Transform TargetLookAt;

    /* 
     * Distance Handling
     */
    // Handling distances to Target variables
    public float StartDistanceToTarget = 5f;
    public float DistanceMinToTarget = 3f;
    public float DistanceMaxToTarget = 10f;

    // Current distance to target
    private float DistanceToTarget = 5f;
    // Ideal distance to target
    private float DesiredDistance = 0f;

    /*
     * Position Handling
     */
    // Get directions from the input
    Vector2 DesiredDirection = Vector2.zero;
    
    private Vector3 DesiredPostion = Vector3.zero;

    // Camera sensitivity
    public float XMoveSensitivity = 1f;
    public float YMoveSensitivity = 1f;

    // To Avoid weird angle with the camera
    public float YMinLimit = -40f;
    public float YMaxLimit = 80f;



    /* 
     * Smoothing Part
     * Smooth for camera moves
     */

    // Smooth distance changes
    public float DistanceSmooth = 0.1f;

    // Smooth rotation of the camera
    private float YSmooth = 0.2f;
    private float XSmooth = 0.1f;

    // Velocity for distance (always equals to 0 as it's modified by SmoothDamp func)
    private float velocityDistance = 0f;
    private float XVel = 0f;
    private float YVel = 0f;
    private float ZVel = 0f;


    // Use this for initialization
    void Start () 
    {
        DistanceToTarget = Mathf.Clamp(DistanceToTarget, DistanceMinToTarget, DistanceMaxToTarget);
        DesiredDistance = StartDistanceToTarget;
    }
	
	// Update is called once per frame
	void LateUpdate () 
    {
		if (TargetLookAt == null)
			return;

        HandlePlayerInput();
        CalculateDesiredPosition(DesiredDirection);
        UpdatePosition();

	}

    void UpdatePosition()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, DesiredPostion.x, ref XVel, XSmooth);
        float posY = Mathf.SmoothDamp(transform.position.y, DesiredPostion.y, ref YVel, YSmooth);
        float posZ = Mathf.SmoothDamp(transform.position.z, DesiredPostion.z, ref ZVel, XSmooth);

        transform.position = new Vector3(posX, posY, posZ);
        transform.LookAt(TargetLookAt);
    }

    void HandlePlayerInput ()
    {
        // Check the move of the mouse or the pad.
        float RightHorizontalAxe = Input.GetAxis("RightHorizontal") * XMoveSensitivity;
        float RightVerticalAxe = Input.GetAxis("RightVertical") * YMoveSensitivity;

        Debug.Log("Right Vetical Axe: " + RightVerticalAxe);
        Debug.Log("Right Horizontal Axe: " + RightHorizontalAxe);

        DesiredDirection += new Vector2(RightHorizontalAxe, RightVerticalAxe);
        
        // Clamp the Y rotation
        DesiredDirection.y = ThirdPersonCameraHelper.ClampAngle(DesiredDirection.y, YMinLimit, YMaxLimit);

    }

    void CalculateDesiredPosition (Vector2 inputMovements)
    {
        // Evaluate our distance
        DistanceToTarget = Mathf.SmoothDamp(DistanceToTarget, DesiredDistance, ref velocityDistance, DistanceSmooth);

        // Get our desired position
        DesiredPostion = CalculatePosition(inputMovements.y, inputMovements.x, DistanceToTarget);
    }

    Vector3 CalculatePosition (float rotationX, float rotationY, float distance)
    {
        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);

        return TargetLookAt.position + rotation * direction;
    }

    public void Reset() 
    {

        DistanceToTarget = StartDistanceToTarget;
        DesiredDistance = DistanceToTarget;
    }

    public static void UseExistingOrCreateMainCamera () 
    {

        GameObject tempCamera;
        // Target that the camera will look (usually mainCharacter)
        GameObject targetLookAt;
        ThirdPersonCamera tpCamera;

        // If the main camera exist we take that one
        if (Camera.main != null)
        {
            tempCamera = Camera.main.gameObject;
            
        }
        // Else we create a new one
        else
        {
            tempCamera = new GameObject("Main Camera");
            tempCamera.AddComponent("Camera");
            tempCamera.tag = "MainCamera";
        }

        tpCamera = tempCamera.GetComponent("ThirdPersonCamera") as ThirdPersonCamera;

        // Attach the script to that camera if not found
        if (tpCamera == null)
        {
            tempCamera.AddComponent("ThirdPersonCamera");
            tpCamera = tempCamera.GetComponent("ThirdPersonCamera") as ThirdPersonCamera;
        }

        targetLookAt = GameObject.FindGameObjectWithTag("targetLookAt");

        if (targetLookAt == null)
        {
            Debug.LogError("Error: Target To Look At Not Found.");
            targetLookAt = new GameObject("targetLookAt");
            targetLookAt.transform.position = Vector3.zero;
        }

        tpCamera.TargetLookAt = targetLookAt.transform;
    }
}
