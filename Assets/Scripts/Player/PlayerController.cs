using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
<<<<<<< HEAD
    public WheelCollider frontLeftW, frontRightW;
    public WheelCollider rearLeftW, rearRightW;
    public Transform frontLeftT, frontRightT;
    public Transform rearLeftT, rearRightT;
    [SerializeField] private Rigidbody rb;
    public GameObject _light;
    [Header("Player Set")]
    public float _maxSteerAngle = 30;
    public float _motorForce = 50;
    public float _brakeForce = 20;
    private float _turnInput;
    private float _moveInput;
    private float _brakeInput;
    private float _steeringAngle;
    private float _currentbreakForce;
    private bool isBreaking;
    private bool islightOn;
    public Vector3 centerOfMass;
    [Header("Other")]
    public ControllMode controll;
    public enum ControllMode
    {
        keyBoard,
        TouchPad
    };
    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        rb.centerOfMass = centerOfMass;
    }
    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
        _currentbreakForce = isBreaking ? _brakeForce : 0f;
        if (isBreaking)
            _light.SetActive(true);
        else
            _light.SetActive(false);
        ApplyBreaking();
    }
    public void GetInput()
    {
        if (controll == ControllMode.keyBoard)
        {
=======
	public static PlayerController instance;
	public WheelCollider frontLeftW, frontRightW;
	public WheelCollider rearLeftW, rearRightW;
	public Transform frontLeftT, frontRightT;
	public Transform rearLeftT, rearRightT;
	[SerializeField] private Rigidbody rb;
	public GameObject _light;
	[Header("Player Set")]
	public float _maxSteerAngle = 30;
	public float _motorForce = 50;
	public float _brakeForcePassive = 50000;
	private float _brakeForce = Mathf.Infinity;
	private float _turnInput;
	private float _moveInput;
	private float _brakeInput;
	private float _steeringAngle;
	private float _currentbreakForce;
	public bool isBreaking;
	private bool islightOn;
	public Vector3 centerOfMass;
	[Header("Other")]
	public ControllMode controll;
	public enum ControllMode{
        keyBoard,
        TouchPad
    };
	private void Awake() {
		if(instance == null)
			instance = this;
	}
	private void Start() {
		rb = GetComponentInChildren<Rigidbody>();
		rb.centerOfMass = centerOfMass;
	}
	private void FixedUpdate()
	{
		GetInput();
		Steer();
		Accelerate();
		UpdateWheelPoses();
		// _currentbreakForce = isBreaking ? _brakeForce : 0f;
		if(isBreaking){
			_light.SetActive(true);
			_currentbreakForce = _brakeForce;//phanh chủ động
		}
		else{
			_light.SetActive(false);
			//phanh bị động (để xử lí va chạm)
			if(rearLeftW.motorTorque == 0 && _moveInput == 0)
				_currentbreakForce = _brakeForcePassive;
			else
				_currentbreakForce = 0f;
		}
        ApplyBreaking();
	}
	#region InputSetting
	public void GetInput()
	{
		if(controll == ControllMode.keyBoard){
>>>>>>> 09ab85c42edcc05ef7879de8556727a6e5084336
            _moveInput = Input.GetAxis("Vertical");
            _turnInput = Input.GetAxis("Horizontal");
            isBreaking = Input.GetKey(KeyCode.Space);
        }
    }
    public void TouchMoveInput(float _input)
    {
        _moveInput = _input;
        Debug.Log("move");
    }
    public void TouchTurnInput(float _input)
    {
        _turnInput = _input;
        Debug.Log("turn");
    }
    public void TouchBrakeInput(float _input)
    {
        if (_input == 1)
            isBreaking = true;
        if (_input == 0)
            isBreaking = false;
        Debug.Log("brake");
<<<<<<< HEAD
=======
	}
	#endregion
	#region carMovement
	private void ApplyBreaking() {
		
        frontRightW.brakeTorque = _currentbreakForce ;
        frontLeftW.brakeTorque = _currentbreakForce ;
        rearLeftW.brakeTorque = _currentbreakForce ;
        rearRightW.brakeTorque = _currentbreakForce ;
		
>>>>>>> 09ab85c42edcc05ef7879de8556727a6e5084336
    }
    private void ApplyBreaking()
    {

<<<<<<< HEAD
        frontRightW.brakeTorque = _currentbreakForce;
        frontLeftW.brakeTorque = _currentbreakForce;
        rearLeftW.brakeTorque = _currentbreakForce;
        rearRightW.brakeTorque = _currentbreakForce;
    }
    private void Steer()
    {
        _steeringAngle = _maxSteerAngle * _turnInput;
        frontLeftW.steerAngle = _steeringAngle;
        frontRightW.steerAngle = _steeringAngle;
    }

    private void Accelerate()
    {
        // frontLeftW.motorTorque = _moveInput  * _motorForce;
        // frontRightW.motorTorque = _moveInput  * _motorForce;
        rearLeftW.motorTorque = _moveInput * _motorForce;
        rearRightW.motorTorque = _moveInput * _motorForce;
    }
=======
	private void Accelerate()
	{
		frontLeftW.motorTorque = _moveInput  * _motorForce;
		frontRightW.motorTorque = _moveInput  * _motorForce;
		// rearLeftW.motorTorque = _moveInput  * _motorForce;
		// rearRightW.motorTorque = _moveInput  * _motorForce;
	}
	#endregion
	#region Wheel
	private void UpdateWheelPoses()
	{
		UpdateWheelPose(frontLeftW, frontLeftT);
		UpdateWheelPose(frontRightW, frontRightT);
		UpdateWheelPose(rearLeftW, rearLeftT);
		UpdateWheelPose(rearRightW, rearRightT);
	}
>>>>>>> 09ab85c42edcc05ef7879de8556727a6e5084336

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontLeftW, frontLeftT);
        UpdateWheelPose(frontRightW, frontRightT);
        UpdateWheelPose(rearLeftW, rearLeftT);
        UpdateWheelPose(rearRightW, rearRightT);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

<<<<<<< HEAD
        _collider.GetWorldPose(out _pos, out _quat);
=======
		_transform.position = _pos;
		_transform.rotation = _quat;
	}
	#endregion
>>>>>>> 09ab85c42edcc05ef7879de8556727a6e5084336

        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "car")
        {
            PlayerHealth.instance.TakeDamage(20);
        }
        if (other.gameObject.tag == "Pick Up")
        {
            GameObject pickUp = GameObject.FindGameObjectWithTag("Pick Up");
			if(pickUp != null)
			{
				Destroy(pickUp);
			}
			CurrentMission.instance.isStarted = true;
        }
        if (other.gameObject.tag == "Finish")
        {
            GameObject finish = GameObject.FindGameObjectWithTag("Finish");
			if(finish != null)
			{
				Destroy(finish);
			}
			CurrentMission.instance.isCompleted = true;
        }
		if(other.gameObject.tag == ("Red"))
		{
			CurrentMission.instance.missionTime -= 20f;
		}
    }

}
