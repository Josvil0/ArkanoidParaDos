using UnityEngine;

public class Block : MonoBehaviour
{
    public enum TipoBloque { Objetivo, PowerUp, Normal, Transformable, DoblePuntos, InvertirControles }
    public TipoBloque tipo;

    public ParticleSystem particles;
    public AudioClip destruccionClip;
    private AudioSource audioSource;

    [SerializeField] private Contador puntaje;
    [SerializeField] private GameObject[] powerUpPrefabs;  // Lista de PowerUps posibles

    private PowerUpManager powerUpManager;

    void Start()
    {
        puntaje = FindObjectOfType<Contador>();
        audioSource = GetComponent<AudioSource>();
        powerUpManager = FindObjectOfType<PowerUpManager>();

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

            if (tipo == TipoBloque.Transformable)
            {
                tipo = TipoBloque.Normal;
                puntaje.subircontador(20);
                if (powerUpManager != null && powerUpManager.isNeon)
                {
                    puntaje.subircontador(40); // Duplicar los puntos si el rastro de neón está activo
                }
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Bloque_Morado");
                return;
            }
            else if (tipo == TipoBloque.Objetivo)
            {
                gameManager.DecrementBlockCount();
            }
            else if (tipo == TipoBloque.PowerUp)
            {
                ActivarPowerUp();
            }
            else if (tipo == TipoBloque.DoblePuntos)  // Bloques verdes dan doble puntos
            {
                puntaje.subircontador(40);

                if (powerUpManager != null && powerUpManager.isNeon)
                {
                    puntaje.subircontador(80); // Duplicar los puntos si el rastro de neón está activo
                }
            }
            else if (tipo == TipoBloque.InvertirControles)  // Bloques azules invierten controles
            {
                ActivarCambioDeControl();
            }
            else if (tipo == TipoBloque.Normal)
            {
                puntaje.subircontador(10);
                if (powerUpManager != null && powerUpManager.isNeon)
                {
                    puntaje.subircontador(20); // Duplicar los puntos si el rastro de neón está activo
                }
            }
        }




        puntaje.subircontador(10);

        if (audioSource != null && destruccionClip != null)
        {
            audioSource.PlayOneShot(destruccionClip);
        }

        Instantiate(particles, transform.position, transform.rotation);
        GetComponent<Renderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        enabled = false;
    }

    void ActivarCambioDeControl()
    {
        Racket racket = FindObjectOfType<Racket>();
        if (racket != null)
        {
            racket.InvertirControles(5f); // Invierte los controles por 5 segundos
        }
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
