using System.Linq;
using UnityEngine;

public class ExplosiveShot : Shot
{
    public GameObject explosionPrefab;
    public float explosionRadius = 5f;
    public float explosionForce = 30f;
    public float upwardModifier = 1f;

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 collisionPoint = collision.contacts[0].point;
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(collisionPoint, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Target"))
            {
                collider.GetComponent<Rigidbody>().AddExplosionForce
                    (explosionForce, collisionPoint, explosionRadius, upwardModifier, ForceMode.Impulse);
            }
        }
        Destroy(gameObject);
    }
}
