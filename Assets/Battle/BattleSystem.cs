using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class BattleSystem : MonoBehaviour
{
    bool isEnd = false;
    [SerializeField] List<Character> listPlayer = new List<Character>() { };
    [SerializeField] List<Character> listEnemy = new List<Character>() { };

    public List<GameObject> listEnemyGameObjs;
    public List<GameObject> listPlayerGameObjs;

    private SpawnUnit spawnUnit;

    private int playerTurnSeq = 0;
    private int enemyTurnSeq = 0;

    private GameObject cerrentCharacter;
    private CharacterController cerrentCon;

    private void Awake()
    {
        spawnUnit = FindObjectOfType<SpawnUnit>();
    }

    async void Start()
    {
        await SetupBattle();

        foreach (var playerGameObj in listPlayerGameObjs)
            _ = StartPlayerBasicAttack(playerGameObj);

        foreach (var enemyGameObj in listEnemyGameObjs)
            _ = StartEnemyBasicAttack(enemyGameObj);

    }

    async Task StartEnemyBasicAttack(GameObject enemyGameObj)
    {

        while (!isEnd && enemyGameObj != null)
        {
            await AnemyBasicAttack(enemyGameObj);
        }
        Debug.Log("Enemy => " + enemyGameObj.name + " Die");
    }

    async Task StartPlayerBasicAttack(GameObject playerGameObj)
    {
        while (!isEnd && playerGameObj != null)
        {
            await PlayerBasicAttack(playerGameObj);
        }
        Debug.Log("Player => " + playerGameObj.name + " Die");
    }

    private async Task SetupBattle()
    {
        // Spawn Unit Player
        listPlayerGameObjs = spawnUnit.SetSpawnUnit(ref listPlayer, UnitTeam.Player, listEnemyGameObjs, listPlayerGameObjs);

        // Spawn Unit Enemy
        listEnemyGameObjs = spawnUnit.SetSpawnUnit(ref listEnemy, UnitTeam.Enemy, listEnemyGameObjs, listPlayerGameObjs);

        await Task.Delay(1000);

        PlayerTurn();
    }

    private void PlayerTurn()
    {
        cerrentCharacter = listEnemyGameObjs[playerTurnSeq];
        cerrentCon = cerrentCharacter.GetComponent<CharacterController>();
    }

    private IEnumerator PlayerBasicAttack()
    {
        // Character use Animation
        cerrentCon.UseBasicHit();

        // Get Enemy
        var enemyUnit = listEnemyGameObjs[0];

        // Take Damage
        var enemyCon = enemyUnit.GetComponent<CharacterController>();
        bool isDead = enemyCon.TakeDamage(cerrentCon.baseDamage);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            listEnemyGameObjs.Remove(enemyUnit);
            CheckEndGame(true);
        }
    }

    private async Task PlayerBasicAttack(GameObject playerCharacter)
    {
        // Get Player
        var playerCon = playerCharacter.GetComponent<CharacterController>();

        // Wait Attack Speed
        await Task.Delay(Mathf.CeilToInt((playerCon.attackSpeed * 1000f)));

        // Get Enemy
        var enemyUnit = listEnemyGameObjs[0];
        var enemyCon = enemyUnit.GetComponent<CharacterController>();

        playerCon.UseBasicHit();

        // Wait Bullet Time
        await Task.Delay(200);
        bool isDead = enemyCon.TakeDamage(playerCon.baseDamage);

        if (isDead)
        {
            listEnemyGameObjs.Remove(enemyUnit);
            enemyUnit = null;
            CheckEndGame(true);
        }

    }

    private async Task AnemyBasicAttack(GameObject enemyCharacter)
    {
        // Get Enemy
        var enemyCon = enemyCharacter.GetComponent<CharacterController>();

        // Wait Attack Speed
        await Task.Delay(Mathf.CeilToInt((enemyCon.attackSpeed * 1000f)));

        // Get Player
        var playerUnit = listPlayerGameObjs[0];
        var playerCon = playerUnit.GetComponent<CharacterController>();

        enemyCon.UseBasicHit();

        // Wait Bullet Time
        await Task.Delay(200);
        bool isDead = playerCon.TakeDamage(enemyCon.baseDamage);

        if (isDead)
        {
            listPlayerGameObjs.Remove(playerUnit);
            playerUnit = null;
            CheckEndGame(false);
        }

    }

    private void CheckEndGame(bool isPlayer)
    {
        if (isEnd) return;

        if (isPlayer && listEnemyGameObjs.Count == 0)
        {
            // Won
            isEnd = true;
            Debug.Log("End Player Win");
        }
        else if (!isPlayer && listPlayerGameObjs.Count == 0)
        {
            // Lost
            isEnd = true;
            Debug.Log("End Enemy Win");
        }

    }

}