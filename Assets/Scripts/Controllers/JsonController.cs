using LitJson;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JsonController : MonoBehaviour
{
	[Header("Views")]
	public GameObject _mainSceneCanvas;
	public GameObject _gameObjectsMainScene;
	public GameObject _loadingSceneCanvas;
	private string jsonString;
	public static JsonData jsonDataStages;
	public static JsonData jsonDataQuestions;
	public string _stageUrl;
	public string _questionsUrl;

    Dictionary<string, string> headers = new Dictionary<string, string>();


#if UNITY_STANDALONE
	void Start()
	{
	 	    jsonDataStages = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Resources/GameData.txt").Trim());
	 	    jsonDataQuestions = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Resources/QuestionsData.txt").Trim());

		    loadGlobalVariablesFromJson();
	 	    this._mainSceneCanvas.SetActive(true);
	 	    this._gameObjectsMainScene.SetActive(true);
	 	    this.GetComponent<AnimationController>().playAnimations(GameState.States.MAINSCENE);
	 	    this._loadingSceneCanvas.SetActive(false);       
	}
#endif

    /*
#if UNITY_WEBGL || UNITY_EDITOR

    private void Awake()
    {

        print("Absolute: " + Application.absoluteURL);
        print("Persistent: " + Application.persistentDataPath);
        print("datapath: " + Application.dataPath);

        print("streamin: " + Application.streamingAssetsPath);
    }

    
    private void Start()
    {
        jsonDataStages = JsonMapper.ToObject(File.ReadAllText(Application.absoluteURL+ "GameData.txt").Trim().Substring(1));
        jsonDataQuestions = JsonMapper.ToObject(File.ReadAllText(Application.absoluteURL+ "QuestionsData.txt").Trim().Substring(1));

        loadGlobalVariablesFromJson();
        this._mainSceneCanvas.SetActive(true);
        this._gameObjectsMainScene.SetActive(true);
        this.GetComponent<AnimationController>().playAnimations(GameState.States.MAINSCENE);
        this._loadingSceneCanvas.SetActive(false);
    }
#endif*/




    private void loadGlobalVariablesFromJson()
    {
        GlobalVariables._totallyStages = jsonDataStages["Stages"].Count;
    }
}
