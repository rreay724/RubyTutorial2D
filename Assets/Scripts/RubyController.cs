using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{

    public int maxHealth = 5;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    public float characterSpeed = 3.0f;
    public int health { get { return currentHealth; } }
    int currentHealth;


    Rigidbody2D rigidBody2d;
    float horizontal;
    float vertical;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
    }

    // In addition to this, the physics system is updated at a
    // different rate than the game. Update is called every time the
    // game computes a new image, the problem is that this is called
    // at an uncertain rate.It could be 20 images per second on a
    // slow computer, or 3000 on a very fast one.

    //  For the physics computation to be stable, it needs to update
    //  at regular intervals(for example, every 16ms).
    //  Unity has another function called FixedUpdate that
    //  needs to be used any time you want to directly influence
    //  physics components or objects, such as a Rigidbody.

    private void FixedUpdate()
    {
        Vector2 position = rigidBody2d.position;
        position.x = position.x + characterSpeed * horizontal * Time.deltaTime; ;
        position.y = position.y + characterSpeed * vertical * Time.deltaTime;

        rigidBody2d.MovePosition(position);
    }

    //Clamping ensures that the first parameter(here currentHealth + amount) never goes lower
    //than the second parameter(here 0) and never goes above the third parameter(maxHealth).
    //So Ruby’s health will always stay between 0 and maxHealth.



 public void ChangeHealth(int amount)
    {
        if(amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
