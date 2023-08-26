using UnityEngine;
using UnityEngine.Events;

public class Spawner : ObjectPool
{
    [SerializeField] private GameObject _baloonPrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _initSecondsBetweenSpawn;

    [SerializeField] private float _timeStepToDecreaseSpawnTime;
    [SerializeField] private float _spawnTimeDecreaseStep;

    [SerializeField] private Timer _timer;
    private float _elapsedTime = 0;
    private float _currentSecondsBetweenSpawn;

    private void Start()
    {
        Initialize(_baloonPrefab);
        _currentSecondsBetweenSpawn = _initSecondsBetweenSpawn;
    }
    private void Update()
    {

        UpdateSpawnDelay();

        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _currentSecondsBetweenSpawn)
        {
            if (TryGetObject(out GameObject baloon))
            {
                _elapsedTime = 0;

                int spawnPointNumber = Random.Range(0, _spawnPoints.Length);

                SetBaloon(baloon, _spawnPoints[spawnPointNumber].position);
            }
        }
    }
    private void UpdateSpawnDelay()
    {
        int speedMultiplier = (int)_timer.CurrentTime / (int)_timeStepToDecreaseSpawnTime;

        float currentSpeedToApply = _initSecondsBetweenSpawn - _spawnTimeDecreaseStep * speedMultiplier;
        
        if (_currentSecondsBetweenSpawn > currentSpeedToApply && currentSpeedToApply >= 1.0f)
        {
            _currentSecondsBetweenSpawn = currentSpeedToApply;
        }
    }
    private void SetBaloon(GameObject ballon, Vector3 spawnPoint)
    {
        ballon.SetActive(true);
        ballon.transform.position = spawnPoint;
    }
}
