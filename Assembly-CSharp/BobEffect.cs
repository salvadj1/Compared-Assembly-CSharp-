using System;
using Facepunch.Precision;
using UnityEngine;

// Token: 0x02000255 RID: 597
public abstract class BobEffect : ScriptableObject
{
	// Token: 0x0600160B RID: 5643
	protected abstract void InitializeNonSerializedData();

	// Token: 0x0600160C RID: 5644
	protected abstract bool OpenData(out BobEffect.Data data);

	// Token: 0x0600160D RID: 5645
	protected abstract void CloseData(BobEffect.Data data);

	// Token: 0x0600160E RID: 5646
	protected abstract BOBRES SimulateData(ref BobEffect.Context ctx);

	// Token: 0x0600160F RID: 5647 RVA: 0x00053074 File Offset: 0x00051274
	public bool Create(out BobEffect.Data data)
	{
		if (!this.loaded)
		{
			this.InitializeNonSerializedData();
			this.loaded = true;
		}
		return this.OpenData(out data);
	}

	// Token: 0x06001610 RID: 5648 RVA: 0x00053098 File Offset: 0x00051298
	public void Destroy(ref BobEffect.Data data)
	{
		if (this.loaded && data != null)
		{
			this.CloseData(data);
			data = null;
		}
	}

	// Token: 0x06001611 RID: 5649 RVA: 0x000530B8 File Offset: 0x000512B8
	public BOBRES Simulate(ref BobEffect.Context ctx)
	{
		if (this.loaded)
		{
			return this.SimulateData(ref ctx);
		}
		return BOBRES.ERROR;
	}

	// Token: 0x04000B23 RID: 2851
	[NonSerialized]
	private bool loaded;

	// Token: 0x02000256 RID: 598
	public class Data
	{
		// Token: 0x06001613 RID: 5651 RVA: 0x000530D8 File Offset: 0x000512D8
		public virtual BobEffect.Data Clone()
		{
			return (BobEffect.Data)base.MemberwiseClone();
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x000530E8 File Offset: 0x000512E8
		public virtual void CopyDataTo(BobEffect.Data target)
		{
			target.force = this.force;
			target.torque = this.torque;
		}

		// Token: 0x04000B24 RID: 2852
		public Vector3G force;

		// Token: 0x04000B25 RID: 2853
		public Vector3G torque;

		// Token: 0x04000B26 RID: 2854
		public BobEffect effect;
	}

	// Token: 0x02000257 RID: 599
	public struct Context
	{
		// Token: 0x04000B27 RID: 2855
		public double dt;

		// Token: 0x04000B28 RID: 2856
		public BobEffect.Data data;
	}
}
