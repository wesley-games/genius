using Array = System.Array;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int Level = 1;
    public GameObject[] buttons;

    public Text LevelText;
    public GameObject LosePanel;
    
    private bool lose = false;
    private int sequenceSize = 3;
    private List<ButtonController> sequence;
    private List<ButtonController> playerSequence;

    void OnEnable()
    {
        ButtonController.OnButtonClicked += OnButtonClicked;
    }

    void OnDisable()
    {
        ButtonController.OnButtonClicked -= OnButtonClicked;
    }

    void Start()
    {
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop () 
    {
        while (!lose)
        {
            yield return new WaitForSeconds(2);

            // Generate random sequence
            Array.ForEach(buttons, b => b.GetComponent<ButtonController>().SetClickable(false));
            sequence = GenerateRandomSequence(sequenceSize);
            yield return StartCoroutine(AnimateButtons(sequence));
            DebugSequence(sequence); // TO REMOVE

            // Wait for player make his sequence
            playerSequence = new List<ButtonController>();
            Array.ForEach(buttons, b => b.GetComponent<ButtonController>().SetClickable(true));
            yield return StartCoroutine(WaitPlayerInput());

            // Evaluate player sequence
            if (EvaluatePlayerInput())
            {
                PlayerWinLevel();
            } else
            {
                lose = true;
            }
            yield return null;
        }
        PlayerLoseLevel();
        yield return null;
    }

    List<ButtonController> GenerateRandomSequence(int size)
    {
        List<ButtonController> sequence = new List<ButtonController>();
        for (int i = 0; i < size; i++)
        {
            sequence.Add(buttons[Random.Range(0, 5)].GetComponent<ButtonController>());
        }
        return sequence;
    }

    IEnumerator AnimateButtons(List<ButtonController> sequence) 
    {
        foreach (ButtonController buttonController in sequence)
        {
            yield return StartCoroutine(buttonController.Animate());
        }
    }

    IEnumerator WaitPlayerInput() 
    {
        while (playerSequence.Count < sequence.Count)
        {
             yield return null;
        }
    }

    bool EvaluatePlayerInput()
    {
        bool inputOk = true;
        for (int i = 0; i < sequenceSize; i++)
        {
            if (sequence[i] != playerSequence[i])
            {
                inputOk = false;
                break;
            }
        }
        return inputOk;
    }

    void OnButtonClicked(ButtonController buttonController)
    {
        playerSequence.Add(buttonController);
    }

    void PlayerWinLevel()
    {
        sequenceSize++;
        Level++;
        LevelText.text = "Level: " + Level;
    }

    void PlayerLoseLevel()
    {
        Array.ForEach(buttons, b => b.GetComponent<ButtonController>().SetClickable(false));
        LosePanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void DebugSequence(List<ButtonController> sequence)
    {
        string debug = "";
        foreach(ButtonController b in sequence)
        {
            debug += ", " + b.Type;
        }
        Debug.Log(debug);
    }
}
