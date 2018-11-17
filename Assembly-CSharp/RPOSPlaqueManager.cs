using System;
using UnityEngine;

// Token: 0x020004CF RID: 1231
public class RPOSPlaqueManager : MonoBehaviour
{
	// Token: 0x06002A5B RID: 10843 RVA: 0x0009D5F8 File Offset: 0x0009B7F8
	public void Awake()
	{
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			transform.gameObject.SetActive(false);
		}
	}

	// Token: 0x06002A5C RID: 10844 RVA: 0x0009D66C File Offset: 0x0009B86C
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

	// Token: 0x0400148F RID: 5263
	public GameObject coldPlaque;

	// Token: 0x04001490 RID: 5264
	public GameObject bleedingPlaque;
}
