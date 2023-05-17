using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour
{
    [Header("Car attribute")]
    public Transform path;
    public float nodeDistance;
    public float maxSteerAngle = 60f;
    public float maxMotorTourque;
    public float currentSpeed;
    public float maxSpeed;
    public float maxBrakeTourque;
    public bool isBraking;
    public Vector3 centerOfMass;
    public bool activeCheckDestroy;
    [SerializeField] private WheelCollider _wheelFL, _wheelFR, _wheelRL, _wheelRR;
    [Header("Sensor")]
    [SerializeField] private GameObject _frontSensor;
    public float maxSensorLength, hitDistance;
    private List<Transform> _nodes;
    public int _currentNode = 0;
    // Start is called before the first frame update
    void Start()
    {
        activeCheckDestroy = false;
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        _nodes = new List<Transform>();
        //Add node into list
        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                _nodes.Add(pathTransforms[i]);
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        Sensors();
        ApplySteer();
        Drive();
        CheckWayPointDistance();
        DestroyAICar();
    }

    private void Sensors()
    {
        RaycastHit hit;
        if (Physics.Raycast(_frontSensor.transform.position, _frontSensor.transform.forward, out hit, maxSensorLength))
        {
            hitDistance = hit.distance;
            if (hit.collider.CompareTag("Red") && hitDistance <= 50
                || hit.collider.CompareTag("Car") && hitDistance <= 50)
            {
                isBraking = true;
            }
        }
        else
        {
            isBraking = false;
            hitDistance = 0;
        }
        //Braking();
    }

    private void ApplySteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(_nodes[_currentNode].position);
        //get sterring side = steering vector.x / steering vector length
        //get sterring angle (steering angle = side * max steering angle)
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        _wheelFL.steerAngle = newSteer;
        _wheelFR.steerAngle = newSteer;
    }

    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * _wheelFL.rpm * 60 / 1000;
        if (currentSpeed < maxSpeed && isBraking == false)
        {
            _wheelFL.motorTorque = maxMotorTourque;
            _wheelFR.motorTorque = maxMotorTourque;
        }
        else
        {
            _wheelFL.motorTorque = 0;
            _wheelFR.motorTorque = 0;
        }
    }

    private void DestroyAICar()
    {
        float rearWeelSpeed = 2 * Mathf.PI * _wheelRL.rpm * 60 / 1000;
        if (rearWeelSpeed >= 20)
        {
            activeCheckDestroy = true;
        }
        if (activeCheckDestroy == true && rearWeelSpeed <= 2)
        {
            StartCoroutine(DestroyCar());
        }
    }

    private void CheckWayPointDistance()
    {
        if (Vector3.Distance(transform.position, _nodes[_currentNode].position) < nodeDistance)
        {
            _currentNode++;
        }
    }

    // private void Braking()
    // {
    //     if (isBraking == true)
    //     {
    //         _wheelRL.brakeTorque = maxBrakeTourque;
    //         _wheelRR.brakeTorque = maxBrakeTourque;
    //     }
    //     else
    //     {
    //         _wheelRL.brakeTorque = 0;
    //         _wheelRR.brakeTorque = 0;
    //     }
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyCar()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
