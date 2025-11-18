using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemColetavel : MonoBehaviour
{
    public GameObject pref;
    public float sTime;
    private float timecont;
    private float varPos = 2f;

    FimGame fimGame;
    MovimentoPersonagem movimentoPersonagem;

    void Start()
    {
        fimGame = FindFirstObjectByType<FimGame>();
        movimentoPersonagem = FindFirstObjectByType<MovimentoPersonagem>();
    }

    
    void Update()
    {
        timecont += Time.deltaTime;

        if (timecont >= sTime)
        {
            varPos = Random.Range(-1, 2);
            Vector2 position = new Vector2(transform.position.x + varPos, transform.position.y);
            pref.GetComponentInChildren<TMP_Text>().text = (fimGame.sScore/3).ToString();
            GameObject go = Instantiate(pref, position, transform.rotation);
            Destroy(go, 5f);
            timecont = 0;

        }

    }
}
