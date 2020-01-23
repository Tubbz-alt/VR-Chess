using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDelay : MonoBehaviour
{
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyAfterDelay(delay));
    }

    IEnumerator DestroyAfterDelay(float delay) {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
