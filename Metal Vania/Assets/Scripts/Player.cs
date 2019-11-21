using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private Rigidbody2D _rigid;
    private PlayerAnimation _anim;
    [SerializeField]
    private SpriteRenderer _sprite;

    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    private float _jumpForce = 3.0f;
    private bool resetJump = false;


    // ### DASH ###
    public float dashSpeed;
    public float startDashTime;
    private float dashTime;
    private int direction;
    public bool isDashing;


    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<PlayerAnimation>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        dashTime = startDashTime;

    }

    void Update()
    {
        Movement();

    }

    void Dash(float move)
    {
        if (dashTime <= 0)
        {

            direction = 0;
            dashTime = startDashTime;
            _rigid.velocity = Vector2.zero;
        }
        else
        {
            isDashing = true;
            dashTime -= Time.deltaTime;
            if (move > 0) direction = 1;
            else if (move < 0) direction = -1;
            _rigid.velocity = new Vector2(direction * dashSpeed, _rigid.velocity.y);
            StartCoroutine(ResetDash());
            print("Dashing : " + _rigid.velocity);
        }

    }

    void Attack()
    {
        _anim.Attack();
    }

    void Movement()
    {
        float move = Input.GetAxis("Horizontal");

        if (move > 0)
        {
            _sprite.flipX = false;
        }

        else if (move < 0)
        {
            _sprite.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            resetJump = true;
            StartCoroutine(ResetJumpRoutine());
        } 
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Attack();
        } 
        else if (Input.GetKeyDown(KeyCode.C))
        {
            Dash(move);
        }
       

        if (!isDashing)
            _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);

        _anim.Move(move);

    }

    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, 1 << 8);
        return (hitInfo.collider != null && resetJump == false);
       
    }

    IEnumerator ResetDash()
    {
        yield return new WaitForSeconds(startDashTime);
        isDashing = false;
    }

    IEnumerator ResetJumpRoutine()
    {
        resetJump = true;
        yield return new WaitForSeconds(0.1f);
        resetJump = false;
    }
}
