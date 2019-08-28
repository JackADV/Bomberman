using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bomb : MonoBehaviour
{
    public int damage = 100;
    public GameObject explosionPrefab;
    public LayerMask levelMask;
   //public LayerMask Breakable;
    private bool exploded = false;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explode", 3f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        StartCoroutine(CreateExplosions(Vector3.forward));
        StartCoroutine(CreateExplosions(Vector3.right));
        StartCoroutine(CreateExplosions(Vector3.back));
        StartCoroutine(CreateExplosions(Vector3.left));
        GetComponent<MeshRenderer>().enabled = false;
        exploded = true;
        transform.Find("Collider").gameObject.SetActive(false);
        Destroy(gameObject, .3f);

    }
    private IEnumerator CreateExplosions(Vector3 direction)
    {
        // Loop through all range of Explosion
        for (int i = 1; i < 3; i++)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position + new Vector3(0, .5f, 0), direction, out hit, i, levelMask);

            // If there is no collider there
            if (!hit.collider)
            {
                // Spawn the explosion effect
                Instantiate(explosionPrefab, transform.position + (i * direction), explosionPrefab.transform.rotation);
            }
            else
            {
                // It hit something!
                BreakableBlock breakable = hit.collider.GetComponent<BreakableBlock>();
                if (breakable)
                {
                    breakable.TakeDamage(damage);
                }
            }
            yield return new WaitForSeconds(.05f);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (!exploded && other.CompareTag("Explosion"))
        {
            //CancelInvoke("Explode");
            Explode();
        }
    }
}
