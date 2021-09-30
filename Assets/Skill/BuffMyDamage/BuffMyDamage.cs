using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Turn Based/Skill/BuffMyDamage")]
public class BuffMyDamage : Skill
{
    public override void InitSkill(GameObject myCharacterObj, List<GameObject> enemyTeam, List<GameObject> playerTeam)
    {
        base.InitSkill(myCharacterObj, enemyTeam, playerTeam);
    }

    public override SkillResult UseSkill()
    {
        SkillResult skillResult = new SkillResult();

        var characterController = myCharacterObj.GetComponent<CharacterController>();
        if (characterController.cerrentMana < useMana)
        {
            return skillResult;
        }

        characterController.ReduceMana(useMana);
        characterController.BuffMyDamage(skillValue);

        return skillResult;
    }
}