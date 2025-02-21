using UnityEngine;

public class Block : MonoBehaviour
{
    public enum TipoBloque { Objetivo, PowerUp, Normal }
    public TipoBloque tipo;

    public ParticleSystem particles;
    public AudioClip destruccionClip;
    private AudioSource audioSource;

    [SerializeField] private Contador puntaje;
    [SerializeField] private GameObject[] powerUpPrefabs;  // Lista de PowerUps posibles

    void Start()
    {
        puntaje = FindObjectOfType<Contador>();
        audioSource = GetComponent<AudioSource>();

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
            if (tipo == TipoBloque.Objetivo)
            {
                gameManager.DecrementBlockCount();
            }
            else if (tipo == TipoBloque.PowerUp)
            {
                ActivarPowerUp();  // Activamos el PowerUp aleatorio cuando se rompe el bloque
            }
        }

        puntaje.subircontador();

        if (audioSource != null && destruccionClip != null)
        {
            audioSource.PlayOneShot(destruccionClip);
        }

        Instantiate(particles, transform.position, transform.rotation);

        Renderer render = GetComponent<Renderer>();
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        render.enabled = false;
        collider.enabled = false;
        enabled = false;
    }

void ActivarPowerUp()
{
    if (powerUpPrefabs.Length > 0)
    {
        // Elegir aleatoriamente un power-up
        int randomIndex = Random.Range(0, powerUpPrefabs.Length);
        GameObject powerUpInstanciado = Instantiate(powerUpPrefabs[randomIndex], transform.position, Quaternion.identity);

        // Hacer que el power-up caiga con física pero no interactúe con los demás objetos
        Rigidbody2D rb = powerUpInstanciado.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = false;  // El power-up caerá, pero no interactuará con todo el mundo
        }

        // Asegúrate de que el power-up sea recogido solo por la raqueta y la bola
        Collider2D col = powerUpInstanciado.GetComponent<Collider2D>();
        if (col != null)
        {
            col.isTrigger = true;  // Usamos un trigger para que el power-up no colisione físicamente
        }
    }
}



}
