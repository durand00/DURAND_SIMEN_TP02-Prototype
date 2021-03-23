using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RbCharacterMovements : MonoBehaviour
{
    public float speed = 0.1f;
    public float jumpHeight = 1f;

    // Transform de la position des pieds
    public Transform feetPosition;

    private float inputVertical;
    private float inputHorizontal;    

    private Vector3 moveDirection;

    private Rigidbody rb;

    private bool isGrounded = true;

    private Animator animatorPeasantMan;

    // Start is called before the first frame update
    void Start()
    {
        // Assigner le Rigidbody
        rb = GetComponent<Rigidbody>();

        // Assigner l'animator
        animatorPeasantMan = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Vérifier si l'on touche le sol
        isGrounded = Physics.CheckSphere(feetPosition.position, 0.15f, 1, QueryTriggerInteraction.Ignore);

        // Vérifier les inputs du joueur
        // Vertical (W, S et Joystick avant/arrière)
        inputVertical = Input.GetAxis("Vertical");
        // Horizontal (A, D et Joystick gauche/droite)
        inputHorizontal = Input.GetAxis("Horizontal");

        // Vecteur de mouvements (Avant/arrière + Gauche/Droite)
        moveDirection = transform.forward * inputVertical + transform.right * inputHorizontal;  
        
        // Sauter
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }

        // Animations de mouvement
        animatorPeasantMan.SetFloat("horizontal", inputHorizontal);
        animatorPeasantMan.SetFloat("vertical", inputVertical);

    }

    private void FixedUpdate()
    {
        // Déplacer le personnage selon le vecteur de direction
        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
    }
}
