using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControll2 : MonoBehaviour
{
    [SerializeField] private Light _redLight, _yellowLight, _greenLight, _redLight1, _yellowLight1, _greenLight1;
    [SerializeField] GameObject redLightLimit;
    [Header("Status")]
    public bool turnGreen;
    public float redTime, yellowTime, greenTime;
    // Start is called before the first frame update
    void Start()
    {
        turnGreen = true;
    }

    // Update is called once per frame
    void Update()
    {
        LightControl();
    }

    private void LightControl()
    {
        if (turnGreen == true)
        {
            StartCoroutine(GreenLight());
        }

    }

    IEnumerator GreenLight()
    {
        turnGreen = false;
        _greenLight.enabled = true;
        _greenLight1.enabled = true;
        yield return new WaitForSeconds(greenTime);
        _greenLight.enabled = false;
        _greenLight1.enabled = false;
        StartCoroutine(YellowLight());
    }
    IEnumerator RedLight()
    {
        _redLight.enabled = true;
        _redLight1.enabled = true;
        redLightLimit.SetActive(true);
        yield return new WaitForSeconds(redTime);
        _redLight.enabled = false;
        _redLight1.enabled = false;
        redLightLimit.SetActive(false);
        turnGreen = true;
    }

    IEnumerator YellowLight()
    {
        _yellowLight.enabled = true;
        _yellowLight1.enabled = true;
        yield return new WaitForSeconds(yellowTime);
        _yellowLight.enabled = false;
        _yellowLight1.enabled = false;
        StartCoroutine(RedLight());
    }
}

