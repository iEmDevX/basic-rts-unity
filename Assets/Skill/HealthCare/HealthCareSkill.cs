using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Turn Based/Skill/HealthCareSkill")]
public class HealthCareSkill : Skill
{

    public override SkillResult UseSkill(GameObject myCharacterObj)
    {
        SkillResult skillResult = new SkillResult();
        var characterController = myCharacterObj.GetComponent<CharacterController>();

        Debug.Log(characterController.cerrentMana);
        Debug.Log(useMana);
        if (characterController.cerrentMana < useMana)
        {
            skillResult.finish = false;
            return skillResult;
        }

        BattleSystem battleSystem = FindObjectOfType<BattleSystem>();
        var playerTeam = battleSystem.listPlayerGameObjs;

        Debug.Log(" HealthCareSkill ");
        characterController.ReduceMana(useMana);
        foreach (var player in playerTeam)
        {
            var enemyController = player.GetComponent<CharacterController>();
            enemyController.HpHeal(skillValue);
        }

        return skillResult;
    }
}
