    using UnityEngine;

    public class UILayer : MonoBehaviour
    {
        public RectTransform rt;
        
        public Camera cam => CamRotator.enabled.cam;
    }