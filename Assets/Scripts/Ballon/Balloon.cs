using UnityEngine;

public class Balloon : MonoBehaviour
{
    [SerializeField] ParticleSystem _boomPacticle;

    private Player _player;
    private ParticleController _particleController;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        if (!_player)
        {
            Debug.LogError("Player not found");
            return;
        }

        _particleController = FindObjectOfType<ParticleController>();
        if (!_particleController)
        {
            Debug.LogError("ParticleController not found");
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && collision.gameObject.TryGetComponent(out Bound bound))
        {
            _player.ApplyDamage(1);
            gameObject.SetActive(false);
        }
    }


    public void Destroy(int score)
    {
        _particleController.RunParticleAtLocation(gameObject.transform.position, _boomPacticle);
        _player.AddScore(score);
        gameObject.SetActive(false); 
    }
}
