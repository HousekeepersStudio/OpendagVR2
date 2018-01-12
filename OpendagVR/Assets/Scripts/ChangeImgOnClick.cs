using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImgOnClick : MonoBehaviour {

    public Sprite[] allSprites;

    public int currentSprite = 0;
    public Sprite nextSprite;
    public GameObject target;


    public void ChangeSprite(bool isForward)
    {
        if (isForward)
        {
            currentSprite++;
        }
        else
        {
            currentSprite--;
        }
        
        if (currentSprite >= 4)
        {
            currentSprite = 0;
        }
        if (currentSprite < 0)
        {
            currentSprite = 3;
        }
        this.nextSprite = allSprites[currentSprite];        
    }


    public void LoadImg(bool isForward = true)
    {
        ChangeSprite(isForward);
        target.gameObject.GetComponent<Image>().sprite = nextSprite;
        PlayerPrefs.SetString("House", nextSprite.name);
    }
    //private void update()
    //{
    //    if (input.getkeydown(keycode.rightarrow))
    //        changesprite(true);

    //    else if(input.getkeydown(keycode.leftarrow))
    //        changesprite(false);
    //}
}
