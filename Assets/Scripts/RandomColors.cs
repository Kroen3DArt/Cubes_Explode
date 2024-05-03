using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class RandomColors : MonoBehaviour
{
    public void SetRandomColor() => GetComponent<Renderer>().material.color = CreateRandomColor();

    private Color CreateRandomColor() => new Color(Random.value, Random.value, Random.value);
}
