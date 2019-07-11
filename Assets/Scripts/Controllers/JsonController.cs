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

	private WWW _wwwStageUrl;
	private WWW _wwwQuestionsUrl;

    Dictionary<string, string> headers = new Dictionary<string, string>();


#if UNITY_STANDALONE
	void Start()
	{
	 	    jsonDataStages = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/GameData.txt").Trim());
	 	    jsonDataQuestions = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/QuestionsData.txt").Trim());

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





#if UNITY_WEBGL || UNITY_EDITOR

    private void Awake()
    {
        /*
        headers.Add("Access-Control-Allow-Credentials", "true");
        headers.Add("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time");
        headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
        headers.Add("Access-Control-Allow-Origin", "*");

        //Debug
        Debug.Log("AbsoluteURL: " + Application.absoluteURL);
        Debug.Log("Persistent DatapPath: " + Application.persistentDataPath);
        Debug.Log("DatapPath: " + Application.dataPath);*/

    }

    
    IEnumerator Start()
	{
        print("Local: " + Application.persistentDataPath);
        _wwwStageUrl = new WWW(Application.dataPath + "/GameData.txt");
		_wwwQuestionsUrl = new WWW(Application.dataPath + "/QuestionsData.txt");

        yield return _wwwStageUrl;
		yield return _wwwQuestionsUrl;

		if (_wwwStageUrl.error == null && _wwwQuestionsUrl.error == null)
		{
			print(_wwwStageUrl.text);
	
			jsonDataStages = JsonMapper.ToObject(_wwwStageUrl.text.Trim());
			jsonDataQuestions = JsonMapper.ToObject(_wwwQuestionsUrl.text.Trim());

            print(jsonDataStages);

			loadGlobalVariablesFromJson();
			yield return new WaitForSeconds(1f);
			this._mainSceneCanvas.SetActive(true);
			this._gameObjectsMainScene.SetActive(true);
			this.GetComponent<AnimationController>().playAnimations(GameState.States.MAINSCENE);
			this._loadingSceneCanvas.SetActive(false);
		}

		else
		{
			Debug.Log("[Develop Message] ERROR: " +  _wwwStageUrl.error);
		}        
	}

        /*
    private void Start()
    {
        Debug.Log("Lugar a buscar: " + Application.dataPath + "/GameData.txt");

        jsonDataStages = JsonMapper.ToObject(File.ReadAllText(Application.dataPath+ "/GameData.txt"));
        jsonDataQuestions = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/QuestionsData.txt"));

        loadGlobalVariablesFromJson();
        this._mainSceneCanvas.SetActive(true);
        this._gameObjectsMainScene.SetActive(true);
        this.GetComponent<AnimationController>().playAnimations(GameState.States.MAINSCENE);
        this._loadingSceneCanvas.SetActive(false);
    }*/
#endif

    private void loadGlobalVariablesFromJson()
    {
        GlobalVariables._totallyStages = jsonDataStages["Stages"].Count;
    }
}
