using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _minimalTimeToLive;
    [SerializeField] private float _maximalTimeToLive;
    [SerializeField] private Color _collidedColor;

    private bool _isCollided;
    private Color _initialColor;
    private Renderer _renderer;

    public event UnityAction<Cube> Disabled;

    private void Start()
    {
        _isCollided = false;
        _renderer = GetComponent<Renderer>();
        _initialColor = _renderer.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out _))
        {
            if (_isCollided == false)
            {
                ChangeColor(_collidedColor);
                _isCollided = true;
            }

            StartCoroutine(nameof(SelfDisabling));
        }
    }

    private void ChangeColor(Color color)
    {
        _renderer.material.color = color;
    }

    private IEnumerator SelfDisabling()
    {
        WaitForSeconds delay = new WaitForSeconds(Random.Range(_minimalTimeToLive, _maximalTimeToLive));

        yield return delay;

        Disabled?.Invoke(this);
        ResetState();
        gameObject.SetActive(false);
    }

    private void ResetState()
    {
        _renderer.material.color = _initialColor;
        _isCollided = false;
    }
}