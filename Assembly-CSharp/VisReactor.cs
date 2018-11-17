using System;
using UnityEngine;

// Token: 0x02000474 RID: 1140
public abstract class VisReactor : MonoBehaviour
{
	// Token: 0x1700092B RID: 2347
	// (set) Token: 0x060027CF RID: 10191 RVA: 0x00090A48 File Offset: 0x0008EC48
	internal global::VisNode __visNode
	{
		set
		{
			this._visNode = value;
		}
	}

	// Token: 0x1700092C RID: 2348
	// (get) Token: 0x060027D0 RID: 10192 RVA: 0x00090A54 File Offset: 0x0008EC54
	public global::VisNode node
	{
		get
		{
			return this._visNode;
		}
	}

	// Token: 0x1700092D RID: 2349
	// (get) Token: 0x060027D1 RID: 10193 RVA: 0x00090A5C File Offset: 0x0008EC5C
	protected global::VisNode self
	{
		get
		{
			return this._visNode;
		}
	}

	// Token: 0x060027D2 RID: 10194 RVA: 0x00090A64 File Offset: 0x0008EC64
	protected virtual void React_SpectatedEnter()
	{
	}

	// Token: 0x060027D3 RID: 10195 RVA: 0x00090A68 File Offset: 0x0008EC68
	protected virtual void React_SpectatedExit()
	{
	}

	// Token: 0x060027D4 RID: 10196 RVA: 0x00090A6C File Offset: 0x0008EC6C
	protected virtual void React_SpectatorAdd(global::VisNode spectator)
	{
	}

	// Token: 0x060027D5 RID: 10197 RVA: 0x00090A70 File Offset: 0x0008EC70
	protected virtual void React_SpectatorRemove(global::VisNode spectator)
	{
	}

	// Token: 0x060027D6 RID: 10198 RVA: 0x00090A74 File Offset: 0x0008EC74
	protected virtual void React_AwareEnter()
	{
	}

	// Token: 0x060027D7 RID: 10199 RVA: 0x00090A78 File Offset: 0x0008EC78
	protected virtual void React_AwareExit()
	{
	}

	// Token: 0x060027D8 RID: 10200 RVA: 0x00090A7C File Offset: 0x0008EC7C
	protected virtual void React_SeeAdd(global::VisNode spotted)
	{
	}

	// Token: 0x060027D9 RID: 10201 RVA: 0x00090A80 File Offset: 0x0008EC80
	protected virtual void React_SeeRemove(global::VisNode lost)
	{
	}

	// Token: 0x060027DA RID: 10202 RVA: 0x00090A84 File Offset: 0x0008EC84
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

	// Token: 0x060027DB RID: 10203 RVA: 0x00090AC8 File Offset: 0x0008ECC8
	internal void SPECTATOR_ADD(global::VisNode spectator)
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

	// Token: 0x060027DC RID: 10204 RVA: 0x00090B0C File Offset: 0x0008ED0C
	internal void SPECTATOR_REMOVE(global::VisNode spectator)
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

	// Token: 0x060027DD RID: 10205 RVA: 0x00090B50 File Offset: 0x0008ED50
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

	// Token: 0x060027DE RID: 10206 RVA: 0x00090B94 File Offset: 0x0008ED94
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

	// Token: 0x060027DF RID: 10207 RVA: 0x00090BD8 File Offset: 0x0008EDD8
	internal void SEE_ADD(global::VisNode spotted)
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

	// Token: 0x060027E0 RID: 10208 RVA: 0x00090C1C File Offset: 0x0008EE1C
	internal void SEE_REMOVE(global::VisNode lost)
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

	// Token: 0x060027E1 RID: 10209 RVA: 0x00090C60 File Offset: 0x0008EE60
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

	// Token: 0x060027E2 RID: 10210 RVA: 0x00090CA4 File Offset: 0x0008EEA4
	protected void Reset()
	{
		this._visNode = base.GetComponent<global::VisNode>();
		if (this._visNode)
		{
			this._visNode.__reactor = this;
		}
	}

	// Token: 0x040012EE RID: 4846
	[PrefetchComponent]
	[SerializeField]
	private global::VisNode _visNode;
}
