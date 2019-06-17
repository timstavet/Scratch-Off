using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using Random = UnityEngine.Random;

public class InitializerController : MonoBehaviour {

    // Stuff for API 
    public string gameName, playerUuid;
    public int numSprites;
    public Vector2[] locations;
    public float prizeScale;


    public List<GameObject> sprites;
    public GameObject SpriteHolder;

    private GameObject[] finalSprites;
    private float winner;


    private void Start()
    {
        finalSprites = new GameObject[6];
    }

    public void InitializeScratchCard()
    {
        // Before starting make sure everything from the last round is gone
        for (int i = 0; i < finalSprites.Length; i++)
        {
            if (finalSprites[i] != null)
                Destroy(finalSprites[i].gameObject);
        }

        winner = (float)Random.Range(0,2);//GetComponent<APIHelper>().Post(gameName, playerUuid);
    

        finalSprites = new GameObject[numSprites];
        int winnerIndex = Random.Range(0, sprites.Count);

        finalSprites[0] = Instantiate(sprites[winnerIndex], Vector3.zero, Quaternion.identity);
        finalSprites[0].GetComponent<Transform>().localScale = Vector3.one * prizeScale;
        finalSprites[0].tag = "winnerKey";
        SetCanvasParent(0);

        int winnerIdexFinalSprites = -1;
        GameObject winnerSprite = sprites[winnerIndex];
        if (winner > 0)
        {
            winnerIdexFinalSprites = (int)(Random.Range(0f, 1f) * 4 + 1f);
        }
        sprites.RemoveAt(winnerIndex);

        for (int i = 1; i < numSprites; i++)
        {
            if (i == winnerIdexFinalSprites && winner > 0)
            {
                finalSprites[i] = Instantiate(winnerSprite, Vector3.zero, Quaternion.identity);
                finalSprites[i].tag = "winner";
            }
            else
            {
                finalSprites[i] = Instantiate(sprites[Random.Range(0, sprites.Count)], Vector3.zero, Quaternion.identity);
                finalSprites[i].tag = "loser";
            }
            SetCanvasParent(i);
        }
        sprites.Add(winnerSprite);


        // Place Sprites
        for(int i = 0; i < finalSprites.Length; i++)
            finalSprites[i].GetComponent<Transform>().position = new Vector3(locations[i].x, locations[i].y, 0);
    }

    public GameObject[] GetFinalSprites()
    {
        return finalSprites;
    }

    public float GetWinner()
    {
        return winner;
    }

    private void SetCanvasParent(int i)
    {
        finalSprites[i].GetComponent<Transform>().SetParent(SpriteHolder.GetComponent<Transform>());
    }


}
