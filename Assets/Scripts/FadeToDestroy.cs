using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeToDestroy : MonoBehaviour {

    private float alpha;
    public float delay;
    public float stepSize;

	// Use this for initialization
	void Start () {
        alpha = 1.0f;

        StartCoroutine(Fade());

    }

    public void Restart()
    {
        gameObject.SetActive(true);
        alpha = 1.0f;
        StartCoroutine(Fade());
    }


    IEnumerator Fade()
    {
        while (alpha > 0)
        {
            yield return new WaitForSeconds(delay);

            alpha -= stepSize;
            

            if (alpha <= 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                GetComponent<CanvasRenderer>().SetAlpha(alpha);
            }
        }
    }

  
}
