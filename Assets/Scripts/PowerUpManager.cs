using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D ballRigidbody;
    private Transform raquetaTransform;
    
    // Para efectos de duración
    private float powerUpDuration = 5f; // Duración de los efectos (en segundos)

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        raquetaTransform = GameObject.FindWithTag("Raqueta").transform;

    }

    public void SetBall(GameObject ball)
    {
        ballRigidbody = ball.GetComponent<Rigidbody2D>();
    }

    public void ActivarPowerUp(PowerUp.TipoPowerUp tipo)
    {
        switch (tipo)
        {
            case PowerUp.TipoPowerUp.ModoChill:
                ActivarModoChill();
              break;
            case PowerUp.TipoPowerUp.RastroDeNeon:
                ActivarRastroDeNeon();
                break;
            case PowerUp.TipoPowerUp.Ralentizacion:
                ActivarRalentizacion();
                break;
            case PowerUp.TipoPowerUp.Aceleracion:
                ActivarAceleracion();
                break;
            case PowerUp.TipoPowerUp.MultiBola:
                ActivarMultiBola();
                break;
            case PowerUp.TipoPowerUp.RaquetaExtendida:
                ActivarRaquetaExtendida();
                break;
            case PowerUp.TipoPowerUp.RaquetaReducida:
                ActivarRaquetaReducida();
                break;
            case PowerUp.TipoPowerUp.BolaDeFuego:
                ActivarBolaDeFuego();
                break;
        }
    }

    public void ActivarModoChill()
    {
        // Reducir velocidad de la bola
        ballRigidbody.linearVelocity *= 0.7f;

        // Cambiar el color de la cámara
        Camera.main.backgroundColor = new Color(0.2f, 0.2f, 0.7f); // Azul/morado

        // Aquí podrías iniciar una duración para que vuelva a la normalidad después de un tiempo
        Invoke("RestaurarModoChill", powerUpDuration);
    }

    void RestaurarModoChill()
    {
        ballRigidbody.linearVelocity /= 0.7f; // Restaurar la velocidad
        Camera.main.backgroundColor = Color.black; // Restaurar el color
    }

    public void ActivarRastroDeNeon()
    {
        // Activar el rastro de neón en la bola
        var trailRenderer = ballRigidbody.gameObject.GetComponent<TrailRenderer>();
        if (trailRenderer != null)
        {
            trailRenderer.enabled = true;
            trailRenderer.startColor = Color.cyan;
            trailRenderer.endColor = Color.blue;
        }

        // Desactivar después de un tiempo
        Invoke("DesactivarRastroDeNeon", powerUpDuration);
    }

    void DesactivarRastroDeNeon()
    {
        var trailRenderer = ballRigidbody.gameObject.GetComponent<TrailRenderer>();
        if (trailRenderer != null)
        {
            trailRenderer.enabled = false;
        }
    }

    public void ActivarRalentizacion()
    {
        // Reducir la velocidad de la bola
        ballRigidbody.linearVelocity *= 0.7f;

        // Restaurar la velocidad después de un tiempo
        Invoke("RestaurarRalentizacion", powerUpDuration);
    }

    void RestaurarRalentizacion()
    {
        ballRigidbody.linearVelocity /= 0.7f; // Restaurar la velocidad
    }

    public void ActivarAceleracion()
    {
        // Aumentar la velocidad de la bola
        ballRigidbody.linearVelocity *= 1.3f;

        // Restaurar la velocidad después de un tiempo
        Invoke("RestaurarAceleracion", powerUpDuration);
    }

    void RestaurarAceleracion()
    {
        ballRigidbody.linearVelocity /= 1.3f; // Restaurar la velocidad
    }

    public void ActivarMultiBola()
    {
        // Crear bolas adicionales
        for (int i = 0; i < 2; i++)
        {
            GameObject nuevaBola = Instantiate(ballRigidbody.gameObject, ballRigidbody.transform.position, Quaternion.identity);
            Rigidbody2D rb = nuevaBola.GetComponent<Rigidbody2D>();
            rb.linearVelocity = new Vector2(Random.Range(-2f, 2f), 4f); // Ajusta la dirección de las bolas
        }

        // Eliminar las bolas después de un tiempo
    }

    void EliminarBolas()
    {
        GameObject[] bolas = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject bola in bolas)
        {
            if (bola != ballRigidbody.gameObject) Destroy(bola);
        }
    }

    public void ActivarRaquetaExtendida()
    {
        raquetaTransform.localScale = new Vector3(2f, 1f, 1f);
        Invoke("RestaurarRaquetaExtendida", powerUpDuration);
    }

    void RestaurarRaquetaExtendida()
    {
        raquetaTransform.localScale = new Vector3(1f, 1f, 1f); // Restaurar tamaño
    }

    public void ActivarRaquetaReducida()
    {
        raquetaTransform.localScale = new Vector3(0.5f, 1f, 1f);
        Invoke("RestaurarRaquetaReducida", powerUpDuration);
    }

    void RestaurarRaquetaReducida()
    {
        raquetaTransform.localScale = new Vector3(1f, 1f, 1f); // Restaurar tamaño
    }

    public void ActivarBolaDeFuego()
    {
        // Hacer que la bola atraviese bloques
        Collider2D[] coliders = FindObjectsOfType<Collider2D>();
        foreach (Collider2D collider in coliders)
        {
            Physics2D.IgnoreCollision(ballRigidbody.GetComponent<Collider2D>(), collider);
        }

        // Restaurar colisiones después de un tiempo
        Invoke("RestaurarBolaDeFuego", powerUpDuration);
    }

    void RestaurarBolaDeFuego()
    {
        Collider2D[] coliders = FindObjectsOfType<Collider2D>();
        foreach (Collider2D collider in coliders)
        {
            Physics2D.IgnoreCollision(ballRigidbody.GetComponent<Collider2D>(), collider, false);
        }
    }
}
