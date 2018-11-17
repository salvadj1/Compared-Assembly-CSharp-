using System;
using UnityEngine;

// Token: 0x02000111 RID: 273
public class CharacterSoundsTrait : CharacterTrait
{
	// Token: 0x170001A6 RID: 422
	// (get) Token: 0x06000718 RID: 1816 RVA: 0x0001FC48 File Offset: 0x0001DE48
	public AudioClipArray attack
	{
		get
		{
			return this._attack;
		}
	}

	// Token: 0x170001A7 RID: 423
	// (get) Token: 0x06000719 RID: 1817 RVA: 0x0001FC50 File Offset: 0x0001DE50
	public AudioClipArray alert
	{
		get
		{
			return this._alert;
		}
	}

	// Token: 0x170001A8 RID: 424
	// (get) Token: 0x0600071A RID: 1818 RVA: 0x0001FC58 File Offset: 0x0001DE58
	public AudioClipArray idle
	{
		get
		{
			return this._idle;
		}
	}

	// Token: 0x170001A9 RID: 425
	// (get) Token: 0x0600071B RID: 1819 RVA: 0x0001FC60 File Offset: 0x0001DE60
	public AudioClipArray persuit
	{
		get
		{
			return this._persuit;
		}
	}

	// Token: 0x170001AA RID: 426
	// (get) Token: 0x0600071C RID: 1820 RVA: 0x0001FC68 File Offset: 0x0001DE68
	public AudioClipArray impact
	{
		get
		{
			return this._impact;
		}
	}

	// Token: 0x170001AB RID: 427
	// (get) Token: 0x0600071D RID: 1821 RVA: 0x0001FC70 File Offset: 0x0001DE70
	public AudioClipArray death
	{
		get
		{
			return this._death;
		}
	}

	// Token: 0x0400053D RID: 1341
	[SerializeField]
	private AudioClipArray _attack;

	// Token: 0x0400053E RID: 1342
	[SerializeField]
	private AudioClipArray _alert;

	// Token: 0x0400053F RID: 1343
	[SerializeField]
	private AudioClipArray _idle;

	// Token: 0x04000540 RID: 1344
	[SerializeField]
	private AudioClipArray _persuit;

	// Token: 0x04000541 RID: 1345
	[SerializeField]
	private AudioClipArray _impact;

	// Token: 0x04000542 RID: 1346
	[SerializeField]
	private AudioClipArray _death;
}
