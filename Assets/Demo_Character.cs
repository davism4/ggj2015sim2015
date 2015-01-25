using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Demo_Character : MonoBehaviour {

    Image image;
    new RectTransform rectTransform;
    float baseY;
    float jumpTime;
    bool neverJumpAgain;

    public static bool Finished = false;

	// Use this for initialization
	void Start () {
        if (imagething.Drawing != null)
        {
            image = GetComponent<Image>();
            image.sprite = Sprite.Create(imagething.Drawing, new Rect(image.sprite.rect.x,
                                                                      image.sprite.rect.y,
                                                                      imagething.Drawing.width,
                                                                      imagething.Drawing.height), 0.5f * Vector2.one);
        }
        rectTransform = GetComponent<RectTransform>();
        baseY = rectTransform.position.y;
        jumpTime = -1f;
        neverJumpAgain = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (rectTransform.position.x > 350)
            rectTransform.Translate(Vector3.right * -150 * Time.deltaTime);
        else
            Finished = true;
        if (rectTransform.position.x <= 850)
        {
            if (jumpTime == -1f && !neverJumpAgain)
            {
                neverJumpAgain = true;
                jumpTime = 1f;
            }
            else
            {
                jumpTime -= Time.deltaTime * 1.2f;
                if (MainGame.CodeQuality > 0.7f)
                {
                    rectTransform.position = new Vector3(rectTransform.position.x, (-jumpTime * jumpTime + 1) * 70 + baseY, 0);
                    if (rectTransform.position.y < baseY)
                        rectTransform.position = new Vector3(rectTransform.position.x, baseY, 0);
                }
                else if (MainGame.CodeQuality >= 0.4f)
                {
                    rectTransform.position = new Vector3(rectTransform.position.x, -jumpTime * 70 + baseY, 0);
                }
                else
                {
                    rectTransform.Translate(Random.Range(-10, 0), Random.Range(-10, 10), 0);
                    if (rectTransform.position.x > 850)
                        rectTransform.position = new Vector3(800, rectTransform.position.y, 0);
                    rectTransform.Rotate(Random.Range(-90, 90), Random.Range(-90, 90), Random.Range(-90, 90));
                }
            }
        }
	}
}
