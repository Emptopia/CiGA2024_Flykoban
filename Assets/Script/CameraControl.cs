using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("旋转中心目标物体")] [SerializeField] private GameObject _target;
    [Header("拖动灵敏度")] [SerializeField] private float _sensitivity = 2.0f;
    [Header("复原速度")] [SerializeField] private float _returnSpeed = 100.0f;
    [Header("X左极限角度")] [SerializeField] private float _xLimitLeft = -40.0f;
    [Header("X右极限角度")] [SerializeField] private float _xLimitRight = 40.0f;
    [Header("Y左极限角度")] [SerializeField] private float _yLimitLeft = -10.0f;
    [Header("Y右极限角度")] [SerializeField] private float _yLimitRight = 40.0f;
    //private Transform originalTrans;
    public float distanceX, distanceY;
    private void Start()
    {
        LookAround();
    }

    private void LateUpdate()
    {

        if (Input.GetMouseButton(0))
        {
            LookAround();
        }
        else
        {
            //LookBack();
        }

        
    }
    
    private void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * _sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _sensitivity;

        if (distanceX >= _xLimitRight && mouseX >= 0) return;
        if (distanceX <= _xLimitLeft && mouseX <= 0) return;
        
        if (distanceY >= _yLimitRight && mouseY >= 0) return;
        if (distanceY <=  _yLimitLeft && mouseY <= 0) return;
        
        transform.RotateAround(_target.transform.position, Vector3.up, mouseX);
        transform.RotateAround(_target.transform.position, transform.right, -mouseY);
        transform.LookAt(_target.transform);
        distanceX += mouseX;
        distanceY += mouseY;
    }

    private void LookBack()
    {
        if (distanceX >= Mathf.Epsilon)
        {
            float moveX = _returnSpeed * Time.deltaTime;
            transform.RotateAround(_target.transform.position, Vector3.up, -moveX);
            distanceX -= moveX;
        }
        if (distanceX <= Mathf.Epsilon)
        {
            float moveX = _returnSpeed * Time.deltaTime;
            transform.RotateAround(_target.transform.position, Vector3.up, moveX);
            distanceX += moveX;
        }

        if (distanceY >= Mathf.Epsilon)
        {
            float moveY = _returnSpeed * Time.deltaTime;
            transform.RotateAround(_target.transform.position, transform.right, +moveY);
            distanceY -= moveY;
        }
        if (distanceY <= Mathf.Epsilon)
        {
            float moveY = _returnSpeed * Time.deltaTime;
            transform.RotateAround(_target.transform.position, transform.right, -moveY);
            distanceY += moveY;
        }
    }
}
