using UnityEngine;


public class Minimap : MonoBehaviour
{
    [Header("References")]
    public RectTransform minimapPoint_1;
    public RectTransform minimapPoint_2;
    public Transform worldPoint_1;
    public Transform worldPoint_2;

    [Header("Player")]
    public RectTransform playerMinimap;
    public Transform playerWorld;
    
    [Header("Mission")]
    public RectTransform missionMinimap;
    public Transform missionWorld;
    
    [Header("Pickup")]
    public RectTransform pickUpMinimap;
    public Transform pickUpWorld;

    private float minimapRatiox;
    private float minimapRatioz;

    private void Awake()
    {
        CalculateMapRatio();
    }

    private void Update()
    {
        playerMinimap.anchoredPosition = 
            minimapPoint_1.anchoredPosition + 
            new Vector2((playerWorld.position.x - worldPoint_1.position.x) * minimapRatiox,
                        (playerWorld.position.z - worldPoint_1.position.z) * minimapRatioz);

        if(MissionSpawner.instance.missionData != null)
        {
            missionWorld = MissionSpawner.instance.missionData[0].transform;
        }
        
        missionMinimap.anchoredPosition = 
            minimapPoint_1.anchoredPosition + 
            new Vector2((missionWorld.position.x - worldPoint_1.position.x) * minimapRatiox,
                        (missionWorld.position.z - worldPoint_1.position.z) * minimapRatioz);  
        
        pickUpMinimap.anchoredPosition = 
            minimapPoint_1.anchoredPosition + 
            new Vector2((pickUpWorld.position.x - worldPoint_1.position.x) * minimapRatiox,
                        (pickUpWorld.position.z - worldPoint_1.position.z) * minimapRatioz);  
    }
    
    public void CalculateMapRatio()
    {
        /*
        //distance world ignoring Y axis
        Vector3 distanceWorldVector = worldPoint_1.position - worldPoint_2.position;
        distanceWorldVector.y = 0f;
        float distanceWorld = distanceWorldVector.magnitude;



        //distance minimap
        float distanceMinimap = Mathf.Sqrt(
            Mathf.Pow((minimapPoint_1.anchoredPosition.x - minimapPoint_2.anchoredPosition.x), 2) +
            Mathf.Pow((minimapPoint_1.anchoredPosition.y - minimapPoint_2.anchoredPosition.y), 2));


        //minimapRatio = distanceMinimap / distanceWorld;
        */
        Vector3 distanceWorldVector = worldPoint_1.position - worldPoint_2.position;
        distanceWorldVector.y = 0f;
        distanceWorldVector.z = 0f;
        float distanceWorld = distanceWorldVector.magnitude;

        float distanceMinimap = Mathf.Sqrt(
            Mathf.Pow((minimapPoint_1.anchoredPosition.x - minimapPoint_2.anchoredPosition.x), 2));

        minimapRatiox = distanceMinimap / distanceWorld;

        distanceWorldVector = worldPoint_1.position - worldPoint_2.position;
        distanceWorldVector.y = 0f;
        distanceWorldVector.x = 0f;
        distanceWorld = distanceWorldVector.magnitude;

        distanceMinimap = Mathf.Sqrt(Mathf.Pow((minimapPoint_1.anchoredPosition.y - minimapPoint_2.anchoredPosition.y), 2));
        minimapRatioz = distanceMinimap / distanceWorld;
    }
}