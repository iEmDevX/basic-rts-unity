using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnit : MonoBehaviour
{

    public List<GameObject> SetSpawnUnit(ref List<Character> characters, UnitTeam unitTeam, List<GameObject> enemyTeam, List<GameObject> playerTeam)
    {
        int index = 0;

        if (UnitTeam.Player.Equals(unitTeam))
        {
            List<GameObject> listPlayerGameObj = new List<GameObject>() { };
            GameObject[] basePlayerObjs = GameObject.FindGameObjectsWithTag("BasePlayerTeam");
            foreach (var baseObj in basePlayerObjs)
            {
                listPlayerGameObj.Add(SpownCharator(characters[index], baseObj, unitTeam, enemyTeam, playerTeam));
                index++;
            }
            return listPlayerGameObj;
        }
        else if (UnitTeam.Enemy.Equals(unitTeam))
        {
            List<GameObject> listEnemyGameObj = new List<GameObject>() { };
            GameObject[] baseEnemyObjs = GameObject.FindGameObjectsWithTag("BaseEnemyTeam");
            foreach (var baseObj in baseEnemyObjs)
            {
                listEnemyGameObj.Add(SpownCharator(characters[index], baseObj, unitTeam, enemyTeam, playerTeam));
                index++;
            }
            return listEnemyGameObj;
        }
        return null;
    }

    private GameObject SpownCharator(Character character, GameObject baseObj, UnitTeam unitTeam, List<GameObject> enemyTeam, List<GameObject> playerTeam)
    {
        BaseController baseController = baseObj.GetComponent<BaseController>();

        // Create Instant
        GameObject model = Instantiate(character.model);

        GameObject characterGet = baseController.SetCharacter(model);
        characterGet.GetComponent<CharacterController>().InitCharacter(character, unitTeam, enemyTeam, playerTeam);
        return characterGet;
    }
}
