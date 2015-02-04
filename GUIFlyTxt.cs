//----------------------------------------------
// Unity GUI 漂浮文字类（需要iTween支持）
// @author: ChenXing
// @email:  onechenxing@163.com
// @date:   2015/1/1
//----------------------------------------------

using UnityEngine;
using System.Collections;

/// <summary>
/// 飘字文本
/// </summary>
public class GUIFlyTxt : MonoBehaviour 
{
    /// <summary>
    /// 创建一个漂浮文字
    /// </summary>
    /// <param name="txt">文本内容</param>
    /// <param name="position">3D位置</param>
    /// <param name="camera">位置相机(如果不传入，取默认相机)</param>
	public static void Show (string txt, Vector3 position, Camera camera = null) 
    {
        if(camera == null)
        {
            camera = Camera.main;
        }
        Vector3 viewPoint = camera.WorldToViewportPoint(position);
	    GameObject uiObj = new GameObject("FlyTxt" , new System.Type[]{typeof(GUIText),typeof(GUIFlyTxt)});
        uiObj.hideFlags = HideFlags.HideInHierarchy;
        uiObj.guiText.text = txt;
        uiObj.guiText.color = Color.red;
        uiObj.guiText.fontSize = 20 * System.Convert.ToInt32(Screen.width / 600.0f);
        uiObj.guiText.transform.position = viewPoint;
        iTween.MoveBy(uiObj, iTween.Hash("y", 0.1f, "time", 1.2f, "oncomplete", "OnTweenComplete", "oncompletetarget", uiObj, "oncompleteparams", uiObj));
	}	

    public void OnTweenComplete(GameObject target)
    {
        GameObject.DestroyImmediate(target,true);
    }
}
