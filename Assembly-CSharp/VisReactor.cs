using System;
using UnityEngine;

// Token: 0x020003C7 RID: 967
public abstract class VisReactor : MonoBehaviour
{
	// Token: 0x170008CD RID: 2253
	// (set) Token: 0x0600246D RID: 9325 RVA: 0x0008B64C File Offset: 0x0008984C
	internal VisNode __visNode
	{
		set
		{
			this._visNode = value;
		}
	}

	// Token: 0x170008CE RID: 2254
	// (get) Token: 0x0600246E RID: 9326 RVA: 0x0008B658 File Offset: 0x00089858
	public VisNode node
	{
		get
		{
			return this._visNode;
		}
	}

	// Token: 0x170008CF RID: 2255
	// (get) Token: 0x0600246F RID: 9327 RVA: 0x0008B660 File Offset: 0x00089860
	protected VisNode self
	{
		get
		{
			return this._visNode;
		}
	}

	// Token: 0x06002470 RID: 9328 RVA: 0x0008B668 File Offset: 0x00089868
	protected virtual void React_SpectatedEnter()
	{
	}

	// Token: 0x06002471 RID: 9329 RVA: 0x0008B66C File Offset: 0x0008986C
	protected virtual void React_SpectatedExit()
	{
	}

	// Token: 0x06002472 RID: 9330 RVA: 0x0008B670 File Offset: 0x00089870
	protected virtual void React_SpectatorAdd(VisNode spectator)
	{
	}

	// Token: 0x06002473 RID: 9331 RVA: 0x0008B674 File Offset: 0x00089874
	protected virtual void React_SpectatorRemove(VisNode spectator)
	{
	}

	// Token: 0x06002474 RID: 9332 RVA: 0x0008B678 File Offset: 0x00089878
	protected virtual void React_AwareEnter()
	{
	}

	// Token: 0x06002475 RID: 9333 RVA: 0x0008B67C File Offset: 0x0008987C
	protected virtual void React_AwareExit()
	{
	}

	// Token: 0x06002476 RID: 9334 RVA: 0x0008B680 File Offset: 0x00089880
	protected virtual void React_SeeAdd(VisNode spotted)
	{
	}

	// Token: 0x06002477 RID: 9335 RVA: 0x0008B684 File Offset: 0x00089884
	protected virtual void React_SeeRemove(VisNode lost)
	{
	}

	// Token: 0x06002478 RID: 9336 RVA: 0x0008B688 File Offset: 0x00089888
	internal void SPECTATED_ENTER()
	{
		try
		{
			this.React_SpectatedEnter();
		}
		catch (Exception ex)
		{
			Debug.LogError(ex, this);
		}
	}

	// Token: 0x06002479 RID: 9337 RVA: 0x0008B6CC File Offset: 0x000898CC
	internal void SPECTATOR_ADD(VisNode spectator)
	{
		try
		{
			this.React_SpectatorAdd(spectator);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex, this);
		}
	}

	// Token: 0x0600247A RID: 9338 RVA: 0x0008B710 File Offset: 0x00089910
	internal void SPECTATOR_REMOVE(VisNode spectator)
	{
		try
		{
			this.React_SpectatorRemove(spectator);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex, this);
		}
	}

	// Token: 0x0600247B RID: 9339 RVA: 0x0008B754 File Offset: 0x00089954
	internal void SPECTATED_EXIT()
	{
		try
		{
			this.React_SpectatedExit();
		}
		catch (Exception ex)
		{
			Debug.LogError(ex, this);
		}
	}

	// Token: 0x0600247C RID: 9340 RVA: 0x0008B798 File Offset: 0x00089998
	internal void AWARE_ENTER()
	{
		try
		{
			this.React_AwareEnter();
		}
		catch (Exception ex)
		{
			Debug.LogError(ex, this);
		}
	}

	// Token: 0x0600247D RID: 9341 RVA: 0x0008B7DC File Offset: 0x000899DC
	internal void SEE_ADD(VisNode spotted)
	{
		try
		{
			this.React_SeeAdd(spotted);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex, this);
		}
	}

	// Token: 0x0600247E RID: 9342 RVA: 0x0008B820 File Offset: 0x00089A20
	internal void SEE_REMOVE(VisNode lost)
	{
		try
		{
			this.React_SeeRemove(lost);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex, this);
		}
	}

	// Token: 0x0600247F RID: 9343 RVA: 0x0008B864 File Offset: 0x00089A64
	internal void AWARE_EXIT()
	{
		try
		{
			this.React_AwareExit();
		}
		catch (Exception ex)
		{
			Debug.LogError(ex, this);
		}
	}

	// Token: 0x06002480 RID: 9344 RVA: 0x0008B8A8 File Offset: 0x00089AA8
	protected void Reset()
	{
		this._visNode = base.GetComponent<VisNode>();
		if (this._visNode)
		{
			this._visNode.__reactor = this;
		}
	}

	// Token: 0x04001188 RID: 4488
	[SerializeField]
	[PrefetchComponent]
	private VisNode _visNode;
}
