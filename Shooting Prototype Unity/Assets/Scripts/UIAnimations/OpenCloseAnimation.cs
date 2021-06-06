using System.Collections;
using UnityEngine;

public class OpenCloseAnimation : MonoBehaviour
{
	RectTransform rectTransform;
	Vector2 previousScale;
	Vector2 previousLocalPos;

	[Header("Animation Time")]
	public float duration;
	public float delay;

	[Header("Local Movement")]
	public float xOffset;
	public float yOffset;

	[Header("Movement Loop")]
	public bool mLoop = false;
	public RectTransform[] points;
	public bool inverse = false;

	[Header("Scale")]
	public bool scale = false;

	[Header("Whole Rotation")]
	public bool hasWholeRotation = false;

	[Header("SFX")]
	public bool applySFX;
	public AudioClip openCloseSFX;

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		previousScale = rectTransform.localScale;
		previousLocalPos = transform.localPosition;
	}

	private void OnEnable()
	{
		if (scale)
		{
			transform.localScale = Vector3.zero;
			LeanTween.scale(rectTransform, previousScale, duration).setDelay(delay).setIgnoreTimeScale(true);

			//if (SoundManager.Instance != null && applySFX) { SoundManager.Instance.PlaySFX(openCloseSFX); }
		}

		if (xOffset != 0f)
		{
			LeanTween.moveLocalX(gameObject, previousLocalPos.x + xOffset, 0f).setIgnoreTimeScale(true);
			LeanTween.moveLocalX(gameObject, previousLocalPos.x, duration).setDelay(delay).setIgnoreTimeScale(true);

			//if (SoundManager.Instance != null && applySFX) { SoundManager.Instance.PlaySFX(openCloseSFX); }
		}

		if (yOffset != 0f)
		{
			LeanTween.moveLocalY(gameObject, previousLocalPos.y + yOffset, 0f).setIgnoreTimeScale(true);
			LeanTween.moveLocalY(gameObject, previousLocalPos.y, duration).setDelay(delay).setIgnoreTimeScale(true);

			//if (SoundManager.Instance != null && applySFX) { SoundManager.Instance.PlaySFX(openCloseSFX); }
		}

		if (hasWholeRotation)
        {
			RotateFirst();
        }

		if (mLoop)
        {
			RotateLastHalfInstant();
        }
	}

	private void RotateFirst()
    {
		LeanTween.rotate(gameObject, Vector3.forward * 180f, duration).setOnComplete(RotateSecond);
	}

	private void RotateSecond()
	{
		LeanTween.rotate(gameObject, Vector3.forward * 360f, duration).setOnComplete(RotateThird);
	}

	private void RotateThird()
    {
		LeanTween.rotate(gameObject, Vector3.back * 180f, duration).setOnComplete(RotateFour);
    }

	private void RotateFour()
    {
		LeanTween.rotate(gameObject, Vector3.back * 360f, duration).setOnComplete(RotateFirst);
    }

	private void RotateHalfInstant()
    {
		if (!inverse)
        {
			LeanTween.rotate(gameObject, Vector3.forward * 180f, 0f);
			LeanTween.moveLocalY(gameObject, points[0].localPosition.y, duration).setOnComplete(RotateLastHalfInstant);
		}
		else
        {
			LeanTween.rotate(gameObject, Vector3.forward * 360f, 0f);
			LeanTween.moveLocalY(gameObject, points[0].localPosition.y, duration).setOnComplete(RotateLastHalfInstant);
		}
	}

	private void RotateLastHalfInstant()
    {
		if (!inverse)
        {
			LeanTween.rotate(gameObject, Vector3.forward * 360f, 0f);
			LeanTween.moveLocalY(gameObject, points[1].localPosition.y, duration).setOnComplete(RotateHalfInstant);
		}
		else
        {
			LeanTween.rotate(gameObject, Vector3.forward * 180f, 0f);
			LeanTween.moveLocalY(gameObject, points[1].localPosition.y, duration).setOnComplete(RotateHalfInstant);
		}
	}
}