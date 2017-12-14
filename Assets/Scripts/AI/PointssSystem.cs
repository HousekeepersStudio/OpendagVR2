using UnityEngine;

public class PointssSystem : MonoBehaviour {

    public int score = 20;
    public int rewardPerBowKill = 10;
    public int rewardPerTurretKill = 5;
    
    public void AddPoints(string typeOfKill)
    {
        if(typeOfKill == "Bow" || typeOfKill == "bow")
        {
            score += rewardPerBowKill;
            Debug.Log(score);

        }
        else if(typeOfKill == "Turret" || typeOfKill == "turret"){
            score += rewardPerTurretKill;

        }
        else
            Debug.Log("The given string is not an option!");
    }

    public void SubmitScore(string name)
    {
        Debug.Log(name);
        // Submit score to database
    }
}
