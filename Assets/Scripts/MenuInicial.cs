using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Salir()
    {
        Debug.Log("Salir");
   #if UNITY_EDITOR
        
        UnityEditor.EditorApplication.isPlaying = false;
#else
        
        Application.Quit();
#endif
    }


    public void Infinito()
    {
        SceneManager.LoadScene("EscenaInfinita");
    }


    public void atras()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void VolverMenu()
    {
        SceneManager.LoadScene("MenuInicio");
    }


    public void Tutorial()
    {
         SceneManager.LoadScene("Tutorial");
    }

   
}
