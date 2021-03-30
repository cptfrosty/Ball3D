using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    #region Singleton
    public static HUDController _instance;

    public static HUDController Instance
    {
        get { return _instance; }
    }
    #endregion

    public UnityEngine.UI.Text TextPoint;

    private void Awake()
    {
        /*Singleton*/
        _instance = this;
    }

    public void UpdatePoint(int point)
    {
        TextPoint.text = "Очки: " + point;
    }
}
