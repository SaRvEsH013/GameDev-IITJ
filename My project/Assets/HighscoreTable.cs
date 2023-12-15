using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
//using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using TMPro;

public class HighscoreTable : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;
    private List<HighscoreEntry> highscoreEntrylList;
    private List<Transform> highscoreEntryTransformList;
    private void Awake()
    {
/*        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");*/
        entryTemplate.gameObject.SetActive(false);

        highscoreEntrylList = new List<HighscoreEntry>() {
            new HighscoreEntry{ score = 521854, name = "AAA" },
            new HighscoreEntry{ score = 358462, name = "ANN" },
            new HighscoreEntry{ score = 785123, name = "CAT" },
            new HighscoreEntry{ score = 15524, name = "JON" },
            new HighscoreEntry{ score = 897621, name = "JOE" },
            new HighscoreEntry{ score = 68245, name = "MIK" },
            new HighscoreEntry{ score = 872931, name = "DAV" },
            new HighscoreEntry{ score = 542024, name = "MAX" },
        };

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscoreEntrylList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);


        }
    }


    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformlist)
    {
        float templateHeight = 50f;
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

    private class HighscoreEntry
    {
        public int score;
        public string name;

    }
}