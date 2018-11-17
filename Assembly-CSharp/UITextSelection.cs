using System;

// Token: 0x020007E8 RID: 2024
public struct UITextSelection
{
	// Token: 0x06004862 RID: 18530 RVA: 0x00125AB0 File Offset: 0x00123CB0
	public UITextSelection(UITextPosition carratPos, UITextPosition selectPos)
	{
		this.carratPos = carratPos;
		this.selectPos = selectPos;
	}

	// Token: 0x17000E0D RID: 3597
	// (get) Token: 0x06004863 RID: 18531 RVA: 0x00125AC0 File Offset: 0x00123CC0
	public bool hasSelection
	{
		get
		{
			return this.carratPos.valid && this.selectPos.valid && this.carratPos.position != this.selectPos.position;
		}
	}

	// Token: 0x17000E0E RID: 3598
	// (get) Token: 0x06004864 RID: 18532 RVA: 0x00125B0C File Offset: 0x00123D0C
	public bool showCarrat
	{
		get
		{
			return this.carratPos.valid && (!this.selectPos.valid || this.selectPos.position == this.carratPos.position);
		}
	}

	// Token: 0x17000E0F RID: 3599
	// (get) Token: 0x06004865 RID: 18533 RVA: 0x00125B58 File Offset: 0x00123D58
	public bool valid
	{
		get
		{
			return this.carratPos.valid;
		}
	}

	// Token: 0x06004866 RID: 18534 RVA: 0x00125B68 File Offset: 0x00123D68
	public bool GetHighlight(out UIHighlight h)
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
		h = UIHighlight.invalid;
		return false;
	}

	// Token: 0x17000E10 RID: 3600
	// (get) Token: 0x06004867 RID: 18535 RVA: 0x00125D08 File Offset: 0x00123F08
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

	// Token: 0x17000E11 RID: 3601
	// (get) Token: 0x06004868 RID: 18536 RVA: 0x00125D88 File Offset: 0x00123F88
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

	// Token: 0x17000E12 RID: 3602
	// (get) Token: 0x06004869 RID: 18537 RVA: 0x00125E08 File Offset: 0x00124008
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

	// Token: 0x17000E13 RID: 3603
	// (get) Token: 0x0600486A RID: 18538 RVA: 0x00125E60 File Offset: 0x00124060
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

	// Token: 0x0600486B RID: 18539 RVA: 0x00125EB8 File Offset: 0x001240B8
	public UITextSelection.Change GetChangesTo(ref UITextSelection value)
	{
		UITextSelection.Change result;
		if (this.carratPos.valid)
		{
			if (!value.carratPos.valid)
			{
				result = ((!this.hasSelection) ? UITextSelection.Change.CarratToNone : UITextSelection.Change.SelectionToNone);
			}
			else if (this.hasSelection)
			{
				if (!value.hasSelection)
				{
					result = UITextSelection.Change.SelectionToCarrat;
				}
				else if (value.carratPos.position != this.carratPos.position || value.selectPos.position != this.selectPos.position)
				{
					result = UITextSelection.Change.SelectionAdjusted;
				}
				else
				{
					result = UITextSelection.Change.None;
				}
			}
			else if (value.hasSelection)
			{
				result = UITextSelection.Change.CarratToSelection;
			}
			else if (value.carratPos.position != this.carratPos.position)
			{
				result = UITextSelection.Change.CarratMove;
			}
			else
			{
				result = UITextSelection.Change.None;
			}
		}
		else if (value.carratPos.valid)
		{
			result = ((!value.hasSelection) ? UITextSelection.Change.NoneToCarrat : UITextSelection.Change.NoneToSelection);
		}
		else
		{
			result = UITextSelection.Change.None;
		}
		return result;
	}

	// Token: 0x0600486C RID: 18540 RVA: 0x00125FC4 File Offset: 0x001241C4
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

	// Token: 0x040028A9 RID: 10409
	private const UITextSelection.Change kSelectChange_None = UITextSelection.Change.None;

	// Token: 0x040028AA RID: 10410
	private const UITextSelection.Change kSelectChange_DropCarrat = UITextSelection.Change.CarratToNone;

	// Token: 0x040028AB RID: 10411
	private const UITextSelection.Change kSelectChange_MoveCarrat = UITextSelection.Change.CarratMove;

	// Token: 0x040028AC RID: 10412
	private const UITextSelection.Change kSelectChange_NewCarrat = UITextSelection.Change.NoneToCarrat;

	// Token: 0x040028AD RID: 10413
	private const UITextSelection.Change kSelectChange_DropSelection = UITextSelection.Change.SelectionToCarrat;

	// Token: 0x040028AE RID: 10414
	private const UITextSelection.Change kSelectChange_MoveSelection = UITextSelection.Change.SelectionAdjusted;

	// Token: 0x040028AF RID: 10415
	private const UITextSelection.Change kSelectChange_NewSelection = UITextSelection.Change.CarratToSelection;

	// Token: 0x040028B0 RID: 10416
	private const UITextSelection.Change kSelectChange_DropAll = UITextSelection.Change.SelectionToNone;

	// Token: 0x040028B1 RID: 10417
	private const UITextSelection.Change kSelectChange_NewAll = UITextSelection.Change.NoneToSelection;

	// Token: 0x040028B2 RID: 10418
	public UITextPosition carratPos;

	// Token: 0x040028B3 RID: 10419
	public UITextPosition selectPos;

	// Token: 0x020007E9 RID: 2025
	public enum Change : sbyte
	{
		// Token: 0x040028B5 RID: 10421
		None,
		// Token: 0x040028B6 RID: 10422
		NoneToCarrat,
		// Token: 0x040028B7 RID: 10423
		CarratMove,
		// Token: 0x040028B8 RID: 10424
		CarratToNone,
		// Token: 0x040028B9 RID: 10425
		CarratToSelection,
		// Token: 0x040028BA RID: 10426
		SelectionAdjusted,
		// Token: 0x040028BB RID: 10427
		SelectionToCarrat,
		// Token: 0x040028BC RID: 10428
		NoneToSelection,
		// Token: 0x040028BD RID: 10429
		SelectionToNone
	}
}
