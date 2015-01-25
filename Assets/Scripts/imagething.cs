using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class imagething : MonoBehaviour {
	
	static int SIZE = 256; // SIZE OF THE PAINTING SURFACE
	static float SCORETOPASS = 0.2f; // % OF DRAWN PIXELS THAT NEED TO BE ON THE MARK TO PASS
	static int BRUSHSIZE=4; // MUST BE BIGGER THAN 1
	static float MINDRAWLENGTH = 401*3; // YOU CAN'T PASS UNTIL THE NUMBER OF PIXELS
									  //  YOU DRAW IS BIGGER THAN THIS NUMBER

    public static Texture2D Drawing;

	Color[,] a = new Color[SIZE,SIZE];
	Color[,] b = new Color[SIZE,SIZE];
	Color[,] c = new Color[SIZE,SIZE];
//	bool[,] d = new bool[SIZE,SIZE];
	Image img;
	int g=0;
	float total=0;
	float hit=0;
	float linemiss=0;
	float outside=0;
	int timer=0;

	float score=0;
	float drawlength=0;
	Color[] startimage;
	// Use this for initialization
	void Start () {

		startimage = (Color[])GetComponent<Image>().sprite.texture.GetPixels().Clone();
//		this.gameObject.GetComponent<Image>().sprite.texture.
		//		Debug.Log(img.fillCenter);
		
//		clear (a,Color.white);

		int row=0;
		int col=0;
		for(int i=0;i<startimage.Length;i++) {
			if(col==SIZE) {
				col=0;
				row++;
			}
//						print (row+"\t"+col);
			a[row,col]=startimage[i];
			col++;
		}
		clear (b,Color.white);

//		for(int i=0;i<2;i++) {
//			for(float t=0f;t<1f;t+=0.01f) {
//				Vector3 p = Vector3.Lerp(new Vector3(100+i,100,0),new Vector3(200+i,200,0),t);
//				a[(int)p.y,(int)p.x]=Color.red;
//			}
//		}
		
		//		randomize(a,Color.red,Color.white);
		//		for(int i=0;i<SIZE;i++) {
		//			for(int j=70;j<99;j++) {
		//				a[j,i]=Color.red;
		//			}
		//		}
		b=(Color[,]) a.Clone();
		//		randomize (b,Color.blue,Color.black);
		
		//		b[0,0]=Color.blue;
		//		b[0,1]=Color.blue;
		//		b[1,0]=Color.blue;
		//		b[2,0]=Color.blue;
		//		b[2,1]=Color.blue;
		//		b[2,2]=Color.blue;
		
		c=addmatrix(a,b);
		
		paint (c);	
		
		
	}
	
	bool itsdown=false;
	bool itusedtobedown=false;
	bool rmbdown=false;
	bool rmbusedtobedown=false;
	Vector3 adjm2last=new Vector3(999,0,0);
	
	void Update() {
		if(Input.GetMouseButtonDown(0)) {
			itsdown=true;
		}
		if(Input.GetMouseButtonDown(1)) {
			rmbdown=true;
		}
		if(Input.GetMouseButtonUp(0)) {
			itsdown=false;
		}
		if(Input.GetMouseButtonUp(1)) {
			rmbdown=false;
		}
		//		GetComponent<Button>().
		//	Debug.Log(Input.mousePosition.ToString());
		
		Rect r = GameObject.Find("Canvas").GetComponent<RectTransform>().rect;
		Vector3 m = Input.mousePosition;
//		Vector3 adjm = new Vector3(m.x/r.width,m.y/r.height,0)*SIZE;
		Vector3 adjm2 = new Vector3((m.x-r.width/2+r.height/2)/r.height,
		                            m.y/r.height,
		                            0)*SIZE;
//		if adjm2
		if(adjm2last.x==999)
			adjm2last=adjm2;
		Vector3 newzero = new Vector3(m.x-r.width/2,m.y-r.height/2,0);
//		Debug.Log(itsdown);
		Color paintcolor=Color.green;
		if(itsdown) {
			paintcolor = Color.blue;
		}
		if(rmbdown) {
			paintcolor = Color.black;
		}
		if(itsdown||rmbdown) {
			for(float t=0f;t<1f;t+=0.01f) {
				for(int i=-(BRUSHSIZE-1)/2;i<(BRUSHSIZE)/2;i++) {
					for(int j=-(BRUSHSIZE-1)/2;j<(BRUSHSIZE)/2;j++) {
						Vector3 p = Vector3.Lerp(adjm2last,adjm2,t);
						if(p.x<BRUSHSIZE)
							p.x=BRUSHSIZE;//paintcolor=Color.clear;
						if(p.x>=SIZE-BRUSHSIZE)
							p.x=SIZE-BRUSHSIZE;//paintcolor=Color.clear;
						if(p.y<BRUSHSIZE)
							p.y=BRUSHSIZE;//paintcolor=Color.clear;
						if(p.y>=SIZE-BRUSHSIZE)
							p.y=SIZE-BRUSHSIZE;//paintcolor=Color.clear;
						b[(int)p.y+i,(int)p.x+j]=(paintcolor.Equals(Color.black) ? a[(int)p.y+i,(int)p.x+j] : (p.x>=SIZE-BRUSHSIZE ? Color.white : paintcolor));
					}
				}
			}
		}
		adjm2last=adjm2;
		//		c=addmatrix(a,b);
		if(itsdown||rmbdown) {
			paint (b);
//			Debug.Log("horf");
		} else {
//			Debug.Log("BLARG");
		}
		//		this.GetComponent<RectTransform>().
	}
	// Update is called once per frame
	void FixedUpdate () {
		//		Input.
		//		Debug.Log(Input.anyKeyDown);
		//		if(!Input.GetKeyDown(KeyCode.Space))
		//			return;
//		return;
//		if(timer++<1)
//			return;
//		Debug.Log(itusedtobedown+"\t"+itsdown);
		if(itusedtobedown==itsdown&&rmbusedtobedown==rmbdown) {
			return;
		}
		if((!itusedtobedown&&itsdown)||(!rmbusedtobedown&&rmbdown)) {
			itusedtobedown=itsdown;
			rmbusedtobedown=rmbdown;
			return;
		}
		timer=0;
		c=addmatrix(a,b);
		paint (c);	
		
		hit=0;
		linemiss=0;
		outside=0;
		total=SIZE*SIZE;

		Color purple = new Color(0.5f,0f,0.5f);
		for(int i=0;i<Mathf.Sqrt(c.Length);i++) {
			for(int j=0;j<Mathf.Sqrt(c.Length);j++) {
				if(c[i,j].Equals(Color.cyan))
					hit++;
				if(c[i,j].Equals(Color.red))
					linemiss++;
				if(c[i,j].Equals(Color.blue))
					outside++;
			}
		}
		
		//		printme (a);
		//		Debug.Log("\n");
		//		printme (b);
		//		Debug.Log("\n");
		//		printme (c);
		//		Debug.Log("\n");
		//		printmeint (d);

		drawlength = (outside+hit);
//		print (linelength);
		score=(hit)/(drawlength);
		Debug.Log("Total: "+total+"\tHit: "+hit+"\tMiss: "+linemiss+"\tOutside: "+outside+"\tScore: "+score+"\tPass: "+(score>SCORETOPASS&&drawlength>MINDRAWLENGTH));

		itusedtobedown=itsdown;
		rmbusedtobedown=rmbdown;
		
	}

    public void ExitArt()
    {
        Color[] b_single = new Color[b.GetLength(0) * b.GetLength(1)];
        for (int r = 0; r < b.GetLength(0); r++)
            for (int c = 0; c < b.GetLength(1); c++)
                b_single[r * b.GetLength(1) + c] = new Color(b[r, c].r, b[r, c].g, b[r, c].b, b[r, c].r > 0 ? 0 : 1);
        Drawing = new Texture2D(b.GetLength(1), b.GetLength(0));
        Drawing.SetPixels(b_single);
        Drawing.Apply();
        MainGame.ArtQuality = (drawlength > MINDRAWLENGTH) ? Mathf.Min(score * 1 / 0.25f, 1f) : (0.5f * score / 0.25f);
        Application.LoadLevel("GameMenuScene");
    }
	
	void OnDestroy() {
		clear(c,Color.white);
		img = this.gameObject.GetComponent<Image>();
		Texture2D tex = img.sprite.texture;

		Color[] alt = new Color[startimage.Length];
		int asdf=0;
		foreach(Color i in startimage) {
			alt[asdf++]=(i.Equals(Color.black) ? Color.clear : i);
		}
		//		Debug.Log (tex.width+" "+tex.height);
		tex.SetPixels(alt);
		tex.Apply();
        ExitArt();
//		Sprite s = Sprite.Create(tex,new Rect(0,0,Mathf.Sqrt(startimage.Length),Mathf.Sqrt(startimage.Length)),Vector2.up);
//		img.sprite=s;
	}
	
	public void paint(Color[,] r) {
		img = this.gameObject.GetComponent<Image>();
		Texture2D tex = img.sprite.texture;
		Texture2D texnew = new Texture2D((int) Mathf.Sqrt(r.Length),(int) Mathf.Sqrt(r.Length));
		
		Color[] alt = new Color[r.Length];
		int asdf=0;
		foreach(Color i in r) {
			alt[asdf++]=(i.Equals(Color.black) ? Color.clear : i);
		}
//		Debug.Log (tex.width+" "+tex.height);
		tex.SetPixels(alt);
		tex.Apply();
		Sprite s = Sprite.Create(tex,new Rect(0,0,Mathf.Sqrt(r.Length),Mathf.Sqrt(r.Length)),Vector2.up);
		img.sprite=s;
	}
	
	public void clear(Color[,] r, Color v) {
		for(int i=0;i<Mathf.Sqrt(r.Length);i++) {
			for(int j=0;j<Mathf.Sqrt(r.Length);j++) {
				r[i,j]=v;
			}
		}
	}
	
	public void randomize(Color[,] r, Color v, Color w) {
		for(int i=0;i<Mathf.Sqrt(r.Length);i++) {
			for(int j=0;j<Mathf.Sqrt(r.Length);j++) {
				if(Random.value<0.5f)
					r[i,j]=w;
				else
					r[i,j]=v;
			}
		}
	}
	
	public void printme(Color[,] hihi) {
		string bob="";
		for(int i=0;i<Mathf.Sqrt(hihi.Length);i++) {
			for(int j=0;j<Mathf.Sqrt(hihi.Length);j++) {
				bob+="("+hihi[i,j].r+","+hihi[i,j].g+","+hihi[i,j].b+") ";
			}
			bob+="\n";
		}
		Debug.Log(bob);
	}
	
	public void printmeint(bool[,] hihi) {
		string bob="";
		for(int i=0;i<Mathf.Sqrt(hihi.Length);i++) {
			for(int j=0;j<Mathf.Sqrt(hihi.Length);j++) {
				bob+=hihi[i,j]+" ";
			}
			bob+="\n";
		}
		Debug.Log(bob);
	}
	
	public Color[,] addmatrix(Color[,] f, Color[,] g) {
//		Color purple = new Color(0.5f,0f,0.5f);
		Color[,] h = new Color[(int) Mathf.Sqrt(f.Length),(int) Mathf.Sqrt(f.Length)];
		for(int i=0;i<Mathf.Sqrt(h.Length);i++) {
			for(int j=0;j<Mathf.Sqrt(h.Length);j++) {
				h[i,j]=(f[i,j]+g[i,j])/2f;
				//				h[i,j]=Color.white;
				if(h[i,j].Equals(new Color(0.5f,0.5f,0.5f)))
					h[i,j]=Color.white;
				if(h[i,j].Equals(new Color(0.5f,0f,0f)))
					h[i,j]=Color.red;
				if(h[i,j].Equals(new Color(0.5f,0.5f,1f)))
					h[i,j]=Color.blue;
				if(h[i,j].Equals(new Color(0.5f,0f,0.5f)))
					h[i,j]=Color.cyan;
			}
		}
		return h;
	}
}
