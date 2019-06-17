using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutcomeDetector : MonoBehaviour {

    public Camera cam;

    private bool winningCard, readyToWin, winnerDetected, gameOver;
    private int detectedCount;
    public float restartDelay;
    public GameObject[] winningRestart, losingRestart;

    // Note we don't need to start because MyGameStateManager resets this on start

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButton(0))
        {

            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && !gameOver)
            {
                Transform objectHit = hit.transform;

                Vector2 hitPos = new Vector2(objectHit.position.x, objectHit.position.y);
                Vector2 clickPos = new Vector2(ray.GetPoint(0f).x, ray.GetPoint(0f).y);

                // If the center is uncovered
                if (Vector2.Distance(hitPos, clickPos) < 0.5)
                {
                    
                    if (objectHit.CompareTag("winner"))
                    {
                        winnerDetected = true;
                        Destroy(objectHit.GetComponent<Collider>());
                        detectedCount++;
                    }
                    else if (objectHit.CompareTag("winnerKey"))
                    {
                        readyToWin = true;
                        detectedCount++;
                        Destroy(objectHit.GetComponent<Collider>());
                    }
                    else if (objectHit.CompareTag("loser"))
                    {
                        detectedCount++;
                        Destroy(objectHit.GetComponent<Collider>());
                    }
                }
            }
        }

        // Mobile
        if (Input.touchCount > 0)
        {

            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.touches[0].position);

            if (Physics.Raycast(ray, out hit) && !gameOver)
            {
                Transform objectHit = hit.transform;

                Vector2 hitPos = new Vector2(objectHit.position.x, objectHit.position.y);
                Vector2 clickPos = new Vector2(ray.GetPoint(0f).x, ray.GetPoint(0f).y);

                // If the center is uncovered
                if (Vector2.Distance(hitPos, clickPos) < 0.5)
                {

                    if (objectHit.CompareTag("winner"))
                    {
                        winnerDetected = true;
                        Destroy(objectHit.GetComponent<Collider>());
                        detectedCount++;
                    }
                    else if (objectHit.CompareTag("winnerKey"))
                    {
                        readyToWin = true;
                        detectedCount++;
                        Destroy(objectHit.GetComponent<Collider>());
                    }
                    else if (objectHit.CompareTag("loser"))
                    {
                        detectedCount++;
                        Destroy(objectHit.GetComponent<Collider>());
                    }
                }
            }
        }


        if (!gameOver)
        {
            if (readyToWin && winnerDetected && winningCard)
            {
                gameOver = true;
                gameObject.GetComponent<Celebrator>().Celebrate();
                StartCoroutine(ActivateRestart());
            }

            else if (!winningCard && detectedCount == gameObject.GetComponent<InitializerController>().GetFinalSprites().Length)
            {
                gameOver = true;
                StartCoroutine(ActivateRestart());
            }
        }
    }

 
    public IEnumerator ActivateRestart()
    {
        yield return new WaitForSeconds(restartDelay);
        if (winningCard)
        {
            foreach (GameObject g in winningRestart)
            {
                g.SetActive(true);
                g.GetComponent<Animator>().SetTrigger("Appear");
            }
        }
        else
        {
            foreach (GameObject g in losingRestart)
            {
                g.SetActive(true);
                g.GetComponent<Animator>().SetTrigger("Appear");
            }
        }
    }



    public void Reset(float winningRound)
    {
        winningCard = winningRound > 0;
        winnerDetected = false;
        readyToWin = false;
        gameOver = false;
        detectedCount = 0;
    }
    
}
