using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Points : MonoBehaviour {

    public int score = 20;
    public int rewardPerBowKill = 10;
    public int rewardPerTurretKill = 5;
    
    public void AddPoints(string typeOfKill)
    {
        if(typeOfKill == "Bow" || typeOfKill == "bow")
        {
            score += rewardPerBowKill;
            Debug.Log("Poinssystem Points:" + score);
        }
        else if(typeOfKill == "Turret" || typeOfKill == "turret"){
            score += rewardPerTurretKill;
            Debug.Log("Poinssystem Points:" + score);
        }
        else
            Debug.Log("The given string is not an option!");
    }

    public void SubmitScore(string name)
    {
        StartCoroutine(Upload());   
    }

    IEnumerator Upload()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("score=" + score + "&name=" + name));

        UnityWebRequest www = UnityWebRequest.Post("https://www.koenvuurens.tk/school/vr/submitHandler.php", formData);
        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }
    
    public int GetScore()
    {
        return score;
    }
}