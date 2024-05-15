using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttributesData
{
    public int healthPoint;
    public int mana;

    public int attackmin;
    public int attackmax;
    public int attackCost;

    public int StrongAttackmin;
    public int StrongAttackmax;
    public int StrongAttackCost;

    public int DeepSharpnessmin;
    public int DeepSharpnessmax;
    public int DeepSharpnessCost;

    public int enemyAttackmin;
    public int enemyAttackmax;

    public int enemyVanishmin;
    public int enemyVanishmax;
    

    public AttributesData() {
        healthPoint = 90;
        mana = 60;
        
        attackmin = 5;
        attackmax = 15;
        attackCost = 5;

        StrongAttackmin = 10;
        StrongAttackmax = 30;
        StrongAttackCost = 7;

        DeepSharpnessmin = 2;
        DeepSharpnessmax = 6;
        DeepSharpnessCost = 4;
    }
}
