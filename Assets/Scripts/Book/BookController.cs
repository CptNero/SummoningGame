using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class BookController : MonoBehaviour
{
    // for example "Resources/Docs/lajos"
    public string pathToPages;

    public LuigiBoardController luigiBoardController;
    public GameController gameController;

    // Start is called before the first frame update
    void OnEnable()
    {
        // TODO: load for current ghost
        LoadPageContents(pathToPages);

        LoadPage(0);
    }

    void OnDisable()
    {
        pages.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentPageIndex = Mathf.Min(pages.Count - 1, currentPageIndex + 1);
            LoadPage(currentPageIndex);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentPageIndex = Mathf.Max(0, currentPageIndex - 1);
            LoadPage(currentPageIndex);
        }
    }

    public class Page
    {
        public string content;

        public Page(string text)
        {
            this.content = text;
        }
    }

    List<Page> pages = new();
    int currentPageIndex = 0;

    public void LoadPageContents(string path)
    {
        if (path == "evidence")
        {
            pages.Add(new Page(gameController.gameState.GetCurrentSinner().evidence));
        }
        else if (path == "documentInfo")
        {
            var currentSinner = gameController.gameState.GetCurrentSinner();
            var pageContent =
                "Name: " + currentSinner.documentInfo.name + "\n"
                + "Age: " + currentSinner.documentInfo.age + "\n"
                + "Cause of death: " + currentSinner.documentInfo.causeOfDeath + "\n"
                + "Sins: ";
            foreach(var sin in currentSinner.documentInfo.sins)
            {
                pageContent += sin + " ";
            }
            pages.Add(new Page(pageContent));
        }
    }

    // NOTE: we are indexing pages from 0
    public void LoadPage(int index)
    {
        currentPageIndex = index;

        UpdateCurrentPageContent();
        luigiBoardController.SetEvidenceUnformattedText(pages[currentPageIndex].content);
    }

    public GameObject currentPageObject;
    void UpdateCurrentPageContent()
    {
        var pageComponent = currentPageObject.GetComponent<TextMeshPro>();
        if (pageComponent != null && currentPageIndex < pages.Count)
        {
            pageComponent.text = pages[currentPageIndex].content;
        }
    }
}
