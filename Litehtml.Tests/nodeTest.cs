using NUnit.Framework;

namespace Litehtml
{
    public class nodeTest
    {
        static Document MakeDocument(string source) => document.createFromString(source.Replace("'", "\""), new container_test(), null, new context());

        [Test]
        public void AttribTest()
        {
            var document = MakeDocument(@"
<html>
<body>
    <button onclick='myFunction()'>Try it</button>
    <p id='demo'></p>
    <div class='w3-code notranslate'>
        <img src='pic_bulboff.gif' width='100' height='180'>
    </div>
</body>
</html>");

            // https://www.w3schools.com/jsref/prop_attr_name.asp
            {
                var x = document.getElementsByTagName("BUTTON")[0].attributes[0].name;
                Assert.AreEqual("onclick", x);
            }

            // https://www.w3schools.com/jsref/prop_attr_value.asp
            {
                var x = document.getElementsByTagName("BUTTON")[0].attributes[0].value;
                Assert.AreEqual("myFunction()", x);
            }
            {
                var x = document.getElementsByTagName("IMG")[0] as Element;
                x.getAttributeNode("src").value = "pic_bulbon.gif";
                Assert.AreEqual("pic_bulbon.gif", x.getAttributeNode("src").value);
            }

            // https://www.w3schools.com/jsref/prop_attr_specified.asp
            {
                var x = document.getElementById("demo").attributes[0].specified;
                Assert.IsTrue(x);
            }
        }

        [Test]
        public void NodemapTest()
        {
            var document = MakeDocument(@"
<html>
<body>
    <h1>Hello World</h1>
    <input type='button' value='OK' />
    <img id='myImg' alt='Flower' src='klematis.jpg' width='150' height='113'>
    <button id='myBtn' onclick='myFunction()' class='example'>Try it</button>
    <p id='demo'></p>
</body>
</html>");

            // https://www.w3schools.com/jsref/met_namednodemap_getnameditem.asp
            {
                var btn = (Element)document.getElementsByTagName("BUTTON")[0];
                var x = btn.attributes.getNamedItem("onclick").value;
                Assert.AreEqual("myFunction()", x);
            }

            // https://www.w3schools.com/jsref/met_namednodemap_item.asp
            {
                var x = document.getElementsByTagName("BUTTON")[0].attributes.item(0).nodeName;
                Assert.AreEqual("id", x);
            }
            {
                var x = document.getElementsByTagName("BUTTON")[0].attributes.item(1);   // The 2nd attribute
                Assert.IsNotNull(x);
            }
            {
                var x = document.getElementsByTagName("BUTTON")[0].attributes[1];        // The 2nd attribute
                Assert.IsNotNull(x);
            }
            {
                document.getElementsByTagName("BUTTON")[0].attributes[1].value = "newClass";
            }

            // https://www.w3schools.com/jsref/prop_namednodemap_length.asp
            {
                var x = document.getElementsByTagName("BUTTON")[0].attributes.length;
                Assert.AreEqual(3, x);
            }
            {
                var txt = "";
                var x = document.getElementById("myBtn").attributes;

                for (var i = 0; i < x.length; i++)
                {
                    txt += "Attribute name: " + x[i].name + "<br>";
                }
                Assert.AreEqual("Attribute name: id<br>Attribute name: onclick<br>Attribute name: class<br>", txt);
            }
            {
                var x = document.getElementById("myImg").attributes.length;
                Assert.AreEqual(5, x);
            }
            {
                var txt = "";
                var x = document.getElementById("myImg");

                for (var i = 0; i < x.attributes.length; i++)
                {
                    txt = txt + x.attributes[i].name + " = " + x.attributes[i].value + "<br>";
                }
                Assert.AreEqual("id = myImg<br>alt = Flower<br>src = klematis.jpg<br>width = 150<br>height = 113<br>", txt);
            }

            // https://www.w3schools.com/jsref/met_namednodemap_removenameditem.asp
            {
                var btn = document.getElementsByTagName("INPUT")[0];
                btn.attributes.removeNamedItem("type");
            }

            // https://www.w3schools.com/jsref/met_namednodemap_setnameditem.asp
            {
                var h = document.getElementsByTagName("H1")[0];
                var typ = document.createAttribute("class");
                //typ.value = "democlass";
                //h.attributes.setNamedItem(typ);
            }
        }

        [Test]
        public void PropertyTest()
        {
            var document = MakeDocument(@"
<html>
<body>
    <p id='demo'></p>
</body>
</html>");

            // https://www.w3schools.com/xml/prop_node_baseuri.asp
            {
                var xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = @this =>
                {
                    if (@this.readyState == 4 && @this.status == 200)
                    {
                        myFunction(@this);
                    }
                };
                xhttp.open("GET", "books_ns.xml", true);
                xhttp.send();

                void myFunction(XMLHttpRequest xml)
                {
                    NodeList x; int i; Document xmlDoc; string txt;
                    xmlDoc = xml.responseXML;
                    txt = "";
                    x = xmlDoc.getElementsByTagName("title");
                    for (i = 0; i < x.length; i++)
                    {
                        txt += "Base URI: " + x.item(i).baseURI + "<br>";
                    }
                    document.getElementById("demo").innerHTML = txt;
                }

                //wait
                Assert.AreEqual(
@"Base URI: https://www.w3schools.com/xml/books_ns.xml
Base URI: https://www.w3schools.com/xml/books_ns.xml
".Replace("\n", "<br>"), document.getElementById("demo").innerHTML);
            }

            // https://www.w3schools.com/xml/prop_node_childnodes.asp
            {
                var xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = @this =>
                {
                    if (@this.readyState == 4 && @this.status == 200)
                    {
                        myFunction(@this);
                    }
                };
                xhttp.open("GET", "books.xml", true);
                xhttp.send();

                void myFunction(XMLHttpRequest xml)
                {
                    NodeList x; int i; Document xmlDoc; string txt;
                    xmlDoc = xml.responseXML;
                    txt = "";
                    x = xmlDoc.childNodes;
                    for (i = 0; i < x.length; i++)
                    {
                        txt += "Nodename: " + x[i].nodeName +
                        " (nodetype: " + x[i].nodeType + ")";
                    }
                    document.getElementById("demo").innerHTML = txt;
                }

                //wait
                Assert.AreEqual("Nodename: bookstore (nodetype: 1)", document.getElementById("demo").innerHTML);
            }

            // https://www.w3schools.com/xml/prop_node_firstchild.asp
            {
                var xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = @this =>
                {
                    if (@this.readyState == 4 && @this.status == 200)
                    {
                        myFunction(@this);
                    }
                };
                xhttp.open("GET", "books.xml", true);
                xhttp.send();

                // Check if the first node is an element node
                Node get_firstchild(Document n)
                {
                    var x = n.firstChild;
                    while (x.nodeType != 1)
                    {
                        x = x.nextSibling;
                    }
                    return x;
                }

                void myFunction(XMLHttpRequest xml)
                {
                    var xmlDoc = xml.responseXML;
                    var x = get_firstchild(xmlDoc);
                    document.getElementById("demo").innerHTML =
                    "Nodename: " + x.nodeName +
                    " (nodetype: " + x.nodeType + ")<br>";
                }

                //wait
                Assert.AreEqual("Nodename: bookstore (nodetype: 1)", document.getElementById("demo").innerHTML);
            }

            // https://www.w3schools.com/xml/prop_node_lastchild.asp
            {
                var xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = @this =>
                {
                    if (@this.readyState == 4 && @this.status == 200)
                    {
                        myFunction(@this);
                    }
                };
                xhttp.open("GET", "books.xml", true);
                xhttp.send();

                // Check if the last node is an element node
                Node get_lastchild(Document n)
                {
                    var x = n.lastChild;
                    while (x.nodeType != 1)
                    {
                        x = x.previousSibling;
                    }
                    return x;
                }

                void myFunction(XMLHttpRequest xml)
                {
                    var xmlDoc = xml.responseXML;
                    var x = get_lastchild(xmlDoc);
                    document.getElementById("demo").innerHTML =
                    "Nodename: " + x.nodeName +
                    " (nodetype: " + x.nodeType + ")<br>";
                }

                //wait
                Assert.AreEqual("Nodename: bookstore (nodetype: 1)", document.getElementById("demo").innerHTML);
            }

            // https://www.w3schools.com/xml/prop_node_nextsibling.asp
            {
                var xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = @this =>
                {
                    if (@this.readyState == 4 && @this.status == 200)
                    {
                        myFunction(@this);
                    }
                };
                xhttp.open("GET", "books.xml", true);
                xhttp.send();

                // Check if the next sibling node is an element node
                Node get_nextsibling(Node n)
                {
                    var x = n.nextSibling;
                    while (x.nodeType != 1)
                    {
                        x = x.nextSibling;
                    }
                    return x;
                }

                void myFunction(XMLHttpRequest xml)
                {
                    var xmlDoc = xml.responseXML;
                    var x = xmlDoc.getElementsByTagName("title")[0];
                    var y = get_nextsibling(x);
                    document.getElementById("demo").innerHTML = x.nodeName + " = " +
                    x.childNodes[0].nodeValue +
                    "<br>Next sibling: " + y.nodeName + " = " +
                    y.childNodes[0].nodeValue;
                }

                //wait
                Assert.AreEqual(
@"title = Everyday Italian
Next sibling: author = Giada De Laurentiis".Replace("\n", "<br>"), document.getElementById("demo").innerHTML);
            }

            // https://www.w3schools.com/xml/prop_node_nodename.asp
            {
                var xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = @this =>
                {
                    if (@this.readyState == 4 && @this.status == 200)
                    {
                        myFunction(@this);
                    }
                };
                xhttp.open("GET", "books.xml", true);
                xhttp.send();

                void myFunction(XMLHttpRequest xml)
                {
                    var xmlDoc = xml.responseXML;
                    document.getElementById("demo").innerHTML =
                    "Nodename: " + xmlDoc.nodeName +
                    " (nodetype: " + xmlDoc.nodeType + ")";
                }

                //wait
                Assert.AreEqual("Nodename: #document (nodetype: 9)", document.getElementById("demo").innerHTML);
            }

            // https://www.w3schools.com/xml/prop_node_nodetype.asp
            {
                var xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = @this =>
                {
                    if (@this.readyState == 4 && @this.status == 200)
                    {
                        myFunction(@this);
                    }
                };
                xhttp.open("GET", "books.xml", true);
                xhttp.send();

                void myFunction(XMLHttpRequest xml)
                {
                    var xmlDoc = xml.responseXML;
                    document.getElementById("demo").innerHTML =
                    "Nodename: " + xmlDoc.nodeName +
                    " (nodetype: " + xmlDoc.nodeType + ")";
                }

                //wait
                Assert.AreEqual("Nodename: #document (nodetype: 9)", document.getElementById("demo").innerHTML);
            }

            // https://www.w3schools.com/xml/prop_node_nodevalue.asp
            {
                var xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = @this =>
                {
                    if (@this.readyState == 4 && @this.status == 200)
                    {
                        myFunction(@this);
                    }
                };
                xhttp.open("GET", "books.xml", true);
                xhttp.send();

                void myFunction(XMLHttpRequest xml)
                {
                    var xmlDoc = xml.responseXML;
                    document.getElementById("demo").innerHTML =
                    "Nodename: " + xmlDoc.nodeName +
                    (" (value: " + xmlDoc.childNodes[0].nodeValue) + ")";
                }

                //wait
                Assert.AreEqual("Nodename: #document (value: null)", document.getElementById("demo").innerHTML);
            }

            // https://www.w3schools.com/xml/prop_node_ownerdocument.asp
            {
                var xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = @this =>
                {
                    if (@this.readyState == 4 && @this.status == 200)
                    {
                        myFunction(@this);
                    }
                };
                xhttp.open("GET", "books.xml", true);
                xhttp.send();

                void myFunction(XMLHttpRequest xml)
                {
                    var xmlDoc = xml.responseXML;
                    var x = xmlDoc.getElementsByTagName("title")[0].ownerDocument;
                    document.getElementById("demo").innerHTML =
                    "Nodename: " + x.nodeName +
                    " (nodetype: " + x.nodeType + ")";
                }

                //wait
                Assert.AreEqual("Nodename: #document (nodetype: 9)", document.getElementById("demo").innerHTML);
            }

            // https://www.w3schools.com/xml/prop_node_parentnode.asp
            {
                var xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = @this =>
                {
                    if (@this.readyState == 4 && @this.status == 200)
                    {
                        myFunction(@this);
                    }
                };
                xhttp.open("GET", "books.xml", true);
                xhttp.send();

                void myFunction(XMLHttpRequest xml)
                {
                    var xmlDoc = xml.responseXML;
                    var x = xmlDoc.getElementsByTagName("title")[0];
                    document.getElementById("demo").innerHTML =
                    "Parent node: " + x.parentNode.nodeName;
                }

                //wait
                Assert.AreEqual("Parent node: book", document.getElementById("demo").innerHTML);
            }

            // https://www.w3schools.com/xml/prop_node_prefix.asp
            {
                var xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = @this =>
                {
                    if (@this.readyState == 4 && @this.status == 200)
                    {
                        myFunction(@this);
                    }
                };
                xhttp.open("GET", "books_ns.xml", true);
                xhttp.send();

                void myFunction(XMLHttpRequest xml)
                {
                    NodeList x; int i; Document xmlDoc; string txt;
                    xmlDoc = xml.responseXML;
                    txt = "";
                    x = xmlDoc.getElementsByTagName("title");
                    for (i = 0; i < x.length; i++)
                    {
                        txt += "Prefix: " + x.item(i).prefix + "<br>";
                    }
                    document.getElementById("demo").innerHTML = txt;
                }

                //wait
                Assert.AreEqual(
@"Prefix: c
Prefix: x
".Replace("\n", "<br>"), document.getElementById("demo").innerHTML);
            }

            // https://www.w3schools.com/xml/prop_node_previoussibling.asp
            {
                var xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = @this =>
                {
                    if (@this.readyState == 4 && @this.status == 200)
                    {
                        myFunction(@this);
                    }
                };
                xhttp.open("GET", "books.xml", true);
                xhttp.send();

                // Check if the previous sibling node is an element node
                Node get_previoussibling(Node n)
                {
                    var x = n.previousSibling;
                    while (x.nodeType != 1)
                    {
                        x = x.previousSibling;
                    }
                    return x;
                }

                void myFunction(XMLHttpRequest xml)
                {
                    var xmlDoc = xml.responseXML;
                    var x = xmlDoc.getElementsByTagName("author")[0];
                    var y = get_previoussibling(x);
                    document.getElementById("demo").innerHTML = x.nodeName + " = " +
                    x.childNodes[0].nodeValue +
                    "<br>Previous sibling: " + y.nodeName + " = " +
                    y.childNodes[0].nodeValue;
                }

                //wait
                Assert.AreEqual(
@"author = Giada De Laurentiis
Previous sibling: title = Everyday Italian".Replace("\n", "<br>"), document.getElementById("demo").innerHTML);
            }

            // https://www.w3schools.com/xml/prop_node_textcontent.asp
            {
                var xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = @this =>
                {
                    if (@this.readyState == 4 && @this.status == 200)
                    {
                        myFunction(@this);
                    }
                };
                xhttp.open("GET", "books.xml", true);
                xhttp.send();

                void myFunction(XMLHttpRequest xml)
                {
                    NodeList x; int i; Document xmlDoc; string txt;
                    xmlDoc = xml.responseXML;
                    txt = "";
                    x = xmlDoc.getElementsByTagName("book");
                    for (i = 0; i < x.length; i++)
                    {
                        txt += x.item(i).textContent + "<br>";
                    }
                    document.getElementById("demo").innerHTML = txt;
                }

                //wait
                Assert.AreEqual(
@"Everyday Italian Giada De Laurentiis 2005 30.00
Harry Potter J K.Rowling 2005 29.99
XQuery Kick Start James McGovern Per Bothner Kurt Cagle James Linn
Vaidyanathan Nagarajan 2003 49.99
Learning XML Erik T.Ray 2003 39.95
".Replace("\n", "<br>"), document.getElementById("demo").innerHTML);
            }
        }

        [Test]
        public void MethodTest()
        {
            var document = MakeDocument(@"
<html>
<body>
    <p id='demo'></p>
</body>
</html>");

            // https://www.w3schools.com/xml/met_node_appendchild.asp
            {
                var xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = @this =>
                {
                    if (@this.readyState == 4 && @this.status == 200)
                    {
                        myFunction(@this);
                    }
                };
                xhttp.open("GET", "books.xml", true);
                xhttp.send();

                void myFunction(XMLHttpRequest xml)
                {
                    var xmlDoc = xml.responseXML;
                    var newel = xmlDoc.createElement("edition");
                    var x = (Element)xmlDoc.getElementsByTagName("book")[0];
                    x.appendChild(newel);
                    document.getElementById("demo").innerHTML =
                    x.getElementsByTagName("edition")[0].nodeName;
                }

                //wait
                Assert.AreEqual("edition", document.getElementById("demo").innerHTML);
            }

            // https://www.w3schools.com/xml/met_node_clonenode.asp
            {
                var xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = @this =>
                {
                    if (@this.readyState == 4 && @this.status == 200)
                    {
                        myFunction(@this);
                    }
                };
                xhttp.open("GET", "books.xml", true);
                xhttp.send();

                void myFunction(XMLHttpRequest xml)
                {
                    Node x; NodeList y; Node cloneNode; int i; Document xmlDoc; string txt;
                    xmlDoc = xml.responseXML;
                    txt = "";
                    x = xmlDoc.getElementsByTagName("book")[0];
                    cloneNode = x.cloneNode(true);
                    xmlDoc.documentElement.appendChild(cloneNode);

                    // Output all titles
                    y = xmlDoc.getElementsByTagName("title");
                    for (i = 0; i < y.length; i++)
                    {
                        txt += y[i].childNodes[0].nodeValue + "<br>";
                    }
                    document.getElementById("demo").innerHTML = txt;
                }

                //wait
                Assert.AreEqual(
@"Everyday Italian
Harry Potter
XQuery Kick Start
Learning XML
Everyday Italian
".Replace("\n", "<br>"), document.getElementById("demo").innerHTML);
            }

            // https://www.w3schools.com/xml/met_node_comparedocumentposition.asp
            {
                var xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = @this =>
                {
                    if (@this.readyState == 4 && @this.status == 200)
                    {
                        myFunction(@this);
                    }
                };
                xhttp.open("GET", "books.xml", true);
                xhttp.send();

                void myFunction(XMLHttpRequest xml)
                {
                    var xmlDoc = xml.responseXML;
                    var x = xmlDoc.getElementsByTagName("book")[0];
                    var y = xmlDoc.getElementsByTagName("book")[2];
                    document.getElementById("demo").innerHTML =
                    x.compareDocumentPosition(y).ToString();
                }

                //wait
                Assert.AreEqual("4", document.getElementById("demo").innerHTML);
            }

            // https://www.w3schools.com/xml/met_node_hasattributes.asp
            {
                var xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = @this =>
                {
                    if (@this.readyState == 4 && @this.status == 200)
                    {
                        myFunction(@this);
                    }
                };
                xhttp.open("GET", "books.xml", true);
                xhttp.send();

                void myFunction(XMLHttpRequest xml)
                {
                    var xmlDoc = xml.responseXML;
                    var x = xmlDoc.getElementsByTagName("book")[0];
                    document.getElementById("demo").innerHTML =
                    x.hasAttributes() ? "true" : "false";
                }

                //wait
                Assert.AreEqual("true", document.getElementById("demo").innerHTML);
            }

            // https://www.w3schools.com/xml/met_node_haschildnodes.asp
            {
                var xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = @this =>
                {
                    if (@this.readyState == 4 && @this.status == 200)
                    {
                        myFunction(@this);
                    }
                };
                xhttp.open("GET", "books.xml", true);
                xhttp.send();

                void myFunction(XMLHttpRequest xml)
                {
                    var xmlDoc = xml.responseXML;
                    var x = xmlDoc.getElementsByTagName("book")[0];
                    document.getElementById("demo").innerHTML =
                    x.hasChildNodes() ? "true" : "false";
                }

                //wait
                Assert.AreEqual("true", document.getElementById("demo").innerHTML);
            }

            // https://www.w3schools.com/xml/met_node_insertbefore.asp
            {
                var xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = @this =>
                {
                    if (@this.readyState == 4 && @this.status == 200)
                    {
                        myFunction(@this);
                    }
                };
                xhttp.open("GET", "books.xml", true);
                xhttp.send();

                void myFunction(XMLHttpRequest xml)
                {
                    var xmlDoc = xml.responseXML;
                    var newNode = xmlDoc.createElement("book");
                    var x = xmlDoc.documentElement;
                    var y = xmlDoc.getElementsByTagName("book");
                    document.getElementById("demo").innerHTML =
                    "Book elements before: " + y.length + "<br>";

                    x.insertBefore(newNode, y[3]);
                    document.getElementById("demo").innerHTML +=
                    "Book elements after: " + y.length;
                }

                //wait
                Assert.AreEqual(
@"Book elements before: 4
Book elements after: 5".Replace("\n", "<br>"), document.getElementById("demo").innerHTML);
            }

            // isDefaultNamespace(URI)
            {
                // none
            }

            // https://www.w3schools.com/xml/met_node_isequalnode.asp
            {
                var xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = @this =>
                {
                    if (@this.readyState == 4 && @this.status == 200)
                    {
                        myFunction(@this);
                    }
                };
                xhttp.open("GET", "books.xml", true);
                xhttp.send();

                void myFunction(XMLHttpRequest xml)
                {
                    var xmlDoc = xml.responseXML;
                    var x = xmlDoc.getElementsByTagName("book")[0];
                    var y = xmlDoc.getElementsByTagName("book")[2];
                    document.getElementById("demo").innerHTML =
                    x.isEqualNode(y) ? "true" : "false";
                }

                //wait
                Assert.AreEqual("true", document.getElementById("demo").innerHTML);
            }

            // https://www.w3schools.com/xml/met_node_issamenode.asp
            {
                var xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = @this =>
                {
                    if (@this.readyState == 4 && @this.status == 200)
                    {
                        myFunction(@this);
                    }
                };
                xhttp.open("GET", "books.xml", true);
                xhttp.send();

                void myFunction(XMLHttpRequest xml)
                {
                    var xmlDoc = xml.responseXML;
                    var x = xmlDoc.getElementsByTagName("book")[1];
                    var y = xmlDoc.getElementsByTagName("book")[1];
                    document.getElementById("demo").innerHTML =
                    x.isSameNode(y) ? "true" : "false";
                }

                //wait
                Assert.AreEqual("true", document.getElementById("demo").innerHTML);
            }

            // https://www.w3schools.com/xml/met_node_lookupnamespaceuri.asp
            {
                var xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = @this =>
                {
                    if (@this.readyState == 4 && @this.status == 200)
                    {
                        myFunction(@this);
                    }
                };
                xhttp.open("GET", "books_ns.xml", true);
                xhttp.send();

                void myFunction(XMLHttpRequest xml)
                {
                    var xmlDoc = xml.responseXML;
                    var x = xmlDoc.getElementsByTagName("book")[0];
                    document.getElementById("demo").innerHTML =
                    x.lookupNamespaceURI("c");
                }

                //wait
                Assert.AreEqual("https://www.w3schools.com/children/", document.getElementById("demo").innerHTML);
            }

            // https://www.w3schools.com/xml/met_node_lookupprefix.asp
            {
                var xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = @this =>
                {
                    if (@this.readyState == 4 && @this.status == 200)
                    {
                        myFunction(@this);
                    }
                };
                xhttp.open("GET", "books_ns.xml", true);
                xhttp.send();

                void myFunction(XMLHttpRequest xml)
                {
                    var xmlDoc = xml.responseXML;
                    var x = xmlDoc.getElementsByTagName("book")[0];
                    document.getElementById("demo").innerHTML =
                    x.lookupPrefix("https://www.w3schools.com/children/");
                }

                //wait
                Assert.AreEqual("c", document.getElementById("demo").innerHTML);
            }

            // https://www.w3schools.com/xml/met_node_normalize.asp
            {
                // none
            }

            // https://www.w3schools.com/xml/met_node_removechild.asp
            {
                var xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = @this =>
                {
                    if (@this.readyState == 4 && @this.status == 200)
                    {
                        myFunction(@this);
                    }
                };
                xhttp.open("GET", "books.xml", true);
                xhttp.send();

                void myFunction(XMLHttpRequest xml)
                {
                    var xmlDoc = xml.responseXML;
                    var root = xmlDoc.documentElement;
                    var currNode = root.childNodes[1];
                    var removedNode = currNode.removeChild(currNode.childNodes[1]);
                    document.getElementById("demo").innerHTML =
                    "Removed node: " + removedNode.nodeName;
                }

                //wait
                Assert.AreEqual("Removed node: title", document.getElementById("demo").innerHTML);
            }

            // https://www.w3schools.com/xml/met_node_replacechild.asp
            {
                var xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = @this =>
                {
                    if (@this.readyState == 4 && @this.status == 200)
                    {
                        myFunction(@this);
                    }
                };
                xhttp.open("GET", "books.xml", true);
                xhttp.send();

                void myFunction(XMLHttpRequest xml)
                {
                    Element x; Node y; NodeList z; int i; Element newNode; Element newTitle; Node newText; Document xmlDoc; string txt;
                    xmlDoc = xml.responseXML;
                    txt = "";
                    x = xmlDoc.documentElement;

                    // Create a book element, title element and a text node
                    newNode = xmlDoc.createElement("book");
                    newTitle = xmlDoc.createElement("title");
                    newText = xmlDoc.createTextNode("A Notebook");

                    // Add a text node to the title node
                    newTitle.appendChild(newText);

                    // Add the title node to the book node
                    newNode.appendChild(newTitle);

                    y = xmlDoc.getElementsByTagName("book")[0];

                    // Replace the first book node with the new book node
                    x.replaceChild(newNode, y);

                    z = xmlDoc.getElementsByTagName("title");
                    // Output all titles
                    for (i = 0; i < z.length; i++)
                    {
                        txt += z[i].childNodes[0].nodeValue + "<br>";
                    }
                    document.getElementById("demo").innerHTML = txt;
                }

                //wait
                Assert.AreEqual(
@"A Notebook
Harry Potter
XQuery Kick Start
Learning XML
", document.getElementById("demo").innerHTML);
            }
        }
    }
}
