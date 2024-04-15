using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CircleHandler : MonoBehaviour
{
    public GameObject currentlyPressedButton {get; set;}
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
        public string displayName;
        public GameObject button;
        public Circle (string definedName, GameObject definedObject)
        {
            displayName = definedName;
            button = definedObject;
        }
    }
    List<Circle> circles = new List<Circle>();

    public Circle currentCircle = new Circle("Purgatory", null);
    void Start()
    {
        const string circleString = "Circle";
        circles.Add(new Circle($"I. {circleString}",button1));
        circles.Add(new Circle($"II. {circleString}",button2));
        circles.Add(new Circle($"III. {circleString}",button3));
        circles.Add(new Circle($"VI. {circleString}",button4));
        circles.Add(new Circle($"V. {circleString}",button5));
        circles.Add(new Circle($"VI. {circleString}",button6));
        circles.Add(new Circle($"VII. {circleString}",button7));
        circles.Add(new Circle($"VIII. {circleString}",button8));
        circles.Add(new Circle($"IX. {circleString}",button9));
        circles.Add(new Circle("Purgatory",button10));

        currentlyPressedButton = circles.Last().button;
        currentCircle = circles.Last();
    }
    void Update()
    {
        if(currentlyPressedButton!=null)
        {
        if(currentlyPressedButton != currentCircle.button)
        {
            currentCircle = circles.Find(obj => obj.button == currentlyPressedButton);
        }
        }
    }
}
