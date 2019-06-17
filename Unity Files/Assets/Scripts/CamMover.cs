using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMover : MonoBehaviour {

    // Automatic 
    public Vector3 targetPos;
    public float lerpRate; //Value between 0 and 1 pls

    public float firstWait;

    private Vector3 startPos;
    private float lerpProgress;
    private bool moveCam;
    private int direction;

    // Manual
    private Vector3 clickPos, dragPos;

    // Use this for initialization
    void Start ()
    {
        startPos = GetComponent<Transform>().position;
        lerpProgress = 0f;
        moveCam = false;
        direction = 1;
    }
	

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(firstWait);
        moveCam = true;
        direction = 1;
        firstWait = 0;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            moveCam = false;
            clickPos = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition).GetPoint(0f);
        }

        if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
            DragCam();
        
        if (moveCam)
            LerpCam();

        if (Input.GetKeyDown(KeyCode.M))
            SwitchDirection(true);
    }

    public void SwitchDirection(bool moving)
    {
        direction *= -1;
        moveCam = moving;
    }

    void LerpCam()
    {
        if (Mathf.Abs((1 + direction) / 2 - lerpProgress) > lerpRate) //AKA not done lerping
        {
            lerpProgress = 1 - Vector3.Distance(GetComponent<Transform>().position, targetPos) / Vector3.Distance(targetPos, startPos);
            lerpProgress += direction * lerpRate; // direction is either 1 or -1
            GetComponent<Transform>().position = Vector3.Lerp(startPos, targetPos, lerpProgress);
        }
        else // Done lerping
        {
            lerpProgress = Mathf.Round(lerpProgress);
            GetComponent<Transform>().position = Vector3.Lerp(startPos, targetPos, lerpProgress);
            moveCam = false;
        }
    }

    void DragCam()
    {
        Vector3 mousePos = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition).GetPoint(0f);
        float sgn = -Mathf.Sign(mousePos.y - clickPos.y); //NEGATIVE BECAUSE FUCK THE SYSTEM
        float l = Mathf.Min(sgn * (mousePos.y - clickPos.y) / Vector3.Distance(startPos, targetPos), 1f);
        lerpProgress += sgn * l;
        lerpProgress = Mathf.Max(lerpProgress, 0f);
        lerpProgress = Mathf.Min(lerpProgress, 1f);
        GetComponent<Transform>().position = Vector3.Lerp(startPos, targetPos, lerpProgress);
    }

    public void MoveDown()
    {
        if (firstWait > 0)
            StartCoroutine(Wait());
        else
        {
            moveCam = true;
            direction = 1;
        }
    }

    public void MoveUp()
    {
        moveCam = true;
        direction = -1;
    }
}
