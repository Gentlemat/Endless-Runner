using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10;
    public float gravityModyfier = 1f;

    private bool isOnGround = true;
    public bool gameOver = false;
    private Rigidbody playerRb;
    private Animator animator;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    private AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        Physics.gravity *= gravityModyfier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            animator.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            audioSource.PlayOneShot(jumpSound);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();

        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            animator.SetBool("Death_b", true);
            animator.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            audioSource.PlayOneShot(crashSound);
        }
    }
}
