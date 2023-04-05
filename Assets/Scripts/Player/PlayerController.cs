using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //đây là bản lỗi, a dùng bản 2 kia nhé
    [Header("Component")]
    [SerializeField] WheelCollider _frontWheel;
    [SerializeField] WheelCollider _backWheel;
    [Header("Player Setting")]
    [SerializeField] float _acceleration = 500f; //Gia toc
    // [SerializeField] float _brakeFore = 300f; //luc khi dap phanh
    [SerializeField] float _maxTurnAngle = 30f; //goc quay xe
    [SerializeField] float _turnSpeed = 1f;
    [SerializeField] float _speed = 100f;
    // private float _currentAcceleration  = 0f;
    // private float _currentBrakeFore = 0f;
    // private float _currentTurnAngle = 0f;
    // private float _moveInput;
    // private float _turnInput;
    // public Vector3 _centerMass;
    // private Rigidbody rb;
    float _moveInput;
    float _turnInput;
    public List<Wheel> wheels;
    Rigidbody rb;
    private void Start() {
        rb = GetComponent<Rigidbody>();
    }
    private void Update() {
        GetInput();
        // AnimateWheels();
    }
    public enum Axel{
        Front,
        Back
    }
    [System.Serializable]
    public struct Wheel{
        public GameObject wheelMess;
        public WheelCollider wheelCollider;
        public Axel axel;
    }
    void GetInput(){
        _moveInput = Input.GetAxis("Vertical");
        _turnInput = Input.GetAxis("Horizontal");
    }
    private void FixedUpdate() {
        Moving();
        Turning();
        // AnimateWheels();
        //brake
        // if(Input.GetKey(KeyCode.Space)){
        //     _currentBrakeFore = _brakeFore;
        // }else{
        //     _currentBrakeFore = 0f;
        // }

        // // _frontWheel.motorTorque = _currentAcceleration;
        // // _backWheel.motorTorque = _currentAcceleration;

        // _frontWheel.brakeTorque = _currentBrakeFore;
        // _backWheel.brakeTorque = _currentBrakeFore;

    }
    void Moving(){
        foreach(var _wheel in wheels){
            //go forward and backward
           _wheel.wheelCollider.motorTorque = _moveInput *_speed* _acceleration;
        }
    }
    void Turning(){
        foreach(var _wheel in wheels){
            if(_wheel.axel == Axel.Front){
                var _turnAngle = _turnInput * _turnSpeed * _maxTurnAngle;
                _wheel.wheelCollider.steerAngle = Mathf.Lerp(_wheel.wheelCollider.steerAngle, _turnAngle, 0.6f);
            }
        }
    }
    // void AnimateWheels()
    // {
    //     foreach(var wheel in wheels)
    //     {
    //         Quaternion rot;
    //         Vector3 pos;
    //         wheel.wheelCollider.GetWorldPose(out pos, out rot);
    //         wheel.wheelMess.transform.position = pos;
    //         wheel.wheelMess.transform.rotation = rot;
    //     }
    // }
}
