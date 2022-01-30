using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow2D : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    void Update()
    {
        float factor= 1f;
        if (player.position.y + 1 >= transform.position.y)
        {
            factor = Time.deltaTime;
        }
        transform.position = new Vector3(
            Mathf.Lerp(transform.position.x,player.position.x + offset.x, 2 * Time.deltaTime)
            , Mathf.Lerp(transform.position.y, player.position.y + offset.y,factor),
            offset.z);   // Camera follows the player with specified offset position
    }
}
