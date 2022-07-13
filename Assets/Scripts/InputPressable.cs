    using UnityEngine;

    public class InputPressable : MonoBehaviour
    {
        public KeyCode keyCode;
        public int mouseButton = -1;

        public bool IsDown()
        {
            if (keyCode != KeyCode.None && Input.GetKeyDown(keyCode))
                return true;
            if (mouseButton >= 0 && Input.GetMouseButtonDown(mouseButton))
                return true;
            return false;
        }

        public bool IsUp()
        {
            if (keyCode != KeyCode.None && Input.GetKeyUp(keyCode))
                return true;
            if (mouseButton >= 0 && Input.GetMouseButtonUp(mouseButton))
                return true;
            return false;
        }

        public bool IsPressing()
        {
            if (keyCode != KeyCode.None && Input.GetKey(keyCode))
                return true;
            if (mouseButton >= 0 && Input.GetMouseButton(mouseButton))
                return true;
            return false;
        }
    }