using System;
using System.Collections.Generic;
using NGUI.Meshing;
using UnityEngine;

// Token: 0x020007F4 RID: 2036
[AddComponentMenu("NGUI/UI/Label")]
[ExecuteInEditMode]
public class UILabel : UIWidget
{
	// Token: 0x060048D5 RID: 18645 RVA: 0x0012A600 File Offset: 0x00128800
	public UILabel() : base(UIWidget.WidgetFlags.CustomRelativeSize | UIWidget.WidgetFlags.CustomMaterialGet)
	{
	}

	// Token: 0x17000E27 RID: 3623
	// (get) Token: 0x060048D6 RID: 18646 RVA: 0x0012A6AC File Offset: 0x001288AC
	private List<UITextMarkup> markups
	{
		get
		{
			List<UITextMarkup> result;
			if ((result = this._markups) == null)
			{
				result = (this._markups = new List<UITextMarkup>());
			}
			return result;
		}
	}

	// Token: 0x17000E28 RID: 3624
	// (get) Token: 0x060048D7 RID: 18647 RVA: 0x0012A6D4 File Offset: 0x001288D4
	// (set) Token: 0x060048D8 RID: 18648 RVA: 0x0012A6DC File Offset: 0x001288DC
	public bool invisibleHack
	{
		get
		{
			return this.mInvisibleHack;
		}
		set
		{
			if (this.mInvisibleHack != value)
			{
				this.mInvisibleHack = value;
				this.ForceChanges();
			}
		}
	}

	// Token: 0x060048D9 RID: 18649 RVA: 0x0012A6F8 File Offset: 0x001288F8
	private bool PendingChanges()
	{
		return this.mShouldBeProcessed || this.mLastText != this.text || this.mInvisibleHack != this.mLastInvisibleHack || this.mLastWidth != this.mMaxLineWidth || this.mLastEncoding != this.mEncoding || this.mLastCount != this.mMaxLineCount || this.mLastPass != this.mPassword || this.mLastShow != this.mShowLastChar || this.mLastEffect != this.mEffectStyle || this.mLastColor != this.mEffectColor;
	}

	// Token: 0x060048DA RID: 18650 RVA: 0x0012A7B4 File Offset: 0x001289B4
	private void ApplyChanges()
	{
		this.mShouldBeProcessed = false;
		this.mLastText = this.text;
		this.mLastInvisibleHack = this.mInvisibleHack;
		this.mLastWidth = this.mMaxLineWidth;
		this.mLastEncoding = this.mEncoding;
		this.mLastCount = this.mMaxLineCount;
		this.mLastPass = this.mPassword;
		this.mLastShow = this.mShowLastChar;
		this.mLastEffect = this.mEffectStyle;
		this.mLastColor = this.mEffectColor;
	}

	// Token: 0x060048DB RID: 18651 RVA: 0x0012A834 File Offset: 0x00128A34
	private void ForceChanges()
	{
		base.ChangedAuto();
		this.mShouldBeProcessed = true;
	}

