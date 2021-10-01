using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Turn Based/Skill/BuffMyDamage")]
public class BuffMyDamage : Skill
{

    public override SkillResult UseSkill(GameObject myCharacterObj)
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
