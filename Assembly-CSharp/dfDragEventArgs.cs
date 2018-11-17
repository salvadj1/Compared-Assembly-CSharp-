using System;
using UnityEngine;

// Token: 0x0200078A RID: 1930
public class dfDragEventArgs : global::dfControlEventArgs
{
	// Token: 0x0600407D RID: 16509 RVA: 0x000EC9D0 File Offset: 0x000EABD0
	internal dfDragEventArgs(global::dfControl source) : base(source)
	{
		this.State = global::dfDragDropState.None;
	}

	// Token: 0x0600407E RID: 16510 RVA: 0x000EC9E0 File Offset: 0x000EABE0
	internal dfDragEventArgs(global::dfControl source, global::dfDragDropState state, object data, Ray ray, Vector2 position) : base(source)
	{
		this.Data = data;
		this.State = state;
		this.Position = position;
		this.Ray = ray;
	}

	// Token: 0x17000C36 RID: 3126
	// (get) Token: 0x0600407F RID: 16511 RVA: 0x000ECA14 File Offset: 0x000EAC14
	// (set) Token: 0x06004080 RID: 16512 RVA: 0x000ECA1C File Offset: 0x000EAC1C
	public global::dfDragDropState State { get; set; }

	// Token: 0x17000C37 RID: 3127
	// (get) Token: 0x06004081 RID: 16513 RVA: 0x000ECA28 File Offset: 0x000EAC28
	// (set) Token: 0x06004082 RID: 16514 RVA: 0x000ECA30 File Offset: 0x000EAC30
	public object Data { get; set; }

	// Token: 0x17000C38 RID: 3128
	// (get) Token: 0x06004083 RID: 16515 RVA: 0x000ECA3C File Offset: 0x000EAC3C
	// (set) Token: 0x06004084 RID: 16516 RVA: 0x000ECA44 File Offset: 0x000EAC44
	public Vector2 Position { get; set; }

	// Token: 0x17000C39 RID: 3129
	// (get) Token: 0x06004085 RID: 16517 RVA: 0x000ECA50 File Offset: 0x000EAC50
	// (set) Token: 0x06004086 RID: 16518 RVA: 0x000ECA58 File Offset: 0x000EAC58
	public global::dfControl Target { get; set; }

	// Token: 0x17000C3A RID: 3130
	// (get) Token: 0x06004087 RID: 16519 RVA: 0x000ECA64 File Offset: 0x000EAC64
	// (set) Token: 0x06004088 RID: 16520 RVA: 0x000ECA6C File Offset: 0x000EAC6C
	public Ray Ray { get; set; }
}
