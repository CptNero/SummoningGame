using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CircleHandler : MonoBehaviour
{
    public GameObject currentlyPressedButton;
    [SerializeField] GameObject button1;
    [SerializeField] GameObject button2;
    [SerializeField] GameObject button3;
    [SerializeField] GameObject button4;
    [SerializeField] GameObject button5;
    [SerializeField] GameObject button6;
    [SerializeField] GameObject button7;
    [SerializeField] GameObject button8;
    [SerializeField] GameObject button9;
    [SerializeField] GameObject button10;
    public class Circle
    {
        public string name;
        public GameObject button;
        public Circle (string definedName, GameObject definedObject)
        {
            name = definedName;
            button = definedObject;
        }
    }
    List<Circle> circles = new List<Circle>();

    public Circle currentCircle = new Circle("No Cirlce Selected",null);
    void Start()
    {
        
        circles.Add(new Circle("I. Cricle",button1));
        circles.Add(new Circle("II. Cricle",button2));
        circles.Add(new Circle("III. Cricle",button3));
        circles.Add(new Circle("VI. Cricle",button4));
        circles.Add(new Circle("V. Cricle",button5));
        circles.Add(new Circle("VI. Cricle",button6));
        circles.Add(new Circle("VII. Cricle",button7));
        circles.Add(new Circle("VIII. Cricle",button8));
        circles.Add(new Circle("IX. Cricle",button9));
        circles.Add(new Circle("Purgatory",button10)); 
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentlyPressedButton!=null)
        {
        if(currentlyPressedButton != currentCircle.button)
        {
            currentCircle = circles.Find(obj => obj.button == currentlyPressedButton);
        }
        }
        
        Debug.Log(currentCircle.name);
    }
}
