using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeToFromBlack : MonoBehaviour
{
    public bool fadingActivated;

    public enum FadeState {black = 0, white = 1, transparent = 2}
    private FadeState currentFadeState = FadeState.transparent;
    private SpriteRenderer srend;
    private Color[] fadeColors = { Color.black, Color.white, new Color(0, 0, 0, 0) };

    
    // Start is called before the first frame update
    void Start()
    {
        srend = GetComponent<SpriteRenderer>();

        UnityStandardAssets.Characters.FirstPerson.HullController.OnDeath += StartColorTransition;

    }


    void StartColorTransition(bool isAlive)
    {
        if (fadingActivated)
        {
            if (!isAlive)
            {
                StartCoroutine(ColorTransition(FadeState.black));
            }
            else
            {
                StartCoroutine(ColorTransition(FadeState.transparent));
            }
        }
    }

    IEnumerator ColorTransition(FadeState newFadeState)
    {
        while (srend.color != fadeColors[(int)newFadeState])
        {
            srend.color = new Color(Mathf.Lerp(srend.color.r, fadeColors[(int)newFadeState].r, 0.2f),
                Mathf.Lerp(srend.color.g, fadeColors[(int)newFadeState].g, 0.2f),
                Mathf.Lerp(srend.color.b, fadeColors[(int)newFadeState].b, 0.2f),
                Mathf.Lerp(srend.color.a, fadeColors[(int)newFadeState].a, 0.2f));
            yield return null;
        }


    }


}
