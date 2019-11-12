using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private Rigidbody2D _rigid;

    [SerializeField]
    private float _jumpForce = 3.0f;

    [SerializeField]
    private float _speed = 3.0f;

    private bool _grounded = true;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontaInput = Input.GetAxis("Horizontal");
        _rigid.velocity = new Vector2(horizontaInput * _speed, _rigid.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && _grounded == true)
        {
            _grounded = false;
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
        }

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, 1 << 8);

        if (hitInfo.collider != null)
        {
            _grounded = true;
        }
        
    }
}
