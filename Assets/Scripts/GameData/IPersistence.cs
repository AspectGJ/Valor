using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPersistence 
{
    //load data
    void LoadData(Data data);

    //save data
    void SaveData(Data data);

    public bool Attack();

    public bool StrongAttack();

    public bool DeepSharpness();

    public void TakeDamage(int damage);

    public void IncreaseMana(int amount);

    public void DecreaseMana(int cost);


}
