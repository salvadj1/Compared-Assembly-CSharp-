using System;
using UnityEngine;

// Token: 0x0200012C RID: 300
public class CharacterOverlayTrait : global::CharacterTrait
{
	// Token: 0x170001B5 RID: 437
	// (get) Token: 0x060007C3 RID: 1987 RVA: 0x0002220C File Offset: 0x0002040C
	public string overlayComponentName
	{
		get
		{
			return this._overlayComponentName;
		}
	}

	// Token: 0x170001B6 RID: 438
	// (get) Token: 0x060007C4 RID: 1988 RVA: 0x00022214 File Offset: 0x00020414
	public Texture2D damageOverlay
	{
		get
		{
			return this._damageOverlay;
		}
	}

	// Token: 0x170001B7 RID: 439
	// (get) Token: 0x060007C5 RID: 1989 RVA: 0x0002221C File Offset: 0x0002041C
	public Texture2D damageOverlay2
	{
		get
		{
			return this._damageOverlay2;
		}
	}

	// Token: 0x170001B8 RID: 440
	// (get) Token: 0x060007C6 RID: 1990 RVA: 0x00022224 File Offset: 0x00020424
	public ScriptableObject takeDamageBob
	{
		get
		{
			return this._takeDamageBob;
		}
	}

	// Token: 0x170001B9 RID: 441
	// (get) Token: 0x060007C7 RID: 1991 RVA: 0x0002222C File Offset: 0x0002042C
	public ScriptableObject meleeBob
	{
		get
		{
			return this._meleeBob;
		}
	}

	// Token: 0x040005F4 RID: 1524
	[SerializeField]
	private string _overlayComponentName = "LocalDamageDisplay";

	// Token: 0x040005F5 RID: 1525
	[SerializeField]
	private Texture2D _damageOverlay;

	// Token: 0x040005F6 RID: 1526
	[SerializeField]
	private Texture2D _damageOverlay2;

	// Token: 0x040005F7 RID: 1527
	[SerializeField]
	private ScriptableObject _takeDamageBob;

	// Token: 0x040005F8 RID: 1528
	[SerializeField]
	private ScriptableObject _meleeBob;
}
