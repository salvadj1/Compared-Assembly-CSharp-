using System;
using UnityEngine;

// Token: 0x02000127 RID: 295
public class CharacterInterpolatorTrait : global::CharacterTrait
{
	// Token: 0x1700019F RID: 415
	// (get) Token: 0x060007A6 RID: 1958 RVA: 0x00021F70 File Offset: 0x00020170
	public string interpolatorComponentTypeName
	{
		get
		{
			return this._interpolatorComponentTypeName;
		}
	}

	// Token: 0x170001A0 RID: 416
	// (get) Token: 0x060007A7 RID: 1959 RVA: 0x00021F78 File Offset: 0x00020178
	public int bufferCapacity
	{
		get
		{
			return this._bufferCapacity;
		}
	}

	// Token: 0x170001A1 RID: 417
	// (get) Token: 0x060007A8 RID: 1960 RVA: 0x00021F80 File Offset: 0x00020180
	public bool allowExtrapolation
	{
		get
		{
			return this._allowExtrapolation;
		}
	}

	// Token: 0x170001A2 RID: 418
	// (get) Token: 0x060007A9 RID: 1961 RVA: 0x00021F88 File Offset: 0x00020188
	public float allowableTimeSpan
	{
		get
		{
			return this._allowableTimeSpan;
		}
	}

	// Token: 0x060007AA RID: 1962 RVA: 0x00021F90 File Offset: 0x00020190
	public virtual global::Interpolator AddInterpolator(IDMain main)
	{
		if (string.IsNullOrEmpty(this._interpolatorComponentTypeName))
		{
			return null;
		}
		Component component = main.gameObject.AddComponent(this._interpolatorComponentTypeName);
		global::Interpolator interpolator = component as global::Interpolator;
		if (interpolator)
		{
			interpolator.idMain = main;
			return interpolator;
		}
		Debug.LogError(this._interpolatorComponentTypeName + " is not a interpolator");
		Object.Destroy(component);
		return null;
	}

	// Token: 0x040005DF RID: 1503
	[SerializeField]
	private string _interpolatorComponentTypeName;

	// Token: 0x040005E0 RID: 1504
	[SerializeField]
	private int _bufferCapacity = -1;

	// Token: 0x040005E1 RID: 1505
	[SerializeField]
	private bool _allowExtrapolation;

	// Token: 0x040005E2 RID: 1506
	[SerializeField]
	private float _allowableTimeSpan = 0.1f;
}
