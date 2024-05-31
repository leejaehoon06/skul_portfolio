using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScaleSprite : MonoBehaviour
{
    [SerializeField]
    Sprite[] sprite;
    UnityEngine.UI.Image image;
    private void Start()
    {
        image = GetComponent<UnityEngine.UI.Image>();
        UIOff();
    }
    private void OnEnable()
    {
        if (image != null)
        {
            StartCoroutine(Animation());
        }
    }

    IEnumerator Animation()
    {
        for (int i=0;i<sprite.Length; i++) 
        {
            image.sprite = sprite[i];
            image.SetNativeSize();
            image.rectTransform.pivot = new Vector2(image.sprite.pivot.x / (image.sprite.bounds.size.x * image.sprite.pixelsPerUnit),
                image.sprite.pivot.y / (image.sprite.bounds.size.y * image.sprite.pixelsPerUnit));
            yield return new WaitForSeconds(2f / 60f);
        }
        UIOff();
    }
    void UIOff()
    {
        gameObject.SetActive(false);
    }
}
