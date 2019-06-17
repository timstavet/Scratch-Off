using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Celebrator : MonoBehaviour {
    public GameObject fireworkPrefab;

    public float startDelay;
    public float loopDelay;

    private List<GameObject> myFireworks;
    private bool celebrating;

    private void Start()
    {
        myFireworks = new List<GameObject>();
    }

    public void Celebrate()
    {
        celebrating = true;
        StartCoroutine(CelebrateActual());
        GetComponent<AudioSource>().enabled = true;
    }

    public void StopIt()
    {
        foreach(GameObject firework in myFireworks)
        {
            Destroy(firework);
        }
        celebrating = false;
        GetComponent<AudioSource>().enabled = false;
    }

    private IEnumerator CelebrateActual() 
    {
        yield return new WaitForSeconds(startDelay);
        while (celebrating)
        {
            myFireworks.Add(Instantiate(fireworkPrefab, new Vector3(Random.Range(-10f, 10f), Random.Range(-5f, 5f), -5), Quaternion.identity));
            yield return new WaitForSeconds(loopDelay);
        }
    }


}
