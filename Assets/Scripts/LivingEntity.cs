using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{

    protected float currHealth;
    protected float maxHealth;
    protected bool bDead;

    public void Start()
    {
        //currHealth = maxHealth;
        bDead = false;

    }
    public void Die()
    {
        Animator a = GetComponent<Animator>();
        a.enabled = false;
        bDead = true;
    }
    public bool isDead()
    {
        return bDead;
    }
    //public void TakeHit(float damage, RaycastHit hit)
    public void TakeHit(float damage)
    {
        currHealth -= damage;
        if(currHealth <= 0 && !bDead)
        {
            currHealth = 0;
            Die();
        }
        if(currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }
        
    }

    public float GetHealth()
    {
        return currHealth;

    }
    public float GetMaxHealth()
    {
        return maxHealth;

    }
    public void SetHealth(float h)
    {
        currHealth = h;

    }
}
