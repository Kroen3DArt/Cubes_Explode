using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public event Action<Cube> Click;

    private void OnMouseDown()
    {
        Click?.Invoke(this);
        Destroy(gameObject);
    }
}
