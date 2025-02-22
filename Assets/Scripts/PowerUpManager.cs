using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private Rigidbody2D ballRigidbody;
    private Transform raquetaTransform;
    public bool isNeon = false;
    
    // Para efectos de duración
    private float powerUpDuration = 5f; // Duración de los efectos (en segundos)
    private Vector3 originalRaquetaScale;

    private void Start()
    {
        raquetaTransform = GameObject.FindWithTag("Raqueta").transform;
        originalRaquetaScale = raquetaTransform.localScale;
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
        }
    }

    public void ActivarModoChill()
    {
        if (ballRigidbody != null)
        {
            // Reducir velocidad de la bola
            ballRigidbody.linearVelocity *= 0.7f;

            // Cambiar el color de la cámara
            Camera.main.backgroundColor = new Color(0.2f, 0.2f, 0.7f); // Azul/morado

            // Aquí podrías iniciar una duración para que vuelva a la normalidad después de un tiempo
            Invoke("RestaurarModoChill", powerUpDuration);
        }
    }

    void RestaurarModoChill()
    {
        if (ballRigidbody != null)
        {
            ballRigidbody.linearVelocity /= 0.7f; // Restaurar la velocidad
            Camera.main.backgroundColor = Color.black; // Restaurar el color
        }
    }

    public void ActivarRastroDeNeon()
    {
        if (ballRigidbody != null)
        {
            // Activar el rastro de neón en la bola
            var trailRenderer = ballRigidbody.gameObject.GetComponent<TrailRenderer>();
            if (trailRenderer == null)
            {
                trailRenderer = ballRigidbody.gameObject.AddComponent<TrailRenderer>();
            }

            trailRenderer.enabled = true;
            trailRenderer.startColor = Color.cyan;
            trailRenderer.endColor = Color.blue;
            trailRenderer.time = 2f; // Duración del rastro
            trailRenderer.widthMultiplier = 10f; // Ancho del rastro

            isNeon = true;

            // Desactivar después de un tiempo
            Invoke("DesactivarRastroDeNeon", powerUpDuration);
        }
    }

    void DesactivarRastroDeNeon()
    {
        if (ballRigidbody != null)
        {
            var trailRenderer = ballRigidbody.gameObject.GetComponent<TrailRenderer>();
            if (trailRenderer != null)
            {
                trailRenderer.enabled = false;
            }

            isNeon = false;
        }
    }

    public void ActivarRalentizacion()
    {
        if (ballRigidbody != null)
        {
            // Reducir la velocidad de la bola
            ballRigidbody.linearVelocity *= 0.7f;

            // Restaurar la velocidad después de un tiempo
            Invoke("RestaurarRalentizacion", powerUpDuration);
        }
    }

    void RestaurarRalentizacion()
    {
        if (ballRigidbody != null)
        {
            ballRigidbody.linearVelocity /= 0.7f; // Restaurar la velocidad
        }
    }

    public void ActivarAceleracion()
    {
        if (ballRigidbody != null)
        {
            // Aumentar la velocidad de la bola
            ballRigidbody.linearVelocity *= 1.3f;

            // Restaurar la velocidad después de un tiempo
            Invoke("RestaurarAceleracion", powerUpDuration);
        }
    }

    void RestaurarAceleracion()
    {
        if (ballRigidbody != null)
        {
            ballRigidbody.linearVelocity /= 1.3f; // Restaurar la velocidad
        }
    }

    public void ActivarMultiBola()
    {
        if (ballRigidbody != null)
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
    }

    void EliminarBolas()
    {
        GameObject[] bolas = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject bola in bolas)
        {
            if (bola != null && bola != ballRigidbody.gameObject)
            {
                Destroy(bola);
            }
        }
    }

    public void ActivarRaquetaExtendida()
    {
        if (raquetaTransform != null)
        {
            raquetaTransform.localScale = new Vector3(2f, 1f, 1f);
            Invoke("RestaurarRaquetaExtendida", powerUpDuration);
        }
    }

    void RestaurarRaquetaExtendida()
    {
        if (raquetaTransform != null)
        {
            raquetaTransform.localScale = originalRaquetaScale; // Restaurar tamaño
        }
    }

    public void ActivarRaquetaReducida()
    {
        if (raquetaTransform != null)
        {
            raquetaTransform.localScale = new Vector3(0.5f, 1f, 1f);
            Invoke("RestaurarRaquetaReducida", powerUpDuration);
        }
    }

    void RestaurarRaquetaReducida()
    {
        if (raquetaTransform != null)
        {
            raquetaTransform.localScale = originalRaquetaScale; // Restaurar tamaño
        }
    }
}