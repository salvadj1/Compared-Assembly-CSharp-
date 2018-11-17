using System;
using System.Collections.Generic;
using NGUIHack;
using UnityEngine;

// Token: 0x020008E2 RID: 2274
[AddComponentMenu("NGUI/UI/Input (Basic)")]
public class UIInput : MonoBehaviour
{
	// Token: 0x17000EBE RID: 3774
	// (get) Token: 0x06004D6A RID: 19818 RVA: 0x00133F40 File Offset: 0x00132140
	public string inputText
	{
		get
		{
			return this.mText;
		}
	}

	// Token: 0x17000EBF RID: 3775
	// (get) Token: 0x06004D6B RID: 19819 RVA: 0x00133F48 File Offset: 0x00132148
	// (set) Token: 0x06004D6C RID: 19820 RVA: 0x00133F50 File Offset: 0x00132150
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

	// Token: 0x17000EC0 RID: 3776
	// (get) Token: 0x06004D6D RID: 19821 RVA: 0x00134018 File Offset: 0x00132218
	// (set) Token: 0x06004D6E RID: 19822 RVA: 0x0013402C File Offset: 0x0013222C
	public bool selected
	{
		get
		{
			return global::UICamera.selectedObject == base.gameObject;
		}
		set
		{
			if (!value && global::UICamera.selectedObject == base.gameObject)
			{
				global::UICamera.selectedObject = null;
			}
			else if (value)
			{
				global::UICamera.selectedObject = base.gameObject;
			}
		}
	}

	// Token: 0x06004D6F RID: 19823 RVA: 0x00134070 File Offset: 0x00132270
	protected void Init()
	{
		if (this.label == null)
		{
			this.label = base.GetComponentInChildren<global::UILabel>();
		}
		if (this.label != null)
		{
			this.mDefaultText = this.label.text;
			this.mDefaultColor = this.label.color;
			this.label.supportEncoding = false;
		}
	}

	// Token: 0x06004D70 RID: 19824 RVA: 0x001340DC File Offset: 0x001322DC
	private void Awake()
	{
		this.markups = new List<global::UITextMarkup>();
		this.Init();
	}

	// Token: 0x06004D71 RID: 19825 RVA: 0x001340F0 File Offset: 0x001322F0
	private void OnEnable()
	{
		if (global::UICamera.IsHighlighted(base.gameObject))
		{
			this.OnSelect(true);
		}
	}

	// Token: 0x06004D72 RID: 19826 RVA: 0x0013410C File Offset: 0x0013230C
	private void OnDisable()
	{
		if (global::UICamera.IsHighlighted(base.gameObject))
		{
			this.OnSelect(false);
		}
	}

	// Token: 0x06004D73 RID: 19827 RVA: 0x00134128 File Offset: 0x00132328
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

	// Token: 0x06004D74 RID: 19828 RVA: 0x00134284 File Offset: 0x00132484
	internal void OnEvent(global::UICamera camera, NGUIHack.Event @event, EventType type)
	{
		switch (type)
		{
		case 0:
			if (@event.button == 0)
			{
				global::UIUnityEvents.TextClickDown(camera, this, @event, this.label);
			}
			break;
		case 1:
			if (@event.button == 0)
			{
				global::UIUnityEvents.TextClickUp(camera, this, @event, this.label);
			}
			else
			{
				Debug.Log("Wee");
			}
			break;
		case 3:
			if (@event.button == 0)
			{
				global::UIUnityEvents.TextDrag(camera, this, @event, this.label);
			}
			break;
		case 4:
			global::UIUnityEvents.TextKeyDown(camera, this, @event, this.label);
			break;
		case 5:
			global::UIUnityEvents.TextKeyUp(camera, this, @event, this.label);
			break;
		}
	}

	// Token: 0x06004D75 RID: 19829 RVA: 0x00134348 File Offset: 0x00132548
	public bool RequestKeyboardFocus()
	{
		return global::UIUnityEvents.RequestKeyboardFocus(this);
	}

	// Token: 0x06004D76 RID: 19830 RVA: 0x00134350 File Offset: 0x00132550
	private void Update()
	{
	}

	// Token: 0x06004D77 RID: 19831 RVA: 0x00134354 File Offset: 0x00132554
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

	// Token: 0x06004D78 RID: 19832 RVA: 0x001343A4 File Offset: 0x001325A4
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
			text = this.label.font.WrapText(this.markups, text, (float)this.label.lineWidth / this.label.cachedTransform.localScale.x, 0, false, global::UIFont.SymbolStyle.None);
			this.markups.SortMarkup();
			this.label.text = text;
			this.label.showLastPasswordChar = this.selected;
		}
	}

	// Token: 0x06004D79 RID: 19833 RVA: 0x001344A0 File Offset: 0x001326A0
	internal void CheckPositioning(int carratPos, int selectPos)
	{
		this.label.selection = this.label.ConvertUnprocessedSelection(carratPos, selectPos);
	}

	// Token: 0x06004D7A RID: 19834 RVA: 0x001344BC File Offset: 0x001326BC
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

	// Token: 0x06004D7B RID: 19835 RVA: 0x001344EC File Offset: 0x001326EC
	internal void LoseFocus()
	{
		global::UIUnityEvents.TextLostFocus(this);
	}

	// Token: 0x06004D7C RID: 19836 RVA: 0x001344F4 File Offset: 0x001326F4
	internal void GainFocus()
	{
		global::UIUnityEvents.TextGainFocus(this);
	}

	// Token: 0x04002B58 RID: 11096
	public static global::UIInput current;

	// Token: 0x04002B59 RID: 11097
	public global::UILabel label;

	// Token: 0x04002B5A RID: 11098
	public int maxChars;

	// Token: 0x04002B5B RID: 11099
	public bool inputMultiline;

	// Token: 0x04002B5C RID: 11100
	public global::UIInput.Validator validator;

	// Token: 0x04002B5D RID: 11101
	public global::UIInput.KeyboardType type;

	// Token: 0x04002B5E RID: 11102
	public bool isPassword;

	// Token: 0x04002B5F RID: 11103
	public Color activeColor = Color.white;

	// Token: 0x04002B60 RID: 11104
	public GameObject eventReceiver;

	// Token: 0x04002B61 RID: 11105
	public string functionName = "OnSubmit";

	// Token: 0x04002B62 RID: 11106
	public bool trippleClickSelect = true;

	// Token: 0x04002B63 RID: 11107
	private List<global::UITextMarkup> markups;

	// Token: 0x04002B64 RID: 11108
	private string mText = string.Empty;

	// Token: 0x04002B65 RID: 11109
	private string mDefaultText = string.Empty;

	// Token: 0x04002B66 RID: 11110
	private Color mDefaultColor = Color.white;

	// Token: 0x04002B67 RID: 11111
	private string mLastIME = string.Empty;

	// Token: 0x020008E3 RID: 2275
	public enum KeyboardType
	{
		// Token: 0x04002B69 RID: 11113
		Default,
		// Token: 0x04002B6A RID: 11114
		ASCIICapable,
		// Token: 0x04002B6B RID: 11115
		NumbersAndPunctuation,
		// Token: 0x04002B6C RID: 11116
		URL,
		// Token: 0x04002B6D RID: 11117
		NumberPad,
		// Token: 0x04002B6E RID: 11118
		PhonePad,
		// Token: 0x04002B6F RID: 11119
		NamePhonePad,
		// Token: 0x04002B70 RID: 11120
		EmailAddress
	}

	// Token: 0x020008E4 RID: 2276
	// (Invoke) Token: 0x06004D7E RID: 19838
	public delegate char Validator(string currentText, char nextChar);
}
