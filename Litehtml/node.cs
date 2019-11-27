using System;
using System.Collections.Generic;

namespace Litehtml
{
    /// <summary>
    /// node
    /// </summary>
    /// <seealso cref="Litehtml.Node" />
    public class node : Node
    {
        readonly element _t;
        public node() => _t = (element)this;

        public Node appendChild(Node node) => throw new NotImplementedException();
        public NamedNodeMap attributes => new NamedNodeMap(((html_tag)_t)._attrs);
        public string baseURI => "base";
        public NodeList childNodes => new NodeList(_t._children);
        public Node cloneNode(bool deep = false) => throw new NotImplementedException();
        public int compareDocumentPosition(Node node) => throw new NotImplementedException();
        public Node firstChild => _t._children[0];
        public bool hasChildNodes() => _t._children.Count > 0;
        public bool isDefaultNamespace(string namespaceURI) => throw new NotImplementedException();
        public bool isEqualNode(Node node) => throw new NotImplementedException();
        public bool isSameNode(Node node) => throw new NotImplementedException();
        public Node lastChild => _t._children.Count > 0 ? _t._children[_t._children.Count - 1] : null;
        public string lookupNamespaceURI(string prefix) => throw new NotImplementedException();
        public string lookupPrefix(string namespaceURI) => throw new NotImplementedException();
        public Node nextSibling => throw new NotImplementedException();
        public string nodeName => throw new NotImplementedException();
        public int nodeType => throw new NotImplementedException();
        public string nodeValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public void normalize() => throw new NotImplementedException();
        public Document ownerDocument => throw new NotImplementedException();
        public Node parentNode => throw new NotImplementedException();
        public string prefix { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Node previousSibling => throw new NotImplementedException();
        public Node removeChild(Node node) => throw new NotImplementedException();
        public Node replaceChild(Node newnode, Node oldnode) => throw new NotImplementedException();
        public string textContent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    /// <summary>
    /// attr
    /// </summary>
    public class attr : Attr
    {
        readonly Dictionary<string, string> _attrs;
        readonly string _name;

        public attr(Dictionary<string, string> attrs, string name)
        {
            _attrs = attrs;
            _name = name;
        }
        public Node appendChild(Node node) => throw new NotSupportedException();
        public NamedNodeMap attributes => NamedNodeMap.Empty;
        public string baseURI => "base";
        public NodeList childNodes => new NodeList();
        public Node cloneNode(bool deep = false) => throw new NotImplementedException();
        public int compareDocumentPosition(Node node) => throw new NotImplementedException();
        public Node firstChild => null;
        public bool hasChildNodes() => throw new NotImplementedException();
        public bool isDefaultNamespace(string namespaceURI) => throw new NotImplementedException();
        public bool isEqualNode(Node node) => throw new NotImplementedException();
        public bool isSameNode(Node node) => throw new NotImplementedException();
        public Node lastChild => throw new NotImplementedException();
        public string lookupNamespaceURI(string prefix) => throw new NotImplementedException();
        public string lookupPrefix(string namespaceURI) => throw new NotImplementedException();
        public Node nextSibling => null;
        public string nodeName => throw new NotImplementedException();
        public int nodeType => throw new NotImplementedException();
        public string nodeValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public void normalize() => throw new NotImplementedException();
        public Document ownerDocument => throw new NotImplementedException();
        public Node parentNode => throw new NotImplementedException();
        public string prefix { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Node previousSibling => null;
        public Node removeChild(Node node) => throw new NotImplementedException();
        public Node replaceChild(Node newnode, Node oldnode) => throw new NotImplementedException();
        public string textContent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //
        public string name => _name;
        public string value { get => _attrs[_name]; set => _attrs[_name] = value; }
        public bool specified => _attrs[_name] != null;
    }

    /// <summary>
    /// element
    /// </summary>
    partial class element : Element
    {
        public bool dispatchMouseEvent(PlatformMouseEvent platformEvent, string eventType, int detail, element relatedTarget)
        {
            var mouseEvent = new MouseEvent(eventType, _doc.windowProxy, platformEvent, detail, relatedTarget);
            var didNotSwallowEvent = true;
            //    //Debug.Assert(mouseEvent.target == null || mouseEvent.target != relatedTarget);
            //    dispatchEvent(mouseEvent);
            //    if (mouseEvent.defaultPrevented || mouseEvent.defaultHandled)
            //        didNotSwallowEvent = false;

            //    //if (mouseEvent->type() == eventNames().clickEvent && mouseEvent->detail() == 2)
            //    //{
            //    //    // Special case: If it's a double click event, we also send the dblclick event. This is not part
            //    //    // of the DOM specs, but is used for compatibility with the ondblclick="" attribute. This is treated
            //    //    // as a separate event in other DOM-compliant browsers like Firefox, and so we do the same.
            //    //    // FIXME: Is it okay that mouseEvent may have been mutated by scripts via initMouseEvent in dispatchEvent above?
            //    //    Ref<MouseEvent> doubleClickEvent = MouseEvent::create(eventNames().dblclickEvent,
            //    //        mouseEvent->bubbles() ? Event::CanBubble::Yes : Event::CanBubble::No,
            //    //        mouseEvent->cancelable() ? Event::IsCancelable::Yes : Event::IsCancelable::No,
            //    //        Event::IsComposed::Yes,
            //    //        mouseEvent->view(), mouseEvent->detail(),
            //    //        mouseEvent->screenX(), mouseEvent->screenY(), mouseEvent->clientX(), mouseEvent->clientY(),
            //    //        mouseEvent->modifierKeys(), mouseEvent->button(), mouseEvent->buttons(), mouseEvent->syntheticClickType(), relatedTarget);

            //    //    if (mouseEvent->defaultHandled())
            //    //        doubleClickEvent->setDefaultHandled();

            //    //    dispatchEvent(doubleClickEvent);
            //    //    if (doubleClickEvent->defaultHandled() || doubleClickEvent->defaultPrevented())
            //    //        return false;
            //    //}
            return didNotSwallowEvent;
        }

        //public bool dispatchWheelEvent(PlatformWheelEvent platformEvent)
        //{
        //    var @event = new WheelEvent(platformEvent, document().windowProxy());

        //    // Events with no deltas are important because they convey platform information about scroll gestures
        //    // and momentum beginning or ending. However, those events should not be sent to the DOM since some
        //    // websites will break. They need to be dispatched because dispatching them will call into the default
        //    // event handler, and our platform code will correctly handle the phase changes. Calling stopPropogation()
        //    // will prevent the event from being sent to the DOM, but will still call the default event handler.
        //    // FIXME: Move this logic into WheelEvent::create.
        //    if (!platformEvent.deltaX && !platformEvent.deltaY)
        //        @event.stopImmediatePropagation();

        //    dispatchEvent(@event);
        //    return !@event.defaultPrevented && !@event.defaultHandled;
        //}

        //public bool dispatchKeyEvent(PlatformKeyboardEvent platformEvent)
        //{
        //    var event_ = new KeyboardEvent(platformEvent, document().windowProxy());

        //    var frame = document().frame();
        //    if (frame != null)
        //        if (frame.eventHandler().accessibilityPreventsEventPropagation(event_))
        //            event_.stopImmediatePropagation();

        //    dispatchEvent(event_);
        //    return !event_.defaultPrevented && !event_.defaultHandled;
        //}

        //void dispatchFocusInEvent(string eventType, element oldFocusedElement)
        //{
        //    //Debug.Assert(eventType == eventNames().focusinEvent || eventType == eventNames().DOMFocusInEvent);
        //    dispatchScopedEvent(new FocusEvent(eventType, Event.CanBubble.Yes, Event.IsCancelable.No, document().windowProxy(), 0, oldFocusedElement));
        //}

        //void dispatchFocusOutEvent(string eventType, element newFocusedElement)
        //{
        //    //Debug.Assert(eventType == eventNames().focusoutEvent || eventType == eventNames().DOMFocusOutEvent);
        //    dispatchScopedEvent(new FocusEvent(eventType, Event.CanBubble.Yes, Event.IsCancelable.No, document().windowProxy(), 0, newFocusedElement));
        //}

        //void dispatchFocusEvent(element oldFocusedElement, FocusDirection x)
        //{
        //    var page = document().page();
        //    if (page != null)
        //        page.chrome().client().elementDidFocus(this);
        //    dispatchEvent(new FocusEvent(eventNames().focusEvent, Event.CanBubble.No, Event.IsCancelable.No, document().windowProxy(), 0, WTFMove(oldFocusedElement)));
        //}

        //void dispatchBlurEvent(element newFocusedElement)
        //{
        //    var page = document().page();
        //    if (page != null)
        //        page.chrome().client().elementDidBlur(this);
        //    dispatchEvent(new FocusEvent(eventNames().blurEvent, Event.CanBubble.No, Event.IsCancelable.No, document().windowProxy(), 0, WTFMove(newFocusedElement)));
        //}


        char Element.accessKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //NamedNodeMap Element.attributes => throw new NotImplementedException();
        int Element.childElementCount => _children.Count;
        //NodeList Element.childNodes => throw new NotImplementedException();
        HTMLCollection Element.children => new HTMLCollection(_children);
        DOMTokenList Element.classList => throw new NotImplementedException();
        string Element.className { get => get_attr("class"); set => set_attr("class", value); }
        int Element.clientHeight => throw new NotImplementedException();
        int Element.clientLeft => throw new NotImplementedException();
        int Element.clientTop => throw new NotImplementedException();
        int Element.clientWidth => throw new NotImplementedException();
        string Element.contentEditable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string Element.dir { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //Node Element.firstChild => throw new NotImplementedException();
        Node Element.firstElementChild => _children.Count > 0 ? _children[0] : null;
        string Element.id { get => get_attr("id"); set => set_attr("id", value); }
        string Element.innerHTML { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string Element.innerText { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool Element.isContentEditable => throw new NotImplementedException();
        string Element.lang { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //Node Element.lastChild => throw new NotImplementedException();
        Node Element.lastElementChild => _children.Count > 0 ? _children[_children.Count - 1] : null;
        string Element.namespaceURI => throw new NotImplementedException();
        //Node Element.nextSibling => throw new NotImplementedException();
        Node Element.nextElementSibling => throw new NotImplementedException();
        //string Element.nodeName => throw new NotImplementedException();
        //int Element.nodeType => throw new NotImplementedException();
        //string Element.nodeValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        int Element.offsetHeight => throw new NotImplementedException();
        int Element.offsetWidth => throw new NotImplementedException();
        int Element.offsetLeft => throw new NotImplementedException();
        Node Element.offsetParent => throw new NotImplementedException();
        int Element.offsetTop => throw new NotImplementedException();
        //Document Element.ownerDocument => throw new NotImplementedException();
        //Node Element.parentNode => throw new NotImplementedException();
        Element Element.parentElement => _parent;
        //Node Element.previousSibling => throw new NotImplementedException();
        Node Element.previousElementSibling => throw new NotImplementedException();
        int Element.scrollHeight => throw new NotImplementedException();
        int Element.scrollLeft => throw new NotImplementedException();
        int Element.scrollTop => throw new NotImplementedException();
        int Element.scrollWidth => throw new NotImplementedException();
        Style Element.style => throw new NotImplementedException();
        int Element.tabIndex { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string Element.tagName => get_tagName();
        //string Element.textContent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string Element.title { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        void Element.addEventListener(string @event, string function, bool useCapture) => addEventListener(@event, null, new EventListenerOptions { capture = useCapture });
        //Node Element.appendChild(Node node) => throw new NotImplementedException();
        void Element.blur() => throw new NotImplementedException();
        void Element.click() => throw new NotImplementedException();
        //Node Element.cloneNode(bool deep) => throw new NotImplementedException();
        //int Element.compareDocumentPosition(Node node) => throw new NotImplementedException();
        bool Element.contains(Node node) => throw new NotImplementedException();
        void Element.exitFullscreen() => throw new NotImplementedException();
        void Element.focus() => throw new NotImplementedException();
        string Element.getAttribute(string attributename) => get_attr(attributename);
        Attr Element.getAttributeNode(string attributename) => new attr(((html_tag)this)._attrs, attributename);
        Rect Element.getBoundingClientRect() => throw new NotImplementedException();
        NodeList Element.getElementsByClassName(string classname)
        {
            var elem = new css_element_selector();
            var attr = new css_attribute_selector
            {
                val = classname,
                condition = attr_select_condition.equal,
                attribute = "class"
            };
            elem._attrs.Add(attr);
            var sel = new css_selector(elem);
            return new NodeList(select_all(sel));
        }
        NodeList Element.getElementsByTagName(string tagname)
        {
            var elem = new css_element_selector { _tag = tagname.ToLowerInvariant() };
            var sel = new css_selector(elem);
            return new NodeList(select_all(sel));
        }
        bool Element.hasAttribute(string attributename) => throw new NotImplementedException();
        bool Element.hasAttributes() => throw new NotImplementedException();
        //bool Element.hasChildNodes() => throw new NotImplementedException();
        void Element.insertAdjacentElement(string position, Element element) => throw new NotImplementedException();
        void Element.insertAdjacentHTML(string position, string text) => throw new NotImplementedException();
        void Element.insertAdjacentText(string position, string text) => throw new NotImplementedException();
        Node Element.insertBefore(Node newnode, Node existingnode) => throw new NotImplementedException();
        //bool Element.isDefaultNamespace(string namespaceURI) => throw new NotImplementedException();
        //bool Element.isEqualNode(Node node) => throw new NotImplementedException();
        //bool Element.isSameNode(Node node) => throw new NotImplementedException();
        //void Element.normalize() => throw new NotImplementedException();
        Element Element.querySelector(string selectors) => select_one(selectors);
        NodeList Element.querySelectorAll(string selectors) => new NodeList(select_all(selectors));
        void Element.removeAttribute(string attributename) => throw new NotImplementedException();
        Attr Element.removeAttributeNode(Attr attributenode) => throw new NotImplementedException();
        //Node Element.removeChild(Node node) => throw new NotImplementedException();
        void Element.removeEventListener(string @event, string function, bool useCapture) => removeEventListener(@event, null, new EventListenerOptions { capture = useCapture });
        //Node Element.replaceChild(Node newnode, Node oldnode) => throw new NotImplementedException();
        void Element.requestFullscreen() => throw new NotImplementedException();
        void Element.scrollIntoView(bool alignTo) => throw new NotImplementedException();
        void Element.setAttribute(string attributename, string attributevalue) => set_attr(attributename, attributevalue);
        Attr Element.setAttributeNode(Attr attributenode) => throw new NotImplementedException();
        string Element.toString() => throw new NotImplementedException();
    }

    /// <summary>
    /// document
    /// </summary>
    partial class document
    {
        internal object windowProxy;

        Element Document.activeElement => throw new NotImplementedException();
        HTMLCollection Document.anchors => throw new NotImplementedException();
        HTMLCollection Document.applets => throw new NotImplementedException();
        string Document.baseURI => throw new NotImplementedException();
        Element Document.body { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string Document.cookie { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string Document.characterSet => throw new NotImplementedException();
        Window Document.defaultView => throw new NotImplementedException();
        string Document.designMode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        DocumentType Document.doctype => throw new NotImplementedException();
        Element Document.documentElement => throw new NotImplementedException();
        string Document.documentURI { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string Document.domain => throw new NotImplementedException();
        HTMLCollection Document.embeds => throw new NotImplementedException();
        HTMLCollection Document.forms => throw new NotImplementedException();
        Element Document.fullscreenElement => throw new NotImplementedException();
        Element Document.head => throw new NotImplementedException();
        HTMLCollection Document.images => throw new NotImplementedException();
        DocumentImplementation Document.implementation => throw new NotImplementedException();
        string Document.inputEncoding => throw new NotImplementedException();
        DateTime Document.lastModified => throw new NotImplementedException();
        HTMLCollection Document.links => throw new NotImplementedException();
        string Document.readyState => throw new NotImplementedException();
        string Document.referrer => throw new NotImplementedException();
        HTMLCollection Document.scripts => throw new NotImplementedException();
        bool Document.strictErrorChecking { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string Document.title { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string Document.URL => throw new NotImplementedException();
        void Document.addEventListener(string @event, string function, bool useCapture) => throw new NotImplementedException();
        Node Document.adoptNode(Node node) => throw new NotImplementedException();
        void Document.close() => throw new NotImplementedException();
        Node Document.createAttribute(string attributename) => throw new NotImplementedException();
        Node Document.createComment(string text) => throw new NotImplementedException();
        DocumentFragment Document.createDocumentFragment() => throw new NotImplementedException();
        Element Document.createElement(string nodename) => throw new NotImplementedException();
        Event Document.createEvent(string type) => throw new NotImplementedException();
        Node Document.createTextNode(string text) => throw new NotImplementedException();
        bool Document.execCommand(string command, bool showUI, object value) => throw new NotImplementedException();
        bool Document.fullscreenEnabled() => throw new NotImplementedException();
        Element Document.getElementById(string elementID)
        {
            var elem = new css_element_selector();
            var attr = new css_attribute_selector
            {
                val = elementID,
                condition = attr_select_condition.equal,
                attribute = "id"
            };
            elem._attrs.Add(attr);
            var sel = new css_selector(elem);
            return _root.select_one(sel);
        }
        NodeList Document.getElementsByClassName(string classname) => ((Element)_root).getElementsByClassName(classname);
        NodeList Document.getElementsByName(string name)
        {
            var elem = new css_element_selector();
            var attr = new css_attribute_selector
            {
                val = name,
                condition = attr_select_condition.equal,
                attribute = "name"
            };
            elem._attrs.Add(attr);
            var sel = new css_selector(elem);
            return new NodeList(_root.select_all(sel));
        }
        NodeList Document.getElementsByTagName(string tagname) => ((Element)_root).getElementsByTagName(tagname);
        bool Document.hasFocus() => throw new NotImplementedException();
        Node Document.importNode(Node node, bool deep) => throw new NotImplementedException();
        void Document.normalize() => throw new NotImplementedException();
        void Document.open(string MIMEtype, string replace) => throw new NotImplementedException();
        Element Document.querySelector(string selectors) => throw new NotImplementedException();
        NodeList Document.querySelectorAll(string selectors) => throw new NotImplementedException();
        void Document.removeEventListener(string @event, string function, bool useCapture) => throw new NotImplementedException();
        Node Document.renameNode(Node node, string namespaceURI, string nodename) => throw new NotImplementedException();
        void Document.write(params object[] args) => throw new NotImplementedException();
        void Document.writeln(params object[] args) => throw new NotImplementedException();
    }
}
