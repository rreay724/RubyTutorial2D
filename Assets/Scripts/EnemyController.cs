using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;

    Rigidbody2D rigidBody2D;
    float timer;
    int direction = 1;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if(timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 position = rigidBody2D.position;

        if (vertical)
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direction);
        }
        

        rigidBody2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
}
