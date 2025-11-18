using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BotaoVoltar : MonoBehaviour {
    void Update() {
        // Detecta o botão "voltar" do Android usando o novo Input System
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame) {
            VoltarCena();
        } else if (Gamepad.current != null && Gamepad.current.startButton.wasPressedThisFrame) {
            VoltarCena();
        }
    }

    private void VoltarCena() {
        int cenaAtualIndex = SceneManager.GetActiveScene().buildIndex;
        if (cenaAtualIndex > 0) {
            SceneManager.LoadScene("Menu");
        } else {
            Debug.Log("Primeira cena — nada para voltar.");
        }
    }
}
