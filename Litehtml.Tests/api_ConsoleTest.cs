using NUnit.Framework;

namespace Litehtml
{
    public class api_ConsoleTest
    {
        static void alert(string message) { }
        static Document MakeDocument(string source) => document.createFromString(source.Replace("'", "\""), new container_test(), null, new context());

        [Test]
        public void Test()
        {
            Document thedoc;
            Console console = null;

            // https://www.w3schools.com/jsref/met_console_assert.asp
            {
                var document = thedoc = MakeDocument(@"
<html>
<body>
<p id='demo'></p>
</body>
</html>");
                {
                    console.assert(document.getElementById("demo"), "You have no element with ID 'demo'");
                }
                {
                    var myObj = new { firstname = "John", lastname = "Doe" };
                    console.assert(document.getElementById("demo"), myObj);
                }
                {
                    var myArr = new[] { "Orange", "Banana", "Mango", "Kiwi" };
                    console.assert(document.getElementById("demo"), myArr);
                }
            }

            // https://www.w3schools.com/jsref/met_console_clear.asp
            {
                {
                    console.clear();
                }
            }

            // https://www.w3schools.com/jsref/met_console_count.asp
            {
                {
                    int i;
                    for (i = 0; i < 10; i++)
                    {
                        console.count();
                    }
                }
                {
                    console.count();
                    console.count();
                }
                {
                    console.count("");
                    console.count("");
                }
                {
                    console.count("myLabel");
                    console.count("myLabel");
                }
            }

            // https://www.w3schools.com/jsref/met_console_error.asp
            {
                {
                    console.error("You made a mistake");
                }
                {
                    var myObj = new { firstname = "John", lastname = "Doe" };
                    console.error(myObj);
                }
                {
                    var myArr = new[] { "Orange", "Banana", "Mango", "Kiwi" };
                    console.error(myArr);
                }
            }

            // https://www.w3schools.com/jsref/met_console_group.asp
            {
                {
                    console.log("Hello world!");
                    console.group();
                    console.log("Hello again, this time inside a group!");
                }
                {
                    console.log("Hello world!");
                    console.group();
                    console.log("Hello again, this time inside a group!");
                    console.groupEnd();
                    console.log("and we are back.");
                }
                {
                    console.log("Hello world!");
                    console.group("myLabel");
                    console.log("Hello again, this time inside a group, with a label!");
                }
            }

            // https://www.w3schools.com/jsref/met_console_groupcollapsed.asp
            {
                {
                    console.log("Hello world!");
                    console.groupCollapsed();
                    console.log("Hello again, this time inside a collapsed group!");
                }
                {
                    console.log("Hello world!");
                    console.groupCollapsed();
                    console.log("Hello again, this time inside a collapsed group!");
                    console.groupEnd();
                    console.log("and we are back.");
                }
                {
                    console.log("Hello world!");
                    console.groupCollapsed("myLabel");
                    console.log("Hello again, this time inside a collapsed group, with a label!");
                }
            }

            // https://www.w3schools.com/jsref/met_console_groupend.asp
            {
                {
                    console.log("Hello world!");
                    console.group();
                    console.log("Hello again, this time inside a group!");
                    console.groupEnd();
                    console.log("and we are back.");
                }
            }

            // https://www.w3schools.com/jsref/met_console_info.asp
            {
                {
                    console.info("Hello world!");
                }
                {
                    var myObj = new { firstname = "John", lastname = "Doe" };
                    console.info(myObj);
                }
                {
                    var myArr = new[] { "Orange", "Banana", "Mango", "Kiwi" };
                    console.info(myArr);
                }
            }

            // https://www.w3schools.com/jsref/met_console_log.asp
            {
                {
                    console.log("Hello world!");
                }
                {
                    var myObj = new { firstname = "John", lastname = "Doe" };
                    console.log(myObj);
                }
                {
                    var myArr = new[] { "Orange", "Banana", "Mango", "Kiwi" };
                    console.log(myArr);
                }
            }

            // https://www.w3schools.com/jsref/met_console_table.asp
            {
                {
                    console.table(new[] { "Audi", "Volvo", "Ford" });
                }
                {
                    console.table(new { firstname = "John", lastname = "Doe" });
                }
                {
                    var car1 = new { name = "Audi", model = "A4" };
                    var car2 = new { name = "Volvo", model = "XC90" };
                    var car3 = new { name = "Ford", model = "Fusion" };

                    console.table(new[] { car1, car2, car3 });
                }
                {
                    var car1 = new { name = "Audi", model = "A4" };
                    var car2 = new { name = "Volvo", model = "XC90" };
                    var car3 = new { name = "Ford", model = "Fusion" };

                    console.table(new[] { car1, car2, car3 }, new[] { "model" });
                }
            }

            // https://www.w3schools.com/jsref/met_console_time.asp
            {
                {
                    console.time();
                    for (int i = 0; i < 100000; i++)
                    {
                        // some code
                    }
                    console.timeEnd();
                }
                {
                    console.time("test1");
                    for (int i = 0; i < 100000; i++)
                    {
                        // some code
                    }
                    console.timeEnd("test1");
                }
                {
                    int i;
                    console.time("test for loop");
                    for (i = 0; i < 100000; i++)
                    {
                        // some code
                    }
                    console.timeEnd("test for loop");

                    i = 0;
                    console.time("test while loop");
                    while (i < 1000000)
                    {
                        i++;
                    }
                    console.timeEnd("test while loop");
                }
            }

            // https://www.w3schools.com/jsref/met_console_timeend.asp
            {
                {
                    console.time();
                    for (int i = 0; i < 100000; i++)
                    {
                        // some code
                    }
                    console.timeEnd();
                }
                {
                    console.time("test1");
                    for (int i = 0; i < 100000; i++)
                    {
                        // some code
                    }
                    console.timeEnd("test1");
                }
                {
                    int i;
                    console.time("test for loop");
                    for (i = 0; i < 100000; i++)
                    {
                        // some code
                    }
                    console.timeEnd("test for loop");

                    i = 0;
                    console.time("test while loop");
                    while (i < 1000000)
                    {
                        i++;
                    }
                    console.timeEnd("test while loop");
                }
            }

            // https://www.w3schools.com/jsref/met_console_trace.asp
            {
                {
                    void myFunction()
                    {
                        myOtherFunction();
                    }

                    void myOtherFunction()
                    {
                        console.trace();
                    }

                    myFunction();
                }
            }

            // https://www.w3schools.com/jsref/met_console_warn.asp
            {
                {
                    console.warn("This is a warning!");
                }
                {
                    var myObj = new { firstname = "John", lastname = "Doe" };
                    console.warn(myObj);
                }
                {
                    var myArr = new[] { "Orange", "Banana", "Mango", "Kiwi" };
                    console.warn(myArr);
                }
            }
        }
    }
}
