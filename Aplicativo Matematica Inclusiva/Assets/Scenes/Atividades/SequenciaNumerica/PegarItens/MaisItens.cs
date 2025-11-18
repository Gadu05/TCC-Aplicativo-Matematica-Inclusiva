using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MaisItens : MonoBehaviour
{


    public Rigidbody2D rig;
    public float speed;

    FimGame fimGame;

    private void Start()
    {
        fimGame = FindFirstObjectByType<FimGame>();
    }

    private void FixedUpdate()
    {
        rig.MovePosition(transform.position + Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            fimGame.AddScore();
        }
    }

}

