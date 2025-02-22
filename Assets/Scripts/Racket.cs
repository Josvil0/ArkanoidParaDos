using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    // Movement Speed
    public float speed = 150;
    private bool invertido = false;
    private SpriteRenderer spriteRenderer;
    void FixedUpdate()
    {
        // Get Horizontal Input
        float h = Input.GetAxisRaw("Horizontal");

        if (invertido)
        {
            h = -h;
        }

        // Set Velocity (movement direction * speed)
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.right * h * speed;
    }

     public void InvertirControles(float duration)
    {
        StartCoroutine(InvertirTemporizador(duration));
    }

    private IEnumerator InvertirTemporizador(float duration)
    {
        invertido = true;
        Sprite sprite = Resources.Load<Sprite>("invertido");
         spriteRenderer.sprite = sprite;
        yield return new WaitForSeconds(duration);
        invertido = false;
        sprite = Resources.Load<Sprite>("original");
        spriteRenderer.sprite = sprite;
    }
}
