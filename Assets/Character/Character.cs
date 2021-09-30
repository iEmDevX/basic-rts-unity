using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Turn Based/Character")]
public class Character : ScriptableObject
{
    public string characterName;
    public GameObject model;
    public int baseDamage;
    public int maxHP;
    public int maxMana;
    public float acttackSpeed;

    [Tooltip("in millisec"), Min(0)]
    public int manaHealRate;

    [Tooltip("add mana per round"), Min(0)]
    public int manaHealValue;

    public Skill skill1;
    public Skill skill2;
}
