using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))] 

public class tilling : MonoBehaviour {
    public int offsetX = 2;             //Offset that we dont get any errors 

    //To check if we need the instantiate stuff.
    public bool hasARightBuddy = false;
    public bool hasALeftBuddy = false;

    public bool reverseScale = false;       
    private float spriteWidth = 0;

    private Camera cam;
    private Transform myTransform;

    private void Awake()
    {
        cam = Camera.main;
        myTransform = transform;
    }
    // Use this for initialization
    void Start () {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();  //WIll return 1st one
        spriteWidth = sRenderer.sprite.bounds.size.x;
    }
	
	// Update is called once per frame
	void Update () {
		if(hasALeftBuddy==false || hasARightBuddy == false)
        {
            //Calculate tthe camara extend (half of the width ) of what camara can see
            float camHorizontalExtend = cam.orthographicSize * Screen.width / Screen.height;

            //calculate the x position where camara can see the edge of the sprite
            float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtend;
            float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtend;


            //Checking if we can see the element. and then calling make new buddy.
            if(cam.transform.position.x >= edgeVisiblePositionRight - offsetX && hasARightBuddy == false)
            {
                MakenewBuddy(1);
                hasARightBuddy = true;
            }

            else if (cam.transform.position.x >= edgeVisiblePositionLeft + offsetX && hasALeftBuddy == false)
            {
                MakenewBuddy(-1);
                hasALeftBuddy = true;
            }

        }
	}

    void MakenewBuddy(int rightOrLeft)
    {
        //calculating hte new position for new buddyd 
        Vector3 newPosition = new Vector3 (myTransform.position.x + spriteWidth * rightOrLeft,myTransform.position.y,myTransform.position.z);
        Transform newBuddy = Instantiate(myTransform,newPosition,myTransform.rotation) as Transform;

        //if not tilable let's reverse it 
        if(reverseScale == true)
        {
            newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
        }

        newBuddy.parent = myTransform.parent;
        if(rightOrLeft > 0)
        {
            newBuddy.GetComponent<tilling>().hasALeftBuddy = true;
        }
        else
        {
            newBuddy.GetComponent<tilling>().hasARightBuddy = true;
        }

    }
}
