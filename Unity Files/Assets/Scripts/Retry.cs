using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retry : MonoBehaviour {

    public GameObject[] startScreen, winningRestart, losingRestart;


    public void SetStartScreen(bool b)
    {
        SetList(startScreen, b);
    }
    public void SetWinningRestart(bool b)
    {
        SetList(winningRestart, b);
    }
    public void SetLosingRestart(bool b)
    {
        SetList(losingRestart, b);
    }

    private void SetList(GameObject[] gO, bool b)
    {
        foreach (GameObject g in gO)
        {
            g.SetActive(b);
        }
    }

    public void DeactivateStartScreen()
    {
        SetStartScreen(false);
    }
    public void DeactivateWinningRestart()
    {
        SetWinningRestart(false);
    }
    public void DeactivateLosingStart()
    {
        SetLosingRestart(false);
    }

    public void ActivateStartScreen()
    {
        SetStartScreen(true);
    }
    public void ActivateWinningRestart()
    {
        SetWinningRestart(true);
    }
    public void ActivateLosingStart()
    {
        SetLosingRestart(true);
    }

    public void StartAnim(string trigger)
    {
        GetComponent<Animator>().SetTrigger(trigger);
    }



}
