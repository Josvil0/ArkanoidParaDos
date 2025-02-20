using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int totalBlocks; // Ya no es estático, ahora se maneja por instancia

    // Este método será llamado por cada bloque cuando se destruya
    public void DecrementBlockCount()
    {
        totalBlocks--;
        Debug.Log("Total Blocks Decremented: " + totalBlocks);

        if (totalBlocks <= 0)
        {
            Debug.Log("No quedan bloques. Cambiando de escena...");
            // Si ya no quedan bloques, cambiar de escena
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void Start()
    {
        // Contamos los bloques solo al inicio de la escena
        totalBlocks = FindObjectsOfType<Block>().Length;
        Debug.Log("Total Blocks al inicio: " + totalBlocks);
    }
}
