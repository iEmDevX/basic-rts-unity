using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Turn Based/Skill/AttackAllSkill")]
public class AttackAllSkill : Skill
{

    public override SkillResult UseSkill(GameObject myCharacterObj)
    {
        SkillResult skillResult = new SkillResult();

        var characterController = myCharacterObj.GetComponent<CharacterController>();
        var baseDamage = characterController.baseDamage;
        if (characterController.cerrentMana < useMana)
        {
            return skillResult;
        }

        characterController.ReduceMana(useMana);

        BattleSystem battleSystem = FindObjectOfType<BattleSystem>();
        var enemyTeam = battleSystem.listEnemyGameObjs;

        foreach (var enemy in enemyTeam)
        {
            var enemyController = enemy.GetComponent<CharacterController>();
            enemyController.TakeDamage(baseDamage);
        }
        Debug.Log(baseDamage);
        Debug.Log(enemyTeam.Count);

        return skillResult;
    }

}
