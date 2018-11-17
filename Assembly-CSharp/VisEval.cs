using System;
using UnityEngine;

// Token: 0x0200039B RID: 923
public class VisEval : ScriptableObject
{
	// Token: 0x1700088C RID: 2188
	// (get) Token: 0x060022F0 RID: 8944 RVA: 0x000867AC File Offset: 0x000849AC
	private int dataCount
	{
		get
		{
			return (this.data != null) ? this.data.Length : 0;
		}
	}

	// Token: 0x1700088D RID: 2189
	// (get) Token: 0x060022F1 RID: 8945 RVA: 0x000867C8 File Offset: 0x000849C8
	public int ruleCount
	{
		get
		{
			return this.dataCount / 4;
		}
	}

	// Token: 0x1700088E RID: 2190
	public Vis.Rule this[int i]
	{
		get
		{
			return Vis.Rule.Decode(this.data, i * 4);
		}
		set
		{
			Vis.Rule.Encode(ref value, this.data, i * 4);
			if (this.expanded)
			{
				this.rules[i] = value;
			}
		}
	}

	// Token: 0x060022F4 RID: 8948 RVA: 0x00086814 File Offset: 0x00084A14
	public bool GetMessage(Vis.Mask current, ref Vis.Mask previous, Vis.Mask other)
	{
		return false;
	}

	// Token: 0x060022F5 RID: 8949 RVA: 0x00086818 File Offset: 0x00084A18
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

	// Token: 0x060022F6 RID: 8950 RVA: 0x000868AC File Offset: 0x00084AAC
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

	// Token: 0x060022F7 RID: 8951 RVA: 0x000868E0 File Offset: 0x00084AE0
	public bool EditorOnly_MoveDown(int index)
	{
		if (index >= this.ruleCount - 1)
		{
			return false;
		}
		this.Swap((index + 1) * 4, index * 4);
		return true;
	}

	// Token: 0x060022F8 RID: 8952 RVA: 0x00086904 File Offset: 0x00084B04
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

	// Token: 0x060022F9 RID: 8953 RVA: 0x00086938 File Offset: 0x00084B38
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

	// Token: 0x060022FA RID: 8954 RVA: 0x0008696C File Offset: 0x00084B6C
	public bool EditorOnly_New()
	{
		Array.Resize<int>(ref this.data, this.dataCount + 4);
		return true;
	}

	// Token: 0x060022FB RID: 8955 RVA: 0x00086984 File Offset: 0x00084B84
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

	// Token: 0x060022FC RID: 8956 RVA: 0x000869FC File Offset: 0x00084BFC
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

	// Token: 0x060022FD RID: 8957 RVA: 0x00086A9C File Offset: 0x00084C9C
	public bool EditorOnly_Clear()
	{
		if (this.data != null)
		{
			this.data = null;
			return true;
		}
		return false;
	}

	// Token: 0x060022FE RID: 8958 RVA: 0x00086AB4 File Offset: 0x00084CB4
	public bool Pass(Vis.Mask self, Vis.Mask instigator)
	{
		if (!this.expanded)
		{
			int ruleCount = this.ruleCount;
			if (ruleCount <= 0)
			{
				return true;
			}
			this.rules = new Vis.Rule[ruleCount];
			for (int i = 0; i < ruleCount; i++)
			{
				this.rules[i] = Vis.Rule.Decode(this.data, i * 4);
			}
			this.expanded = true;
		}
		for (int j = this.rules.Length - 1; j >= 0; j--)
		{
			Vis.Rule.Failure failure = this.rules[j].Pass(self, instigator);
			if (failure != Vis.Rule.Failure.None)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0400109D RID: 4253
	[SerializeField]
	private int[] data;

	// Token: 0x0400109E RID: 4254
	[NonSerialized]
	private bool expanded;

	// Token: 0x0400109F RID: 4255
	[NonSerialized]
	private Vis.Rule[] rules;
}
