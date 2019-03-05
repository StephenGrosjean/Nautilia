using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    [SerializeField] private List<Sprite> frames;
    [SerializeField] private float waitEachFrame;

    [SerializeField] private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("PlayAnimation");
    }

    //Animate sprite without using the animator (Save FPS and file size)
    IEnumerator PlayAnimation() {
        while (true) {
            foreach(Sprite frame in frames) {
                spriteRenderer.sprite = frame;
                yield return new WaitForSeconds(waitEachFrame);
            }
        }

    }
}
