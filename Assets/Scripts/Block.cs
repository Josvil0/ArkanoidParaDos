using TMPro;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Block : MonoBehaviour
{
    public ParticleSystem particles; 
    private static int totalBlocks;

    [SerializeField] private Contador puntaje;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        puntaje = FindObjectOfType<Contador>();
        totalBlocks = FindObjectsByType<Block>(FindObjectsSortMode.None).Length;
        //Debug.Log("Bloques: " + totalBlocks);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
            totalBlocks--;
            puntaje.subircontador();
            //Debug.Log("Bloques: " + totalBlocks);
            if (totalBlocks <= 0)
                 {
                     SceneManager.LoadScene("Scene2");
                     //  LoadNextScene(); // Cuando no queden bloques cambia de escena
                 }
            Renderer render = GetComponent<Renderer>();
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            render.enabled = false;
            collider.enabled = false;

            String colores = GetComponent<SpriteRenderer>().name.Split("_")[1];
            Debug.Log(colores[1].ToString());

            Instantiate(particles, collider.transform.position, collider.transform.rotation);
            /*
            switch (colores)
            {
                case "yellow":
                    
                    break;
                case "red":
                    break;
                case "green":
                    break;
                case "blue":
                    break;
                case "pink":
                    break;
                
            }
            */
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
