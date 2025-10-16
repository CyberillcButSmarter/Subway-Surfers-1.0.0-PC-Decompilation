using UnityEngine;

[RequireComponent(typeof(UILabel))]
public class UIVersionLabel : MonoBehaviour
{
	private void Start()
	{
		GetComponent<UILabel>().text = "0.0.1-PreAlpha";
	}
}
