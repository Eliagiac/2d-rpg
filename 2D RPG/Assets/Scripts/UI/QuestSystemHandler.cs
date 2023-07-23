using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSystemHandler : MonoBehaviour
{
    public GameObject questTree;

    public float zoomSpeed = 50f;

    public Button zoomInButton;
    public Button zoomOutButton;

    float questTreePosX;

    public int multiplier = 200;

    public Animator questAnimator;

    public int questsAmm = 1;

    bool[] completedQuests;

    private void Start()
    {
        completedQuests = new bool[questsAmm];
    }

    void Update()
    {
        #region Stop Zoom
        if (questTree.transform.localScale.x >= 2)
        {
            zoomInButton.gameObject.SetActive(false);
        }

        else
        {
            zoomInButton.gameObject.SetActive(true);
        }

        if (questTree.transform.localScale.x <= 0.4f)
        {
            zoomOutButton.gameObject.SetActive(false);
        }

        else
        {
            zoomOutButton.gameObject.SetActive(true);
        }
        #endregion

        questTreePosX = questTree.GetComponent<RectTransform>().anchoredPosition.x;

        Scroll();

        questTree.GetComponent<RectTransform>().anchoredPosition = new Vector2(questTreePosX, questTree.GetComponent<RectTransform>().anchoredPosition.y);

        if (Input.GetKeyDown(KeyCode.E))
        {
            QuestsButtonHandler.isQuestsUIOpen = false;

            gameObject.SetActive(false);
        }

        for (int i = 0; i < questsAmm; i++)
        {
            CheckQuestCompletion(i);
        }
    }

    public void ZoomIn()
    {
        questTree.transform.localScale = new Vector3(questTree.transform.localScale.x + 0.2f, questTree.transform.localScale.y + 0.2f, 1);
    }

    public void ZoomOut()
    {
        questTree.transform.localScale = new Vector3(questTree.transform.localScale.x - 0.2f, questTree.transform.localScale.y - 0.2f, 1);
    }

    void Scroll()
    {
        questTreePosX += Input.GetAxisRaw("Mouse ScrollWheel") * multiplier;
    }

    public void OnQuestClick()
    {
        questAnimator.SetBool("Is Quest Open", !questAnimator.GetBool("Is Quest Open"));
    }

    void CheckQuestCompletion(int quest)
    {
        if (quest == 0 && completedQuests[0] == false)
        {
            if (PlayerHandler.craftedObjects[0])
            {
                completedQuests[0] = true;
            }
        }

        for (int i = 0; i < completedQuests.Length; i++)
        {
            if (completedQuests[i])
            {
                for (int n = 0; n < questTree.transform.GetChild(0).GetChild(0).GetChild(i).childCount; n++)
                {
                    if (questTree.transform.GetChild(0).GetChild(0).GetChild(i).GetChild(n).GetComponent<Image>() != null && questTree.transform.GetChild(0).GetChild(0).GetChild(i).GetChild(n).GetComponent<Button>() == null)
                    {
                        questTree.transform.GetChild(0).GetChild(0).GetChild(i).GetChild(n).GetComponent<Image>().color = new Color(1, 1, 0, 1);
                    }
                }
            }
        }
    }

    private void OnDisable()
    {
        questAnimator.SetBool("Is Quest Open", false);
    }
}