	// Token: 0x17000E29 RID: 3625
	// (get) Token: 0x060048DC RID: 18652 RVA: 0x0012A844 File Offset: 0x00128A44
	// (set) Token: 0x060048DD RID: 18653 RVA: 0x0012A84C File Offset: 0x00128A4C
	public UIFont font
	{
		get
		{
			return this.mFont;
		}
		set
		{
			if (this.mFont != value)
			{
				this.mFont = value;
				base.baseMaterial = ((!(this.mFont != null)) ? null : ((UIMaterial)this.mFont.material));
				base.ChangedAuto();
				this.ForceChanges();
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000E2A RID: 3626
	// (get) Token: 0x060048DE RID: 18654 RVA: 0x0012A8B0 File Offset: 0x00128AB0
	// (set) Token: 0x060048DF RID: 18655 RVA: 0x0012A8B8 File Offset: 0x00128AB8
	public string text
	{
		get
		{
			return this.mText;
		}
		set
		{
			if (value != null && this.mText != value)
			{
				this.mText = value;
				this.ForceChanges();
			}
		}
	}

	// Token: 0x17000E2B RID: 3627
	// (get) Token: 0x060048E0 RID: 18656 RVA: 0x0012A8EC File Offset: 0x00128AEC
	// (set) Token: 0x060048E1 RID: 18657 RVA: 0x0012A8F4 File Offset: 0x00128AF4
	public bool supportEncoding
	{
		get
		{
			return this.mEncoding;
		}
		set
		{
			if (this.mEncoding != value)
			{
				this.mEncoding = value;
				this.ForceChanges();
				if (value)
				{
					this.mPassword = false;
				}
			}
		}
	}

	// Token: 0x17000E2C RID: 3628
	// (get) Token: 0x060048E2 RID: 18658 RVA: 0x0012A928 File Offset: 0x00128B28
	// (set) Token: 0x060048E3 RID: 18659 RVA: 0x0012A930 File Offset: 0x00128B30
	public bool overflowRight
	{
		get
		{
			return this.mOverflowRight;
		}
		set
		{
			if (this.mOverflowRight != value)
			{
				this.mOverflowRight = value;
				UIWidget.Pivot pivot = base.pivot;
				switch (pivot)
				{
				case UIWidget.Pivot.TopLeft:
				case UIWidget.Pivot.Left:
					break;
				default:
					if (pivot != UIWidget.Pivot.BottomLeft)
					{
						return;
					}
					break;
				}
				this.ForceChanges();
			}
		}
	}

	// Token: 0x17000E2D RID: 3629
	// (get) Token: 0x060048E4 RID: 18660 RVA: 0x0012A984 File Offset: 0x00128B84
	// (set) Token: 0x060048E5 RID: 18661 RVA: 0x0012A98C File Offset: 0x00128B8C
	public UIFont.SymbolStyle symbolStyle
	{
		get
		{
			return this.mSymbols;
		}
		set
		{
			if (this.mSymbols != value)
			{
				this.mSymbols = value;
				this.ForceChanges();
			}
		}
	}

	// Token: 0x17000E2E RID: 3630
	// (get) Token: 0x060048E6 RID: 18662 RVA: 0x0012A9A8 File Offset: 0x00128BA8
	// (set) Token: 0x060048E7 RID: 18663 RVA: 0x0012A9B0 File Offset: 0x00128BB0
	public int lineWidth
	{
		get
		{
			return this.mMaxLineWidth;
		}
		set
		{
			if (this.mMaxLineWidth != value)
			{
				this.mMaxLineWidth = value;
				this.ForceChanges();
			}
		}
	}

	// Token: 0x17000E2F RID: 3631
	// (get) Token: 0x060048E8 RID: 18664 RVA: 0x0012A9CC File Offset: 0x00128BCC
	// (set) Token: 0x060048E9 RID: 18665 RVA: 0x0012A9DC File Offset: 0x00128BDC
	public bool multiLine
	{
		get
		{
			return this.mMaxLineCount != 1;
		}
		set
		{
			if (this.mMaxLineCount != 1 != value)
			{
				this.mMaxLineCount = ((!value) ? 1 : 0);
				this.ForceChanges();
				if (value)
				{
					this.mPassword = false;
				}
			}
		}
	}

	// Token: 0x17000E30 RID: 3632
	// (get) Token: 0x060048EA RID: 18666 RVA: 0x0012AA24 File Offset: 0x00128C24
	// (set) Token: 0x060048EB RID: 18667 RVA: 0x0012AA2C File Offset: 0x00128C2C
	public int maxLineCount
	{
		get
		{
			return this.mMaxLineCount;
		}
		set
		{
			if (this.mMaxLineCount != value)
			{
				this.mMaxLineCount = Mathf.Max(value, 0);
				this.ForceChanges();
				if (value == 1)
				{
					this.mPassword = false;
				}
			}
		}
	}

	// Token: 0x17000E31 RID: 3633
	// (get) Token: 0x060048EC RID: 18668 RVA: 0x0012AA5C File Offset: 0x00128C5C
	// (set) Token: 0x060048ED RID: 18669 RVA: 0x0012AA64 File Offset: 0x00128C64
	public bool password
	{
		get
		{
			return this.mPassword;
		}
		set
		{
			if (this.mPassword != value)
			{
				this.mPassword = value;
				this.mMaxLineCount = 1;
				this.mEncoding = false;
				this.ForceChanges();
			}
		}
	}

	// Token: 0x17000E32 RID: 3634
	// (get) Token: 0x060048EE RID: 18670 RVA: 0x0012AA90 File Offset: 0x00128C90
	// (set) Token: 0x060048EF RID: 18671 RVA: 0x0012AA98 File Offset: 0x00128C98
	public bool showLastPasswordChar
	{
		get
		{
			return this.mShowLastChar;
		}
		set
		{
			if (this.mShowLastChar != value)
			{
				this.mShowLastChar = value;
				this.ForceChanges();
			}
		}
	}

	// Token: 0x17000E33 RID: 3635
	// (get) Token: 0x060048F0 RID: 18672 RVA: 0x0012AAB4 File Offset: 0x00128CB4
	// (set) Token: 0x060048F1 RID: 18673 RVA: 0x0012AABC File Offset: 0x00128CBC
	public UILabel.Effect effectStyle
	{
		get
		{
			return this.mEffectStyle;
		}
		set
		{
			if (this.mEffectStyle != value)
			{
				this.mEffectStyle = value;
				this.ForceChanges();
			}
		}
	}

	// Token: 0x17000E34 RID: 3636
	// (get) Token: 0x060048F2 RID: 18674 RVA: 0x0012AAD8 File Offset: 0x00128CD8
	// (set) Token: 0x060048F3 RID: 18675 RVA: 0x0012AAE0 File Offset: 0x00128CE0
	public Color effectColor
	{
		get
		{
			return this.mEffectColor;
		}
		set
		{
			if (this.mEffectColor != value)
			{
				this.mEffectColor = value;
				if (this.mEffectStyle != UILabel.Effect.None)
				{
					this.ForceChanges();
				}
			}
		}
	}

	// Token: 0x17000E35 RID: 3637
	// (get) Token: 0x060048F4 RID: 18676 RVA: 0x0012AB0C File Offset: 0x00128D0C
	public string processedText
	{
		get
		{
			if (this.mLastScale != base.cachedTransform.localScale)
			{
				this.mLastScale = base.cachedTransform.localScale;
				this.mShouldBeProcessed = true;
			}
			if (this.PendingChanges())
			{
				this.ProcessText();
			}
			return this.mProcessedText;
		}
	}

	// Token: 0x17000E36 RID: 3638
	// (get) Token: 0x060048F5 RID: 18677 RVA: 0x0012AB64 File Offset: 0x00128D64
	// (set) Token: 0x060048F6 RID: 18678 RVA: 0x0012AB6C File Offset: 0x00128D6C
	public Color highlightTextColor
	{
		get
		{
			return this.mHighlightTextColor;
		}
		set
		{
			if (this.mHighlightTextColor != value)
			{
				bool flag = (this.hasSelection && this.mHighlightColor.a > 0f) || value.a > 0f;
				this.mHighlightTextColor = value;
				if (flag)
				{
					this.ForceChanges();
				}
			}
		}
	}

	// Token: 0x17000E37 RID: 3639
	// (get) Token: 0x060048F7 RID: 18679 RVA: 0x0012ABD0 File Offset: 0x00128DD0
	// (set) Token: 0x060048F8 RID: 18680 RVA: 0x0012ABD8 File Offset: 0x00128DD8
	public Color highlightColor
	{
		get
		{
			return this.mHighlightColor;
		}
		set
		{
			if (this.mHighlightColor != value)
			{
				bool flag = (this.hasSelection && this.mHighlightChar != '\0' && this.mHighlightColor.a > 0f) || value.a > 0f;
				this.mHighlightColor = value;
				if (flag)
				{
					this.ForceChanges();
				}
			}
		}
	}

	// Token: 0x17000E38 RID: 3640
	// (get) Token: 0x060048F9 RID: 18681 RVA: 0x0012AC48 File Offset: 0x00128E48
	// (set) Token: 0x060048FA RID: 18682 RVA: 0x0012AC50 File Offset: 0x00128E50
	public char highlightChar
	{
		get
		{
			return this.mHighlightChar;
		}
		set
		{
			if (this.mHighlightChar != value)
			{
				bool flag = this.hasSelection && this.mHighlightColor.a > 0f;
				this.mHighlightChar = value;
				if (flag)
				{
					this.ForceChanges();
				}
			}
		}
	}

	// Token: 0x17000E39 RID: 3641
	// (get) Token: 0x060048FB RID: 18683 RVA: 0x0012ACA0 File Offset: 0x00128EA0
	// (set) Token: 0x060048FC RID: 18684 RVA: 0x0012ACA8 File Offset: 0x00128EA8
	public float highlightCharSplit
	{
		get
		{
			return this.mHighlightCharSplit;
		}
		set
		{
			if (value > 1f)
			{
				value = 1f;
			}
			else if (value < 0f)
			{
				value = 0f;
			}
			if (this.mHighlightCharSplit != value)
			{
				bool flag = this.hasSelection && this.mHighlightColor.a > 0f && this.mHighlightChar != '\0';
				this.mHighlightCharSplit = value;
				if (flag)
				{
					this.ForceChanges();
				}
			}
		}
	}

	// Token: 0x17000E3A RID: 3642
	// (get) Token: 0x060048FD RID: 18685 RVA: 0x0012AD30 File Offset: 0x00128F30
	// (set) Token: 0x060048FE RID: 18686 RVA: 0x0012AD38 File Offset: 0x00128F38
	public char carratChar
	{
		get
		{
			return this.mCarratChar;
		}
		set
		{
			if (this.mCarratChar != value)
			{
				bool shouldShowCarrat = this.shouldShowCarrat;
				this.mCarratChar = value;
				if (shouldShowCarrat)
				{
					this.ForceChanges();
				}
			}
		}
	}

	// Token: 0x17000E3B RID: 3643
	// (get) Token: 0x060048FF RID: 18687 RVA: 0x0012AD6C File Offset: 0x00128F6C
	public bool hasSelection
	{
		get
		{
			return this.mSelection.hasSelection;
		}
	}

	// Token: 0x17000E3C RID: 3644
	// (get) Token: 0x06004900 RID: 18688 RVA: 0x0012AD7C File Offset: 0x00128F7C
	public bool shouldShowCarrat
	{
		get
		{
			return this.mSelection.showCarrat;
		}
	}

	// Token: 0x17000E3D RID: 3645
	// (get) Token: 0x06004901 RID: 18689 RVA: 0x0012AD8C File Offset: 0x00128F8C
	public bool drawingCarrat
	{
		get
		{
			return this.mCarratChar != '\0' && this.mSelection.showCarrat;
		}
	}

	// Token: 0x17000E3E RID: 3646
	// (get) Token: 0x06004902 RID: 18690 RVA: 0x0012ADA8 File Offset: 0x00128FA8
	public UITextPosition carratPosition
	{
		get
		{
			return this.mSelection.carratPos;
		}
	}

	// Token: 0x17000E3F RID: 3647
	// (get) Token: 0x06004903 RID: 18691 RVA: 0x0012ADB8 File Offset: 0x00128FB8
	public UITextPosition selectPosition
	{
		get
		{
			return this.mSelection.selectPos;
		}
	}

	// Token: 0x17000E40 RID: 3648
	// (get) Token: 0x06004904 RID: 18692 RVA: 0x0012ADC8 File Offset: 0x00128FC8
	private bool highlightWouldBeVisibleIfOn
	{
		get
		{
			return (this.mHighlightChar != '\0' && this.mHighlightColor.a > 0f) || this.mHighlightTextColor != base.color;
		}
	}

	// Token: 0x17000E41 RID: 3649
	// (get) Token: 0x06004905 RID: 18693 RVA: 0x0012AE0C File Offset: 0x0012900C
	private bool carratWouldBeVisibleIfOn
	{
		get
		{
			return this.mCarratChar != '\0';
		}
	}

	// Token: 0x17000E42 RID: 3650
	// (get) Token: 0x06004906 RID: 18694 RVA: 0x0012AE1C File Offset: 0x0012901C
	// (set) Token: 0x06004907 RID: 18695 RVA: 0x0012AE24 File Offset: 0x00129024
	public UITextSelection selection
	{
		get
		{
			return this.mSelection;
		}
		set
		{
			UITextSelection.Change changesTo = this.mSelection.GetChangesTo(ref value);
			this.mSelection = value;
			switch (changesTo)
			{
			case UITextSelection.Change.NoneToCarrat:
			case UITextSelection.Change.CarratMove:
			case UITextSelection.Change.CarratToNone:
				if (this.carratWouldBeVisibleIfOn)
				{
					this.ForceChanges();
				}
				break;
			case UITextSelection.Change.CarratToSelection:
			case UITextSelection.Change.SelectionToCarrat:
				if (this.carratWouldBeVisibleIfOn || this.highlightWouldBeVisibleIfOn)
				{
					this.ForceChanges();
				}
				break;
			case UITextSelection.Change.SelectionAdjusted:
			case UITextSelection.Change.NoneToSelection:
			case UITextSelection.Change.SelectionToNone:
				if (this.highlightWouldBeVisibleIfOn)
				{
					this.ForceChanges();
				}
				break;
			}
		}
	}

	// Token: 0x17000E43 RID: 3651
	// (get) Token: 0x06004908 RID: 18696 RVA: 0x0012AECC File Offset: 0x001290CC
	// (set) Token: 0x06004909 RID: 18697 RVA: 0x0012AF1C File Offset: 0x0012911C
	public new UIMaterial material
	{
		get
		{
			UIMaterial uimaterial = base.baseMaterial;
			if (uimaterial == null)
			{
				uimaterial = ((!(this.mFont != null)) ? null : ((UIMaterial)this.mFont.material));
				this.material = uimaterial;
			}
			return uimaterial;
		}
		set
		{
			base.material = value;
		}
	}

	// Token: 0x17000E44 RID: 3652
	// (get) Token: 0x0600490A RID: 18698 RVA: 0x0012AF28 File Offset: 0x00129128
	protected override UIMaterial customMaterial
	{
		get
		{
			return this.material;
		}
	}

	// Token: 0x17000E45 RID: 3653
	// (get) Token: 0x0600490B RID: 18699 RVA: 0x0012AF30 File Offset: 0x00129130
	public new Vector2 relativeSize
	{
		get
		{
			if (this.mFont == null)
			{
				return Vector3.one;
			}
			if (this.PendingChanges())
			{
				this.ProcessText();
			}
			return this.mSize;
		}
	}

	// Token: 0x0600490C RID: 18700 RVA: 0x0012AF78 File Offset: 0x00129178
	protected override void GetCustomVector2s(int start, int end, UIWidget.WidgetFlags[] flags, Vector2[] v)
	{
		for (int i = 0; i < end; i++)
		{
			if (flags[i] == UIWidget.WidgetFlags.CustomRelativeSize)
			{
				v[i] = this.relativeSize;
			}
			else
			{
				base.GetCustomVector2s(i, i + 1, flags, v);
			}
		}
	}

	// Token: 0x0600490D RID: 18701 RVA: 0x0012AFC8 File Offset: 0x001291C8
	protected override void OnStart()
	{
		if (this.mLineWidth > 0f)
		{
			this.mMaxLineWidth = Mathf.RoundToInt(this.mLineWidth);
			this.mLineWidth = 0f;
		}
		if (!this.mMultiline)
		{
			this.mMaxLineCount = 1;
			this.mMultiline = true;
		}
	}

	// Token: 0x0600490E RID: 18702 RVA: 0x0012B01C File Offset: 0x0012921C
	public override void MarkAsChanged()
	{
		this.ForceChanges();
		base.MarkAsChanged();
	}

	// Token: 0x0600490F RID: 18703 RVA: 0x0012B02C File Offset: 0x0012922C
	private void ProcessText()
	{
		base.ChangedAuto();
		this.mLastText = this.mText;
		this.markups.Clear();
		string a = this.mProcessedText;
		this.mProcessedText = this.mText;
		if (this.mPassword)
		{
			this.mProcessedText = this.mFont.WrapText(this.markups, this.mProcessedText, 100000f, 1, false, UIFont.SymbolStyle.None);
			string text = string.Empty;
			if (this.mShowLastChar)
			{
				int i = 1;
				int length = this.mProcessedText.Length;
				while (i < length)
				{
					text += "*";
					i++;
				}
				if (this.mProcessedText.Length > 0)
				{
					text += this.mProcessedText[this.mProcessedText.Length - 1];
				}
			}
			else
			{
				int j = 0;
				int length2 = this.mProcessedText.Length;
				while (j < length2)
				{
					text += "*";
					j++;
				}
			}
			this.mProcessedText = text;
		}
		else if (this.mMaxLineWidth > 0)
		{
			this.mProcessedText = this.mFont.WrapText(this.markups, this.mProcessedText, (float)this.mMaxLineWidth / base.cachedTransform.localScale.x, this.mMaxLineCount, this.mEncoding, this.mSymbols);
		}
		else if (this.mMaxLineCount > 0)
		{
			this.mProcessedText = this.mFont.WrapText(this.markups, this.mProcessedText, 100000f, this.mMaxLineCount, this.mEncoding, this.mSymbols);
		}
		this.mSize = (string.IsNullOrEmpty(this.mProcessedText) ? Vector2.one : this.mFont.CalculatePrintedSize(this.mProcessedText, this.mEncoding, this.mSymbols));
		float x = base.cachedTransform.localScale.x;
		this.mSize.x = Mathf.Max(this.mSize.x, (!(this.mFont != null) || x <= 1f) ? 1f : ((float)this.lineWidth / x));
		this.mSize.y = Mathf.Max(this.mSize.y, 1f);
		if (a != this.mProcessedText)
		{
			this.mSelection = default(UITextSelection);
		}
		this.ApplyChanges();
	}

	// Token: 0x06004910 RID: 18704 RVA: 0x0012B2D4 File Offset: 0x001294D4
	public int CalculateTextPosition(Space space, Vector3[] points, UITextPosition[] positions)
	{
		if (!this.mFont)
		{
			return -1;
		}
		string processedText = this.processedText;
		int num;
		if (space == 1)
		{
			num = this.mFont.CalculatePlacement(points, positions, processedText);
		}
		else
		{
			num = this.mFont.CalculatePlacement(points, positions, processedText, base.cachedTransform.worldToLocalMatrix);
		}
		int num2 = -1;
		for (int i = 0; i < num; i++)
		{
			this.ConvertProcessedTextPosition(ref positions[i], ref num2);
		}
		return num;
	}

	// Token: 0x06004911 RID: 18705 RVA: 0x0012B354 File Offset: 0x00129554
	public void MakePositionPerfect()
	{
		float num = (!(this.font.atlas != null)) ? 1f : this.font.atlas.pixelSize;
		Vector3 localScale = base.cachedTransform.localScale;
		if (this.mFont.size == Mathf.RoundToInt(localScale.x / num) && this.mFont.size == Mathf.RoundToInt(localScale.y / num) && base.cachedTransform.localRotation == Quaternion.identity)
		{
			Vector2 vector = this.relativeSize * localScale.x;
			int num2 = Mathf.RoundToInt(vector.x / num);
			int num3 = Mathf.RoundToInt(vector.y / num);
			Vector3 localPosition = base.cachedTransform.localPosition;
			localPosition.x = (float)Mathf.FloorToInt(localPosition.x / num);
			localPosition.y = (float)Mathf.CeilToInt(localPosition.y / num);
			localPosition.z = (float)Mathf.RoundToInt(localPosition.z);
			if (num2 % 2 == 1 && (base.pivot == UIWidget.Pivot.Top || base.pivot == UIWidget.Pivot.Center || base.pivot == UIWidget.Pivot.Bottom))
			{
				localPosition.x += 0.5f;
			}
			if (num3 % 2 == 1 && (base.pivot == UIWidget.Pivot.Left || base.pivot == UIWidget.Pivot.Center || base.pivot == UIWidget.Pivot.Right))
			{
				localPosition.y -= 0.5f;
			}
			localPosition.x *= num;
			localPosition.y *= num;
			if (base.cachedTransform.localPosition != localPosition)
			{
				base.cachedTransform.localPosition = localPosition;
			}
		}
	}

	// Token: 0x06004912 RID: 18706 RVA: 0x0012B538 File Offset: 0x00129738
	public override void MakePixelPerfect()
	{
		if (this.mFont != null)
		{
			float num = (!(this.font.atlas != null)) ? 1f : this.font.atlas.pixelSize;
			Vector3 localScale = base.cachedTransform.localScale;
			localScale.x = (float)this.mFont.size * num;
			localScale.y = localScale.x;
			localScale.z = 1f;
			Vector2 vector = this.relativeSize * localScale.x;
			int num2 = Mathf.RoundToInt(vector.x / num);
			int num3 = Mathf.RoundToInt(vector.y / num);
			Vector3 localPosition = base.cachedTransform.localPosition;
			localPosition.x = (float)Mathf.FloorToInt(localPosition.x / num);
			localPosition.y = (float)Mathf.CeilToInt(localPosition.y / num);
			localPosition.z = (float)Mathf.RoundToInt(localPosition.z);
			if (base.cachedTransform.localRotation == Quaternion.identity)
			{
				if (num2 % 2 == 1 && (base.pivot == UIWidget.Pivot.Top || base.pivot == UIWidget.Pivot.Center || base.pivot == UIWidget.Pivot.Bottom))
				{
					localPosition.x += 0.5f;
				}
				if (num3 % 2 == 1 && (base.pivot == UIWidget.Pivot.Left || base.pivot == UIWidget.Pivot.Center || base.pivot == UIWidget.Pivot.Right))
				{
					localPosition.y -= 0.5f;
				}
			}
			localPosition.x *= num;
			localPosition.y *= num;
			base.cachedTransform.localPosition = localPosition;
			base.cachedTransform.localScale = localScale;
		}
		else
		{
			base.MakePixelPerfect();
		}
	}

	// Token: 0x06004913 RID: 18707 RVA: 0x0012B720 File Offset: 0x00129920
	public override void OnFill(MeshBuffer m)
	{
		if (this.mFont == null)
		{
			return;
		}
		Color normalColor = (!this.mInvisibleHack) ? base.color : Color.clear;
		this.MakePositionPerfect();
		UIWidget.Pivot pivot = base.pivot;
		int vSize = m.vSize;
		if (pivot == UIWidget.Pivot.Left || pivot == UIWidget.Pivot.TopLeft || pivot == UIWidget.Pivot.BottomLeft)
		{
			if (this.mOverflowRight)
			{
				this.mFont.Print(this.processedText, normalColor, m, this.mEncoding, this.mSymbols, UIFont.Alignment.LeftOverflowRight, Mathf.RoundToInt(this.relativeSize.x * (float)this.mFont.size), ref this.mSelection, this.mCarratChar, this.mHighlightTextColor, this.mHighlightColor, this.mHighlightChar, this.mHighlightCharSplit);
			}
			else
			{
				this.mFont.Print(this.processedText, normalColor, m, this.mEncoding, this.mSymbols, UIFont.Alignment.Left, 0, ref this.mSelection, this.mCarratChar, this.mHighlightTextColor, this.mHighlightColor, this.mHighlightChar, this.mHighlightCharSplit);
			}
		}
		else if (pivot == UIWidget.Pivot.Right || pivot == UIWidget.Pivot.TopRight || pivot == UIWidget.Pivot.BottomRight)
		{
			this.mFont.Print(this.processedText, normalColor, m, this.mEncoding, this.mSymbols, UIFont.Alignment.Right, Mathf.RoundToInt(this.relativeSize.x * (float)this.mFont.size), ref this.mSelection, this.mCarratChar, this.mHighlightTextColor, this.mHighlightColor, this.mHighlightChar, this.mHighlightCharSplit);
		}
		else
		{
			this.mFont.Print(this.processedText, normalColor, m, this.mEncoding, this.mSymbols, UIFont.Alignment.Center, Mathf.RoundToInt(this.relativeSize.x * (float)this.mFont.size), ref this.mSelection, this.mCarratChar, this.mHighlightTextColor, this.mHighlightColor, this.mHighlightChar, this.mHighlightCharSplit);
		}
		m.ApplyEffect(base.cachedTransform, vSize, this.effectStyle, this.effectColor, (float)this.mFont.size);
	}

	// Token: 0x06004914 RID: 18708 RVA: 0x0012B94C File Offset: 0x00129B4C
	public void UnionProcessedChanges(string newProcessedText)
	{
		this.text = newProcessedText;
	}

	// Token: 0x06004915 RID: 18709 RVA: 0x0012B958 File Offset: 0x00129B58
	private void ConvertProcessedTextPosition(ref UITextPosition p, ref int markupCount)
	{
		if (markupCount == -1)
		{
			markupCount = this.markups.Count;
		}
		if (markupCount == 0)
		{
			return;
		}
		UITextMarkup uitextMarkup = this.markups[0];
		int num = 0;
		while (p.position <= uitextMarkup.index)
		{
			switch (uitextMarkup.mod)
			{
			case UITextMod.End:
				p.deformed = (short)(this.mText.Length - uitextMarkup.index);
				break;
			case UITextMod.Removed:
				p.deformed += 1;
				if (++num < markupCount)
				{
					uitextMarkup = this.markups[num];
					continue;
				}
				break;
			case UITextMod.Replaced:
				if (++num < markupCount)
				{
					uitextMarkup = this.markups[num];
					continue;
				}
				break;
			case UITextMod.Added:
				p.deformed -= 1;
				if (++num < markupCount)
				{
					uitextMarkup = this.markups[num];
					continue;
				}
				break;
			default:
				if (++num < markupCount)
				{
					uitextMarkup = this.markups[num];
					continue;
				}
				break;
			}
			break;
		}
	}

	// Token: 0x06004916 RID: 18710 RVA: 0x0012BA9C File Offset: 0x00129C9C
	public void GetProcessedIndices(ref int start, ref int end)
	{
		int count = this.markups.Count;
		if (count == 0 || this.markups[0].index > end)
		{
			return;
		}
		int num = start;
		int num2 = end;
		int num3 = 0;
		while (this.markups[num3].index <= start)
		{
			switch (this.markups[num3].mod)
			{
			case UITextMod.End:
				num2 = this.mProcessedText.Length - 1;
				return;
			case UITextMod.Removed:
				num--;
				num2--;
				break;
			case UITextMod.Added:
				num++;
				num2++;
				break;
			}
			if (++num3 >= count)
			{
				start = num;
				end = num2;
				return;
			}
		}
		while (this.markups[num3].index <= end)
		{
			switch (this.markups[num3].mod)
			{
			case UITextMod.End:
				num2 = this.mProcessedText.Length - 1;
				return;
			case UITextMod.Removed:
				num2--;
				break;
			case UITextMod.Added:
				num2++;
				break;
			}
			if (++num3 >= count)
			{
				break;
			}
		}
		start = num;
		end = num2;
	}

	// Token: 0x06004917 RID: 18711 RVA: 0x0012BBFC File Offset: 0x00129DFC
	private static void CountLinesGetColumn(string text, int inPos, out int pos, out int lines, out int column, out UITextRegion region)
	{
		if (inPos < 0)
		{
			region = UITextRegion.Before;
			pos = 0;
			lines = 0;
			column = 0;
		}
		else if (inPos == 0)
		{
			pos = 0;
			lines = 0;
			column = 0;
			region = UITextRegion.Pre;
		}
		else
		{
			if (inPos > text.Length)
			{
				region = UITextRegion.End;
				pos = text.Length;
			}
			else if (inPos == text.Length)
			{
				region = UITextRegion.Past;
				pos = inPos;
			}
			else
			{
				region = UITextRegion.Inside;
				pos = inPos;
			}
			int num = text.IndexOf('\n', 0, pos);
			if (num == -1)
			{
				lines = 0;
				column = pos;
			}
			else
			{
				int num2 = num;
				lines = 1;
				while (++num < pos)
				{
					num = text.IndexOf('\n', num, pos - num);
					if (num == -1)
					{
						break;
					}
					lines++;
					num2 = num;
				}
				column = pos - (num2 + 1);
			}
		}
	}

	// Token: 0x06004918 RID: 18712 RVA: 0x0012BCDC File Offset: 0x00129EDC
	public UITextPosition ConvertUnprocessedPosition(int position)
	{
		string processedText = this.processedText;
		int count = this.markups.Count;
		int num = position;
		if (count > 0)
		{
			int num2 = 0;
			UITextMarkup uitextMarkup = this.markups[num2];
			while (uitextMarkup.index <= position)
			{
				switch (uitextMarkup.mod)
				{
				case UITextMod.End:
					position -= num - uitextMarkup.index;
					num2 = count;
					break;
				case UITextMod.Removed:
					position--;
					break;
				case UITextMod.Added:
					position++;
					break;
				}
				if (++num2 >= count)
				{
					break;
				}
				uitextMarkup = this.markups[num2];
			}
		}
		UITextPosition result;
		UILabel.CountLinesGetColumn(processedText, position, out result.position, out result.line, out result.column, out result.region);
		result.uniformRegion = result.region;
		result.deformed = (short)(num - result.position);
		return result;
	}

	// Token: 0x06004919 RID: 18713 RVA: 0x0012BDDC File Offset: 0x00129FDC
	public UITextSelection ConvertUnprocessedSelection(int carratPos, int selectPos)
	{
		UITextSelection result;
		result.carratPos = this.ConvertUnprocessedPosition(carratPos);
		if (carratPos == selectPos)
		{
			result.selectPos = result.carratPos;
		}
		else
		{
			result.selectPos = this.ConvertUnprocessedPosition(selectPos);
		}
		return result;
	}

	// Token: 0x04002924 RID: 10532
	[HideInInspector]
	[SerializeField]
	private UIFont mFont;

	// Token: 0x04002925 RID: 10533
	[SerializeField]
	[HideInInspector]
	private string mText = string.Empty;

	// Token: 0x04002926 RID: 10534
	[SerializeField]
	[HideInInspector]
	private int mMaxLineWidth;

	// Token: 0x04002927 RID: 10535
	[HideInInspector]
	[SerializeField]
	private bool mEncoding = true;

	// Token: 0x04002928 RID: 10536
	[HideInInspector]
	[SerializeField]
	private int mMaxLineCount;

	// Token: 0x04002929 RID: 10537
	[SerializeField]
	[HideInInspector]
	private bool mPassword;

	// Token: 0x0400292A RID: 10538
	[HideInInspector]
	[SerializeField]
	private bool mShowLastChar;

	// Token: 0x0400292B RID: 10539
	[SerializeField]
	[HideInInspector]
	private bool mOverflowRight;

	// Token: 0x0400292C RID: 10540
	[SerializeField]
	[HideInInspector]
	private UILabel.Effect mEffectStyle;

	// Token: 0x0400292D RID: 10541
	[SerializeField]
	[HideInInspector]
	private Color mEffectColor = Color.black;

	// Token: 0x0400292E RID: 10542
	[SerializeField]
	[HideInInspector]
	private UIFont.SymbolStyle mSymbols = UIFont.SymbolStyle.Uncolored;

	// Token: 0x0400292F RID: 10543
	[SerializeField]
	[HideInInspector]
	private char mCarratChar = '|';

	// Token: 0x04002930 RID: 10544
	[SerializeField]
	[HideInInspector]
	private Color mHighlightTextColor = Color.cyan;

	// Token: 0x04002931 RID: 10545
	[HideInInspector]
	[SerializeField]
	private Color mHighlightColor = Color.black;

	// Token: 0x04002932 RID: 10546
	[HideInInspector]
	[SerializeField]
	private char mHighlightChar = '|';

	// Token: 0x04002933 RID: 10547
	[SerializeField]
	[HideInInspector]
	private float mHighlightCharSplit = 0.5f;

	// Token: 0x04002934 RID: 10548
	[HideInInspector]
	[SerializeField]
	private float mLineWidth;

	// Token: 0x04002935 RID: 10549
	[HideInInspector]
	[SerializeField]
	private bool mMultiline = true;

	// Token: 0x04002936 RID: 10550
	private Vector3? lastQueryPos;

	// Token: 0x04002937 RID: 10551
	private bool mShouldBeProcessed = true;

	// Token: 0x04002938 RID: 10552
	private string mProcessedText;

	// Token: 0x04002939 RID: 10553
	private UITextSelection mSelection;

	// Token: 0x0400293A RID: 10554
	private Vector3 mLastScale = Vector3.one;

	// Token: 0x0400293B RID: 10555
	private string mLastText = string.Empty;

	// Token: 0x0400293C RID: 10556
	private int mLastWidth;

	// Token: 0x0400293D RID: 10557
	private bool mLastEncoding = true;

	// Token: 0x0400293E RID: 10558
	private int mLastCount;

	// Token: 0x0400293F RID: 10559
	private bool mLastPass;

	// Token: 0x04002940 RID: 10560
	private bool mLastShow;

	// Token: 0x04002941 RID: 10561
	private bool mInvisibleHack;

	// Token: 0x04002942 RID: 10562
	private bool mLastInvisibleHack;

	// Token: 0x04002943 RID: 10563
	private UILabel.Effect mLastEffect;

	// Token: 0x04002944 RID: 10564
	private Color mLastColor = Color.black;

	// Token: 0x04002945 RID: 10565
	private Vector3 mSize = Vector3.zero;

	// Token: 0x04002946 RID: 10566
	private List<UITextMarkup> _markups;

	// Token: 0x020007F5 RID: 2037
	public enum Effect
	{
		// Token: 0x04002948 RID: 10568
		None,
		// Token: 0x04002949 RID: 10569
		Shadow,
		// Token: 0x0400294A RID: 10570
		Outline
	}
}
