using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRB;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;
    Animator playerAnim;
    public ParticleSystem groundParticles;
    public ParticleSystem deathParticles;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            groundParticles.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            groundParticles.Play();
        } 
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            collision.gameObject.GetComponent<Collider>().enabled = false;
            Debug.Log("Game over");
            playerAnim.SetBool("Death_b", true);
            groundParticles.Stop();
            deathParticles.Play();

        }
    }
}
