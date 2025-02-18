using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Movement Speed
    public float speed = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.up * speed;
        StartCoroutine(IncreaseSpeedOverTime());
    }

    float hitFactor(Vector2 ballPos, Vector2 racketPos, float racketWidth)
    {
        //
        // 1  -0.5  0  0.5   1  <- x value
        // ===================  <- racket
        //
        return (ballPos.x - racketPos.x) / racketWidth;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        // Hit the Racket?
        if (col.gameObject.name == "racket")
        {
            // Calculate hit Factor, direction of the ball
            float x = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.x);

            // Calculate direction, set length to 1
            Vector2 dir = new Vector2(x, 1).normalized;

            // Set Velocity with dir * speed
            GetComponent<Rigidbody2D>().linearVelocity = dir * speed;
        }
    }
    
    void Update()
    {
    // Verifica la posición de la bola en cada frame
        if (this.transform.position.y < -110)
        {
            Debug.Log("You lose!");
            SceneManager.LoadScene("Scenes/GameOver");
            if (SceneManager.GetActiveScene().name.Equals("GameOver"))
        {
                Debug.Log("QUIT!"); 
                Application.Quit(); 
            
        }
        } 
    }
    
    
    IEnumerator IncreaseSpeedOverTime()
    {
        while (true)
        {
            if (SceneManager.GetActiveScene().name.Equals("Scene2"))
            {
                yield return new WaitForSeconds(10);
                speed += 13;
                Debug.Log("VELCOIDAD ACTUAL"+speed);
                GetComponent<Rigidbody2D>().linearVelocity = GetComponent<Rigidbody2D>().linearVelocity.normalized * speed;
            }
            else
            {
            yield return new WaitForSeconds(20);
            speed += 10;
            Debug.Log("VELCOIDAD ACTUAL"+speed);
            GetComponent<Rigidbody2D>().linearVelocity = GetComponent<Rigidbody2D>().linearVelocity.normalized * speed;
                
            }
        }
    }
    
}
