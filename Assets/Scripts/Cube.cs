using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _minimalTimeToLive;
    [SerializeField] private float _maximalTimeToLive;
    [SerializeField] private Color _collidedColor;

    private bool _isCollided;
    private Color _initialColor;
    private Renderer _renderer;

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
            if (!_isCollided)
            {
                ChangeColor(_collidedColor);
                _isCollided = true;
            }

            StartCoroutine(nameof(SelfDisabling));
        }
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    private void ChangeColor(Color color)
    {
        _renderer.material.color = color;
    }

    private IEnumerator SelfDisabling()
    {
        WaitForSeconds delay = new WaitForSeconds(Random.Range(_maximalTimeToLive, _maximalTimeToLive));

        yield return delay;

        Reset();
        gameObject.SetActive(false);
    }

    private void Reset()
    {
        _renderer.material.color = _initialColor;
        _isCollided = false;
    }
}