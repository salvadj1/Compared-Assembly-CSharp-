using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

// Token: 0x020007D0 RID: 2000
[RequireComponent(typeof(BoxCollider))]
[AddComponentMenu("Daikon Forge/User Interface/Textbox")]
[ExecuteInEditMode]
[Serializable]
public class dfTextbox : global::dfInteractiveBase, global::IDFMultiRender
{
	// Token: 0x14000055 RID: 85
	// (add) Token: 0x060044EA RID: 17642 RVA: 0x00101C10 File Offset: 0x000FFE10
	// (remove) Token: 0x060044EB RID: 17643 RVA: 0x00101C2C File Offset: 0x000FFE2C
	public event global::PropertyChangedEventHandler<bool> ReadOnlyChanged;

	// Token: 0x14000056 RID: 86
	// (add) Token: 0x060044EC RID: 17644 RVA: 0x00101C48 File Offset: 0x000FFE48
	// (remove) Token: 0x060044ED RID: 17645 RVA: 0x00101C64 File Offset: 0x000FFE64
	public event global::PropertyChangedEventHandler<string> PasswordCharacterChanged;

	// Token: 0x14000057 RID: 87
	// (add) Token: 0x060044EE RID: 17646 RVA: 0x00101C80 File Offset: 0x000FFE80
	// (remove) Token: 0x060044EF RID: 17647 RVA: 0x00101C9C File Offset: 0x000FFE9C
	public event global::PropertyChangedEventHandler<string> TextChanged;

	// Token: 0x14000058 RID: 88
	// (add) Token: 0x060044F0 RID: 17648 RVA: 0x00101CB8 File Offset: 0x000FFEB8
	// (remove) Token: 0x060044F1 RID: 17649 RVA: 0x00101CD4 File Offset: 0x000FFED4
	public event global::PropertyChangedEventHandler<string> TextSubmitted;

	// Token: 0x14000059 RID: 89
	// (add) Token: 0x060044F2 RID: 17650 RVA: 0x00101CF0 File Offset: 0x000FFEF0
	// (remove) Token: 0x060044F3 RID: 17651 RVA: 0x00101D0C File Offset: 0x000FFF0C
	public event global::PropertyChangedEventHandler<string> TextCancelled;

