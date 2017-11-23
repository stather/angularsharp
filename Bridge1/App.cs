﻿using Bridge;
using Bridge.Html5;
using Newtonsoft.Json;
using System;
using Bridge.jQuery2;
using Bridge1.comp1;

namespace Bridge1
{
    public class App
    {
        public static void Main()
        {
            // Create a new HTML Button
            var button = new HTMLButtonElement();

            // Set the Button text
            button.InnerHTML = "Click Me";

            var l = new CompLoader();
            var c = new Comp1();
            l.Load(Document.Body, c);

            // Add a Click event handler
            button.OnClick = (ev) =>
            {
                c.MyProp = "Hello";
                // Write a message to the Console
                //Console.WriteLine("Welcome to Bridge.NET");
            };

            // Add the button to the document body
            Document.Body.AppendChild(button);

            // After building (Ctrl + Shift + B) this project, 
            // browse to the /bin/Debug or /bin/Release folder.

            // A new bridge/ folder has been created and
            // contains your projects JavaScript files. 

            // Open the bridge/index.html file in a browser by
            // Right-Click > Open With..., then choose a
            // web browser from the list

            // This application will then run in a browser.
        }
    }
}