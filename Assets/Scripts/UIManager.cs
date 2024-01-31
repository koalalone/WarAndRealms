using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager _gameManager;
    private AllyBase _allyBase;
    [SerializeField]
    private TextMeshProUGUI _foodText;
    [SerializeField]
    private TextMeshProUGUI _gameOverText;

    [SerializeField]
    private Button[] _spawnButtons;

    private float[] _unitCosts;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _allyBase = GameObject.Find("AllyBase").GetComponent<AllyBase>();
        _unitCosts = _allyBase.getCosts();
    }

    public void UpdateFood(float food)
    {
        _foodText.text = food.ToString("F1");
        UpdateButtons(food);
    }

    public void GameOver(string text)
    {
        _gameManager.GameOver(text);
        _gameOverText.text = text;
    }

    public void Spawn(int id)
    {
        _allyBase.Spawn(id);
    }

    public void UpdateButtons(float food)
    {
        for (int i = 0; i < 3; i++)
        {
            if (food >= _unitCosts[i])
            {
                _spawnButtons[i].interactable = true;
            }
            else
            {
                _spawnButtons[i].interactable = false;
            }
        }
    }

}
