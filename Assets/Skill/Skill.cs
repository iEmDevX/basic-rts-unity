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

    protected GameObject myCharacterObj;
    protected List<GameObject> enemyTeam;
    protected List<GameObject> playerTeam;

    public virtual void InitSkill(GameObject myCharacterObj, List<GameObject> enemyTeam, List<GameObject> playerTeam)
    {
        this.myCharacterObj = myCharacterObj;
        this.enemyTeam = enemyTeam;
        this.playerTeam = playerTeam;
    }
    public abstract SkillResult UseSkill();
}

public class SkillResult
{
    public bool finish = true;
}