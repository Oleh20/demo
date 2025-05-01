using UnityEngine;

public class TargetView : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    private TargetModel _targetModel;

    private System.Action<TargetModel> _onClick;
    private System.Action<TargetView> _onReturnToPool;

    public void Initialize(TargetModel model, System.Action<TargetModel> onClick, float targetLifeTime, System.Action<TargetView> onReturn)
    {
        _targetModel = model;
        _onClick = onClick;
        _onReturnToPool = onReturn;

        Color color = model.Type == TargetType.Good ? Color.green : Color.red;
        _renderer.material.color = color;

        Invoke(nameof(Deactivate), targetLifeTime);
    }

    private void OnMouseDown()
    {
        _onClick?.Invoke(_targetModel);
        Destroy(gameObject);
    }
    private void Deactivate()
    {
        CancelInvoke();
        _onReturnToPool?.Invoke(this);
    }
}
