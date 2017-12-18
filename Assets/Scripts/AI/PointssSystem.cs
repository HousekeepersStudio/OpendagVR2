using UnityEngine;

public class PointssSystem : MonoBehaviour {

    public int score = 20;
    public int rewardPerBowKill = 10;
    public int rewardPerTurretKill = 5;
    
    public void AddPoints(string typeOfKill)
    {
        Debug.Log("PRE: " + typeOfKill);
        typeOfKill = typeOfKill.ToUpper();
        Debug.Log("POST: " + typeOfKill);

        if (typeOfKill == "BOW")
        {
            score += rewardPerBowKill;
            Debug.Log(score);

        }
        else if(typeOfKill == "TURRET"){
            score += rewardPerTurretKill;

        }
        else
        {
            Debug.Log("The given string is not an option!");

        }
    }

    public void SubmitScore(string name)
    {
        Debug.Log(name);
        // Submit score to database
    }
}
