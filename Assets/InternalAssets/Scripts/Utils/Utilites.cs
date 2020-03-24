using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

        public static bool IsPointerOverUIElement(Vector3 screenPosition)
        {
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = screenPosition;
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }
    }
}