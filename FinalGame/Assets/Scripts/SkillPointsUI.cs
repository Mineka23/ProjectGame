using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPointsUI : MonoBehaviour
{
    public Text spText;

    void Update()
    {
        spText.text = "SP: " + PlayerStats.skillPoints.ToString();
    }
}
