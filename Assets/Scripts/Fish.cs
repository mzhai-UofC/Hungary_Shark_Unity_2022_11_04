using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float acceleration = 1f;

    Vector3 velocity;
    Vector3 desiredVelocity;

    Vector3 leftBottomPoint, rightTopPoint;

    bool isInScreen = false;
    int level;
  

    void Awake()
    {
        velocity = new Vector3(0, 0, 0);
        desiredVelocity = new Vector3(0, 0, 0);

        leftBottomPoint = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f,
            Mathf.Abs(Camera.main.transform.position.z)));
        rightTopPoint = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f,
                Mathf.Abs(Camera.main.transform.position.z)));
    }

    void Clamp()
    {
        Vector3 position = transform.localPosition;
        if (position.x > rightTopPoint.x)
        {
            position.x = rightTopPoint.x;
            velocity.x = 0;
        }
        else if (position.x < leftBottomPoint.x)
        {
            position.x = leftBottomPoint.x;
            velocity.x = 0;
        }
        if (position.y > rightTopPoint.y)
        {
            position.y = rightTopPoint.y;
            velocity.y = 0;
        }
        else if (position.y < leftBottomPoint.y)
        {
            position.y = leftBottomPoint.y;
            velocity.y = 0;
        }
        transform.localPosition = position;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }


    public float speed = 5f;

    void Update()
    {
       // Vector3 playerInput = new Vector3(0, 0, 0);
       // playerInput.x = Input.GetAxis("Horizontal");
       // playerInput.y = Input.GetAxis("Vertical");
       // desiredVelocity = playerInput * speed;
        velocity = Vector3.MoveTowards(velocity, desiredVelocity, acceleration);

        if (velocity.x > 0) transform.localEulerAngles = new Vector3(0, 0, 0);
        else if (velocity.x < 0) transform.localEulerAngles = new Vector3(0, 180, 0);

        transform.localPosition += velocity * Time.deltaTime;


        if (this.tag == "Player") Clamp();
        else Die();
    }

    public void SetDesiredVelocity(Vector3 Input)
    {
        Vector3.ClampMagnitude(Input, 1f);
        desiredVelocity = Input * speed;
    }

    void Die()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        if ((x >= leftBottomPoint.x && x <= rightTopPoint.x) &&
            (y >= leftBottomPoint.y && y <= rightTopPoint.y))
        {
            isInScreen = true;
        }

        else
        {
            if (isInScreen)
            {
                Destroy(this.gameObject, 2f);
            }
        }
    }

   

    public int GetLevel()
    {
        return this.level;
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Fish player = other.GetComponent<Fish>();
            if (player.GetLevel() <= level)
            {
                Destroy(other.gameObject);
            }
            else
                Control.score++;
                Destroy(this.gameObject);
        }
    }
}

