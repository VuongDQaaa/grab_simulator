using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInsite : MonoBehaviour
{
    public Sprite Mark;
    public Sprite Guild;
    public SpriteRenderer spriteRenderer;
    public Transform MinimapCam;
	public float MinimapSize;
    public GameObject Player;
	Vector3 TempV3;

	void Update () {
		TempV3 = transform.parent.transform.position;
		TempV3.y = transform.position.y;
		transform.position = TempV3;

        if( (transform.position.x >= MinimapCam.position.x-MinimapSize && transform.position.x <= MinimapCam.position.x+MinimapSize) &&
            (transform.position.z >= MinimapCam.position.z-MinimapSize && transform.position.z <= MinimapCam.position.z+MinimapSize)){
            spriteRenderer.sprite =Mark;
        }else{
            spriteRenderer.sprite =Guild;
        }
	}

	void LateUpdate () {
		transform.position = new Vector3 (
			Mathf.Clamp(transform.position.x, MinimapCam.position.x-MinimapSize, MinimapSize+MinimapCam.position.x),
			transform.position.y,
			Mathf.Clamp(transform.position.z, MinimapCam.position.z-MinimapSize, MinimapSize+MinimapCam.position.z)
		);
	}
}
