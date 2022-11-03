using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    public GameObject particelPrefab;

    void OnCollisionEnter(Collision collision)
    {
        FindObjectOfType<GameManager>().UpdateTargets();
        Instantiate(particelPrefab, transform.position, transform.rotation);
        Destroy(collision.gameObject);
        Destroy(this.gameObject);

    }

}
