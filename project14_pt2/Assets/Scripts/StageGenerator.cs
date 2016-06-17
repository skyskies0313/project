using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class StageGenerator : MonoBehaviour {
    const int StageTipSize = 240;

    int currentTipIndex;

    public Transform character;
    public GameObject stageTips;
    public int startTipIndex;
    public int preInstantiate;
    public List<GameObject> generatedStageList = new List<GameObject>();
    public static string label2;
	void Start () {
        currentTipIndex = startTipIndex - 1;
        UpdateStage(preInstantiate);
	}
	
	// Update is called once per frame
	void Update () {
        int charaPositionIndex;
        if (character.position.z <= 0)
        {
            charaPositionIndex = (int)(character.position.z / StageTipSize - 1);
        }
        else {
            charaPositionIndex = (int)(character.position.z / StageTipSize);
        }

        if (Input.GetKeyDown("g"))
        {
            Debug.Log(charaPositionIndex + preInstantiate);
            Debug.Log(currentTipIndex);
        }
        if(charaPositionIndex + preInstantiate != currentTipIndex)
        {
            UpdateStage(charaPositionIndex + preInstantiate);
        }
	}

    void UpdateStage(int toTipIndex)
    {
        label2 = toTipIndex.ToString();
        if (toTipIndex == currentTipIndex) return;   
        for(int i = currentTipIndex + 1; i <= toTipIndex; i++)
        {
            GameObject frontStageObject = GenerateFrontStage(i);
            GameObject backStageObject = GenerateBackStage(i);

            generatedStageList.Add(frontStageObject);
            generatedStageList.Add(backStageObject);
        }

        while (generatedStageList.Count > 2*preInstantiate + 2) DestroyOldestStage();

        currentTipIndex = toTipIndex;
    }

    GameObject GenerateFrontStage(int tipIndex)
    {
        GameObject stageObject = (GameObject)Instantiate(
            stageTips,
            new Vector3(0, 0, tipIndex * StageTipSize),
            Quaternion.identity
            );
        return stageObject;
    }

    GameObject GenerateBackStage(int tipIndex)
    {
        GameObject stageObject = (GameObject)Instantiate(
            stageTips,
            new Vector3(0, 0, tipIndex * StageTipSize -240*2),
            Quaternion.identity
            );
        return stageObject;
    }

    void DestroyOldestStage()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject oldStage = generatedStageList[i];
            //  GameObject secondNewStage = generatedStageList[generatedStageList.Count - 1];
            generatedStageList.RemoveAt(i);
           // generatedStageList.RemoveAt(generatedStageList.Count);
            Destroy(oldStage);
            //Destroy(secondNewStage);
        }
    }

    void OnGUI()
    {
        string label = currentTipIndex.ToString();
        string labell = label2;
        GUI.Label(new Rect(0, 0, 100, 30), label);
        GUI.Label(new Rect(0, 50, 100, 30), labell);
    }
}