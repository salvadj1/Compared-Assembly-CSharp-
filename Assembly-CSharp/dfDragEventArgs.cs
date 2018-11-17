using System;
using UnityEngine;

// Token: 0x020006BF RID: 1727
public class dfDragEventArgs : dfControlEventArgs
{
	// Token: 0x06003C73 RID: 15475 RVA: 0x000E3E8C File Offset: 0x000E208C
	internal dfDragEventArgs(dfControl source) : base(source)
	{
		this.State = dfDragDropState.None;
	}

	// Token: 0x06003C74 RID: 15476 RVA: 0x000E3E9C File Offset: 0x000E209C
	internal dfDragEventArgs(dfControl source, dfDragDropState state, object data, Ray ray, Vector2 position) : base(source)
	{
		this.Data = data;
		this.State = state;
		this.Position = position;
		this.Ray = ray;
	}

	// Token: 0x17000BB2 RID: 2994
	// (get) Token: 0x06003C75 RID: 15477 RVA: 0x000E3ED0 File Offset: 0x000E20D0
	// (set) Token: 0x06003C76 RID: 15478 RVA: 0x000E3ED8 File Offset: 0x000E20D8
	public dfDragDropState State { get; set; }

	// Token: 0x17000BB3 RID: 2995
	// (get) Token: 0x06003C77 RID: 15479 RVA: 0x000E3EE4 File Offset: 0x000E20E4
	// (set) Token: 0x06003C78 RID: 15480 RVA: 0x000E3EEC File Offset: 0x000E20EC
	public object Data { get; set; }

	// Token: 0x17000BB4 RID: 2996
	// (get) Token: 0x06003C79 RID: 15481 RVA: 0x000E3EF8 File Offset: 0x000E20F8
	// (set) Token: 0x06003C7A RID: 15482 RVA: 0x000E3F00 File Offset: 0x000E2100
	public Vector2 Position { get; set; }

	// Token: 0x17000BB5 RID: 2997
	// (get) Token: 0x06003C7B RID: 15483 RVA: 0x000E3F0C File Offset: 0x000E210C
	// (set) Token: 0x06003C7C RID: 15484 RVA: 0x000E3F14 File Offset: 0x000E2114
	public dfControl Target { get; set; }

	// Token: 0x17000BB6 RID: 2998
	// (get) Token: 0x06003C7D RID: 15485 RVA: 0x000E3F20 File Offset: 0x000E2120
	// (set) Token: 0x06003C7E RID: 15486 RVA: 0x000E3F28 File Offset: 0x000E2128
	public Ray Ray { get; set; }
}
