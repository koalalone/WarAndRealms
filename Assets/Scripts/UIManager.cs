using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameManager _gameManager;
    [SerializeField]
    private TextMeshProUGUI _foodText;
    [SerializeField]
    private TextMeshProUGUI _gameOverText;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void UpdateFood(float food)
    {

        _foodText.text = food.ToString("F1"); 
    }

    public void GameOver(string text)
    {
        _gameManager.GameOver(text);
        _gameOverText.text = text;
    }
}
