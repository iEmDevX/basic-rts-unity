using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public enum GameState
{
    WON,
    LOSE,
}

public class BattleSystem : MonoBehaviour
{
    bool isEnd = false;
    [SerializeField] List<Character> listPlayer = new List<Character>() { };
    [SerializeField] List<Character> listEnemy = new List<Character>() { };

    public List<GameObject> listEnemyGameObjs;
    public List<GameObject> listPlayerGameObjs;

    private SpawnUnit spawnUnit;
    private UiManager uiManager;
    [SerializeField] WinLoseUIManagor winLoseUIManagor;

    private int playerTurnSeq = 0;
    private int enemyTurnSeq = 0;

    private GameObject cerrentCharacter;
    private CharacterController cerrentCon;

    List<string> dieIds = new List<string>();

    private void Awake()
    {
        spawnUnit = FindObjectOfType<SpawnUnit>();
        uiManager = FindObjectOfType<UiManager>();
    }

    async void Start()
    {
        await SetupBattle();

        int index = 0;
        foreach (var playerGameObj in listPlayerGameObjs)
        {
            _ = StartPlayerBasicAttack(playerGameObj, index);
            index++;
        }

        index = 0;
        foreach (var enemyGameObj in listEnemyGameObjs)
        {
            _ = StartEnemyBasicAttack(enemyGameObj, index);
            index++;
        }

    }

    private async Task SetupBattle()
    {
        // Spawn Unit Enemy
        listEnemyGameObjs = spawnUnit.SetSpawnUnit(listEnemy, UnitTeam.Enemy);

        // Spawn Unit Player
        listPlayerGameObjs = spawnUnit.SetSpawnUnit(listPlayer, UnitTeam.Player);
        
        PlayerTurn();
        await Task.Delay(1000);
    }

    private void PlayerTurn()
    {
        uiManager.SetCerrent(listPlayerGameObjs[2].GetComponent<CharacterController>());
    }

    async Task StartEnemyBasicAttack(GameObject enemyGameObj, int index)
    {
        string id = UnitTeam.Enemy.ToString() + index.ToString();
        while (!isEnd && enemyGameObj != null)
        {
            if (dieIds.Find(item => item == id) != null)
            {
                break;
            }
            await EnemyBasicAttack(enemyGameObj, id);
        }

        Debug.Log("Enemy => " + enemyGameObj.name + " Die");
    }

    async Task StartPlayerBasicAttack(GameObject playerGameObj, int index)
    {
        string id = UnitTeam.Player.ToString() + index.ToString();
        while (!isEnd && playerGameObj != null)
        {
            if (dieIds.Find(item => item == id) != null)
            {
                break;
            }
            await PlayerBasicAttack(playerGameObj, id);
        }

        Debug.Log("Player => " + playerGameObj.name + " Die");
    }

    private async Task PlayerBasicAttack(GameObject playerCharacter, string id)
    {
        // Get Player
        var playerCon = playerCharacter.GetComponent<CharacterController>();

        // Wait Attack Speed
        await Task.Delay(Mathf.CeilToInt((playerCon.attackSpeed * 1000f)));

        if (dieIds.Find(item => item == id) != null)
        {
            return;
        }

        // Get Enemy
        var enemyUnit = listEnemyGameObjs[0];
        var enemyCon = enemyUnit.GetComponent<CharacterController>();

        playerCon.UseBasicHit();

        // Wait Bullet Time
        await Task.Delay(200);
        bool isDead = enemyCon.TakeDamage(playerCon.baseDamage);

        if (isDead)
        {
            dieIds.Add(enemyCon.id);
            listEnemyGameObjs.Remove(enemyUnit);
            CheckEndGame(true);
        }

    }

    private async Task EnemyBasicAttack(GameObject enemyCharacter, string id)
    {
        // Get Enemy
        var enemyCon = enemyCharacter.GetComponent<CharacterController>();

        // Wait Attack Speed
        await Task.Delay(Mathf.CeilToInt((enemyCon.attackSpeed * 1000f)));

        if (dieIds.Find(item => item == id) != null)
        {
            return;
        }

        // Get Player
        var playerUnit = listPlayerGameObjs[0];
        var playerCon = playerUnit.GetComponent<CharacterController>();

        enemyCon.UseBasicHit();

        // Wait Bullet Time
        await Task.Delay(200);
        bool isDead = playerCon.TakeDamage(enemyCon.baseDamage);

        if (isDead)
        {
            dieIds.Add(playerCon.id);
            listPlayerGameObjs.Remove(playerUnit);
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
            winLoseUIManagor.Show(GameState.WON);
        }
        else if (!isPlayer && listPlayerGameObjs.Count == 0)
        {
            // Lost
            isEnd = true;
            Debug.Log("End Enemy Win");
            winLoseUIManagor.Show(GameState.LOSE);
        }

    }

}