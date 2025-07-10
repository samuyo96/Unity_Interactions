using System.Collections;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public float timeToLive = 2f;

    private void OnEnable()
    {
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }
}
