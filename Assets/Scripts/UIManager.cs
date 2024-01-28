using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    [SerializeField]
    private TextMeshProUGUI _foodText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateScore(float food)
    {

        _foodText.text = "Food: " + food.ToString("F1"); 
    }
}
