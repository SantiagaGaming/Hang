using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
   [SerializeField] GameObject startWindow;
    [SerializeField] GameObject gameWindow;

    public void StartGame()
    {
        startWindow.SetActive(false);
        gameWindow.SetActive(true);

    }
}
