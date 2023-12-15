using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
//using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using TMPro;
using SimpleJSON;

public class HighscoreTable : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;
    private void Awake()
    {
        /*        entryContainer = transform.Find("highscoreEntryContainer");
                entryTemplate = entryContainer.Find("highscoreEntryTemplate");*/
        entryTemplate.gameObject.SetActive(false);

        /*highscoreEntrylList = new List<HighscoreEntry>() {
            new HighscoreEntry{ score = 521854, name = "AAA" },
            new HighscoreEntry{ score = 358462, name = "ANN" },
            new HighscoreEntry{ score = 785123, name = "CAT" },
            new HighscoreEntry{ score = 15524, name = "JON" },
            new HighscoreEntry{ score = 897621, name = "JOE" },
            new HighscoreEntry{ score = 68245, name = "MIK" },
            new HighscoreEntry{ score = 872931, name = "DAV" },
            new HighscoreEntry{ score = 542024, name = "MAX" },
        };*/

        //if highscoreTable does not exist, create it
        if (!PlayerPrefs.HasKey("highscoreTable"))
        {
            string json = "";
            PlayerPrefs.SetString("highscoreTable", json);
            PlayerPrefs.Save();
        }


        string JsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(JsonString);

        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score < highscores.highscoreEntryList[i].score)
                {
                    // Swap
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }
        int cnt = 0;

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
            cnt++;
            if(cnt >= 5)
            {
                break;
            }
        }

        /*Highscores highscores = new Highscores { highscoreEntryList = highscoreEntrylList };
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);*/


        /*PlayerPrefs.Save();

        Debug.Log(PlayerPrefs.GetString("highscoraTable"));*/
    }


    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformlist)
    {
        float templateHeight = 40f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(8, -templateHeight * transformlist.Count);
        entryTransform.gameObject.SetActive(true);
        int rank = transformlist.Count + 1;
        string rankstring;
        switch (rank)
        {
            default:
                rankstring = rank + "TH"; break;
            case 1: rankstring = "1ST"; break;
            case 2: rankstring = "2ND"; break;
            case 3: rankstring = "3RD"; break;
        }

        entryTransform.Find("posText").GetComponent<TMP_Text>().text = rankstring;
        int score = highscoreEntry.score;
        entryTransform.Find("scoreText").GetComponent<TMP_Text>().text = score.ToString();
        string name = highscoreEntry.name;
        entryTransform.Find("nameText").GetComponent<TMP_Text>().text = name;
        transformlist.Add(entryTransform);
    }

    public static void AddHighscoreEntry(int score, string name)
    {
        // Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };
        // Load saved Highscores
        string jsonstring = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonstring);
        // Add new entry to Highscores
        highscores.highscoreEntryList.Add(highscoreEntry);
        // save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;

    }
}