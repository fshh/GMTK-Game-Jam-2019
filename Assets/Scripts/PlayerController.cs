using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IKillable
{
    public void OnHit()
    {
        Destroy(this.gameObject);
    }
}
