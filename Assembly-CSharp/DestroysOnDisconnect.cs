using System;
using System.Collections.Generic;
using uLink;
using UnityEngine;

// Token: 0x020002A7 RID: 679
public sealed class DestroysOnDisconnect : MonoBehaviour
{
	// Token: 0x0600186B RID: 6251 RVA: 0x00060748 File Offset: 0x0005E948
	private void Awake()
	{
		if (!this.inList)
		{
			this.inList = true;
			try
			{
				DestroysOnDisconnect.List.all.Add(this);
			}
			catch
			{
				this.inList = false;
				throw;
			}
		}
	}

	// Token: 0x0600186C RID: 6252 RVA: 0x000607A4 File Offset: 0x0005E9A4
	private void OnDestroy()
	{
		if (this.inList)
		{
			try
			{
				if (!DestroysOnDisconnect.List.all.Remove(this))
				{
					Debug.LogWarning("serious problem, script reload?", this);
				}
			}
			finally
			{
				this.inList = false;
			}
		}
	}

	// Token: 0x0600186D RID: 6253 RVA: 0x00060800 File Offset: 0x0005EA00
	private void DestroyManually()
	{
		if (this.inList)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0600186E RID: 6254 RVA: 0x00060818 File Offset: 0x0005EA18
	public static void ApplyToGameObject(GameObject gameObject)
	{
		DestroysOnDisconnect component = gameObject.GetComponent<DestroysOnDisconnect>();
		if (!component)
		{
			gameObject.AddComponent<DestroysOnDisconnect>();
		}
	}

	// Token: 0x0600186F RID: 6255 RVA: 0x00060840 File Offset: 0x0005EA40
	private void uLink_OnDisconnectedFromServer(NetworkDisconnection blowme)
	{
		this.DestroyManually();
	}

	// Token: 0x06001870 RID: 6256 RVA: 0x00060848 File Offset: 0x0005EA48
	public static void OnDisconnectedFromServer()
	{
		if (DestroysOnDisconnect.ListClassInitialized && DestroysOnDisconnect.List.all.Count > 0)
		{
			foreach (DestroysOnDisconnect destroysOnDisconnect in DestroysOnDisconnect.List.all.ToArray())
			{
				if (destroysOnDisconnect)
				{
					Object.Destroy(destroysOnDisconnect.gameObject);
				}
			}
		}
	}

	// Token: 0x04000D02 RID: 3330
	private static bool ListClassInitialized;

	// Token: 0x04000D03 RID: 3331
	private bool inList;

	// Token: 0x020002A8 RID: 680
	private static class List
	{
		// Token: 0x06001871 RID: 6257 RVA: 0x000608A8 File Offset: 0x0005EAA8
		static List()
		{
			DestroysOnDisconnect.ListClassInitialized = true;
		}

		// Token: 0x04000D04 RID: 3332
		public static readonly List<DestroysOnDisconnect> all = new List<DestroysOnDisconnect>();
	}
}
