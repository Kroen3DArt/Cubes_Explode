using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RandomColors), typeof(Rigidbody))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private Explode _explode;
    [SerializeField] private GameObject _explosionVFX;

    private float _sizeReduction = 2;
    private int _minCountCubes = 2;
    private int _maxCountCubes = 6;
    private float _maxSeparateChance = 100;
    private int _separateFactor = 2;
    private float _separateChance;
    private List<Rigidbody> _cubesForThrow = new List<Rigidbody>();
    private float _explosionFactor = 1f;

    public float GetExplosionFactor() => _explosionFactor;

    private void OnEnable()
    {
        _separateChance = _maxSeparateChance;
        _cube.Click += SplitCube;
    }

    private void OnDisable()
    {
        _cube.Click -= SplitCube;
    }

    private void SplitCube(Cube cube)
    {
        if (Random.Range(0, _maxSeparateChance) <= _separateChance)
        {
            int countCubesCreate = Random.Range(_minCountCubes, _maxCountCubes + 1);

            for (int i = 0; i < countCubesCreate; i++)
                CreateCubes(cube);

            foreach (Rigidbody newCube in _cubesForThrow)
                newCube.AddForce(cube.transform.position);
        }
        else
        {
            _explode.Boom(cube.transform.position);
            GameObject explosionVFX = Instantiate(_explosionVFX, transform.position, transform.rotation);
            Destroy(explosionVFX, 2);
        }
    }

    private Cube CreateCubes(Cube cube)
    {
        var offsetX = Random.Range(-1f, 1f);
        var offsetY = 0.1f;
        var offsetZ = Random.Range(-1f, 1f);

        Cube newCube = Instantiate(cube, gameObject.transform.position + new Vector3(offsetX, offsetY, offsetZ), Quaternion.identity);
        newCube.transform.localScale /= _sizeReduction;
        newCube.GetComponent<RandomColors>().SetRandomColor();
        newCube.GetComponent<Spawner>()._separateChance = _separateChance / _separateFactor;
        _explosionFactor++;
        _cubesForThrow.Add(newCube.GetComponent<Rigidbody>());

        return newCube;
    }
}
