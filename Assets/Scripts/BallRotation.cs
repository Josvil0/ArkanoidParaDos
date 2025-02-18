using UnityEngine;

public class BallRotation : MonoBehaviour
{
    public Rigidbody2D rb;
    public float rotationMultiplier = 10f; // Ajusta el valor para una rotación más realista

    void Update()
    {
        float speed = rb.linearVelocity.magnitude;
        transform.Rotate(0, 0, speed * rotationMultiplier * Time.deltaTime);
    }
}
