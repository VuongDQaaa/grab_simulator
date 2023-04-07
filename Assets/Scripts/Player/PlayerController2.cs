using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    [Header("Player Setting")]
    [SerializeField] float _acceleration = 30f;
    [SerializeField] float _brakeAcceleration = 50f;
    [SerializeField] float _turnSpeed = 1f;
    [SerializeField] float _maxTurnAngle = 30f;
    [SerializeField] float _speed = 30;
    [SerializeField] float _brakeForce = 300;
    [Header("Component")]
    [SerializeField] WheelCollider frontWheelCollider;
    [SerializeField] WheelCollider backWheelCollider;
    [SerializeField] Transform frontWheelTrans;
    [SerializeField] Transform backWheelTrans;
    [SerializeField] Animator animator;
    [SerializeField] GameObject brakeLight;
    [SerializeField] AudioSource audioBrake;
    [SerializeField] AudioClip brakeSound;
    private Rigidbody rb;
    [Header("Other")]
    public List<Wheel> wheels;
    float _moveInput;
    float _turnInput;
    public ControllMode controll;
    private void Start() {
        rb = GetComponent<Rigidbody>();
        audioBrake.clip = brakeSound;
    }
    private void Update() {
        GetInput();
        if(_turnInput <0){
            animator.SetBool("turnLeft", true);
            animator.SetBool("turnRight", false);
        }else if(_turnInput > 0)
        {
            animator.SetBool("turnRight", true);
            animator.SetBool("turnLeft", false);
        }else{
            animator.SetBool("turnRight", false);
            animator.SetBool("turnLeft", false);
        }
    }
    private void FixedUpdate() {
        Moving();
        Turning();
        Braking();
        UpdateWheel(frontWheelCollider,frontWheelTrans);
        UpdateWheel(backWheelCollider,backWheelTrans);
    }
    public enum ControllMode{
        keyBoard,
        TouchPad
    };
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
        if(controll == ControllMode.keyBoard){
            _moveInput = Input.GetAxis("Vertical");
            _turnInput = Input.GetAxis("Horizontal");
        }
    }
    public void TouchMoveInput(float _input){
        _moveInput = _input;
    }
    public void TouchTurnInput(float _input){
        _turnInput = _input;
    }
    void Moving(){
        foreach(var _wheel in wheels){
            _wheel.wheelCollider.motorTorque = _moveInput * _acceleration * _speed * Time.fixedDeltaTime;
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
    void Braking(){
        if(Input.GetKey(KeyCode.Space)){ //|| _moveInput == 0 *note: thêm vào nếu test trên phone, 
        //androi, còn test trên máy tính thì bỏ ra, như thế xe sẽ k phanh lại để có thể thử rẽ trái phải
            foreach(var _wheel in wheels){
                _wheel.wheelCollider.brakeTorque = _brakeForce *_brakeAcceleration * Time.fixedDeltaTime;
                brakeLight.SetActive(true);
            }
            if(audioBrake.isPlaying == false){
                audioBrake.Play();
            }
        }else{
             foreach(var _wheel in wheels){
                _wheel.wheelCollider.brakeTorque = 0;
                brakeLight.SetActive(false);
                
            }
        }
    }
    void UpdateWheel(WheelCollider col, Transform trans){
        Vector3 pos = trans.position;
        Quaternion rotation = trans.rotation;
        col.GetWorldPose(out pos, out rotation);
        trans.position = pos;
        trans.rotation = rotation;
    }
}