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
    }

    class Page
    {
        public PageContent content;
    }



    List<Page> pages = new(); 
    int currentPageIndex = 0;

    void LoadPageContents()
    {
        string line;
        try
        {
            //Pass the file path and file name to the StreamReader constructor
            StreamReader sr = new StreamReader("pages.txt");
            //Read the first line of text
            line = sr.ReadLine();
            //Continue to read until you reach end of file
            while (line != null)
            {
                //write the line to console window
                Console.WriteLine(line);
                //Read the next line
                line = sr.ReadLine();
            }
            //close the file
            sr.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.WriteLine("Executing finally block.");
        }

        // TODO: REMOVE
        var firstPage = new Page();
        firstPage.content = new PageContent();
        firstPage.content.text = "left";

        var secondPage = new Page();
        secondPage.content = new PageContent();
        secondPage.content.text = "test";

        
        pages.Add(secondPage);
        pages.Add(firstPage);
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
