using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapLocate : MonoBehaviour
{
    public static MiniMapLocate instance;
    public Vector2 providerLocation;
    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private RectTransform provider;

    private void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Awake()
    {
        MakeInstance();
    }

    // Update is called once per frame
    void Update()
    {
        GetLocation();
    }

    private void GetLocation()
    {
        GameObject pickUpObj = GameObject.FindGameObjectWithTag("Mission");
        if (pickUpObj != null)
        {
            //get distance
            var distance = Vector3.Distance(pickUpObj.transform.position, PlayerTransform.position);
            //get angle
            var dir = pickUpObj.transform.position - PlayerTransform.position;
            var angle = Vector2.Angle(dir, Vector2.right);
            
            var sinAgle = Mathf.Sin(angle * Mathf.Deg2Rad);

            providerLocation.x = (sinAgle * distance) / Mathf.Sqrt(1 + Mathf.Pow(sinAgle, 2));
            providerLocation.y = Mathf.Sqrt(Mathf.Pow(distance,2) - Mathf.Pow(providerLocation.x, 2));

            provider.anchoredPosition = providerLocation;
        }
    }
}
