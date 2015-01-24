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

        public LinkedList()
        {
            head = null;
            current = null;
            pointer = null;
            currentIndex = 0;
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
	void Update () 
    {
	}

    bool shiftPressed;
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
                    current.Remove();
                    text.text = text.text.Substring(0, text.text.Length - 2);

                    // check for match
                }
                else if (e.keyCode == KeyCode.Space || e.keyCode == KeyCode.Tab)
                {
                    // Ignore  
                }
                else if (e.keyCode == KeyCode.Return)
                {
                    // Add \n instead of KeyCode.Return
                }
                else if (e.keyCode == KeyCode.LeftShift || e.keyCode == KeyCode.RightShift)
                {
                    shiftPressed = true;
                }
                else
                {
                    char result = '\0';
                    string possibleKeys = "!@#$%^&*()";
                    if (e.keyCode.ToString().Contains("Alpha"))
                        result = shiftPressed ? possibleKeys[int.Parse(e.keyCode.ToString().Replace("Alpha", "")) - 1] : e.keyCode.ToString().Replace("Alpha", "")[0];
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
                        // Add result to string
                        // Add result to linked list
                        // Check if linked list matches
                    }
                }
            }
        }
        else if (e.type == EventType.KeyUp)
        {
            KeyCode k = e.keyCode;
            if (e.keyCode == KeyCode.LeftShift || e.keyCode == KeyCode.RightShift)
            {
                shiftPressed = false;
            }
        }
    }
}
