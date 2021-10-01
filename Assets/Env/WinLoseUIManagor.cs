using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseUIManagor : MonoBehaviour
{
    [SerializeField] Text text;

    private void Start()
    {
        gameObject.SetActive(false);
    }


    public void Show(GameState gameState)
    {
        gameObject.SetActive(true);
        Debug.Log(gameState);
        if (GameState.WON.Equals(gameState))
        {
            Debug.Log("win");
            text.text = "You WIN!";
        }
        else if (GameState.LOSE.Equals(gameState))
        {
            Debug.Log("lose");
            text.text = "You LOSE!";
        }
    }

}
