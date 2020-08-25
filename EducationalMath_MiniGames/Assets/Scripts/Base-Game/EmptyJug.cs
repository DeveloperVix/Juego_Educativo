using UnityEngine;

public class EmptyJug : MonoBehaviour
{
    public FullingCarboy fullingCarboy;

    private void OnMouseDown()
    {
        fullingCarboy.EmptyJug();
    }
}
