using System;
using System.Collections;
using System.Reflection;
using NGUIHack;
using UnityEngine;

// Token: 0x0200080E RID: 2062
[AddComponentMenu("")]
public class UIUnityEvents : MonoBehaviour
{
	// Token: 0x060049ED RID: 18925 RVA: 0x0013B460 File Offset: 0x00139660
	public static void CameraCreated(UICamera camera)
	{
		if (Application.isPlaying && !UIUnityEvents.LateLoaded.singleton)
		{
			Debug.Log("singleton check failed.");
		}
	}

	// Token: 0x060049EE RID: 18926 RVA: 0x0013B488 File Offset: 0x00139688
	private void Awake()
	{
		base.useGUILayout = false;
	}

	// Token: 0x060049EF RID: 18927 RVA: 0x0013B494 File Offset: 0x00139694
	private void OnDestroy()
	{
		if (UIUnityEvents.madeSingleton && UIUnityEvents.LateLoaded.singleton == this)
		{
			UIUnityEvents.LateLoaded.singleton = null;
		}
	}

	// Token: 0x060049F0 RID: 18928 RVA: 0x0013B4C4 File Offset: 0x001396C4
	private static bool PerformOperation(TextEditor te, UIUnityEvents.TextEditOp operation)
	{
		switch (operation)
		{
		case UIUnityEvents.TextEditOp.MoveLeft:
			te.MoveLeft();
			return false;
		case UIUnityEvents.TextEditOp.MoveRight:
			te.MoveRight();
			return false;
		case UIUnityEvents.TextEditOp.MoveUp:
			te.MoveUp();
			return false;
		case UIUnityEvents.TextEditOp.MoveDown:
			te.MoveDown();
			return false;
		case UIUnityEvents.TextEditOp.MoveLineStart:
			te.MoveLineStart();
			return false;
		case UIUnityEvents.TextEditOp.MoveLineEnd:
			te.MoveLineEnd();
			return false;
		case UIUnityEvents.TextEditOp.MoveTextStart:
			te.MoveTextStart();
			return false;
		case UIUnityEvents.TextEditOp.MoveTextEnd:
			te.MoveTextEnd();
			return false;
		case UIUnityEvents.TextEditOp.MoveGraphicalLineStart:
			te.MoveGraphicalLineStart();
			return false;
		case UIUnityEvents.TextEditOp.MoveGraphicalLineEnd:
			te.MoveGraphicalLineEnd();
			return false;
		case UIUnityEvents.TextEditOp.MoveWordLeft:
			te.MoveWordLeft();
			return false;
		case UIUnityEvents.TextEditOp.MoveWordRight:
			te.MoveWordRight();
			return false;
		case UIUnityEvents.TextEditOp.MoveParagraphForward:
			te.MoveParagraphForward();
			return false;
		case UIUnityEvents.TextEditOp.MoveParagraphBackward:
			te.MoveParagraphBackward();
			return false;
		case UIUnityEvents.TextEditOp.MoveToStartOfNextWord:
			te.MoveToStartOfNextWord();
			return false;
		case UIUnityEvents.TextEditOp.MoveToEndOfPreviousWord:
			te.MoveToEndOfPreviousWord();
			return false;
		case UIUnityEvents.TextEditOp.SelectLeft:
			te.SelectLeft();
			return false;
		case UIUnityEvents.TextEditOp.SelectRight:
			te.SelectRight();
			return false;
		case UIUnityEvents.TextEditOp.SelectUp:
			te.SelectUp();
			return false;
		case UIUnityEvents.TextEditOp.SelectDown:
			te.SelectDown();
			return false;
		case UIUnityEvents.TextEditOp.SelectTextStart:
			te.SelectTextStart();
			return false;
		case UIUnityEvents.TextEditOp.SelectTextEnd:
			te.SelectTextEnd();
			return false;
		case UIUnityEvents.TextEditOp.ExpandSelectGraphicalLineStart:
			te.ExpandSelectGraphicalLineStart();
			return false;
		case UIUnityEvents.TextEditOp.ExpandSelectGraphicalLineEnd:
			te.ExpandSelectGraphicalLineEnd();
			return false;
		case UIUnityEvents.TextEditOp.SelectGraphicalLineStart:
			te.SelectGraphicalLineStart();
			return false;
		case UIUnityEvents.TextEditOp.SelectGraphicalLineEnd:
			te.SelectGraphicalLineEnd();
			return false;
		case UIUnityEvents.TextEditOp.SelectWordLeft:
			te.SelectWordLeft();
			return false;
		case UIUnityEvents.TextEditOp.SelectWordRight:
			te.SelectWordRight();
			return false;
		case UIUnityEvents.TextEditOp.SelectToEndOfPreviousWord:
			te.SelectToEndOfPreviousWord();
			return false;
		case UIUnityEvents.TextEditOp.SelectToStartOfNextWord:
			te.SelectToStartOfNextWord();
			return false;
		case UIUnityEvents.TextEditOp.SelectParagraphBackward:
			te.SelectParagraphBackward();
			return false;
		case UIUnityEvents.TextEditOp.SelectParagraphForward:
			te.SelectParagraphForward();
			return false;
		case UIUnityEvents.TextEditOp.Delete:
			return te.Delete();
		case UIUnityEvents.TextEditOp.Backspace:
			return te.Backspace();
		case UIUnityEvents.TextEditOp.DeleteWordBack:
			return te.DeleteWordBack();
		case UIUnityEvents.TextEditOp.DeleteWordForward:
			return te.DeleteWordForward();
		case UIUnityEvents.TextEditOp.Cut:
			return te.Cut();
		case UIUnityEvents.TextEditOp.Copy:
			te.Copy();
			return false;
		case UIUnityEvents.TextEditOp.Paste:
			return te.Paste();
		case UIUnityEvents.TextEditOp.SelectAll:
			te.SelectAll();
			return false;
		case UIUnityEvents.TextEditOp.SelectNone:
			te.SelectNone();
			return false;
		}
		Debug.Log("Unimplemented: " + operation);
		return false;
	}

