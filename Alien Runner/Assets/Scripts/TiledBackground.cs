using UnityEngine;
using System.Collections;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 
* VERSION: 1.0
* SCRIPT: Tiled Background Class
*/

/// <summary>
/// 
/// </summary>
public class TiledBackground : MonoBehaviour {

    public int textureSize = 32;
    public bool scaleHorizontally = true;
    public bool scaleVertically = true;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        SetHeightAndWidth(GetWidth(), GetHeight());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="newWidth"></param>
    /// <param name="newHeight"></param>
    private void SetHeightAndWidth(float newWidth, float newHeight)
    {
        transform.localScale = new Vector3(newWidth * textureSize, newHeight * textureSize, 1);
        GetComponent<Renderer>().material.mainTextureScale = new Vector3(newWidth, newHeight, 1);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private float GetHeight()
    {
        return !scaleVertically ? 1 : Mathf.Ceil(Screen.height / (textureSize * PixelPerfectCamera.scale));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private float GetWidth()
    {
        return !scaleHorizontally ? 1 : Mathf.Ceil(Screen.width / (textureSize * PixelPerfectCamera.scale));
    }
}
