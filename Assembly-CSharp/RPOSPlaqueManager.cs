using System;
using UnityEngine;

// Token: 0x0200041A RID: 1050
public class RPOSPlaqueManager : MonoBehaviour
{
	// Token: 0x060026D1 RID: 9937 RVA: 0x00097734 File Offset: 0x00095934
	public void Awake()
	{
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			transform.gameObject.SetActive(false);
		}
	}

	// Token: 0x060026D2 RID: 9938 RVA: 0x000977A8 File Offset: 0x000959A8
	public void SetPlaqueActive(string plaqueName, bool on)
	{
		GameObject gameObject = null;
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.name == plaqueName)
			{
				gameObject = transform.gameObject;
			}
		}
		if (gameObject && gameObject.activeSelf != on)
		{
			gameObject.SetActive(on);
			float num = 21f;
			foreach (object obj2 in base.transform)
			{
				Transform transform2 = (Transform)obj2;
				if (transform2.gameObject.activeSelf)
				{
					transform2.SetLocalPositionY(num);
					num += 28f;
				}
			}
		}
	}

	// Token: 0x0400130F RID: 4879
	public GameObject coldPlaque;

	// Token: 0x04001310 RID: 4880
	public GameObject bleedingPlaque;
}
