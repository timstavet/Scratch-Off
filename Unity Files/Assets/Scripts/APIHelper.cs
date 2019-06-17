using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;

public class APIHelper : MonoBehaviour {

    private float winner;

    //public Text text;
    

    // This is the method that can be used by someone else, they just supply this game info and we return whether they win
    public float Post(string gameName, string playerUuid) //What type is betAmount?
    {
        StartCoroutine(PostStartPlayRequest(SetWinner, gameName, playerUuid));

        return winner;
    }


    /** Posts a \startPlay request to ViceLotteries API using UnityWebRequest
     * Note that UnityWebRequest will be depracated in the coming years, to be replaced by a different infrastructure.
     */
    IEnumerator PostStartPlayRequest(Action<ResultHolder> onSuccess, string gN, string pID)
    {
        // This is how we create the JSON body for the post request
        WWWForm postData = new WWWForm();
        postData.AddField("gameName", gN);
        postData.AddField("playerUuid", pID);
        
        // UnityWebRequest makes creating the request easy!
        UnityWebRequest req = UnityWebRequest.Post("http://localhost:80/api/play/startplay/", postData);
        {
            // First we send request
            yield return req.SendWebRequest();

            // Until request is done, just do nothing for the frame. 
            // This is all happening in an IEnumerator which means that every time we return null, the whole function stops
            // until the next frame of the game, when it resumes back at this while loop. The net result is that the function
            // makes the request, lets the game run until the request is completed, and then we procede.
            while (!req.isDone)
                yield return null;

            // Get the data in JSON format. Truth be told, I don't understand exactly what these functions do, but they work.
            //     ¯\_(ツ)_/¯  
            byte[] result = req.downloadHandler.data;
            string resultJSON = System.Text.Encoding.Default.GetString(result);

            //LOG THAT SHIT
            //Debug.Log(resultJSON);

            // Convert from Json format to our created class ResultHolder. See that class for an explanation
            ResultHolder info = JsonUtility.FromJson<ResultHolder>(resultJSON);

            // IEnumerators can't return anything so we passed a function as a parameter and this allows us to
            // both do something with the outcome and return it to the user. For right now
            // we just set the winner boolean, so this whole passing a function as a parameter is a bit overkill,
            // but if we ever want to do something more complicated this a good way to do that.
            onSuccess(info);
        }
    }

    // This is how the JSON output is parsed. The objects in this class line up with the fields in the JSON file
    // (so there is a thing called "result" and it's a class consisting of a bunch of objects, 
    // one of those being a boolean called winner. So we make another class and give it a boolean called winner so that 
    // Unity goes through the JSON file and lines up our result with JSON's result and our winner with JSON's winner. 
    // If we wanted different information, we would have to do analgous class constructions.
    [Serializable]
    public class ResultHolder
    {
        public Result result;
        public Player player;
    }

    [Serializable]
    public class Result
    {
        public bool winner;
        public float winAmount; //Win Amount is in cents
    }


    [Serializable]
    public class Player
    {
        public float balance;
    }


    private void SetWinner(ResultHolder resultHolder) 
    {
        winner = resultHolder.result.winAmount;
        winner = winner / 100f; //Convert winner from cents to dollars

    //    text.text = "" + resultHolder.player.balance;
    }


}
