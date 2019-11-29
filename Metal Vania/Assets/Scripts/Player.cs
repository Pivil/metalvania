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
    private float direction;
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
        Collider2D dashArea = Physics2D.OverlapCircle(_rigid.position, 1f, 1<<9);

        if (dashArea.isTrigger) {
            //Vector2 player = transform.position;
            //Vector2 iron = dashArea.transform.position;
            //direction = AngleBetweenVector2(iron, player);
            //Debug.Log("Direction: " + direction);
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
            Vector2 player = transform.position;
            Vector2 iron = dashArea.transform.position;
            direction = AngleBetweenVector2(player, iron);
            float _x, _y;
            if (direction >= -90.0f && direction <= 90.0f)
            {
                _x = -1.0f;
            }
            else
            {
                _x = 1.0f;
            }

            if (direction >= 0.0f && direction <= 180.0f)
            {
                _y = -1.0f;
            }
            else
            {
                _y = 1.0f;
            }

            _rigid.velocity = new Vector2(_x * dashSpeed, _y * dashSpeed);
            StartCoroutine(ResetDash());
            print("Dashing : " + _rigid.velocity);
        }
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

    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }
}
