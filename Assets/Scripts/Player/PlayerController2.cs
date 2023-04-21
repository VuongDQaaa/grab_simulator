using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

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
    [SerializeField] WheelCollider frontWheelColliderRight;
    [SerializeField] WheelCollider frontWheelColliderLeft;
    [SerializeField] WheelCollider backWheelColliderRight;
    [SerializeField] WheelCollider backWheelColliderLeft;
    [SerializeField] Transform frontWheelTransRight;
    [SerializeField] Transform frontWheelTransLeft;
    [SerializeField] Transform backWheelTransRight;
    [SerializeField] Transform backWheelTransLeft;
    // [SerializeField] Animator animator;
    [SerializeField] GameObject brakeLight;
    [SerializeField] AudioSource audioBrake;
    [SerializeField] AudioClip brakeSound;
    private Rigidbody rb;
    [Header("Timer")]
    [SerializeField] float _timeValue = 120;
    [SerializeField] TextMeshProUGUI timerUI;
    [Header("Score")]
    [SerializeField] SO scoreSO;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] float _ScoreStart = 1200;
    private float _currentScore;
    [Header("Other")]
    public static PlayerController2 instace;
    public List<Wheel> wheels;
    float _moveInput;
    float _turnInput;
    public ControllMode controll;
    private void Awake() {
        if( instace == null){
            instace = this;
        }
    }
    private void Start() {
        rb = GetComponent<Rigidbody>();
        audioBrake.clip = brakeSound;
        PointDown();
    }
    private void Update() {
        _currentScore = _ScoreStart;
        scoreSO.value = _currentScore;
        GetInput();
        // SetAnimation();
        //timer
        if(_timeValue > 0 ){
            _timeValue -= Time.deltaTime;
        }else{
            _timeValue = 0;
        }
        CountDisPlay(_timeValue);
        if(_timeValue == 0){
            _ScoreStart = 0;
            Invoke("Delay", 1f);
        }
    }
    void Delay(){
        GameManager.instance.GameOver();
    }
    private void FixedUpdate() {
        Moving();
        Turning();
        Braking();
        UpdateWheel(frontWheelColliderRight,frontWheelTransRight);
        UpdateWheel(frontWheelColliderLeft,frontWheelTransLeft);
        UpdateWheel(backWheelColliderRight,backWheelTransRight);
        UpdateWheel(backWheelColliderLeft,backWheelTransLeft);
    }
    // void SetAnimation(){
    //     if(_turnInput <0){
    //         animator.SetBool("turnLeft", true);
    //         animator.SetBool("turnRight", false);
    //     }else if(_turnInput > 0)
    //     {
    //         animator.SetBool("turnRight", true);
    //         animator.SetBool("turnLeft", false);
    //     }else{
    //         animator.SetBool("turnRight", false);
    //         animator.SetBool("turnLeft", false);
    //     }
    // }
    public void PointDown(){
        if(_ScoreStart >= 0){
            scoreText.text = "Point Left: " + scoreSO.value; //đặt hiển thị
            _ScoreStart--;
            Invoke("PointDown", 0.5f); //giảm 1 điểm mỗi 0.1s
            // getScore = _ScoreStart;
        } else{
            _ScoreStart = 0;
        }
    }
    #region Setting enum
    public enum ControllMode{
        keyBoard,
        TouchPad
    };
    public enum Axel{
        Front,
        Back
    }
    #endregion
    #region Setting PlayerControll Input
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
        Debug.Log("move");
    }
    public void TouchTurnInput(float _input){
        _turnInput = _input;
        Debug.Log("turn");
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
        if(Input.GetKey(KeyCode.Space )){ //|| _moveInput == 0 *note: thêm vào nếu test trên phone, 
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
    #endregion
    #region Timer
    void CountDisPlay(float _timeToDisPlay){
        if(_timeToDisPlay <0 ){
            _timeToDisPlay = 0;
        }else if(_timeToDisPlay > 0){
            _timeToDisPlay += 1;
        }

        float _minutes = Mathf.FloorToInt(_timeToDisPlay / 60);
        float _seconds = Mathf.FloorToInt(_timeToDisPlay % 60);

        timerUI.text = string.Format("{0:00} : {1:00}", _minutes, _seconds);
        
    }
    public void TakeTime(float _timeDown){
        _timeValue -= _timeDown;
    }
    public void AddTime(float _timeUp){
        _timeValue += _timeUp;
    }
    #endregion

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Red"){
            Destroy(other.gameObject);
            TakeTime(10);
            PlayerHealth.instance.TakeDamage(20);
        }
        if(other.gameObject.tag == "TimeUp"){
            Destroy(other.gameObject);
            AddTime(30);
        }
    }
}

