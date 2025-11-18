using System.Collections.Generic;
using UnityEngine;
using LiteDB;

[System.Serializable]
public class Ciclo {

    [BsonId]
    public ObjectId codigo { get; private set; }
    public string nome { get; private set; }
    public List<string> Atividades { get; private set; }

    public void setCodigo(ObjectId codigo) {
        this.codigo = codigo;
    }

    public void setNome(string nome) {
        
        if (nome.Length < 3) {
            throw new System.Exception("O nome deve ter pelo menos 3 caracteres.");
        }

        this.nome = nome;

    }

    public void setAtividades(List<string> atividades) {

        if (atividades == null || atividades.Count == 0) {
            throw new System.Exception("A lista de atividades não pode ser nula ou vazia.");
        }

        this.Atividades = atividades;

    }

}
