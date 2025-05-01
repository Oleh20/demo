using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public event System.Action<TargetModel> OnTargetClicked;

    [SerializeField] private GameSettings _settings;
    [SerializeField] private TargetView _targetPrefab;
    [SerializeField] private BoxCollider _spawnArea;

    private ObjectPool<TargetView> _targetPool;
    float _spawnInterval;
    private void Awake()
    {
        _targetPool = new ObjectPool<TargetView>(_targetPrefab, 30);
    }
    private void Start()
    {
        _spawnInterval = _settings.spawnInterval;
        InvokeRepeating(nameof(SpawnTarget), 1f, _spawnInterval);
    }

    private void SpawnTarget()
    {
        Vector3 spawnPosition = GetRandomPointInBounds(_spawnArea.bounds);
        spawnPosition.y = _spawnArea.bounds.min.y + 0.5f;

        TargetView view = _targetPool.Get();
        view.transform.position = spawnPosition;
        view.transform.rotation = Quaternion.identity;
        view.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

        Vector3 direction = Vector3.up + new Vector3(Random.Range(-0.2f, 0.2f), 0, Random.Range(-0.2f, 0.2f));
        direction.Normalize();

        float force = Random.Range(_settings.impulseMin, _settings.impulseMax);
        view.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);

        TargetType type = Random.value > 0.3f ? TargetType.Good : TargetType.Bad;
        TargetModel model = new TargetModel(type);

        view.Initialize(model, ReportClick, _settings.targetLifetime, ReturnToPool);
    }
    private void ReturnToPool(TargetView view)
    {
        _targetPool.Return(view);
    }
    private Vector3 GetRandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            bounds.min.y,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
    public void DecreaseSpawnInterval(float amount)
    {
        _spawnInterval = Mathf.Max(0.5f, _spawnInterval - amount);

        StopSpawn();
        InvokeRepeating(nameof(SpawnTarget), _spawnInterval, _spawnInterval);
    }
    public void StopSpawn()
    {
        CancelInvoke(nameof(SpawnTarget));
    }
    public void ReportClick(TargetModel model)
    {
        OnTargetClicked?.Invoke(model);
    }
}
