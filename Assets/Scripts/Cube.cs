using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(RandomColors), typeof(Cube))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    private float _separateChance;
    private bool _isSepareted = false;
    private int _explosionFactor;

    public Cube()
    {
        SeparateFactor = 2;
        MaxSeparateChance = 100;
        _separateChance = MaxSeparateChance;
        _explosionFactor = 1;
    }

    public int SeparateFactor { get; private set; }
    public float MaxSeparateChance { get; private set; }

    public event Action<Cube> Click;

    public int GetExplosionFactor() => _explosionFactor;

    public void GrowExplosionFactor() => _explosionFactor++;

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
