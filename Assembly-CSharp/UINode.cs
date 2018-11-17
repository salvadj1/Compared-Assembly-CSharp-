using System;
using UnityEngine;

// Token: 0x02000890 RID: 2192
public class UINode
{
	// Token: 0x06004B4F RID: 19279 RVA: 0x00126044 File Offset: 0x00124244
	public UINode(Transform t)
	{
		this.trans = t;
		this.lastPos = this.trans.localPosition;
		this.lastRot = this.trans.localRotation;
		this.lastScale = this.trans.localScale;
		this.mGo = t.gameObject;
	}

	// Token: 0x17000E37 RID: 3639
	// (get) Token: 0x06004B50 RID: 19280 RVA: 0x001260AC File Offset: 0x001242AC
	// (set) Token: 0x06004B51 RID: 19281 RVA: 0x001260D8 File Offset: 0x001242D8
	public int visibleFlag
	{
		get
		{
			return (!(this.widget != null)) ? this.mVisibleFlag : this.widget.visibleFlag;
		}
		set
		{
			if (this.widget != null)
			{
				this.widget.visibleFlag = value;
			}
			else
			{
				this.mVisibleFlag = value;
			}
		}
	}

	// Token: 0x06004B52 RID: 19282 RVA: 0x00126104 File Offset: 0x00124304
	public bool HasChanged()
	{
		bool flag = this.mGo.activeInHierarchy && (this.widget == null || (this.widget.enabled && this.widget.color.a > 0.001f));
		if (this.lastActive != flag || (flag && (this.lastPos != this.trans.localPosition || this.lastRot != this.trans.localRotation || this.lastScale != this.trans.localScale)))
		{
			this.lastActive = flag;
			this.lastPos = this.trans.localPosition;
			this.lastRot = this.trans.localRotation;
			this.lastScale = this.trans.localScale;
			return true;
		}
		return false;
	}

	// Token: 0x04002914 RID: 10516
	private int mVisibleFlag = -1;

	// Token: 0x04002915 RID: 10517
	public Transform trans;

	// Token: 0x04002916 RID: 10518
	public global::UIWidget widget;

	// Token: 0x04002917 RID: 10519
	public bool lastActive;

	// Token: 0x04002918 RID: 10520
	public Vector3 lastPos;

	// Token: 0x04002919 RID: 10521
	public Quaternion lastRot;

	// Token: 0x0400291A RID: 10522
	public Vector3 lastScale;

	// Token: 0x0400291B RID: 10523
	public int changeFlag = -1;

	// Token: 0x0400291C RID: 10524
	private GameObject mGo;
}
