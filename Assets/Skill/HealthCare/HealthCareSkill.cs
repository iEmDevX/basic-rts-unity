using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Turn Based/Skill/HealthCareSkill")]
public class HealthCareSkill : Skill
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
        foreach (var player in playerTeam)
        {
            var enemyController = player.GetComponent<CharacterController>();
            enemyController.HpHeal(skillValue);
        }

        return skillResult;
    }
}
