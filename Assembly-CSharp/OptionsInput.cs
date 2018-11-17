using System;
using UnityEngine;

// Token: 0x020004B1 RID: 1201
public class OptionsInput : MonoBehaviour
{
	// Token: 0x06002913 RID: 10515 RVA: 0x000966FC File Offset: 0x000948FC
	private void Start()
	{
		foreach (global::GameInput.GameButton button in global::GameInput.Buttons)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(this.lineObject);
			gameObject.transform.parent = base.transform;
			gameObject.GetComponent<global::OptionsKeyBinding>().Setup(button);
		}
	}

	// Token: 0x040013B7 RID: 5047
	public GameObject lineObject;
}
