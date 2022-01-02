using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController instance;

    private Bot[] bots;
    private float time;

    private UnityEvent update = new UnityEvent();
    private UnityAction gui;

    public static GameController Get { get => instance; }
    public Bot[] Bots { get => bots;  }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gui = GameStart;
    }

    private void Update()
    {
        update.Invoke();
    }

    public void Enable()
    {
        bots = FindObjectsOfType<Bot>();
        time = 0;

        foreach (var bot in bots)
        {
            bot.Enable();
        }

        update.AddListener(Timer);
        gui = BotsStatus;
    }

    public void Disable()
    {
        update.RemoveListener(Timer);
        gui = GameEnd;
    }

    private void Timer()
    {
        time += Time.deltaTime;
    }

    // Все строки ниже отрисовка GUI

    private void OnGUI()
    {
        gui.Invoke();

    }

    private void GameStart()
    {

        if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2, 100, 30), "Start"))
        {
            Enable();

        }
    }
    private void GameEnd()
    {
        GUI.color = Color.black;
        GUI.Label(new Rect(Screen.width / 2, Screen.height / 2+ 100, 200, 50), "Game Time: " + time +" s");

        GUI.color = Color.white;
        if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + 130, 100, 30), "Reload"))
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(index);
        }
    }

    private void BotsStatus()
    {
        float posY = 10;
        foreach (var bot in bots)
        {
            if (bot == null) continue;

            posY += 30;

            string txt = bot.name + " HP: " + bot.Hp;

            if (bot.IsEnamy(ObjectType.RedTeam))
            {
                GUI.color = Color.cyan;
            }
            else
            {
                GUI.color = Color.red;
            }

            GUI.Label(new Rect(10, posY, 200, 20), txt);
        }
    }



}
