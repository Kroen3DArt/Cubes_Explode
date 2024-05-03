using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Cube> _cubes;
    [SerializeField] private Explode _explode;
    [SerializeField] private GameObject _explosionVFX;

    private float _sizeReduction = 2;
    private int _minCountCubes = 2;
    private int _maxCountCubes = 6;
    private readonly List<Rigidbody> _cubesForThrow = new();

    private void Awake()
    {
        RandomColors[] randomColors = FindObjectsOfType<RandomColors>();

        for (int i = 0; i < randomColors.Length; i++)
            randomColors[i].SetRandomColor();
    }

    private void OnEnable()
    {
        foreach (var cube in _cubes)
            cube.Click += SplitCube;
    }

    private void OnDisable()
    {
        foreach (var cube in _cubes)
            cube.Click -= SplitCube;
    }

    private void SplitCube(Cube cube)
    {
        if (Random.Range(0, cube.MaxSeparateChance) <= cube.SplitChance())
        {
            int countCubesCreate = Random.Range(_minCountCubes, _maxCountCubes + 1);

            for (int i = 0; i < countCubesCreate; i++)
                CreateCubes(cube);

            foreach (Rigidbody newCube in _cubesForThrow)
            {
                if (newCube != null)
                    newCube.AddForce(cube.transform.position);
            }
        }
        else
        {
            GetComponent<Explode>().Boom(cube.transform.position, cube);
            GameObject explosionVFX = Instantiate(_explosionVFX, cube.transform.position, transform.rotation);
            Destroy(explosionVFX, 2);
        }
    }

    private Cube CreateCubes(Cube cube)
    {
        var offsetX = Random.Range(-1f, 1f);
        var offsetY = 0.1f;
        var offsetZ = Random.Range(-1f, 1f);

        Cube newCube = Instantiate(cube, cube.transform.position + new Vector3(offsetX, offsetY, offsetZ), Quaternion.identity);
        newCube.transform.localScale /= _sizeReduction;
        newCube.GrowExplosionFactor();
        newCube.Click += SplitCube;
        newCube.SplitChance();
        _cubesForThrow.Add(newCube.GetComponent<Rigidbody>());

        return newCube;
    }
}
