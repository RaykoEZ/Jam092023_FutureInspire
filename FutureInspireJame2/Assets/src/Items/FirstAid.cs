using UnityEngine;


public class FirstAid : HangingItem
{
    [SerializeField] int healAmount = default;
    public override void ActivateEffect(Hand hand, DropItem item)
    {

        //TODO: heal current hand
    }
}
