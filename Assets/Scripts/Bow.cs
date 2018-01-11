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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Bow : MonoBehaviour
{
    public Transform attachedArrow;
    public SkinnedMeshRenderer BowSkinnedMesh;

    public float blendMultiplier = 100f;
    public GameObject realArrowPrefab;

    public float velocity;

    public float maxShootSpeed = 100;

    public AudioClip fireSound;

    bool IsArmed()
    {
        return attachedArrow.gameObject.activeSelf;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, attachedArrow.position);
        BowSkinnedMesh.SetBlendShapeWeight(0, Mathf.Max(0, distance * blendMultiplier));
    }

    private void Arm()
    {
        attachedArrow.gameObject.SetActive(true);
    }

    private void Disarm()
    {
        BowSkinnedMesh.SetBlendShapeWeight(0, 0);
        attachedArrow.position = transform.position;
        attachedArrow.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        try
        {          

            if (!IsArmed() && other.CompareTag("InteractionObject") && other.GetComponent<RealArrow>() && !other.GetComponent<RWVR_InteractionObject>().IsFree())
            {
                Destroy(other.gameObject);
                Arm();
            }
        }
        catch { }
    }

    public void ShootArrow()
    {
        GameObject arrow = Instantiate(realArrowPrefab, transform.position, transform.rotation);
        float distance = Vector3.Distance(transform.position, attachedArrow.position);

        arrow.GetComponent<Rigidbody>().velocity = arrow.transform.forward * distance * maxShootSpeed * velocity;
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
        RWVR_InteractionObject rwvrobj = GetComponent<RWVR_InteractionObject>();
        //Debug.Log(rwvrobj.currentController.ToString());
        rwvrobj.currentController.Vibrate(3500);
        arrow.GetComponent<RealArrow>().Launch();
        arrow.transform.GetComponentInChildren<ParticleSystem>().gameObject.SetActive(true);

        Disarm();
    }
}
