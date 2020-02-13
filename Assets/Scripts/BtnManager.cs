using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
    public GameObject searchPanel;
    public GameObject createPanel;
    public GameObject browsePanel;
    public GameObject searchResult;



    //This script is used to recognize which button was clicked for the Room Panel
    public void SearchBtn()
    {
        searchPanel.SetActive(true);
    }

    public void closeSearch()
    {
        searchPanel.SetActive(false);
    }

    public void CreatBtn()
    {
        createPanel.SetActive(true);
    }

    public void closeCreate()
    {
        createPanel.SetActive(false);
    }


    public void BrowseBtn()
    {
        browsePanel.SetActive(true);
    }

    public void closeBrowse()
    {
        browsePanel.SetActive(false);
        var clones = GameObject.FindGameObjectsWithTag("roomList");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
    }

    public void QuitBtn()
    {
        SceneManager.LoadScene("Menu");
    }
}
