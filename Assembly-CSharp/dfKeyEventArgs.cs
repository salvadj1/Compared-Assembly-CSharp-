using System;
using UnityEngine;

// Token: 0x020006C0 RID: 1728
public class dfKeyEventArgs : dfControlEventArgs
{
	// Token: 0x06003C7F RID: 15487 RVA: 0x000E3F34 File Offset: 0x000E2134
	internal dfKeyEventArgs(dfControl source, KeyCode Key, bool Control, bool Shift, bool Alt) : base(source)
	{
		this.KeyCode = Key;
		this.Control = Control;
		this.Shift = Shift;
		this.Alt = Alt;
	}

	// Token: 0x17000BB7 RID: 2999
	// (get) Token: 0x06003C80 RID: 15488 RVA: 0x000E3F68 File Offset: 0x000E2168
	// (set) Token: 0x06003C81 RID: 15489 RVA: 0x000E3F70 File Offset: 0x000E2170
	public KeyCode KeyCode { get; set; }

	// Token: 0x17000BB8 RID: 3000
	// (get) Token: 0x06003C82 RID: 15490 RVA: 0x000E3F7C File Offset: 0x000E217C
	// (set) Token: 0x06003C83 RID: 15491 RVA: 0x000E3F84 File Offset: 0x000E2184
	public char Character { get; set; }

	// Token: 0x17000BB9 RID: 3001
	// (get) Token: 0x06003C84 RID: 15492 RVA: 0x000E3F90 File Offset: 0x000E2190
	// (set) Token: 0x06003C85 RID: 15493 RVA: 0x000E3F98 File Offset: 0x000E2198
	public bool Control { get; set; }

	// Token: 0x17000BBA RID: 3002
	// (get) Token: 0x06003C86 RID: 15494 RVA: 0x000E3FA4 File Offset: 0x000E21A4
	// (set) Token: 0x06003C87 RID: 15495 RVA: 0x000E3FAC File Offset: 0x000E21AC
	public bool Shift { get; set; }

	// Token: 0x17000BBB RID: 3003
	// (get) Token: 0x06003C88 RID: 15496 RVA: 0x000E3FB8 File Offset: 0x000E21B8
	// (set) Token: 0x06003C89 RID: 15497 RVA: 0x000E3FC0 File Offset: 0x000E21C0
	public bool Alt { get; set; }

	// Token: 0x06003C8A RID: 15498 RVA: 0x000E3FCC File Offset: 0x000E21CC
	public override string ToString()
	{
		return string.Format("Key: {0}, Control: {1}, Shift: {2}, Alt: {3}", new object[]
		{
			this.KeyCode,
			this.Control,
			this.Shift,
			this.Alt
		});
	}
}
