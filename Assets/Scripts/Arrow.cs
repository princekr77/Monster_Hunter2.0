using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Stop physics so arrow can "stick"
        Destroy(GetComponent<Rigidbody>());

        if (other.CompareTag("Widow"))
        {
            transform.parent = other.transform; // stick to widow
            other.GetComponent<widow>().TakeDamage(20);
        }

        // Disappear only after collision
        StartCoroutine(DisappearAfterHit());
    }

    private IEnumerator DisappearAfterHit()
    {
        yield return new WaitForSeconds(2f); // wait 2 sec
        Destroy(gameObject); // remove only this arrow
    }
}
