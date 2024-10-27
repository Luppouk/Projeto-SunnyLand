using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemieMovement : MonoBehaviour
{
    public Rigidbody2D enemieRb;
    public float xDirection = 1.0f;
    public float xIntensity = 3.0f;
    public Vector3 facingRight;
    public Vector3 facingLeft;
    // Start is called before the first frame update
    void Start()
    {
        this.enemieRb = gameObject.GetComponent<Rigidbody2D>();
        this.enemieRb.freezeRotation = true;
        facingRight = transform.localScale;
        facingLeft = transform.localScale;
        facingLeft.x = facingLeft.x * -1;
    }

    // Update is called once per frame
    void Update()
    {
        xMove();
    }

    void xMove()
    {
        if (this.enemieRb.velocity.x == 0)
        {
            float x = this.xIntensity * this.xDirection;
            print(x);
            Vector2 movement = new Vector2(x, 0);
            this.enemieRb.velocity = movement;
            this.xDirection = this.xDirection * -1;
        }
        if (xDirection < 0)
        {
            transform.localScale = facingRight;
        }
        if (xDirection > 0)
        {
            transform.localScale = facingLeft;
        }
    }
}
