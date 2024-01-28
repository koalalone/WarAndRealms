using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameManager _gameManager;
    [SerializeField]
    private TextMeshProUGUI _foodText;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void UpdateScore(float food)
    {

        _foodText.text = "Food: " + food.ToString("F1"); 
    }

    public void GameOver(string text)
    {
        _gameManager.GameOver(text);
    }
}
