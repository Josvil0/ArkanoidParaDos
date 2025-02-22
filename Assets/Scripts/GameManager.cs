using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int totalObjetivos = 0;  // Solo cuenta los bloques rojos (objetivo)
    public GameObject ball;
    public GameObject ballPrefab;
    public Image corazon_1, corazon_2, corazon_3;
    public int vidas = 3;
    private PowerUpManager powerUpManager;

    void Start()
    {
        if (corazon_1 == null)
            corazon_1 = GameObject.Find("Corazon_1").GetComponent<Image>();
        if (corazon_2 == null)
            corazon_2 = GameObject.Find("Corazon_2").GetComponent<Image>();
        if (corazon_3 == null)
            corazon_3 = GameObject.Find("Corazon_3").GetComponent<Image>();

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

        // Obtener referencia al PowerUpManager
        powerUpManager = FindObjectOfType<PowerUpManager>();

        // Asegúrate de que powerUpManager no sea null antes de llamar a InstanciarBola
        if (powerUpManager != null)
        {
            InstanciarBola();
        }
        else
        {
            Debug.LogError("PowerUpManager no encontrado.");
        }
    }

    void Update()
    {
        Ball ball = FindObjectOfType<Ball>();
        if (ball == null)
        {
            vidas--;
            InstanciarBola();
        }

        if (vidas == 2)
        {
            corazon_3.sprite = Resources.Load<Sprite>("Corazon_Vacio");
        }
        else if (vidas == 1)
        {
            corazon_2.sprite = Resources.Load<Sprite>("Corazon_Vacio");
        }
        else if (vidas == 0)
        {
            corazon_1.sprite = Resources.Load<Sprite>("Corazon_Vacio");
            Debug.Log("¡Has perdido!");
            SceneManager.LoadScene("Scenes/GameOver");
        }
    }

    // Método para instanciar una nueva bola
    void InstanciarBola()
    {
        if (vidas > 0 && ballPrefab != null)
        {
            GameObject newBall = Instantiate(ballPrefab, new Vector3(40, 23, 0), Quaternion.identity);
            if (powerUpManager != null)
            {
                powerUpManager.SetBall(newBall);
            }
            else
            {
                Debug.LogError("PowerUpManager no encontrado.");
            }
        }
        else if (ballPrefab == null)
        {
            Debug.LogError("El prefab de la bola no está asignado en el inspector.");
        }
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