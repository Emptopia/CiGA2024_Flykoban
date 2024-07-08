using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGarvity : MonoBehaviour
{
    public bool CanPush;
    private Rigidbody rig;
    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        rig.useGravity = true;

    }
    public enum CubeGarvityType
    {
        Up,
        down,
        left,
        right,
        forward,
        back
    }
    public CubeGarvityType cubeGarvityType;
    Vector3 gravity;
    private void Update()
    {   
        switch (cubeGarvityType)
        {
            case CubeGarvityType.Up:
                rig.AddForce(new Vector3(0, 9.8f, 0));
                break;
            case CubeGarvityType.down:
                rig.AddForce(new Vector3(0, -9.8f, 0));
                break;
            case CubeGarvityType.left:
                rig.AddForce(new Vector3(-9.8f, 0, 0));
                break;
            case CubeGarvityType.right:
                rig.AddForce(new Vector3(9.8f, 0, 0));
                break;
            case CubeGarvityType.forward:
                rig.AddForce(new Vector3(0, 0, -9.8f));
                break;
            case CubeGarvityType.back:
                rig.AddForce(new Vector3(0, 0, 9.8f));
                break;
        }

        if (CanPush)
        {

        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
        }
    }
}
