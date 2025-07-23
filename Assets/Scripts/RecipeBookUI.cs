using UnityEngine;

public class RecipeBookUI : MonoBehaviour
{

    public void Back()
    {
        UIControl.Singleton.OpenWidow(UIControl.Singleton.GetPrevOpenWindow());
    }    
}
