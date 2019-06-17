using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameStateManager : MonoBehaviour {
    

    public GameObject scratchCardPrefab, imageCard;
    public Camera cam;
    private GameObject oldScratchCard;

    private void Start()
    {
        /**
        // Since you can't put hierarchy objects into prefabs, we do it at start.
        scratchCardPrefab.GetComponent<ScratchCardManager>().MainCamera = cam;
        scratchCardPrefab.GetComponent<ScratchCardManager>().ImageCard = imageCard;

        // Set up the Scratch Card
        gameObject.GetComponent<InitializerController>().InitializeScratchCard();
        oldScratchCard = InstantiateScratchCard();

        // Prep the detector
        gameObject.GetComponent<OutcomeDetector>().Reset(gameObject.GetComponent<InitializerController>().GetWinner());
        */
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
        {
            Reset();
        }
	}

    public void Reset()
    {
        // Reset actual game
        gameObject.GetComponent<InitializerController>().InitializeScratchCard();

        // The reset function for the Scratch Card asset doesn't work, so I just re-instantiate a new one every time
        Destroy(oldScratchCard);
        oldScratchCard = InstantiateScratchCard();

        // Reset Outcome detector
        gameObject.GetComponent<OutcomeDetector>().Reset(gameObject.GetComponent<InitializerController>().GetWinner());

        gameObject.GetComponent<Celebrator>().StopIt();
    }

    private GameObject InstantiateScratchCard()
    {
        return Instantiate(scratchCardPrefab, new Vector3(0, 0, -8), Quaternion.identity);
    }
}
