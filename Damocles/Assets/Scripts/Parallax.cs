using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    private float length, startpos, ypos;
    public GameObject cam;
    public float parallaxEffect;

    private Transform cameraTransform;

    private float textureUnitSize;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        startpos = transform.position.x;
        ypos = transform.position.y;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSize = texture.width / sprite.pixelsPerUnit;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);
        float ydist = (cam.transform.position.y * parallaxEffect);

        transform.position = new Vector3(startpos + dist, ypos + ydist, transform.position.z);
        if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSize)
        {
            float offSetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSize;
            transform.position = new Vector3(cameraTransform.position.x + offSetPositionX, transform.position.y);
        }
    }

}
