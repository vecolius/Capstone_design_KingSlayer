using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f,10f)]
    private float fadeTime;
    [SerializeField]
    private AnimationCurve fadeCurve;
    private Image image;
    private Color color;
    public static FadeEffect instance = null;


    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        image = GetComponent<Image>();
        color = image.color;
    }

    public void Fadeset()
    {
        color.a=255;
        image.color = color;
        StartCoroutine(Fade(1,0));
        
    }
    private IEnumerator Fade(float start, float end)
    {

        float currentTime = 0.0f;
        float percent = 0.0f;
        while ( percent<1)
        {
            currentTime+= Time.deltaTime;
            percent = currentTime / fadeTime;

            color.a = Mathf.Lerp(start, end, fadeCurve.Evaluate(percent));
            image.color = color;

            yield return null;
        }
    }
}
