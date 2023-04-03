using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] WheelCollider _frontWheel;
    [SerializeField] WheelCollider _backWheel;
    [Header("Player Setting")]
    [SerializeField] float _acceleration = 500f; //Gia toc
    [SerializeField] float _brakeFore = 300f; //luc khi dap phanh
    private float _currentAcceleration  = 0f;
    private float _currentBrakeFore = 0f;

    private void FixedUpdate() {
        //go forward and backward
        _currentAcceleration = _acceleration * Input.GetAxis("Vertical");
        //brake
        if(Input.GetKey(KeyCode.Space)){
            _currentBrakeFore = _brakeFore;
        }else{
            _currentBrakeFore = 0f;
        }
        
        _frontWheel.motorTorque = _currentAcceleration;
        _backWheel.motorTorque = _currentAcceleration;

        _frontWheel.brakeTorque = _currentBrakeFore;
        _backWheel.brakeTorque = _currentBrakeFore;
    }
}
