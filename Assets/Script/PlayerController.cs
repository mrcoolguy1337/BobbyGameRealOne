using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float MovementSpeed;
    public Rigidbody2D rb;

    public BoxCollider2D boxCollider;

    public LayerMask GroundLayer;

    public void Start()
    {
        MovementSpeed = 5f;
        rb = GetComponent<Rigidbody2D>();
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(new Vector2(0, 70), ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(MovementSpeed, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-MovementSpeed, 0, 0) * Time.deltaTime;
        }

    }
    public bool IsGrounded()
    {
        float extraHeight = .1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, extraHeight, GroundLayer);
        return raycastHit.collider != null;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Ladder")
        {
            while (Input.GetKey(KeyCode.W))
            {
                transform.position += new Vector3(0, 8, 0) * Time.deltaTime;
            }
        }
    }
}