	// Token: 0x060049F1 RID: 18929 RVA: 0x0013B758 File Offset: 0x00139958
	private static bool Perform(TextEditor te, UIUnityEvents.TextEditOp operation)
	{
		return UIUnityEvents.PerformOperation(te, operation);
	}

	// Token: 0x060049F2 RID: 18930 RVA: 0x0013B770 File Offset: 0x00139970
	private static bool GetTextEditor(out TextEditor te)
	{
		UIUnityEvents.submit = false;
		if (!UIUnityEvents.focusSetInOnGUI && UIUnityEvents.requiresBinding && UIUnityEvents.lastInput && UIUnityEvents.lastInputCamera)
		{
			GUI.FocusControl("ngui-unityevents");
		}
		UIUnityEvents.Bind();
		te = (GUIUtility.GetStateObject(typeof(TextEditor), UIUnityEvents.controlID) as TextEditor);
		if (UIUnityEvents.lastInput)
		{
			GUIContent guicontent;
			if ((guicontent = UIUnityEvents.textInputContent) == null)
			{
				guicontent = (UIUnityEvents.textInputContent = new GUIContent());
			}
			guicontent.text = UIUnityEvents.lastInput.inputText;
			te.content.text = UIUnityEvents.textInputContent.text;
			te.SaveBackup();
			te.position = UIUnityEvents.idRect;
			te.style = UIUnityEvents.textStyle;
			te.multiline = UIUnityEvents.lastInput.inputMultiline;
			te.controlID = UIUnityEvents.controlID;
			te.ClampPos();
			return true;
		}
		te = null;
		return false;
	}

	// Token: 0x060049F3 RID: 18931 RVA: 0x0013B874 File Offset: 0x00139A74
	private static bool SetKeyboardControl()
	{
		GUIUtility.keyboardControl = UIUnityEvents.controlID;
		return GUIUtility.keyboardControl == UIUnityEvents.controlID;
	}

	// Token: 0x060049F4 RID: 18932 RVA: 0x0013B88C File Offset: 0x00139A8C
	private static bool GetKeyboardControl()
	{
		int keyboardControl = GUIUtility.keyboardControl;
		return keyboardControl == UIUnityEvents.controlID;
	}

	// Token: 0x17000E85 RID: 3717
	// (get) Token: 0x060049F5 RID: 18933 RVA: 0x0013B8B0 File Offset: 0x00139AB0
	private static GUIStyle textStyle
	{
		get
		{
			return GUI.skin.textField;
		}
	}

	// Token: 0x060049F6 RID: 18934 RVA: 0x0013B8BC File Offset: 0x00139ABC
	private static bool TextEditorHandleEvent2(Event e, TextEditor te)
	{
		if (UIUnityEvents.LateLoaded.Keyactions.Contains(e))
		{
			UIUnityEvents.Perform(te, (UIUnityEvents.TextEditOp)Convert.ToInt32(UIUnityEvents.LateLoaded.Keyactions[e]));
			return true;
		}
		return false;
	}

	// Token: 0x060049F7 RID: 18935 RVA: 0x0013B8F4 File Offset: 0x00139AF4
	private static bool TextEditorHandleEvent(Event e, TextEditor te)
	{
		EventModifiers modifiers = e.modifiers;
		if ((modifiers & 32) == 32)
		{
			try
			{
				e.modifiers = (modifiers & -33);
				return UIUnityEvents.TextEditorHandleEvent2(e, te);
			}
			finally
			{
				e.modifiers = modifiers;
			}
		}
		return UIUnityEvents.TextEditorHandleEvent2(e, te);
	}

	// Token: 0x060049F8 RID: 18936 RVA: 0x0013B95C File Offset: 0x00139B5C
	private static void TextSharedEnd(bool changed, TextEditor te, Event @event)
	{
		if (UIUnityEvents.GetKeyboardControl())
		{
			UIUnityEvents.LateLoaded.textFieldInput = true;
		}
		if (changed || @event.type == 12)
		{
			if (UIUnityEvents.lastInput)
			{
				UIUnityEvents.textInputContent.text = te.content.text;
			}
			if (changed)
			{
				GUI.changed = true;
				UIUnityEvents.lastInput.CheckChanges(UIUnityEvents.textInputContent.text);
				UIUnityEvents.lastInput.CheckPositioning(te.pos, te.selectPos);
				@event.Use();
			}
			else
			{
				UIUnityEvents.lastInput.CheckPositioning(te.pos, te.selectPos);
			}
		}
		if (UIUnityEvents.submit)
		{
			UIUnityEvents.submit = false;
			if (UIUnityEvents.lastInput.SendSubmitMessage())
			{
				@event.Use();
			}
		}
	}

	// Token: 0x060049F9 RID: 18937 RVA: 0x0013BA30 File Offset: 0x00139C30
	private static bool MoveTextPosition(Event @event, TextEditor te, ref UITextPosition res)
	{
		UIUnityEvents.lastTextPosition = res;
		if (res.valid)
		{
			te.pos = res.uniformPosition;
			if (!@event.shift)
			{
				te.selectPos = te.pos;
			}
			return true;
		}
		return false;
	}

	// Token: 0x060049FA RID: 18938 RVA: 0x0013BA7C File Offset: 0x00139C7C
	private static bool SelectTextPosition(Event @event, TextEditor te, ref UITextPosition res)
	{
		UIUnityEvents.lastTextPosition = res;
		if (res.valid)
		{
			UIUnityEvents.lastCursorPosition = UIUnityEvents.textStyle.GetCursorPixelPosition(UIUnityEvents.idRect, UIUnityEvents.textInputContent, res.uniformPosition);
			te.SelectToPosition(UIUnityEvents.lastCursorPosition);
			return true;
		}
		return false;
	}

