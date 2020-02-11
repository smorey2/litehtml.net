using Litehtml.Services;
using NUnit.Framework;
using System;
#pragma warning disable 8321

namespace Litehtml
{
    public class api_ElementTest
    {
        static void alert(string message) { }
        static Document MakeDocument(string source) => document.createFromString(source.Replace("'", "\""), new container_test(), null, new context());

        [Test]
        public void Test()
        {
            Window window = null;
            Document thedoc;

            // https://www.w3schools.com/jsref/prop_html_accesskey.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<a id='myAnchor' href='https://www.w3schools.com/'>W3Schools</a><br>
<button onclick='myFunction()'>Try it</button>
<p id='demo'></p>
</body>
</html>");
                {
                    document.getElementById("myAnchor").accessKey = 'w';
                }
                {
                    var x = document.getElementById("myAnchor").accessKey;
                    Assert.AreEqual("w", x);
                }
            }

            // https://www.w3schools.com/jsref/met_element_addeventlistener.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<button id='myBtn'>Try it</button>
<p id='demo'></p>
</body>
</html>");
                {
                    document.getElementById("myBtn").addEventListener("click", @this =>
                    {
                        document.getElementById("demo").innerHTML = "Hello World";
                    });
                }
                {
                    document.getElementById("myBtn").addEventListener("click", myFunction);

                    void myFunction(Element @this)
                    {
                        document.getElementById("demo").innerHTML = "Hello World";
                    }
                }
                {
                    Action<Element> myFunction = null, someOtherFunction = null;
                    document.getElementById("myBtn").addEventListener("click", myFunction);
                    document.getElementById("myBtn").addEventListener("click", someOtherFunction);
                }
                {
                    Action<Element> myFunction = null, someOtherFunction = null;
                    document.getElementById("myBtn").addEventListener("mouseover", myFunction);
                    document.getElementById("myBtn").addEventListener("click", someOtherFunction);
                    document.getElementById("myBtn").addEventListener("mouseout", someOtherFunction);
                }
                {
                    Action<object, object> myFunction = null; object p1 = null, p2 = null;
                    document.getElementById("myBtn").addEventListener("click", @this =>
                    {
                        myFunction(p1, p2);
                    });
                }
                {
                    document.getElementById("myBtn").addEventListener("click", @this =>
                    {
                        @this.style.backgroundColor = "red";
                    });
                }
                {
                    Action<Element> myFunction = null;
                    document.getElementById("myDiv").addEventListener("click", myFunction, true);
                }
                {
                    Action<Element> myFunction = null;
                    // Attach an event handler to <div>
                    document.getElementById("myDIV").addEventListener("mousemove", myFunction);

                    // Remove the event handler from <div>
                    document.getElementById("myDIV").removeEventListener("mousemove", myFunction);
                }
            }

