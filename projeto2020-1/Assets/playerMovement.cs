using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public Animator animator;

    public float xIntensity = 5.0f;
    public float jumpIntensity = 5.0f;
    public float xDirection = 0;
    public int extraJumps;
    public float timeLeftBonus = 0f;
    public LayerMask ground;
    public Transform feetPos;
    public bool isGrounded;

    public Text scoreText;
    public int score = 0;

    public bool bonus = false;

    public int health = 3;
    public Text healthText;
    public bool damaged;
    public float timeLeftDamage = 0f;

    public Vector3 facingRight;
    public Vector3 facingLeft;
    // Start is called before the first frame update
    void Start()
    {
        this.playerRb = gameObject.GetComponent<Rigidbody2D>();
        this.playerRb.freezeRotation = true;
        facingRight = transform.localScale;
        facingLeft = transform.localScale;
        facingLeft.x = facingLeft.x * -1;
    }

    // Update is called once per frame
    void Update()
    {
        xMove();
        jump();
        damage();
        speedBonus();
        scoreText.text = score.ToString();
        healthText.text = health.ToString();

        isGrounded = Physics2D.OverlapCircle(feetPos.position, 0.2f, ground);
        if (isGrounded == true)
        {
            extraJumps = 2;
        }
        animator.SetFloat("speed", Mathf.Abs(xDirection));
        animator.SetBool("grounded", isGrounded);
        animator.SetFloat("speedY", playerRb.velocity.y);
    }

    void xMove()
    {
        this.xDirection = Input.GetAxis("Horizontal");
        float x = this.xDirection * xIntensity;
        float y = this.playerRb.velocity.y;
        Vector2 movement = new Vector2(x, y);
        this.playerRb.velocity = movement;

        if (xDirection > 0)
        {
            transform.localScale = facingRight;
        }
        if (xDirection < 0)
        {
            transform.localScale = facingLeft;
        }
    }

    void jump()
    {
        if ((Input.GetMouseButtonDown(0)) && (isGrounded == true))
        {
            float x = this.playerRb.velocity.x;
            float y = jumpIntensity;
            Vector2 movement = new Vector2(x, y);
            this.playerRb.velocity = movement;
            extraJumps--;
        }
        if ((Input.GetMouseButtonDown(0)) && (isGrounded == false) && extraJumps > 1)
        {
            float x = this.playerRb.velocity.x;
            float y = jumpIntensity;
            Vector2 movement = new Vector2(x, y);
            this.playerRb.velocity = movement;
            extraJumps--;
        }
    }

    void OnCollisionEnter2D(Collision2D objeto)
    {
        if(objeto.gameObject.tag == "cherry")
        {
            Destroy(objeto.gameObject);
            this.xIntensity = 7.0f;
            this.timeLeftBonus += 5.0f;
            bonus = true;
        }

        if (objeto.gameObject.tag == "gem")
        {
            Destroy(objeto.gameObject);
            this.score += 1;
        }

        if (objeto.gameObject.tag == "enemie")
        {
            if (damaged == false)
            {
                this.health -= 1;
                this.timeLeftDamage += 1.0f;
                damaged = true;
            }

        }

        if (objeto.gameObject.tag == "trigger")
        {
            GameObject[] vetorObjetos = GameObject.FindGameObjectsWithTag("crates");
            int qtdObjetos = vetorObjetos.Length;
            int i = 0;
            for (i = 0; i < qtdObjetos; i++)
            {
                Rigidbody2D rigid = vetorObjetos[i].GetComponent<Rigidbody2D>();
                rigid.gravityScale = 1;
            }
        }
    }

    void damage()
    {
        if (this.damaged == true)
        {
            this.timeLeftDamage -= Time.deltaTime;
            if (timeLeftDamage <= 0)
            {
                this.damaged = false;
            }
        }
        if (health <= 0)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    void speedBonus()
    {
        if(this.bonus == true)
        {
            this.timeLeftBonus -= Time.deltaTime;
            if(timeLeftBonus <= 0)
            {
                this.bonus = false;
                this.xIntensity = 5.0f;
            }
        }
    }
}
