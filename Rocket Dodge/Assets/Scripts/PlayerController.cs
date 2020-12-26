using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public float speed = 7.0f;
    public GameObject character;
    public Rigidbody2D rb;
    public Text directions;
    private bool isDead = false;
    private float startTime;
    private float animationDuration = 2.0f;


    // test vars from web
    private Vector3 mOffset;
    private float mZCoord;

    public GameObject effect;


    void Start()
    {
        float halfPlayerWidth = transform.localScale.x / 2.0f;


        rb = GetComponent<Rigidbody2D>();
        startTime = Time.time;

    }


    void Update()
    {
        if (isDead)
            return;
        if (Time.time - startTime < animationDuration)
        {

            directions.text = "TOUCH SHIP THEN MOVE AROUND";

            return;
        }
        if (Time.time + startTime > animationDuration)
        {
            Destroy(directions);
        }
        // player movement
        // insert code if needed


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Death();

        }
    }
    private void Death()
    {
        isDead = true;
        GetComponent<Score>().onDeath();


    }


    void OnMouseDown()

    {

        mZCoord = Camera.main.WorldToScreenPoint(

            gameObject.transform.position).z;



        // Store offset = gameobject world pos - mouse world pos

        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();

    }



    private Vector3 GetMouseAsWorldPoint()

    {

        // Pixel coordinates of mouse (x,y)

        Vector3 mousePoint = Input.mousePosition;



        // z coordinate of game object on screen

        mousePoint.z = mZCoord;



        // Convert it to world points

        return Camera.main.ScreenToWorldPoint(mousePoint);

    }



    void OnMouseDrag()

    {

        transform.position = GetMouseAsWorldPoint() + mOffset;

    }

}
