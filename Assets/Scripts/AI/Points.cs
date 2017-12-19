using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Points : MonoBehaviour {

    public int score = 0;
    public int balance = 0;
    public int rewardPerBowKill = 10;
    public int rewardPerTurretKill = 5;
    public bool scoreAdded = false;
    
    public void AddPoints(string typeOfKill)
    {
        typeOfKill = typeOfKill.ToUpper();

        switch (typeOfKill)
        {
            case "BOW":
                score += rewardPerBowKill;
                balance += rewardPerBowKill;

                Debug.Log("Poinssystem Points:" + score);
                Debug.Log("Balance :" + balance);

                break;
            case "TURRET":
                score += rewardPerTurretKill;
                balance += rewardPerTurretKill;

                Debug.Log("Poinssystem Points:" + score);
                Debug.Log("Balance :" + balance);

                break;
            default:
                Debug.Log("The given string is not an option!");

                break;
        }
    }

    public void SubmitScore(string name)
    {
        StartCoroutine(Upload(name));   
    }

    IEnumerator Upload(string name)
    {
        scoreAdded = false;
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("score=" + score + "&name=" + name));

        UnityWebRequest www = UnityWebRequest.Get("https://www.koenvuurens.tk/school/vr/submitHandler.php?score=" + score + "&name=" + name + "&key=aeae846e5d69ecaee6c76f37c143f263");
        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
            scoreAdded = true;
        }
    }
    
    public int GetScore()
    {
        return score;
    }

    public void BuyTower(int cost)
    {
        balance -= cost;
    }
}