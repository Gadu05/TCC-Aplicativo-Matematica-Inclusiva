
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.Android.LowLevel;

public class PegarItens : MonoBehaviour
{

    public Rigidbody2D rig;
    public float speed;

    private void Start()
    {
        transform.Rotate(new Vector3(0, 0, Random.Range(-145, 45)));

    }

    private void FixedUpdate()
    {
        rig.MovePosition(transform.position + transform.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FimGame.fim.ShowGameOver();
        }
    }

}


