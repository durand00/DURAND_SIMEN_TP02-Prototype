using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienDeplacement : MonoBehaviour
{
    public Transform eyes;
    public Transform playerHead;
    public Transform target;
    public float distance;
    public float walkingSpeed = 1f;
    public float runningSpeed = 2f;
    public float chaseRange = 10;
    public float attackRange = 2.2f;
    public float attackRepeaTime = 1;
    private float attackTime;

    //public void Die();

    public bool dead;


    private NavMeshAgent alien;

    private bool isAlienBusy = false;

    // Start is called before the first frame update
    void Start()
    {
        attackTime = Time.time;
        //alien = GetComponent<NavMeshAgent>();
        alien = gameObject.GetComponent<NavMeshAgent>();
        InvokeRepeating("DelayedUpdate", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        // Si le zombie n'est pas occupé et que l'alien n'est pas mort
        if (!isAlienBusy && dead == false)
        {
            

            Vector3 newDestination = new Vector3(Random.Range(12f, 8f), 1f, Random.Range(-12f, 12));

            StartCoroutine(GoToDestination(newDestination, walkingSpeed));
        }
       // alien.destination = target.position;
    }

    void DelayedUpdate()
    {
        if (dead == true)
            return;

        // Creer un rayon entre la police et le joueur
        RaycastHit hit;

        if (Physics.Linecast(eyes.position, playerHead.position, out hit))
        {
            // Si le rayon touche le joueur
            if (hit.transform.CompareTag("Player"))
            {
                // le zombie poursuit le joueur( a sa position actuelle)
                Debug.Log("Le joueur est en vue!");

                // Arreter la coroutine en cours
                StopAllCoroutines();
                StartCoroutine(GoToDestination(hit.point, runningSpeed));

            }
            
        }
        
            
    }

    // Coroutine de déplacements
    IEnumerator GoToDestination(Vector3 newDestination, float speed)
    {
       
        //Je suis maintenant occupé
        isAlienBusy = true;

        //Se deplacer vers la destination
        alien.SetDestination(newDestination);
        alien.speed = speed;
        //Tant que je ne suis pas rendu a destination, la coroutine attend
        while (alien.pathPending || alien.remainingDistance > alien.stoppingDistance)
        {
            // Attendre
            yield return null;
        }

        // le zombie est rendu a destination, il prend une pause
        yield return new WaitForSeconds(Random.Range(4f, 6f));

        //Je suis rendu a la destination et j'ai pris ma pause
        isAlienBusy = false;
    }

    public void Die()
    {
        dead = true;
        alien.isStopped = true;
    }
}
