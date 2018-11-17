using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

// Token: 0x020006FE RID: 1790
[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider))]
[AddComponentMenu("Daikon Forge/User Interface/Textbox")]
[Serializable]
public class dfTextbox : dfInteractiveBase, IDFMultiRender
{
	// Token: 0x14000055 RID: 85
	// (add) Token: 0x060040CE RID: 16590 RVA: 0x000F900C File Offset: 0x000F720C
	// (remove) Token: 0x060040CF RID: 16591 RVA: 0x000F9028 File Offset: 0x000F7228
	public event PropertyChangedEventHandler<bool> ReadOnlyChanged;

	// Token: 0x14000056 RID: 86
	// (add) Token: 0x060040D0 RID: 16592 RVA: 0x000F9044 File Offset: 0x000F7244
	// (remove) Token: 0x060040D1 RID: 16593 RVA: 0x000F9060 File Offset: 0x000F7260
	public event PropertyChangedEventHandler<string> PasswordCharacterChanged;

	// Token: 0x14000057 RID: 87
	// (add) Token: 0x060040D2 RID: 16594 RVA: 0x000F907C File Offset: 0x000F727C
	// (remove) Token: 0x060040D3 RID: 16595 RVA: 0x000F9098 File Offset: 0x000F7298
	public event PropertyChangedEventHandler<string> TextChanged;

	// Token: 0x14000058 RID: 88
	// (add) Token: 0x060040D4 RID: 16596 RVA: 0x000F90B4 File Offset: 0x000F72B4
	// (remove) Token: 0x060040D5 RID: 16597 RVA: 0x000F90D0 File Offset: 0x000F72D0
	public event PropertyChangedEventHandler<string> TextSubmitted;

	// Token: 0x14000059 RID: 89
	// (add) Token: 0x060040D6 RID: 16598 RVA: 0x000F90EC File Offset: 0x000F72EC
	// (remove) Token: 0x060040D7 RID: 16599 RVA: 0x000F9108 File Offset: 0x000F7308
	public event PropertyChangedEventHandler<string> TextCancelled;

