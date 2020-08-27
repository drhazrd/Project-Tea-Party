 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camCtrl : MonoBehaviour {
	public Transform target;
	public Vector3 offset;
    public Vector3 offsetPos;
    public bool useOffsetValues;
	public float rotationSpeed;
	public Transform pivot;
	public float maxViewAngle;
	public float minViewAngle;
	public bool invertY;
    public bool usingMouseCam = true;
    public bool usingFixedCam;

    public Animator anim;
	// Use this for initialization
	void Start () {
		if (!useOffsetValues){
		offset = target.position - new Vector3(transform.position.x, -transform.position.y, transform.position.z);
		}
		Cursor.lockState = CursorLockMode.Locked;
		pivot.transform.position = target.transform.position;
		//pivot.transform.parent = target.transform;
		pivot.transform.parent = null;
        //Debug.Log(this.name);
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (!usingFixedCam && usingMouseCam) {
            MousesControls();
            usingFixedCam = false;
            usingMouseCam = true;
            Debug.Log("Full Motion Camera!");

        }
        else if (!usingMouseCam && usingFixedCam){
            FixedCameraLock();

            usingFixedCam = true;
            usingMouseCam = false;
        }
    }

    private void MousesControls() {

        pivot.transform.position = target.transform.position;

        float horizontal = Input.GetAxis("Mouse X") * rotationSpeed;
        pivot.Rotate(0, horizontal, 0);
        float vertical = Input.GetAxis("Mouse Y") * rotationSpeed;
        if (!invertY)
        {
            pivot.Rotate(vertical, 0, 0);
        }
        else
        {
            pivot.Rotate(-vertical, 0, 0);
        }
        if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
        }

        if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < /*360 - (mVA)*/minViewAngle)
        {
            pivot.rotation = Quaternion.Euler(/*360 - (mVA)*/minViewAngle, 0, 0);
        }
        float desiredYAngle = pivot.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * offset);


        //transform.position = target.position - offset;
        if (transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y - .5f, transform.position.z);
        }
        transform.LookAt(target);
    }
    private void FixedCameraLock() {
        transform.position = target.position - offsetPos;
        transform.LookAt(target);


    }
}
