using System;
using System.Collections.Generic;
using NGUI.Meshing;
using UnityEngine;

// Token: 0x020008E6 RID: 2278
[AddComponentMenu("NGUI/UI/Label")]
[ExecuteInEditMode]
public class UILabel : global::UIWidget
{
	// Token: 0x06004D84 RID: 19844 RVA: 0x00134564 File Offset: 0x00132764
	public UILabel() : base(global::UIWidget.WidgetFlags.CustomRelativeSize | global::UIWidget.WidgetFlags.CustomMaterialGet)
	{
	}

	// Token: 0x17000EC1 RID: 3777
	// (get) Token: 0x06004D85 RID: 19845 RVA: 0x00134610 File Offset: 0x00132810
	private List<global::UITextMarkup> markups
	{
		get
		{
			List<global::UITextMarkup> result;
			if ((result = this._markups) == null)
			{
				result = (this._markups = new List<global::UITextMarkup>());
			}
			return result;
		}
	}

	// Token: 0x17000EC2 RID: 3778
	// (get) Token: 0x06004D86 RID: 19846 RVA: 0x00134638 File Offset: 0x00132838
	// (set) Token: 0x06004D87 RID: 19847 RVA: 0x00134640 File Offset: 0x00132840
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

	// Token: 0x06004D88 RID: 19848 RVA: 0x0013465C File Offset: 0x0013285C
	private bool PendingChanges()
	{
		return this.mShouldBeProcessed || this.mLastText != this.text || this.mInvisibleHack != this.mLastInvisibleHack || this.mLastWidth != this.mMaxLineWidth || this.mLastEncoding != this.mEncoding || this.mLastCount != this.mMaxLineCount || this.mLastPass != this.mPassword || this.mLastShow != this.mShowLastChar || this.mLastEffect != this.mEffectStyle || this.mLastColor != this.mEffectColor;
	}

	// Token: 0x06004D89 RID: 19849 RVA: 0x00134718 File Offset: 0x00132918
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

	// Token: 0x06004D8A RID: 19850 RVA: 0x00134798 File Offset: 0x00132998
	private void ForceChanges()
	{
		base.ChangedAuto();
		this.mShouldBeProcessed = true;
	}

	// Token: 0x17000EC3 RID: 3779
	// (get) Token: 0x06004D8B RID: 19851 RVA: 0x001347A8 File Offset: 0x001329A8
	// (set) Token: 0x06004D8C RID: 19852 RVA: 0x001347B0 File Offset: 0x001329B0
	public global::UIFont font
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
				base.baseMaterial = ((!(this.mFont != null)) ? null : ((global::UIMaterial)this.mFont.material));
				base.ChangedAuto();
				this.ForceChanges();
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000EC4 RID: 3780
	// (get) Token: 0x06004D8D RID: 19853 RVA: 0x00134814 File Offset: 0x00132A14
	// (set) Token: 0x06004D8E RID: 19854 RVA: 0x0013481C File Offset: 0x00132A1C
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

	// Token: 0x17000EC5 RID: 3781
	// (get) Token: 0x06004D8F RID: 19855 RVA: 0x00134850 File Offset: 0x00132A50
	// (set) Token: 0x06004D90 RID: 19856 RVA: 0x00134858 File Offset: 0x00132A58
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

