using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour {
    private static string CHAR_NAME_DELIMITER = "::";
    private static string VARIABLE_OPENER = "{{";
    private static string VARIABLE_CLOSER = "}}";

    private Queue<string> linesQueue;

	void Start () {
        linesQueue = new Queue<string>();
	}

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting conversation");
        linesQueue.Clear();

        foreach(string line in dialogue.pureText)
        {
            // TODO: process line
            linesQueue.Enqueue(line);
        }

        DisplayNextLine();
    }

    public void DisplayNextLine() {
        if(linesQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        string line = linesQueue.Dequeue();
        Debug.Log(line.ToString());
    }

    public void EndDialogue()
    {
        Debug.Log("End of conversation");
    }
}
