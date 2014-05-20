using UnityEngine;
using System.Collections;

/*
 * Fonctionalities:
 *      Primary: Process character motion
 *      Secondary: Rotate character to match camera rotation
 */
public class ThirdPersonMotor : MonoBehaviour {

    public float MoveSpeed = 10f;
    public Vector3 MoveVector { get; set; }

	// Use this for initialization
	void Start () 
    {
        MoveVector = Vector3.zero;
	}
	
	// Update is called once per frame
	public void UpdateMotor () {
        //AlignCharacterWithCamera();
        ProcessMotion();
	}

    void AlignCharacterWithCamera() {
        // If character moving
        if (MoveVector.x != 0 || MoveVector.z != 0)
        {
            // Align the rotation of the character to the main camera
            transform.rotation = Quaternion.Euler(this.transform.eulerAngles.x,
                                                  Camera.main.transform.eulerAngles.y,
                                                  this.transform.eulerAngles.z);
        }
    }

    void ProcessMotion() { 
        // Transform MoveVector to World Space
        MoveVector = transform.TransformDirection(MoveVector);

        // Normalize MoveVector if Magnitude > 1 (to limit the MoveVector)
        if (MoveVector.magnitude > 1)
            MoveVector = Vector3.Normalize(MoveVector);

        MoveVector *= MoveSpeed;

        // Multiply MoveVector with the DeltaTimeUpdate (Avoid speed depending on processor speed)
        MoveVector *= Time.deltaTime;

        ThirdPersonController.CharacterController.Move(MoveVector);

    }
}
