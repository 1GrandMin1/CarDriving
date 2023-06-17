using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Controller : MonoBehaviour
{
    [SerializeField] private Transform _transformFR;
    [SerializeField] private Transform _transformFL;
    [SerializeField] private Transform _transformBR;
    [SerializeField] private Transform _transformBL;

    [SerializeField] private WheelCollider _colliderFR;
    [SerializeField] private WheelCollider _colliderFL;
    [SerializeField] private WheelCollider _colliderBR;
    [SerializeField] private WheelCollider _colliderBL;

    [SerializeField] private float _force;
    [SerializeField]private float _maxAngle;
    [SerializeField] private float _torque = 3000f;
    [SerializeField] private float _torqueStop = 0f;


    private void FixedUpdate()
    {
        _colliderFL.motorTorque = -Input.GetAxis("Vertical")*_force;
        _colliderFR.motorTorque = -Input.GetAxis("Vertical")*_force;

        if (Input.GetKey(KeyCode.Space))
        {
            _colliderFL.brakeTorque = _torque;
            _colliderFR.brakeTorque = _torque;
            _colliderBL.brakeTorque = _torque;
            _colliderBR.brakeTorque = _torque;

        }
        else
        {
            _colliderFL.brakeTorque = _torqueStop;
            _colliderFR.brakeTorque = _torqueStop;
            _colliderBL.brakeTorque = _torqueStop;
            _colliderBR.brakeTorque = _torqueStop;
        }
        _colliderFL.steerAngle = _maxAngle * Input.GetAxis("Horizontal");
        _colliderFR.steerAngle = _maxAngle * Input.GetAxis("Horizontal");
        RotateWheel(_colliderFL, _transformFL);
        RotateWheel(_colliderFR, _transformFR);
        RotateWheel(_colliderBL, _transformBL);
        RotateWheel(_colliderBR, _transformBR);
    }
    private void RotateWheel(WheelCollider collider,Transform transform)
    {
        Vector3 position;
        Quaternion rotation;

        collider.GetWorldPose(out position, out rotation);
        transform.rotation = rotation;
        transform.position = position;
    }
}

