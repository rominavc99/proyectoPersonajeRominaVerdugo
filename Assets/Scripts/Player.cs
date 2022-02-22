using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    float moveSpeed = 5f;
    [SerializeField]
    float jumpForce = 7f;

    [SerializeField]
    float raydistance=5f;
    [SerializeField]
    Color raycolor= Color.red;
    [SerializeField]
    LayerMask groundlayer;

    [SerializeField]
    Vector3 rayOrigin;

    Rigidbody2D rb2d;

    SpriteRenderer spr;

    Animator anim;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        transform.Translate(Vector2.right * Axis.x * moveSpeed * Time.deltaTime);
        spr.flipX = FlipSpriteX;
        if(JumpButton && IsGrounding){
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        anim.SetFloat("AxisX", Mathf.Abs(Axis.x));
    }

    Vector2 Axis => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    bool JumpButton => Input.GetButtonDown("Jump");
    bool IsGrounding => Physics2D.Raycast(transform.position + rayOrigin, Vector2.down, raydistance, groundlayer);
    bool FlipSpriteX => Axis.x > 0f ? false : Axis.x < 0f ? true : spr.flipX;
    void OnDrawGizmosSelected() {
        Gizmos.color = raycolor;
        Gizmos.DrawRay(transform.position + rayOrigin, Vector2.down * raydistance);
    }
    
}
