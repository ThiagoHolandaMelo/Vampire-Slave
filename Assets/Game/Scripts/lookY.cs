using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookY : MonoBehaviour {

    [SerializeField]
    private float sensitivity = 5f;

    float maxRotationX = 320f;
    float minRotationX = 40f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 newRotation = transform.localEulerAngles;
        newRotation.x -= ( mouseY * sensitivity );

        if(newRotation.x < maxRotationX && newRotation.x > 300f)
        {
            newRotation.x = maxRotationX;
        }
        
        if(newRotation.x > minRotationX && newRotation.x < maxRotationX)
        {   
            newRotation.x = minRotationX;
        }
        

        transform.localEulerAngles = newRotation;

	}
}
