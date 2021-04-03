using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform barrelEnd;
    public ParticleSystem particule;
    //public LineRenderer bulletLine;

    void Start()
    {
        
    }

   
    void Update()
    {
        // Si je fais feu
        if(Input.GetMouseButtonDown(0))
        {
            particule.Play();
            // je fais un rayon à partir du BarrelEnd
            Ray bulletRay = new Ray(barrelEnd.position, barrelEnd.forward);
            RaycastHit hit;

            // PointA du LineRenderer
            //bulletLine.SetPosition(0, barrelEnd.position);
            // Si le rayon impacte sur un objet, on le propulse
            if(Physics.Raycast(bulletRay, out hit))
            {
                // PointB du LineRenderer (tir réussi)
                //bulletLine.SetPosition(1, hit.point);
               
                Explode(hit.point);
                

                // Ragdoll?
                Ragdoll ragdoll = hit.collider.GetComponentInParent<Ragdoll>();
                if (ragdoll != null)
                    ragdoll.die = true;

            }
           
        }

    }

    void Explode(Vector3 impactPosition)
    {
        // Recupérer tous les objets qui sont dans le perimètre de l'explosion
        Collider[] colliders = Physics.OverlapSphere(impactPosition, 5f);

        // Pour chaque objet récupéré, je lui donne une vélocité
        foreach (Collider coll in colliders)
        {
            Rigidbody rb = coll.GetComponent<Rigidbody>();
            // Si le Rigidbody a été trouvé
            if(rb!=null)
            {
                rb.AddExplosionForce(5f, impactPosition, 5f, 1f, ForceMode.Impulse);
                particule.Play();
            }
        }
    }

    
}
