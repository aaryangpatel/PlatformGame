using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Message : MonoBehaviour
{
    public TextMeshProUGUI text;
    void Start()
    {
        text.text = (TotalScore.finalMessage);
    }
}
