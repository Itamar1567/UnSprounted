public class Smelter : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void PerformAction()
    {
        UIControl.Singleton.OpenWidow(2);
    }
}
