using UnityEngine;

public class Block : MonoBehaviour
{
    public ParticleSystem particles;
    public AudioClip destruccionClip;
    private AudioSource audioSource;

    [SerializeField] private Contador puntaje;

    void Start()
    {
        puntaje = FindObjectOfType<Contador>();
        audioSource = GetComponent<AudioSource>();

        if (destruccionClip == null)
        {
            destruccionClip = Resources.Load<AudioClip>("Audio/Sonido_bloque");
            if (destruccionClip == null)
            {
                Debug.LogWarning("No se pudo cargar el audio 'sonido_bloque'. Verifica la ruta y que el archivo esté en Resources/Audio.");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Ahora solo decrementamos el contador en el GameManager
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.DecrementBlockCount();
        }

        // Subir el contador de puntaje
        puntaje.subircontador();

        // Reproducir el sonido de destrucción
        if (audioSource != null && destruccionClip != null)
        {
            audioSource.PlayOneShot(destruccionClip);
        }

        // Instanciar el efecto de partículas en la posición del bloque
        Instantiate(particles, transform.position, transform.rotation);

        // Deshabilitar el renderizado y el collider para hacer "desaparecer" el bloque
        Renderer render = GetComponent<Renderer>();
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        render.enabled = false;
        collider.enabled = false;

        // Destruir el objeto después de un breve delay para que se pueda reproducir el sonido y el efecto
        Destroy(gameObject, 0.2f);
    }
}
