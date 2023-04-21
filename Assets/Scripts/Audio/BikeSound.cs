using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public static BikeSound instance;
    public Slider SliderCar;
    public Image ImgBtnCar;
    public Sprite spriteMute;
    public Sprite spriteUnMute;
    [SerializeField] private UnitsData LoadData;
    private void Awake() {
        if(instance == null)
            instance = this;
    }
    private void Start() {
        audioSource = GetComponent<AudioSource>();
        bikeRb = GetComponentInChildren<Rigidbody>();
        SliderCar.value = LoadData.CarVolume;
        SliderCar.onValueChanged.AddListener(delegate { OnSliderSoundChange(); });
        if(LoadData.IsCarVolume == true) {
            ImgBtnCar.sprite = spriteUnMute;
        }else{
            ImgBtnCar.sprite = spriteMute;
        }
    }
    private void LateUpdate() {
        EngineSound();
    }
    private void Update() {
        if(LoadData.IsCarVolume == false){
            audioSource.volume = 0;
        }else{
            audioSource.volume = SliderCar.value / 100f;
        }
    } 
    public void BtnMuteSound(){
        LoadData.IsCarVolume = !LoadData.IsCarVolume;
        if(LoadData.IsCarVolume){
            ImgBtnCar.sprite = spriteUnMute;
        }else{
            ImgBtnCar.sprite = spriteMute;
        }
    }
    private void OnSliderSoundChange()
    {
        // Update the volume of the audio source to match the value of the slider
        audioSource.volume = SliderCar.value;
        LoadData.CarVolume = (int)SliderCar.value;
    }
    private void EngineSound(){
        _pitchFromBike = bikeRb.velocity.magnitude;
        _tempSpeed1 = _pitchFromBike/_speedToPitch;
        _tempSpeed2 = (int)_tempSpeed1;

        _pitchDif = _tempSpeed1 - _tempSpeed2;
        audioSource.pitch = Mathf.Lerp(audioSource.pitch, (_maxPitch * _pitchDif) + _minPitch,0.01f);
    }

}
