using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Unit_Data", order = 1)]
public class Data : ScriptableObject
{
    public float hp;
    public float damage;
    public float moveSpeed;
    public float attackSpeed;
}
