using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;

public class BookController : MonoBehaviour
{
    // for example "Resources/Docs/lajos"
    public string pathToPages;
    public string extension;

    public LuigiBoardController luigiBoardController;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: load for current ghost
        LoadPageContents(pathToPages, extension);

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

    void LoadPageContents(string path, string extension)
    {
        var pagesDir = Path.Join(Application.dataPath, path);
        try
        {
            var directoryInfo = new DirectoryInfo(pagesDir);
            var pages = directoryInfo.GetFiles("*." + extension);

            foreach (var page in pages)
            {
                var reader = new StreamReader(page.OpenRead());
                var text = reader.ReadToEnd();

                this.pages.Add(new Page(text));
            }
        } catch
        {
            Debug.Log("directory not found");
        }

        
    }

    // NOTE: we are indexing pages from 0
    void LoadPage(int index)
    {
        currentPageIndex = index;

        UpdateCurrentPageContent();
        luigiBoardController.SetUnformattedText(pages[currentPageIndex].content.text);
    }

    public GameObject currentPageObject;
    void UpdateCurrentPageContent()
    {
        var pageComponent = currentPageObject.GetComponent<TextMeshPro>();
        if (pageComponent != null && currentPageIndex < pages.Count)
        {
            pageComponent.text = pages[currentPageIndex].content.text;
        }
    }
}
