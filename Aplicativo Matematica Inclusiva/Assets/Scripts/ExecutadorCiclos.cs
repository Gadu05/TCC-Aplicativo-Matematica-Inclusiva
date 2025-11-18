using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ExecutadorCiclos : MonoBehaviour {

    // Instância única acessível em qualquer cena
    public static ExecutadorCiclos instance { get; private set; }

    private Ciclo cicloAtual;
    private int indiceAtual;

    private void Awake() {
        // Garante que só exista um CicloExecutor
        if (instance != null && instance != this) {
            Destroy(gameObject); // já existe outro, destrói este
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // não destrói ao trocar de Scene
    }

    // Inicia execução do ciclo
    public void iniciarCiclo(Ciclo ciclo) {
        this.cicloAtual = ciclo;
        this.indiceAtual = 0;
        this.carregarProximaAtividade();
    }

    private void carregarProximaAtividade() {

        if (cicloAtual == null) {
            SceneManager.LoadScene("Menu");
        }

        if (indiceAtual < cicloAtual.Atividades.Count) {
            string sceneName = cicloAtual.Atividades[indiceAtual];
            indiceAtual++;

            Debug.Log("Carregando Scene: " + sceneName);
            SceneManager.LoadScene(sceneName);
        } else {
            Debug.Log("Ciclo concluído!");
            SceneManager.LoadScene("Menu"); // volta para tela inicial (ajuste o nome)
        }
    }

    // Chamado no fim de cada atividade
    public void proximaAtividade() {
        SceneManager.LoadScene("ConclusaoAtividade");
    }

    public void btnCarregarProximaAtividade() {
        carregarProximaAtividade();
    }

    public int getQtdeAtividades() {
        return cicloAtual != null ? cicloAtual.Atividades.Count : 0;
    }

    public int getQtdeAtividadesRestantes() {
        return cicloAtual != null ? cicloAtual.Atividades.Count - indiceAtual : 0;
    }

}
