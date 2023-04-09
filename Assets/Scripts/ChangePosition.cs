using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePosition : MonoBehaviour
{
    public PlayerMovement player;

    public void OnMouseDown()
    {      
      player.PlayerMove(transform.position);
      transform.parent.gameObject.SetActive(false);
    }

}
