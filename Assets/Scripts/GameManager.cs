using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int totalObjetivos = 0;  // Solo cuenta los bloques rojos (objetivo)

    void Start()
    {
        // Contamos solo los bloques de tipo Objetivo
        Block[] todosLosBloques = FindObjectsOfType<Block>();

        foreach (Block bloque in todosLosBloques)
        {
            if (bloque.tipo == Block.TipoBloque.Objetivo)  // Solo contar los bloques rojos (objetivos)
            {
                totalObjetivos++;
            }
        }

        Debug.Log("Total de bloques objetivo al inicio: " + totalObjetivos);
    }

    // Este método será llamado por los bloques ROJOS cuando se destruyan
    public void DecrementBlockCount()
    {
        totalObjetivos--;
        Debug.Log("Bloques objetivo restantes: " + totalObjetivos);

        if (totalObjetivos <= 0)
        {
            Debug.Log("¡Nivel Completado! Cambiando de escena...");
            // Cambiar de escena cuando no haya bloques objetivo
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
