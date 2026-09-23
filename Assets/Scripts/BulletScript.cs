using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour{
    
    private Rigidbody2D Rigidbody2D;
    public float Speed;

    void Start(){
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate(){
        Rigidbody2D.linearVelocity = Vector2.right * Speed;
    }


}
