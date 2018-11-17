using System;
using UnityEngine;

// Token: 0x02000448 RID: 1096
public class VisEval : ScriptableObject
{
	// Token: 0x170008EA RID: 2282
	// (get) Token: 0x06002652 RID: 9810 RVA: 0x0008BBA8 File Offset: 0x00089DA8
	private int dataCount
	{
		get
		{
			return (this.data != null) ? this.data.Length : 0;
		}
	}

	// Token: 0x170008EB RID: 2283
	// (get) Token: 0x06002653 RID: 9811 RVA: 0x0008BBC4 File Offset: 0x00089DC4
	public int ruleCount
	{
		get
		{
			return this.dataCount / 4;
		}
	}

	// Token: 0x170008EC RID: 2284
	public global::Vis.Rule this[int i]
	{
		get
		{
			return global::Vis.Rule.Decode(this.data, i * 4);
		}
		set
		{
			global::Vis.Rule.Encode(ref value, this.data, i * 4);
			if (this.expanded)
			{
				this.rules[i] = value;
			}
		}
	}

	// Token: 0x06002656 RID: 9814 RVA: 0x0008BC10 File Offset: 0x00089E10
	public bool GetMessage(global::Vis.Mask current, ref global::Vis.Mask previous, global::Vis.Mask other)
	{
		return false;
	}

	// Token: 0x06002657 RID: 9815 RVA: 0x0008BC14 File Offset: 0x00089E14
	private void Swap(int i, int j)
	{
		int num = this.data[j];
		this.data[j++] = this.data[i];
		this.data[i++] = num;
		num = this.data[j];
		this.data[j++] = this.data[i];
		this.data[i++] = num;
		num = this.data[j];
		this.data[j++] = this.data[i];
		this.data[i++] = num;
	}

	// Token: 0x06002658 RID: 9816 RVA: 0x0008BCA8 File Offset: 0x00089EA8
	public bool EditorOnly_MoveUp(int index)
	{
		if (index == 0)
		{
			return false;
		}
		if (index >= this.ruleCount)
		{
			return false;
		}
		this.Swap((index - 1) * 4, index * 4);
		return true;
	}

	// Token: 0x06002659 RID: 9817 RVA: 0x0008BCDC File Offset: 0x00089EDC
	public bool EditorOnly_MoveDown(int index)
	{
		if (index >= this.ruleCount - 1)
		{
			return false;
		}
		this.Swap((index + 1) * 4, index * 4);
		return true;
	}

	// Token: 0x0600265A RID: 9818 RVA: 0x0008BD00 File Offset: 0x00089F00
	public bool EditorOnly_MoveTop(int index)
	{
		if (this.EditorOnly_MoveUp(index--))
		{
			while (this.EditorOnly_MoveUp(index--))
			{
			}
			return true;
		}
		return false;
	}

	// Token: 0x0600265B RID: 9819 RVA: 0x0008BD34 File Offset: 0x00089F34
	public bool EditorOnly_MoveBottom(int index)
	{
		if (this.EditorOnly_MoveUp(index--))
		{
			while (this.EditorOnly_MoveUp(index--))
			{
			}
			return true;
		}
		return false;
	}

	// Token: 0x0600265C RID: 9820 RVA: 0x0008BD68 File Offset: 0x00089F68
	public bool EditorOnly_New()
	{
		Array.Resize<int>(ref this.data, this.dataCount + 4);
		return true;
	}

	// Token: 0x0600265D RID: 9821 RVA: 0x0008BD80 File Offset: 0x00089F80
	public bool EditorOnly_Clone(int index)
	{
		if (index >= 0 && index < this.ruleCount)
		{
			this.EditorOnly_New();
			for (int i = this.ruleCount - 1; i > index; i--)
			{
				int num = i * 4;
				int num2 = (i - 1) * 4;
				for (int j = 0; j < 4; j++)
				{
					this.data[num] = this.data[i];
					num++;
					num2++;
				}
			}
			return true;
		}
		return false;
	}

	// Token: 0x0600265E RID: 9822 RVA: 0x0008BDF8 File Offset: 0x00089FF8
	public bool EditorOnly_Delete(int index)
	{
		if (index >= 0 && index < this.ruleCount)
		{
			for (int i = index; i < this.ruleCount - 1; i++)
			{
				int num = i * 4;
				int num2 = (i + 1) * 4;
				for (int j = 0; j < 4; j++)
				{
					this.data[num] = this.data[i];
					num++;
					num2++;
				}
			}
			if (this.ruleCount == 1)
			{
				this.data = null;
			}
			else
			{
				Array.Resize<int>(ref this.data, this.data.Length - 4);
			}
			return true;
		}
		return false;
	}

	// Token: 0x0600265F RID: 9823 RVA: 0x0008BE98 File Offset: 0x0008A098
	public bool EditorOnly_Clear()
	{
		if (this.data != null)
		{
			this.data = null;
			return true;
		}
		return false;
	}

	// Token: 0x06002660 RID: 9824 RVA: 0x0008BEB0 File Offset: 0x0008A0B0
	public bool Pass(global::Vis.Mask self, global::Vis.Mask instigator)
	{
		if (!this.expanded)
		{
			int ruleCount = this.ruleCount;
			if (ruleCount <= 0)
			{
				return true;
			}
			this.rules = new global::Vis.Rule[ruleCount];
			for (int i = 0; i < ruleCount; i++)
			{
				this.rules[i] = global::Vis.Rule.Decode(this.data, i * 4);
			}
			this.expanded = true;
		}
		for (int j = this.rules.Length - 1; j >= 0; j--)
		{
			global::Vis.Rule.Failure failure = this.rules[j].Pass(self, instigator);
			if (failure != global::Vis.Rule.Failure.None)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x04001203 RID: 4611
	[SerializeField]
	private int[] data;

	// Token: 0x04001204 RID: 4612
	[NonSerialized]
	private bool expanded;

	// Token: 0x04001205 RID: 4613
	[NonSerialized]
	private global::Vis.Rule[] rules;
}
