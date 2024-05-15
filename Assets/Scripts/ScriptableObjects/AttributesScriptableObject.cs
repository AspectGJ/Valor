using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attributes", menuName = "ScriptableObjects/AttributesScriptableObject", order = 1)]
public class AttributesScriptableObject : ScriptableObject
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
}
