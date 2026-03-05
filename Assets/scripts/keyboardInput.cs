using UnityEngine;
using UnityEngine.InputSystem;

public class keyboardInput : MonoBehaviour
{
    public PoopPlacer PoopPlacer; 
    public corgi corgi;
    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard.wKey.isPressed)
        {
            corgi.Move(Vector2.up);
        }
        if (keyboard.sKey.isPressed)
        {
            corgi.Move(Vector2.down);
        }
        if (keyboard.aKey.isPressed)
        {
            corgi.Move(Vector2.left);
        }
        if (keyboard.dKey.isPressed)
        {
            corgi.Move(Vector2.right);
        }
        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            PoopPlacer.Place(corgi.GetPosition());
        }

    }
}
