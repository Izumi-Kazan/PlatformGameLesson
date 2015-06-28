using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class MyWebView : MonoBehaviour {
    // インスペクタでアタッチしたUI Textを指定
    private Text htmlField;

    //
    public string htmlFile;

    //
    private string filePath;

	// Use this for initialization
	void Start () {
        if (htmlFile == null) return;

        filePath = Path.Combine(Application.persistentDataPath, htmlFile);
	}

    /**<summary>
    
    </summary>*/
    void PrepareHTML ()
    {  
        // ガーベッジコレクションを強制的に発動させるために
        using (var writer = new StreamWriter( htmlFile, false))
        {

        }
    }
    
	
	// Update is called once per frame
	void Update () {
	
	}
}
