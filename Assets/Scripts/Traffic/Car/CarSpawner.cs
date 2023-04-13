using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _carPerfab;
    public float spawnTime;
    public bool spawn;
    private void Start()
    {
        spawn = true;
    }
    private void Update()
    {
        if(spawn == true)
        {
            spawn = false;
            SpawnCar();
        }
    }

    private void SpawnCar()
    {
        Invoke("DelaySpawn", spawnTime);
        Instantiate(_carPerfab, transform.position, Quaternion.identity);
    }

    private void DelaySpawn()
    {
        if (spawn == false)
        {
            spawn = true;
        }
    }
}
