using UnityEngine;

public class BombWire : MonoBehaviour
{
    public string wireName;

    private bool cut = false;
    public bool IsCut()
    {
        return cut;
    }

    public void Cut()
    {
        cut = true;
        gameObject.SetActive(false);
    }
}
