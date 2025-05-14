using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Background : MonoBehaviour
{
    Tilemap bg;
    Color morningColor = new Color(1.0f, 0.7f, 0.5f); // ¿¬ÇÑ ÁÖÈ²
    Color noonColor = new Color(1.0f, 1.0f, 1.0f);     // ÇÏ¾á»ö
    Color eveningColor = new Color(0.5f, 0.3f, 0.7f);  // º¸¶ùºû
    Color nightColor = new Color(0.1f, 0.1f, 0.2f);    // ¾îµÎ¿î ¹ã

    float timeCycleDuration = 60f;

    // Start is called before the first frame update
    void Start()
    {
        bg = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.time % timeCycleDuration;
        float t = time / timeCycleDuration;

        Color currentColor;

        if (t < 0.25f) // ¾ÆÄ§ ¡æ ³·
        {
            float localT = t / 0.25f;
            currentColor = Color.Lerp(morningColor, noonColor, localT);
        }
        else if (t < 0.5f) // ³· ¡æ Àú³á
        {
            float localT = (t - 0.25f) / 0.25f;
            currentColor = Color.Lerp(noonColor, eveningColor, localT);
        }
        else if (t < 0.75f) // Àú³á ¡æ ¹ã
        {
            float localT = (t - 0.5f) / 0.25f;
            currentColor = Color.Lerp(eveningColor, nightColor, localT);
        }
        else // ¹ã ¡æ ¾ÆÄ§
        {
            float localT = (t - 0.75f) / 0.25f;
            currentColor = Color.Lerp(nightColor, morningColor, localT);
        }

        bg.color = currentColor;
    }
}
