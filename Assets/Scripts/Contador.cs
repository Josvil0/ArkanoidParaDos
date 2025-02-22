using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Contador : MonoBehaviour
{
    private float puntos;
    public TextMeshProUGUI numPoints;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        numPoints = GetComponentInParent<TextMeshProUGUI>();
        if (SceneManager.GetActiveScene().name.Equals("EscenaSegunda"))
        {
            numPoints.text = PlayerPrefs.GetInt("Contador").ToString();
            
        }
        else
        {
        numPoints.text = "000";
            
        }

        
        
    }

    public void subircontador(int puntuacion)
    {
        
        int currentPoints = int.Parse(numPoints.text);
        currentPoints += puntuacion;
        PlayerPrefs.SetInt("Contador", currentPoints);
        numPoints.text = currentPoints.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
