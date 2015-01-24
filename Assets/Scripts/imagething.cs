using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class imagething : MonoBehaviour {
	
	static int SIZE = 256;
	
	Color[,] a = new Color[SIZE,SIZE];
	Color[,] b = new Color[SIZE,SIZE];
	Color[,] c = new Color[SIZE,SIZE];
	bool[,] d = new bool[SIZE,SIZE];
	Image img;
	int g=0;
	float total=0;
	float hit=0;
	float linemiss=0;
	float outside=0;
	int timer=0;
	
	// Use this for initialization
	void Start () {
		
		//		Debug.Log(img.fillCenter);
		
		clear (a,Color.white);
		clear (b,Color.white);

		for(int i=0;i<2;i++) {
			for(float t=0f;t<1f;t+=0.01f) {
				Vector3 p = Vector3.Lerp(new Vector3(100+i,100,0),new Vector3(200+i,200,0),t);
				a[(int)p.y,(int)p.x]=Color.red;
			}
		}
		
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
	Vector3 adjm2last=new Vector3(999,0,0);
	
	void Update() {
		if(Input.GetMouseButtonDown(0)) {
			itsdown=true;
		}
		if(Input.GetMouseButtonUp(0)) {
			itsdown=false;
		}
		//		GetComponent<Button>().
		//	Debug.Log(Input.mousePosition.ToString());
		
		Rect r = GameObject.Find("Canvas").GetComponent<RectTransform>().rect;
		Vector3 m = Input.mousePosition;
		Vector3 adjm = new Vector3(m.x/r.width,m.y/r.height,0)*SIZE;
		Vector3 adjm2 = new Vector3((m.x-r.width/2+r.height/2)/r.height,
		                            m.y/r.height,
		                            0)*SIZE;
		if(adjm2last.x==999)
			adjm2last=adjm2;
		Vector3 newzero = new Vector3(m.x-r.width/2,m.y-r.height/2,0);
//		Debug.Log(itsdown);
		if(itsdown) {
			for(float t=0f;t<1f;t+=0.01f) {
				Vector3 p = Vector3.Lerp(adjm2last,adjm2,t);
				b[(int)p.y,(int)p.x]=Color.blue;
			}
			b[(int)adjm2.y,(int)adjm2.x]=Color.blue;
		}
		adjm2last=adjm2;
		//		c=addmatrix(a,b);
		if(itsdown) {
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
		if(itusedtobedown==itsdown) {
			return;
		}
		if(!itusedtobedown&&itsdown) {
			itusedtobedown=itsdown;
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

				Debug.Log("Total: "+total+"\tHit: "+hit+"\tMiss: "+linemiss+"\tOutside: "+outside+"\tPass: "+(3f*hit-outside>0&&linemiss<150));
		
		itusedtobedown=itsdown;
		
	}
	
	void OnDestroy() {
		clear(c,Color.white);
	}
	
	public void paint(Color[,] r) {
		img = this.gameObject.GetComponent<Image>();
		Texture2D tex = img.sprite.texture;
		Texture2D texnew = new Texture2D((int) Mathf.Sqrt(r.Length),(int) Mathf.Sqrt(r.Length));
		
		Color[] alt = new Color[r.Length];
		int asdf=0;
		foreach(Color i in r) {
			alt[asdf++]=i;
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
				if(h[i,j].Equals(new Color(0.5f,0.5f,1f)))
					h[i,j]=Color.blue;
				if(h[i,j].Equals(new Color(0.5f,0f,0.5f)))
					h[i,j]=Color.cyan;
			}
		}
		return h;
	}
}
