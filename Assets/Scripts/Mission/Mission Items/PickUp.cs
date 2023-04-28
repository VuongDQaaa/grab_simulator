using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private SO _healthSO;
    private void OnTriggerEnter(Collider other)

    {
        if(other.CompareTag("Player"))
        {
            CurrentMission.instance.isStarted = true;
            Destroy(gameObject);
            _healthSO.value = 100;

        }
    }
}
