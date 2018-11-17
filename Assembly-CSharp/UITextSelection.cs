using System;

// Token: 0x020008D9 RID: 2265
public struct UITextSelection
{
	// Token: 0x06004D0D RID: 19725 RVA: 0x0012FA14 File Offset: 0x0012DC14
	public UITextSelection(global::UITextPosition carratPos, global::UITextPosition selectPos)
	{
		this.carratPos = carratPos;
		this.selectPos = selectPos;
	}

	// Token: 0x17000EA7 RID: 3751
	// (get) Token: 0x06004D0E RID: 19726 RVA: 0x0012FA24 File Offset: 0x0012DC24
	public bool hasSelection
	{
		get
		{
			return this.carratPos.valid && this.selectPos.valid && this.carratPos.position != this.selectPos.position;
		}
	}

	// Token: 0x17000EA8 RID: 3752
	// (get) Token: 0x06004D0F RID: 19727 RVA: 0x0012FA70 File Offset: 0x0012DC70
	public bool showCarrat
	{
		get
		{
			return this.carratPos.valid && (!this.selectPos.valid || this.selectPos.position == this.carratPos.position);
		}
	}

	// Token: 0x17000EA9 RID: 3753
	// (get) Token: 0x06004D10 RID: 19728 RVA: 0x0012FABC File Offset: 0x0012DCBC
	public bool valid
	{
		get
		{
			return this.carratPos.valid;
		}
	}

	// Token: 0x06004D11 RID: 19729 RVA: 0x0012FACC File Offset: 0x0012DCCC
	public bool GetHighlight(out global::UIHighlight h)
	{
		if (this.selectPos.position < this.carratPos.position)
		{
			if (this.carratPos.valid && this.selectPos.valid)
			{
				h.a.i = this.selectPos.position;
				h.a.L = this.selectPos.line;
				h.a.C = this.selectPos.column;
				h.b.i = this.carratPos.position;
				h.b.L = this.carratPos.line;
				h.b.C = this.carratPos.column;
				return true;
			}
		}
		else if (this.selectPos.position > this.carratPos.position && this.carratPos.valid && this.selectPos.valid)
		{
			h.b.i = this.selectPos.position;
			h.b.L = this.selectPos.line;
			h.b.C = this.selectPos.column;
			h.a.i = this.carratPos.position;
			h.a.L = this.carratPos.line;
			h.a.C = this.carratPos.column;
			return true;
		}
		h = global::UIHighlight.invalid;
		return false;
	}

	// Token: 0x17000EAA RID: 3754
	// (get) Token: 0x06004D12 RID: 19730 RVA: 0x0012FC6C File Offset: 0x0012DE6C
	public int highlightBegin
	{
		get
		{
			if (this.carratPos.valid && this.selectPos.valid && this.selectPos.position != this.carratPos.position)
			{
				return (this.selectPos.position >= this.carratPos.position) ? this.carratPos.position : this.selectPos.position;
			}
			return -1;
		}
	}

	// Token: 0x17000EAB RID: 3755
	// (get) Token: 0x06004D13 RID: 19731 RVA: 0x0012FCEC File Offset: 0x0012DEEC
	public int highlightEnd
	{
		get
		{
			if (this.carratPos.valid && this.selectPos.valid && this.selectPos.position != this.carratPos.position)
			{
				return (this.selectPos.position >= this.carratPos.position) ? this.selectPos.position : this.carratPos.position;
			}
			return -1;
		}
	}

	// Token: 0x17000EAC RID: 3756
	// (get) Token: 0x06004D14 RID: 19732 RVA: 0x0012FD6C File Offset: 0x0012DF6C
	public int carratIndex
	{
		get
		{
			if ((this.carratPos.position == this.selectPos.position || !this.selectPos.valid) && this.carratPos.valid)
			{
				return this.carratPos.position;
			}
			return -1;
		}
	}

	// Token: 0x17000EAD RID: 3757
	// (get) Token: 0x06004D15 RID: 19733 RVA: 0x0012FDC4 File Offset: 0x0012DFC4
	public int selectIndex
	{
		get
		{
			if (this.carratPos.valid && this.selectPos.valid && this.selectPos.position != this.carratPos.position)
			{
				return this.selectPos.position;
			}
			return -1;
		}
	}

