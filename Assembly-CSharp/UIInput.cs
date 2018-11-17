using System;
using System.Collections.Generic;
using NGUIHack;
using UnityEngine;

// Token: 0x020007F1 RID: 2033
[AddComponentMenu("NGUI/UI/Input (Basic)")]
public class UIInput : MonoBehaviour
{
	// Token: 0x17000E24 RID: 3620
	// (get) Token: 0x060048BF RID: 18623 RVA: 0x00129FDC File Offset: 0x001281DC
	public string inputText
	{
		get
		{
			return this.mText;
		}
	}

	// Token: 0x17000E25 RID: 3621
	// (get) Token: 0x060048C0 RID: 18624 RVA: 0x00129FE4 File Offset: 0x001281E4
	// (set) Token: 0x060048C1 RID: 18625 RVA: 0x00129FEC File Offset: 0x001281EC
	public string text
	{
		get
		{
			return this.mText;
		}
		set
		{
			value = (value ?? string.Empty);
			bool flag = (this.mText ?? string.Empty) != value;
			this.mText = value;
			if (this.label != null)
			{
				if (value != string.Empty)
				{
					value = this.mDefaultText;
				}
				this.label.supportEncoding = false;
				this.label.showLastPasswordChar = this.selected;
				this.label.color = ((!this.selected && !(value != this.mDefaultText)) ? this.mDefaultColor : this.activeColor);
				if (flag)
				{
					this.UpdateLabel();
				}
			}
		}
	}

	// Token: 0x17000E26 RID: 3622
	// (get) Token: 0x060048C2 RID: 18626 RVA: 0x0012A0B4 File Offset: 0x001282B4
	// (set) Token: 0x060048C3 RID: 18627 RVA: 0x0012A0C8 File Offset: 0x001282C8
	public bool selected
	{
		get
		{
			return UICamera.selectedObject == base.gameObject;
		}
		set
		{
			if (!value && UICamera.selectedObject == base.gameObject)
			{
				UICamera.selectedObject = null;
			}
			else if (value)
			{
				UICamera.selectedObject = base.gameObject;
			}
		}
	}

	// Token: 0x060048C4 RID: 18628 RVA: 0x0012A10C File Offset: 0x0012830C
	protected void Init()
	{
		if (this.label == null)
		{
			this.label = base.GetComponentInChildren<UILabel>();
		}
		if (this.label != null)
		{
			this.mDefaultText = this.label.text;
			this.mDefaultColor = this.label.color;
			this.label.supportEncoding = false;
		}
	}

	// Token: 0x060048C5 RID: 18629 RVA: 0x0012A178 File Offset: 0x00128378
	private void Awake()
	{
		this.markups = new List<UITextMarkup>();
		this.Init();
	}

	// Token: 0x060048C6 RID: 18630 RVA: 0x0012A18C File Offset: 0x0012838C
	private void OnEnable()
	{
		if (UICamera.IsHighlighted(base.gameObject))
		{
			this.OnSelect(true);
		}
	}

	// Token: 0x060048C7 RID: 18631 RVA: 0x0012A1A8 File Offset: 0x001283A8
	private void OnDisable()
	{
		if (UICamera.IsHighlighted(base.gameObject))
		{
			this.OnSelect(false);
		}
	}

	// Token: 0x060048C8 RID: 18632 RVA: 0x0012A1C4 File Offset: 0x001283C4
	private void OnSelect(bool isSelected)
	{
		if (this.label != null && base.enabled && base.gameObject.activeInHierarchy)
		{
			if (isSelected)
			{
				this.mText = ((!(this.label.text == this.mDefaultText)) ? this.label.text : string.Empty);
				this.label.color = this.activeColor;
				if (this.isPassword)
				{
					this.label.password = true;
				}
				Transform cachedTransform = this.label.cachedTransform;
				Vector3 vector = this.label.pivotOffset;
				vector.y += this.label.relativeSize.y;
				vector = cachedTransform.TransformPoint(vector);
				this.UpdateLabel();
			}
			else
			{
				if (string.IsNullOrEmpty(this.mText))
				{
					this.label.text = this.mDefaultText;
					this.label.color = this.mDefaultColor;
					if (this.isPassword)
					{
						this.label.password = false;
					}
				}
				else
				{
					this.label.text = this.mText;
				}
				this.label.showLastPasswordChar = false;
			}
		}
	}

	// Token: 0x060048C9 RID: 18633 RVA: 0x0012A320 File Offset: 0x00128520
	internal void OnEvent(UICamera camera, Event @event, EventType type)
	{
		switch (type)
		{
		case 0:
			if (@event.button == 0)
			{
				UIUnityEvents.TextClickDown(camera, this, @event, this.label);
			}
			break;
		case 1:
			if (@event.button == 0)
			{
				UIUnityEvents.TextClickUp(camera, this, @event, this.label);
			}
			else
			{
				Debug.Log("Wee");
			}
			break;
		case 3:
			if (@event.button == 0)
			{
				UIUnityEvents.TextDrag(camera, this, @event, this.label);
			}
			break;
		case 4:
			UIUnityEvents.TextKeyDown(camera, this, @event, this.label);
			break;
		case 5:
			UIUnityEvents.TextKeyUp(camera, this, @event, this.label);
			break;
		}
	}

