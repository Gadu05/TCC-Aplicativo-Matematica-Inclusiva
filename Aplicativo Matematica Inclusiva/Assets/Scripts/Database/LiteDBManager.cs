using System.Collections.Generic;
using UnityEngine;
using LiteDB;

public class LiteDBManager : MonoBehaviour {

    public static LiteDBManager Instance { get; private set; }
    private static LiteDatabase db;
    public LiteDatabase Database => db;


    void Awake() {

        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        string dbPath = Application.persistentDataPath + "/ciclos.db";
        db = new LiteDatabase(dbPath);

    }

    void OnApplicationQuit() {
        db?.Dispose();
    }

}