	// Token: 0x060049FB RID: 18939 RVA: 0x0013BACC File Offset: 0x00139CCC
	internal static void TextGainFocus(UIInput input)
	{
	}

	// Token: 0x060049FC RID: 18940 RVA: 0x0013BAD0 File Offset: 0x00139CD0
	internal static void TextLostFocus(UIInput input)
	{
		if (input == UIUnityEvents.lastInput)
		{
			if (UIUnityEvents.lastInputCamera && UICamera.selectedObject == input)
			{
				UICamera.selectedObject = null;
			}
			UIUnityEvents.lastInput = null;
			UIUnityEvents.lastInputCamera = null;
			UIUnityEvents.lastLabel = null;
		}
	}

	// Token: 0x060049FD RID: 18941 RVA: 0x0013BB24 File Offset: 0x00139D24
	internal static void TextClickDown(UICamera camera, UIInput input, Event @event, UILabel label)
	{
		UIUnityEvents.TextClickDown(camera, input, @event.real, label);
	}

	// Token: 0x060049FE RID: 18942 RVA: 0x0013BB34 File Offset: 0x00139D34
	private static void ChangeFocus(UICamera camera, UIInput input, UILabel label)
	{
		bool flag = UIUnityEvents.lastInput != input;
		if (flag)
		{
			UIUnityEvents.lastInput = input;
			UIUnityEvents.textInputContent = null;
			UIUnityEvents.requiresBinding = input;
			UIUnityEvents.focusSetInOnGUI = UIUnityEvents.inOnGUI;
		}
		UIUnityEvents.lastInputCamera = camera;
		UIUnityEvents.lastLabel = label;
	}

	// Token: 0x060049FF RID: 18943 RVA: 0x0013BB80 File Offset: 0x00139D80
	private static void Bind()
	{
		if (UIUnityEvents.requiresBinding && UIUnityEvents.lastInput && UIUnityEvents.lastInputCamera)
		{
			UIUnityEvents.SetKeyboardControl();
			UIUnityEvents.requiresBinding = false;
			UIUnityEvents.focusSetInOnGUI = true;
		}
	}

	// Token: 0x06004A00 RID: 18944 RVA: 0x0013BBC8 File Offset: 0x00139DC8
	private static void TextClickDown(UICamera camera, UIInput input, Event @event, UILabel label)
	{
		UITextPosition uitextPosition = (!@event.shift) ? camera.RaycastText(Input.mousePosition, label) : default(UITextPosition);
		TextEditor textEditor = null;
		UIUnityEvents.ChangeFocus(camera, input, label);
		if (!UIUnityEvents.GetTextEditor(out textEditor))
		{
			Debug.LogError("Null Text Editor");
		}
		else
		{
			GUIUtility.hotControl = UIUnityEvents.controlID;
			UIUnityEvents.SetKeyboardControl();
			UIUnityEvents.MoveTextPosition(@event, textEditor, ref uitextPosition);
			int clickCount = @event.clickCount;
			if (clickCount != 2)
			{
				if (clickCount == 3)
				{
					if (input.trippleClickSelect)
					{
						textEditor.SelectCurrentParagraph();
						textEditor.MouseDragSelectsWholeWords(true);
						textEditor.DblClickSnap(1);
					}
				}
			}
			else
			{
				textEditor.SelectCurrentWord();
				textEditor.DblClickSnap(0);
				textEditor.MouseDragSelectsWholeWords(true);
			}
			@event.Use();
		}
		UIUnityEvents.TextSharedEnd(false, textEditor, @event);
	}

	// Token: 0x06004A01 RID: 18945 RVA: 0x0013BCA0 File Offset: 0x00139EA0
	internal static void TextClickUp(UICamera camera, UIInput input, Event @event, UILabel label)
	{
		UIUnityEvents.TextClickUp(camera, input, @event.real, label);
	}

	// Token: 0x06004A02 RID: 18946 RVA: 0x0013BCB0 File Offset: 0x00139EB0
	private static void TextClickUp(UICamera camera, UIInput input, Event @event, UILabel label)
	{
		if (input == UIUnityEvents.lastInput && camera == UIUnityEvents.lastInputCamera)
		{
			UIUnityEvents.lastLabel = label;
			TextEditor textEditor = null;
			if (!UIUnityEvents.GetTextEditor(out textEditor))
			{
				return;
			}
			if (UIUnityEvents.controlID == GUIUtility.hotControl)
			{
				textEditor.MouseDragSelectsWholeWords(false);
				GUIUtility.hotControl = 0;
				@event.Use();
				UIUnityEvents.SetKeyboardControl();
			}
			else
			{
				Debug.Log(string.Concat(new object[]
				{
					"Did not match ",
					UIUnityEvents.controlID,
					" ",
					GUIUtility.hotControl
				}));
			}
			UIUnityEvents.TextSharedEnd(false, textEditor, @event);
		}
	}

	// Token: 0x06004A03 RID: 18947 RVA: 0x0013BD64 File Offset: 0x00139F64
	internal static void TextDrag(UICamera camera, UIInput input, Event @event, UILabel label)
	{
		UIUnityEvents.TextDrag(camera, input, @event.real, label);
	}

