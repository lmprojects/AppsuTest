using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    protected Text scoreTxt;
    [SerializeField]
    protected Text distanceTxt;
    // Start is called before the first frame update
    void Start()
    {
        scoreTxt.text = "Score: 0";
        distanceTxt.text = "Distance: 0";
    }

    public void UpdateUI(int score, float distance)
    {
        scoreTxt.text = "Score: " + score;
        distanceTxt.text = "Distance: " + Math.Round((decimal)distance, 3);
    }
}
