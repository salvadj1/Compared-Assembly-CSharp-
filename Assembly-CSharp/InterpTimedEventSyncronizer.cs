using System;
using UnityEngine;

// Token: 0x020002B2 RID: 690
public class InterpTimedEventSyncronizer : MonoBehaviour
{
	// Token: 0x17000723 RID: 1827
	// (get) Token: 0x060018B1 RID: 6321 RVA: 0x00061C20 File Offset: 0x0005FE20
	// (set) Token: 0x060018B2 RID: 6322 RVA: 0x00061C28 File Offset: 0x0005FE28
	internal static bool paused
	{
		get
		{
			return InterpTimedEventSyncronizer.syncronizationPaused;
		}
		set
		{
			InterpTimedEventSyncronizer.syncronizationPaused = value;
			if (InterpTimedEventSyncronizer.singleton)
			{
				InterpTimedEventSyncronizer.singleton.enabled = !InterpTimedEventSyncronizer.syncronizationPaused;
			}
		}
	}

	// Token: 0x17000724 RID: 1828
	// (get) Token: 0x060018B3 RID: 6323 RVA: 0x00061C54 File Offset: 0x0005FE54
	internal static bool available
	{
		get
		{
			return InterpTimedEventSyncronizer.exists;
		}
	}

	// Token: 0x060018B4 RID: 6324 RVA: 0x00061C5C File Offset: 0x0005FE5C
	private void Awake()
	{
		if (InterpTimedEventSyncronizer.singleton)
		{
			Debug.LogWarning("Destroying old singleton!", InterpTimedEventSyncronizer.singleton.gameObject);
			Object.Destroy(InterpTimedEventSyncronizer.singleton);
		}
		InterpTimedEventSyncronizer.singleton = this;
		InterpTimedEventSyncronizer.exists = true;
	}

	// Token: 0x060018B5 RID: 6325 RVA: 0x00061C98 File Offset: 0x0005FE98
	private void Update()
	{
		InterpTimedEvent.Catchup();
	}

	// Token: 0x060018B6 RID: 6326 RVA: 0x00061CA0 File Offset: 0x0005FEA0
	private void OnDestroy()
	{
		if (InterpTimedEventSyncronizer.singleton == this)
		{
			try
			{
				InterpTimedEvent.Clear(false);
			}
			finally
			{
				InterpTimedEventSyncronizer.singleton = null;
				InterpTimedEventSyncronizer.exists = false;
			}
		}
	}

	// Token: 0x04000D27 RID: 3367
	private static InterpTimedEventSyncronizer singleton;

	// Token: 0x04000D28 RID: 3368
	private static bool syncronizationPaused;

	// Token: 0x04000D29 RID: 3369
	private static bool exists;
}
