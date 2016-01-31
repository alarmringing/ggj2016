﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
///  Crosshair allows you to point at objects that are interactable and customize the crosshair with sounds.
/// </summary>
public class Crosshair : MonoBehaviour
{
    
    #region OBJECT REFERENCES

    public GameObject crosshairs;
    public Text interactionText;
    public Texture2D standardCrosshairs;
    public Texture2D copyCodeCrosshair;
    public Texture2D accessibleObjectCrosshair;
    public Texture2D interactCrosshair;

    #endregion

    private float timeLastFired = 0.0f;
    private float delayTime = 0.5f;

    public AudioClip shootSound;
    public AudioClip hoverSound;
    public AudioClip[] execSounds;
    public AudioClip copySound;
    public AudioClip errorSound;
    public AudioClip selectSound;
    public AudioClip copiedSound;

    private RaycastHit rayHit;
    private bool raycastReturn;
    private bool isEnabled = true;

    public void Enable(bool enable)
    {
        isEnabled = enable;
        crosshairs.SetActive(enable);
        interactionText.gameObject.SetActive(enable);
    }

    /// <summary>
    /// Plays a randomized sound clip.
    /// </summary>
    /// <param name="soundName"></param>
    /// <returns></returns>
    private AudioClip RandomizeSound(string soundName)
    {
        if (execSounds.Length > 0)
        {
            return execSounds[Random.Range(0, execSounds.Length)];
        }
        else
        {
            return shootSound;
        }
    }

    private void HoverSound()
    {
        GetComponent<AudioSource>().PlayOneShot(hoverSound);
    }

    private void ClickSound()
    {
        GetComponent<AudioSource>().PlayOneShot(shootSound);
    }

    private void ErrorSound()
    {
        GetComponent<AudioSource>().PlayOneShot(errorSound);
    }

    

    public GameObject targetObject;
    public GameObject lastTargetObject;
    public string triggerMessage = "";

    private void Update()
    {
        if (isEnabled)
        {
            // reset cross hairs to normal unless we detect something else
            SwapCrosshairs(standardCrosshairs);

            raycastReturn = Physics.Raycast(transform.position, transform.forward, out rayHit, Mathf.Infinity);
            // check non-layer filtered mouseover here - copyableCode etc, change crosshair color
            if ((raycastReturn))
            {
                ActivateTriggerCrosshair aTrigger = rayHit.transform.gameObject.GetComponent<ActivateTriggerCrosshair>();
                if (aTrigger != null)
                {
                    if (interactCrosshair != null)
                    {
                        SwapCrosshairs(interactCrosshair);
                    }
                    else
                    {
                        if (crosshairs != null)
                        {
                            crosshairs.GetComponent<RawImage>().texture = null;
                        }
                    }
                    targetObject = rayHit.transform.gameObject;
                    triggerMessage = aTrigger.activateOnShootMessage;
                    if (lastTargetObject == null || targetObject != lastTargetObject)
                    {
                        HoverSound();
                    }
                    lastTargetObject = targetObject;
                }
                else
                {
                    targetObject = null;
                    triggerMessage = "";
                }
            }

            // on mouse press
            if (Input.GetButtonDown("Fire1"))
            {
                if (debug) Debug.Log("Fire1");
                // check cool down
                if (Time.time > timeLastFired + delayTime)
                {

                    if (targetObject != null)
                    {
                        if (debug) Debug.Log("Shoot");
                        targetObject.SendMessage("Shoot");
                        ClickSound();
                        timeLastFired = Time.time;
                        targetObject = null;
                    }
                    else
                    {
                        ErrorSound();
                    }
                }
            }

            if (triggerMessage != "")
            {
                interactionText.text = triggerMessage;
            }
            else
            {
                interactionText.text = "";
            }
        }
    }
	
	public bool debug = false;

    /// <summary>
    /// Crosshair switches to indicate what is in front of you.
    /// </summary>
    /// <param name="crosshairTexture"></param>
    void SwapCrosshairs(Texture2D crosshairTexture)
	{
		if(crosshairTexture!=null)
		{
			crosshairs.GetComponent<RawImage>().texture = crosshairTexture;
		}
    }
}