	// Token: 0x06004D16 RID: 19734 RVA: 0x0012FE1C File Offset: 0x0012E01C
	public global::UITextSelection.Change GetChangesTo(ref global::UITextSelection value)
	{
		global::UITextSelection.Change result;
		if (this.carratPos.valid)
		{
			if (!value.carratPos.valid)
			{
				result = ((!this.hasSelection) ? global::UITextSelection.Change.CarratToNone : global::UITextSelection.Change.SelectionToNone);
			}
			else if (this.hasSelection)
			{
				if (!value.hasSelection)
				{
					result = global::UITextSelection.Change.SelectionToCarrat;
				}
				else if (value.carratPos.position != this.carratPos.position || value.selectPos.position != this.selectPos.position)
				{
					result = global::UITextSelection.Change.SelectionAdjusted;
				}
				else
				{
					result = global::UITextSelection.Change.None;
				}
			}
			else if (value.hasSelection)
			{
				result = global::UITextSelection.Change.CarratToSelection;
			}
			else if (value.carratPos.position != this.carratPos.position)
			{
				result = global::UITextSelection.Change.CarratMove;
			}
			else
			{
				result = global::UITextSelection.Change.None;
			}
		}
		else if (value.carratPos.valid)
		{
			result = ((!value.hasSelection) ? global::UITextSelection.Change.NoneToCarrat : global::UITextSelection.Change.NoneToSelection);
		}
		else
		{
			result = global::UITextSelection.Change.None;
		}
		return result;
	}

	// Token: 0x06004D17 RID: 19735 RVA: 0x0012FF28 File Offset: 0x0012E128
	public override string ToString()
	{
		return string.Format("[hasSelection={0}, showCarrat={1}, highlight=[{2}->{3}], carratPos={4}, selectPos={5}]", new object[]
		{
			this.hasSelection,
			this.showCarrat,
			this.highlightBegin,
			this.highlightEnd,
			this.carratPos.ToString(),
			this.selectPos.ToString()
		});
	}

	// Token: 0x04002AF7 RID: 10999
	private const global::UITextSelection.Change kSelectChange_None = global::UITextSelection.Change.None;

	// Token: 0x04002AF8 RID: 11000
	private const global::UITextSelection.Change kSelectChange_DropCarrat = global::UITextSelection.Change.CarratToNone;

	// Token: 0x04002AF9 RID: 11001
	private const global::UITextSelection.Change kSelectChange_MoveCarrat = global::UITextSelection.Change.CarratMove;

	// Token: 0x04002AFA RID: 11002
	private const global::UITextSelection.Change kSelectChange_NewCarrat = global::UITextSelection.Change.NoneToCarrat;

	// Token: 0x04002AFB RID: 11003
	private const global::UITextSelection.Change kSelectChange_DropSelection = global::UITextSelection.Change.SelectionToCarrat;

	// Token: 0x04002AFC RID: 11004
	private const global::UITextSelection.Change kSelectChange_MoveSelection = global::UITextSelection.Change.SelectionAdjusted;

	// Token: 0x04002AFD RID: 11005
	private const global::UITextSelection.Change kSelectChange_NewSelection = global::UITextSelection.Change.CarratToSelection;

	// Token: 0x04002AFE RID: 11006
	private const global::UITextSelection.Change kSelectChange_DropAll = global::UITextSelection.Change.SelectionToNone;

	// Token: 0x04002AFF RID: 11007
	private const global::UITextSelection.Change kSelectChange_NewAll = global::UITextSelection.Change.NoneToSelection;

	// Token: 0x04002B00 RID: 11008
	public global::UITextPosition carratPos;

	// Token: 0x04002B01 RID: 11009
	public global::UITextPosition selectPos;

	// Token: 0x020008DA RID: 2266
	public enum Change : sbyte
	{
		// Token: 0x04002B03 RID: 11011
		None,
		// Token: 0x04002B04 RID: 11012
		NoneToCarrat,
		// Token: 0x04002B05 RID: 11013
		CarratMove,
		// Token: 0x04002B06 RID: 11014
		CarratToNone,
		// Token: 0x04002B07 RID: 11015
		CarratToSelection,
		// Token: 0x04002B08 RID: 11016
		SelectionAdjusted,
		// Token: 0x04002B09 RID: 11017
		SelectionToCarrat,
		// Token: 0x04002B0A RID: 11018
		NoneToSelection,
		// Token: 0x04002B0B RID: 11019
		SelectionToNone
	}
}
