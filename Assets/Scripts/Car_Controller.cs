using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] private float _forceForward;
    [SerializeField] private float _forceBack;
    [SerializeField] private float _forceCurrent;
    [SerializeField] private float _forceDefault;
    [SerializeField] private float _maxAngle;
    private float _defAngle = 0f;
    [SerializeField] private float _torque = 3000f;
    [SerializeField] private float _torqueStop = 0f;

  

    [SerializeField] private Joystick _joystick;

   
    private void FixedUpdate()
    {
       
        
        if (_joystick.Vertical > 0.1f)
        {
            _forceCurrent = Mathf.Lerp(_forceCurrent, _forceForward, 10);
            _colliderFL.motorTorque = -_joystick.Vertical * _forceCurrent;
            _colliderFR.motorTorque = -_joystick.Vertical * _forceCurrent;    
        }
        else if (_joystick.Vertical < -0.1f)
        {
            _forceCurrent = Mathf.Lerp(_forceCurrent, _forceBack, 10);

            _colliderFL.motorTorque = -_joystick.Vertical * _forceCurrent;
            _colliderFR.motorTorque = -_joystick.Vertical * _forceCurrent; 
        }
        else
        {
            _forceCurrent = Mathf.Lerp(_forceCurrent, _forceDefault, 10);

            _colliderFL.motorTorque = -_joystick.Vertical * _forceCurrent;
            _colliderFR.motorTorque = -_joystick.Vertical * _forceCurrent;

        }
        if (_joystick.Horizontal > 0.1f)
        {          
            _colliderFL.steerAngle = _joystick.Horizontal * _maxAngle;
            _colliderFR.steerAngle = _joystick.Horizontal * _maxAngle;
        }
        else if (_joystick.Horizontal < -0.1f)
        {
            _colliderFL.steerAngle = _joystick.Horizontal * _maxAngle;
            _colliderFR.steerAngle = _joystick.Horizontal * _maxAngle;
        }
        else
        {
            _colliderFL.steerAngle = _joystick.Horizontal * _defAngle;
            _colliderFR.steerAngle = _joystick.Horizontal * _defAngle;
        }

        RotateWheel(_colliderFL, _transformFL);
        RotateWheel(_colliderFR, _transformFR);
        RotateWheel(_colliderBL, _transformBL);
        RotateWheel(_colliderBR, _transformBR);
    }
    private void RotateWheel(WheelCollider collider, Transform transform)
    {
        Vector3 position;
        Quaternion rotation;

        collider.GetWorldPose(out position, out rotation);
        transform.rotation = rotation;
        transform.position = position;
    }
    public void Brake()
    {
        _colliderFL.brakeTorque = _torque;
        _colliderFR.brakeTorque = _torque;
        _colliderBL.brakeTorque = _torque;
        _colliderBR.brakeTorque = _torque;
    }
    public void releaseTheBrake()
    {
        _colliderFL.brakeTorque = _torqueStop;
        _colliderFR.brakeTorque = _torqueStop;
        _colliderBL.brakeTorque = _torqueStop;
        _colliderBR.brakeTorque = _torqueStop;
    }
}

