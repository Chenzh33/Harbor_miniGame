using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {
    //public Canvas dialogCanvas;
    //public Canvas basicCanvas;
    private TextMeshProUGUI textDisplay;
    private TextMeshProUGUI textButton;
    private GameObject continueButton;
    private int index;
    private bool inDialogue;
    private bool sentenceDisplayed;

    private string[] sentences;
    private string[] heads;
    private string[] buttonTexts;
    public float typingSpeed;

    public DialogueFiles dialogueFiles;

    IEnumerator TypeLetters () {
        sentenceDisplayed = false;
        //string[] words = sentences[index].Split(" "[0]);
        char[] words = sentences[index].ToCharArray ();
        yield return new WaitForSeconds (0.5f);
        for (int i = 0; i != words.Length; ++i) {
            textDisplay.text += words[i];
            //yield return new WaitForSeconds(typingSpeed * words[i].Length);
            yield return new WaitForSeconds (typingSpeed);
        }
        yield return new WaitForSeconds (0.2f);
        sentenceDisplayed = true;
        /*
        foreach(char letter in sentences[index].ToCharArray()){
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);

        }
        */

    }
    public void LoadDialogueFiles (string name) {
        index = 0;
        //dialogueFiles = AssetDatabase.LoadAssetAtPath<DialogueFiles> (name);
        //string filename = "Dialogues/"+ name + ".txt";
        string filename = "Dialogues/"+ name;
        Debug.Log(filename);
        //TextAsset dialogueTextFiles = Resources.Load(filename, typeof(TextAsset)) as TextAsset;
        var dialogueTextFiles = Resources.Load<TextAsset>(filename);
        Debug.Log(dialogueTextFiles);
        //string[] allText = dialogueTextFiles.text.Split("\r"[0]);
        string[] allText = dialogueTextFiles.text.Split('\n');
        heads = new string[allText.Length];
        sentences = new string[allText.Length];
        buttonTexts = new string[allText.Length];
        for(int i = 0;i!=allText.Length;++i)
        {
            string[] elements = allText[i].Split('\t');
            heads[i] = elements[0];
            sentences[i] = elements[1];
            buttonTexts[i] = elements[2];
            Debug.Log(heads[i]);
            Debug.Log(sentences[i]);
            Debug.Log(buttonTexts[i]);

        }
        /*
        sentences = dialogueFiles.sentences;
        heads = dialogueFiles.heads;
        buttonTexts = dialogueFiles.buttonTexts;
        */
    }

    public void BeginDialogue () {
        //basicCanvas.enabled = false;
        inDialogue = true;
        //dialogCanvas.enabled = true;
        textDisplay.text = heads[0];
        textDisplay.text += "\n";
        textButton.text = buttonTexts[0];
        StartCoroutine (TypeLetters ());
    }

    public void EndDialogue () {
        inDialogue = false;
        //dialogCanvas.enabled = false;
        //basicCanvas.enabled = true;
    }

    public bool isDialogueEnd () {
        return !inDialogue;
    }

    public void NextSentence () {

        continueButton.SetActive (false);
        if (index < sentences.Length - 1) {
            ++index;
            textDisplay.text = heads[index];
            textDisplay.text += "\n";
            textButton.text = buttonTexts[index];
            if (index == sentences.Length - 1) {
                //textButton.text = "开始旅行";
                float scaleFactor = 1.7f;
                continueButton.transform.localScale = new Vector3 (scaleFactor, scaleFactor, scaleFactor);
            }
            StartCoroutine (TypeLetters ());
        } else {
            textDisplay.text = "";
            EndDialogue ();
            continueButton.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
        }

    }

    public void Awake () {
        inDialogue = false;
        //continueButton = gameObject.Find("CanvasDialogue/ButtonContinue");
        continueButton = transform.Find ("ButtonContinue").gameObject;
        GameObject textDialogue = transform.Find ("TextDialogue").gameObject;
        //GameObject textDialogue = GameObject.Find("CanvasDialogue/TextDialogue");
        textDisplay = textDialogue.GetComponent<TextMeshProUGUI> ();
        textButton = continueButton.GetComponentInChildren<TextMeshProUGUI> ();

    }

    public void Start () {
        textDisplay.text = "";
        continueButton.SetActive (false);
        //if(BeginDialogueAfterGameStart == true)
        //BeginDialogue();

    }

    public void HideDialogue () {
        //textDisplay.SetActive(false);
        continueButton.SetActive (false);

    }

    void Update () {
        if (inDialogue && sentenceDisplayed) {
            continueButton.SetActive (true);

        }

    }

}