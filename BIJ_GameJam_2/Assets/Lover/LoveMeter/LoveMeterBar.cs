using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoveMeterBar : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private Image foregroundImage;

    [SerializeField]
    private float updateSpeedSeconds = 0.5f;

    [SerializeField]
    private float hideCanvasTimeSeconds = 0.5f;

    private Coroutine hideCanvasCoroutine = null;

    private void Awake()
    {
        GetComponentInParent<LoveMeter>().OnLovePctChanged += HandleLoveMeterChanged;
        canvas.enabled = false;
    }

    private void HandleLoveMeterChanged(float pct)
    {
        StartCoroutine(ChangeToPct(pct));
    }

    private IEnumerator ChangeToPct(float pct)
    {
        canvas.enabled = true;

        float preChangePct = foregroundImage.fillAmount;
        float elapsed = 0f;

        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / updateSpeedSeconds);
            yield return null;
        }

        foregroundImage.fillAmount = pct;

        if (hideCanvasCoroutine != null)
        {
            StopCoroutine(hideCanvasCoroutine);
        }
        hideCanvasCoroutine = StartCoroutine(HideCanvas());   
    }

    private IEnumerator HideCanvas()
    {
        yield return new WaitForSeconds(hideCanvasTimeSeconds);
        canvas.enabled = false;
        yield return null;
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }
}
