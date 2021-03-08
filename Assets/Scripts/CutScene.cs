using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CutScene : MonoBehaviour {
    private TextMeshProUGUI textCutScene;
    private TextMeshProUGUI textCutSceneTitle;
    private bool cutSceneEnd;
    public float fadeSpeedTitle = 4f;
    public float fadeSpeedTitleToClear = 6f;
    public float fadeSpeed = 2f;
    public float fadeSpeedToClear = 6f;

    IEnumerator ChapterBeginCoro () {
        textCutSceneTitle.color = Color.clear;
        textCutScene.color = Color.clear;
        while (textCutSceneTitle.color.a <= 0.98f) {
            textCutSceneTitle.color = Color.Lerp (textCutSceneTitle.color, Color.white, fadeSpeedTitle * Time.deltaTime);
            yield return null;

        }
        textCutSceneTitle.color = Color.white;

        yield return new WaitForSeconds (3.0f);

        while (textCutSceneTitle.color.a > 0.02f) {
            textCutSceneTitle.color = Color.Lerp (textCutSceneTitle.color, Color.clear, fadeSpeedTitleToClear * Time.deltaTime);
            yield return null;

        }
        textCutSceneTitle.color = Color.clear;
        cutSceneEnd = true;
    }

    IEnumerator ChapterEndCoro () {
        textCutSceneTitle.color = Color.clear;
        textCutScene.color = Color.clear;
        while (textCutSceneTitle.color.a <= 0.98f) {
            textCutSceneTitle.color = Color.Lerp (textCutSceneTitle.color, Color.white, fadeSpeedTitle * Time.deltaTime);
            yield return null;

        }
        textCutSceneTitle.color = Color.white;
        yield return new WaitForSeconds (1.0f);
        while (textCutScene.color.a <= 0.98f) {
            textCutScene.color = Color.Lerp (textCutScene.color, Color.white, fadeSpeed * Time.deltaTime);
            yield return null;
        }
        textCutScene.color = Color.white;



        yield return new WaitForSeconds (4.0f);


        while (textCutSceneTitle.color.a > 0.02f) {
            textCutSceneTitle.color = Color.Lerp (textCutSceneTitle.color, Color.clear, fadeSpeedTitleToClear * Time.deltaTime);
            textCutScene.color = Color.Lerp (textCutScene.color, Color.clear, fadeSpeedToClear * Time.deltaTime);
            yield return null;

        }
        textCutSceneTitle.color = Color.clear;
        textCutScene.color = Color.clear;


        cutSceneEnd = true;

    }

    public void ChapterBegin () {

        cutSceneEnd = false;
        StartCoroutine (ChapterBeginCoro ());
    }

    public void ChapterEnd () {

        cutSceneEnd = false;
        StartCoroutine (ChapterEndCoro ());
    }
    public void Awake () {
        GameObject textCutSceneObj = transform.Find ("TextCutScene").gameObject;
        GameObject textCutSceneTitleObj = transform.Find ("TextCutSceneTitle").gameObject;
        textCutScene = textCutSceneObj.GetComponent<TextMeshProUGUI> ();
        textCutSceneTitle = textCutSceneTitleObj.GetComponent<TextMeshProUGUI> ();
        cutSceneEnd = false;

    }

    public void Start () {

    }

    void Update () {

    }

    public bool isCutSceneEnd () {
        return cutSceneEnd;
    }
}