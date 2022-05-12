using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;
    public bool vertical;
    public float changeTime = 3.0f;
    

    Rigidbody2D rigidbody2d;
    float timer;
    float direction = 1;
    int x = 0;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer < 0)
        {
            timer = changeTime;
            vertical = !vertical;
            x += 1;
            if (x == 2)
            {
                direction = -direction;
                x = 0;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
        }
        
        rigidbody2d.MovePosition(position);

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if(player != null)
        {
            player.ChangeHealth(-1);
        }
    }
}
