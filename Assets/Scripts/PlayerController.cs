using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;

    public float jumpForce = 10.0f;
    public float gravityModifier = 1f;

    public bool isOnGround = true;
    public bool gameOver = false;
    public bool doubleJumpAvailable = true;
    public bool doubleSpeed = false;

    protected float timer;
    private int delayAmount = 1;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }

        else if (Input.GetKeyDown(KeyCode.D) && !gameOver && isOnGround)
        {
            doubleSpeed = true;
            playerAnim.speed *= 2;
        }

        else if (Input.GetKeyDown(KeyCode.X) && !gameOver && isOnGround && doubleSpeed)
        {
            doubleSpeed = false;
            playerAnim.speed /= 2;
        }

        else if (!isOnGround)
        {
            if (Input.GetKeyDown(KeyCode.Space) && doubleJumpAvailable && !gameOver)
            {
                playerRb.AddForce(Vector3.up * 350, ForceMode.Impulse);
                playerAnim.SetTrigger("Jump_trig");
                playerAudio.PlayOneShot(jumpSound, 1.0f);
                doubleJumpAvailable = false;
            }
        }

        timer += Time.deltaTime;

        if (timer >= delayAmount && !gameOver)
        {
            timer = 0f;
            score++;
            if (doubleSpeed)
            {
                score++;
            }
            Debug.Log("Score = " + score);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            doubleJumpAvailable = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!");
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
