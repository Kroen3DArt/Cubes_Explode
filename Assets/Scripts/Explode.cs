using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] private float _force = 10f;
    [SerializeField] private float _radius = 5f;

    public void Boom(Vector3 explosionPosition)
    {
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, _radius);

        foreach (Collider collider in colliders)
        {
            Rigidbody rigidbody = collider.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                Vector3 direction = (collider.transform.position - explosionPosition).normalized;
                rigidbody.AddForce(direction * _force, ForceMode.Impulse);
            }
        }
    }
}
