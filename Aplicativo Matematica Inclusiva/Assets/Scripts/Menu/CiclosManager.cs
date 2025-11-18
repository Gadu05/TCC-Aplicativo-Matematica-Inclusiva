using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CiclosManager : MonoBehaviour
{

    [SerializeField] private Transform contentParent;
    [SerializeField] private GameObject itemPrefab;

    private CicloDAO dao = new CicloDAO();
    private List<Ciclo> lista;
    private ExecutadorCiclos executor;

    void Start()
    {
        executor = FindFirstObjectByType<ExecutadorCiclos>();
        atualizarLista();
    }

    void atualizarLista()
    {

        // Limpa os elementos anteriores do ScrollView
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        lista = dao.Listar();

        foreach (Ciclo ciclo in lista)
        {
            // Cria um item novo a partir do prefab
            GameObject item = Instantiate(itemPrefab, contentParent);

            // Botões do item
            var btnCiclo = item.transform.Find("btnCiclo").GetComponent<Button>();
            //var btnEditar = item.transform.Find("btnEditar").GetComponent<Button>();
            var btnRemover = item.transform.Find("btnRemover").GetComponent<Button>();

            // Texto do botão principal (nome do ciclo)
            btnCiclo.GetComponentInChildren<TMP_Text>().text = ciclo.nome;

            // Armazena uma cópia local da variável ciclo (evita problemas com closures)
            Debug.Log($"Capturando ciclo: {ciclo.nome}");
            Ciclo cicloCapturado = ciclo;

            // --- 🔹 Botão de executar ciclo ---
            btnCiclo.onClick.AddListener(() => {
                executor.iniciarCiclo(cicloCapturado);
            });

            /*
            // --- 🔹 Botão de editar ---
            btnEditar.onClick.AddListener(() => {
                abrirFormularioEdicao(cicloCapturado);
            });*/

            // --- 🔹 Botão de remover ---
            btnRemover.onClick.AddListener(() => {
                //StartCoroutine(ConfirmarRemocao(cicloCapturado));
                RemoverCiclo(cicloCapturado);
            });

            Debug.Log($"Ciclo: {ciclo.nome} com {ciclo.Atividades.Count} atividades.");
        }

        Debug.Log("Total de ciclos: " + lista.Count);
        Debug.Log("Filhos do contentParent: " + contentParent.childCount);
        
    }

    void abrirFormularioEdicao(Ciclo ciclo)
    {
        // TODO: abrir UI de edição com os dados do ciclo
        Debug.Log($"Abrir formulário de edição para: {ciclo.nome}");
    }

    void RemoverCiclo(Ciclo ciclo)
    {
        Debug.Log(ciclo.codigo);
        Debug.Log($"Removendo ciclo: {ciclo.nome}");
        dao.Remover(ciclo.codigo);
        atualizarLista(); // atualiza visualmente a lista após remover
    }



    IEnumerator ConfirmarRemocao(Ciclo ciclo)
    {
        // Aqui você pode abrir uma UI custom de confirmação.
        // Temporariamente, vamos só usar Debug e simular confirmação:
        bool confirmar = true; // substitua por seu sistema de confirmação
        if (confirmar)
        {
            dao.Remover(ciclo.codigo);
            atualizarLista();
        }
        yield return null;
    }


}
