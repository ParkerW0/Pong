using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    float speed;

    float radius;
    Vector2 direction;
    Vector2 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = gameObject.transform.position;
        StartCoroutine(wait());
        direction = Vector2.one.normalized; //direction (1, 1)
        radius = transform.localScale.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime );

        if (transform.position.y < GameManager.bottomLeft.y + radius)
        {
            direction.y = -direction.y;
        }
        if (transform.position.y > GameManager.topRight.y - radius)
        {
            direction.y = -direction.y;
        }

        if (transform.position.x < GameManager.bottomLeft.x + radius)
        {
            Debug.Log("Right Player Point!");
            ScoreScriptR.scoreR += 1;
            
            gameObject.transform.position = originalPos;
            StartCoroutine(wait());
        }
        if (transform.position.x > GameManager.topRight.x + radius)
        {
            Debug.Log("Left Player Point!");
            ScoreScriptL.scoreL += 1;

            gameObject.transform.position = originalPos;
            StartCoroutine(wait());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Paddle")
        {
            bool isRight = other.GetComponent<Paddle>().isRight;
            //if hitting right paddle
            if(isRight == true && direction.x > 0)
            {
                direction.x = -direction.x;
            }
            //if hitting left paddle
            if (isRight == false && direction.x < 0)
            {
                direction.x = -direction.x;
            }
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(3);
    }
}