	// Token: 0x06004A04 RID: 18948 RVA: 0x0013BD74 File Offset: 0x00139F74
	private static void TextDrag(UICamera camera, UIInput input, Event @event, UILabel label)
	{
		if (input == UIUnityEvents.lastInput && camera == UIUnityEvents.lastInputCamera)
		{
			UIUnityEvents.lastLabel = label;
			TextEditor te = null;
			if (!UIUnityEvents.GetTextEditor(out te))
			{
				return;
			}
			if (UIUnityEvents.controlID == GUIUtility.hotControl)
			{
				UITextPosition uitextPosition = camera.RaycastText(Input.mousePosition, label);
				if (!@event.shift)
				{
					UIUnityEvents.SelectTextPosition(@event, te, ref uitextPosition);
				}
				else
				{
					UIUnityEvents.MoveTextPosition(@event, te, ref uitextPosition);
				}
				@event.Use();
			}
			UIUnityEvents.TextSharedEnd(false, te, @event);
		}
	}

	// Token: 0x06004A05 RID: 18949 RVA: 0x0013BE04 File Offset: 0x0013A004
	internal static void TextKeyUp(UICamera camera, UIInput input, Event @event, UILabel label)
	{
		UIUnityEvents.TextKeyUp(camera, input, @event.real, label);
	}

	// Token: 0x06004A06 RID: 18950 RVA: 0x0013BE14 File Offset: 0x0013A014
	private static void TextKeyUp(UICamera camera, UIInput input, Event @event, UILabel label)
	{
		if (input == UIUnityEvents.lastInput && camera == UIUnityEvents.lastInputCamera)
		{
			UIUnityEvents.lastLabel = label;
			TextEditor te = null;
			if (!UIUnityEvents.GetTextEditor(out te))
			{
				return;
			}
			UIUnityEvents.TextSharedEnd(false, te, @event);
		}
	}

	// Token: 0x06004A07 RID: 18951 RVA: 0x0013BE60 File Offset: 0x0013A060
	internal static void TextKeyDown(UICamera camera, UIInput input, Event @event, UILabel label)
	{
		UIUnityEvents.TextKeyDown(camera, input, @event.real, label);
	}

	// Token: 0x06004A08 RID: 18952 RVA: 0x0013BE70 File Offset: 0x0013A070
	private static void TextKeyDown(UICamera camera, UIInput input, Event @event, UILabel label)
	{
		if (input == UIUnityEvents.lastInput && camera == UIUnityEvents.lastInputCamera)
		{
			UIUnityEvents.lastLabel = label;
			TextEditor textEditor = null;
			if (!UIUnityEvents.GetTextEditor(out textEditor))
			{
				return;
			}
			if (!UIUnityEvents.GetKeyboardControl())
			{
				Debug.Log("Did not " + @event);
				return;
			}
			bool changed = false;
			if (UIUnityEvents.TextEditorHandleEvent(@event, textEditor))
			{
				@event.Use();
				changed = true;
			}
			else
			{
				KeyCode keyCode = @event.keyCode;
				if (keyCode == 9)
				{
					return;
				}
				if (keyCode == null)
				{
					char character = @event.character;
					if (character == '\t')
					{
						return;
					}
					bool flag = character == '\n';
					BMFont bmFont;
					if (flag && !input.inputMultiline && !@event.alt)
					{
						UIUnityEvents.submit = true;
					}
					else if (label.font && (bmFont = label.font.bmFont) != null)
					{
						if (flag || (character != '\0' && bmFont.ContainsGlyph((int)character)))
						{
							textEditor.Insert(character);
							changed = true;
						}
						else if (character == '\0')
						{
							if (Input.compositionString.Length > 0)
							{
								textEditor.ReplaceSelection(string.Empty);
								changed = true;
							}
							@event.Use();
						}
					}
				}
			}
			UIUnityEvents.TextSharedEnd(changed, textEditor, @event);
		}
	}

	// Token: 0x06004A09 RID: 18953 RVA: 0x0013BFBC File Offset: 0x0013A1BC
	internal static bool RequestKeyboardFocus(UIInput input)
	{
		if (input == UIUnityEvents.lastInput)
		{
			return true;
		}
		if (UIUnityEvents.lastInput)
		{
			return false;
		}
		if (!input.label || !input.label.enabled)
		{
			return false;
		}
		int layer = input.label.gameObject.layer;
		UICamera uicamera = UICamera.FindCameraForLayer(layer);
		if (!uicamera)
		{
			return false;
		}
		if (uicamera.SetKeyboardFocus(input))
		{
			UIUnityEvents.ChangeFocus(uicamera, input, input.label);
			return true;
		}
		return false;
	}

	// Token: 0x17000E86 RID: 3718
	// (get) Token: 0x06004A0A RID: 18954 RVA: 0x0013C050 File Offset: 0x0013A250
	public static bool shouldBlockButtonInput
	{
		get
		{
			return UIUnityEvents.lastInput;
		}
	}

	// Token: 0x06004A0B RID: 18955 RVA: 0x0013C05C File Offset: 0x0013A25C
	private void OnGUI()
	{
		try
		{
			UIUnityEvents.inOnGUI = true;
			GUI.depth = 49;
			UIUnityEvents.blankID = GUIUtility.GetControlID(1);
			GUI.SetNextControlName("ngui-unityevents");
			UIUnityEvents.controlID = GUIUtility.GetControlID(1);
			GUI.color = Color.clear;
			Event current = Event.current;
			EventType type = current.type;
			if (type == 2)
			{
				Debug.Log("Mouse Move");
			}
			switch (type)
			{
			case 0:
				if (!UIUnityEvents.forbidHandlingNewEvents)
				{
					bool flag = current.button == 0;
					using (Event @event = new Event(current))
					{
						UICamera.HandleEvent(@event, type);
					}
					if (flag && current.type == 12 && GUIUtility.hotControl == 0)
					{
						GUIUtility.hotControl = UIUnityEvents.blankID;
					}
				}
				break;
			case 1:
			{
				bool flag2 = current.button == 0;
				using (Event event2 = new Event(current))
				{
					UICamera.HandleEvent(event2, type);
				}
				if (flag2 && GUIUtility.hotControl == UIUnityEvents.blankID)
				{
					GUIUtility.hotControl = 0;
				}
				break;
			}
			case 2:
			case 3:
			case 5:
			case 6:
				using (Event event3 = new Event(current))
				{
					UICamera.HandleEvent(event3, type);
				}
				break;
			case 4:
				if (!UIUnityEvents.forbidHandlingNewEvents)
				{
					using (Event event4 = new Event(current))
					{
						UICamera.HandleEvent(event4, type);
					}
				}
				break;
			case 7:
				if (!UIUnityEvents.forbidHandlingNewEvents && UIUnityEvents.lastMousePosition != current.mousePosition)
				{
					UIUnityEvents.lastMousePosition = current.mousePosition;
					using (Event event5 = new Event(current, 2))
					{
						UICamera.HandleEvent(event5, 2);
					}
				}
				break;
			case 12:
				Debug.Log("Used");
				return;
			}
			if (type == 7)
			{
			}
		}
		finally
		{
			UIUnityEvents.inOnGUI = false;
		}
	}

