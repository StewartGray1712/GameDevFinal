using UnityEngine;

public static class InputSimulator
{
    static bool clickQueued = false;
    static bool keyQueued = false;
    static KeyCode queuedKey;

    public static void ClickMouseLeft()
    {
        clickQueued = true;
    }

    public static void PressKey(KeyCode key)
    {
        keyQueued = true;
        queuedKey = key;
    }

    public static bool CheckMouseClick()
    {
        if (clickQueued)
        {
            clickQueued = false;
            return true;
        }
        return false;
    }

    public static bool CheckKeyPress(KeyCode key)
    {
        if (keyQueued && queuedKey == key)
        {
            keyQueued = false;
            return true;
        }
        return false;
    }
}