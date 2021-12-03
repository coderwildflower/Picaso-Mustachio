using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    float MoveSpeed;

    Vector2 inputDir;
    SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector2(xInput, yInput).normalized;
        transform.position += inputDir * MoveSpeed * Time.deltaTime;

        if (inputDir.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if(inputDir.x > 0) _spriteRenderer.flipX = false;

    }

    void Rotate()
    {
        float targetAngle = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;

        if (inputDir != Vector2.zero)
        {
            transform.eulerAngles = Vector3.forward * targetAngle;
        }

    }
}
