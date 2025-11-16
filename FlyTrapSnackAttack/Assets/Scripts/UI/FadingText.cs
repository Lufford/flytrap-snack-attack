using UnityEngine;
using TMPro;
using System.Collections;

public class FadingText : MonoBehaviour
{
    private TextMeshProUGUI text;
    private float fadeTime = 3.5f;
    private float delay = 1.5f;

    private Color color;

    void Start()
    {
        if (text == null)
        {
            text = GetComponent<TextMeshProUGUI>();
        }
        
        color = text.color;
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        yield return new WaitForSeconds(delay);

        float i = 0f;

        while (i < fadeTime)
        {
            i += Time.deltaTime;
            float result = Mathf.Lerp(1f, 0f, i / fadeTime);
            text.color = new Color(color.r, color.g, color.b, result);
            
            yield return null;
        }

        Destroy(gameObject);
    }
}
