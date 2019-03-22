using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int Level = 1;
    public GameObject[] buttons;
    
    private bool lose = false;
    private int sequenceSize = 3;
    private GameObject[] sequence;

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
        // while (!lose)
        // {
            sequence = GenerateRandomSequence(sequenceSize);
            yield return StartCoroutine(AnimateButtons(sequence));
            yield return new WaitForSeconds(2);
        // }
        yield return null;
    }

    GameObject[] GenerateRandomSequence(int size)
    {
        GameObject[] sequence = new GameObject[size];
        for (int i = 0; i < size; i++)
        {
            sequence[i] = buttons[Random.Range(0, 4)];
        }
        return sequence;
    }

    IEnumerator AnimateButtons(GameObject[] sequence) {
        foreach (GameObject button in sequence)
        {
            Debug.Log(button.GetComponent<ButtonController>().type);
            button.GetComponent<ButtonController>().Inflate();
            yield return new WaitForSeconds(0.5f);
            button.GetComponent<ButtonController>().Deflate();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void OnButtonClicked(ButtonController button)
    {
        Debug.Log(button.GetComponent<ButtonController>().type);
    }
}
