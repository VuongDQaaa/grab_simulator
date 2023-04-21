using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UnitsData : ScriptableObject
{   
    [SerializeField] private int _musicVolume;
    [SerializeField] private bool _isMusicVolume;
    [SerializeField] private int _soundVolume;
    [SerializeField] private bool _isSoundVolume;


    public int MusicVolume { get => _musicVolume; set => _musicVolume = value;}
    public bool IsMusicVolume { get => _isMusicVolume; set => _isMusicVolume = value;}
    public int SoundVolume { get => _soundVolume; set => _soundVolume = value;}
    public bool IsSoundVolume { get => _isSoundVolume; set => _isSoundVolume = value;}
    public int CarVolume { get => _soundVolume; set => _soundVolume = value;}
    public bool IsCarVolume { get => _isSoundVolume; set => _isSoundVolume = value;}
}
