using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
// 주석
public class ChatSystem : MonoBehaviour
{
    public Queue<string> sentences;
    public string currentSentences;
    public TextMeshPro text;
    public GameObject quad;
    public Renderer ren;

    public void Awake() {
        ren = GetComponent<Renderer>();
    }
    public void Ondialogue(string[] lines, Transform chatPoint)
    {
        transform.position = chatPoint.position;
        sentences = new Queue<string>();
        sentences.Clear();
        foreach (var line in lines)
        {
            sentences.Enqueue(line);
        }
        StartCoroutine(DialongueFlow(chatPoint));
    }

    IEnumerator DialongueFlow(Transform chatPoint)
    {
        yield return null;
        while(sentences.Count > 0)
        {
            currentSentences = sentences.Dequeue();
            text.text = currentSentences;
            float x = text.preferredWidth;
            x = (x > 3) ? 3:x + 0.3f;
            quad.transform.localScale = new Vector2(x,text.preferredHeight + 0.3f);
            transform.position = new Vector2(chatPoint.position.x,chatPoint.position.y+text.preferredHeight/2);
            ren.sortingLayerName = "UI";
            yield return new WaitForSeconds(3f);
        }
        Destroy(gameObject);
    }
}
