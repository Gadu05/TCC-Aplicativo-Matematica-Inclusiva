using UnityEngine;

public class AtividadeItemUI : MonoBehaviour{

    public AtividadeInfo atividadeInfo { get; private set; }

    public void setAtividadeInfo(AtividadeInfo atividadeInfo) {
        this.atividadeInfo = atividadeInfo;
    }

}
