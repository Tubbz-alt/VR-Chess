using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollide : MonoBehaviour
{
    public float delay;
    public bool destroyOnCollision;

    // Start is called before the first frame update
    void Start()
    {
        if (!destroyOnCollision)
            StartCoroutine(destroyAfterDelay());
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(destroyAfterDelay());
    }


    IEnumerator destroyAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
}
