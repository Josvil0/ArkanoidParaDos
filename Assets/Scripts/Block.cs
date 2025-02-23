using UnityEngine;

public class Block : MonoBehaviour
{
    public enum TipoBloque { Objetivo, PowerUp, Normal, Transformable, DoblePuntos, InvertirControles, Infinito };
    public TipoBloque tipo;

    public ParticleSystem particles;
    public AudioClip destruccionClip;
    private AudioSource audioSource;
    public bool isInfinito;

    [SerializeField] private Contador puntaje;
    [SerializeField] private GameObject[] powerUpPrefabs;
    private PowerUpManager powerUpManager;
    private Vector3 originalScale;

    void Start()
    {
        puntaje = FindObjectOfType<Contador>();
        audioSource = GetComponent<AudioSource>();
        powerUpManager = FindObjectOfType<PowerUpManager>();
        originalScale = transform.localScale;

        if (destruccionClip == null)
        {
            destruccionClip = Resources.Load<AudioClip>("Audio/Sonido_bloque");
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        GameManager gameManager = FindObjectOfType<GameManager>();

        if (gameManager != null)
        {
            // Bloque transformable: cambia de tipo y da puntos extra
            if (tipo == TipoBloque.Transformable)
            {
                tipo = TipoBloque.Normal;
                puntaje.subircontador(20);
                if (powerUpManager != null && powerUpManager.isNeon)
                {
                    puntaje.subircontador(40);
                }
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Bloque_Morado");
                return;
            }

            // Bloque objetivo: disminuye el conteo de bloques restantes
            if (tipo == TipoBloque.Objetivo)
            {
                gameManager.DecrementBlockCount();
            }

            // Bloque PowerUp: activa el power-up correspondiente
            if (tipo == TipoBloque.PowerUp)
            {
                ActivarPowerUp();
            }

            // Bloque DoblePuntos: duplica la puntuación
            if (tipo == TipoBloque.DoblePuntos)
            {
                puntaje.subircontador(40);
                if (powerUpManager != null && powerUpManager.isNeon)
                {
                    puntaje.subircontador(80);
                }
            }

            // Bloque InvertirControles: invierte los controles del jugador
            if (tipo == TipoBloque.InvertirControles)
            {
                ActivarCambioDeControl();
            }

            // Bloque Normal: da puntos al jugador
            if (tipo == TipoBloque.Normal)
            {
                puntaje.subircontador(10);
                if (powerUpManager != null && powerUpManager.isNeon)
                {
                    puntaje.subircontador(20);
                }
            }

            // Desactiva el bloque en lugar de destruirlo
            gameObject.SetActive(false);

            // Si es un bloque infinito, programamos su regeneración
            if (isInfinito)
            {
                Invoke("RegenerarBloque", 5f);
            }
        }

        // Efectos de sonido y partículas al romperse
        if (audioSource != null && destruccionClip != null)
        {
            audioSource.PlayOneShot(destruccionClip);
        }

        Instantiate(particles, transform.position, transform.rotation);
    }

    void RegenerarBloque()
    {
        // Reactivamos el mismo bloque en lugar de crear uno nuevo
        gameObject.SetActive(true);
        transform.localScale = originalScale; // Restauramos su tamaño original

        // Reiniciamos la física para evitar problemas de colisión
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        Debug.Log("Bloque regenerado correctamente en: " + transform.position);
    }

    void ActivarCambioDeControl()
    {
        Racket racket = FindObjectOfType<Racket>();
        if (racket != null)
        {
            racket.InvertirControles(5f);
        }
    }

    void ActivarPowerUp()
    {
        if (powerUpPrefabs.Length > 0)
        {
            int randomIndex = Random.Range(0, powerUpPrefabs.Length);
            GameObject powerUpInstanciado = Instantiate(powerUpPrefabs[randomIndex], transform.position, Quaternion.identity);

            Rigidbody2D rb = powerUpInstanciado.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }

            Collider2D col = powerUpInstanciado.GetComponent<Collider2D>();
            if (col != null)
            {
                col.isTrigger = true;
            }
        }
    }
}