	// Token: 0x17000CBD RID: 3261
	// (get) Token: 0x060040D8 RID: 16600 RVA: 0x000F9124 File Offset: 0x000F7324
	// (set) Token: 0x060040D9 RID: 16601 RVA: 0x000F9168 File Offset: 0x000F7368
	public dfFontBase Font
	{
		get
		{
			if (this.font == null)
			{
				dfGUIManager manager = base.GetManager();
				if (manager != null)
				{
					this.font = manager.DefaultFont;
				}
			}
			return this.font;
		}
		set
		{
			if (value != this.font)
			{
				this.font = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CBE RID: 3262
	// (get) Token: 0x060040DA RID: 16602 RVA: 0x000F9188 File Offset: 0x000F7388
	// (set) Token: 0x060040DB RID: 16603 RVA: 0x000F9190 File Offset: 0x000F7390
	public int SelectionStart
	{
		get
		{
			return this.selectionStart;
		}
		set
		{
			if (value != this.selectionStart)
			{
				this.selectionStart = Mathf.Max(0, Mathf.Min(value, this.text.Length));
				this.selectionEnd = Mathf.Max(this.selectionEnd, this.selectionStart);
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CBF RID: 3263
	// (get) Token: 0x060040DC RID: 16604 RVA: 0x000F91E4 File Offset: 0x000F73E4
	// (set) Token: 0x060040DD RID: 16605 RVA: 0x000F91EC File Offset: 0x000F73EC
	public int SelectionEnd
	{
		get
		{
			return this.selectionEnd;
		}
		set
		{
			if (value != this.selectionEnd)
			{
				this.selectionEnd = Mathf.Max(0, Mathf.Min(value, this.text.Length));
				this.selectionStart = Mathf.Max(this.selectionStart, this.selectionEnd);
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CC0 RID: 3264
	// (get) Token: 0x060040DE RID: 16606 RVA: 0x000F9240 File Offset: 0x000F7440
	public int SelectionLength
	{
		get
		{
			return this.selectionEnd - this.selectionStart;
		}
	}

	// Token: 0x17000CC1 RID: 3265
	// (get) Token: 0x060040DF RID: 16607 RVA: 0x000F9250 File Offset: 0x000F7450
	public string SelectedText
	{
		get
		{
			if (this.selectionEnd == this.selectionStart)
			{
				return string.Empty;
			}
			return this.text.Substring(this.selectionStart, this.selectionEnd - this.selectionStart);
		}
	}

	// Token: 0x17000CC2 RID: 3266
	// (get) Token: 0x060040E0 RID: 16608 RVA: 0x000F9288 File Offset: 0x000F7488
	// (set) Token: 0x060040E1 RID: 16609 RVA: 0x000F9290 File Offset: 0x000F7490
	public bool SelectOnFocus
	{
		get
		{
			return this.selectOnFocus;
		}
		set
		{
			this.selectOnFocus = value;
		}
	}

	// Token: 0x17000CC3 RID: 3267
	// (get) Token: 0x060040E2 RID: 16610 RVA: 0x000F929C File Offset: 0x000F749C
	// (set) Token: 0x060040E3 RID: 16611 RVA: 0x000F92BC File Offset: 0x000F74BC
	public RectOffset Padding
	{
		get
		{
			if (this.padding == null)
			{
				this.padding = new RectOffset();
			}
			return this.padding;
		}
		set
		{
			value = value.ConstrainPadding();
			if (!object.Equals(value, this.padding))
			{
				this.padding = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CC4 RID: 3268
	// (get) Token: 0x060040E4 RID: 16612 RVA: 0x000F92F0 File Offset: 0x000F74F0
	// (set) Token: 0x060040E5 RID: 16613 RVA: 0x000F92F8 File Offset: 0x000F74F8
	public bool IsPasswordField
	{
		get
		{
			return this.displayAsPassword;
		}
		set
		{
			if (value != this.displayAsPassword)
			{
				this.displayAsPassword = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CC5 RID: 3269
	// (get) Token: 0x060040E6 RID: 16614 RVA: 0x000F9314 File Offset: 0x000F7514
	// (set) Token: 0x060040E7 RID: 16615 RVA: 0x000F931C File Offset: 0x000F751C
	public string PasswordCharacter
	{
		get
		{
			return this.passwordChar;
		}
		set
		{
			if (!string.IsNullOrEmpty(value))
			{
				this.passwordChar = value[0].ToString();
			}
			else
			{
				this.passwordChar = value;
			}
			this.OnPasswordCharacterChanged();
			this.Invalidate();
		}
	}

	// Token: 0x17000CC6 RID: 3270
	// (get) Token: 0x060040E8 RID: 16616 RVA: 0x000F9364 File Offset: 0x000F7564
	// (set) Token: 0x060040E9 RID: 16617 RVA: 0x000F936C File Offset: 0x000F756C
	public float CursorBlinkTime
	{
		get
		{
			return this.cursorBlinkTime;
		}
		set
		{
			this.cursorBlinkTime = value;
		}
	}

	// Token: 0x17000CC7 RID: 3271
	// (get) Token: 0x060040EA RID: 16618 RVA: 0x000F9378 File Offset: 0x000F7578
	// (set) Token: 0x060040EB RID: 16619 RVA: 0x000F9380 File Offset: 0x000F7580
	public int CursorWidth
	{
		get
		{
			return this.cursorWidth;
		}
		set
		{
			this.cursorWidth = value;
		}
	}

	// Token: 0x17000CC8 RID: 3272
	// (get) Token: 0x060040EC RID: 16620 RVA: 0x000F938C File Offset: 0x000F758C
	// (set) Token: 0x060040ED RID: 16621 RVA: 0x000F9394 File Offset: 0x000F7594
	public int CursorIndex
	{
		get
		{
			return this.cursorIndex;
		}
		set
		{
			value = Mathf.Max(0, value);
			value = Mathf.Min(0, this.text.Length - 1);
			this.cursorIndex = value;
		}
	}

	// Token: 0x17000CC9 RID: 3273
	// (get) Token: 0x060040EE RID: 16622 RVA: 0x000F93BC File Offset: 0x000F75BC
	// (set) Token: 0x060040EF RID: 16623 RVA: 0x000F93C4 File Offset: 0x000F75C4
	public bool ReadOnly
	{
		get
		{
			return this.readOnly;
		}
		set
		{
			if (value != this.readOnly)
			{
				this.readOnly = value;
				this.OnReadOnlyChanged();
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CCA RID: 3274
	// (get) Token: 0x060040F0 RID: 16624 RVA: 0x000F93E8 File Offset: 0x000F75E8
	// (set) Token: 0x060040F1 RID: 16625 RVA: 0x000F93F0 File Offset: 0x000F75F0
	public string Text
	{
		get
		{
			return this.text;
		}
		set
		{
			if (value.Length > this.MaxLength)
			{
				value = value.Substring(0, this.MaxLength);
			}
			value = value.Replace("\t", " ");
			if (value != this.text)
			{
				this.text = value;
				this.scrollIndex = (this.cursorIndex = 0);
				this.OnTextChanged();
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CCB RID: 3275
	// (get) Token: 0x060040F2 RID: 16626 RVA: 0x000F9464 File Offset: 0x000F7664
	// (set) Token: 0x060040F3 RID: 16627 RVA: 0x000F946C File Offset: 0x000F766C
	public Color32 TextColor
	{
		get
		{
			return this.textColor;
		}
		set
		{
			this.textColor = value;
			this.Invalidate();
		}
	}

	// Token: 0x17000CCC RID: 3276
	// (get) Token: 0x060040F4 RID: 16628 RVA: 0x000F947C File Offset: 0x000F767C
	// (set) Token: 0x060040F5 RID: 16629 RVA: 0x000F9484 File Offset: 0x000F7684
	public string SelectionSprite
	{
		get
		{
			return this.selectionSprite;
		}
		set
		{
			if (value != this.selectionSprite)
			{
				this.selectionSprite = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CCD RID: 3277
	// (get) Token: 0x060040F6 RID: 16630 RVA: 0x000F94A4 File Offset: 0x000F76A4
	// (set) Token: 0x060040F7 RID: 16631 RVA: 0x000F94AC File Offset: 0x000F76AC
	public Color32 SelectionBackgroundColor
	{
		get
		{
			return this.selectionBackground;
		}
		set
		{
			this.selectionBackground = value;
			this.Invalidate();
		}
	}

	// Token: 0x17000CCE RID: 3278
	// (get) Token: 0x060040F8 RID: 16632 RVA: 0x000F94BC File Offset: 0x000F76BC
	// (set) Token: 0x060040F9 RID: 16633 RVA: 0x000F94C4 File Offset: 0x000F76C4
	public float TextScale
	{
		get
		{
			return this.textScale;
		}
		set
		{
			value = Mathf.Max(0.1f, value);
			if (!Mathf.Approximately(this.textScale, value))
			{
				this.textScale = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CCF RID: 3279
	// (get) Token: 0x060040FA RID: 16634 RVA: 0x000F94F4 File Offset: 0x000F76F4
	// (set) Token: 0x060040FB RID: 16635 RVA: 0x000F94FC File Offset: 0x000F76FC
	public dfTextScaleMode TextScaleMode
	{
		get
		{
			return this.textScaleMode;
		}
		set
		{
			this.textScaleMode = value;
			this.Invalidate();
		}
	}

	// Token: 0x17000CD0 RID: 3280
	// (get) Token: 0x060040FC RID: 16636 RVA: 0x000F950C File Offset: 0x000F770C
	// (set) Token: 0x060040FD RID: 16637 RVA: 0x000F9514 File Offset: 0x000F7714
	public int MaxLength
	{
		get
		{
			return this.maxLength;
		}
		set
		{
			if (value != this.maxLength)
			{
				this.maxLength = Mathf.Max(0, value);
				if (this.maxLength < this.text.Length)
				{
					this.Text = this.text.Substring(0, this.maxLength);
				}
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CD1 RID: 3281
	// (get) Token: 0x060040FE RID: 16638 RVA: 0x000F9570 File Offset: 0x000F7770
	// (set) Token: 0x060040FF RID: 16639 RVA: 0x000F9578 File Offset: 0x000F7778
	public TextAlignment TextAlignment
	{
		get
		{
			return this.textAlign;
		}
		set
		{
			if (value != this.textAlign)
			{
				this.textAlign = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CD2 RID: 3282
	// (get) Token: 0x06004100 RID: 16640 RVA: 0x000F9594 File Offset: 0x000F7794
	// (set) Token: 0x06004101 RID: 16641 RVA: 0x000F959C File Offset: 0x000F779C
	public bool Shadow
	{
		get
		{
			return this.shadow;
		}
		set
		{
			if (value != this.shadow)
			{
				this.shadow = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CD3 RID: 3283
	// (get) Token: 0x06004102 RID: 16642 RVA: 0x000F95B8 File Offset: 0x000F77B8
	// (set) Token: 0x06004103 RID: 16643 RVA: 0x000F95C0 File Offset: 0x000F77C0
	public Color32 ShadowColor
	{
		get
		{
			return this.shadowColor;
		}
		set
		{
			if (!value.Equals(this.shadowColor))
			{
				this.shadowColor = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CD4 RID: 3284
	// (get) Token: 0x06004104 RID: 16644 RVA: 0x000F95F8 File Offset: 0x000F77F8
	// (set) Token: 0x06004105 RID: 16645 RVA: 0x000F9600 File Offset: 0x000F7800
	public Vector2 ShadowOffset
	{
		get
		{
			return this.shadowOffset;
		}
		set
		{
			if (value != this.shadowOffset)
			{
				this.shadowOffset = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CD5 RID: 3285
	// (get) Token: 0x06004106 RID: 16646 RVA: 0x000F9620 File Offset: 0x000F7820
	// (set) Token: 0x06004107 RID: 16647 RVA: 0x000F9628 File Offset: 0x000F7828
	public bool UseMobileKeyboard
	{
		get
		{
			return this.useMobileKeyboard;
		}
		set
		{
			this.useMobileKeyboard = value;
		}
	}

	// Token: 0x17000CD6 RID: 3286
	// (get) Token: 0x06004108 RID: 16648 RVA: 0x000F9634 File Offset: 0x000F7834
	// (set) Token: 0x06004109 RID: 16649 RVA: 0x000F963C File Offset: 0x000F783C
	public bool MobileAutoCorrect
	{
		get
		{
			return this.mobileAutoCorrect;
		}
		set
		{
			this.mobileAutoCorrect = value;
		}
	}

	// Token: 0x17000CD7 RID: 3287
	// (get) Token: 0x0600410A RID: 16650 RVA: 0x000F9648 File Offset: 0x000F7848
	// (set) Token: 0x0600410B RID: 16651 RVA: 0x000F9650 File Offset: 0x000F7850
	public bool HideMobileInputField
	{
		get
		{
			return this.mobileHideInputField;
		}
		set
		{
			this.mobileHideInputField = value;
		}
	}

	// Token: 0x17000CD8 RID: 3288
	// (get) Token: 0x0600410C RID: 16652 RVA: 0x000F965C File Offset: 0x000F785C
	// (set) Token: 0x0600410D RID: 16653 RVA: 0x000F9664 File Offset: 0x000F7864
	public dfMobileKeyboardTrigger MobileKeyboardTrigger
	{
		get
		{
			return this.mobileKeyboardTrigger;
		}
		set
		{
			this.mobileKeyboardTrigger = value;
		}
	}

	// Token: 0x0600410E RID: 16654 RVA: 0x000F9670 File Offset: 0x000F7870
	protected override void OnTabKeyPressed(dfKeyEventArgs args)
	{
		if (this.acceptsTab)
		{
			base.OnKeyPress(args);
			if (args.Used)
			{
				return;
			}
			args.Character = '\t';
			this.processKeyPress(args);
		}
		else
		{
			base.OnTabKeyPressed(args);
		}
	}

	// Token: 0x0600410F RID: 16655 RVA: 0x000F96B8 File Offset: 0x000F78B8
	protected internal override void OnKeyPress(dfKeyEventArgs args)
	{
		if (this.ReadOnly || char.IsControl(args.Character))
		{
			base.OnKeyPress(args);
			return;
		}
		base.OnKeyPress(args);
		if (args.Used)
		{
			return;
		}
		this.processKeyPress(args);
	}

	// Token: 0x06004110 RID: 16656 RVA: 0x000F9704 File Offset: 0x000F7904
	private void processKeyPress(dfKeyEventArgs args)
	{
		this.deleteSelection();
		if (this.text.Length < this.MaxLength)
		{
			if (this.cursorIndex == this.text.Length)
			{
				this.text += args.Character;
			}
			else
			{
				this.text = this.text.Insert(this.cursorIndex, args.Character.ToString());
			}
			this.cursorIndex++;
			this.OnTextChanged();
			this.Invalidate();
		}
		args.Use();
	}

	// Token: 0x06004111 RID: 16657 RVA: 0x000F97AC File Offset: 0x000F79AC
	protected internal override void OnKeyDown(dfKeyEventArgs args)
	{
		if (this.ReadOnly)
		{
			return;
		}
		base.OnKeyDown(args);
		if (args.Used)
		{
			return;
		}
		KeyCode keyCode = args.KeyCode;
		switch (keyCode)
		{
		case 275:
			if (args.Control)
			{
				if (args.Shift)
				{
					this.moveSelectionPointRightWord();
				}
				else
				{
					this.moveToNextWord();
				}
			}
			else if (args.Shift)
			{
				this.moveSelectionPointRight();
			}
			else
			{
				this.moveToNextChar();
			}
			break;
		case 276:
			if (args.Control)
			{
				if (args.Shift)
				{
					this.moveSelectionPointLeftWord();
				}
				else
				{
					this.moveToPreviousWord();
				}
			}
			else if (args.Shift)
			{
				this.moveSelectionPointLeft();
			}
			else
			{
				this.moveToPreviousChar();
			}
			break;
		case 277:
			if (args.Shift)
			{
				string clipBoard = dfClipboardHelper.clipBoard;
				if (!string.IsNullOrEmpty(clipBoard))
				{
					this.pasteAtCursor(clipBoard);
				}
			}
			break;
		case 278:
			if (args.Shift)
			{
				this.selectToStart();
			}
			else
			{
				this.moveToStart();
			}
			break;
		case 279:
			if (args.Shift)
			{
				this.selectToEnd();
			}
			else
			{
				this.moveToEnd();
			}
			break;
		default:
			switch (keyCode)
			{
			case 97:
				if (args.Control)
				{
					this.selectAll();
				}
				break;
			default:
				switch (keyCode)
				{
				case 118:
					if (args.Control)
					{
						string clipBoard2 = dfClipboardHelper.clipBoard;
						if (!string.IsNullOrEmpty(clipBoard2))
						{
							this.pasteAtCursor(clipBoard2);
						}
					}
					break;
				default:
					if (keyCode != 8)
					{
						if (keyCode != 13)
						{
							if (keyCode != 27)
							{
								if (keyCode != 127)
								{
									base.OnKeyDown(args);
									return;
								}
								if (this.selectionStart != this.selectionEnd)
								{
									this.deleteSelection();
								}
								else if (args.Control)
								{
									this.deleteNextWord();
								}
								else
								{
									this.deleteNextChar();
								}
							}
							else
							{
								this.ClearSelection();
								this.cursorIndex = (this.scrollIndex = 0);
								this.Invalidate();
								this.OnCancel();
							}
						}
						else
						{
							this.OnSubmit();
						}
					}
					else if (args.Control)
					{
						this.deletePreviousWord();
					}
					else
					{
						this.deletePreviousChar();
					}
					break;
				case 120:
					if (args.Control)
					{
						this.cutSelectionToClipboard();
					}
					break;
				}
				break;
			case 99:
				if (args.Control)
				{
					this.copySelectionToClipboard();
				}
				break;
			}
			break;
		}
		args.Use();
	}

	// Token: 0x06004112 RID: 16658 RVA: 0x000F9A5C File Offset: 0x000F7C5C
	private void selectAll()
	{
		this.selectionStart = 0;
		this.selectionEnd = this.text.Length;
		this.scrollIndex = 0;
		this.setCursorPos(0);
	}

	// Token: 0x06004113 RID: 16659 RVA: 0x000F9A90 File Offset: 0x000F7C90
	private void cutSelectionToClipboard()
	{
		this.copySelectionToClipboard();
		this.deleteSelection();
	}

	// Token: 0x06004114 RID: 16660 RVA: 0x000F9AA0 File Offset: 0x000F7CA0
	private void copySelectionToClipboard()
	{
		if (this.selectionStart == this.selectionEnd)
		{
			return;
		}
		dfClipboardHelper.clipBoard = this.text.Substring(this.selectionStart, this.selectionEnd - this.selectionStart);
	}

	// Token: 0x06004115 RID: 16661 RVA: 0x000F9AD8 File Offset: 0x000F7CD8
	private void pasteAtCursor(string clipData)
	{
		this.deleteSelection();
		StringBuilder stringBuilder = new StringBuilder(this.text.Length + clipData.Length);
		stringBuilder.Append(this.text);
		foreach (char c in clipData)
		{
			if (c >= ' ')
			{
				stringBuilder.Insert(this.cursorIndex++, c);
			}
		}
		stringBuilder.Length = Mathf.Min(stringBuilder.Length, this.maxLength);
		this.text = stringBuilder.ToString();
		this.setCursorPos(this.cursorIndex);
		this.OnTextChanged();
		this.Invalidate();
	}

	// Token: 0x06004116 RID: 16662 RVA: 0x000F9B8C File Offset: 0x000F7D8C
	private void selectWordAtIndex(int index)
	{
		if (string.IsNullOrEmpty(this.text))
		{
			return;
		}
		index = Mathf.Max(Mathf.Min(this.text.Length - 1, index), 0);
		char c = this.text[index];
		if (!char.IsLetterOrDigit(c))
		{
			this.selectionStart = index;
			this.selectionEnd = index + 1;
			this.mouseSelectionAnchor = 0;
		}
		else
		{
			this.selectionStart = index;
			for (int i = index; i > 0; i--)
			{
				if (!char.IsLetterOrDigit(this.text[i - 1]))
				{
					break;
				}
				this.selectionStart--;
			}
			this.selectionEnd = index;
			for (int j = index; j < this.text.Length; j++)
			{
				if (!char.IsLetterOrDigit(this.text[j]))
				{
					break;
				}
				this.selectionEnd = j + 1;
			}
		}
		this.cursorIndex = this.selectionStart;
		this.Invalidate();
	}

	// Token: 0x06004117 RID: 16663 RVA: 0x000F9CA0 File Offset: 0x000F7EA0
	private void moveToNextWord()
	{
		this.ClearSelection();
		if (this.cursorIndex == this.text.Length)
		{
			return;
		}
		int cursorPos = this.findNextWord(this.cursorIndex);
		this.setCursorPos(cursorPos);
	}

	// Token: 0x06004118 RID: 16664 RVA: 0x000F9CE0 File Offset: 0x000F7EE0
	private void moveToPreviousWord()
	{
		this.ClearSelection();
		if (this.cursorIndex == 0)
		{
			return;
		}
		int cursorPos = this.findPreviousWord(this.cursorIndex);
		this.setCursorPos(cursorPos);
	}

	// Token: 0x06004119 RID: 16665 RVA: 0x000F9D14 File Offset: 0x000F7F14
	private void deletePreviousChar()
	{
		if (this.selectionStart != this.selectionEnd)
		{
			int cursorPos = this.selectionStart;
			this.deleteSelection();
			this.setCursorPos(cursorPos);
			return;
		}
		this.ClearSelection();
		if (this.cursorIndex == 0)
		{
			return;
		}
		this.text = this.text.Remove(this.cursorIndex - 1, 1);
		this.cursorIndex--;
		this.cursorShown = true;
		this.OnTextChanged();
		this.Invalidate();
	}

	// Token: 0x0600411A RID: 16666 RVA: 0x000F9D94 File Offset: 0x000F7F94
	private void deletePreviousWord()
	{
		this.ClearSelection();
		if (this.cursorIndex == 0)
		{
			return;
		}
		int num = this.findPreviousWord(this.cursorIndex);
		if (num == this.cursorIndex)
		{
			num = 0;
		}
		this.text = this.text.Remove(num, this.cursorIndex - num);
		this.setCursorPos(num);
		this.OnTextChanged();
		this.Invalidate();
	}

	// Token: 0x0600411B RID: 16667 RVA: 0x000F9DFC File Offset: 0x000F7FFC
	private void deleteSelection()
	{
		if (this.selectionStart == this.selectionEnd)
		{
			return;
		}
		this.text = this.text.Remove(this.selectionStart, this.selectionEnd - this.selectionStart);
		this.setCursorPos(this.selectionStart);
		this.ClearSelection();
		this.OnTextChanged();
		this.Invalidate();
	}

	// Token: 0x0600411C RID: 16668 RVA: 0x000F9E60 File Offset: 0x000F8060
	private void deleteNextChar()
	{
		this.ClearSelection();
		if (this.cursorIndex >= this.text.Length)
		{
			return;
		}
		this.text = this.text.Remove(this.cursorIndex, 1);
		this.cursorShown = true;
		this.OnTextChanged();
		this.Invalidate();
	}

	// Token: 0x0600411D RID: 16669 RVA: 0x000F9EB8 File Offset: 0x000F80B8
	private void deleteNextWord()
	{
		this.ClearSelection();
		if (this.cursorIndex == this.text.Length)
		{
			return;
		}
		int num = this.findNextWord(this.cursorIndex);
		if (num == this.cursorIndex)
		{
			num = this.text.Length;
		}
		this.text = this.text.Remove(this.cursorIndex, num - this.cursorIndex);
		this.OnTextChanged();
		this.Invalidate();
	}

	// Token: 0x0600411E RID: 16670 RVA: 0x000F9F34 File Offset: 0x000F8134
	private void selectToStart()
	{
		if (this.cursorIndex == 0)
		{
			return;
		}
		if (this.selectionEnd == this.selectionStart)
		{
			this.selectionEnd = this.cursorIndex;
		}
		else if (this.selectionEnd == this.cursorIndex)
		{
			this.selectionEnd = this.selectionStart;
		}
		this.selectionStart = 0;
		this.setCursorPos(0);
	}

	// Token: 0x0600411F RID: 16671 RVA: 0x000F9F9C File Offset: 0x000F819C
	private void selectToEnd()
	{
		if (this.cursorIndex == this.text.Length)
		{
			return;
		}
		if (this.selectionEnd == this.selectionStart)
		{
			this.selectionStart = this.cursorIndex;
		}
		else if (this.selectionStart == this.cursorIndex)
		{
			this.selectionStart = this.selectionEnd;
		}
		this.selectionEnd = this.text.Length;
		this.setCursorPos(this.text.Length);
	}

	// Token: 0x06004120 RID: 16672 RVA: 0x000FA024 File Offset: 0x000F8224
	private void moveToEnd()
	{
		this.ClearSelection();
		this.setCursorPos(this.text.Length);
	}

	// Token: 0x06004121 RID: 16673 RVA: 0x000FA040 File Offset: 0x000F8240
	private void moveToStart()
	{
		this.ClearSelection();
		this.setCursorPos(0);
	}

	// Token: 0x06004122 RID: 16674 RVA: 0x000FA050 File Offset: 0x000F8250
	private void moveToNextChar()
	{
		this.ClearSelection();
		this.setCursorPos(this.cursorIndex + 1);
	}

	// Token: 0x06004123 RID: 16675 RVA: 0x000FA068 File Offset: 0x000F8268
	private void moveSelectionPointRightWord()
	{
		if (this.cursorIndex == this.text.Length)
		{
			return;
		}
		int cursorPos = this.findNextWord(this.cursorIndex);
		if (this.selectionEnd == this.selectionStart)
		{
			this.selectionStart = this.cursorIndex;
			this.selectionEnd = cursorPos;
		}
		else if (this.selectionEnd == this.cursorIndex)
		{
			this.selectionEnd = cursorPos;
		}
		else if (this.selectionStart == this.cursorIndex)
		{
			this.selectionStart = cursorPos;
		}
		this.setCursorPos(cursorPos);
	}

	// Token: 0x06004124 RID: 16676 RVA: 0x000FA100 File Offset: 0x000F8300
	private void moveSelectionPointLeftWord()
	{
		if (this.cursorIndex == 0)
		{
			return;
		}
		int cursorPos = this.findPreviousWord(this.cursorIndex);
		if (this.selectionEnd == this.selectionStart)
		{
			this.selectionEnd = this.cursorIndex;
			this.selectionStart = cursorPos;
		}
		else if (this.selectionEnd == this.cursorIndex)
		{
			this.selectionEnd = cursorPos;
		}
		else if (this.selectionStart == this.cursorIndex)
		{
			this.selectionStart = cursorPos;
		}
		this.setCursorPos(cursorPos);
	}

	// Token: 0x06004125 RID: 16677 RVA: 0x000FA18C File Offset: 0x000F838C
	private void moveSelectionPointRight()
	{
		if (this.cursorIndex == this.text.Length)
		{
			return;
		}
		if (this.selectionEnd == this.selectionStart)
		{
			this.selectionEnd = this.cursorIndex + 1;
			this.selectionStart = this.cursorIndex;
		}
		else if (this.selectionEnd == this.cursorIndex)
		{
			this.selectionEnd++;
		}
		else if (this.selectionStart == this.cursorIndex)
		{
			this.selectionStart++;
		}
		this.setCursorPos(this.cursorIndex + 1);
	}

	// Token: 0x06004126 RID: 16678 RVA: 0x000FA234 File Offset: 0x000F8434
	private void moveSelectionPointLeft()
	{
		if (this.cursorIndex == 0)
		{
			return;
		}
		if (this.selectionEnd == this.selectionStart)
		{
			this.selectionEnd = this.cursorIndex;
			this.selectionStart = this.cursorIndex - 1;
		}
		else if (this.selectionEnd == this.cursorIndex)
		{
			this.selectionEnd--;
		}
		else if (this.selectionStart == this.cursorIndex)
		{
			this.selectionStart--;
		}
		this.setCursorPos(this.cursorIndex - 1);
	}

	// Token: 0x06004127 RID: 16679 RVA: 0x000FA2D0 File Offset: 0x000F84D0
	private void moveToPreviousChar()
	{
		this.ClearSelection();
		this.setCursorPos(this.cursorIndex - 1);
	}

	// Token: 0x06004128 RID: 16680 RVA: 0x000FA2E8 File Offset: 0x000F84E8
	private void setCursorPos(int index)
	{
		index = Mathf.Max(0, Mathf.Min(this.text.Length, index));
		if (index == this.cursorIndex)
		{
			return;
		}
		this.cursorIndex = index;
		this.cursorShown = this.HasFocus;
		this.scrollIndex = Mathf.Min(this.scrollIndex, this.cursorIndex);
		this.Invalidate();
	}

	// Token: 0x06004129 RID: 16681 RVA: 0x000FA34C File Offset: 0x000F854C
	private int findPreviousWord(int startIndex)
	{
		int i;
		for (i = startIndex; i > 0; i--)
		{
			char c = this.text[i - 1];
			if (!char.IsWhiteSpace(c) && !char.IsSeparator(c) && !char.IsPunctuation(c))
			{
				break;
			}
		}
		for (int j = i; j >= 0; j--)
		{
			if (j == 0)
			{
				i = 0;
				break;
			}
			char c2 = this.text[j - 1];
			if (char.IsWhiteSpace(c2) || char.IsSeparator(c2) || char.IsPunctuation(c2))
			{
				i = j;
				break;
			}
		}
		return i;
	}

	// Token: 0x0600412A RID: 16682 RVA: 0x000FA3FC File Offset: 0x000F85FC
	private int findNextWord(int startIndex)
	{
		int length = this.text.Length;
		int i = startIndex;
		for (int j = i; j < length; j++)
		{
			char c = this.text[j];
			if (char.IsWhiteSpace(c) || char.IsSeparator(c) || char.IsPunctuation(c))
			{
				i = j;
				break;
			}
		}
		while (i < length)
		{
			char c2 = this.text[i];
			if (!char.IsWhiteSpace(c2) && !char.IsSeparator(c2) && !char.IsPunctuation(c2))
			{
				break;
			}
			i++;
		}
		return i;
	}

	// Token: 0x0600412B RID: 16683 RVA: 0x000FA4AC File Offset: 0x000F86AC
	public override void OnEnable()
	{
		if (this.padding == null)
		{
			this.padding = new RectOffset();
		}
		base.OnEnable();
		if (this.size.magnitude == 0f)
		{
			base.Size = new Vector2(100f, 20f);
		}
		this.cursorShown = false;
		this.cursorIndex = (this.scrollIndex = 0);
		bool flag = this.Font != null && this.Font.IsValid;
		if (Application.isPlaying && !flag)
		{
			this.Font = base.GetManager().DefaultFont;
		}
	}

	// Token: 0x0600412C RID: 16684 RVA: 0x000FA558 File Offset: 0x000F8758
	public override void Awake()
	{
		base.Awake();
		this.startSize = base.Size;
	}

	// Token: 0x0600412D RID: 16685 RVA: 0x000FA56C File Offset: 0x000F876C
	protected internal override void OnEnterFocus(dfFocusEventArgs args)
	{
		base.OnEnterFocus(args);
		this.undoText = this.Text;
		if (!this.ReadOnly)
		{
			this.whenGotFocus = Time.realtimeSinceStartup;
			base.StartCoroutine(this.doCursorBlink());
			if (this.selectOnFocus)
			{
				this.selectionStart = 0;
				this.selectionEnd = this.text.Length;
			}
			else
			{
				this.selectionStart = (this.selectionEnd = 0);
			}
		}
		this.Invalidate();
	}

	// Token: 0x0600412E RID: 16686 RVA: 0x000FA5F0 File Offset: 0x000F87F0
	protected internal override void OnLeaveFocus(dfFocusEventArgs args)
	{
		base.OnLeaveFocus(args);
		this.cursorShown = false;
		this.ClearSelection();
		this.Invalidate();
		this.whenGotFocus = 0f;
	}

	// Token: 0x0600412F RID: 16687 RVA: 0x000FA618 File Offset: 0x000F8818
	protected internal override void OnDoubleClick(dfMouseEventArgs args)
	{
		if (args.Source != this)
		{
			base.OnDoubleClick(args);
			return;
		}
		if (!this.ReadOnly && this.HasFocus && args.Buttons.IsSet(dfMouseButtons.Left) && Time.realtimeSinceStartup - this.whenGotFocus > 0.5f)
		{
			int charIndexOfMouse = this.getCharIndexOfMouse(args);
			this.selectWordAtIndex(charIndexOfMouse);
		}
		base.OnDoubleClick(args);
	}

	// Token: 0x06004130 RID: 16688 RVA: 0x000FA694 File Offset: 0x000F8894
	protected internal override void OnMouseDown(dfMouseEventArgs args)
	{
		if (args.Source != this)
		{
			base.OnMouseDown(args);
			return;
		}
		bool flag = !this.ReadOnly && args.Buttons.IsSet(dfMouseButtons.Left) && ((!this.HasFocus && !this.SelectOnFocus) || Time.realtimeSinceStartup - this.whenGotFocus > 0.25f);
		if (flag)
		{
			int charIndexOfMouse = this.getCharIndexOfMouse(args);
			if (charIndexOfMouse != this.cursorIndex)
			{
				this.cursorIndex = charIndexOfMouse;
				this.cursorShown = true;
				this.Invalidate();
				args.Use();
			}
			this.mouseSelectionAnchor = this.cursorIndex;
			this.selectionStart = (this.selectionEnd = this.cursorIndex);
		}
		base.OnMouseDown(args);
	}

	// Token: 0x06004131 RID: 16689 RVA: 0x000FA764 File Offset: 0x000F8964
	protected internal override void OnMouseMove(dfMouseEventArgs args)
	{
		if (args.Source != this)
		{
			base.OnMouseMove(args);
			return;
		}
		if (!this.ReadOnly && this.HasFocus && args.Buttons.IsSet(dfMouseButtons.Left))
		{
			int charIndexOfMouse = this.getCharIndexOfMouse(args);
			if (charIndexOfMouse != this.cursorIndex)
			{
				this.cursorIndex = charIndexOfMouse;
				this.cursorShown = true;
				this.Invalidate();
				args.Use();
				this.selectionStart = Mathf.Min(this.mouseSelectionAnchor, charIndexOfMouse);
				this.selectionEnd = Mathf.Max(this.mouseSelectionAnchor, charIndexOfMouse);
				return;
			}
		}
		base.OnMouseMove(args);
	}

	// Token: 0x06004132 RID: 16690 RVA: 0x000FA80C File Offset: 0x000F8A0C
	protected internal virtual void OnTextChanged()
	{
		base.SignalHierarchy("OnTextChanged", new object[]
		{
			this.text
		});
		if (this.TextChanged != null)
		{
			this.TextChanged(this, this.text);
		}
	}

	// Token: 0x06004133 RID: 16691 RVA: 0x000FA854 File Offset: 0x000F8A54
	protected internal virtual void OnReadOnlyChanged()
	{
		if (this.ReadOnlyChanged != null)
		{
			this.ReadOnlyChanged(this, this.readOnly);
		}
	}

	// Token: 0x06004134 RID: 16692 RVA: 0x000FA874 File Offset: 0x000F8A74
	protected internal virtual void OnPasswordCharacterChanged()
	{
		if (this.PasswordCharacterChanged != null)
		{
			this.PasswordCharacterChanged(this, this.passwordChar);
		}
	}

	// Token: 0x06004135 RID: 16693 RVA: 0x000FA894 File Offset: 0x000F8A94
	protected internal virtual void OnSubmit()
	{
		base.SignalHierarchy("OnTextSubmitted", new object[]
		{
			this,
			this.text
		});
		if (this.TextSubmitted != null)
		{
			this.TextSubmitted(this, this.text);
		}
	}

	// Token: 0x06004136 RID: 16694 RVA: 0x000FA8E0 File Offset: 0x000F8AE0
	protected internal virtual void OnCancel()
	{
		this.text = this.undoText;
		base.SignalHierarchy("OnTextCancelled", new object[]
		{
			this,
			this.text
		});
		if (this.TextCancelled != null)
		{
			this.TextCancelled(this, this.text);
		}
	}

	// Token: 0x06004137 RID: 16695 RVA: 0x000FA938 File Offset: 0x000F8B38
	public void ClearSelection()
	{
		this.selectionStart = 0;
		this.selectionEnd = 0;
		this.mouseSelectionAnchor = 0;
	}

	// Token: 0x06004138 RID: 16696 RVA: 0x000FA950 File Offset: 0x000F8B50
	private IEnumerator doCursorBlink()
	{
		if (!Application.isPlaying)
		{
			yield break;
		}
		this.cursorShown = true;
		while (this.ContainsFocus)
		{
			yield return new WaitForSeconds(this.cursorBlinkTime);
			this.cursorShown = !this.cursorShown;
			this.Invalidate();
		}
		this.cursorShown = false;
		yield break;
	}

	// Token: 0x06004139 RID: 16697 RVA: 0x000FA96C File Offset: 0x000F8B6C
	private void renderText(dfRenderData textBuffer)
	{
		float num = base.PixelsToUnits();
		Vector2 vector;
		vector..ctor(this.size.x - (float)this.padding.horizontal, this.size.y - (float)this.padding.vertical);
		Vector3 vector2 = this.pivot.TransformToUpperLeft(base.Size);
		Vector3 vectorOffset = new Vector3(vector2.x + (float)this.padding.left, vector2.y - (float)this.padding.top, 0f) * num;
		string text = (!this.IsPasswordField || string.IsNullOrEmpty(this.passwordChar)) ? this.text : this.passwordDisplayText();
		Color32 color = (!base.IsEnabled) ? base.DisabledColor : this.TextColor;
		float textScaleMultiplier = this.getTextScaleMultiplier();
		using (dfFontRendererBase dfFontRendererBase = this.font.ObtainRenderer())
		{
			dfFontRendererBase.WordWrap = false;
			dfFontRendererBase.MaxSize = vector;
			dfFontRendererBase.PixelRatio = num;
			dfFontRendererBase.TextScale = this.TextScale * textScaleMultiplier;
			dfFontRendererBase.VectorOffset = vectorOffset;
			dfFontRendererBase.MultiLine = false;
			dfFontRendererBase.TextAlign = 0;
			dfFontRendererBase.ProcessMarkup = false;
			dfFontRendererBase.DefaultColor = color;
			dfFontRendererBase.BottomColor = new Color32?(color);
			dfFontRendererBase.OverrideMarkupColors = false;
			dfFontRendererBase.Opacity = base.CalculateOpacity();
			dfFontRendererBase.Shadow = this.Shadow;
			dfFontRendererBase.ShadowColor = this.ShadowColor;
			dfFontRendererBase.ShadowOffset = this.ShadowOffset;
			this.cursorIndex = Mathf.Min(this.cursorIndex, text.Length);
			this.scrollIndex = Mathf.Min(Mathf.Min(this.scrollIndex, this.cursorIndex), text.Length);
			this.charWidths = dfFontRendererBase.GetCharacterWidths(text);
			Vector2 vector3 = vector * num;
			this.leftOffset = 0f;
			if (this.textAlign == null)
			{
				float num2 = 0f;
				for (int i = this.scrollIndex; i < this.cursorIndex; i++)
				{
					num2 += this.charWidths[i];
				}
				while (num2 >= vector3.x && this.scrollIndex < this.cursorIndex)
				{
					num2 -= this.charWidths[this.scrollIndex++];
				}
			}
			else
			{
				this.scrollIndex = Mathf.Max(0, Mathf.Min(this.cursorIndex, text.Length - 1));
				float num3 = 0f;
				float num4 = (float)this.font.FontSize * 1.25f * num;
				while (this.scrollIndex > 0 && num3 < vector3.x - num4)
				{
					num3 += this.charWidths[this.scrollIndex--];
				}
				float num5 = (text.Length <= 0) ? 0f : dfFontRendererBase.GetCharacterWidths(text.Substring(this.scrollIndex)).Sum();
				TextAlignment textAlignment = this.textAlign;
				if (textAlignment != 1)
				{
					if (textAlignment == 2)
					{
						this.leftOffset = Mathf.Max(0f, vector3.x - num5);
					}
				}
				else
				{
					this.leftOffset = Mathf.Max(0f, (vector3.x - num5) * 0.5f);
				}
				vectorOffset.x += this.leftOffset;
				dfFontRendererBase.VectorOffset = vectorOffset;
			}
			if (this.selectionEnd != this.selectionStart)
			{
				this.renderSelection(this.scrollIndex, this.charWidths, this.leftOffset);
			}
			else if (this.cursorShown)
			{
				this.renderCursor(this.scrollIndex, this.cursorIndex, this.charWidths, this.leftOffset);
			}
			dfFontRendererBase.Render(text.Substring(this.scrollIndex), textBuffer);
		}
	}

	// Token: 0x0600413A RID: 16698 RVA: 0x000FADB0 File Offset: 0x000F8FB0
	private float getTextScaleMultiplier()
	{
		if (this.textScaleMode == dfTextScaleMode.None || !Application.isPlaying)
		{
			return 1f;
		}
		if (this.textScaleMode == dfTextScaleMode.ScreenResolution)
		{
			return (float)Screen.height / (float)this.manager.FixedHeight;
		}
		return base.Size.y / this.startSize.y;
	}

	// Token: 0x0600413B RID: 16699 RVA: 0x000FAE14 File Offset: 0x000F9014
	private string passwordDisplayText()
	{
		return new string(this.passwordChar[0], this.text.Length);
	}

	// Token: 0x0600413C RID: 16700 RVA: 0x000FAE34 File Offset: 0x000F9034
	private void renderSelection(int scrollIndex, float[] charWidths, float leftOffset)
	{
		if (string.IsNullOrEmpty(this.SelectionSprite) || base.Atlas == null)
		{
			return;
		}
		float num = base.PixelsToUnits();
		float num2 = (this.size.x - (float)this.padding.horizontal) * num;
		int num3 = scrollIndex;
		float num4 = 0f;
		for (int i = scrollIndex; i < this.text.Length; i++)
		{
			num3++;
			num4 += charWidths[i];
			if (num4 > num2)
			{
				break;
			}
		}
		if (this.selectionStart > num3 || this.selectionEnd < scrollIndex)
		{
			return;
		}
		int num5 = Mathf.Max(scrollIndex, this.selectionStart);
		if (num5 > num3)
		{
			return;
		}
		int num6 = Mathf.Min(this.selectionEnd, num3);
		if (num6 <= scrollIndex)
		{
			return;
		}
		float num7 = 0f;
		float num8 = 0f;
		num4 = 0f;
		for (int j = scrollIndex; j <= num3; j++)
		{
			if (j == num5)
			{
				num7 = num4;
			}
			if (j == num6)
			{
				num8 = num4;
				break;
			}
			num4 += charWidths[j];
		}
		float num9 = base.Size.y * num;
		this.addQuadIndices(this.renderData.Vertices, this.renderData.Triangles);
		float num10 = num7 + leftOffset + (float)this.padding.left * num;
		float num11 = num10 + Mathf.Min(num8 - num7, num2);
		float num12 = (float)(-(float)(this.padding.top + 1)) * num;
		float num13 = num12 - num9 + (float)(this.padding.vertical + 2) * num;
		Vector3 vector = this.pivot.TransformToUpperLeft(base.Size) * num;
		Vector3 item = new Vector3(num10, num12) + vector;
		Vector3 item2 = new Vector3(num11, num12) + vector;
		Vector3 item3 = new Vector3(num10, num13) + vector;
		Vector3 item4 = new Vector3(num11, num13) + vector;
		this.renderData.Vertices.Add(item);
		this.renderData.Vertices.Add(item2);
		this.renderData.Vertices.Add(item4);
		this.renderData.Vertices.Add(item3);
		Color32 item5 = base.ApplyOpacity(this.SelectionBackgroundColor);
		this.renderData.Colors.Add(item5);
		this.renderData.Colors.Add(item5);
		this.renderData.Colors.Add(item5);
		this.renderData.Colors.Add(item5);
		dfAtlas.ItemInfo itemInfo = base.Atlas[this.SelectionSprite];
		Rect region = itemInfo.region;
		float num14 = region.width / itemInfo.sizeInPixels.x;
		float num15 = region.height / itemInfo.sizeInPixels.y;
		this.renderData.UV.Add(new Vector2(region.x + num14, region.yMax - num15));
		this.renderData.UV.Add(new Vector2(region.xMax - num14, region.yMax - num15));
		this.renderData.UV.Add(new Vector2(region.xMax - num14, region.y + num15));
		this.renderData.UV.Add(new Vector2(region.x + num14, region.y + num15));
	}

	// Token: 0x0600413D RID: 16701 RVA: 0x000FB1C4 File Offset: 0x000F93C4
	private void renderCursor(int startIndex, int cursorIndex, float[] charWidths, float leftOffset)
	{
		if (string.IsNullOrEmpty(this.SelectionSprite) || base.Atlas == null)
		{
			return;
		}
		float num = 0f;
		for (int i = startIndex; i < cursorIndex; i++)
		{
			num += charWidths[i];
		}
		float num2 = base.PixelsToUnits();
		float num3 = (num + leftOffset + (float)this.padding.left * num2).Quantize(num2);
		float num4 = (float)(-(float)this.padding.top) * num2;
		float num5 = num2 * (float)this.cursorWidth;
		float num6 = (this.size.y - (float)this.padding.vertical) * num2;
		Vector3 vector;
		vector..ctor(num3, num4);
		Vector3 vector2;
		vector2..ctor(num3 + num5, num4);
		Vector3 vector3;
		vector3..ctor(num3 + num5, num4 - num6);
		Vector3 vector4;
		vector4..ctor(num3, num4 - num6);
		dfList<Vector3> vertices = this.renderData.Vertices;
		dfList<int> triangles = this.renderData.Triangles;
		dfList<Vector2> uv = this.renderData.UV;
		dfList<Color32> colors = this.renderData.Colors;
		Vector3 vector5 = this.pivot.TransformToUpperLeft(this.size) * num2;
		this.addQuadIndices(vertices, triangles);
		vertices.Add(vector + vector5);
		vertices.Add(vector2 + vector5);
		vertices.Add(vector3 + vector5);
		vertices.Add(vector4 + vector5);
		Color32 item = base.ApplyOpacity(this.TextColor);
		colors.Add(item);
		colors.Add(item);
		colors.Add(item);
		colors.Add(item);
		dfAtlas.ItemInfo itemInfo = base.Atlas[this.SelectionSprite];
		Rect region = itemInfo.region;
		uv.Add(new Vector2(region.x, region.yMax));
		uv.Add(new Vector2(region.xMax, region.yMax));
		uv.Add(new Vector2(region.xMax, region.y));
		uv.Add(new Vector2(region.x, region.y));
	}

	// Token: 0x0600413E RID: 16702 RVA: 0x000FB3EC File Offset: 0x000F95EC
	private void addQuadIndices(dfList<Vector3> verts, dfList<int> triangles)
	{
		int count = verts.Count;
		int[] array = new int[]
		{
			0,
			1,
			3,
			3,
			1,
			2
		};
		for (int i = 0; i < array.Length; i++)
		{
			triangles.Add(count + array[i]);
		}
	}

	// Token: 0x0600413F RID: 16703 RVA: 0x000FB434 File Offset: 0x000F9634
	private int getCharIndexOfMouse(dfMouseEventArgs args)
	{
		Vector2 hitPosition = base.GetHitPosition(args);
		float num = base.PixelsToUnits();
		int num2 = this.scrollIndex;
		float num3 = this.leftOffset / num;
		for (int i = this.scrollIndex; i < this.charWidths.Length; i++)
		{
			num3 += this.charWidths[i] / num;
			if (num3 < hitPosition.x)
			{
				num2++;
			}
		}
		return num2;
	}

	// Token: 0x06004140 RID: 16704 RVA: 0x000FB4A4 File Offset: 0x000F96A4
	public dfList<dfRenderData> RenderMultiple()
	{
		if (base.Atlas == null || this.Font == null)
		{
			return null;
		}
		if (!this.isVisible)
		{
			return null;
		}
		if (this.renderData == null)
		{
			this.renderData = dfRenderData.Obtain();
			this.textRenderData = dfRenderData.Obtain();
			this.isControlInvalidated = true;
		}
		if (!this.isControlInvalidated)
		{
			for (int i = 0; i < this.buffers.Count; i++)
			{
				this.buffers[i].Transform = base.transform.localToWorldMatrix;
			}
			return this.buffers;
		}
		this.buffers.Clear();
		this.renderData.Clear();
		this.renderData.Material = base.Atlas.Material;
		this.renderData.Transform = base.transform.localToWorldMatrix;
		this.buffers.Add(this.renderData);
		this.textRenderData.Clear();
		this.textRenderData.Material = base.Atlas.Material;
		this.textRenderData.Transform = base.transform.localToWorldMatrix;
		this.buffers.Add(this.textRenderData);
		this.renderBackground();
		this.renderText(this.textRenderData);
		this.isControlInvalidated = false;
		this.updateCollider();
		return this.buffers;
	}

	// Token: 0x04002250 RID: 8784
	[SerializeField]
	protected dfFontBase font;

	// Token: 0x04002251 RID: 8785
	[SerializeField]
	protected bool acceptsTab;

	// Token: 0x04002252 RID: 8786
	[SerializeField]
	protected bool displayAsPassword;

	// Token: 0x04002253 RID: 8787
	[SerializeField]
	protected string passwordChar = "*";

	// Token: 0x04002254 RID: 8788
	[SerializeField]
	protected bool readOnly;

	// Token: 0x04002255 RID: 8789
	[SerializeField]
	protected string text = string.Empty;

	// Token: 0x04002256 RID: 8790
	[SerializeField]
	protected Color32 textColor = UnityEngine.Color.white;

	// Token: 0x04002257 RID: 8791
	[SerializeField]
	protected Color32 selectionBackground = new Color32(0, 105, 210, byte.MaxValue);

	// Token: 0x04002258 RID: 8792
	[SerializeField]
	protected string selectionSprite = string.Empty;

	// Token: 0x04002259 RID: 8793
	[SerializeField]
	protected float textScale = 1f;

	// Token: 0x0400225A RID: 8794
	[SerializeField]
	protected dfTextScaleMode textScaleMode;

	// Token: 0x0400225B RID: 8795
	[SerializeField]
	protected RectOffset padding = new RectOffset();

	// Token: 0x0400225C RID: 8796
	[SerializeField]
	protected float cursorBlinkTime = 0.45f;

	// Token: 0x0400225D RID: 8797
	[SerializeField]
	protected int cursorWidth = 1;

	// Token: 0x0400225E RID: 8798
	[SerializeField]
	protected int maxLength = 1024;

	// Token: 0x0400225F RID: 8799
	[SerializeField]
	protected bool selectOnFocus;

	// Token: 0x04002260 RID: 8800
	[SerializeField]
	protected bool shadow;

	// Token: 0x04002261 RID: 8801
	[SerializeField]
	protected Color32 shadowColor = UnityEngine.Color.black;

	// Token: 0x04002262 RID: 8802
	[SerializeField]
	protected Vector2 shadowOffset = new Vector2(1f, -1f);

	// Token: 0x04002263 RID: 8803
	[SerializeField]
	protected bool useMobileKeyboard;

	// Token: 0x04002264 RID: 8804
	[SerializeField]
	protected int mobileKeyboardType;

	// Token: 0x04002265 RID: 8805
	[SerializeField]
	protected bool mobileAutoCorrect;

	// Token: 0x04002266 RID: 8806
	[SerializeField]
	protected bool mobileHideInputField;

	// Token: 0x04002267 RID: 8807
	[SerializeField]
	protected dfMobileKeyboardTrigger mobileKeyboardTrigger;

	// Token: 0x04002268 RID: 8808
	[SerializeField]
	protected TextAlignment textAlign;

	// Token: 0x04002269 RID: 8809
	private Vector2 startSize = Vector2.zero;

	// Token: 0x0400226A RID: 8810
	private int selectionStart;

	// Token: 0x0400226B RID: 8811
	private int selectionEnd;

	// Token: 0x0400226C RID: 8812
	private int mouseSelectionAnchor;

	// Token: 0x0400226D RID: 8813
	private int scrollIndex;

	// Token: 0x0400226E RID: 8814
	private int cursorIndex;

	// Token: 0x0400226F RID: 8815
	private float leftOffset;

	// Token: 0x04002270 RID: 8816
	private bool cursorShown;

	// Token: 0x04002271 RID: 8817
	private float[] charWidths;

	// Token: 0x04002272 RID: 8818
	private float whenGotFocus;

	// Token: 0x04002273 RID: 8819
	private string undoText = string.Empty;

	// Token: 0x04002274 RID: 8820
	private dfRenderData textRenderData;

	// Token: 0x04002275 RID: 8821
	private dfList<dfRenderData> buffers = dfList<dfRenderData>.Obtain();
}
