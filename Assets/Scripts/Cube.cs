using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(RandomColors), typeof(Cube))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    public event Action<Cube> Click;

    private float _separateChance;
    private bool _isSepareted = false;

    public int ExplosionFactor { get; private set; }
    public int SeparateFactor { get; private set; }
    public float MaxSeparateChance { get; private set; }

    private void Awake()
    {
        SeparateFactor = 2;
        MaxSeparateChance = 100;
        _separateChance = MaxSeparateChance;
        ExplosionFactor = 1;
    }

    public void GrowExplosionFactor() => ExplosionFactor++;

    public float SplitChance()
    {
        if (_isSepareted == true)
            _separateChance /= SeparateFactor;
        else
            _isSepareted = true;

        return _separateChance;
    }

    private void OnMouseDown()
    {
        Click?.Invoke(this);
        Destroy(gameObject);
    }
}
