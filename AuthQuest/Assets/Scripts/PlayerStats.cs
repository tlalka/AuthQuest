using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int MeleeAttack;
    public int MeleeDefense;
    public int RangeAttack;
    public int RangeDefense;
    public int Speed;
    public int Health;
    public int currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        MeleeAttack = 1;
        MeleeDefense = 1;
        RangeAttack = 1;
        RangeDefense = 1;
        Speed = 1;
        Health = 100;
        currentLevel = 1;

}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelUp(string color)
    {
        switch (color)
        {
            case "red":
                MeleeAttack++;
                break;
            case "yellow":
                Speed++;
                break;
            case "green":
                Health++;
                break;
            case "blue":
                MeleeDefense++;
                break;
            case "pink":
                RangeAttack++;
                break;
            case "purple":
                RangeDefense++;
                break;
        }
        currentLevel++;
    }
}
