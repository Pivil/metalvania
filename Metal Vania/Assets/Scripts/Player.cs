using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private Rigidbody2D _rigid;
    private PlayerAnimation _anim;

    [SerializeField]
    private float _jumpForce = 3.0f;
    private bool resetJump = false;

    [SerializeField]
    private float _speed = 3.0f;
    
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

       
    }

    void Attack()
    {
        _anim.Attack();
    }

    void Movement()
    {
        float move = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            resetJump = true;
            StartCoroutine(ResetJumpRoutine());
        } else if (Input.GetKeyDown(KeyCode.E))
        {
            Attack();
        }

        _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);

        _anim.Move(move);

    }

    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, 1 << 8);
        return (hitInfo.collider != null && resetJump == false);
       
    }


    IEnumerator ResetJumpRoutine()
    {
        resetJump = true;
        yield return new WaitForSeconds(0.1f);
        resetJump = false;
    }
}
