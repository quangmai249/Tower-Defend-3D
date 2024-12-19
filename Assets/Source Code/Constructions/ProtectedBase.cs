using UnityEngine;

public class ProtectedBase : MonoBehaviour
{
    [SerializeField] ParticleSystem parAttacked;
    private GameManager gameManager;
    private AudioManager audioManager;
    private GameStats gameStats;
    private void Start()
    {
        gameManager = GameManager.Instance;
        audioManager = AudioManager.Instance;
        gameStats = gameManager.GameStats;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (parAttacked.isPlaying == false)
                parAttacked.Play();

            gameStats.Lives -= 1;
            audioManager.ActiveAudioProtectedBaseTakeDamage(true);

            other.gameObject.SetActive(false);
        }
    }
}
