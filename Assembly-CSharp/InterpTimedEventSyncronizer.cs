using System;
using UnityEngine;

// Token: 0x020002EF RID: 751
public class InterpTimedEventSyncronizer : MonoBehaviour
{
	// Token: 0x17000777 RID: 1911
	// (get) Token: 0x06001A41 RID: 6721 RVA: 0x00066594 File Offset: 0x00064794
	// (set) Token: 0x06001A42 RID: 6722 RVA: 0x0006659C File Offset: 0x0006479C
	internal static bool paused
	{
		get
		{
			return global::InterpTimedEventSyncronizer.syncronizationPaused;
		}
		set
		{
			global::InterpTimedEventSyncronizer.syncronizationPaused = value;
			if (global::InterpTimedEventSyncronizer.singleton)
			{
				global::InterpTimedEventSyncronizer.singleton.enabled = !global::InterpTimedEventSyncronizer.syncronizationPaused;
			}
		}
	}

	// Token: 0x17000778 RID: 1912
	// (get) Token: 0x06001A43 RID: 6723 RVA: 0x000665C8 File Offset: 0x000647C8
	internal static bool available
	{
		get
		{
			return global::InterpTimedEventSyncronizer.exists;
		}
	}

	// Token: 0x06001A44 RID: 6724 RVA: 0x000665D0 File Offset: 0x000647D0
	private void Awake()
	{
		if (global::InterpTimedEventSyncronizer.singleton)
		{
			Debug.LogWarning("Destroying old singleton!", global::InterpTimedEventSyncronizer.singleton.gameObject);
			Object.Destroy(global::InterpTimedEventSyncronizer.singleton);
		}
		global::InterpTimedEventSyncronizer.singleton = this;
		global::InterpTimedEventSyncronizer.exists = true;
	}

	// Token: 0x06001A45 RID: 6725 RVA: 0x0006660C File Offset: 0x0006480C
	private void Update()
	{
		global::InterpTimedEvent.Catchup();
	}

	// Token: 0x06001A46 RID: 6726 RVA: 0x00066614 File Offset: 0x00064814
	private void OnDestroy()
	{
		if (global::InterpTimedEventSyncronizer.singleton == this)
		{
			try
			{
				global::InterpTimedEvent.Clear(false);
			}
			finally
			{
				global::InterpTimedEventSyncronizer.singleton = null;
				global::InterpTimedEventSyncronizer.exists = false;
			}
		}
	}

	// Token: 0x04000E62 RID: 3682
	private static global::InterpTimedEventSyncronizer singleton;

	// Token: 0x04000E63 RID: 3683
	private static bool syncronizationPaused;

	// Token: 0x04000E64 RID: 3684
	private static bool exists;
}
