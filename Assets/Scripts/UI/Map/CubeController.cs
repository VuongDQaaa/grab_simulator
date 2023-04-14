using UnityEngine;

public class CubeController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody rb;

    
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.velocity = movement * moveSpeed;
    }
}
