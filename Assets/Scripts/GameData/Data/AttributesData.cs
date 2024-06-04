using System;
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

    public int Healmin;
    public int Healmax;
    public int HealCost;

    public int Stunmin;
    public int Stunmax;
    public int StunCost;

    public int SHAttackmin;
    public int SHAttackmax;
    public int SHAttackCost;
    

    public AttributesData(bool isPlayer) {
        if(isPlayer)
        {
            healthPoint = 300;
            mana = 200;
        }
        else
        {
            healthPoint = 200;
            mana = 150;
        }
        
        attackmin = 5;
        attackmax = 15;
        attackCost = 5;

        StrongAttackmin = 10;
        StrongAttackmax = 30;
        StrongAttackCost = 7;

        DeepSharpnessmin = 2;
        DeepSharpnessmax = 6;
        DeepSharpnessCost = 4;

        Healmin = 5;
        Healmax = 12;
        HealCost = 5;

        Stunmin = 0;
        Stunmax = 2;
        StunCost = 3;

        SHAttackmin = 4;
        SHAttackmax = 7;
        SHAttackCost = 4;
    }

    public String toString() {
        return "Health: " + healthPoint + ",\n" + 
        " Mana: " + mana + ",\n" +
        " Attack Min: " + attackmin + ",\n" +
        " Attack Max: " + attackmax + ",\n" +
        " Attack Cost: " + attackCost + ",\n" +
        " Strong Attack Min: " + StrongAttackmin + ",\n" +
        " Strong Attack Max: " + StrongAttackmax + ",\n" +
        " Strong Attack Cost: " + StrongAttackCost + ",\n" +
        " Deep Sharpness Min: " + DeepSharpnessmin + ",\n" +
        " Deep Sharpness Max: " + DeepSharpnessmax + ",\n" +
        " Deep Sharpness Cost: " + DeepSharpnessCost + ",\n" +
        " Enemy Attack Min: " + enemyAttackmin + ",\n" +
        " Enemy Attack Max: " + enemyAttackmax + ",\n" +
        " Enemy Vanish Min: " + enemyVanishmin + ",\n" +
        " Enemy Vanish Max: " + enemyVanishmax + ",\n";
    }
}
