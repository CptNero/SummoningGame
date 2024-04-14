using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;

public class BookController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LoadPageContents();

        // TODO: fix
        LoadPage(0);
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

    class PageContent
    {
        public string text;

        public PageContent(string text)
        {
            this.text = text;
        }
    }

    class Page
    {
        public PageContent content;

        public Page(string text)
        {
            this.content = new PageContent(text);
        }
    }



    List<Page> pages = new(); 
    int currentPageIndex = 0;

    void LoadPageContents()
    {
        var pagesDir = Path.Join(Application.dataPath, "Resources/Docs");
        var directoryInfo = new DirectoryInfo(pagesDir);
        var pages = directoryInfo.GetFiles("*.txt");

        foreach (var page in pages)
        {
            var reader = new StreamReader(page.OpenRead());
            var text = reader.ReadToEnd();

            this.pages.Add(new Page(text));
        }

        
    }

    // NOTE: we are indexing pages from 0
    void LoadPage(int index)
    {
        currentPageIndex = index;

        UpdateCurrentPageContent();
    }

    public GameObject currentPageObject;
    void UpdateCurrentPageContent()
    {
        var pageComponent = currentPageObject.GetComponent<TextMeshPro>();

        pageComponent.text = pages[currentPageIndex].content.text;
    }
}
