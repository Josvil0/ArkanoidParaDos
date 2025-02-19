using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject BotonPausa;
    [SerializeField] private GameObject menuPausa;
    
    public void Pausa()
    {
        Time.timeScale = 0f;
        BotonPausa.SetActive(false);
        menuPausa.SetActive(true);
    }

    public void Continuar()
    {
        Time.timeScale = 1f;
        BotonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        menuPausa.SetActive(false);
    }

    public void Salir()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuInicio");
    }
}
