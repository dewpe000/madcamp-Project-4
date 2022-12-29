using UnityEngine;

public class Fixed : MonoBehaviour
{
    private void Start()
    {
        SetResolution(); 
    }

    public void SetResolution()
    {
        int setWidth = 1920;
        int setHeight = 1080;
        Screen.SetResolution(setWidth, setHeight, true);
    }
}