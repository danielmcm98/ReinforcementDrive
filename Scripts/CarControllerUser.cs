using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerUser : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizInput;
    private float vertInput;
    private float steerAngleNow;
    private float breakingForceNow;
    private bool isBreak;

    [SerializeField] private float driveForce;
    [SerializeField] private float breakingForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider backLeftWheelCollider;
    [SerializeField] private WheelCollider backRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheeTransform;
    [SerializeField] private Transform backLeftWheelTransform;
    [SerializeField] private Transform backRightWheelTransform;

    private void FixedUpdate()
    {
        GetInput();
        HandleDrive();
        HandleSteer();
        MoveWheels();
    }


    private void GetInput()
    {
        horizInput = Input.GetAxis(HORIZONTAL);
        vertInput = Input.GetAxis(VERTICAL);
        isBreak = Input.GetKey(KeyCode.Space);
    }

    private void HandleDrive()
    {
        frontLeftWheelCollider.motorTorque = vertInput * driveForce;
        frontRightWheelCollider.motorTorque = vertInput * driveForce;
        breakingForceNow = isBreak ? breakingForce : 0f;
        UseBreak();
    }

    private void UseBreak()
    {
        frontRightWheelCollider.brakeTorque = breakingForceNow;
        frontLeftWheelCollider.brakeTorque = breakingForceNow;
        backLeftWheelCollider.brakeTorque = breakingForceNow;
        backRightWheelCollider.brakeTorque = breakingForceNow;
    }

    private void HandleSteer()
    {
        steerAngleNow = maxSteerAngle * horizInput;
        frontLeftWheelCollider.steerAngle = steerAngleNow;
        frontRightWheelCollider.steerAngle = steerAngleNow;
    }

    private void MoveWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheeTransform);
        UpdateSingleWheel(backRightWheelCollider, backRightWheelTransform);
        UpdateSingleWheel(backLeftWheelCollider, backLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 position;
        Quaternion rotation; 
        wheelCollider.GetWorldPose(out position, out rotation);
        wheelTransform.rotation = rotation;
        wheelTransform.position = position;
    }
}

