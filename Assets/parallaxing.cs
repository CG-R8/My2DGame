using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxing : MonoBehaviour {

    public Transform[] backgrounds;
    private float[] parallaxScale;
    public float smoothing =1;         //How smooth the parallax gonna be.
    private Transform cam;
    private Vector3 previousCamPosition;

    //should be called before Start.just to get the references  
    private void Awake()
    {
        cam = Camera.main.transform;
    }

    // Use this for initialization
    void Start () {
        previousCamPosition = cam.position;
        parallaxScale = new float[backgrounds.Length];
        for(int i=0; i < backgrounds.Length; i++)
        {
            parallaxScale[i] = backgrounds[i].position.z * -1;
        }
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (previousCamPosition.x - cam.position.x) * parallaxScale[i];

            //set a target X position which is the current position + parallax
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;
            //create a taerget position which is the background current position with its target X position.
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            //fade
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);

        }
        previousCamPosition = cam.position;
    }
}
