using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] private Light _redLight, _yellowLight, _greenLight, _redLight1, _yellowLight1, _greenLight1;
    [SerializeField] GameObject redLightLimit;
    [Header("Status")]
    public bool turnRed;
    public float redTime, yellowTime, greenTime;
    // Start is called before the first frame update
    void Start()
    {
        turnRed = true;
    }

    // Update is called once per frame
    void Update()
    {
        LightControl();
    }

    private void LightControl()
    {
        if (turnRed == true)
        {
            StartCoroutine(RedLight());
        }

    }

    IEnumerator RedLight()
    {
        turnRed = false;
        _redLight.enabled = true;
        _redLight1.enabled = true;
        redLightLimit.SetActive(true);
        yield return new WaitForSeconds(redTime);
        _redLight.enabled = false;
        _redLight1.enabled = false;
        redLightLimit.SetActive(false);
        StartCoroutine(GreenLight());
    }
    IEnumerator GreenLight()
    {
        _greenLight.enabled = true;
        _greenLight1.enabled = true;
        yield return new WaitForSeconds(greenTime);
        _greenLight.enabled = false;
        _greenLight1.enabled = false;
        StartCoroutine(YellowLight());
    }

    IEnumerator YellowLight()
    {
        _yellowLight.enabled = true;
        _yellowLight1.enabled = true;
        yield return new WaitForSeconds(yellowTime);
        _yellowLight.enabled = false;
        _yellowLight1.enabled = false;
        turnRed = true;
    }
}
