using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasController : MonoBehaviour
{
    public GameManager gm;
    public Button NextTurn;
    public Button Surrender;
    public Button FinishStartup;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    public void FinishStartUp()
    {
        gm.cardsInPlay.Clear();
        if (gm.PlayersReady())
        {
            FinishStartup.gameObject.SetActive(false);
            Surrender.gameObject.SetActive(true);
            NextTurn.gameObject.SetActive(true);
        }
    }

    public void MakeStartUp()
    {
        FinishStartup.gameObject.SetActive(true);
        Surrender.gameObject.SetActive(false);
        NextTurn.gameObject.SetActive(false);
    }

}