	// Token: 0x060048CA RID: 18634 RVA: 0x0012A3E4 File Offset: 0x001285E4
	public bool RequestKeyboardFocus()
	{
		return UIUnityEvents.RequestKeyboardFocus(this);
	}

	// Token: 0x060048CB RID: 18635 RVA: 0x0012A3EC File Offset: 0x001285EC
	private void Update()
	{
	}

	// Token: 0x060048CC RID: 18636 RVA: 0x0012A3F0 File Offset: 0x001285F0
	public bool SendSubmitMessage()
	{
		if (this.eventReceiver == null)
		{
			this.eventReceiver = base.gameObject;
		}
		string a = this.mText;
		this.eventReceiver.SendMessage(this.functionName, 1);
		return a != this.mText;
	}

	// Token: 0x060048CD RID: 18637 RVA: 0x0012A440 File Offset: 0x00128640
	internal void UpdateLabel()
	{
		if (this.maxChars > 0 && this.mText.Length > this.maxChars)
		{
			this.mText = this.mText.Substring(0, this.maxChars);
		}
		if (this.label.font != null)
		{
			string text;
			if (this.selected)
			{
				text = this.mText + this.mLastIME;
			}
			else
			{
				text = this.mText;
			}
			this.label.supportEncoding = false;
			text = this.label.font.WrapText(this.markups, text, (float)this.label.lineWidth / this.label.cachedTransform.localScale.x, 0, false, UIFont.SymbolStyle.None);
			this.markups.SortMarkup();
			this.label.text = text;
			this.label.showLastPasswordChar = this.selected;
		}
	}

	// Token: 0x060048CE RID: 18638 RVA: 0x0012A53C File Offset: 0x0012873C
	internal void CheckPositioning(int carratPos, int selectPos)
	{
		this.label.selection = this.label.ConvertUnprocessedSelection(carratPos, selectPos);
	}

	// Token: 0x060048CF RID: 18639 RVA: 0x0012A558 File Offset: 0x00128758
	internal string CheckChanges(string newText)
	{
		if (this.mText != newText)
		{
			this.mText = newText;
			this.UpdateLabel();
			return this.mText;
		}
		return this.mText;
	}

	// Token: 0x060048D0 RID: 18640 RVA: 0x0012A588 File Offset: 0x00128788
	internal void LoseFocus()
	{
		UIUnityEvents.TextLostFocus(this);
	}

	// Token: 0x060048D1 RID: 18641 RVA: 0x0012A590 File Offset: 0x00128790
	internal void GainFocus()
	{
		UIUnityEvents.TextGainFocus(this);
	}

	// Token: 0x0400290A RID: 10506
	public static UIInput current;

	// Token: 0x0400290B RID: 10507
	public UILabel label;

	// Token: 0x0400290C RID: 10508
	public int maxChars;

	// Token: 0x0400290D RID: 10509
	public bool inputMultiline;

	// Token: 0x0400290E RID: 10510
	public UIInput.Validator validator;

	// Token: 0x0400290F RID: 10511
	public UIInput.KeyboardType type;

	// Token: 0x04002910 RID: 10512
	public bool isPassword;

	// Token: 0x04002911 RID: 10513
	public Color activeColor = Color.white;

	// Token: 0x04002912 RID: 10514
	public GameObject eventReceiver;

	// Token: 0x04002913 RID: 10515
	public string functionName = "OnSubmit";

	// Token: 0x04002914 RID: 10516
	public bool trippleClickSelect = true;

	// Token: 0x04002915 RID: 10517
	private List<UITextMarkup> markups;

	// Token: 0x04002916 RID: 10518
	private string mText = string.Empty;

	// Token: 0x04002917 RID: 10519
	private string mDefaultText = string.Empty;

	// Token: 0x04002918 RID: 10520
	private Color mDefaultColor = Color.white;

	// Token: 0x04002919 RID: 10521
	private string mLastIME = string.Empty;

	// Token: 0x020007F2 RID: 2034
	public enum KeyboardType
	{
		// Token: 0x0400291B RID: 10523
		Default,
		// Token: 0x0400291C RID: 10524
		ASCIICapable,
		// Token: 0x0400291D RID: 10525
		NumbersAndPunctuation,
		// Token: 0x0400291E RID: 10526
		URL,
		// Token: 0x0400291F RID: 10527
		NumberPad,
		// Token: 0x04002920 RID: 10528
		PhonePad,
		// Token: 0x04002921 RID: 10529
		NamePhonePad,
		// Token: 0x04002922 RID: 10530
		EmailAddress
	}

	// Token: 0x020008EE RID: 2286
	// (Invoke) Token: 0x06004D90 RID: 19856
	public delegate char Validator(string currentText, char nextChar);
}
