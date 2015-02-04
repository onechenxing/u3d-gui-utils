//----------------------------------------------
// Unity GUI 血条类
// @author: ChenXing
// @email:  onechenxing@163.com
// @date:   2015/1/1
//----------------------------------------------
using UnityEngine;
using System.Collections;

/// <summary>
/// 血条
/// </summary>
public class GUIBloodBar : MonoBehaviour
{
    /// <summary>
    /// 百分比显示
    /// </summary>
    public float value = 1;
    /// <summary>
    /// 偏移的3D位置(一般为中心点到头顶上的位置)
    /// </summary>
    public Vector3 offset = Vector3.zero;
    /// <summary>
    /// 相机
    /// </summary>
    public Camera uiCamera;

    public int barWidth = 100;
    public int barHeight = 10;

    /// <summary>
    /// 背景贴图
    /// </summary>
    private Texture2D texBg;
    /// <summary>
    /// 贴图
    /// </summary>
    private Texture2D tex;

    /// <summary>
    /// 创建一个血条
    /// </summary>
    /// <param name="parent">绑定对象</param>
    /// <param name="offset">对象的偏移位置(一般为中心点到头顶上的位置)</param>
    /// <param name="camera">相机(如果不传入，取默认相机)</param>
    /// <returns></returns>
    public static GUIBloodBar Show(GameObject parent, Vector3 offset, Camera camera = null)
    {
        if (camera == null)
        {
            camera = Camera.main;
        }        
        GUIBloodBar bar = parent.AddComponent<GUIBloodBar>();
        bar.uiCamera = camera;
        bar.offset = offset;
        return bar;
    }

    public void Start()
    {
        texBg = new Texture2D(barWidth, barHeight);
        //构建一个100*20的红色贴图
        tex = new Texture2D(barWidth, barHeight);
        for (int row = 0; row < barHeight; row++)
        {
            for (int col = 0; col < barWidth; col++)
            {
                tex.SetPixel(col,row,Color.red);
            }
        }
        tex.Apply();
    }

    public void OnDestroy()
    {
        Destroy(tex);
        tex = null;
    }    

    public void OnGUI()
    {
        if (uiCamera.gameObject.activeSelf == false)
        {
            return;
        }
        //跟随本GameObject移动
        Vector3 screenPoint = uiCamera.WorldToScreenPoint(this.transform.position + offset);
        GUI.DrawTexture(new Rect(screenPoint.x - barWidth / 2, Screen.height - screenPoint.y, barWidth , barHeight), texBg);
        if(value > 0)
        {
            GUI.DrawTextureWithTexCoords(new Rect(screenPoint.x - barWidth/2, Screen.height - screenPoint.y, barWidth * value, barHeight), tex, new Rect(0, 0, value, 1.0f));
        }
    }
}
