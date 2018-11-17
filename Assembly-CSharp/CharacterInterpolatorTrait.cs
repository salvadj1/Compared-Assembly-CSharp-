using System;
using UnityEngine;

// Token: 0x02000108 RID: 264
public class CharacterInterpolatorTrait : CharacterTrait
{
	// Token: 0x17000171 RID: 369
	// (get) Token: 0x060006D4 RID: 1748 RVA: 0x0001F39C File Offset: 0x0001D59C
	public string interpolatorComponentTypeName
	{
		get
		{
			return this._interpolatorComponentTypeName;
		}
	}

	// Token: 0x17000172 RID: 370
	// (get) Token: 0x060006D5 RID: 1749 RVA: 0x0001F3A4 File Offset: 0x0001D5A4
	public int bufferCapacity
	{
		get
		{
			return this._bufferCapacity;
		}
	}

	// Token: 0x17000173 RID: 371
	// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0001F3AC File Offset: 0x0001D5AC
	public bool allowExtrapolation
	{
		get
		{
			return this._allowExtrapolation;
		}
	}

	// Token: 0x17000174 RID: 372
	// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0001F3B4 File Offset: 0x0001D5B4
	public float allowableTimeSpan
	{
		get
		{
			return this._allowableTimeSpan;
		}
	}

	// Token: 0x060006D8 RID: 1752 RVA: 0x0001F3BC File Offset: 0x0001D5BC
	public virtual Interpolator AddInterpolator(IDMain main)
	{
		if (string.IsNullOrEmpty(this._interpolatorComponentTypeName))
		{
			return null;
		}
		Component component = main.gameObject.AddComponent(this._interpolatorComponentTypeName);
		Interpolator interpolator = component as Interpolator;
		if (interpolator)
		{
			interpolator.idMain = main;
			return interpolator;
		}
		Debug.LogError(this._interpolatorComponentTypeName + " is not a interpolator");
		Object.Destroy(component);
		return null;
	}

	// Token: 0x04000514 RID: 1300
	[SerializeField]
	private string _interpolatorComponentTypeName;

	// Token: 0x04000515 RID: 1301
	[SerializeField]
	private int _bufferCapacity = -1;

	// Token: 0x04000516 RID: 1302
	[SerializeField]
	private bool _allowExtrapolation;

	// Token: 0x04000517 RID: 1303
	[SerializeField]
	private float _allowableTimeSpan = 0.1f;
}
