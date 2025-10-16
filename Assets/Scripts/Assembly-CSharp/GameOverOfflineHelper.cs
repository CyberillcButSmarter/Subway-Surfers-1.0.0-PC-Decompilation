using System.Collections;
using UnityEngine;

public class GameOverOfflineHelper : MonoBehaviour
{
	public GameObject FacebookLoginButton;

	public GameObject GameCenterLoginButton;

	public UISprite FacebookIcon;

	public UISprite GameCenterIcon;

	public void EnableButtons()
	{
		FacebookLoginButton.GetComponent<UIButtonColor>().defaultColor = Color.white;
		GameCenterLoginButton.GetComponent<UIButtonColor>().defaultColor = Color.white;
		NGUITools.AddWidgetCollider(FacebookLoginButton);
		NGUITools.AddWidgetCollider(GameCenterLoginButton);
		StartCoroutine(AnimateAlpha(FacebookIcon, GameCenterIcon, 0.2f, 1f));
	}

	public void DisableButtons()
	{
		if (FacebookLoginButton.GetComponent<Collider>() != null)
		{
			Object.Destroy(FacebookLoginButton.GetComponent<Collider>());
		}
		if (GameCenterLoginButton.GetComponent<Collider>() != null)
		{
			Object.Destroy(GameCenterLoginButton.GetComponent<Collider>());
		}
		FacebookIcon.alpha = 0.5f;
		GameCenterIcon.alpha = 0.5f;
	}

	private IEnumerator AnimateAlpha(UISprite sprite1, UISprite sprite2, float duration, float toAlpha)
	{
		float fromAlpha = sprite1.alpha;
		float factor = 0f;
		while (factor < 1f)
		{
			factor += Time.deltaTime / duration;
			factor = Mathf.Clamp01(factor);
			sprite1.alpha = Mathf.Lerp(fromAlpha, toAlpha, factor);
			sprite2.alpha = Mathf.Lerp(fromAlpha, toAlpha, factor);
			yield return null;
		}
		sprite1.alpha = toAlpha;
		sprite2.alpha = toAlpha;
	}
}
