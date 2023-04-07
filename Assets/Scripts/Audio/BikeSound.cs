using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeSound : MonoBehaviour
{
    [Header("Audio Setting")]
    [SerializeField] float _speedToPitch;
    [SerializeField] float _minPitch;
    [SerializeField] float _maxPitch;
    private float _pitchFromBike;
    private float _tempSpeed1;
    private int _tempSpeed2;
    private float _pitchDif;
    [Header("Component")]
    private Rigidbody bikeRb;
    private AudioSource audioSource;
    private void Start() {
        audioSource = GetComponent<AudioSource>();
        bikeRb = GetComponent<Rigidbody>();
    }
    private void LateUpdate() {
        EngineSound();
    }
    private void EngineSound(){
        _pitchFromBike = bikeRb.velocity.magnitude;
        _tempSpeed1 = _pitchFromBike/_speedToPitch;
        _tempSpeed2 = (int)_tempSpeed1;

        _pitchDif = _tempSpeed1 - _tempSpeed2;
        audioSource.pitch = Mathf.Lerp(audioSource.pitch, (_maxPitch * _pitchDif) + _minPitch,0.01f);
    }

}
