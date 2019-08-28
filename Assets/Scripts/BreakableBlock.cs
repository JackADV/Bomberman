using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    public int maxHealth = 100;

    private int currentHealth = 100;
    //public new GameObject gameObject;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        print("Damaged!");
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
        //}
        //public void OnTriggerEnter(Collider other)
        //{
        //    if (!broken && other.CompareTag("Explosion"))
        //    {
        //        broken = true;
        //        //CancelInvoke("Explode");
        //        Destroy(gameObject);
        //    }
        //}
    }