	// Token: 0x17000EC6 RID: 3782
	// (get) Token: 0x06004D91 RID: 19857 RVA: 0x0013488C File Offset: 0x00132A8C
	// (set) Token: 0x06004D92 RID: 19858 RVA: 0x00134894 File Offset: 0x00132A94
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
				global::UIWidget.Pivot pivot = base.pivot;
				switch (pivot)
				{
				case global::UIWidget.Pivot.TopLeft:
				case global::UIWidget.Pivot.Left:
					break;
				default:
					if (pivot != global::UIWidget.Pivot.BottomLeft)
					{
						return;
					}
					break;
				}
				this.ForceChanges();
			}
		}
	}

	// Token: 0x17000EC7 RID: 3783
	// (get) Token: 0x06004D93 RID: 19859 RVA: 0x001348E8 File Offset: 0x00132AE8
	// (set) Token: 0x06004D94 RID: 19860 RVA: 0x001348F0 File Offset: 0x00132AF0
	public global::UIFont.SymbolStyle symbolStyle
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

	// Token: 0x17000EC8 RID: 3784
	// (get) Token: 0x06004D95 RID: 19861 RVA: 0x0013490C File Offset: 0x00132B0C
	// (set) Token: 0x06004D96 RID: 19862 RVA: 0x00134914 File Offset: 0x00132B14
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

	// Token: 0x17000EC9 RID: 3785
	// (get) Token: 0x06004D97 RID: 19863 RVA: 0x00134930 File Offset: 0x00132B30
	// (set) Token: 0x06004D98 RID: 19864 RVA: 0x00134940 File Offset: 0x00132B40
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

	// Token: 0x17000ECA RID: 3786
	// (get) Token: 0x06004D99 RID: 19865 RVA: 0x00134988 File Offset: 0x00132B88
	// (set) Token: 0x06004D9A RID: 19866 RVA: 0x00134990 File Offset: 0x00132B90
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

	// Token: 0x17000ECB RID: 3787
	// (get) Token: 0x06004D9B RID: 19867 RVA: 0x001349C0 File Offset: 0x00132BC0
	// (set) Token: 0x06004D9C RID: 19868 RVA: 0x001349C8 File Offset: 0x00132BC8
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

	// Token: 0x17000ECC RID: 3788
	// (get) Token: 0x06004D9D RID: 19869 RVA: 0x001349F4 File Offset: 0x00132BF4
	// (set) Token: 0x06004D9E RID: 19870 RVA: 0x001349FC File Offset: 0x00132BFC
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

	// Token: 0x17000ECD RID: 3789
	// (get) Token: 0x06004D9F RID: 19871 RVA: 0x00134A18 File Offset: 0x00132C18
	// (set) Token: 0x06004DA0 RID: 19872 RVA: 0x00134A20 File Offset: 0x00132C20
	public global::UILabel.Effect effectStyle
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

	// Token: 0x17000ECE RID: 3790
	// (get) Token: 0x06004DA1 RID: 19873 RVA: 0x00134A3C File Offset: 0x00132C3C
	// (set) Token: 0x06004DA2 RID: 19874 RVA: 0x00134A44 File Offset: 0x00132C44
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
				if (this.mEffectStyle != global::UILabel.Effect.None)
				{
					this.ForceChanges();
				}
			}
		}
	}

	// Token: 0x17000ECF RID: 3791
	// (get) Token: 0x06004DA3 RID: 19875 RVA: 0x00134A70 File Offset: 0x00132C70
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

	// Token: 0x17000ED0 RID: 3792
	// (get) Token: 0x06004DA4 RID: 19876 RVA: 0x00134AC8 File Offset: 0x00132CC8
	// (set) Token: 0x06004DA5 RID: 19877 RVA: 0x00134AD0 File Offset: 0x00132CD0
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

	// Token: 0x17000ED1 RID: 3793
	// (get) Token: 0x06004DA6 RID: 19878 RVA: 0x00134B34 File Offset: 0x00132D34
	// (set) Token: 0x06004DA7 RID: 19879 RVA: 0x00134B3C File Offset: 0x00132D3C
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

	// Token: 0x17000ED2 RID: 3794
	// (get) Token: 0x06004DA8 RID: 19880 RVA: 0x00134BAC File Offset: 0x00132DAC
	// (set) Token: 0x06004DA9 RID: 19881 RVA: 0x00134BB4 File Offset: 0x00132DB4
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

	// Token: 0x17000ED3 RID: 3795
	// (get) Token: 0x06004DAA RID: 19882 RVA: 0x00134C04 File Offset: 0x00132E04
	// (set) Token: 0x06004DAB RID: 19883 RVA: 0x00134C0C File Offset: 0x00132E0C
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

	// Token: 0x17000ED4 RID: 3796
	// (get) Token: 0x06004DAC RID: 19884 RVA: 0x00134C94 File Offset: 0x00132E94
	// (set) Token: 0x06004DAD RID: 19885 RVA: 0x00134C9C File Offset: 0x00132E9C
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

	// Token: 0x17000ED5 RID: 3797
	// (get) Token: 0x06004DAE RID: 19886 RVA: 0x00134CD0 File Offset: 0x00132ED0
	public bool hasSelection
	{
		get
		{
			return this.mSelection.hasSelection;
		}
	}

	// Token: 0x17000ED6 RID: 3798
	// (get) Token: 0x06004DAF RID: 19887 RVA: 0x00134CE0 File Offset: 0x00132EE0
	public bool shouldShowCarrat
	{
		get
		{
			return this.mSelection.showCarrat;
		}
	}

	// Token: 0x17000ED7 RID: 3799
	// (get) Token: 0x06004DB0 RID: 19888 RVA: 0x00134CF0 File Offset: 0x00132EF0
	public bool drawingCarrat
	{
		get
		{
			return this.mCarratChar != '\0' && this.mSelection.showCarrat;
		}
	}

	// Token: 0x17000ED8 RID: 3800
	// (get) Token: 0x06004DB1 RID: 19889 RVA: 0x00134D0C File Offset: 0x00132F0C
	public global::UITextPosition carratPosition
	{
		get
		{
			return this.mSelection.carratPos;
		}
	}

	// Token: 0x17000ED9 RID: 3801
	// (get) Token: 0x06004DB2 RID: 19890 RVA: 0x00134D1C File Offset: 0x00132F1C
	public global::UITextPosition selectPosition
	{
		get
		{
			return this.mSelection.selectPos;
		}
	}

	// Token: 0x17000EDA RID: 3802
	// (get) Token: 0x06004DB3 RID: 19891 RVA: 0x00134D2C File Offset: 0x00132F2C
	private bool highlightWouldBeVisibleIfOn
	{
		get
		{
			return (this.mHighlightChar != '\0' && this.mHighlightColor.a > 0f) || this.mHighlightTextColor != base.color;
		}
	}

	// Token: 0x17000EDB RID: 3803
	// (get) Token: 0x06004DB4 RID: 19892 RVA: 0x00134D70 File Offset: 0x00132F70
	private bool carratWouldBeVisibleIfOn
	{
		get
		{
			return this.mCarratChar != '\0';
		}
	}

	// Token: 0x17000EDC RID: 3804
	// (get) Token: 0x06004DB5 RID: 19893 RVA: 0x00134D80 File Offset: 0x00132F80
	// (set) Token: 0x06004DB6 RID: 19894 RVA: 0x00134D88 File Offset: 0x00132F88
	public global::UITextSelection selection
	{
		get
		{
			return this.mSelection;
		}
		set
		{
			global::UITextSelection.Change changesTo = this.mSelection.GetChangesTo(ref value);
			this.mSelection = value;
			switch (changesTo)
			{
			case global::UITextSelection.Change.NoneToCarrat:
			case global::UITextSelection.Change.CarratMove:
			case global::UITextSelection.Change.CarratToNone:
				if (this.carratWouldBeVisibleIfOn)
				{
					this.ForceChanges();
				}
				break;
			case global::UITextSelection.Change.CarratToSelection:
			case global::UITextSelection.Change.SelectionToCarrat:
				if (this.carratWouldBeVisibleIfOn || this.highlightWouldBeVisibleIfOn)
				{
					this.ForceChanges();
				}
				break;
			case global::UITextSelection.Change.SelectionAdjusted:
			case global::UITextSelection.Change.NoneToSelection:
			case global::UITextSelection.Change.SelectionToNone:
				if (this.highlightWouldBeVisibleIfOn)
				{
					this.ForceChanges();
				}
				break;
			}
		}
	}

	// Token: 0x17000EDD RID: 3805
	// (get) Token: 0x06004DB7 RID: 19895 RVA: 0x00134E30 File Offset: 0x00133030
	// (set) Token: 0x06004DB8 RID: 19896 RVA: 0x00134E80 File Offset: 0x00133080
	public new global::UIMaterial material
	{
		get
		{
			global::UIMaterial uimaterial = base.baseMaterial;
			if (uimaterial == null)
			{
				uimaterial = ((!(this.mFont != null)) ? null : ((global::UIMaterial)this.mFont.material));
				this.material = uimaterial;
			}
			return uimaterial;
		}
		set
		{
			base.material = value;
		}
	}

	// Token: 0x17000EDE RID: 3806
	// (get) Token: 0x06004DB9 RID: 19897 RVA: 0x00134E8C File Offset: 0x0013308C
	protected override global::UIMaterial customMaterial
	{
		get
		{
			return this.material;
		}
	}

	// Token: 0x17000EDF RID: 3807
	// (get) Token: 0x06004DBA RID: 19898 RVA: 0x00134E94 File Offset: 0x00133094
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

	// Token: 0x06004DBB RID: 19899 RVA: 0x00134EDC File Offset: 0x001330DC
	protected override void GetCustomVector2s(int start, int end, global::UIWidget.WidgetFlags[] flags, Vector2[] v)
	{
		for (int i = 0; i < end; i++)
		{
			if (flags[i] == global::UIWidget.WidgetFlags.CustomRelativeSize)
			{
				v[i] = this.relativeSize;
			}
			else
			{
				base.GetCustomVector2s(i, i + 1, flags, v);
			}
		}
	}

	// Token: 0x06004DBC RID: 19900 RVA: 0x00134F2C File Offset: 0x0013312C
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

	// Token: 0x06004DBD RID: 19901 RVA: 0x00134F80 File Offset: 0x00133180
	public override void MarkAsChanged()
	{
		this.ForceChanges();
		base.MarkAsChanged();
	}

	// Token: 0x06004DBE RID: 19902 RVA: 0x00134F90 File Offset: 0x00133190
	private void ProcessText()
	{
		base.ChangedAuto();
		this.mLastText = this.mText;
		this.markups.Clear();
		string a = this.mProcessedText;
		this.mProcessedText = this.mText;
		if (this.mPassword)
		{
			this.mProcessedText = this.mFont.WrapText(this.markups, this.mProcessedText, 100000f, 1, false, global::UIFont.SymbolStyle.None);
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
			this.mSelection = default(global::UITextSelection);
		}
		this.ApplyChanges();
	}

	// Token: 0x06004DBF RID: 19903 RVA: 0x00135238 File Offset: 0x00133438
	public int CalculateTextPosition(Space space, Vector3[] points, global::UITextPosition[] positions)
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

	// Token: 0x06004DC0 RID: 19904 RVA: 0x001352B8 File Offset: 0x001334B8
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
			if (num2 % 2 == 1 && (base.pivot == global::UIWidget.Pivot.Top || base.pivot == global::UIWidget.Pivot.Center || base.pivot == global::UIWidget.Pivot.Bottom))
			{
				localPosition.x += 0.5f;
			}
			if (num3 % 2 == 1 && (base.pivot == global::UIWidget.Pivot.Left || base.pivot == global::UIWidget.Pivot.Center || base.pivot == global::UIWidget.Pivot.Right))
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

	// Token: 0x06004DC1 RID: 19905 RVA: 0x0013549C File Offset: 0x0013369C
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
				if (num2 % 2 == 1 && (base.pivot == global::UIWidget.Pivot.Top || base.pivot == global::UIWidget.Pivot.Center || base.pivot == global::UIWidget.Pivot.Bottom))
				{
					localPosition.x += 0.5f;
				}
				if (num3 % 2 == 1 && (base.pivot == global::UIWidget.Pivot.Left || base.pivot == global::UIWidget.Pivot.Center || base.pivot == global::UIWidget.Pivot.Right))
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

	// Token: 0x06004DC2 RID: 19906 RVA: 0x00135684 File Offset: 0x00133884
	public override void OnFill(NGUI.Meshing.MeshBuffer m)
	{
		if (this.mFont == null)
		{
			return;
		}
		Color normalColor = (!this.mInvisibleHack) ? base.color : Color.clear;
		this.MakePositionPerfect();
		global::UIWidget.Pivot pivot = base.pivot;
		int vSize = m.vSize;
		if (pivot == global::UIWidget.Pivot.Left || pivot == global::UIWidget.Pivot.TopLeft || pivot == global::UIWidget.Pivot.BottomLeft)
		{
			if (this.mOverflowRight)
			{
				this.mFont.Print(this.processedText, normalColor, m, this.mEncoding, this.mSymbols, global::UIFont.Alignment.LeftOverflowRight, Mathf.RoundToInt(this.relativeSize.x * (float)this.mFont.size), ref this.mSelection, this.mCarratChar, this.mHighlightTextColor, this.mHighlightColor, this.mHighlightChar, this.mHighlightCharSplit);
			}
			else
			{
				this.mFont.Print(this.processedText, normalColor, m, this.mEncoding, this.mSymbols, global::UIFont.Alignment.Left, 0, ref this.mSelection, this.mCarratChar, this.mHighlightTextColor, this.mHighlightColor, this.mHighlightChar, this.mHighlightCharSplit);
			}
		}
		else if (pivot == global::UIWidget.Pivot.Right || pivot == global::UIWidget.Pivot.TopRight || pivot == global::UIWidget.Pivot.BottomRight)
		{
			this.mFont.Print(this.processedText, normalColor, m, this.mEncoding, this.mSymbols, global::UIFont.Alignment.Right, Mathf.RoundToInt(this.relativeSize.x * (float)this.mFont.size), ref this.mSelection, this.mCarratChar, this.mHighlightTextColor, this.mHighlightColor, this.mHighlightChar, this.mHighlightCharSplit);
		}
		else
		{
			this.mFont.Print(this.processedText, normalColor, m, this.mEncoding, this.mSymbols, global::UIFont.Alignment.Center, Mathf.RoundToInt(this.relativeSize.x * (float)this.mFont.size), ref this.mSelection, this.mCarratChar, this.mHighlightTextColor, this.mHighlightColor, this.mHighlightChar, this.mHighlightCharSplit);
		}
		m.ApplyEffect(base.cachedTransform, vSize, this.effectStyle, this.effectColor, (float)this.mFont.size);
	}

	// Token: 0x06004DC3 RID: 19907 RVA: 0x001358B0 File Offset: 0x00133AB0
	public void UnionProcessedChanges(string newProcessedText)
	{
		this.text = newProcessedText;
	}

	// Token: 0x06004DC4 RID: 19908 RVA: 0x001358BC File Offset: 0x00133ABC
	private void ConvertProcessedTextPosition(ref global::UITextPosition p, ref int markupCount)
	{
		if (markupCount == -1)
		{
			markupCount = this.markups.Count;
		}
		if (markupCount == 0)
		{
			return;
		}
		global::UITextMarkup uitextMarkup = this.markups[0];
		int num = 0;
		while (p.position <= uitextMarkup.index)
		{
			switch (uitextMarkup.mod)
			{
			case global::UITextMod.End:
				p.deformed = (short)(this.mText.Length - uitextMarkup.index);
				break;
			case global::UITextMod.Removed:
				p.deformed += 1;
				if (++num < markupCount)
				{
					uitextMarkup = this.markups[num];
					continue;
				}
				break;
			case global::UITextMod.Replaced:
				if (++num < markupCount)
				{
					uitextMarkup = this.markups[num];
					continue;
				}
				break;
			case global::UITextMod.Added:
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

	// Token: 0x06004DC5 RID: 19909 RVA: 0x00135A00 File Offset: 0x00133C00
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
			case global::UITextMod.End:
				num2 = this.mProcessedText.Length - 1;
				return;
			case global::UITextMod.Removed:
				num--;
				num2--;
				break;
			case global::UITextMod.Added:
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
			case global::UITextMod.End:
				num2 = this.mProcessedText.Length - 1;
				return;
			case global::UITextMod.Removed:
				num2--;
				break;
			case global::UITextMod.Added:
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

	// Token: 0x06004DC6 RID: 19910 RVA: 0x00135B60 File Offset: 0x00133D60
	private static void CountLinesGetColumn(string text, int inPos, out int pos, out int lines, out int column, out global::UITextRegion region)
	{
		if (inPos < 0)
		{
			region = global::UITextRegion.Before;
			pos = 0;
			lines = 0;
			column = 0;
		}
		else if (inPos == 0)
		{
			pos = 0;
			lines = 0;
			column = 0;
			region = global::UITextRegion.Pre;
		}
		else
		{
			if (inPos > text.Length)
			{
				region = global::UITextRegion.End;
				pos = text.Length;
			}
			else if (inPos == text.Length)
			{
				region = global::UITextRegion.Past;
				pos = inPos;
			}
			else
			{
				region = global::UITextRegion.Inside;
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

	// Token: 0x06004DC7 RID: 19911 RVA: 0x00135C40 File Offset: 0x00133E40
	public global::UITextPosition ConvertUnprocessedPosition(int position)
	{
		string processedText = this.processedText;
		int count = this.markups.Count;
		int num = position;
		if (count > 0)
		{
			int num2 = 0;
			global::UITextMarkup uitextMarkup = this.markups[num2];
			while (uitextMarkup.index <= position)
			{
				switch (uitextMarkup.mod)
				{
				case global::UITextMod.End:
					position -= num - uitextMarkup.index;
					num2 = count;
					break;
				case global::UITextMod.Removed:
					position--;
					break;
				case global::UITextMod.Added:
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
		global::UITextPosition result;
		global::UILabel.CountLinesGetColumn(processedText, position, out result.position, out result.line, out result.column, out result.region);
		result.uniformRegion = result.region;
		result.deformed = (short)(num - result.position);
		return result;
	}

	// Token: 0x06004DC8 RID: 19912 RVA: 0x00135D40 File Offset: 0x00133F40
	public global::UITextSelection ConvertUnprocessedSelection(int carratPos, int selectPos)
	{
		global::UITextSelection result;
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

	// Token: 0x04002B72 RID: 11122
	[SerializeField]
	[HideInInspector]
	private global::UIFont mFont;

	// Token: 0x04002B73 RID: 11123
	[SerializeField]
	[HideInInspector]
	private string mText = string.Empty;

	// Token: 0x04002B74 RID: 11124
	[SerializeField]
	[HideInInspector]
	private int mMaxLineWidth;

	// Token: 0x04002B75 RID: 11125
	[SerializeField]
	[HideInInspector]
	private bool mEncoding = true;

	// Token: 0x04002B76 RID: 11126
	[SerializeField]
	[HideInInspector]
	private int mMaxLineCount;

	// Token: 0x04002B77 RID: 11127
	[HideInInspector]
	[SerializeField]
	private bool mPassword;

	// Token: 0x04002B78 RID: 11128
	[SerializeField]
	[HideInInspector]
	private bool mShowLastChar;

	// Token: 0x04002B79 RID: 11129
	[HideInInspector]
	[SerializeField]
	private bool mOverflowRight;

	// Token: 0x04002B7A RID: 11130
	[SerializeField]
	[HideInInspector]
	private global::UILabel.Effect mEffectStyle;

	// Token: 0x04002B7B RID: 11131
	[HideInInspector]
	[SerializeField]
	private Color mEffectColor = Color.black;

	// Token: 0x04002B7C RID: 11132
	[SerializeField]
	[HideInInspector]
	private global::UIFont.SymbolStyle mSymbols = global::UIFont.SymbolStyle.Uncolored;

	// Token: 0x04002B7D RID: 11133
	[SerializeField]
	[HideInInspector]
	private char mCarratChar = '|';

	// Token: 0x04002B7E RID: 11134
	[HideInInspector]
	[SerializeField]
	private Color mHighlightTextColor = Color.cyan;

	// Token: 0x04002B7F RID: 11135
	[SerializeField]
	[HideInInspector]
	private Color mHighlightColor = Color.black;

	// Token: 0x04002B80 RID: 11136
	[HideInInspector]
	[SerializeField]
	private char mHighlightChar = '|';

	// Token: 0x04002B81 RID: 11137
	[HideInInspector]
	[SerializeField]
	private float mHighlightCharSplit = 0.5f;

	// Token: 0x04002B82 RID: 11138
	[HideInInspector]
	[SerializeField]
	private float mLineWidth;

	// Token: 0x04002B83 RID: 11139
	[HideInInspector]
	[SerializeField]
	private bool mMultiline = true;

	// Token: 0x04002B84 RID: 11140
	private Vector3? lastQueryPos;

	// Token: 0x04002B85 RID: 11141
	private bool mShouldBeProcessed = true;

	// Token: 0x04002B86 RID: 11142
	private string mProcessedText;

	// Token: 0x04002B87 RID: 11143
	private global::UITextSelection mSelection;

	// Token: 0x04002B88 RID: 11144
	private Vector3 mLastScale = Vector3.one;

	// Token: 0x04002B89 RID: 11145
	private string mLastText = string.Empty;

	// Token: 0x04002B8A RID: 11146
	private int mLastWidth;

	// Token: 0x04002B8B RID: 11147
	private bool mLastEncoding = true;

	// Token: 0x04002B8C RID: 11148
	private int mLastCount;

	// Token: 0x04002B8D RID: 11149
	private bool mLastPass;

	// Token: 0x04002B8E RID: 11150
	private bool mLastShow;

	// Token: 0x04002B8F RID: 11151
	private bool mInvisibleHack;

	// Token: 0x04002B90 RID: 11152
	private bool mLastInvisibleHack;

	// Token: 0x04002B91 RID: 11153
	private global::UILabel.Effect mLastEffect;

	// Token: 0x04002B92 RID: 11154
	private Color mLastColor = Color.black;

	// Token: 0x04002B93 RID: 11155
	private Vector3 mSize = Vector3.zero;

	// Token: 0x04002B94 RID: 11156
	private List<global::UITextMarkup> _markups;

	// Token: 0x020008E7 RID: 2279
	public enum Effect
	{
		// Token: 0x04002B96 RID: 11158
		None,
		// Token: 0x04002B97 RID: 11159
		Shadow,
		// Token: 0x04002B98 RID: 11160
		Outline
	}
}