            // https://www.w3schools.com/jsref/met_node_appendchild.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<ul id='myList'>
  <li>Coffee</li>
  <li>Tea</li>
</ul>
<ul id='myList1'><li>Coffee</li><li>Tea</li></ul>
<ul id='myList2'><li>Water</li><li>Milk</li></ul>
<div id='myDIV'>
A DIV element
</div>
<button onclick='myFunction()'>Try it</button>
<p id='demo'></p>
</body>
</html>");
                {
                    var node = document.createElement("LI");                 // Create a <li> node
                    var textnode = document.createTextNode("Water");         // Create a text node
                    node.appendChild(textnode);                              // Append the text to <li>
                    document.getElementById("myList").appendChild(node);     // Append <li> to <ul> with id="myList"
                }
                {
                    var node = document.getElementById("myList2").lastChild;
                    document.getElementById("myList1").appendChild(node);
                }
                {
                    var para = document.createElement("P");                       // Create a <p> node
                    var t = document.createTextNode("This is a paragraph.");      // Create a text node
                    para.appendChild(t);                                          // Append the text to <p>
                    document.getElementById("myDIV").appendChild(para);           // Append <p> to <div> with id="myDIV"
                }
                {
                    var x = document.createElement("P");                        // Create a <p> node
                    var t = document.createTextNode("This is a paragraph.");    // Create a text node
                    x.appendChild(t);                                           // Append the text to <p>
                    document.body.appendChild(x);                               // Append <p> to <body>
                }
            }

            // https://www.w3schools.com/jsref/prop_node_attributes.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var x = document.getElementById("myBtn").attributes.length;
                }
                {
                    var x = document.getElementById("myBtn").attributes[1].name;
                }
                {
                    var x = document.getElementById("myImg").attributes.length;
                }
                {
                    var x = document.getElementById("myImg");
                    var txt = "";
                    int i;
                    for (i = 0; i < x.attributes.length; i++)
                    {
                        txt = txt + x.attributes[i].name + " = " + x.attributes[i].value + "<br>";
                    }
                }
            }

            // https://www.w3schools.com/jsref/met_html_blur.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    document.getElementById("myAnchor").blur();
                }
                {
                    document.getElementById("myText").blur();
                }
            }

            // https://www.w3schools.com/jsref/prop_element_childelementcount.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var x = document.getElementById("myDIV").childElementCount;
                }
            }

            // https://www.w3schools.com/jsref/prop_node_childnodes.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var c = document.body.childNodes;
                }
                {
                    var c = document.getElementById("myDIV").childNodes.length;
                }
                {
                    var c = document.getElementById("myDIV").childNodes;
                    (c[1] as Element).style.backgroundColor = "yellow";
                }
                {
                    var c = document.getElementById("mySelect").childNodes[2].textContent;
                }
            }

            // https://www.w3schools.com/jsref/prop_element_children.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var c = document.body.children;
                }
                {
                    var c = document.getElementById("myDIV").children.length;
                }
                {
                    var c = document.getElementById("myDIV").children;
                    c[1].style.backgroundColor = "yellow";
                }
                {
                    var c = document.getElementById("mySelect").children[2].textContent;
                }
                {
                    var c = document.body.children;
                    int i;
                    for (i = 0; i < c.length; i++)
                    {
                        c[i].style.backgroundColor = "red";
                    }
                }
            }

            // https://www.w3schools.com/jsref/prop_element_classlist.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    document.getElementById("myDIV").classList.add("mystyle");
                }
                {
                    document.getElementById("myDIV").classList.add("mystyle", "anotherClass", "thirdClass");
                }
                {
                    document.getElementById("myDIV").classList.remove("mystyle");
                }
                {
                    document.getElementById("myDIV").classList.remove("mystyle", "anotherClass", "thirdClass");
                }
                {
                    document.getElementById("myDIV").classList.toggle("newClassName");
                }
                {
                    document = MakeDocument(@"<div id='myDIV' class='mystyle anotherClass thirdClass'>I am a DIV element</div>");

                    var x = document.getElementById("myDIV").classList;
                }
                document = thedoc;
                {
                    var x = document.getElementById("myDIV").classList.length;
                }
                {
                    var x = document.getElementById("myDIV").classList.item(0);
                }
                {
                    var x = document.getElementById("myDIV").classList.contains("mystyle");
                }
                {
                    var x = document.getElementById("myDIV");

                    if (x.classList.contains("mystyle"))
                    {
                        x.classList.remove("anotherClass");
                    }
                    else
                    {
                        alert("Could not find it.");
                    }
                }
                {
                    // Get the button, and when the user clicks on it, execute myFunction
                    document.getElementById("myBtn").on("click", @this => { myFunction(); });

                    /* myFunction toggles between adding and removing the show class, which is used to hide and show the dropdown content */
                    void myFunction()
                    {
                        document.getElementById("myDropdown").classList.toggle("show");
                    }
                }
                {
                    Element x; string name; string[] arr;
                    x = document.getElementById("myDIV");
                    if (x.classList != null)
                    {
                        x.classList.add("mystyle");
                    }
                    else
                    {
                        name = "mystyle";
                        arr = x.className.Split(" ");
                        if (Array.IndexOf(arr, name) == -1)
                        {
                            x.className += " " + name;
                        }
                    }
                }
                {
                    var navbar = document.getElementById("navbar");

                    // Get the offset position of the navbar
                    var sticky = navbar.offsetTop;

                    // Add the sticky class to the navbar when you reach its scroll position. Remove the sticky class when you leave the scroll position.
                    void myFunction()
                    {
                        if (window.pageYOffset >= sticky)
                        {
                            navbar.classList.add("sticky");
                        }
                        else
                        {
                            navbar.classList.remove("sticky");
                        }
                    }
                }
            }

            // https://www.w3schools.com/jsref/prop_html_classname.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    document.getElementById("myDIV").className = "mystyle";
                }
                {
                    var x = document.getElementsByTagName("DIV")[0].className;
                }
                {
                    var x = document.getElementsByClassName("mystyle")[0].className;
                    var y = document.getElementById("myDIV").className;
                }
                {
                    var x = document.getElementById("myDIV").className;
                }
                {
                    document = MakeDocument(@"<div id='myDIV' class='mystyle test example'>I am a DIV element</div>");

                    var x = document.getElementById("myDIV").className;
                }
                document = thedoc;
                {
                    document = MakeDocument(@"<div id='myDIV' class='mystyle'>I am a DIV element</div>");

                    document.getElementById("myDIV").className = "newClassName";
                }
                document = thedoc;
                {
                    document.getElementById("myDIV").className += " anotherClass";
                }
                {
                    var x = document.getElementById("myDIV");

                    if (x.className == "mystyle")
                    {
                        x.style.fontSize = "30px";
                    }
                }
                {
                    void myFunction()
                    {
                        var x = document.getElementById("myDIV");
                        // If "mystyle" exist, overwrite it with "mystyle2"
                        if (x.className == "mystyle")
                        {
                            x.className = "mystyle2";
                        }
                        else
                        {
                            x.className = "mystyle";
                        }
                    }
                }
                {
                    window.on("scroll", @this => { myFunction(); });

                    void myFunction()
                    {
                        if (document.body.scrollTop > 50)
                        {
                            document.getElementById("myP").className = "test";
                        }
                        else
                        {
                            document.getElementById("myP").className = "";
                        }
                    }
                }
            }

            // https://www.w3schools.com/jsref/met_html_click.asp
            {
                var document = thedoc = MakeDocument(@"<input type='checkbox' id='myCheck' onmouseover='myFunction()' onclick='alert('clicked')'>");

                {
                    // On mouse-over, execute myFunction
                    void myFunction()
                    {
                        document.getElementById("myCheck").click(); // Click on the checkbox
                    }
                }
            }

            // https://www.w3schools.com/jsref/prop_element_clientheight.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var elmnt = document.getElementById("myDIV");
                    var txt = "Height with padding: " + elmnt.clientHeight + "px<br>";
                    txt += "Width with padding: " + elmnt.clientWidth + "px";
                }
                {
                    var elmnt = document.getElementById("myDIV");
                    var txt = "";
                    txt += "Height with padding: " + elmnt.clientHeight + "px<br>";
                    txt += "Height with padding and border: " + elmnt.offsetHeight + "px<br>";
                    txt += "Width with padding: " + elmnt.clientWidth + "px<br>";
                    txt += "Width with padding and border: " + elmnt.offsetWidth + "px";
                }
                {
                    var elmnt = document.getElementById("myDIV");
                    var txt = "";
                    txt += "Height with padding: " + elmnt.clientHeight + "px<br>";
                    txt += "Height with padding, border and scrollbar: " + elmnt.offsetHeight + "px<br>";
                    txt += "Width with padding: " + elmnt.clientWidth + "px<br>";
                    txt += "Width with padding, border and scrollbar: " + elmnt.offsetWidth + "px";
                }
            }

            // https://www.w3schools.com/jsref/prop_element_clientleft.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var elmnt = document.getElementById("myDIV");
                    var txt = "Border top width: " + elmnt.clientTop + "px<br>";
                    txt += "Border left width: " + elmnt.clientLeft + "px";
                }
                {
                    var left = document.getElementById("myDIV").clientLeft;
                }
            }

            // https://www.w3schools.com/jsref/prop_element_clienttop.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var elmnt = document.getElementById("myDIV");
                    var txt = "Border top width: " + elmnt.clientTop + "px<br>";
                    txt += "Border left width: " + elmnt.clientLeft + "px";
                }
            }

            // https://www.w3schools.com/jsref/prop_element_clientwidth.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var elmnt = document.getElementById("myDIV");
                    var txt = "Height with padding: " + elmnt.clientHeight + "px<br>";
                    txt += "Width with padding: " + elmnt.clientWidth + "px";
                }
                {
                    var elmnt = document.getElementById("myDIV");
                    var txt = "";
                    txt += "Height with padding: " + elmnt.clientHeight + "px<br>";
                    txt += "Height with padding and border: " + elmnt.offsetHeight + "px<br>";
                    txt += "Width with padding: " + elmnt.clientWidth + "px<br>";
                    txt += "Width with padding and border: " + elmnt.offsetWidth + "px";
                }
                {
                    var elmnt = document.getElementById("myDIV");
                    var txt = "";
                    txt += "Height with padding: " + elmnt.clientHeight + "px<br>";
                    txt += "Height with padding, border and scrollbar: " + elmnt.offsetHeight + "px<br>";
                    txt += "Width with padding: " + elmnt.clientWidth + "px<br>";
                    txt += "Width with padding, border and scrollbar: " + elmnt.offsetWidth + "px";
                }
            }

            // https://www.w3schools.com/jsref/met_node_clonenode.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var itm = document.getElementById("myList2").lastChild;

                    // Copy the <li> element and its child nodes
                    var cln = itm.cloneNode(true);

                    // Append the cloned <li> element to <ul> with id="myList1"
                    document.getElementById("myList1").appendChild(cln);
                }
                {
                    var elmnt = document.getElementsByTagName("DIV")[0];
                    var cln = elmnt.cloneNode(true);
                    document.body.appendChild(cln);
                }
            }

            // https://www.w3schools.com/jsref/met_node_comparedocumentposition.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var p1 = document.getElementById("p1");
                    var p2 = document.getElementById("p2");
                    var x = p1.compareDocumentPosition(p2);
                }
            }

            // https://www.w3schools.com/jsref/met_node_contains.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var span = document.getElementById("mySPAN");
                    var div = document.getElementById("myDIV").contains(span);
                }
            }

            // https://www.w3schools.com/jsref/prop_html_contenteditable.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    document.getElementById("myP").contentEditable = "true";
                }
                {
                    var x = document.getElementById("myP").contentEditable;
                }
                {
                    var button = document.getElementsByTagName("button")[0];

                    var x = document.getElementById("myP");
                    if (x.contentEditable == "true")
                    {
                        x.contentEditable = "false";
                        button.innerHTML = "Enable content of p to be editable!";
                    }
                    else
                    {
                        x.contentEditable = "true";
                        button.innerHTML = "Disable content of p to be editable!";
                    }
                }
            }

            // https://www.w3schools.com/jsref/prop_html_dir.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    document.getElementById("myP").dir = "rtl";
                }
            }

            // https://www.w3schools.com/jsref/met_element_exitfullscreen.asp
            {
            }

            // https://www.w3schools.com/jsref/prop_node_firstchild.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var x = (document.getElementById("myList").firstChild as Element).innerHTML;
                }
                {
                    document = MakeDocument(
    @"<div id='myDIV'>
  <p>Looks like first child</p>
  <span>Looks like last Child</span>
</div>");

                    var x = document.getElementById("myDIV").firstChild.nodeName;
                    document.getElementById("demo").innerHTML = x;
                }
                document = thedoc;
                {
                    document = MakeDocument(@"<div id='myDIV'><p>First child</p><span>Last Child</span></div>");

                    var x = document.getElementById("myDIV").firstChild.nodeName;
                    document.getElementById("demo").innerHTML = x;
                }
                document = thedoc;
                {
                    var x = document.getElementById("mySelect").firstChild.textContent;
                }
            }

            // https://www.w3schools.com/jsref/prop_element_firstelementchild.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var x = document.getElementById("myList").firstElementChild.innerHTML;
                }
                {
                    var x = document.getElementById("myDIV").firstElementChild.tagName;
                    document.getElementById("demo").innerHTML = x;
                }
                {
                    var x = document.getElementById("mySelect").firstElementChild.textContent;
                }
            }

            // https://www.w3schools.com/jsref/met_html_focus.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    document.getElementById("myAnchor").focus();
                }
                {
                    document.getElementById("myText").focus();
                }
                {
                    window.on("load", @this =>
                    {
                        document.getElementById("myText").focus();
                    });
                }
            }

            // https://www.w3schools.com/jsref/met_element_getattribute.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var x = document.getElementsByTagName("H1")[0].getAttribute("class");
                }
                {
                    var x = document.getElementById("myAnchor").getAttribute("target");
                }
                {
                    var x = document.getElementById("myBtn").getAttribute("onclick");
                }
            }

            // https://www.w3schools.com/jsref/met_element_getattributenode.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var elmnt = document.getElementsByTagName("H1")[0];
                    var attr = elmnt.getAttributeNode("class").value;
                }
                {
                    var elmnt = document.getElementById("myAnchor");
                    var attr = elmnt.getAttributeNode("target").value;
                }
                {
                    var elmnt = document.getElementById("myBtn");
                    var attr = elmnt.getAttributeNode("onclick").value;
                }
            }

            // https://www.w3schools.com/jsref/met_element_getboundingclientrect.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var obj = new Element();
                    var rect = obj.getBoundingClientRect();
                }
            }

            // https://www.w3schools.com/jsref/met_element_getelementsbyclassname.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var list = document.getElementsByClassName("example")[0];
                    list.getElementsByClassName("child")[0].innerHTML = "Milk";
                }
                {
                    var x = document.getElementById("myDIV");
                    x.getElementsByClassName("child")[1].style.backgroundColor = "red";
                }
                {
                    var x = document.getElementById("myDIV").getElementsByClassName("child").length;
                }
                {
                    var x = document.getElementsByClassName("example")[1];
                    x.getElementsByClassName("child color")[0].style.backgroundColor = "red";
                }
                {
                    var x = document.getElementById("myDIV");
                    var y = x.getElementsByClassName("child");
                    int i;
                    for (i = 0; i < y.length; i++)
                    {
                        y[i].style.backgroundColor = "red";
                    }
                }
            }

            // https://www.w3schools.com/jsref/met_element_getelementsbytagname.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var list = document.getElementsByTagName("UL")[0];
                    list.getElementsByTagName("LI")[0].innerHTML = "Milk";
                }
                {
                    var x = document.getElementById("myDIV").getElementsByTagName("P").length;
                }
                {
                    var x = document.getElementById("myDIV");
                    x.getElementsByTagName("P")[1].style.backgroundColor = "red";
                }
                {
                    var x = document.getElementById("myDIV");
                    var y = x.getElementsByTagName("P");
                    int i;
                    for (i = 0; i < y.length; i++)
                    {
                        y[i].style.backgroundColor = "red";
                    }
                }
                {
                    var x = document.getElementById("myDIV");
                    x.getElementsByTagName("*")[3].style.backgroundColor = "red";
                }
                {
                    var x = document.getElementById("myDIV");
                    var y = x.getElementsByTagName("*");
                    int i;
                    for (i = 0; i < y.length; i++)
                    {
                        y[i].style.backgroundColor = "red";
                    }
                }
            }

            // https://www.w3schools.com/jsref/met_element_hasattribute.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var x = document.getElementById("myBtn").hasAttribute("onclick");
                }
                {
                    // Get the <a> element with id="myAnchor"
                    var x = document.getElementById("myAnchor");

                    // If the <a> element has a target attribute, set the value to "_self"
                    if (x.hasAttribute("target"))
                    {
                        x.setAttribute("target", "_self");
                    }
                }
            }

            // https://www.w3schools.com/jsref/met_node_hasattributes.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var x = document.body.hasAttributes();
                }
            }

            // https://www.w3schools.com/jsref/met_node_haschildnodes.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var list = document.getElementById("myList").hasChildNodes();
                }
                {
                    // Get the <ul> element with id="myList"
                    var list = document.getElementById("myList");

                    // If the <ul> element has any child nodes, remove its first child node
                    if (list.hasChildNodes())
                    {
                        list.removeChild(list.childNodes[0]);
                    }
                }
            }

            // https://www.w3schools.com/jsref/prop_html_id.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var x = document.getElementsByClassName("anchors")[0].id;
                }
                {
                    document.getElementById("demo").id = "newid";
                }
                {
                    var x = document.getElementsByTagName("DIV")[0];

                    if (x.id == "myDIV")
                    {
                        x.style.fontSize = "30px";
                    }
                }
            }

            // https://www.w3schools.com/jsref/prop_html_innerhtml.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    document.getElementById("demo").innerHTML = "Paragraph changed!";
                }
                {
                    var x = document.getElementById("myP").innerHTML;
                }
                {
                    var x = document.getElementById("myList").innerHTML;
                }
                {
                    document.getElementById("myP").innerHTML = "Hello Dolly.";
                    document.getElementById("myDIV").innerHTML = "How are you?";
                }
                {
                    alert(document.getElementById("demo").innerHTML);
                }
                {
                    document.getElementById("demo").innerHTML = "";  // Replaces the content of <p> with an empty string
                }
                {
                    document.getElementById("myAnchor").innerHTML = "W3Schools";
                    document.getElementById("myAnchor").setAttribute("href", "https://www.w3schools.com");
                    document.getElementById("myAnchor").setAttribute("target", "_blank");
                }
            }

            // https://www.w3schools.com/jsref/prop_node_innertext.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var x = document.getElementById("myBtn").innerText;
                }
                {
                    document = MakeDocument(@"<p id='demo'>   This element has extra spacing     and contains <span>a span element</span>.</p>");

                    void getInnerText()
                    {
                        alert(document.getElementById("demo").innerText);
                        //Assert.AreEqual("This element has extra spacing and contains a span element.");
                    }

                    void getHTML()
                    {
                        alert(document.getElementById("demo").innerHTML);
                        //Assert.AreEqual("   This element has extra spacing     and contains <span>a span element</span>.");
                    }

                    void getTextContent()
                    {
                        alert(document.getElementById("demo").textContent);
                        //Assert.AreEqual("   This element has extra spacing    and contains a span element.");
                    }
                }
                document = thedoc;
            }

            // https://www.w3schools.com/jsref/met_node_insertadjacentelement.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var s = document.getElementById("mySpan");
                    var h = document.getElementById("myH2");
                    h.insertAdjacentElement("afterend", s);
                }
                {
                    var s = document.getElementById("mySpan");
                    var h = document.getElementById("myH2");
                    h.insertAdjacentElement("afterbegin", s);
                }
                {
                    var s = document.getElementById("mySpan");
                    var h = document.getElementById("myH2");
                    h.insertAdjacentElement("beforebegin", s);
                }
                {
                    var s = document.getElementById("mySpan");
                    var h = document.getElementById("myH2");
                    h.insertAdjacentElement("beforeend", s);
                }
            }

            // https://www.w3schools.com/jsref/met_node_insertadjacenthtml.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                var h = document.getElementById("myH2");
                {
                    h.insertAdjacentHTML("afterend", "<p>My new paragraph</p>");
                }
                {
                    h.insertAdjacentHTML("afterbegin", "<span style='color:red'>My span</span>");
                }
                {
                    h.insertAdjacentHTML("beforebegin", "<span style='color:red'>My span</span>");
                }
                {
                    h.insertAdjacentHTML("beforeend", "<span style='color:red'>My span</span>");
                }
            }

            // https://www.w3schools.com/jsref/met_node_insertadjacenttext.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var h = document.getElementById("myH2");
                    h.insertAdjacentText("afterend", "My inserted text");
                }
                {
                    var h = document.getElementById("myH2");
                    h.insertAdjacentText("afterbegin", "My inserted text");
                }
                {
                    var h = document.getElementById("myH2");
                    h.insertAdjacentText("beforebegin", "My inserted text");
                }
                {
                    var h = document.getElementById("myH2");
                    h.insertAdjacentText("beforeend", "My inserted text");
                }
            }

            // https://www.w3schools.com/jsref/met_node_insertbefore.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var newItem = document.createElement("LI");       // Create a <li> node
                    var textnode = document.createTextNode("Water");  // Create a text node
                    newItem.appendChild(textnode);                    // Append the text to <li>

                    var list = document.getElementById("myList");    // Get the <ul> element to insert a new node
                    list.insertBefore(newItem, list.childNodes[0]);  // Insert <li> before the first child of <ul>
                }
                {
                    var node = document.getElementById("myList2").lastChild;
                    var list = document.getElementById("myList1");
                    list.insertBefore(node, list.childNodes[0]);
                }
            }

            // https://www.w3schools.com/jsref/prop_html_iscontenteditable.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var x = document.getElementById("myP").isContentEditable;
                }
            }

            // https://www.w3schools.com/jsref/met_node_isdefaultnamespace.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var x = document.documentElement.isDefaultNamespace("http://www.w3.org/1999/xhtml");
                }
            }

            // https://www.w3schools.com/jsref/met_node_isequalnode.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var item1 = document.getElementById("myList1").firstChild;
                    var item2 = document.getElementById("myList2").firstChild;
                    var x = item1.isEqualNode(item2);
                }
            }

            // https://www.w3schools.com/jsref/met_node_issamenode.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var item1 = document.getElementById("myList1");      // An <ul> element with id="myList"
                    var item2 = document.getElementsByTagName("UL")[0];  // The first <ul> element in the document
                    var x = item1.isSameNode(item2);
                }
                {
                    var item1 = document.getElementById("myList");
                    var item2 = document.getElementsByTagName("UL")[0];

                    if (item1 == item2)
                    {
                        alert("THEY ARE THE SAME!!");
                    }
                    else
                    {
                        alert("They are not the same.");
                    }
                }
            }

            // https://www.w3schools.com/jsref/prop_html_lang.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var x = document.getElementById("myP").lang;
                }
            }


            // https://www.w3schools.com/jsref/prop_node_lastchild.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var x = (document.getElementById("myList").lastChild as Element).innerHTML;
                }
                {
                    document = MakeDocument(@"
<div id='myDIV'>
  <p>Looks like first child</p>
  <span>Looks like last Child</span>
</div>");

                    var x = document.getElementById("myDIV").lastChild.nodeName;
                    document.getElementById("demo").innerHTML = x;
                }
                {
                    document = MakeDocument(@"<div id='myDIV'><p>First child</p><span>Last Child</span></div>");

                    var x = document.getElementById("myDIV").lastChild.nodeName;
                    document.getElementById("demo").innerHTML = x;
                }
                {
                    var x = document.getElementById("mySelect").lastChild.textContent;
                }
            }

            // https://www.w3schools.com/jsref/prop_element_lastelementchild.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var x = document.getElementById("myList").lastElementChild.innerHTML;
                }
                {
                    var x = document.getElementById("myDIV").lastElementChild.tagName;
                    document.getElementById("demo").innerHTML = x;
                }
                {
                    var x = document.getElementById("mySelect").lastElementChild.textContent;
                }
            }

            // https://www.w3schools.com/jsref/prop_node_namespaceuri.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var x = document.documentElement.namespaceURI;
                }
            }

            // https://www.w3schools.com/jsref/prop_node_nextsibling.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var x = (document.getElementById("item1").nextSibling as Element).innerHTML;
                }
            }

            // https://www.w3schools.com/jsref/prop_element_nextelementsibling.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var x = document.getElementById("item1").nextElementSibling.innerHTML;
                }
            }

            // https://www.w3schools.com/jsref/prop_node_nodename.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var x = document.getElementById("myP").nodeName;
                }
                {
                    var x = document.body.nodeName;
                }
                {
                    var c = document.body.childNodes;
                    var txt = "";
                    int i;
                    for (i = 0; i < c.length; i++)
                    {
                        txt = txt + c[i].nodeName + "<br>";
                    }

                    document.getElementById("demo").innerHTML = txt;
                }
                {
                    document = MakeDocument(@"<div id='myDIV'>This is a div element.</div>");

                    var x = document.getElementById("myDIV").firstChild;
                    var txt = "";
                    txt += "The node name: " + x.nodeName + "<br>";
                    txt += "The node value: " + x.nodeValue + "<br>";
                    txt += "The node type: " + x.nodeType;
                }
                document = thedoc;
            }


            // https://www.w3schools.com/jsref/prop_node_nodetype.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var x = document.getElementById("myP").nodeType;
                }
                {
                    var x = document.body.nodeType;
                }
                {
                    var c = document.body.childNodes;
                    var txt = "";
                    int i;
                    for (i = 0; i < c.length; i++)
                    {
                        txt = txt + c[i].nodeType + "<br>";
                    }

                    document.getElementById("demo").innerHTML = txt;
                }
                {
                    document = MakeDocument(@"<div id='myDIV'>This is a div element.</div>");

                    var x = document.getElementById("myDIV").firstChild;
                    var txt = "";
                    txt += "The node name: " + x.nodeName + "<br>";
                    txt += "The node value: " + x.nodeValue + "<br>";
                    txt += "The node type: " + x.nodeType;
                }
                document = thedoc;
            }

            // https://www.w3schools.com/jsref/prop_node_nodevalue.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var x = document.getElementsByTagName("BUTTON")[0].childNodes[0].nodeValue;
                }
                {
                    document = MakeDocument(@"<div id='myDIV'>This is a div element.</div>");

                    var x = document.getElementById("myDIV").firstChild;
                    var txt = "";
                    txt += "The node name: " + x.nodeName + "<br>";
                    txt += "The node value: " + x.nodeValue + "<br>";
                    txt += "The node type: " + x.nodeType;
                }
                document = thedoc;
            }

            // https://www.w3schools.com/jsref/met_node_normalize.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    document.getElementById("demo").normalize();
                }
            }

            // https://www.w3schools.com/jsref/prop_element_offsetheight.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var elmnt = document.getElementById("myDIV");
                    var txt = "Height with padding and border: " + elmnt.offsetHeight + "px<br>";
                    txt += "Width with padding and border: " + elmnt.offsetWidth + "px";
                }
                {
                    var elmnt = document.getElementById("myDIV");
                    var txt = "";
                    txt += "Height with padding: " + elmnt.clientHeight + "px<br>";
                    txt += "Height with padding and border: " + elmnt.offsetHeight + "px<br>";
                    txt += "Width with padding: " + elmnt.clientWidth + "px<br>";
                    txt += "Width with padding and border: " + elmnt.offsetWidth + "px";
                }
                {
                    var elmnt = document.getElementById("myDIV");
                    var txt = "";
                    txt += "Height with padding: " + elmnt.clientHeight + "px<br>";
                    txt += "Height with padding, border and scrollbar: " + elmnt.offsetHeight + "px<br>";
                    txt += "Width with padding: " + elmnt.clientWidth + "px<br>";
                    txt += "Width with padding, border and scrollbar: " + elmnt.offsetWidth + "px";
                }
            }


            // https://www.w3schools.com/jsref/prop_element_offsetwidth.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var elmnt = document.getElementById("myDIV");
                    var txt = "Height with padding and border: " + elmnt.offsetHeight + "px<br>";
                    txt += "Width with padding and border: " + elmnt.offsetWidth + "px";
                }
                {
                    var elmnt = document.getElementById("myDIV");
                    var txt = "";
                    txt += "Height with padding: " + elmnt.clientHeight + "px<br>";
                    txt += "Height with padding and border: " + elmnt.offsetHeight + "px<br>";
                    txt += "Width with padding: " + elmnt.clientWidth + "px<br>";
                    txt += "Width with padding and border: " + elmnt.offsetWidth + "px";
                }
                {
                    var elmnt = document.getElementById("myDIV");
                    var txt = "";
                    txt += "Height with padding: " + elmnt.clientHeight + "px<br>";
                    txt += "Height with padding, border and scrollbar: " + elmnt.offsetHeight + "px<br>";
                    txt += "Width with padding: " + elmnt.clientWidth + "px<br>";
                    txt += "Width with padding, border and scrollbar: " + elmnt.offsetWidth + "px";
                }
            }

            // https://www.w3schools.com/jsref/prop_element_offsetleft.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var testDiv = document.getElementById("test");
                    document.getElementById("demo").innerHTML = testDiv.offsetLeft.ToString();
                }
                {
                    var testDiv = document.getElementById("test");
                    var demoDiv = document.getElementById("demo");
                    demoDiv.innerHTML = "offsetLeft: " + testDiv.offsetLeft + "<br>offsetTop: " + testDiv.offsetTop;
                }
            }

            // https://www.w3schools.com/jsref/prop_element_offsetparent.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var testDiv = document.getElementById("test");
                    document.getElementById("demo").innerHTML = testDiv.offsetParent.ToString();
                }
            }

            // https://www.w3schools.com/jsref/prop_element_offsettop.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var testDiv = document.getElementById("test");
                    document.getElementById("demo").innerHTML = testDiv.offsetTop.ToString();
                }
                {
                    var testDiv = document.getElementById("test");
                    var demoDiv = document.getElementById("demo");
                    demoDiv.innerHTML = "offsetLeft: " + testDiv.offsetLeft + "<br>offsetTop: " + testDiv.offsetTop;
                }
                {
                    // Get the navbar
                    var navbar = document.getElementById("navbar");

                    // Get the offset position of the navbar
                    var sticky = navbar.offsetTop;

                    // Add the sticky class to the navbar when you reach its scroll position. Remove the sticky class when you leave the scroll position.
                    void myFunction()
                    {
                        if (window.pageYOffset >= sticky)
                        {
                            navbar.classList.add("sticky");
                        }
                        else
                        {
                            navbar.classList.remove("sticky");
                        }
                    }
                }
            }


            /*
            // https://www.w3schools.com/jsref/prop_html_outerhtml.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    document.getElementsByTagName("h1").outerHTML = "<h3>Header Changed!</h3>";
                }
                {
                    var x = document.getElementsByTagName("h1")[0].outerHTML;
                    alert(x);
                }
            }
            */

            /*
            // https://www.w3schools.com/jsref/prop_node_outertext.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    document.getElementById("myH1").outerText = "Changed content!";
                }
            }
            */

            // https://www.w3schools.com/jsref/prop_node_ownerdocument.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var x = document.getElementById("myP").ownerDocument.nodeType;
                }
            }

            // https://www.w3schools.com/jsref/prop_node_parentnode.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var x = document.getElementById("myLI").parentNode.nodeName;
                }
            }

            // https://www.w3schools.com/jsref/prop_node_parentelement.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var x = document.getElementById("myLI").parentElement.nodeName;
                }
            }

            // https://www.w3schools.com/jsref/prop_node_previoussibling.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var x = (document.getElementById("item2").previousSibling as Element).innerHTML;
                }
            }

            // https://www.w3schools.com/jsref/prop_element_previouselementsibling.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var x = document.getElementById("item2").previousElementSibling.innerHTML;
                }
            }

            // https://www.w3schools.com/jsref/met_element_queryselector.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var x = document.getElementById("myDIV");
                    x.querySelector(".example").innerHTML = "Hello World!";
                }
                {
                    var x = document.getElementById("myDIV");
                    x.querySelector("p").innerHTML = "Hello World!";
                }
                {
                    var x = document.getElementById("myDIV");
                    x.querySelector("p.example").innerHTML = "Hello World!";
                }
                {
                    var x = document.getElementById("myDIV");
                    x.querySelector("#demo").innerHTML = "Hello World!";
                }
                {
                    var x = document.getElementById("myDIV");
                    x.querySelector("a[target]").style.border = "10px solid red";
                }
                {
                    document = MakeDocument(@"
<div id='myDIV'>
  <h2>A h2 element</h2>
  <h3>A h3 element</h3>
</div>
");

                    var x = document.getElementById("myDIV");
                    x.querySelector("h2, h3").style.backgroundColor = "red";
                }
                document = thedoc;
                {
                    document = MakeDocument(@"
<div id='myDIV'>
  <h3>A h3 element</h3>
  <h2>A h2 element</h2>
</div>
");

                    var x = document.getElementById("myDIV");
                    x.querySelector("h2, h3").style.backgroundColor = "red";
                }
                document = thedoc;
            }

            // https://www.w3schools.com/jsref/met_element_queryselectorall.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    // Get the element with id="myDIV" (a div), then get all elements inside div with class="example"
                    var x = document.getElementById("myDIV").querySelectorAll(".example");

                    // Set the background color of the first element with class="example" (index 0) in div
                    x[0].style.backgroundColor = "red";
                }
                {
                    // Get the element with id="myDIV" (a div), then get all p elements inside div
                    var x = document.getElementById("myDIV").querySelectorAll("p");

                    // Set the background color of the first <p> element (index 0) in div
                    x[0].style.backgroundColor = "red";
                }
                {
                    // Get the element with id="myDIV" (a div), then get all p elements with class="example" inside div
                    var x = document.getElementById("myDIV").querySelectorAll("p.example");

                    // Set the background color of the first <p> element with class="example" (index 0) in div
                    x[0].style.backgroundColor = "red";
                }
                {
                    /* Get the element with id="myDIV" (a div), then get all p elements with class="example" inside div, and return the number of elements found */
                    var x = document.getElementById("myDIV").querySelectorAll(".example").length;
                }
                {
                    // Get the element with id="myDIV" (a div), then get all elements with class="example" inside div
                    var x = document.getElementById("myDIV").querySelectorAll(".example");

                    // Create a for loop and set the background color of all elements with class="example" in div
                    int i;
                    for (i = 0; i < x.length; i++)
                    {
                        x[i].style.backgroundColor = "red";
                    }
                }
                {
                    // Get the element with id="myDIV" (a div), then get all p elements inside div
                    var x = document.getElementById("myDIV").querySelectorAll("p");

                    // Create a for loop and set the background color of all p elements in div
                    int i;
                    for (i = 0; i < x.length; i++)
                    {
                        x[i].style.backgroundColor = "red";
                    }
                }
                {
                    // Get the element with id="myDIV" (a div), then get all <a> elements with a "target" attribute inside div
                    var x = document.getElementById("myDIV").querySelectorAll("a[target]");

                    // Create a for loop and set the border of all <a> elements with a target attribute in div
                    int i;
                    for (i = 0; i < x.length; i++)
                    {
                        x[i].style.border = "10px solid red";
                    }
                }
                {
                    // Get the element with id="myDIV" (a div), then get all <h2>, <div> and <span> elements inside <div>
                    var x = document.getElementById("myDIV").querySelectorAll("h2, div, span");

                    // Create a for loop and set the background color of all <h2>, <div> and <span> elements in <div>
                    int i;
                    for (i = 0; i < x.length; i++)
                    {
                        x[i].style.backgroundColor = "red";
                    }
                }
            }

            /*
            // https://www.w3schools.com/jsref/met_element_queryselectorall.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var myobj = document.getElementById("demo");
                    myobj.remove();
                }
            }
            */

            // https://www.w3schools.com/jsref/met_element_removeattribute.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    document.getElementsByTagName("H1")[0].removeAttribute("class");
                }
                {
                    document.getElementById("myAnchor").removeAttribute("href");
                }
            }

            // https://www.w3schools.com/jsref/met_element_removeattributenode.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var elmnt = document.getElementsByTagName("H1")[0];  // Get the first <h1> element in the document
                    var attr = elmnt.getAttributeNode("class");          // Get the class attribute node from <h1>
                    elmnt.removeAttributeNode(attr);                     // Remove the class attribute node from <h1>
                }
                {
                    var elmnt = document.getElementById("myAnchor");   // Get the <a> element with id="myAnchor"
                    var attr = elmnt.getAttributeNode("href");         // Get the href attribute node from <a>
                    elmnt.removeAttributeNode(attr);                   // Remove the href attribute node from <a>
                }
            }

            // https://www.w3schools.com/jsref/met_node_removechild.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var list = document.getElementById("myList");   // Get the <ul> element with id="myList"
                    list.removeChild(list.childNodes[0]);           // Remove <ul>'s first child node (index 0)
                }
                {
                    // Get the <ul> element with id="myList"
                    var list = document.getElementById("myList");

                    // If the <ul> element has any child nodes, remove its first child node
                    if (list.hasChildNodes())
                    {
                        list.removeChild(list.childNodes[0]);
                    }
                }
                {
                    // Get the <ul> element with id="myList"
                    var list = document.getElementById("myList");

                    // As long as <ul> has a child node, remove it
                    while (list.hasChildNodes())
                    {
                        list.removeChild(list.firstChild);
                    }
                }
                {
                    var item = document.getElementById("myLI");
                    item.parentNode.removeChild(item);
                }
                {
                    var item = document.getElementById("myLI");

                    void removeLi()
                    {
                        item.parentNode.removeChild(item);
                    }

                    void appendLi()
                    {
                        var list = document.getElementById("myList");
                        list.appendChild(item);
                    }
                }
                /*
                {
                    var child = document.getElementById("mySpan");

                    void removeLi()
                    {
                        child.parentNode.removeChild(child);
                    }

                    void myFunction()
                    {
                        var frame = document.getElementsByTagName("IFRAME")[0];
                        var h = frame.contentWindow.document.getElementsByTagName("H1")[0];
                        var x = document.adoptNode(child);
                        h.appendChild(x);
                    }
                }
                */
            }

            // https://www.w3schools.com/jsref/met_element_removeeventlistener.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    Action<Element> myFunction = null;

                    // Attach an event handler to <div>
                    document.getElementById("myDIV").addEventListener("mousemove", myFunction);

                    // Remove the event handler from <div>
                    document.getElementById("myDIV").removeEventListener("mousemove", myFunction);
                }
            }

            // https://www.w3schools.com/jsref/met_node_replacechild.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    // Create a new text node called "Water"
                    var textnode = document.createTextNode("Water");

                    // Get the first child node of an <ul> element
                    var item = document.getElementById("myList").childNodes[0];

                    // Replace the first child node of <ul> with the newly created text node
                    item.replaceChild(textnode, item.childNodes[0]);

                    // Note: This example replaces only the Text node "Coffee" with a Text node "Water"
                }
                {
                    // Create a new <li> element
                    var elmnt = document.createElement("li");

                    // Create a new text node called "Water"
                    var textnode = document.createTextNode("Water");

                    // Append the text node to <li>
                    elmnt.appendChild(textnode);

                    // Get the <ul> element with id="myList"
                    var item = document.getElementById("myList");

                    // Replace the first child node (<li> with index 0) in <ul> with the newly created <li> element
                    item.replaceChild(elmnt, item.childNodes[0]);

                    // Note: This example replaces the entire <li> element
                }
            }

            // https://www.w3schools.com/jsref/met_element_requestfullscreen.asp
            {
            }

            // https://www.w3schools.com/jsref/prop_element_scrollheight.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var elmnt = document.getElementById("content");
                    var y = elmnt.scrollHeight;
                    var x = elmnt.scrollWidth;
                }
                {
                    var elmnt = document.getElementById("content");
                    var y = elmnt.scrollHeight;
                    var x = elmnt.scrollWidth;
                }
                {
                    var elmnt = document.getElementById("content");

                    void getFunction()
                    {
                        var x = elmnt.scrollWidth;
                        var y = elmnt.scrollHeight;
                    }

                    void setFunction()
                    {
                        elmnt.style.height = elmnt.scrollHeight + "px";
                        elmnt.style.width = elmnt.scrollWidth + "px";
                    }
                }
            }

            // https://www.w3schools.com/jsref/met_element_scrollintoview.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var elmnt = document.getElementById("content");
                    elmnt.scrollIntoView();
                }
                {
                    var elmnt = document.getElementById("content");

                    void scrollToTop()
                    {
                        elmnt.scrollIntoView(true); // Top
                    }

                    void scrollToBottom()
                    {
                        elmnt.scrollIntoView(false); // Bottom
                    }
                }
            }

            // XXXX
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {

                }
            }

            // https://www.w3schools.com/jsref/prop_element_scrollleft.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var elmnt = document.getElementById("myDIV");
                    var x = elmnt.scrollLeft;
                    var y = elmnt.scrollTop;
                }
                {
                    var elmnt = document.getElementById("myDIV");
                    elmnt.scrollLeft = 50;
                    elmnt.scrollTop = 10;
                }
                {
                    var elmnt = document.getElementById("myDIV");
                    elmnt.scrollLeft += 50;
                    elmnt.scrollTop += 10;
                }
                {
                    var body = document.body; // Safari
                    var html = document.documentElement; // Chrome, Firefox, IE and Opera places the overflow at the <html> level, unless else is specified. Therefore, we use the documentElement property for these browsers
                    body.scrollLeft += 30;
                    body.scrollTop += 10;
                    html.scrollLeft += 30;
                    html.scrollTop += 10;
                }
            }

            // https://www.w3schools.com/jsref/prop_element_scrolltop.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var elmnt = document.getElementById("myDIV");
                    var x = elmnt.scrollLeft;
                    var y = elmnt.scrollTop;
                }
                {
                    var elmnt = document.getElementById("myDIV");
                    elmnt.scrollLeft = 50;
                    elmnt.scrollTop = 10;
                }
                {
                    var elmnt = document.getElementById("myDIV");
                    elmnt.scrollLeft += 50;
                    elmnt.scrollTop += 10;
                }
                {
                    var body = document.body; // Safari
                    var html = document.documentElement; // Chrome, Firefox, IE and Opera places the overflow at the <html> level, unless else is specified. Therefore, we use the documentElement property for these browsers
                    body.scrollLeft += 30;
                    body.scrollTop += 10;
                    html.scrollLeft += 30;
                    html.scrollTop += 10;
                }
                {
                    window.on("scroll", @this => { myFunction(); });

                    void myFunction()
                    {
                        if (document.body.scrollTop > 50 || document.documentElement.scrollTop > 50)
                        {
                            document.getElementById("myP").className = "test";
                        }
                        else
                        {
                            document.getElementById("myP").className = "";
                        }
                    }
                }
                {
                    window.on("scroll", @this => { myFunction(); });

                    void myFunction()
                    {
                        if (document.body.scrollTop > 350 || document.documentElement.scrollTop > 350)
                        {
                            document.getElementById("myImg").className = "slideUp";
                        }
                    }
                }
                /*
                {
                    document = MakeDocument(@"
<svg id='mySVG'>
  <path fill='none' stroke='red' stroke-width='3' id='triangle' d='M150 0 L75 200 L225 200 Z'>
</svg>");

                    // Get the id of the <path> element and the length of <path>
                    var triangle = document.getElementById("triangle");
                    var length = triangle.getTotalLength();

                    // The start position of the drawing
                    triangle.style.strokeDasharray = length;

                    // Hide the triangle by offsetting dash. Remove this line to show the triangle before scroll draw
                    triangle.style.strokeDashoffset = length;

                    // Find scroll percentage on scroll (using cross-browser properties), and offset dash same amount as percentage scrolled
                    window.addEventListener("scroll", myFunction);

                    void myFunction()
                    {
                        var scrollpercent = (document.body.scrollTop + document.documentElement.scrollTop) / (document.documentElement.scrollHeight - document.documentElement.clientHeight);

                        var draw = length * scrollpercent;

                        // Reverse the drawing (when scrolling upwards)
                        triangle.style.strokeDashoffset = length - draw;
                    }
                }
                document = thedoc;
                */
            }

            // https://www.w3schools.com/jsref/prop_element_scrollwidth.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var elmnt = document.getElementById("content");
                    var y = elmnt.scrollHeight;
                    var x = elmnt.scrollWidth;
                }
                {
                    var elmnt = document.getElementById("content");
                    var y = elmnt.scrollHeight;
                    var x = elmnt.scrollWidth;
                }
                {
                    var elmnt = document.getElementById("content");

                    void getFunction()
                    {
                        var x = elmnt.scrollWidth;
                        var y = elmnt.scrollHeight;
                    }

                    void setFunction()
                    {
                        elmnt.style.height = elmnt.scrollHeight + "px";
                        elmnt.style.width = elmnt.scrollWidth + "px";
                    }
                }
            }

            // https://www.w3schools.com/jsref/met_element_setattribute.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    document.getElementsByTagName("H1")[0].setAttribute("class", "democlass");
                }
                {
                    document.getElementsByTagName("INPUT")[0].setAttribute("type", "button");
                }
                {
                    document.getElementById("myAnchor").setAttribute("href", "https://www.w3schools.com");
                }
                {
                    // Get the <a> element with id="myAnchor"
                    var x = document.getElementById("myAnchor");

                    // If the <a> element has a target attribute, set the value to "_self"
                    if (x.hasAttribute("target"))
                    {
                        x.setAttribute("target", "_self");
                    }
                }
            }

            // https://www.w3schools.com/jsref/met_element_setattributenode.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var h1 = document.getElementsByTagName("H1")[0];   // Get the first <h1> element in the document
                    var att = document.createAttribute("class");       // Create a "class" attribute
                    att.value = "democlass";                           // Set the value of the class attribute
                    h1.setAttributeNode(att);                          // Add the class attribute to <h1>
                }
                /*
                {
                    var att = document.createAttribute("href");        // Create a "href" attribute
                    att.value = "https://www.w3schools.com";            // Set the value of the href attribute
                    anchor.setAttributeNode(att);                      // Add the href attribute to <a>
                }
                */
            }

            // https://www.w3schools.com/jsref/prop_html_style.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    document.getElementById("myH1").style.color = "red";
                }
                {
                    var x = document.getElementById("myP").style.borderTop;
                }
            }

            // https://www.w3schools.com/jsref/prop_html_tabindex.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    document.getElementById("myAnchor1").tabIndex = "3";
                    document.getElementById("myAnchor2").tabIndex = "2";
                    document.getElementById("myAnchor3").tabIndex = "1";
                }
                {
                    var x = document.getElementsByTagName("A")[0].tabIndex;
                    document.getElementById("demo").innerHTML = x;
                }
            }

            // https://www.w3schools.com/jsref/prop_element_tagname.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var x = document.getElementById("myP").tagName;
                }
                /*
                {
                    var x = @event.target.tagName;
                }
                */
            }

            // https://www.w3schools.com/jsref/prop_node_textcontent.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var x = document.getElementById("myBtn").textContent;
                }
                {
                    document.getElementById("demo").textContent = "Paragraph changed!";
                }
                {
                    var x = document.getElementById("myList").textContent;
                }
                {
                    document = MakeDocument(@"<p id='demo'>   This element has extra spacing     and contains <span>a span element</span>.</p>");

                    void getInnerText()
                    {
                        alert(document.getElementById("demo").innerText);
                        //Assert.AreSame("This element has extra spacing and contains a span element.");
                    }

                    void getHTML()
                    {
                        alert(document.getElementById("demo").innerHTML);
                        //Assert.AreSame("   This element has extra spacing     and contains <span>a span element</span>.");
                    }

                    void getTextContent()
                    {
                        alert(document.getElementById("demo").textContent);
                        //Assert.AreSame("   This element has extra spacing    and contains a span element.");
                    }
                }
            }

            // https://www.w3schools.com/jsref/prop_html_title.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    var x = document.getElementById("myAbbr").title;
                }
                {
                    document.getElementById("myP").title = "The World's Largest Web Development Site";
                }
            }
        }
    }
}