	// Token: 0x040029D8 RID: 10712
	private const int idLoop = 300;

	// Token: 0x040029D9 RID: 10713
	private const int controlIDHint = 320323492;

	// Token: 0x040029DA RID: 10714
	private const string kControlName = "ngui-unityevents";

	// Token: 0x040029DB RID: 10715
	private const int kGUIDepth = 49;

	// Token: 0x040029DC RID: 10716
	public static bool forbidHandlingNewEvents;

	// Token: 0x040029DD RID: 10717
	private UIInput mInput;

	// Token: 0x040029DE RID: 10718
	private UICamera mCamera;

	// Token: 0x040029DF RID: 10719
	private static bool madeSingleton;

	// Token: 0x040029E0 RID: 10720
	private static readonly Rect idRect = new Rect(0f, 0f, 69999f, 69999f);

	// Token: 0x040029E1 RID: 10721
	private static int controlID;

	// Token: 0x040029E2 RID: 10722
	private static UIInput lastInput;

	// Token: 0x040029E3 RID: 10723
	private static UILabel lastLabel;

	// Token: 0x040029E4 RID: 10724
	private static UICamera lastInputCamera;

	// Token: 0x040029E5 RID: 10725
	private static bool submit;

	// Token: 0x040029E6 RID: 10726
	private static GUIContent textInputContent = null;

	// Token: 0x040029E7 RID: 10727
	private static Vector2 lastCursorPosition;

	// Token: 0x040029E8 RID: 10728
	private static UITextPosition lastTextPosition;

	// Token: 0x040029E9 RID: 10729
	private static bool requiresBinding;

	// Token: 0x040029EA RID: 10730
	private static bool focusSetInOnGUI;

	// Token: 0x040029EB RID: 10731
	private static Vector2 lastMousePosition = new Vector2(-100f, -100f);

	// Token: 0x040029EC RID: 10732
	private static int blankID;

	// Token: 0x040029ED RID: 10733
	private static bool inOnGUI;

