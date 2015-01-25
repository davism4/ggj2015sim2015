using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Prog_TypedText : MonoBehaviour {

    public static string[] Words = new string[] {
        "void", "import", "make_game()", "public", "class", "static", "string[]", "linkedlist", "system.out.println",
        "integer", "float", "reference", "null", "boolean", "variable", "while", "x=14", "{};", "unityengine",
        "system.collections"
    };

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
        public Node tail;
        public int pointerIndex;
        public Node pointer;

        public LinkedList()
        {
            head = null;
            tail = null;
            pointer = null;
            pointerIndex = 0;
        }

        public void Add(char c)
        {
            if (head == null)
            {
                head = new Node(c);
                tail = head;
                pointer = head;
            }
            else
            {
                tail.next = new Node(c);
                tail.next.prev = tail;
                tail = tail.next;
            }
        }

        public char Remove()
        {
            if (head == null)
                return '\0';
            else
            {
                if (head == tail)
                    head = null;
                tail = tail.prev;
                if (tail != null)
                {
                    tail.next = null;
                    return tail.value;
                }
                return '\0';
            }
        }
    }

    public static string ToType;

    public GameObject TypeThis;
    public Color GoodColor;
    public Color BadColor;

    public bool bad;
    public float badTimer;

    int wordCount;

    private Text text;
    private LinkedList needToType;
    private LinkedList current;

	// Use this for initialization
    void Start()
    {
        wordCount = 0;
        bad = false;
        badTimer = 0;
        text = GetComponent<Text>();
        SetupNextWord();
    }

    void SetupNextWord()
    {
        text.text = "";
        wordCount++;
        ToType = Words[Random.Range(0, Words.Length - 1)];
        TypeThis.GetComponent<Text>().text = ToType;
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

        if (bad)
        {
            badTimer -= Time.deltaTime;
            if (badTimer <= 0)
            {
                text.color = GoodColor;
                bad = false;
                SetupNextWord();
            }
        }
	}

    public void OnGUI()
    {
        if (bad)
            return;

        Event e = Event.current;
        if (e.type == EventType.KeyDown)
        {
            KeyCode k = e.keyCode;
            if (e.keyCode != KeyCode.None)
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
                    if (current.head != current.tail)
                    {
                        current.pointer = current.pointer.next;
                        current.pointerIndex++;
                    }
                    text.text += result;

                    if (needToType.pointer.value == current.pointer.value)
                    {
                        needToType.pointer = needToType.pointer.next;
                        needToType.pointerIndex++;
                        if (needToType.pointer == null)
                        {
                            MainGame.CodeQuality += 0.1f;
                            SetupNextWord();
                            if (wordCount >= 10)
                            {
                                Application.LoadLevel("GameMenuScene");
                                return;
                            }
                        }
                    }
                    else
                    {
                        bad = true;
                        badTimer = 0.5f;
                        text.color = BadColor;
                    }
                }
            }
        }
    }
}
