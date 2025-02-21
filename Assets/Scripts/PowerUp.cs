using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum TipoPowerUp
    {
        ModoChill,
        RastroDeNeon,
        Ralentizacion,
        Aceleracion,
        MultiBola,
        RaquetaExtendida,
        RaquetaReducida,
        BolaDeFuego
    }

    public TipoPowerUp tipoPowerUp;

    private PowerUpManager powerUpManager;

    private void Start()
    {
        powerUpManager = FindObjectOfType<PowerUpManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificamos si el objeto que toca el power-up tiene el tag 'Raqueta' o 'Ball'
        if (other.CompareTag("Raqueta") || other.CompareTag("Ball"))
        {
            Debug.Log($"PowerUp {tipoPowerUp} recogido por {other.gameObject.tag}");  // Verifica si se detecta la colisión

            // Activamos el PowerUp
            if (powerUpManager != null)
            {
                powerUpManager.ActivarPowerUp(tipoPowerUp);
                // Destruir el power-up una vez que haya sido recogido
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("No se encontró PowerUpManager.");
            }
        }
    }
}
