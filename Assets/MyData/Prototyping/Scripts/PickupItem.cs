using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public enum Type
    {
        Exp,
        Gold
    }

    [field: SerializeField] public Type pickupType { get; private set; }
    [field: SerializeField] public int pickupQuantity { get; private set; }
}
