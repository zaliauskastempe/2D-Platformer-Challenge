using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    public AudioSource music;
    private SpriteRenderer spr;
    private Rigidbody2D rb2d;
    public float speed;
    public float jumpForce;
    public float maxSpeed = 50f;//Replace with your max speed
    private bool direction = true;
    private int count;
    private int lives;
    public Text ScoreText;          //Store a reference to the UI Text component which will display the number of pickups collected.
    public Text WinText;
    public Text LivesText;
    private int room;//Store a reference to the UI Text component which will display the 'You win' message.
    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        count = 0;
        lives = 3;
        room = 1;
        SetCountText();
    }

    // Update is called once per frame
    void Update()
    {
        if (lives < 1)
        {
            Destroy(gameObject);
            WinText.text = "Game Over!";
        }


        if (Input.GetKey(KeyCode.D))
        {
            anim.SetInteger("State", 1);
            direction = true;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            anim.SetInteger("State", 1);
            direction = false;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }

        if (direction == true)
        {
            spr.flipX = false;
        }
        if (direction == false)
        {
            spr.flipX = true;
        }
    }

    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);
        rb2d.AddForce(movement * speed);
       
        
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground") {
            anim.SetInteger("Jump", 0);
            if (Input.GetKeyDown(KeyCode.W)) {
                anim.SetInteger("Jump", 1);
                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

            }

        }
        else
        {
                anim.SetInteger("Jump", 2);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("Pickup"))
        {

            //... then set the other object we just collided with to inactive.
            other.gameObject.SetActive(false);

            //Add one to the current value of our count variable.
            count = count + 1;

            //Update the currently displayed count by calling the SetCountText function.
            SetCountText();
        }

        if (other.gameObject.CompareTag("Spike"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        LivesText.text = "Lives: " + lives.ToString();
        ScoreText.text = "Score: " + count.ToString();
        if (count >= 4)
        {
            if (room == 2)
            {

                music.Play();
                ScoreText.fontStyle = FontStyle.Bold;
                WinText.text = "You Win!";
            }

            if (room == 1)
            {
                transform.position = new Vector2(-4.7f, -35);   //GO TO NEXT LEVEL
                count = 0;
                lives = 3;
                room = 2;
                LivesText.text = "Lives: " + lives.ToString();
                ScoreText.text = "Count: " + count.ToString();
            }

        }
    }

}
