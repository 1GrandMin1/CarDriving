using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCar : MonoBehaviour
{
    [SerializeField] private Transform _car;

    private Vector3 _offset = new Vector3(0, 3f, 6f);
    private float _speed = 10f;

    private void FixedUpdate()
    {
        var _targetPosition = _car.TransformPoint(_offset);
        transform.position = Vector3.Lerp(transform.position, _targetPosition, _speed * Time.deltaTime);

        var direction = _car.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _speed * Time.deltaTime);
    }

}
