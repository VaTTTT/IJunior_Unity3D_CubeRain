using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _minimalDetonationTime;
    [SerializeField] private float _maximalDetonationTime;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    private Color _initialColor;
    private float _transparencyDefaultValue;
    private Renderer _renderer;
    private float _delay;

    private Collider[] _colliders = new Collider[100];

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _initialColor = _renderer.material.color;
        _transparencyDefaultValue = 1;
    }

    private void OnEnable()
    {
        _delay = Random.Range(_minimalDetonationTime, _maximalDetonationTime);
        StartCoroutine(nameof(ChangingTransparency));
        StartCoroutine(nameof(Detonate));
    }

    private void OnDisable()
    {
        ResetState();
    }

    public IEnumerator Detonate()
    {
        yield return new WaitForSeconds(_delay);

        int collidersNumber = Physics.OverlapSphereNonAlloc(transform.position, _explosionRadius, _colliders);

        for (int i = 0; i < collidersNumber; i++)
        {
            if (_colliders[i].TryGetComponent(out Rigidbody rigidBody))
            {
                rigidBody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }

        gameObject.SetActive(false);
    }

    private IEnumerator ChangingTransparency()
    {
        float transparencyCurrentValue = _transparencyDefaultValue;
        float transparencyTargetValue = 0;

        Color tempColor = _renderer.material.color;

        while (_renderer.material.color.a > 0)
        {
            transparencyCurrentValue = Mathf.MoveTowards(transparencyCurrentValue, transparencyTargetValue, _transparencyDefaultValue / _delay * Time.deltaTime);
            tempColor.a = transparencyCurrentValue;
            _renderer.material.color = tempColor;

            yield return null;
        }
    }

    private void ResetState()
    {
        _renderer.material.color = _initialColor;
    }
}