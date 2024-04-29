using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _radius;

    private Spawner _spawner;

    private void Start()
    {
        _spawner = GetComponent<Spawner>();
    }

    public void Boom(Vector3 explosionPosition)
    {
        float currentExplosionFactor = _spawner.GetExplosionFactor();
        _radius *= currentExplosionFactor;

        Collider[] colliders = Physics.OverlapSphere(explosionPosition, _radius);

        foreach (Collider collider in colliders)
        {
            Rigidbody rigidbody = collider.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                Vector3 direction = (collider.transform.position - explosionPosition).normalized;
                rigidbody.AddForce(direction * (_force * currentExplosionFactor), ForceMode.Impulse);
            }
        }
    }
}
