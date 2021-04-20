using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Scrollbar scrollbar;
    public int result, countNum, oneNum;
    int clickNum= 1000000000;

    public GameObject egg;
    public Text score;

    public void Start()
    {
        
        result = 0;
        score.text = "100,000,0000";
        StartCoroutine(CountTime());

    }

    public void Update()
    {
        
    }

    public void MainClick()
    {
        clickNum -= countNum;
        score.text = string.Format("{0:#,#}", clickNum);
    }
    IEnumerator CountTime()
    {
        int time = 0;
          
        if(scrollbar.value == 0)
        {
            StartCoroutine(CountTime());
            time++;
            clickNum -= oneNum;
            score.text = string.Format("{0:#,#}", clickNum);
            yield return new WaitForSeconds(1f);
            Debug.Log("돌고있음" + time);
        } else StopCoroutine(CountTime());


    }


    
}
