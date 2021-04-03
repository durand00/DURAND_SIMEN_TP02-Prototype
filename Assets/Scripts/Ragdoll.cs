using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    public bool die;
    Rigidbody[] ragdollRbs;
    Animator animator;
    public bool dead;
   

    void Awake()
    {
        ragdollRbs = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();

        ToggleRbs(true);
    }

    
    void Update()
    {
        if(die == true)
        {
            die = false;
            Die();
        }
    }

    // Active/Desactive les Rigidbody du Ragdoll
    void ToggleRbs(bool value)
    {
        foreach(Rigidbody r in ragdollRbs)
        {
            r.isKinematic = value;
        }
        //animator.enabled = value;
    }

    void Die()
    {
        // Desactiver l'animator
        animator.enabled = false;
        dead = true;
        GetComponent < AlienDeplacement> ().Die();
        // Activer le Ragdoll
        ToggleRbs(false);
        
        
    }

    
}
