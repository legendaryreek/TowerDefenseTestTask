using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP.TowerDefense.Utils
{
    public static class Utilites
    {
        public static Vector3 ScreenToCanvasPoint(Canvas canvas, Vector3 screenPoint)
        {
            float xCoeff = screenPoint.x / Screen.width;
            float yCoeff = screenPoint.y / Screen.height;

            return new Vector3(xCoeff * canvas.GetComponent<RectTransform>().rect.width, yCoeff * canvas.GetComponent<RectTransform>().rect.height);
        }
    }
}