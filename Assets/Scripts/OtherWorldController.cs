using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherWorldController : MonoBehaviour {
    public GameObject[] switches;
    public int[] solution;
    private int[] currentAnswer;
    private int currSwitchIdx;

    public void Awake () {
        currentAnswer = new int[solution.Length];
        ClearAnswerQueue ();

    }

    public void PushAnswerQueue (GameObject go) {
        int idx = System.Array.IndexOf (switches, go);
        Debug.Log("push "+idx);
        currentAnswer[currSwitchIdx] = idx;
        ++currSwitchIdx;
        if (currSwitchIdx == solution.Length)
            CheckStatus ();

    }

    public void CheckStatus () {
        Debug.Log(currentAnswer);
        bool flagSolve = true;
        for (int i = 0; i != solution.Length; ++i) {
            if (currentAnswer[i] != solution[i]) {
                flagSolve = false;
                break;
            }
        }
        if (flagSolve)
            PuzzleSolve ();
        else
            PuzzleUnsolve ();

    }

    public void ClearAnswerQueue () {
        currSwitchIdx = 0;
        for (int i = 0; i != solution.Length; ++i) {
            currentAnswer[i] = 0;

        }
    }

    public void PuzzleSolve () {
        Debug.Log ("solve!!!");
        for (int i = 0; i != switches.Length; ++i) {
            Animator atrSwitch = switches[i].GetComponentInChildren<Animator> ();
            atrSwitch.SetBool ("activated", true);
            atrSwitch.SetBool ("triggered", true);
        }
        GameManager.Instance.LeaveOtherWorld();

    }
    public void PuzzleUnsolve () {
        Debug.Log ("unsolve...");
        for (int i = 0; i != switches.Length; ++i) {
            Animator atrSwitch = switches[i].GetComponentInChildren<Animator> ();
            atrSwitch.SetBool ("activated", false);
        }
        ClearAnswerQueue ();
        
    }

}