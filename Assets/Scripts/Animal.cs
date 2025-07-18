using System.Collections;
using UnityEngine;

public class Animal : NPC
{

    private Coroutine moveCourtineRef;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

        if (destinationSetter.target != null)
        {
            if (isChangePosCoroutineRunning == false && ai.reachedDestination)
            {
                moveCourtineRef = StartCoroutine(MovePosition());
            }
        }
        if (ai.reachedDestination == true)
        {
            ai.canMove = false;
        }
        else
        {
            ai.canMove = true;
        }

        MoveAnimations();
    }
    protected override void Inititalize()
    {
        base.Inititalize();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (moveCourtineRef != null) { StopCoroutine(moveCourtineRef); }
        EndMoveCoroutine();
    }
    protected override void DropItems()
    {
        base.DropItems();
    }
    protected override void MoveAnimations()
    {
        base.MoveAnimations();
    }
    protected override IEnumerator MovePosition()
    {
        return base.MovePosition();
    }

}
