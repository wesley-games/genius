using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int Level = 1;
    public GameObject[] buttons;
    
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
            sequence = GenerateRandomSequence(sequenceSize);
            yield return StartCoroutine(AnimateButtons(sequence));
            playerSequence = new List<ButtonController>();
            yield return StartCoroutine(WaitPlayerInput());

            if (EvaluatePlayerInput())
            {
                yield return new WaitForSeconds(2);
                Debug.Log("Ganhou !");
                Level++;
                sequenceSize++;
            } else
            {
                lose = true;
            }
            yield return null;
        }
        Debug.Log("Perdeu !");
        yield return null;
    }

    List<ButtonController> GenerateRandomSequence(int size)
    {
        List<ButtonController> sequence = new List<ButtonController>();
        for (int i = 0; i < size; i++)
        {
            sequence.Add(buttons[Random.Range(0, 4)].GetComponent<ButtonController>());
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
}
