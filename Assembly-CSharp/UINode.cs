using System;
using UnityEngine;

// Token: 0x020007A5 RID: 1957
public class UINode
{
	// Token: 0x060046CA RID: 18122 RVA: 0x0011C6C4 File Offset: 0x0011A8C4
	public UINode(Transform t)
	{
		this.trans = t;
		this.lastPos = this.trans.localPosition;
		this.lastRot = this.trans.localRotation;
		this.lastScale = this.trans.localScale;
		this.mGo = t.gameObject;
	}

	// Token: 0x17000DA7 RID: 3495
	// (get) Token: 0x060046CB RID: 18123 RVA: 0x0011C72C File Offset: 0x0011A92C
	// (set) Token: 0x060046CC RID: 18124 RVA: 0x0011C758 File Offset: 0x0011A958
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

	// Token: 0x060046CD RID: 18125 RVA: 0x0011C784 File Offset: 0x0011A984
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

	// Token: 0x040026DD RID: 9949
	private int mVisibleFlag = -1;

	// Token: 0x040026DE RID: 9950
	public Transform trans;

	// Token: 0x040026DF RID: 9951
	public UIWidget widget;

	// Token: 0x040026E0 RID: 9952
	public bool lastActive;

	// Token: 0x040026E1 RID: 9953
	public Vector3 lastPos;

	// Token: 0x040026E2 RID: 9954
	public Quaternion lastRot;

	// Token: 0x040026E3 RID: 9955
	public Vector3 lastScale;

	// Token: 0x040026E4 RID: 9956
	public int changeFlag = -1;

	// Token: 0x040026E5 RID: 9957
	private GameObject mGo;
}