	// Token: 0x17000D41 RID: 3393
	// (get) Token: 0x060044F4 RID: 17652 RVA: 0x00101D28 File Offset: 0x000FFF28
	// (set) Token: 0x060044F5 RID: 17653 RVA: 0x00101D6C File Offset: 0x000FFF6C
	public global::dfFontBase Font
	{
		get
		{
			if (this.font == null)
			{
				global::dfGUIManager manager = base.GetManager();
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

	// Token: 0x17000D42 RID: 3394
	// (get) Token: 0x060044F6 RID: 17654 RVA: 0x00101D8C File Offset: 0x000FFF8C
	// (set) Token: 0x060044F7 RID: 17655 RVA: 0x00101D94 File Offset: 0x000FFF94
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

	// Token: 0x17000D43 RID: 3395
	// (get) Token: 0x060044F8 RID: 17656 RVA: 0x00101DE8 File Offset: 0x000FFFE8
	// (set) Token: 0x060044F9 RID: 17657 RVA: 0x00101DF0 File Offset: 0x000FFFF0
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

	// Token: 0x17000D44 RID: 3396
	// (get) Token: 0x060044FA RID: 17658 RVA: 0x00101E44 File Offset: 0x00100044
	public int SelectionLength
	{
		get
		{
			return this.selectionEnd - this.selectionStart;
		}
	}

	// Token: 0x17000D45 RID: 3397
	// (get) Token: 0x060044FB RID: 17659 RVA: 0x00101E54 File Offset: 0x00100054
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

	// Token: 0x17000D46 RID: 3398
	// (get) Token: 0x060044FC RID: 17660 RVA: 0x00101E8C File Offset: 0x0010008C
	// (set) Token: 0x060044FD RID: 17661 RVA: 0x00101E94 File Offset: 0x00100094
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

	// Token: 0x17000D47 RID: 3399
	// (get) Token: 0x060044FE RID: 17662 RVA: 0x00101EA0 File Offset: 0x001000A0
	// (set) Token: 0x060044FF RID: 17663 RVA: 0x00101EC0 File Offset: 0x001000C0
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

	// Token: 0x17000D48 RID: 3400
	// (get) Token: 0x06004500 RID: 17664 RVA: 0x00101EF4 File Offset: 0x001000F4
	// (set) Token: 0x06004501 RID: 17665 RVA: 0x00101EFC File Offset: 0x001000FC
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

	// Token: 0x17000D49 RID: 3401
	// (get) Token: 0x06004502 RID: 17666 RVA: 0x00101F18 File Offset: 0x00100118
	// (set) Token: 0x06004503 RID: 17667 RVA: 0x00101F20 File Offset: 0x00100120
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

	// Token: 0x17000D4A RID: 3402
	// (get) Token: 0x06004504 RID: 17668 RVA: 0x00101F68 File Offset: 0x00100168
	// (set) Token: 0x06004505 RID: 17669 RVA: 0x00101F70 File Offset: 0x00100170
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

	// Token: 0x17000D4B RID: 3403
	// (get) Token: 0x06004506 RID: 17670 RVA: 0x00101F7C File Offset: 0x0010017C
	// (set) Token: 0x06004507 RID: 17671 RVA: 0x00101F84 File Offset: 0x00100184
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

	// Token: 0x17000D4C RID: 3404
	// (get) Token: 0x06004508 RID: 17672 RVA: 0x00101F90 File Offset: 0x00100190
	// (set) Token: 0x06004509 RID: 17673 RVA: 0x00101F98 File Offset: 0x00100198
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

	// Token: 0x17000D4D RID: 3405
	// (get) Token: 0x0600450A RID: 17674 RVA: 0x00101FC0 File Offset: 0x001001C0
	// (set) Token: 0x0600450B RID: 17675 RVA: 0x00101FC8 File Offset: 0x001001C8
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

	// Token: 0x17000D4E RID: 3406
	// (get) Token: 0x0600450C RID: 17676 RVA: 0x00101FEC File Offset: 0x001001EC
	// (set) Token: 0x0600450D RID: 17677 RVA: 0x00101FF4 File Offset: 0x001001F4
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

	// Token: 0x17000D4F RID: 3407
	// (get) Token: 0x0600450E RID: 17678 RVA: 0x00102068 File Offset: 0x00100268
	// (set) Token: 0x0600450F RID: 17679 RVA: 0x00102070 File Offset: 0x00100270
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

	// Token: 0x17000D50 RID: 3408
	// (get) Token: 0x06004510 RID: 17680 RVA: 0x00102080 File Offset: 0x00100280
	// (set) Token: 0x06004511 RID: 17681 RVA: 0x00102088 File Offset: 0x00100288
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

	// Token: 0x17000D51 RID: 3409
	// (get) Token: 0x06004512 RID: 17682 RVA: 0x001020A8 File Offset: 0x001002A8
	// (set) Token: 0x06004513 RID: 17683 RVA: 0x001020B0 File Offset: 0x001002B0
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

	// Token: 0x17000D52 RID: 3410
	// (get) Token: 0x06004514 RID: 17684 RVA: 0x001020C0 File Offset: 0x001002C0
	// (set) Token: 0x06004515 RID: 17685 RVA: 0x001020C8 File Offset: 0x001002C8
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

	// Token: 0x17000D53 RID: 3411
	// (get) Token: 0x06004516 RID: 17686 RVA: 0x001020F8 File Offset: 0x001002F8
	// (set) Token: 0x06004517 RID: 17687 RVA: 0x00102100 File Offset: 0x00100300
	public global::dfTextScaleMode TextScaleMode
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

	// Token: 0x17000D54 RID: 3412
	// (get) Token: 0x06004518 RID: 17688 RVA: 0x00102110 File Offset: 0x00100310
	// (set) Token: 0x06004519 RID: 17689 RVA: 0x00102118 File Offset: 0x00100318
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

	// Token: 0x17000D55 RID: 3413
	// (get) Token: 0x0600451A RID: 17690 RVA: 0x00102174 File Offset: 0x00100374
	// (set) Token: 0x0600451B RID: 17691 RVA: 0x0010217C File Offset: 0x0010037C
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

	// Token: 0x17000D56 RID: 3414
	// (get) Token: 0x0600451C RID: 17692 RVA: 0x00102198 File Offset: 0x00100398
	// (set) Token: 0x0600451D RID: 17693 RVA: 0x001021A0 File Offset: 0x001003A0
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

	// Token: 0x17000D57 RID: 3415
	// (get) Token: 0x0600451E RID: 17694 RVA: 0x001021BC File Offset: 0x001003BC
	// (set) Token: 0x0600451F RID: 17695 RVA: 0x001021C4 File Offset: 0x001003C4
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

	// Token: 0x17000D58 RID: 3416
	// (get) Token: 0x06004520 RID: 17696 RVA: 0x001021FC File Offset: 0x001003FC
	// (set) Token: 0x06004521 RID: 17697 RVA: 0x00102204 File Offset: 0x00100404
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

	// Token: 0x17000D59 RID: 3417
	// (get) Token: 0x06004522 RID: 17698 RVA: 0x00102224 File Offset: 0x00100424
	// (set) Token: 0x06004523 RID: 17699 RVA: 0x0010222C File Offset: 0x0010042C
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

	// Token: 0x17000D5A RID: 3418
	// (get) Token: 0x06004524 RID: 17700 RVA: 0x00102238 File Offset: 0x00100438
	// (set) Token: 0x06004525 RID: 17701 RVA: 0x00102240 File Offset: 0x00100440
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

	// Token: 0x17000D5B RID: 3419
	// (get) Token: 0x06004526 RID: 17702 RVA: 0x0010224C File Offset: 0x0010044C
	// (set) Token: 0x06004527 RID: 17703 RVA: 0x00102254 File Offset: 0x00100454
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

	// Token: 0x17000D5C RID: 3420
	// (get) Token: 0x06004528 RID: 17704 RVA: 0x00102260 File Offset: 0x00100460
	// (set) Token: 0x06004529 RID: 17705 RVA: 0x00102268 File Offset: 0x00100468
	public global::dfMobileKeyboardTrigger MobileKeyboardTrigger
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

	// Token: 0x0600452A RID: 17706 RVA: 0x00102274 File Offset: 0x00100474
	protected override void OnTabKeyPressed(global::dfKeyEventArgs args)
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

	// Token: 0x0600452B RID: 17707 RVA: 0x001022BC File Offset: 0x001004BC
	protected internal override void OnKeyPress(global::dfKeyEventArgs args)
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

	// Token: 0x0600452C RID: 17708 RVA: 0x00102308 File Offset: 0x00100508
	private void processKeyPress(global::dfKeyEventArgs args)
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

	// Token: 0x0600452D RID: 17709 RVA: 0x001023B0 File Offset: 0x001005B0
	protected internal override void OnKeyDown(global::dfKeyEventArgs args)
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
				string clipBoard = global::dfClipboardHelper.clipBoard;
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
						string clipBoard2 = global::dfClipboardHelper.clipBoard;
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

	// Token: 0x0600452E RID: 17710 RVA: 0x00102660 File Offset: 0x00100860
	private void selectAll()
	{
		this.selectionStart = 0;
		this.selectionEnd = this.text.Length;
		this.scrollIndex = 0;
		this.setCursorPos(0);
	}

	// Token: 0x0600452F RID: 17711 RVA: 0x00102694 File Offset: 0x00100894
	private void cutSelectionToClipboard()
	{
		this.copySelectionToClipboard();
		this.deleteSelection();
	}

	// Token: 0x06004530 RID: 17712 RVA: 0x001026A4 File Offset: 0x001008A4
	private void copySelectionToClipboard()
	{
		if (this.selectionStart == this.selectionEnd)
		{
			return;
		}
		global::dfClipboardHelper.clipBoard = this.text.Substring(this.selectionStart, this.selectionEnd - this.selectionStart);
	}

	// Token: 0x06004531 RID: 17713 RVA: 0x001026DC File Offset: 0x001008DC
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

	// Token: 0x06004532 RID: 17714 RVA: 0x00102790 File Offset: 0x00100990
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

	// Token: 0x06004533 RID: 17715 RVA: 0x001028A4 File Offset: 0x00100AA4
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

	// Token: 0x06004534 RID: 17716 RVA: 0x001028E4 File Offset: 0x00100AE4
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

	// Token: 0x06004535 RID: 17717 RVA: 0x00102918 File Offset: 0x00100B18
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

	// Token: 0x06004536 RID: 17718 RVA: 0x00102998 File Offset: 0x00100B98
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

	// Token: 0x06004537 RID: 17719 RVA: 0x00102A00 File Offset: 0x00100C00
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

	// Token: 0x06004538 RID: 17720 RVA: 0x00102A64 File Offset: 0x00100C64
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

	// Token: 0x06004539 RID: 17721 RVA: 0x00102ABC File Offset: 0x00100CBC
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

	// Token: 0x0600453A RID: 17722 RVA: 0x00102B38 File Offset: 0x00100D38
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

	// Token: 0x0600453B RID: 17723 RVA: 0x00102BA0 File Offset: 0x00100DA0
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

	// Token: 0x0600453C RID: 17724 RVA: 0x00102C28 File Offset: 0x00100E28
	private void moveToEnd()
	{
		this.ClearSelection();
		this.setCursorPos(this.text.Length);
	}

	// Token: 0x0600453D RID: 17725 RVA: 0x00102C44 File Offset: 0x00100E44
	private void moveToStart()
	{
		this.ClearSelection();
		this.setCursorPos(0);
	}

	// Token: 0x0600453E RID: 17726 RVA: 0x00102C54 File Offset: 0x00100E54
	private void moveToNextChar()
	{
		this.ClearSelection();
		this.setCursorPos(this.cursorIndex + 1);
	}

	// Token: 0x0600453F RID: 17727 RVA: 0x00102C6C File Offset: 0x00100E6C
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

	// Token: 0x06004540 RID: 17728 RVA: 0x00102D04 File Offset: 0x00100F04
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

	// Token: 0x06004541 RID: 17729 RVA: 0x00102D90 File Offset: 0x00100F90
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

	// Token: 0x06004542 RID: 17730 RVA: 0x00102E38 File Offset: 0x00101038
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

	// Token: 0x06004543 RID: 17731 RVA: 0x00102ED4 File Offset: 0x001010D4
	private void moveToPreviousChar()
	{
		this.ClearSelection();
		this.setCursorPos(this.cursorIndex - 1);
	}

	// Token: 0x06004544 RID: 17732 RVA: 0x00102EEC File Offset: 0x001010EC
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

	// Token: 0x06004545 RID: 17733 RVA: 0x00102F50 File Offset: 0x00101150
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

	// Token: 0x06004546 RID: 17734 RVA: 0x00103000 File Offset: 0x00101200
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

	// Token: 0x06004547 RID: 17735 RVA: 0x001030B0 File Offset: 0x001012B0
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

	// Token: 0x06004548 RID: 17736 RVA: 0x0010315C File Offset: 0x0010135C
	public override void Awake()
	{
		base.Awake();
		this.startSize = base.Size;
	}

	// Token: 0x06004549 RID: 17737 RVA: 0x00103170 File Offset: 0x00101370
	protected internal override void OnEnterFocus(global::dfFocusEventArgs args)
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

	// Token: 0x0600454A RID: 17738 RVA: 0x001031F4 File Offset: 0x001013F4
	protected internal override void OnLeaveFocus(global::dfFocusEventArgs args)
	{
		base.OnLeaveFocus(args);
		this.cursorShown = false;
		this.ClearSelection();
		this.Invalidate();
		this.whenGotFocus = 0f;
	}

	// Token: 0x0600454B RID: 17739 RVA: 0x0010321C File Offset: 0x0010141C
	protected internal override void OnDoubleClick(global::dfMouseEventArgs args)
	{
		if (args.Source != this)
		{
			base.OnDoubleClick(args);
			return;
		}
		if (!this.ReadOnly && this.HasFocus && args.Buttons.IsSet(global::dfMouseButtons.Left) && Time.realtimeSinceStartup - this.whenGotFocus > 0.5f)
		{
			int charIndexOfMouse = this.getCharIndexOfMouse(args);
			this.selectWordAtIndex(charIndexOfMouse);
		}
		base.OnDoubleClick(args);
	}

	// Token: 0x0600454C RID: 17740 RVA: 0x00103298 File Offset: 0x00101498
	protected internal override void OnMouseDown(global::dfMouseEventArgs args)
	{
		if (args.Source != this)
		{
			base.OnMouseDown(args);
			return;
		}
		bool flag = !this.ReadOnly && args.Buttons.IsSet(global::dfMouseButtons.Left) && ((!this.HasFocus && !this.SelectOnFocus) || Time.realtimeSinceStartup - this.whenGotFocus > 0.25f);
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

	// Token: 0x0600454D RID: 17741 RVA: 0x00103368 File Offset: 0x00101568
	protected internal override void OnMouseMove(global::dfMouseEventArgs args)
	{
		if (args.Source != this)
		{
			base.OnMouseMove(args);
			return;
		}
		if (!this.ReadOnly && this.HasFocus && args.Buttons.IsSet(global::dfMouseButtons.Left))
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

	// Token: 0x0600454E RID: 17742 RVA: 0x00103410 File Offset: 0x00101610
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

	// Token: 0x0600454F RID: 17743 RVA: 0x00103458 File Offset: 0x00101658
	protected internal virtual void OnReadOnlyChanged()
	{
		if (this.ReadOnlyChanged != null)
		{
			this.ReadOnlyChanged(this, this.readOnly);
		}
	}

	// Token: 0x06004550 RID: 17744 RVA: 0x00103478 File Offset: 0x00101678
	protected internal virtual void OnPasswordCharacterChanged()
	{
		if (this.PasswordCharacterChanged != null)
		{
			this.PasswordCharacterChanged(this, this.passwordChar);
		}
	}

	// Token: 0x06004551 RID: 17745 RVA: 0x00103498 File Offset: 0x00101698
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

	// Token: 0x06004552 RID: 17746 RVA: 0x001034E4 File Offset: 0x001016E4
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

	// Token: 0x06004553 RID: 17747 RVA: 0x0010353C File Offset: 0x0010173C
	public void ClearSelection()
	{
		this.selectionStart = 0;
		this.selectionEnd = 0;
		this.mouseSelectionAnchor = 0;
	}

	// Token: 0x06004554 RID: 17748 RVA: 0x00103554 File Offset: 0x00101754
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

	// Token: 0x06004555 RID: 17749 RVA: 0x00103570 File Offset: 0x00101770
	private void renderText(global::dfRenderData textBuffer)
	{
		float num = base.PixelsToUnits();
		Vector2 vector;
		vector..ctor(this.size.x - (float)this.padding.horizontal, this.size.y - (float)this.padding.vertical);
		Vector3 vector2 = this.pivot.TransformToUpperLeft(base.Size);
		Vector3 vectorOffset = new Vector3(vector2.x + (float)this.padding.left, vector2.y - (float)this.padding.top, 0f) * num;
		string text = (!this.IsPasswordField || string.IsNullOrEmpty(this.passwordChar)) ? this.text : this.passwordDisplayText();
		Color32 color = (!base.IsEnabled) ? base.DisabledColor : this.TextColor;
		float textScaleMultiplier = this.getTextScaleMultiplier();
		using (global::dfFontRendererBase dfFontRendererBase = this.font.ObtainRenderer())
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

	// Token: 0x06004556 RID: 17750 RVA: 0x001039B4 File Offset: 0x00101BB4
	private float getTextScaleMultiplier()
	{
		if (this.textScaleMode == global::dfTextScaleMode.None || !Application.isPlaying)
		{
			return 1f;
		}
		if (this.textScaleMode == global::dfTextScaleMode.ScreenResolution)
		{
			return (float)Screen.height / (float)this.manager.FixedHeight;
		}
		return base.Size.y / this.startSize.y;
	}

	// Token: 0x06004557 RID: 17751 RVA: 0x00103A18 File Offset: 0x00101C18
	private string passwordDisplayText()
	{
		return new string(this.passwordChar[0], this.text.Length);
	}

	// Token: 0x06004558 RID: 17752 RVA: 0x00103A38 File Offset: 0x00101C38
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
		global::dfAtlas.ItemInfo itemInfo = base.Atlas[this.SelectionSprite];
		Rect region = itemInfo.region;
		float num14 = region.width / itemInfo.sizeInPixels.x;
		float num15 = region.height / itemInfo.sizeInPixels.y;
		this.renderData.UV.Add(new Vector2(region.x + num14, region.yMax - num15));
		this.renderData.UV.Add(new Vector2(region.xMax - num14, region.yMax - num15));
		this.renderData.UV.Add(new Vector2(region.xMax - num14, region.y + num15));
		this.renderData.UV.Add(new Vector2(region.x + num14, region.y + num15));
	}

	// Token: 0x06004559 RID: 17753 RVA: 0x00103DC8 File Offset: 0x00101FC8
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
		global::dfList<Vector3> vertices = this.renderData.Vertices;
		global::dfList<int> triangles = this.renderData.Triangles;
		global::dfList<Vector2> uv = this.renderData.UV;
		global::dfList<Color32> colors = this.renderData.Colors;
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
		global::dfAtlas.ItemInfo itemInfo = base.Atlas[this.SelectionSprite];
		Rect region = itemInfo.region;
		uv.Add(new Vector2(region.x, region.yMax));
		uv.Add(new Vector2(region.xMax, region.yMax));
		uv.Add(new Vector2(region.xMax, region.y));
		uv.Add(new Vector2(region.x, region.y));
	}

	// Token: 0x0600455A RID: 17754 RVA: 0x00103FF0 File Offset: 0x001021F0
	private void addQuadIndices(global::dfList<Vector3> verts, global::dfList<int> triangles)
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

	// Token: 0x0600455B RID: 17755 RVA: 0x00104038 File Offset: 0x00102238
	private int getCharIndexOfMouse(global::dfMouseEventArgs args)
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

	// Token: 0x0600455C RID: 17756 RVA: 0x001040A8 File Offset: 0x001022A8
	public global::dfList<global::dfRenderData> RenderMultiple()
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
			this.renderData = global::dfRenderData.Obtain();
			this.textRenderData = global::dfRenderData.Obtain();
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

	// Token: 0x04002459 RID: 9305
	[SerializeField]
	protected global::dfFontBase font;

	// Token: 0x0400245A RID: 9306
	[SerializeField]
	protected bool acceptsTab;

	// Token: 0x0400245B RID: 9307
	[SerializeField]
	protected bool displayAsPassword;

	// Token: 0x0400245C RID: 9308
	[SerializeField]
	protected string passwordChar = "*";

	// Token: 0x0400245D RID: 9309
	[SerializeField]
	protected bool readOnly;

	// Token: 0x0400245E RID: 9310
	[SerializeField]
	protected string text = string.Empty;

	// Token: 0x0400245F RID: 9311
	[SerializeField]
	protected Color32 textColor = UnityEngine.Color.white;

	// Token: 0x04002460 RID: 9312
	[SerializeField]
	protected Color32 selectionBackground = new Color32(0, 105, 210, byte.MaxValue);

	// Token: 0x04002461 RID: 9313
	[SerializeField]
	protected string selectionSprite = string.Empty;

	// Token: 0x04002462 RID: 9314
	[SerializeField]
	protected float textScale = 1f;

	// Token: 0x04002463 RID: 9315
	[SerializeField]
	protected global::dfTextScaleMode textScaleMode;

	// Token: 0x04002464 RID: 9316
	[SerializeField]
	protected RectOffset padding = new RectOffset();

	// Token: 0x04002465 RID: 9317
	[SerializeField]
	protected float cursorBlinkTime = 0.45f;

	// Token: 0x04002466 RID: 9318
	[SerializeField]
	protected int cursorWidth = 1;

	// Token: 0x04002467 RID: 9319
	[SerializeField]
	protected int maxLength = 1024;

	// Token: 0x04002468 RID: 9320
	[SerializeField]
	protected bool selectOnFocus;

	// Token: 0x04002469 RID: 9321
	[SerializeField]
	protected bool shadow;

	// Token: 0x0400246A RID: 9322
	[SerializeField]
	protected Color32 shadowColor = UnityEngine.Color.black;

	// Token: 0x0400246B RID: 9323
	[SerializeField]
	protected Vector2 shadowOffset = new Vector2(1f, -1f);

	// Token: 0x0400246C RID: 9324
	[SerializeField]
	protected bool useMobileKeyboard;

	// Token: 0x0400246D RID: 9325
	[SerializeField]
	protected int mobileKeyboardType;

	// Token: 0x0400246E RID: 9326
	[SerializeField]
	protected bool mobileAutoCorrect;

	// Token: 0x0400246F RID: 9327
	[SerializeField]
	protected bool mobileHideInputField;

	// Token: 0x04002470 RID: 9328
	[SerializeField]
	protected global::dfMobileKeyboardTrigger mobileKeyboardTrigger;

	// Token: 0x04002471 RID: 9329
	[SerializeField]
	protected TextAlignment textAlign;

	// Token: 0x04002472 RID: 9330
	private Vector2 startSize = Vector2.zero;

	// Token: 0x04002473 RID: 9331
	private int selectionStart;

	// Token: 0x04002474 RID: 9332
	private int selectionEnd;

	// Token: 0x04002475 RID: 9333
	private int mouseSelectionAnchor;

	// Token: 0x04002476 RID: 9334
	private int scrollIndex;

	// Token: 0x04002477 RID: 9335
	private int cursorIndex;

	// Token: 0x04002478 RID: 9336
	private float leftOffset;

	// Token: 0x04002479 RID: 9337
	private bool cursorShown;

	// Token: 0x0400247A RID: 9338
	private float[] charWidths;

	// Token: 0x0400247B RID: 9339
	private float whenGotFocus;

	// Token: 0x0400247C RID: 9340
	private string undoText = string.Empty;

	// Token: 0x0400247D RID: 9341
	private global::dfRenderData textRenderData;

	// Token: 0x0400247E RID: 9342
	private global::dfList<global::dfRenderData> buffers = global::dfList<global::dfRenderData>.Obtain();
}
