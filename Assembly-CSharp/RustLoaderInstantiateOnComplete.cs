using System;
using UnityEngine;

// Token: 0x0200002F RID: 47
public class RustLoaderInstantiateOnComplete : MonoBehaviour
{
	// Token: 0x060001EE RID: 494 RVA: 0x0000B4E0 File Offset: 0x000096E0
	private void OnRustReady()
	{
		if (this.prefabs != null)
		{
			foreach (GameObject gameObject in this.prefabs)
			{
				if (gameObject)
				{
					this.InstantiatePrefab(gameObject);
				}
			}
		}
	}

	// Token: 0x060001EF RID: 495 RVA: 0x0000B52C File Offset: 0x0000972C
	private void InstantiatePrefab(GameObject prefab)
	{
		try
		{
			Object.Instantiate(prefab).name = prefab.name;
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
		}
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x0000B578 File Offset: 0x00009778
	private void Reset()
	{
		Object[] array = Object.FindObjectsOfType(typeof(RustLoader));
		if (array.Length > 0)
		{
			((RustLoader)array[0]).AddMessageReceiver(base.gameObject);
		}
	}

	// Token: 0x0400012E RID: 302
	public GameObject[] prefabs;
}
