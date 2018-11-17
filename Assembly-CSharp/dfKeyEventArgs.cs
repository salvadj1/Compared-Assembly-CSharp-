using System;
using UnityEngine;

// Token: 0x0200078B RID: 1931
public class dfKeyEventArgs : global::dfControlEventArgs
{
	// Token: 0x06004089 RID: 16521 RVA: 0x000ECA78 File Offset: 0x000EAC78
	internal dfKeyEventArgs(global::dfControl source, KeyCode Key, bool Control, bool Shift, bool Alt) : base(source)
	{
		this.KeyCode = Key;
		this.Control = Control;
		this.Shift = Shift;
		this.Alt = Alt;
	}

	// Token: 0x17000C3B RID: 3131
	// (get) Token: 0x0600408A RID: 16522 RVA: 0x000ECAAC File Offset: 0x000EACAC
	// (set) Token: 0x0600408B RID: 16523 RVA: 0x000ECAB4 File Offset: 0x000EACB4
	public KeyCode KeyCode { get; set; }

	// Token: 0x17000C3C RID: 3132
	// (get) Token: 0x0600408C RID: 16524 RVA: 0x000ECAC0 File Offset: 0x000EACC0
	// (set) Token: 0x0600408D RID: 16525 RVA: 0x000ECAC8 File Offset: 0x000EACC8
	public char Character { get; set; }

	// Token: 0x17000C3D RID: 3133
	// (get) Token: 0x0600408E RID: 16526 RVA: 0x000ECAD4 File Offset: 0x000EACD4
	// (set) Token: 0x0600408F RID: 16527 RVA: 0x000ECADC File Offset: 0x000EACDC
	public bool Control { get; set; }

	// Token: 0x17000C3E RID: 3134
	// (get) Token: 0x06004090 RID: 16528 RVA: 0x000ECAE8 File Offset: 0x000EACE8
	// (set) Token: 0x06004091 RID: 16529 RVA: 0x000ECAF0 File Offset: 0x000EACF0
	public bool Shift { get; set; }

	// Token: 0x17000C3F RID: 3135
	// (get) Token: 0x06004092 RID: 16530 RVA: 0x000ECAFC File Offset: 0x000EACFC
	// (set) Token: 0x06004093 RID: 16531 RVA: 0x000ECB04 File Offset: 0x000EAD04
	public bool Alt { get; set; }

	// Token: 0x06004094 RID: 16532 RVA: 0x000ECB10 File Offset: 0x000EAD10
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
