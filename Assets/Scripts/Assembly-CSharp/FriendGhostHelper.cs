using System.Collections;
using UnityEngine;

public class FriendGhostHelper : MonoBehaviour
{
	public UISlicedSprite background;

	public UISlicedSprite frame;

	public UILabel points;

	public UITexture picture;

	private Transform _cachedTransform;

	private Vector3 _resetPosition = new Vector3(80f, 0f, 0f);

	private Vector3 _activePosition = Vector3.zero;

	private Vector3 _moveOutPosition = new Vector3(0f, -140f, 0f);

	private float _backgroundAlphaDefault;

	private float _frameAlphaDefault;

	private float _pointsAlphaDefault;

	private float _pictureAlphaDefault;

	private bool inited;

	private bool _gameRunning;

	public bool noFriendsLeftToGhost;

	private FriendGhostHandler handler;

	public bool animatingNow;

	private void Awake()
	{
		if (!inited)
		{
			Init();
		}
	}

	private void Init()
	{
		_backgroundAlphaDefault = background.alpha;
		_frameAlphaDefault = frame.alpha;
		_pointsAlphaDefault = points.alpha;
		_pictureAlphaDefault = picture.alpha;
		_cachedTransform = base.transform;
		picture.material = new Material(Shader.Find("Unlit/Transparent Colored"));
		inited = true;
		handler = _cachedTransform.parent.GetComponent<FriendGhostHandler>();
	}

	public void NewGame()
	{
		if (!inited)
		{
			Init();
		}
		_gameRunning = true;
		_cachedTransform.localPosition = _resetPosition;
		background.alpha = _backgroundAlphaDefault;
		frame.alpha = _frameAlphaDefault;
		points.alpha = _pointsAlphaDefault;
		picture.alpha = _pictureAlphaDefault;
		noFriendsLeftToGhost = false;
	}

	public void AnimateIn()
	{
		if (!noFriendsLeftToGhost)
		{
			StartCoroutine(_AnimateIn());
		}
	}

	public void AnimateOut()
	{
		if (_gameRunning)
		{
			StartCoroutine(_AnimateOut());
		}
	}

	public void NoFriendsLeft()
	{
		_cachedTransform.localPosition = _resetPosition;
		background.alpha = _backgroundAlphaDefault;
		frame.alpha = _frameAlphaDefault;
		points.alpha = _pointsAlphaDefault;
		picture.alpha = _pictureAlphaDefault;
		noFriendsLeftToGhost = true;
	}

	public void GameOver()
	{
		_gameRunning = false;
		_cachedTransform.localPosition = _resetPosition;
		background.alpha = _backgroundAlphaDefault;
		frame.alpha = _frameAlphaDefault;
		points.alpha = _pointsAlphaDefault;
		picture.alpha = _pictureAlphaDefault;
	}

	private IEnumerator _AnimateIn()
	{
		animatingNow = true;
		float duration = 0.5f;
		float factor = 0f;
		while (factor < 1f && _gameRunning)
		{
			factor += Time.deltaTime / duration;
			factor = Mathf.Clamp01(factor);
			_cachedTransform.localPosition = Vector3.Lerp(_resetPosition, _activePosition, factor);
			yield return null;
		}
		if (!_gameRunning)
		{
			_cachedTransform.localScale = _resetPosition;
		}
		else
		{
			_cachedTransform.localPosition = _activePosition;
		}
		animatingNow = false;
	}

	private IEnumerator _AnimateOut()
	{
		animatingNow = true;
		float duration = 0.5f;
		float factor = 0f;
		while (factor < 1f && _gameRunning)
		{
			factor += Time.deltaTime / duration;
			factor = Mathf.Clamp01(factor);
			_cachedTransform.localPosition = Vector3.Lerp(_activePosition, _moveOutPosition, factor);
			background.alpha = Mathf.Lerp(_backgroundAlphaDefault, 0f, factor);
			frame.alpha = Mathf.Lerp(_frameAlphaDefault, 0f, factor);
			points.alpha = Mathf.Lerp(_pointsAlphaDefault, 0f, factor);
			picture.alpha = Mathf.Lerp(_pictureAlphaDefault, 0f, factor);
			yield return null;
		}
		_cachedTransform.localPosition = _resetPosition;
		background.alpha = _backgroundAlphaDefault;
		frame.alpha = _frameAlphaDefault;
		points.alpha = _pointsAlphaDefault;
		picture.alpha = _pictureAlphaDefault;
		animatingNow = false;
		handler.FinishedAnimatingOut();
	}
}