	// Token: 0x0200080F RID: 2063
	private static class LateLoaded
	{
		// Token: 0x06004A0C RID: 18956 RVA: 0x0013C31C File Offset: 0x0013A51C
		static LateLoaded()
		{
			UIUnityEvents.LateLoaded.mTextBlockStyle.alignment = 0;
			UIUnityEvents.LateLoaded.mTextBlockStyle.border = new RectOffset(0, 0, 0, 0);
			UIUnityEvents.LateLoaded.mTextBlockStyle.clipping = 0;
			UIUnityEvents.LateLoaded.mTextBlockStyle.contentOffset = default(Vector2);
			UIUnityEvents.LateLoaded.mTextBlockStyle.fixedWidth = -1f;
			UIUnityEvents.LateLoaded.mTextBlockStyle.fixedHeight = -1f;
			UIUnityEvents.LateLoaded.mTextBlockStyle.imagePosition = 3;
			UIUnityEvents.LateLoaded.mTextBlockStyle.margin = new RectOffset(0, 0, 0, 0);
			UIUnityEvents.LateLoaded.mTextBlockStyle.name = "BLOCK STYLE";
			UIUnityEvents.LateLoaded.mTextBlockStyle.overflow = new RectOffset(0, 0, 0, 0);
			UIUnityEvents.LateLoaded.mTextBlockStyle.padding = new RectOffset(0, 0, 0, 0);
			UIUnityEvents.LateLoaded.mTextBlockStyle.stretchHeight = false;
			UIUnityEvents.LateLoaded.mTextBlockStyle.stretchWidth = false;
			UIUnityEvents.LateLoaded.mTextBlockStyle.wordWrap = false;
			GUIStyleState guistyleState = new GUIStyleState();
			guistyleState.background = null;
			guistyleState.textColor = Color.clear;
			UIUnityEvents.LateLoaded.mTextBlockStyle.active = guistyleState;
			UIUnityEvents.LateLoaded.mTextBlockStyle.focused = guistyleState;
			UIUnityEvents.LateLoaded.mTextBlockStyle.hover = guistyleState;
			UIUnityEvents.LateLoaded.mTextBlockStyle.normal = guistyleState;
			UIUnityEvents.LateLoaded.mTextBlockStyle.onActive = guistyleState;
			UIUnityEvents.LateLoaded.mTextBlockStyle.onFocused = guistyleState;
			UIUnityEvents.LateLoaded.mTextBlockStyle.onHover = guistyleState;
			UIUnityEvents.LateLoaded.mTextBlockStyle.onNormal = guistyleState;
			UIUnityEvents.LateLoaded._textFieldInput = typeof(GUIUtility).GetProperty("textFieldInput", BindingFlags.Static | BindingFlags.NonPublic);
			if (UIUnityEvents.LateLoaded._textFieldInput == null)
			{
				UIUnityEvents.LateLoaded.failedInvokeTextFieldInputGet = true;
				UIUnityEvents.LateLoaded.failedInvokeTextFieldInputSet = true;
				Debug.LogError("Unity has changed. no bool property textFieldInput in GUIUtility");
			}
			GameObject gameObject = new GameObject("__UIUnityEvents", new Type[]
			{
				typeof(UIUnityEvents)
			});
			UIUnityEvents.LateLoaded.singleton = gameObject.GetComponent<UIUnityEvents>();
			UIUnityEvents.madeSingleton = true;
			Object.DontDestroyOnLoad(gameObject);
			TextEditor textEditor = null;
			if (textEditor != null)
			{
				Debug.Log("Thats imposible.");
			}
			try
			{
				MethodInfo method = typeof(TextEditor).GetMethod("InitKeyActions", BindingFlags.Instance | BindingFlags.NonPublic);
				if (method == null)
				{
					throw new MethodAccessException("Unity has changed. no InitKeyActions member in TextEditor");
				}
				method.Invoke(new TextEditor(), new object[0]);
				object obj = typeof(TextEditor).InvokeMember("s_Keyactions", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.GetField, null, null, new object[0]);
				if (obj is Hashtable)
				{
					UIUnityEvents.LateLoaded.Keyactions = (Hashtable)obj;
				}
				else
				{
					if (!(obj is IDictionary))
					{
						throw new MethodAccessException("Unity has changed. no s_Keyactions member in TextEditor");
					}
					UIUnityEvents.LateLoaded.Keyactions = new Hashtable(obj as IDictionary);
				}
			}
			catch (MethodAccessException arg)
			{
				Debug.Log("Caught exception \r\n" + arg + "\r\nManually building keyactions.");
				UIUnityEvents.LateLoaded.Keyactions = new Hashtable();
				UIUnityEvents.LateLoaded.MapKey("left", UIUnityEvents.TextEditOp.MoveLeft);
				UIUnityEvents.LateLoaded.MapKey("right", UIUnityEvents.TextEditOp.MoveRight);
				UIUnityEvents.LateLoaded.MapKey("up", UIUnityEvents.TextEditOp.MoveUp);
				UIUnityEvents.LateLoaded.MapKey("down", UIUnityEvents.TextEditOp.MoveDown);
				UIUnityEvents.LateLoaded.MapKey("#left", UIUnityEvents.TextEditOp.SelectLeft);
				UIUnityEvents.LateLoaded.MapKey("#right", UIUnityEvents.TextEditOp.SelectRight);
				UIUnityEvents.LateLoaded.MapKey("#up", UIUnityEvents.TextEditOp.SelectUp);
				UIUnityEvents.LateLoaded.MapKey("#down", UIUnityEvents.TextEditOp.SelectDown);
				UIUnityEvents.LateLoaded.MapKey("delete", UIUnityEvents.TextEditOp.Delete);
				UIUnityEvents.LateLoaded.MapKey("backspace", UIUnityEvents.TextEditOp.Backspace);
				UIUnityEvents.LateLoaded.MapKey("#backspace", UIUnityEvents.TextEditOp.Backspace);
				if (Application.platform != 2 && Application.platform != 5 && Application.platform != 7)
				{
					UIUnityEvents.LateLoaded.MapKey("^left", UIUnityEvents.TextEditOp.MoveGraphicalLineStart);
					UIUnityEvents.LateLoaded.MapKey("^right", UIUnityEvents.TextEditOp.MoveGraphicalLineEnd);
					UIUnityEvents.LateLoaded.MapKey("&left", UIUnityEvents.TextEditOp.MoveWordLeft);
					UIUnityEvents.LateLoaded.MapKey("&right", UIUnityEvents.TextEditOp.MoveWordRight);
					UIUnityEvents.LateLoaded.MapKey("&up", UIUnityEvents.TextEditOp.MoveParagraphBackward);
					UIUnityEvents.LateLoaded.MapKey("&down", UIUnityEvents.TextEditOp.MoveParagraphForward);
					UIUnityEvents.LateLoaded.MapKey("%left", UIUnityEvents.TextEditOp.MoveGraphicalLineStart);
					UIUnityEvents.LateLoaded.MapKey("%right", UIUnityEvents.TextEditOp.MoveGraphicalLineEnd);
					UIUnityEvents.LateLoaded.MapKey("%up", UIUnityEvents.TextEditOp.MoveTextStart);
					UIUnityEvents.LateLoaded.MapKey("%down", UIUnityEvents.TextEditOp.MoveTextEnd);
					UIUnityEvents.LateLoaded.MapKey("#home", UIUnityEvents.TextEditOp.SelectTextStart);
					UIUnityEvents.LateLoaded.MapKey("#end", UIUnityEvents.TextEditOp.SelectTextEnd);
					UIUnityEvents.LateLoaded.MapKey("#^left", UIUnityEvents.TextEditOp.ExpandSelectGraphicalLineStart);
					UIUnityEvents.LateLoaded.MapKey("#^right", UIUnityEvents.TextEditOp.ExpandSelectGraphicalLineEnd);
					UIUnityEvents.LateLoaded.MapKey("#^up", UIUnityEvents.TextEditOp.SelectParagraphBackward);
					UIUnityEvents.LateLoaded.MapKey("#^down", UIUnityEvents.TextEditOp.SelectParagraphForward);
					UIUnityEvents.LateLoaded.MapKey("#&left", UIUnityEvents.TextEditOp.SelectWordLeft);
					UIUnityEvents.LateLoaded.MapKey("#&right", UIUnityEvents.TextEditOp.SelectWordRight);
					UIUnityEvents.LateLoaded.MapKey("#&up", UIUnityEvents.TextEditOp.SelectParagraphBackward);
					UIUnityEvents.LateLoaded.MapKey("#&down", UIUnityEvents.TextEditOp.SelectParagraphForward);
					UIUnityEvents.LateLoaded.MapKey("#%left", UIUnityEvents.TextEditOp.ExpandSelectGraphicalLineStart);
					UIUnityEvents.LateLoaded.MapKey("#%right", UIUnityEvents.TextEditOp.ExpandSelectGraphicalLineEnd);
					UIUnityEvents.LateLoaded.MapKey("#%up", UIUnityEvents.TextEditOp.SelectTextStart);
					UIUnityEvents.LateLoaded.MapKey("#%down", UIUnityEvents.TextEditOp.SelectTextEnd);
					UIUnityEvents.LateLoaded.MapKey("%a", UIUnityEvents.TextEditOp.SelectAll);
					UIUnityEvents.LateLoaded.MapKey("%x", UIUnityEvents.TextEditOp.Cut);
					UIUnityEvents.LateLoaded.MapKey("%c", UIUnityEvents.TextEditOp.Copy);
					UIUnityEvents.LateLoaded.MapKey("%v", UIUnityEvents.TextEditOp.Paste);
					UIUnityEvents.LateLoaded.MapKey("^d", UIUnityEvents.TextEditOp.Delete);
					UIUnityEvents.LateLoaded.MapKey("^h", UIUnityEvents.TextEditOp.Backspace);
					UIUnityEvents.LateLoaded.MapKey("^b", UIUnityEvents.TextEditOp.MoveLeft);
					UIUnityEvents.LateLoaded.MapKey("^f", UIUnityEvents.TextEditOp.MoveRight);
					UIUnityEvents.LateLoaded.MapKey("^a", UIUnityEvents.TextEditOp.MoveLineStart);
					UIUnityEvents.LateLoaded.MapKey("^e", UIUnityEvents.TextEditOp.MoveLineEnd);
					UIUnityEvents.LateLoaded.MapKey("&delete", UIUnityEvents.TextEditOp.DeleteWordForward);
					UIUnityEvents.LateLoaded.MapKey("&backspace", UIUnityEvents.TextEditOp.DeleteWordBack);
				}
				else
				{
					UIUnityEvents.LateLoaded.MapKey("home", UIUnityEvents.TextEditOp.MoveGraphicalLineStart);
					UIUnityEvents.LateLoaded.MapKey("end", UIUnityEvents.TextEditOp.MoveGraphicalLineEnd);
					UIUnityEvents.LateLoaded.MapKey("%left", UIUnityEvents.TextEditOp.MoveWordLeft);
					UIUnityEvents.LateLoaded.MapKey("%right", UIUnityEvents.TextEditOp.MoveWordRight);
					UIUnityEvents.LateLoaded.MapKey("%up", UIUnityEvents.TextEditOp.MoveParagraphBackward);
					UIUnityEvents.LateLoaded.MapKey("%down", UIUnityEvents.TextEditOp.MoveParagraphForward);
					UIUnityEvents.LateLoaded.MapKey("^left", UIUnityEvents.TextEditOp.MoveToEndOfPreviousWord);
					UIUnityEvents.LateLoaded.MapKey("^right", UIUnityEvents.TextEditOp.MoveToStartOfNextWord);
					UIUnityEvents.LateLoaded.MapKey("^up", UIUnityEvents.TextEditOp.MoveParagraphBackward);
					UIUnityEvents.LateLoaded.MapKey("^down", UIUnityEvents.TextEditOp.MoveParagraphForward);
					UIUnityEvents.LateLoaded.MapKey("#^left", UIUnityEvents.TextEditOp.SelectToEndOfPreviousWord);
					UIUnityEvents.LateLoaded.MapKey("#^right", UIUnityEvents.TextEditOp.SelectToStartOfNextWord);
					UIUnityEvents.LateLoaded.MapKey("#^up", UIUnityEvents.TextEditOp.SelectParagraphBackward);
					UIUnityEvents.LateLoaded.MapKey("#^down", UIUnityEvents.TextEditOp.SelectParagraphForward);
					UIUnityEvents.LateLoaded.MapKey("#home", UIUnityEvents.TextEditOp.SelectGraphicalLineStart);
					UIUnityEvents.LateLoaded.MapKey("#end", UIUnityEvents.TextEditOp.SelectGraphicalLineEnd);
					UIUnityEvents.LateLoaded.MapKey("^delete", UIUnityEvents.TextEditOp.DeleteWordForward);
					UIUnityEvents.LateLoaded.MapKey("^backspace", UIUnityEvents.TextEditOp.DeleteWordBack);
					UIUnityEvents.LateLoaded.MapKey("^a", UIUnityEvents.TextEditOp.SelectAll);
					UIUnityEvents.LateLoaded.MapKey("^x", UIUnityEvents.TextEditOp.Cut);
					UIUnityEvents.LateLoaded.MapKey("^c", UIUnityEvents.TextEditOp.Copy);
					UIUnityEvents.LateLoaded.MapKey("^v", UIUnityEvents.TextEditOp.Paste);
					UIUnityEvents.LateLoaded.MapKey("#delete", UIUnityEvents.TextEditOp.Cut);
					UIUnityEvents.LateLoaded.MapKey("^insert", UIUnityEvents.TextEditOp.Copy);
					UIUnityEvents.LateLoaded.MapKey("#insert", UIUnityEvents.TextEditOp.Paste);
				}
			}
		}

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x06004A0D RID: 18957 RVA: 0x0013C960 File Offset: 0x0013AB60
		// (set) Token: 0x06004A0E RID: 18958 RVA: 0x0013C9D0 File Offset: 0x0013ABD0
		public static bool textFieldInput
		{
			get
			{
				if (!UIUnityEvents.LateLoaded.failedInvokeTextFieldInputGet)
				{
					try
					{
						return (bool)UIUnityEvents.LateLoaded._textFieldInput.GetValue(null, null);
					}
					catch (MethodAccessException arg)
					{
						UIUnityEvents.LateLoaded.failedInvokeTextFieldInputGet = true;
						Debug.Log("Can not get GUIUtility.textFieldInput\r\n" + arg);
					}
					return false;
				}
				return false;
			}
			set
			{
				if (!UIUnityEvents.LateLoaded.failedInvokeTextFieldInputSet)
				{
					try
					{
						UIUnityEvents.LateLoaded._textFieldInput.SetValue(null, value, null);
					}
					catch (MethodAccessException arg)
					{
						UIUnityEvents.LateLoaded.failedInvokeTextFieldInputSet = true;
						Debug.Log("Can not set GUIUtility.textFieldInput\r\n" + arg);
					}
				}
			}
		}

