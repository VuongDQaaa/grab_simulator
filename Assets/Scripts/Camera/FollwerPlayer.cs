using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollwerPlayer : MonoBehaviour
{
    public Transform player;
    public float OffsetY;

    [System.Obsolete]
    private void Update(){
        if(player != null) {
            this.transform.position = new Vector3(player.position.x, player.position.y + OffsetY, player.position.z);
            this.transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
        }
    }
}