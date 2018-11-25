using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Dialogue {
    public TextAsset rawDialogue;

    public string[] pureText;

    private static string START_TAG = "[start]";
    private static string END_TAG = "[end]";

    public void StripStartEndTags()
    {
        Debug.Log(rawDialogue.text);
        int startIndex = rawDialogue.text.IndexOf(START_TAG) + START_TAG.Length;
        if(startIndex < 0)
        {
            throw new System.Exception("Error: Expected " + rawDialogue.name + " to contain a '[start]' tag.");
        }
        // the first index we DON'T need
        int endIndex = rawDialogue.text.IndexOf(END_TAG);
        if (endIndex < 0)
        {
            throw new System.Exception("Error: Expected " + rawDialogue.name + " to contain an '[end]' tag.");
        }

        string dialogueMeat = rawDialogue.text.Substring(startIndex, endIndex - startIndex);

        Debug.Log("meat: " + dialogueMeat);

        pureText = dialogueMeat.Split(
            new[] { "\r\n", "\r", "\n" },
            System.StringSplitOptions.None)
            .Where((line) => {
                return !string.IsNullOrEmpty(line.Trim());
            })
            .ToArray();
    }
}
