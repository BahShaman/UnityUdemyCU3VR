using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //print("hello");
        ProcessInput();
    }

    private void ProcessInput()
    {
        

        if (Input.GetKey(KeyCode.Space)) //can thrust while rotating
        {
            //print("Trusting");
            rigidBody.AddRelativeForce(Vector3.up);
        }

        if (Input.GetKey(KeyCode.W))
        {
            //print("Rotate forward");
            transform.Rotate(Vector3.left);

        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(-Vector3.left);
            //print("Rotate back");
        }


        if (Input.GetKey(KeyCode.A))
        {
            //print("Rotate right");
            transform.Rotate(Vector3.forward);

        }else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward);
            //print("Rotate left");
        }

    }
}
