using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioController : MonoBehaviour
{
    [SerializeField] private UnitsData LoadData;
    public AudioSource Music;
    public AudioSource Sound;
    public Slider SliderMusic;
    public Slider SliderSound;
    public Image ImgBtnMusic;
    public Image ImgBtnSound;
    public Sprite spriteMute;
    public Sprite spriteUnMute;



    private void Start() {
        SliderMusic.value = LoadData.MusicVolume;
        SliderSound.value = LoadData.SoundVolume;
        SliderMusic.onValueChanged.AddListener(delegate { OnSliderMusicChange(); });
        SliderSound.onValueChanged.AddListener(delegate { OnSliderSoundChange(); }); 
        if(LoadData.IsMusicVolume == true)  {
            ImgBtnMusic.sprite = spriteUnMute;
        }else{
            ImgBtnMusic.sprite = spriteMute;
        }

        if(LoadData.IsSoundVolume == true) {
            ImgBtnSound.sprite = spriteUnMute;
        }else{
            ImgBtnSound.sprite = spriteMute;
        }
    }
    private void OnSliderMusicChange()
    {
        // Update the volume of the audio source to match the value of the slider
        Music.volume = SliderMusic.value;
        LoadData.MusicVolume = (int)SliderMusic.value;
    }

    private void OnSliderSoundChange()
    {
        // Update the volume of the audio source to match the value of the slider
        Sound.volume = SliderSound.value;
        LoadData.SoundVolume = (int)SliderSound.value;
    }

    private void Update() {
        if(LoadData.IsMusicVolume == false){
            Music.volume = 0;
        }else{
            Music.volume = SliderMusic.value / 100f;
        }

        if(LoadData.IsSoundVolume == false){
            Sound.volume = 0;
        }else{
            Sound.volume = SliderSound.value / 100f;
        }
    }   
    
    public void _BtnMuteMusic(){
        LoadData.IsMusicVolume = !LoadData.IsMusicVolume;
        if(LoadData.IsMusicVolume){
            ImgBtnMusic.sprite = spriteUnMute;
        }else{
            ImgBtnMusic.sprite = spriteMute;
        }
    }

    public void _BtnMuteSound(){
        LoadData.IsSoundVolume = !LoadData.IsSoundVolume;
        if(LoadData.IsSoundVolume){
            ImgBtnSound.sprite = spriteUnMute;
        }else{
            ImgBtnSound.sprite = spriteMute;
        }
    }
}
