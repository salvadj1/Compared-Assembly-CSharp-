using System;
using UnityEngine;

// Token: 0x0200010D RID: 269
public class CharacterOverlayTrait : CharacterTrait
{
	// Token: 0x17000187 RID: 391
	// (get) Token: 0x060006F1 RID: 1777 RVA: 0x0001F638 File Offset: 0x0001D838
	public string overlayComponentName
	{
		get
		{
			return this._overlayComponentName;
		}
	}

	// Token: 0x17000188 RID: 392
	// (get) Token: 0x060006F2 RID: 1778 RVA: 0x0001F640 File Offset: 0x0001D840
	public Texture2D damageOverlay
	{
		get
		{
			return this._damageOverlay;
		}
	}

	// Token: 0x17000189 RID: 393
	// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0001F648 File Offset: 0x0001D848
	public Texture2D damageOverlay2
	{
		get
		{
			return this._damageOverlay2;
		}
	}

	// Token: 0x1700018A RID: 394
	// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0001F650 File Offset: 0x0001D850
	public ScriptableObject takeDamageBob
	{
		get
		{
			return this._takeDamageBob;
		}
	}

	// Token: 0x1700018B RID: 395
	// (get) Token: 0x060006F5 RID: 1781 RVA: 0x0001F658 File Offset: 0x0001D858
	public ScriptableObject meleeBob
	{
		get
		{
			return this._meleeBob;
		}
	}

	// Token: 0x04000529 RID: 1321
	[SerializeField]
	private string _overlayComponentName = "LocalDamageDisplay";

	// Token: 0x0400052A RID: 1322
	[SerializeField]
	private Texture2D _damageOverlay;

	// Token: 0x0400052B RID: 1323
	[SerializeField]
	private Texture2D _damageOverlay2;

	// Token: 0x0400052C RID: 1324
	[SerializeField]
	private ScriptableObject _takeDamageBob;

	// Token: 0x0400052D RID: 1325
	[SerializeField]
	private ScriptableObject _meleeBob;
}
