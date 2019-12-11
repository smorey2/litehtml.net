using System;
using System.Linq;
using System.Collections.Generic;

namespace Litehtml
{
    partial class Event
    {
        public object Target { get; set; }
        public object CurrentTarget { get; set; }
        public char EventPhase { get; set; }

        public void resetBeforeDispatch()
        {
        }

        public void resetAfterDispatch()
        {
        }
    }

    public delegate void EventListener(object ctx, Event e);

    public class EventListenerOptions
    {
        public bool capture;
        public bool passive;
        public bool once;
    }

    public interface EventTarget
    {
        //bool addEventListener(string eventType, EventListener listener, EventListenerOptions options);
        //bool removeEventListener(string eventType, EventListener listener, EventListenerOptions options);
    }

    public class EventSystem
    {
        readonly Dictionary<string, List<EventEntry>> _eventEntries = new Dictionary<string, List<EventEntry>>();

        enum EventInvokePhase
        {
            Capturing,
            Bubbling
        }

        struct EventEntry
        {
            public EventListener listener;
            public EventListenerOptions options;
            public EventEntry(EventListener l, EventListenerOptions o) { listener = l; options = o; }
        }

        public bool addEventListener(string eventType, EventListener listener, EventListenerOptions options)
        {
            lock (this)
            {
                if (_eventEntries.TryGetValue(eventType, out var eventEntry))
                {
                    if (eventEntry.Any(x => x.options.capture == options.capture))
                        return false;
                    _eventEntries[eventType].Add(new EventEntry(listener, options));
                    return true;
                }
                _eventEntries[eventType] = new List<EventEntry> { new EventEntry(listener, options) };
                return true;
            }
        }

        public bool removeEventListener(string eventType, EventListener listener, EventListenerOptions options)
        {
            lock (this)
            {
                if (_eventEntries.TryGetValue(eventType, out var eventEntry))
                {
                    var r = eventEntry.RemoveAll(x => x.options.capture == options.capture) > 0;
                    if (eventEntry.Count == 0)
                        _eventEntries.Remove(eventType);
                    return r;
                }
            }
            return false;
        }

        protected void dispatchEvent(Event ev)
        {
            ev.Target = this;
            ev.CurrentTarget = this;
            ev.EventPhase = '@';
            ev.resetBeforeDispatch();
            fireEventListeners(ev, EventInvokePhase.Capturing);
            fireEventListeners(ev, EventInvokePhase.Bubbling);
            ev.resetAfterDispatch();
        }

        void fireEventListeners(Event ev, EventInvokePhase phase)
        {
            if (_eventEntries.TryGetValue(ev.type, out var eventEntry))
                invokeEventListeners(ev, eventEntry, phase);
        }

        void invokeEventListeners(Event ev, IList<EventEntry> listeners, EventInvokePhase phase)
        {
            //Debug.Assert(listeners.Count != 0);
            var ctx = (object)null; //scriptExecutionContext
            foreach (var registeredListener in listeners)
            {
                if (phase == EventInvokePhase.Capturing && !registeredListener.options.capture)
                    continue;
                if (phase == EventInvokePhase.Bubbling && registeredListener.options.capture)
                    continue;
                // If stopImmediatePropagation has been called, we just break out immediately, without handling any more events on this target.
                if (ev._immediatePropagationStopped)
                    break;
                // Do this before invocation to avoid reentrancy issues.
                if (registeredListener.options.once)
                    removeEventListener(ev.type, registeredListener.listener, registeredListener.options);
                if (registeredListener.options.passive)
                    ev._inPassiveListener = true;
                registeredListener.listener(ctx, ev);
                if (registeredListener.options.passive)
                    ev._inPassiveListener = false;
            }
        }
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
