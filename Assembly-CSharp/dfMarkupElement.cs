using System;
using System.Collections.Generic;

// Token: 0x02000719 RID: 1817
public abstract class dfMarkupElement
{
	// Token: 0x06004293 RID: 17043 RVA: 0x00102720 File Offset: 0x00100920
	public dfMarkupElement()
	{
		this.ChildNodes = new List<dfMarkupElement>();
	}

	// Token: 0x17000D14 RID: 3348
	// (get) Token: 0x06004294 RID: 17044 RVA: 0x00102734 File Offset: 0x00100934
	// (set) Token: 0x06004295 RID: 17045 RVA: 0x0010273C File Offset: 0x0010093C
	public dfMarkupElement Parent { get; protected set; }

	// Token: 0x17000D15 RID: 3349
	// (get) Token: 0x06004296 RID: 17046 RVA: 0x00102748 File Offset: 0x00100948
	// (set) Token: 0x06004297 RID: 17047 RVA: 0x00102750 File Offset: 0x00100950
	private protected List<dfMarkupElement> ChildNodes { protected get; private set; }

	// Token: 0x06004298 RID: 17048 RVA: 0x0010275C File Offset: 0x0010095C
	public void AddChildNode(dfMarkupElement node)
	{
		node.Parent = this;
		this.ChildNodes.Add(node);
	}

	// Token: 0x06004299 RID: 17049 RVA: 0x00102774 File Offset: 0x00100974
	public void PerformLayout(dfMarkupBox container, dfMarkupStyle style)
	{
		this._PerformLayoutImpl(container, style);
	}

	// Token: 0x0600429A RID: 17050 RVA: 0x00102780 File Offset: 0x00100980
	internal virtual void Release()
	{
		this.Parent = null;
		this.ChildNodes.Clear();
	}

	// Token: 0x0600429B RID: 17051
	protected abstract void _PerformLayoutImpl(dfMarkupBox container, dfMarkupStyle style);
}
