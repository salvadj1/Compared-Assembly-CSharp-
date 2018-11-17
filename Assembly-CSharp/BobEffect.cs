using System;
using Facepunch.Precision;
using UnityEngine;

// Token: 0x02000288 RID: 648
public abstract class BobEffect : ScriptableObject
{
	// Token: 0x0600175F RID: 5983
	protected abstract void InitializeNonSerializedData();

	// Token: 0x06001760 RID: 5984
	protected abstract bool OpenData(out global::BobEffect.Data data);

	// Token: 0x06001761 RID: 5985
	protected abstract void CloseData(global::BobEffect.Data data);

	// Token: 0x06001762 RID: 5986
	protected abstract global::BOBRES SimulateData(ref global::BobEffect.Context ctx);

	// Token: 0x06001763 RID: 5987 RVA: 0x0005741C File Offset: 0x0005561C
	public bool Create(out global::BobEffect.Data data)
	{
		if (!this.loaded)
		{
			this.InitializeNonSerializedData();
			this.loaded = true;
		}
		return this.OpenData(out data);
	}

	// Token: 0x06001764 RID: 5988 RVA: 0x00057440 File Offset: 0x00055640
	public void Destroy(ref global::BobEffect.Data data)
	{
		if (this.loaded && data != null)
		{
			this.CloseData(data);
			data = null;
		}
	}

	// Token: 0x06001765 RID: 5989 RVA: 0x00057460 File Offset: 0x00055660
	public global::BOBRES Simulate(ref global::BobEffect.Context ctx)
	{
		if (this.loaded)
		{
			return this.SimulateData(ref ctx);
		}
		return global::BOBRES.ERROR;
	}

	// Token: 0x04000C46 RID: 3142
	[NonSerialized]
	private bool loaded;

	// Token: 0x02000289 RID: 649
	public class Data
	{
		// Token: 0x06001767 RID: 5991 RVA: 0x00057480 File Offset: 0x00055680
		public virtual global::BobEffect.Data Clone()
		{
			return (global::BobEffect.Data)base.MemberwiseClone();
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x00057490 File Offset: 0x00055690
		public virtual void CopyDataTo(global::BobEffect.Data target)
		{
			target.force = this.force;
			target.torque = this.torque;
		}

		// Token: 0x04000C47 RID: 3143
		public Vector3G force;

		// Token: 0x04000C48 RID: 3144
		public Vector3G torque;

		// Token: 0x04000C49 RID: 3145
		public global::BobEffect effect;
	}

	// Token: 0x0200028A RID: 650
	public struct Context
	{
		// Token: 0x04000C4A RID: 3146
		public double dt;

		// Token: 0x04000C4B RID: 3147
		public global::BobEffect.Data data;
	}
}