		// Token: 0x06004A0F RID: 18959 RVA: 0x0013CA38 File Offset: 0x0013AC38
		private static void MapKey(string key, UIUnityEvents.TextEditOp action)
		{
			UIUnityEvents.LateLoaded.Keyactions[Event.KeyboardEvent(key)] = action;
		}

		// Token: 0x040029EE RID: 10734
		public static readonly GUIStyle mTextBlockStyle = new GUIStyle();

		// Token: 0x040029EF RID: 10735
		private static readonly PropertyInfo _textFieldInput;

		// Token: 0x040029F0 RID: 10736
		public static UIUnityEvents singleton;

		// Token: 0x040029F1 RID: 10737
		public static Hashtable Keyactions;

		// Token: 0x040029F2 RID: 10738
		private static bool failedInvokeTextFieldInputGet;

		// Token: 0x040029F3 RID: 10739
		private static bool failedInvokeTextFieldInputSet;
	}

	// Token: 0x02000810 RID: 2064
	private enum TextEditOp
	{
		// Token: 0x040029F5 RID: 10741
		MoveLeft,
		// Token: 0x040029F6 RID: 10742
		MoveRight,
		// Token: 0x040029F7 RID: 10743
		MoveUp,
		// Token: 0x040029F8 RID: 10744
		MoveDown,
		// Token: 0x040029F9 RID: 10745
		MoveLineStart,
		// Token: 0x040029FA RID: 10746
		MoveLineEnd,
		// Token: 0x040029FB RID: 10747
		MoveTextStart,
		// Token: 0x040029FC RID: 10748
		MoveTextEnd,
		// Token: 0x040029FD RID: 10749
		MovePageUp,
		// Token: 0x040029FE RID: 10750
		MovePageDown,
		// Token: 0x040029FF RID: 10751
		MoveGraphicalLineStart,
		// Token: 0x04002A00 RID: 10752
		MoveGraphicalLineEnd,
		// Token: 0x04002A01 RID: 10753
		MoveWordLeft,
		// Token: 0x04002A02 RID: 10754
		MoveWordRight,
		// Token: 0x04002A03 RID: 10755
		MoveParagraphForward,
		// Token: 0x04002A04 RID: 10756
		MoveParagraphBackward,
		// Token: 0x04002A05 RID: 10757
		MoveToStartOfNextWord,
		// Token: 0x04002A06 RID: 10758
		MoveToEndOfPreviousWord,
		// Token: 0x04002A07 RID: 10759
		SelectLeft,
		// Token: 0x04002A08 RID: 10760
		SelectRight,
		// Token: 0x04002A09 RID: 10761
		SelectUp,
		// Token: 0x04002A0A RID: 10762
		SelectDown,
		// Token: 0x04002A0B RID: 10763
		SelectTextStart,
		// Token: 0x04002A0C RID: 10764
		SelectTextEnd,
		// Token: 0x04002A0D RID: 10765
		SelectPageUp,
		// Token: 0x04002A0E RID: 10766
		SelectPageDown,
		// Token: 0x04002A0F RID: 10767
		ExpandSelectGraphicalLineStart,
		// Token: 0x04002A10 RID: 10768
		ExpandSelectGraphicalLineEnd,
		// Token: 0x04002A11 RID: 10769
		SelectGraphicalLineStart,
		// Token: 0x04002A12 RID: 10770
		SelectGraphicalLineEnd,
		// Token: 0x04002A13 RID: 10771
		SelectWordLeft,
		// Token: 0x04002A14 RID: 10772
		SelectWordRight,
		// Token: 0x04002A15 RID: 10773
		SelectToEndOfPreviousWord,
		// Token: 0x04002A16 RID: 10774
		SelectToStartOfNextWord,
		// Token: 0x04002A17 RID: 10775
		SelectParagraphBackward,
		// Token: 0x04002A18 RID: 10776
		SelectParagraphForward,
		// Token: 0x04002A19 RID: 10777
		Delete,
		// Token: 0x04002A1A RID: 10778
		Backspace,
		// Token: 0x04002A1B RID: 10779
		DeleteWordBack,
		// Token: 0x04002A1C RID: 10780
		DeleteWordForward,
		// Token: 0x04002A1D RID: 10781
		Cut,
		// Token: 0x04002A1E RID: 10782
		Copy,
		// Token: 0x04002A1F RID: 10783
		Paste,
		// Token: 0x04002A20 RID: 10784
		SelectAll,
		// Token: 0x04002A21 RID: 10785
		SelectNone,
		// Token: 0x04002A22 RID: 10786
		ScrollStart,
		// Token: 0x04002A23 RID: 10787
		ScrollEnd,
		// Token: 0x04002A24 RID: 10788
		ScrollPageUp,
		// Token: 0x04002A25 RID: 10789
		ScrollPageDown
	}
}
