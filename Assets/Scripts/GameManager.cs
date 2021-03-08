using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    static GameManager instance;

    private Scene currScene;
    public Canvas mainMenuCanvas;
    public Canvas basicCanvas;
    public Canvas gameOverCanvas;
    public Canvas dialogCanvas;
    public Canvas pauseMenuCanvas;
    public Canvas cutSceneCanvas;
    public delegate void PlayerDeadHandler (LivingEntity entity);
    public event PlayerDeadHandler PlayerDeadEvent;
    private XmlManager xmlManager;
    private GameSaveData currGameData;
    private bool isPaused;

    static public GameManager Instance {
        get {
            if (instance == null) {
                instance = Object.FindObjectOfType (typeof (GameManager)) as GameManager;

                if (instance == null) {
                    GameObject go = new GameObject ("GameManager");
                    DontDestroyOnLoad (go);
                    instance = go.AddComponent<GameManager> ();
                }
            }
            return instance;
        }
    }

    public void Awake () {
        PlayerDeadEvent = GameOver;
        xmlManager = new XmlManager ();
        currGameData = new GameSaveData ();
        //SceneManager.sceneLoaded += OnSceneLoaded;
        isPaused = false;
        currScene = SceneManager.GetActiveScene ();
        InitSceneInfo (false);
    }

    public void InitSceneInfo (bool isOpening) {
        Debug.Log (currScene.name);
        switch (currScene.name) {
            case "MainMenu":
                GameObject mainMenuCanvasObj = GameObject.Find ("CanvasMainMenu");
                mainMenuCanvas = mainMenuCanvasObj.GetComponent<Canvas> ();
                mainMenuCanvas.enabled = true;
                break;
            case "Level_0":
                GameObject basicMenuCanvasObj = GameObject.Find ("CanvasBasic");
                basicCanvas = basicMenuCanvasObj.GetComponent<Canvas> ();
                basicCanvas.enabled = true;

                GameObject gameOverCanvasObj = GameObject.Find ("CanvasGameOver");
                gameOverCanvas = gameOverCanvasObj.GetComponent<Canvas> ();
                gameOverCanvas.enabled = false;

                GameObject dialogCanvasObj = GameObject.Find ("CanvasDialogue");
                dialogCanvas = dialogCanvasObj.GetComponent<Canvas> ();
                dialogCanvas.enabled = false;

                GameObject pauseMenuCanvasObj = GameObject.Find ("CanvasPauseMenu");
                pauseMenuCanvas = pauseMenuCanvasObj.GetComponent<Canvas> ();
                pauseMenuCanvas.enabled = false;

                GameObject cutSceneCanvasObj = GameObject.Find ("CanvasCutScene");
                cutSceneCanvas = cutSceneCanvasObj.GetComponent<Canvas> ();
                cutSceneCanvas.enabled = false;
                //GameObject fadeInOutCanvasObj = GameObject.Find("CanvasFadeInOut");
                //fadeInOutCanvas = fadeInOutCanvasObj.GetComponent<Canvas>();
                //fadeInOutCanvas.enabled = true;

/*
                GameObject guideball = GameObject.Find ("GuideBall");
                Guide gc = guideball.GetComponent<Guide> ();
                gc.enabled = false;
                */

                if (isOpening)
                    StartCoroutine (LevelZeroOpening ());

                break;

        }
    }

    public void Start () {

    }

    public void Update () {
        /*
        if(master.isDead())
            PlayerDeadEvent(master);
            */

    }

    public void FixedUpdate () {

    }

    public void NewGame () {
        StartCoroutine (LoadAsyncScene ("level_0", true));
    }

    IEnumerator LoadAsyncScene (string sceneName, bool isOpening) {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync (sceneName);

        while (!asyncLoad.isDone) {
            yield return null;
        }

        currScene = SceneManager.GetActiveScene ();
        InitSceneInfo (isOpening);
    }

    IEnumerator LevelZeroOpening () {
        dialogCanvas.enabled = true;
        basicCanvas.enabled = false;
        //SceneFadeInOut fade = dialogCanvas.GetComponentInChildren<SceneFadeInOut> ();
        GameObject BGDialogueObj = GameObject.Find ("CanvasDialogue/BGDialogue");
        SceneFadeInOut fade = BGDialogueObj.GetComponent<SceneFadeInOut> ();
        Dialogue dialog = dialogCanvas.GetComponent<Dialogue> ();
        //dialog.LoadDialogueFiles ("Dialogue Files New Game");
        dialog.LoadDialogueFiles ("DialogueFilesNewGame");
        fade.SetBlack ();
        //yield return new WaitForSeconds (1f);
        CutScene cutScene = cutSceneCanvas.GetComponentInChildren<CutScene> ();
        cutSceneCanvas.enabled = true;
        cutScene.ChapterBegin ();
        while (!cutScene.isCutSceneEnd ()) {
            yield return null;
        }
        yield return new WaitForSeconds (2f);
        cutSceneCanvas.enabled = false;
        //fade.BeginFadeToBlack ();

        dialog.BeginDialogue ();
        while (!dialog.isDialogueEnd ()) {
            yield return null;
        }
        dialog.HideDialogue ();
        yield return new WaitForSeconds (1.0f);

        fade.BeginFadeToClearFromBlack ();
        yield return new WaitForSeconds (1.5f);
        //fadeInOutCanvas.enabled = false;
        dialogCanvas.enabled = false;
        basicCanvas.enabled = true;
    }

    public void PickUpBear () {

        StartCoroutine (PickUpBearCoro ());
    }

    public void EnterOtherWorld () {
        StartCoroutine (OtherWorldLoadingCoro ());
    }

    public void LeaveOtherWorld () {
        StartCoroutine (OtherWorldEndingCoro ());

    }

    public void LevelComplete() {
        StartCoroutine (LevelCompleteCoro ());

    }

    IEnumerator PickUpBearCoro () {
        GameObject followingCameraGuideObj = GameObject.Find ("CM vcam1");
        GameObject followingCameraMasterObj = GameObject.Find ("CM vcam2");
        GameObject BGDialogueSObj = GameObject.Find ("CanvasDialogue/BGDialogue_small");
        //GameObject BGDialogueObj = GameObject.Find ("CanvasDialogue/BGDialogue");
        Cinemachine.CinemachineVirtualCamera followingCameraMaster = followingCameraMasterObj.GetComponent<Cinemachine.CinemachineVirtualCamera> ();
        Cinemachine.CinemachineVirtualCamera followingCameraGuide = followingCameraGuideObj.GetComponent<Cinemachine.CinemachineVirtualCamera> ();
        GameObject bearObj = GameObject.Find ("bear");
        GameObject masterObj = GameObject.Find ("master");
        Animator atorBear = bearObj.GetComponent<Animator>();
        followingCameraMaster.Follow = bearObj.transform;
        followingCameraGuide.enabled = false;
        followingCameraMaster.enabled = true;
        dialogCanvas.enabled = true;
        basicCanvas.enabled = false;
        yield return new WaitForSeconds (2f);
        SceneFadeInOut fade = BGDialogueSObj.GetComponent<SceneFadeInOut> ();
        Dialogue dialog = dialogCanvas.GetComponent<Dialogue> ();
        //dialog.LoadDialogueFiles ("Assets/Dialogue Files Pick Up Bear.asset");
        dialog.LoadDialogueFiles ("DialogueFilesPickUpBear");

        fade.BeginFadeToWhite();
        yield return new WaitForSeconds (1.0f);

        dialog.BeginDialogue ();
        while (!dialog.isDialogueEnd ()) {
            yield return null;
        }
        dialog.HideDialogue ();
        yield return new WaitForSeconds (1.0f);

        followingCameraMaster.enabled = false;
        followingCameraGuide.enabled = true;

        fade.BeginFadeToClearFromWhite ();
        
        GameObject triggerAreaObj = GameObject.Find("trigger area 1");
        triggerAreaObj.SetActive(false);

        yield return new WaitForSeconds (1.0f);

        atorBear.SetBool("fade", true);

        yield return new WaitForSeconds (1.0f);

        dialogCanvas.enabled = false;
        basicCanvas.enabled = true;

        followingCameraMaster.Follow = masterObj.transform;
        yield return new WaitForSeconds (1.0f);
        bearObj.SetActive(false);
        GameObject bear2Obj = GameObject.Find ("bear_2");
        Animator atorBear2 = bear2Obj.GetComponent<Animator>();
        atorBear2.SetBool("fade", true);
    }

    IEnumerator OtherWorldLoadingCoro () {
        GameObject followingCameraGuideObj = GameObject.Find ("CM vcam1");
        GameObject followingCameraMasterObj = GameObject.Find ("CM vcam2");
        GameObject masterCheckpoint = GameObject.Find ("MasterCheckpoint");
        GameObject switchGateObj = GameObject.Find ("switch_gate");
        GameObject GateObj = GameObject.Find ("barrier");
        Animator atorSwitchGate = switchGateObj.GetComponent<Animator> ();
        Animator atorGate = GateObj.GetComponent<Animator> ();
        GameObject master = GameObject.Find ("master");
        Cinemachine.CinemachineVirtualCamera followingCameraMaster = followingCameraMasterObj.GetComponent<Cinemachine.CinemachineVirtualCamera> ();
        Cinemachine.CinemachineVirtualCamera followingCameraGuide = followingCameraGuideObj.GetComponent<Cinemachine.CinemachineVirtualCamera> ();
        //followingCameraMaster.Follow = switchGateObj.transform;
        followingCameraGuide.enabled = false;
        followingCameraMaster.enabled = true;
        dialogCanvas.enabled = true;
        basicCanvas.enabled = false;
        GameObject BGDialogueObj = GameObject.Find ("CanvasDialogue/BGDialogue");

        GameObject guideButtonObj = GameObject.Find ("CanvasBasic/Guide Button Background/Guide Button");
        GuideButtonController gbc = guideButtonObj.GetComponent<GuideButtonController>();
        Image ib = guideButtonObj.GetComponent<Image>();
        Animator atorButton = guideButtonObj.GetComponent<Animator>();
        gbc.enabled = false;
        ib.enabled = false;
        atorButton.ResetTrigger("Pressed");
        atorButton.SetTrigger("Normal");

        SceneFadeInOut fade = BGDialogueObj.GetComponent<SceneFadeInOut> ();
        Dialogue dialog = dialogCanvas.GetComponent<Dialogue> ();
        dialog.LoadDialogueFiles ("DialogueFilesOtherWorldLoading");
        //followingCamera.LookAt = master.transform;
        atorSwitchGate.SetBool("activated", true);
        yield return new WaitForSeconds (2f);
        followingCameraGuide.Follow = GateObj.transform;
        yield return new WaitForSeconds (1f);
        followingCameraMaster.enabled = false;
        followingCameraGuide.enabled = true;
        yield return new WaitForSeconds (1.5f);
        atorSwitchGate.SetBool("triggered", true);
        atorGate.SetBool("opened", true);

        Collider2D colliderGate = GateObj.GetComponent<Collider2D> ();
        colliderGate.enabled = false;

        yield return new WaitForSeconds (2f);
        followingCameraMaster.enabled = true;
        followingCameraGuide.enabled = false;
        //followingCameraGuide.Follow = guideBall.transform;

        yield return new WaitForSeconds (2.5f);
        //fade.SetBlack ();
        fade.BeginFadeToBlack ();
        yield return new WaitForSeconds (1f);

        GameObject OtherWorldManager = GameObject.Find ("OtherWorldManager");
        //GameObject guideBall = GameObject.Find ("GuideBall");
        //Guide gcb = guideBall.GetComponent<Guide> ();
        //gcb.enabled = true;

        GameObject guide = GameObject.Find ("guide");
        Guide gc = guide.GetComponent<Guide> ();
        gc.enabled = false;

        Guide gcm = master.GetComponent<Guide> ();
        gcm.enabled = true;

        Master mc = master.GetComponent<Master> ();
        mc.enabled = false;

        Animator ma = master.GetComponent<Animator> ();

        gcm.guideMovementSpeed = gcm.guideMovementSpeed * 0.7f;
        ma.speed = ma.speed * 0.7f;
        //mc.minDistBetweenGuideAMaster = 0.5f;
        //mc.masterMovementSpeed = 1f;
        //ma.speed = 0.5f;
        SpriteRenderer rd = master.GetComponent<SpriteRenderer> ();
        rd.color = new Color (0.4f, 0.4f, 0.4f);
        //mc.guide = guideBall;

        master.transform.position = new Vector2 (masterCheckpoint.transform.position.x, masterCheckpoint.transform.position.y);

/*
        followingCameraGuide.enabled = true;
        followingCameraMaster.enabled = false;
        followingCameraGuide.Follow = guideBall.transform;
        */

        dialog.BeginDialogue ();
        while (!dialog.isDialogueEnd ()) {
            yield return null;
        }
        dialog.HideDialogue ();
        yield return new WaitForSeconds (1.0f);

        fade.BeginFadeToClearFromBlack ();
        yield return new WaitForSeconds (1.5f);
        //fadeInOutCanvas.enabled = false;

        dialogCanvas.enabled = false;
        basicCanvas.enabled = true;

        yield return new WaitForSeconds (1.5f);
        OWController owc = OtherWorldManager.GetComponent<OWController>();
        owc.BeginGuideProcess();


    }

    IEnumerator OtherWorldEndingCoro () {
        GameObject followingCameraGuideObj = GameObject.Find ("CM vcam1");
        GameObject followingCameraMasterObj = GameObject.Find ("CM vcam2");
        GameObject master = GameObject.Find ("master");
        Cinemachine.CinemachineVirtualCamera followingCameraMaster = followingCameraMasterObj.GetComponent<Cinemachine.CinemachineVirtualCamera> ();
        Cinemachine.CinemachineVirtualCamera followingCameraGuide = followingCameraGuideObj.GetComponent<Cinemachine.CinemachineVirtualCamera> ();
        dialogCanvas.enabled = true;
        basicCanvas.enabled = false;

        GameObject guideButtonObj = GameObject.Find("CanvasBasic/Guide Button Background/Guide Button");
        GuideButtonController gbc = guideButtonObj.GetComponent<GuideButtonController>();
        Image ib = guideButtonObj.GetComponent<Image>();
        gbc.enabled = true;
        ib.enabled = true;


        GameObject BGDialogueObj = GameObject.Find ("CanvasDialogue/BGDialogue");
        SceneFadeInOut fade = BGDialogueObj.GetComponent<SceneFadeInOut> ();
        Dialogue dialog = dialogCanvas.GetComponent<Dialogue> ();
        dialog.LoadDialogueFiles ("DialogueFilesOtherWorldEnding");

        yield return new WaitForSeconds (3f);
        fade.BeginFadeToBlack ();
        yield return new WaitForSeconds (1f);

        Guide gcm = master.GetComponent<Guide> ();
        gcm.guideMovementSpeed = gcm.guideMovementSpeed / 0.7f;
        gcm.enabled = false;

        Master mc = master.GetComponent<Master> ();
        mc.enabled = true;

        //GameObject guideBall = GameObject.Find("GuideBall");
        //Guide gcb = guideBall.GetComponent<Guide>();
        //gcb.enabled = false;

        GameObject guide = GameObject.Find("guide");
        Guide gc = guide.GetComponent<Guide>();
        gc.enabled = true;

        Animator ma = master.GetComponent<Animator>();
        //mc.minDistBetweenGuideAMaster = mc.minDistBetweenGuideAMaster * 3f;
        ma.speed = ma.speed / 0.7f;
        SpriteRenderer rd = master.GetComponent<SpriteRenderer>();
        rd.color = new Color(1f, 1f, 1f);
        //mc.guide = guide;

        GameObject masterCheckpoint = GameObject.Find ("MasterCheckpoint_2");
        GameObject guideCheckpoint = GameObject.Find ("GuideCheckpoint");
        master.transform.position = new Vector2 (masterCheckpoint.transform.position.x, masterCheckpoint.transform.position.y);
        guide.transform.position = new Vector2 (guideCheckpoint.transform.position.x, guideCheckpoint.transform.position.y);

        followingCameraGuide.enabled = true;
        followingCameraMaster.enabled = false;
        followingCameraGuide.Follow = guide.transform;

        dialog.BeginDialogue ();
        while (!dialog.isDialogueEnd ()) {
            yield return null;
        }
        dialog.HideDialogue ();
        yield return new WaitForSeconds (1.0f);

        fade.BeginFadeToClearFromBlack ();
        yield return new WaitForSeconds (1.5f);
/*
        CutScene cutScene = cutSceneCanvas.GetComponentInChildren<CutScene> ();
        cutSceneCanvas.enabled = true;
        cutScene.ChapterEnd ();
        while (!cutScene.isCutSceneEnd ()) {
            yield return null;
        }
        cutSceneCanvas.enabled = false;
        yield return new WaitForSeconds (2.0f);
        GameManager.Instance.ExitToMainMenu ();
        //yield return new WaitForSeconds (1.5f);
        //fadeInOutCanvas.enabled = false;
        //dialogCanvas.enabled = false;
        //basicCanvas.enabled = true;;
        */

        GameObject bear2Obj = GameObject.Find("bear_2");
        Animator atorBear2 = bear2Obj.GetComponent<Animator>();
        atorBear2.SetBool("fade", false);

        GameObject triggerAreaObj = GameObject.Find("trigger area 2");
        triggerAreaObj.SetActive(false);

        yield return new WaitForSeconds (1.5f);
        dialogCanvas.enabled = false;
        basicCanvas.enabled = true;
    }

    IEnumerator LevelCompleteCoro () {
        dialogCanvas.enabled = true;
        basicCanvas.enabled = false;
        GameObject BGDialogueObj = GameObject.Find ("CanvasDialogue/BGDialogue");
        SceneFadeInOut fade = BGDialogueObj.GetComponent<SceneFadeInOut> ();
        //Dialogue dialog = dialogCanvas.GetComponent<Dialogue> ();
        //dialog.LoadDialogueFiles ("DialogueFilesLevelComplete");

        yield return new WaitForSeconds (1.5f);
        fade.BeginFadeToBlack ();
        yield return new WaitForSeconds (1f);

/*
        dialog.BeginDialogue ();
        while (!dialog.isDialogueEnd ()) {
            yield return null;
        }
        dialog.HideDialogue ();
        yield return new WaitForSeconds (1.0f);
        */

        //fade.BeginFadeToClearFromBlack ();
        //yield return new WaitForSeconds (1.5f);

        CutScene cutScene = cutSceneCanvas.GetComponentInChildren<CutScene> ();
        cutSceneCanvas.enabled = true;
        cutScene.ChapterEnd ();
        while (!cutScene.isCutSceneEnd ()) {
            yield return null;
        }
        cutSceneCanvas.enabled = false;
        yield return new WaitForSeconds (2.0f);
        GameManager.Instance.ExitToMainMenu ();
        //yield return new WaitForSeconds (1.5f);
        //fadeInOutCanvas.enabled = false;
        //dialogCanvas.enabled = false;
        //basicCanvas.enabled = true;;
    }


    public void ExitGame () {

        Application.Quit ();

    }
    public void GameOver (LivingEntity entity) {

        basicCanvas.enabled = false;
        dialogCanvas.enabled = false;
        pauseMenuCanvas.enabled = false;
        gameOverCanvas.enabled = true;
        Time.timeScale = 0;

    }
    public void PauseGame () {

        if (!isPaused) {
            basicCanvas.enabled = false;
            pauseMenuCanvas.enabled = true;
            Time.timeScale = 0;
            isPaused = true;

        }

    }

    public void ResumeGame () {

        if (isPaused) {
            pauseMenuCanvas.enabled = false;
            basicCanvas.enabled = true;
            Time.timeScale = 1;
            isPaused = false;

        }

    }

    private void CollectGameData () {
        GameObject master = GameObject.Find ("master");
        GameObject guide = GameObject.Find ("guide");
        currGameData.masterPos = master.transform.position;
        currGameData.guidePos = guide.transform.position;
        currGameData.sceneName = currScene.name;
        Debug.Log ("data collect completed.");

    }

    IEnumerator ApplyGameDataProcess () {
        string sceneName = currGameData.sceneName;

        if (sceneName == "MainMenu") {
            ExitToMainMenu ();
        } else {
            ResumeGame ();

            if (currScene.name != sceneName) {
                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync (sceneName);
                while (!asyncLoad.isDone) {
                    yield return null;
                }
                currScene = SceneManager.GetActiveScene ();
                InitSceneInfo (false);
            }
            GameObject master = GameObject.Find ("master");
            GameObject guide = GameObject.Find ("guide");
            master.transform.position = currGameData.masterPos;
            guide.transform.position = currGameData.guidePos;

        }
    }

    private void ApplyGameData () {
        StartCoroutine (ApplyGameDataProcess ());
    }
    public void SaveGame () {
        CollectGameData ();
        xmlManager.Save ("save001", currGameData);
        Debug.Log ("Current game data saved successfully.");
    }

    public void LoadGame () {
        xmlManager.Load ("save001", ref currGameData);
        ApplyGameData ();
        Debug.Log ("Current game data loaded successfully.");

    }
    public void ExitToMainMenu () {
        ResumeGame ();
        StartCoroutine (LoadAsyncScene ("MainMenu", false));
        //SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

}