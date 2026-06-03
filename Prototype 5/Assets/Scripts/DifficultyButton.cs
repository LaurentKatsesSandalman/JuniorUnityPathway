using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button button;
    public int difficulty;
    private GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
        gameManager = FindAnyObjectByType<GameManager>();
        button.onClick.AddListener(SetDifficulty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetDifficulty()
    {
        if(button.gameObject.name == "Easy")
        {
            difficulty = 1;
        }
        else if (button.gameObject.name == "Medium")
        {
            difficulty = 2;
        }
        else if (button.gameObject.name == "Hard")
        {
            difficulty = 3;
        }
        gameManager.StartGame(difficulty);
    }
}
