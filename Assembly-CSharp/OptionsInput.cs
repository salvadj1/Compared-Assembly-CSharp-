using System;
using UnityEngine;

// Token: 0x02000400 RID: 1024
public class OptionsInput : MonoBehaviour
{
	// Token: 0x0600259B RID: 9627 RVA: 0x000908C4 File Offset: 0x0008EAC4
	private void Start()
	{
		foreach (GameInput.GameButton button in GameInput.Buttons)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(this.lineObject);
			gameObject.transform.parent = base.transform;
			gameObject.GetComponent<OptionsKeyBinding>().Setup(button);
		}
	}

	// Token: 0x0400123A RID: 4666
	public GameObject lineObject;
}
