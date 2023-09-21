using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageScroller : MonoBehaviour
{
    [SerializeField] RawImage rawImg;
    [SerializeField] float x, y;
   
    // Update is called once per frame
    void Update()
    {
        rawImg.uvRect = new Rect(rawImg.uvRect.position+new Vector2(x, y)*Time.deltaTime , rawImg.uvRect.size);
    }
    
}
