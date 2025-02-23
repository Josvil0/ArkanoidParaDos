using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Contador : MonoBehaviour
{
    private int puntos;
    public TextMeshProUGUI numPoints;

    // Lista de escenas donde se debe reiniciar el contador
    private string[] escenasReinicio = { "EscenaPrimera", "MenuInicio", "EscenaInfinita" }; 

    void Start()
    {
        numPoints = GetComponentInParent<TextMeshProUGUI>();

        // Si la escena actual está en la lista, reiniciar el contador
        if (EscenaReinicio(SceneManager.GetActiveScene().name))  // Cambiar .buildIndex por .name
        {
            puntos = 0;
            PlayerPrefs.SetInt("Contador", 0);
        }
        else
        {
            puntos = PlayerPrefs.GetInt("Contador", 0);
        }

        numPoints.text = puntos.ToString("000");
    }

    public void subircontador(int puntuacion)
    {
        puntos += puntuacion;
        PlayerPrefs.SetInt("Contador", puntos);
        numPoints.text = puntos.ToString("000");
    }

    private bool EscenaReinicio(string sceneName)  // Ahora recibe un string
    {
        foreach (string name in escenasReinicio)  // Recorre la lista de nombres
        {
            if (sceneName == name)  // Compara nombres en lugar de índices
                return true;
        }
        return false;
    }
}
