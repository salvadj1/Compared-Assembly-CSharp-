using System;
using System.Collections.Generic;

// Token: 0x020007F5 RID: 2037
public abstract class dfMarkupElement
{
	// Token: 0x060046D7 RID: 18135 RVA: 0x0010BA30 File Offset: 0x00109C30
	public dfMarkupElement()
	{
		this.ChildNodes = new List<global::dfMarkupElement>();
	}

	// Token: 0x17000D9E RID: 3486
	// (get) Token: 0x060046D8 RID: 18136 RVA: 0x0010BA44 File Offset: 0x00109C44
	// (set) Token: 0x060046D9 RID: 18137 RVA: 0x0010BA4C File Offset: 0x00109C4C
	public global::dfMarkupElement Parent { get; protected set; }

	// Token: 0x17000D9F RID: 3487
	// (get) Token: 0x060046DA RID: 18138 RVA: 0x0010BA58 File Offset: 0x00109C58
	// (set) Token: 0x060046DB RID: 18139 RVA: 0x0010BA60 File Offset: 0x00109C60
	private protected List<global::dfMarkupElement> ChildNodes { protected get; private set; }

	// Token: 0x060046DC RID: 18140 RVA: 0x0010BA6C File Offset: 0x00109C6C
	public void AddChildNode(global::dfMarkupElement node)
	{
		node.Parent = this;
		this.ChildNodes.Add(node);
	}

	// Token: 0x060046DD RID: 18141 RVA: 0x0010BA84 File Offset: 0x00109C84
	public void PerformLayout(global::dfMarkupBox container, global::dfMarkupStyle style)
	{
		this._PerformLayoutImpl(container, style);
	}

	// Token: 0x060046DE RID: 18142 RVA: 0x0010BA90 File Offset: 0x00109C90
	internal virtual void Release()
	{
		this.Parent = null;
		this.ChildNodes.Clear();
	}

	// Token: 0x060046DF RID: 18143
	protected abstract void _PerformLayoutImpl(global::dfMarkupBox container, global::dfMarkupStyle style);
}
