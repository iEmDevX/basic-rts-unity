using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    SingleTarget,
    AllTarget,
    SingleBuff,
    MyBuff,
    AllBuff,
}

public abstract class Skill : ScriptableObject
{
    public string sillName;
    public Sprite skillImg;
    public string skillDesc;
    public SkillType skillType;
    public int useMana;
    // Damage // buff
    public int skillValue;
    public abstract SkillResult UseSkill(GameObject myCharacterObj);
}

public class SkillResult
{
    public bool finish = true;
}