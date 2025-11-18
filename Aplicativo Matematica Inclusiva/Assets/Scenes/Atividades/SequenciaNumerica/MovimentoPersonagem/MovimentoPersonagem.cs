using System;
using Unity.VisualScripting;
using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private int direcao = 0;
    private bool isGrounded = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public int pontuacao = 0;
    private void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Numero")) {
            pontuacao++;
            Debug.Log("Pontuação: " + pontuacao);
            Destroy(col.gameObject);
        }
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

      transform.Translate(Vector2.right * direcao * speed * Time.deltaTime);
        //Debug.Log("Direcao: " + direcao);
    }

    public void BotaoEsquerda (bool pressionado)
    {
        direcao = pressionado ? -1 : 0;
    }

    public void BotaoDireta(bool pressionado)
    {
        direcao = pressionado ? 1 : 0;
    }

     public void BotaoPular()
    {
        Debug.Log("isGrounded: " + isGrounded);
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("Pulou!");
        } else
        {
            Debug.Log("Nao pulou!");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

}