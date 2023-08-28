using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int skillPoints;
    public int startSkillPoints = 10;

    public static int rounds;

    void Start()
    {
        skillPoints = startSkillPoints;
        rounds = 0;
    }
}
