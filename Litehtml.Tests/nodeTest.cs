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
                xhttp.onreadystatechange = this_ =>
                {
                    if (this_.readyState == 4 && this_.status == 200)
                    {
                        myFunction(this_);
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
            }

            // https://www.w3schools.com/xml/prop_node_childnodes.asp
            {
                var xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = this_ =>
                {
                    if (this_.readyState == 4 && this_.status == 200)
                    {
                        myFunction(this_);
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
            }

            // https://www.w3schools.com/xml/prop_node_firstchild.asp
            {
                var xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = this_ => {
                    if (this_.readyState == 4 && this_.status == 200)
                    {
                        myFunction(this_);
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
            }
        }
    }
}
