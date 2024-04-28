using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class RandomColors : MonoBehaviour
{
    public void SetRandomColor() => GetComponent<Renderer>().material.color = GetColor();

    private Color GetColor()
    {
        Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        return color;
    }

}
