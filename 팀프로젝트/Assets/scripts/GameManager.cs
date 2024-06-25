using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public PlayerMove player; 

    public static GameManager instance { get { set(); return _instance; } }
    public static void set()
    {
        GameObject go = GameObject.Find("GameManager");
        if (go == null)
        {
            go = new GameObject { name = "GameManager" };
            _instance = go.AddComponent<GameManager>();
        }
        return;
    }

    public bool stop = false;

    private void Awake()
    {
        _instance = this;
    }
}
