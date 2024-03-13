using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public GameObject button6;
    public GameObject button7;
    public GameObject button8;

    public GameObject puzzle;

    public int code;

    private GameObject[] buttons;
    private bool isSolved;
    private string[] answer;

    public AudioSource yay;

    // Start is called before the first frame update
    void Start()
    {
        buttons = new GameObject[8]{button1, button2, button3, button4, button5, button6, button7, button8};
        answer = new string[8] {"unselected", "unselected", "unselected", "unselected", "unselected", "unselected", "unselected", "unselected"};
        answer[code] = "selected";
        isSolved = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool curAnswer = true;
        for (int i = 0; i < 8; i++) {
            curAnswer = curAnswer && buttons[i].CompareTag(answer[i]);
        }
        isSolved = curAnswer;

        if (isSolved) {
            puzzle.SetActive(false);
            yay.Play();
        }
    }
}
