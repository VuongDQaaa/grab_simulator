using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource Source;
    public AudioSource Select;
    public AudioSource QuitOrBack;

    private void Update() {
        Select.volume = Source.volume;
        QuitOrBack.volume = Source.volume;
    }
}
