using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Prog_TypedText : MonoBehaviour {

    public class LinkedList
    {
        public class Node
        {
            public Node next, prev;
            public char value;

            public Node(char c)
            {
                next = null;
                prev = null;
                value = c;
            }
        }

        public Node head;
        public Node current;
        public int currentIndex;
        public Node pointer;
        public bool bad;

        public LinkedList()
        {
            head = null;
            current = null;
            pointer = null;
            currentIndex = 0;
            bad = false;
        }

        public void Add(char c)
        {
            if (head == null)
            {
                head = new Node(c);
                current = head;
                pointer = head;
            }
            else
            {
                current.next = new Node(c);
                current.next.prev = current;
                current = current.next;
            }
            currentIndex++;
        }

        public char Remove()
        {
            if (head == null)
                return '\0';
            else
            {
                if (head == current)
                    head = null;
                current = current.prev;
                currentIndex--;
                if (current != null)
                {
                    current.next = null;
                    return current.value;
                }
                return '\0';
            }
        }
    }

    public static string ToType;

    public GameObject TypeThis;

    private Text text;
    private LinkedList needToType;
    private LinkedList current;

	// Use this for initialization
    void Start()
    {
        ToType = TypeThis.GetComponent<Text>().text;
        text = GetComponent<Text>();
        needToType = new LinkedList();
        for (int i = 0; i < ToType.Length; i++)
        {
            needToType.Add(ToType[i]);
        }
        current = new LinkedList();
    }

    // Update is called once per frame
    bool shiftPressed;
	void Update () 
    {
        shiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
	}

    public void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown)
        {
            KeyCode k = e.keyCode;
            Debug.Log(k.ToString());
            if (e.keyCode != KeyCode.None)
            {
                if (e.keyCode == KeyCode.Backspace)
                {
                    // Remove string last character from typed string
                    current.Remove();
                    text.text = text.text.Substring(0, text.text.Length - 1);

                    // Remove autobackspace whitespace

                    // Check if pointers line up again and set "bad" to false if it's fixed
                }
                else if (e.keyCode == KeyCode.Space || e.keyCode == KeyCode.Tab || e.keyCode == KeyCode.Return)
                {
                    // Ignore  
                }
                else
                {
                    char result = '\0';
                    string possibleKeys = ")!@#$%^&*(";
                    if (e.keyCode.ToString().Contains("Alpha"))
                        result = shiftPressed ? possibleKeys[int.Parse(e.keyCode.ToString().Replace("Alpha", "")) % 10] : e.keyCode.ToString().Replace("Alpha", "")[0];
                    else if (e.keyCode == KeyCode.BackQuote)
                        result = shiftPressed ? '~' : '`';
                    else if (e.keyCode == KeyCode.Minus)
                        result = shiftPressed ? '_' : '-';
                    else if (e.keyCode == KeyCode.Equals)
                        result = shiftPressed ? '+' : '=';
                    else if (e.keyCode == KeyCode.LeftBracket)
                        result = shiftPressed ? '{' : '[';
                    else if (e.keyCode == KeyCode.RightBracket)
                        result = shiftPressed ? '}' : ']';
                    else if (e.keyCode == KeyCode.Backslash)
                        result = shiftPressed ? '|' : '\\';
                    else if (e.keyCode == KeyCode.Semicolon)
                        result = shiftPressed ? ':' : ';';
                    else if (e.keyCode == KeyCode.Quote)
                        result = shiftPressed ? '"' : '\'';
                    else if (e.keyCode == KeyCode.Comma)
                        result = shiftPressed ? '<' : ',';
                    else if (e.keyCode == KeyCode.Period)
                        result = shiftPressed ? '>' : '.';
                    else if (e.keyCode == KeyCode.Slash)
                        result = shiftPressed ? '?' : '/';
                    else if (e.keyCode.ToString().Length == 1)
                        result = shiftPressed ? e.keyCode.ToString()[0] : e.keyCode.ToString().ToLower()[0];

                    if (result != '\0')
                    {
                        current.Add(result);
                        if (current.head != current.current)
                            current.pointer = current.pointer.next;
                        text.text += result;

                        // Check if linked list matches
                        if (!current.bad)
                        {
                            if (needToType.pointer.value == current.pointer.value)
                            {
                                needToType.pointer = needToType.pointer.next;
                                if (needToType.pointer == null)
                                {
                                    Debug.Log("YOU WIN");
                                }
                            }
                            else
                            {
                                current.bad = true;
                            }
                        }

                        while (needToType.pointer.value == ' ' || needToType.pointer.value == '\n' || needToType.pointer.value == '\t')
                        {
                            current.Add(needToType.pointer.value);
                            text.text += needToType.pointer.value;
                            current.pointer = current.pointer.next;
                            needToType.pointer = needToType.pointer.next;
                        }
                    }
                }
            }
        }
    }
}
