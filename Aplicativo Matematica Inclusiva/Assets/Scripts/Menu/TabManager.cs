using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour {
    
    [SerializeField]
    private GameObject panelCiclos;
    [SerializeField]
    private GameObject panelAtividades;
    [SerializeField]
    private GameObject panelDetalhes;
    [SerializeField]
    private Button btnCiclos;
    [SerializeField]
    private Button btnAtividades;

    public void ShowCiclos() {
        this.HideAll();
        this.panelCiclos.SetActive(true);
        this.btnCiclos.GetComponentInChildren<TMP_Text>().text = "<color=#35B6EE><sprite=7 color=#35B6EE>\nHOME";
        this.btnAtividades.GetComponentInChildren<TMP_Text>().text = "<color=#8C7E7E><sprite=3 color=#8C7E7E>\nATIVIDADES";
    }

    public void ShowAtividades() {
        this.HideAll();
        this.panelAtividades.SetActive(true);
        this.btnCiclos.GetComponentInChildren<TMP_Text>().text = "<color=#8C7E7E><sprite=7 color=#8C7E7E>\nHOME";
        this.btnAtividades.GetComponentInChildren<TMP_Text>().text = "<color=#35B6EE><sprite=3 color=#35B6EE>\nATIVIDADES";
    }

    private void HideAll() {
        this.panelCiclos.SetActive(false);
        this.panelAtividades.SetActive(false);
        this.panelDetalhes.SetActive(false);
    }
    
}
