using Litehtml;
using Litehtml.Containers;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Browser.Unity
{
    public class ConsoleControl : container_unity
    {
        readonly Window _window = new Window();
        readonly context _context = new context();
        document _doc;
        string _cursor;
        Vector3 _size;

        protected virtual void Start()
        {
            _size = new Vector3(500, 500, 10);
        }

        void render_console(int width)
        {
            if (_doc == null)
                return;
            _doc.render(width);
        }

        void update_cursor()
        {
            //var defArrow = Cursors.Default;
            //if (_cursor == "pointer") Cursor = Cursors.Hand;
            //else if (_cursor == "text") Cursor = Cursors.IBeam;
            //else Cursor = defArrow;
        }

        protected override void OnPaint()
        {
            var cr = gameObject;
            var pos = new position
            {
                width = (int)_size.x,
                height = (int)_size.y,
                depth = (int)_size.z,
                x = 0,
                y = 0,
                z = 0,
            };
            if (_doc != null)
                _doc.draw(cr, 0, 0, 0, pos);
        }

        public void create()
        {
            _context.load_master_stylesheet("html,div,body { display: block; } head,style { display: none; }");
            string html;
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Browser.Unity.console.console.html"))
            using (var reader = new StreamReader(stream))
                html = reader.ReadToEnd();
            _doc = document.createFromString(html, this, new DefaultScriptEngine(_window), _context);
            render_console((int)_size.x);
        }

        protected override object get_image(string url, Dictionary<string, string> attrs, bool redraw_on_ready)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"Browser.Unity.console.{url}"))
            {
                var m = new MemoryStream();
                stream.CopyTo(m);
                var tex = new Texture2D(1, 1);
                var loaded = tex.LoadImage(m.ToArray());
                if (!loaded)
                    Debug.Log("get_image: !loaded");
                return tex;
            }
        }

        public int set_width(int width)
        {
            if (_doc == null)
                return 0;
            render_console(width);
            return _doc.height;
        }

        public void on_page_loaded(string url)
        {
        }

        public override void set_cursor(string cursor) => _cursor = cursor;

        public override void get_client_rect(out position client) => client = new position
        {
            width = (int)_size.x,
            height = (int)_size.y,
            depth = (int)_size.z,
            x = 0,
            y = 0,
            z = 0,
        };
    }
}
