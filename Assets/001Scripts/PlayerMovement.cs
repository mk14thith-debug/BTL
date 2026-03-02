using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class PlayerMovement : MonoBehaviour
{
    private int currentLevel;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    public AudioClip[] audioClips;
    public Camera MainCamera;
    public Animator Player;
    public PhysicsMaterial2D bouncyMaterial, normalMaterial , slipperyMaterial;
 
    public LayerMask groundMask;

    private bool isSquatting, isGrounded, isProned, isFalling;
    private float jumpForce, moveInput;
  
    public float walkSpeed, minJumpForce, maxJumpForce, increasingForceSpeed;

    private float fallVelocity;
    public float proneThreshold = -15f;
    public float proneDuration = 0.5f;
    private float proneTimer = 0f;
    private bool isStunned = false;
    public Vector2 getVelocity()
    {
        return rb.velocity;
    }
    public bool getIsSquatting()
    {
        return isSquatting;
    }
    public bool getIsGrounded()
    {
        return isGrounded;
    }
    public bool getIsProned()
    {
        return isProned;
    }
    public bool getIsFalling()
    {
        return isFalling;
    }
    void Start()
    {
        Application.targetFrameRate = 60;
        
        audioSource = GetComponent<AudioSource>();
        Debug.Log("hello");
        rb = GetComponent<Rigidbody2D>();
        Player = GetComponent<Animator>();

        if (PlayerPrefs.GetInt("HasSavedGame", 0) == 1)
        {
            float x = PlayerPrefs.GetFloat("PlayerPosX", transform.position.x);
            float y = PlayerPrefs.GetFloat("PlayerPosY", transform.position.y);

            transform.position = new Vector3(x, y, transform.position.z);
        }
        else
        {
            Player.SetBool("isIdle", true);
            Player.SetBool("isRunning", false);
            Player.SetBool("isSquatting", false);
            Player.SetBool("isJumping", false);
            Player.SetBool("isFalling", false);
            Player.SetBool("isProned", false);        
            isSquatting = false;
            isGrounded = false;
            isProned = false;
            isFalling = false;         
        }
    

    }
    public void PlaySound()
    {
        
    }
    private void PlaySound(int clipNumber)
    {
        if (clipNumber < audioClips.Length && audioClips[clipNumber] != null)
        {
            audioSource.PlayOneShot(audioClips[clipNumber]);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isGrounded)
        {
            PlaySound(0);
            //Debug.Log("Sound0");
        }
        
    }


    void Update()
    {
         moveInput = Input.GetAxisRaw("Horizontal");
        fallVelocity = rb.velocity.y;

        
        if (isStunned)
        {
            rb.sharedMaterial = slipperyMaterial;
            proneTimer -= Time.deltaTime;

            rb.velocity = Vector2.zero;

            if (proneTimer <= 0f)
            {
                isProned = false;
                isStunned = false;
                Player.SetBool("isProned", false);
            }

            return; 
        }
        Vector2 checkPos = (Vector2)transform.position + Vector2.down * 0.5f;

        RaycastHit2D hit = Physics2D.BoxCast(
             checkPos,
             new Vector2(0.6f, 0.1f),
             0,
             Vector2.down,
             0.1f,
             groundMask
         );

        isGrounded = hit.collider != null && rb.velocity.y <= 0.1f;
        if (isGrounded)
            {
            rb.sharedMaterial = normalMaterial;

    
            if (fallVelocity < proneThreshold && !isStunned)
            {
                isProned = true;
                isStunned = true;
                proneTimer = proneDuration;

                Player.SetBool("isProned", true);
                PlaySound(2);
            }

            if (!isProned)
            {
                if (rb.velocity.x == 0)
                {
                    Player.SetBool("isIdle", true);
                    Player.SetBool("isRunning", false);
                    Player.SetBool("isFalling", false);
                    isFalling = false;
                }
            }

        }
            //Player walk
            if ((isGrounded && !isSquatting) )
            {
                
                rb.velocity = new Vector2(walkSpeed * moveInput, rb.velocity.y);
                if (moveInput != 0)
                {
                    transform.localScale = new Vector2(moveInput, transform.localScale.y);
                    Player.SetBool("isRunning", true);
                    Player.SetBool("isIdle", false);
                    Player.SetBool("isProned", false);
                    isSquatting = false;
                }
            }
            //Player squat
            if (Input.GetKey(KeyCode.Space) && isGrounded)
            {
                 jumpForce += increasingForceSpeed * Time.deltaTime;
                 rb.velocity = new Vector2(0, rb.velocity.y);
                //Debug.Log("isSquatting");
                Debug.Log("jumpForce" + jumpForce);

                Player.SetBool("isSquatting", true);
                Player.SetBool("isRunning", false);
                Player.SetBool("isFalling", false);
                Player.SetBool("isIdle", false);
                Player.SetBool("isProned", false);
                isSquatting = true;

                if (jumpForce >= maxJumpForce)
                {
                    Jump();
                }
            }
            //Player Jump
            if (Input.GetKeyUp(KeyCode.Space) && isSquatting  && jumpForce < maxJumpForce)
            {
                 Jump();

            
            
            }
            //Player Jumping
            if (rb.velocity.y > 0.1f && !isGrounded )
            {
                
                    rb.sharedMaterial = bouncyMaterial;
                    //Debug.Log("isJumping");

                    Player.SetBool("isJumping", true);
                    Player.SetBool("isIdle", false);
                    Player.SetBool("isRunning", false);
                
               
            }
            //Player falling
            if (rb.velocity.y < -0.1 && !isGrounded)
            {
                rb.sharedMaterial = slipperyMaterial;
                Player.SetBool("isFalling", true);
                Player.SetBool("isIdle", false);
                Player.SetBool("isRunning", false);
                Player.SetBool("isJumping", false);
                isFalling = true;
                //Debug.Log("isFalling");
            }
        
    }

    void Jump()
    {
        if (jumpForce < minJumpForce)
            jumpForce = minJumpForce;

        if (jumpForce > maxJumpForce)
            jumpForce = maxJumpForce;

        rb.velocity = new Vector2(moveInput * walkSpeed, jumpForce);
        PlaySound(1);
        jumpForce = 0;
        isSquatting = false;

        Player.SetBool("isRunning", false);
        Player.SetBool("isSquatting", false);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector2(transform.position.x, transform.position.y - 0.5f), new Vector2(0.7f, 0.3f));
    }

}
