﻿/*
 * Copyright (c) 2017 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;
using System.Collections;
using System;

public class RealArrow : MonoBehaviour
{
    public BoxCollider pickupCollider;

    public int damage = 15;
    private Rigidbody rb;
    private bool launched;
    private bool stuckInWall;

    // Use this for initialization
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (launched && !stuckInWall && rb.velocity != Vector3.zero)
        {
            rb.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }

    public void SetAllowPickup(bool allow)
    {
        pickupCollider.enabled = allow;
    }

    public void Launch()
    {
        launched = true;
        SetAllowPickup(false);
    }

    private void GetStuck(Collider other)
    {
        StandardEnemy enemy = null;
        launched = false;
        rb.isKinematic = true;
        stuckInWall = true;
        SetAllowPickup(true);
        transform.SetParent(other.transform);
        GetComponentInChildren<ParticleSystem>().gameObject.SetActive(false);
        if(other.gameObject.tag.Contains("Enemy"))
            enemy = other.gameObject.GetComponentInParent<StandardEnemy>();
        switch (other.gameObject.tag)
        {
            
            case "EnemyHead":
                enemy.TakeDamage(enemy.GetMaxHealth());
                Debug.Log("HeadShot");
                break;

            case "EnemyBody":
                enemy.TakeDamage(enemy.GetMaxHealth() / 3);
                Debug.Log("Body Hit");
                break;

            case "EnemyLimb":
                enemy.TakeDamage(enemy.GetMaxHealth() / 9);
                Debug.Log("Limb Hit");
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Controller") || other.GetComponent<Bow>())
        {
            return;
        }

        if (launched && !stuckInWall)
        {
            GetStuck(other);
        }
    }
}
