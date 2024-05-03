using UnityEngine;

public class Explode : MonoBehaviour
{
    private float _force = 250;
    private float _radius = 350;

    public void Boom(Vector3 explosionPosition, Cube cube)
    {
        float currentRadius = _radius * cube.ExplosionFactor;
        float currentForce = _force * cube.ExplosionFactor;

        Collider[] colliders = Physics.OverlapSphere(explosionPosition, _radius);

        foreach (Collider collider in colliders)
        {
            Rigidbody rigidbody = collider.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                Vector3 direction = (collider.transform.position - explosionPosition).normalized;
                rigidbody.AddExplosionForce(currentForce, direction, currentRadius);
            }
        }
    }
}
