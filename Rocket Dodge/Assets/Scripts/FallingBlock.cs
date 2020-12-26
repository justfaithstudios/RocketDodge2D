using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    public Vector2 speedMinMax;
    float speed;
    float visualHeightthreshold;
    public AudioSource soundEffect;
    private void Start()
    {
        visualHeightthreshold = -Camera.main.orthographicSize - transform.localScale.y;
        speed = Mathf.Lerp(speedMinMax.x, speedMinMax.y, Difficulty.GetDifficultyPercent());
    }
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < visualHeightthreshold)
        {
            Destroy(gameObject);
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Instantiate(soundEffect, transform.position, Quaternion.identity);

        }
    }
}
