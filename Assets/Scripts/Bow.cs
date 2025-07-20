using Pathfinding;
using System.Collections;
using UnityEngine;

public class Bow : Shooter
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Shoot(Vector3 target, GameObject shotBy)
    {
        base.Shoot(target, shotBy);
    }

    protected override IEnumerator HideShooter()
    {
        return base.HideShooter();
    }
}
