using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    [SerializeField] private Transform attachPoint;
    public Transform AttachPoint { get => attachPoint; set => attachPoint = value; }


}
