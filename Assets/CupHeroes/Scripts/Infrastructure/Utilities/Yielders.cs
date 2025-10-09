using UnityEngine;

public static class Yielders
{
    public static readonly WaitForEndOfFrame EndOfFrame = new WaitForEndOfFrame();
    public static readonly WaitForFixedUpdate FixedUpdate = new WaitForFixedUpdate();
}
