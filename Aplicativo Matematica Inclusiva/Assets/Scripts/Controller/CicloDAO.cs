using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using LiteDB;

public class CicloDAO{

    public void Inserir(Ciclo ciclo) {
        var col = LiteDBManager.Instance.Database
            .GetCollection<Ciclo>("ciclos");
        col.Insert(ciclo);
    }

    public List<Ciclo> Listar() {
        return LiteDBManager.Instance.Database
            .GetCollection<Ciclo>("ciclos")
            .FindAll()
            .ToList();
    }

    public void Atualizar(Ciclo ciclo) {
        LiteDBManager.Instance.Database
            .GetCollection<Ciclo>("ciclos")
            .Update(ciclo);
    }

    public void Remover(ObjectId id) {
        LiteDBManager.Instance.Database
            .GetCollection<Ciclo>("ciclos")
            .Delete(id);
    }

}
