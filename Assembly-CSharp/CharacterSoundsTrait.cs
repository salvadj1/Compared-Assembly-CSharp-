using System;
using UnityEngine;

// Token: 0x02000130 RID: 304
public class CharacterSoundsTrait : global::CharacterTrait
{
	// Token: 0x170001D4 RID: 468
	// (get) Token: 0x060007EA RID: 2026 RVA: 0x0002281C File Offset: 0x00020A1C
	public global::AudioClipArray attack
	{
		get
		{
			return this._attack;
		}
	}

	// Token: 0x170001D5 RID: 469
	// (get) Token: 0x060007EB RID: 2027 RVA: 0x00022824 File Offset: 0x00020A24
	public global::AudioClipArray alert
	{
		get
		{
			return this._alert;
		}
	}

	// Token: 0x170001D6 RID: 470
	// (get) Token: 0x060007EC RID: 2028 RVA: 0x0002282C File Offset: 0x00020A2C
	public global::AudioClipArray idle
	{
		get
		{
			return this._idle;
		}
	}

	// Token: 0x170001D7 RID: 471
	// (get) Token: 0x060007ED RID: 2029 RVA: 0x00022834 File Offset: 0x00020A34
	public global::AudioClipArray persuit
	{
		get
		{
			return this._persuit;
		}
	}

	// Token: 0x170001D8 RID: 472
	// (get) Token: 0x060007EE RID: 2030 RVA: 0x0002283C File Offset: 0x00020A3C
	public global::AudioClipArray impact
	{
		get
		{
			return this._impact;
		}
	}

	// Token: 0x170001D9 RID: 473
	// (get) Token: 0x060007EF RID: 2031 RVA: 0x00022844 File Offset: 0x00020A44
	public global::AudioClipArray death
	{
		get
		{
			return this._death;
		}
	}

	// Token: 0x04000608 RID: 1544
	[SerializeField]
	private global::AudioClipArray _attack;

	// Token: 0x04000609 RID: 1545
	[SerializeField]
	private global::AudioClipArray _alert;

	// Token: 0x0400060A RID: 1546
	[SerializeField]
	private global::AudioClipArray _idle;

	// Token: 0x0400060B RID: 1547
	[SerializeField]
	private global::AudioClipArray _persuit;

	// Token: 0x0400060C RID: 1548
	[SerializeField]
	private global::AudioClipArray _impact;

	// Token: 0x0400060D RID: 1549
	[SerializeField]
	private global::AudioClipArray _death;
}
