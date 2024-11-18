using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PCController : MonoBehaviour
{
    public ParticleSystem dust;
    public Animator PCAnimation;
    public enum PCStates
    {
        DEFAULT,
        JUMP,
        ABILITY,
        DEAD

    }

    public class PlayerController : MonoBehaviour
    {
        public int scoreValue = 10; // Adjust this value based on how much you want to increase the score.

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Coin")) // Adjust the tag based on your game logic.
            {
                // Handle coin collection logic here.
                Scoreboard scoreManager = FindObjectOfType<Scoreboard>();
                if (scoreManager != null)
                {
                    scoreManager.IncreaseScore(scoreValue);
                }

                // Additional coin collection logic goes here.
                Destroy(other.gameObject); // Destroy the coin, for example.
            }
        }


    }


    [SerializeField]
    private float currentMoveSpeed = 2f;

    [SerializeField]
    private float acceleration = 2f;

    [SerializeField]
    private float upwardAcceleration = 2f;

    [SerializeField]
    private float downwardAcceleration = 5f;

    [SerializeField]
    private float minSpeed = 5f;

    [SerializeField]
    private float maxSpeed = 10f;

    [SerializeField]
    private float jumpforce = 200f;


    Rigidbody2D playerRB;

    InputManager InputManager;

    GroundCheckerButBetter groundChecker;

    public PCStates currentState;

    private bool isDead = false;


    // Start is called before the first frame update
    void Start()
    {
        InputManager = FindObjectOfType<InputManager>();
        groundChecker = FindObjectOfType<GroundCheckerButBetter>();



        playerRB = GetComponent<Rigidbody2D>();
        currentMoveSpeed = minSpeed;
        currentState = PCStates.DEFAULT;
    }

    // Update is called once per frame
    void Update()

    {
        PCAnimation.SetBool("IsGrounded", groundChecker.IsGrounded());
        float slopeAngle = GetSlopeAngle();

        if (GameManager.instance.GameState != GameManager.GameStates.Wait)
        {
            NormalMovement(slopeAngle);

            switch (currentState)
            {

                case PCStates.DEFAULT:
                    CreateDust(); 



                    if (InputManager.IsTouch() && groundChecker.IsGrounded())
                    {
                        Jump();
                        PCAnimation.SetBool("IsJumping", true);
                        ChangeState(PCStates.JUMP);

                    }
                    break;

                case PCStates.JUMP:

                    if (InputManager.IsTouch() && !groundChecker.IsGrounded())
                    {
                        PCAnimation.SetBool("IsJumping", false);
                        PCAnimation.SetBool("IsGliding", true);
                        ChangeState(PCStates.ABILITY);

                    }
                    else if (playerRB.velocity.y < 0.1f && groundChecker.IsGrounded())
                    {
                        PCAnimation.SetBool("IsJumping", false);
                        ChangeState(PCStates.DEFAULT);
                    }

                    break;

                case PCStates.ABILITY:

                    playerRB.gravityScale = 0.3f;

                    if (groundChecker.IsGrounded())
                    {
                        playerRB.gravityScale = 1f;
                        PCAnimation.SetBool("IsGliding", false);
                        ChangeState(PCStates.DEFAULT);


                    }
                    break;

                case PCStates.DEAD:

                    break;
            }

        }
       
    }


    void Jump()
    {
        playerRB.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);

        


    }

    void NormalMovement(float slopeAngle)
    {

        float slopeAngleDeg = Mathf.Rad2Deg * slopeAngle;

        Vector2 movementVector = new Vector2(currentMoveSpeed, playerRB.velocity.y);

        if (Mathf.Abs(slopeAngle) > 0.1f)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -Mathf.Rad2Deg * slopeAngle);

            if (slopeAngleDeg < 0)
            {
                currentMoveSpeed -= upwardAcceleration * Time.deltaTime;
            }
            else if (slopeAngle > 0)
            {
                currentMoveSpeed += downwardAcceleration * Time.deltaTime;
            }

        }

        else
        {
            transform.rotation = Quaternion.identity;
            currentMoveSpeed += acceleration * Time.deltaTime;
        }


        currentMoveSpeed += acceleration * Time.deltaTime;
        currentMoveSpeed = Mathf.Clamp(currentMoveSpeed, minSpeed, maxSpeed);


        playerRB.velocity = movementVector;
    }


    void ChangeState(PCStates state)
    {
        currentState = state;
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        // Check if the other collider is the square
        if (other.gameObject.CompareTag("Square"))
        {
            if (!isDead)
            {
                Debug.Log("I'm Dead");

                // Set isDead to true to avoid multiple collisions
                isDead = true;

            }


        }
    }

    public float GetSlopeAngle()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 2f, LayerMask.GetMask("Ground"));

        if (hit.collider != null)
        {
            float groundAngle = Mathf.Atan2(hit.normal.x, hit.normal.y);
            return groundAngle;
        }
        return 0.0f;
    }
      void CreateDust()
    {
        dust.Play();
    }
}
