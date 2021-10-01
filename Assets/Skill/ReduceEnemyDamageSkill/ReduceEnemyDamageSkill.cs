using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Turn Based/Skill/ReduceEnemyDamageSkill")]
public class ReduceEnemyDamageSkill : Skill
{

    public override SkillResult UseSkill(GameObject myCharacterObj)
    {
        SkillResult skillResult = new SkillResult();

        BattleSystem battleSystem = FindObjectOfType<BattleSystem>();
        var enemyTeam = battleSystem.listEnemyGameObjs;

        var characterController = myCharacterObj.GetComponent<CharacterController>();
        if (characterController.cerrentMana < useMana)
        {
            return skillResult;
        }

        characterController.ReduceMana(useMana);
        foreach (var enemy in enemyTeam)
        {
            var enemyController = enemy.GetComponent<CharacterController>();
            enemyController.DebuffMyDamage(skillValue);
        }

        return skillResult;
    }
}

