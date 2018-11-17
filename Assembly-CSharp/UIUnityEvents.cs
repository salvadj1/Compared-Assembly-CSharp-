using System;
using System.Collections;
using System.Reflection;
using NGUIHack;
using UnityEngine;

// Token: 0x02000900 RID: 2304
[AddComponentMenu("")]
public class UIUnityEvents : MonoBehaviour
{
	// Token: 0x06004E9C RID: 20124 RVA: 0x001453C4 File Offset: 0x001435C4
	public static void CameraCreated(global::UICamera camera)
	{
		if (Application.isPlaying && !global::UIUnityEvents.LateLoaded.singleton)
		{
			Debug.Log("singleton check failed.");
		}
	}

	// Token: 0x06004E9D RID: 20125 RVA: 0x001453EC File Offset: 0x001435EC
	private void Awake()
	{
		base.useGUILayout = false;
	}

	// Token: 0x06004E9E RID: 20126 RVA: 0x001453F8 File Offset: 0x001435F8
	private void OnDestroy()
	{
		if (global::UIUnityEvents.madeSingleton && global::UIUnityEvents.LateLoaded.singleton == this)
		{
			global::UIUnityEvents.LateLoaded.singleton = null;
		}
	}

	// Token: 0x06004E9F RID: 20127 RVA: 0x00145428 File Offset: 0x00143628
	private static bool PerformOperation(TextEditor te, global::UIUnityEvents.TextEditOp operation)
	{
		switch (operation)
		{
		case global::UIUnityEvents.TextEditOp.MoveLeft:
			te.MoveLeft();
			return false;
		case global::UIUnityEvents.TextEditOp.MoveRight:
			te.MoveRight();
			return false;
		case global::UIUnityEvents.TextEditOp.MoveUp:
			te.MoveUp();
			return false;
		case global::UIUnityEvents.TextEditOp.MoveDown:
			te.MoveDown();
			return false;
		case global::UIUnityEvents.TextEditOp.MoveLineStart:
			te.MoveLineStart();
			return false;
		case global::UIUnityEvents.TextEditOp.MoveLineEnd:
			te.MoveLineEnd();
			return false;
		case global::UIUnityEvents.TextEditOp.MoveTextStart:
			te.MoveTextStart();
			return false;
		case global::UIUnityEvents.TextEditOp.MoveTextEnd:
			te.MoveTextEnd();
			return false;
		case global::UIUnityEvents.TextEditOp.MoveGraphicalLineStart:
			te.MoveGraphicalLineStart();
			return false;
		case global::UIUnityEvents.TextEditOp.MoveGraphicalLineEnd:
			te.MoveGraphicalLineEnd();
			return false;
		case global::UIUnityEvents.TextEditOp.MoveWordLeft:
			te.MoveWordLeft();
			return false;
		case global::UIUnityEvents.TextEditOp.MoveWordRight:
			te.MoveWordRight();
			return false;
		case global::UIUnityEvents.TextEditOp.MoveParagraphForward:
			te.MoveParagraphForward();
			return false;
		case global::UIUnityEvents.TextEditOp.MoveParagraphBackward:
			te.MoveParagraphBackward();
			return false;
		case global::UIUnityEvents.TextEditOp.MoveToStartOfNextWord:
			te.MoveToStartOfNextWord();
			return false;
		case global::UIUnityEvents.TextEditOp.MoveToEndOfPreviousWord:
			te.MoveToEndOfPreviousWord();
			return false;
		case global::UIUnityEvents.TextEditOp.SelectLeft:
			te.SelectLeft();
			return false;
		case global::UIUnityEvents.TextEditOp.SelectRight:
			te.SelectRight();
			return false;
		case global::UIUnityEvents.TextEditOp.SelectUp:
			te.SelectUp();
			return false;
		case global::UIUnityEvents.TextEditOp.SelectDown:
			te.SelectDown();
			return false;
		case global::UIUnityEvents.TextEditOp.SelectTextStart:
			te.SelectTextStart();
			return false;
		case global::UIUnityEvents.TextEditOp.SelectTextEnd:
			te.SelectTextEnd();
			return false;
		case global::UIUnityEvents.TextEditOp.ExpandSelectGraphicalLineStart:
			te.ExpandSelectGraphicalLineStart();
			return false;
		case global::UIUnityEvents.TextEditOp.ExpandSelectGraphicalLineEnd:
			te.ExpandSelectGraphicalLineEnd();
			return false;
		case global::UIUnityEvents.TextEditOp.SelectGraphicalLineStart:
			te.SelectGraphicalLineStart();
			return false;
		case global::UIUnityEvents.TextEditOp.SelectGraphicalLineEnd:
			te.SelectGraphicalLineEnd();
			return false;
		case global::UIUnityEvents.TextEditOp.SelectWordLeft:
			te.SelectWordLeft();
			return false;
		case global::UIUnityEvents.TextEditOp.SelectWordRight:
			te.SelectWordRight();
			return false;
		case global::UIUnityEvents.TextEditOp.SelectToEndOfPreviousWord:
			te.SelectToEndOfPreviousWord();
			return false;
		case global::UIUnityEvents.TextEditOp.SelectToStartOfNextWord:
			te.SelectToStartOfNextWord();
			return false;
		case global::UIUnityEvents.TextEditOp.SelectParagraphBackward:
			te.SelectParagraphBackward();
			return false;
		case global::UIUnityEvents.TextEditOp.SelectParagraphForward:
			te.SelectParagraphForward();
			return false;
		case global::UIUnityEvents.TextEditOp.Delete:
			return te.Delete();
		case global::UIUnityEvents.TextEditOp.Backspace:
			return te.Backspace();
		case global::UIUnityEvents.TextEditOp.DeleteWordBack:
			return te.DeleteWordBack();
		case global::UIUnityEvents.TextEditOp.DeleteWordForward:
			return te.DeleteWordForward();
		case global::UIUnityEvents.TextEditOp.Cut:
			return te.Cut();
		case global::UIUnityEvents.TextEditOp.Copy:
			te.Copy();
			return false;
		case global::UIUnityEvents.TextEditOp.Paste:
			return te.Paste();
		case global::UIUnityEvents.TextEditOp.SelectAll:
			te.SelectAll();
			return false;
		case global::UIUnityEvents.TextEditOp.SelectNone:
			te.SelectNone();
			return false;
		}
		Debug.Log("Unimplemented: " + operation);
		return false;
	}

	// Token: 0x06004EA0 RID: 20128 RVA: 0x001456BC File Offset: 0x001438BC
	private static bool Perform(TextEditor te, global::UIUnityEvents.TextEditOp operation)
	{
		return global::UIUnityEvents.PerformOperation(te, operation);
	}

	// Token: 0x06004EA1 RID: 20129 RVA: 0x001456D4 File Offset: 0x001438D4
	private static bool GetTextEditor(out TextEditor te)
	{
		global::UIUnityEvents.submit = false;
		if (!global::UIUnityEvents.focusSetInOnGUI && global::UIUnityEvents.requiresBinding && global::UIUnityEvents.lastInput && global::UIUnityEvents.lastInputCamera)
		{
			GUI.FocusControl("ngui-unityevents");
		}
		global::UIUnityEvents.Bind();
		te = (GUIUtility.GetStateObject(typeof(TextEditor), global::UIUnityEvents.controlID) as TextEditor);
		if (global::UIUnityEvents.lastInput)
		{
			GUIContent guicontent;
			if ((guicontent = global::UIUnityEvents.textInputContent) == null)
			{
				guicontent = (global::UIUnityEvents.textInputContent = new GUIContent());
			}
			guicontent.text = global::UIUnityEvents.lastInput.inputText;
			te.content.text = global::UIUnityEvents.textInputContent.text;
			te.SaveBackup();
			te.position = global::UIUnityEvents.idRect;
			te.style = global::UIUnityEvents.textStyle;
			te.multiline = global::UIUnityEvents.lastInput.inputMultiline;
			te.controlID = global::UIUnityEvents.controlID;
			te.ClampPos();
			return true;
		}
		te = null;
		return false;
	}

	// Token: 0x06004EA2 RID: 20130 RVA: 0x001457D8 File Offset: 0x001439D8
	private static bool SetKeyboardControl()
	{
		GUIUtility.keyboardControl = global::UIUnityEvents.controlID;
		return GUIUtility.keyboardControl == global::UIUnityEvents.controlID;
	}

	// Token: 0x06004EA3 RID: 20131 RVA: 0x001457F0 File Offset: 0x001439F0
	private static bool GetKeyboardControl()
	{
		int keyboardControl = GUIUtility.keyboardControl;
		return keyboardControl == global::UIUnityEvents.controlID;
	}

	// Token: 0x17000F1F RID: 3871
	// (get) Token: 0x06004EA4 RID: 20132 RVA: 0x00145814 File Offset: 0x00143A14
	private static GUIStyle textStyle
	{
		get
		{
			return GUI.skin.textField;
		}
	}

	// Token: 0x06004EA5 RID: 20133 RVA: 0x00145820 File Offset: 0x00143A20
	private static bool TextEditorHandleEvent2(UnityEngine.Event e, TextEditor te)
	{
		if (global::UIUnityEvents.LateLoaded.Keyactions.Contains(e))
		{
			global::UIUnityEvents.Perform(te, (global::UIUnityEvents.TextEditOp)Convert.ToInt32(global::UIUnityEvents.LateLoaded.Keyactions[e]));
			return true;
		}
		return false;
	}

	// Token: 0x06004EA6 RID: 20134 RVA: 0x00145858 File Offset: 0x00143A58
	private static bool TextEditorHandleEvent(UnityEngine.Event e, TextEditor te)
	{
		EventModifiers modifiers = e.modifiers;
		if ((modifiers & 32) == 32)
		{
			try
			{
				e.modifiers = (modifiers & -33);
				return global::UIUnityEvents.TextEditorHandleEvent2(e, te);
			}
			finally
			{
				e.modifiers = modifiers;
			}
		}
		return global::UIUnityEvents.TextEditorHandleEvent2(e, te);
	}

	// Token: 0x06004EA7 RID: 20135 RVA: 0x001458C0 File Offset: 0x00143AC0
	private static void TextSharedEnd(bool changed, TextEditor te, UnityEngine.Event @event)
	{
		if (global::UIUnityEvents.GetKeyboardControl())
		{
			global::UIUnityEvents.LateLoaded.textFieldInput = true;
		}
		if (changed || @event.type == 12)
		{
			if (global::UIUnityEvents.lastInput)
			{
				global::UIUnityEvents.textInputContent.text = te.content.text;
			}
			if (changed)
			{
				GUI.changed = true;
				global::UIUnityEvents.lastInput.CheckChanges(global::UIUnityEvents.textInputContent.text);
				global::UIUnityEvents.lastInput.CheckPositioning(te.pos, te.selectPos);
				@event.Use();
			}
			else
			{
				global::UIUnityEvents.lastInput.CheckPositioning(te.pos, te.selectPos);
			}
		}
		if (global::UIUnityEvents.submit)
		{
			global::UIUnityEvents.submit = false;
			if (global::UIUnityEvents.lastInput.SendSubmitMessage())
			{
				@event.Use();
			}
		}
	}

	// Token: 0x06004EA8 RID: 20136 RVA: 0x00145994 File Offset: 0x00143B94
	private static bool MoveTextPosition(UnityEngine.Event @event, TextEditor te, ref global::UITextPosition res)
	{
		global::UIUnityEvents.lastTextPosition = res;
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

	// Token: 0x06004EA9 RID: 20137 RVA: 0x001459E0 File Offset: 0x00143BE0
	private static bool SelectTextPosition(UnityEngine.Event @event, TextEditor te, ref global::UITextPosition res)
	{
		global::UIUnityEvents.lastTextPosition = res;
		if (res.valid)
		{
			global::UIUnityEvents.lastCursorPosition = global::UIUnityEvents.textStyle.GetCursorPixelPosition(global::UIUnityEvents.idRect, global::UIUnityEvents.textInputContent, res.uniformPosition);
			te.SelectToPosition(global::UIUnityEvents.lastCursorPosition);
			return true;
		}
		return false;
	}

	// Token: 0x06004EAA RID: 20138 RVA: 0x00145A30 File Offset: 0x00143C30
	internal static void TextGainFocus(global::UIInput input)
	{
	}

	// Token: 0x06004EAB RID: 20139 RVA: 0x00145A34 File Offset: 0x00143C34
	internal static void TextLostFocus(global::UIInput input)
	{
		if (input == global::UIUnityEvents.lastInput)
		{
			if (global::UIUnityEvents.lastInputCamera && global::UICamera.selectedObject == input)
			{
				global::UICamera.selectedObject = null;
			}
			global::UIUnityEvents.lastInput = null;
			global::UIUnityEvents.lastInputCamera = null;
			global::UIUnityEvents.lastLabel = null;
		}
	}

	// Token: 0x06004EAC RID: 20140 RVA: 0x00145A88 File Offset: 0x00143C88
	internal static void TextClickDown(global::UICamera camera, global::UIInput input, NGUIHack.Event @event, global::UILabel label)
	{
		global::UIUnityEvents.TextClickDown(camera, input, @event.real, label);
	}

	// Token: 0x06004EAD RID: 20141 RVA: 0x00145A98 File Offset: 0x00143C98
	private static void ChangeFocus(global::UICamera camera, global::UIInput input, global::UILabel label)
	{
		bool flag = global::UIUnityEvents.lastInput != input;
		if (flag)
		{
			global::UIUnityEvents.lastInput = input;
			global::UIUnityEvents.textInputContent = null;
			global::UIUnityEvents.requiresBinding = input;
			global::UIUnityEvents.focusSetInOnGUI = global::UIUnityEvents.inOnGUI;
		}
		global::UIUnityEvents.lastInputCamera = camera;
		global::UIUnityEvents.lastLabel = label;
	}

	// Token: 0x06004EAE RID: 20142 RVA: 0x00145AE4 File Offset: 0x00143CE4
	private static void Bind()
	{
		if (global::UIUnityEvents.requiresBinding && global::UIUnityEvents.lastInput && global::UIUnityEvents.lastInputCamera)
		{
			global::UIUnityEvents.SetKeyboardControl();
			global::UIUnityEvents.requiresBinding = false;
			global::UIUnityEvents.focusSetInOnGUI = true;
		}
	}

	// Token: 0x06004EAF RID: 20143 RVA: 0x00145B2C File Offset: 0x00143D2C
	private static void TextClickDown(global::UICamera camera, global::UIInput input, UnityEngine.Event @event, global::UILabel label)
	{
		global::UITextPosition uitextPosition = (!@event.shift) ? camera.RaycastText(Input.mousePosition, label) : default(global::UITextPosition);
		TextEditor textEditor = null;
		global::UIUnityEvents.ChangeFocus(camera, input, label);
		if (!global::UIUnityEvents.GetTextEditor(out textEditor))
		{
			Debug.LogError("Null Text Editor");
		}
		else
		{
			GUIUtility.hotControl = global::UIUnityEvents.controlID;
			global::UIUnityEvents.SetKeyboardControl();
			global::UIUnityEvents.MoveTextPosition(@event, textEditor, ref uitextPosition);
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
		global::UIUnityEvents.TextSharedEnd(false, textEditor, @event);
	}

	// Token: 0x06004EB0 RID: 20144 RVA: 0x00145C04 File Offset: 0x00143E04
	internal static void TextClickUp(global::UICamera camera, global::UIInput input, NGUIHack.Event @event, global::UILabel label)
	{
		global::UIUnityEvents.TextClickUp(camera, input, @event.real, label);
	}

	// Token: 0x06004EB1 RID: 20145 RVA: 0x00145C14 File Offset: 0x00143E14
	private static void TextClickUp(global::UICamera camera, global::UIInput input, UnityEngine.Event @event, global::UILabel label)
	{
		if (input == global::UIUnityEvents.lastInput && camera == global::UIUnityEvents.lastInputCamera)
		{
			global::UIUnityEvents.lastLabel = label;
			TextEditor textEditor = null;
			if (!global::UIUnityEvents.GetTextEditor(out textEditor))
			{
				return;
			}
			if (global::UIUnityEvents.controlID == GUIUtility.hotControl)
			{
				textEditor.MouseDragSelectsWholeWords(false);
				GUIUtility.hotControl = 0;
				@event.Use();
				global::UIUnityEvents.SetKeyboardControl();
			}
			else
			{
				Debug.Log(string.Concat(new object[]
				{
					"Did not match ",
					global::UIUnityEvents.controlID,
					" ",
					GUIUtility.hotControl
				}));
			}
			global::UIUnityEvents.TextSharedEnd(false, textEditor, @event);
		}
	}

	// Token: 0x06004EB2 RID: 20146 RVA: 0x00145CC8 File Offset: 0x00143EC8
	internal static void TextDrag(global::UICamera camera, global::UIInput input, NGUIHack.Event @event, global::UILabel label)
	{
		global::UIUnityEvents.TextDrag(camera, input, @event.real, label);
	}

	// Token: 0x06004EB3 RID: 20147 RVA: 0x00145CD8 File Offset: 0x00143ED8
	private static void TextDrag(global::UICamera camera, global::UIInput input, UnityEngine.Event @event, global::UILabel label)
	{
		if (input == global::UIUnityEvents.lastInput && camera == global::UIUnityEvents.lastInputCamera)
		{
			global::UIUnityEvents.lastLabel = label;
			TextEditor te = null;
			if (!global::UIUnityEvents.GetTextEditor(out te))
			{
				return;
			}
			if (global::UIUnityEvents.controlID == GUIUtility.hotControl)
			{
				global::UITextPosition uitextPosition = camera.RaycastText(Input.mousePosition, label);
				if (!@event.shift)
				{
					global::UIUnityEvents.SelectTextPosition(@event, te, ref uitextPosition);
				}
				else
				{
					global::UIUnityEvents.MoveTextPosition(@event, te, ref uitextPosition);
				}
				@event.Use();
			}
			global::UIUnityEvents.TextSharedEnd(false, te, @event);
		}
	}

	// Token: 0x06004EB4 RID: 20148 RVA: 0x00145D68 File Offset: 0x00143F68
	internal static void TextKeyUp(global::UICamera camera, global::UIInput input, NGUIHack.Event @event, global::UILabel label)
	{
		global::UIUnityEvents.TextKeyUp(camera, input, @event.real, label);
	}

	// Token: 0x06004EB5 RID: 20149 RVA: 0x00145D78 File Offset: 0x00143F78
	private static void TextKeyUp(global::UICamera camera, global::UIInput input, UnityEngine.Event @event, global::UILabel label)
	{
		if (input == global::UIUnityEvents.lastInput && camera == global::UIUnityEvents.lastInputCamera)
		{
			global::UIUnityEvents.lastLabel = label;
			TextEditor te = null;
			if (!global::UIUnityEvents.GetTextEditor(out te))
			{
				return;
			}
			global::UIUnityEvents.TextSharedEnd(false, te, @event);
		}
	}

	// Token: 0x06004EB6 RID: 20150 RVA: 0x00145DC4 File Offset: 0x00143FC4
	internal static void TextKeyDown(global::UICamera camera, global::UIInput input, NGUIHack.Event @event, global::UILabel label)
	{
		global::UIUnityEvents.TextKeyDown(camera, input, @event.real, label);
	}

	// Token: 0x06004EB7 RID: 20151 RVA: 0x00145DD4 File Offset: 0x00143FD4
	private static void TextKeyDown(global::UICamera camera, global::UIInput input, UnityEngine.Event @event, global::UILabel label)
	{
		if (input == global::UIUnityEvents.lastInput && camera == global::UIUnityEvents.lastInputCamera)
		{
			global::UIUnityEvents.lastLabel = label;
			TextEditor textEditor = null;
			if (!global::UIUnityEvents.GetTextEditor(out textEditor))
			{
				return;
			}
			if (!global::UIUnityEvents.GetKeyboardControl())
			{
				Debug.Log("Did not " + @event);
				return;
			}
			bool changed = false;
			if (global::UIUnityEvents.TextEditorHandleEvent(@event, textEditor))
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
					global::BMFont bmFont;
					if (flag && !input.inputMultiline && !@event.alt)
					{
						global::UIUnityEvents.submit = true;
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
			global::UIUnityEvents.TextSharedEnd(changed, textEditor, @event);
		}
	}

	// Token: 0x06004EB8 RID: 20152 RVA: 0x00145F20 File Offset: 0x00144120
	internal static bool RequestKeyboardFocus(global::UIInput input)
	{
		if (input == global::UIUnityEvents.lastInput)
		{
			return true;
		}
		if (global::UIUnityEvents.lastInput)
		{
			return false;
		}
		if (!input.label || !input.label.enabled)
		{
			return false;
		}
		int layer = input.label.gameObject.layer;
		global::UICamera uicamera = global::UICamera.FindCameraForLayer(layer);
		if (!uicamera)
		{
			return false;
		}
		if (uicamera.SetKeyboardFocus(input))
		{
			global::UIUnityEvents.ChangeFocus(uicamera, input, input.label);
			return true;
		}
		return false;
	}

	// Token: 0x17000F20 RID: 3872
	// (get) Token: 0x06004EB9 RID: 20153 RVA: 0x00145FB4 File Offset: 0x001441B4
	public static bool shouldBlockButtonInput
	{
		get
		{
			return global::UIUnityEvents.lastInput;
		}
	}

	// Token: 0x06004EBA RID: 20154 RVA: 0x00145FC0 File Offset: 0x001441C0
	private void OnGUI()
	{
		try
		{
			global::UIUnityEvents.inOnGUI = true;
			GUI.depth = 49;
			global::UIUnityEvents.blankID = GUIUtility.GetControlID(1);
			GUI.SetNextControlName("ngui-unityevents");
			global::UIUnityEvents.controlID = GUIUtility.GetControlID(1);
			GUI.color = Color.clear;
			UnityEngine.Event current = UnityEngine.Event.current;
			EventType type = current.type;
			if (type == 2)
			{
				Debug.Log("Mouse Move");
			}
			switch (type)
			{
			case 0:
				if (!global::UIUnityEvents.forbidHandlingNewEvents)
				{
					bool flag = current.button == 0;
					using (NGUIHack.Event @event = new NGUIHack.Event(current))
					{
						global::UICamera.HandleEvent(@event, type);
					}
					if (flag && current.type == 12 && GUIUtility.hotControl == 0)
					{
						GUIUtility.hotControl = global::UIUnityEvents.blankID;
					}
				}
				break;
			case 1:
			{
				bool flag2 = current.button == 0;
				using (NGUIHack.Event event2 = new NGUIHack.Event(current))
				{
					global::UICamera.HandleEvent(event2, type);
				}
				if (flag2 && GUIUtility.hotControl == global::UIUnityEvents.blankID)
				{
					GUIUtility.hotControl = 0;
				}
				break;
			}
			case 2:
			case 3:
			case 5:
			case 6:
				using (NGUIHack.Event event3 = new NGUIHack.Event(current))
				{
					global::UICamera.HandleEvent(event3, type);
				}
				break;
			case 4:
				if (!global::UIUnityEvents.forbidHandlingNewEvents)
				{
					using (NGUIHack.Event event4 = new NGUIHack.Event(current))
					{
						global::UICamera.HandleEvent(event4, type);
					}
				}
				break;
			case 7:
				if (!global::UIUnityEvents.forbidHandlingNewEvents && global::UIUnityEvents.lastMousePosition != current.mousePosition)
				{
					global::UIUnityEvents.lastMousePosition = current.mousePosition;
					using (NGUIHack.Event event5 = new NGUIHack.Event(current, 2))
					{
						global::UICamera.HandleEvent(event5, 2);
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
			global::UIUnityEvents.inOnGUI = false;
		}
	}

	// Token: 0x04002C26 RID: 11302
	private const int idLoop = 300;

	// Token: 0x04002C27 RID: 11303
	private const int controlIDHint = 320323492;

	// Token: 0x04002C28 RID: 11304
	private const string kControlName = "ngui-unityevents";

	// Token: 0x04002C29 RID: 11305
	private const int kGUIDepth = 49;

	// Token: 0x04002C2A RID: 11306
	public static bool forbidHandlingNewEvents;

	// Token: 0x04002C2B RID: 11307
	private global::UIInput mInput;

	// Token: 0x04002C2C RID: 11308
	private global::UICamera mCamera;

	// Token: 0x04002C2D RID: 11309
	private static bool madeSingleton;

	// Token: 0x04002C2E RID: 11310
	private static readonly Rect idRect = new Rect(0f, 0f, 69999f, 69999f);

	// Token: 0x04002C2F RID: 11311
	private static int controlID;

	// Token: 0x04002C30 RID: 11312
	private static global::UIInput lastInput;

	// Token: 0x04002C31 RID: 11313
	private static global::UILabel lastLabel;

	// Token: 0x04002C32 RID: 11314
	private static global::UICamera lastInputCamera;

	// Token: 0x04002C33 RID: 11315
	private static bool submit;

	// Token: 0x04002C34 RID: 11316
	private static GUIContent textInputContent = null;

	// Token: 0x04002C35 RID: 11317
	private static Vector2 lastCursorPosition;

	// Token: 0x04002C36 RID: 11318
	private static global::UITextPosition lastTextPosition;

	// Token: 0x04002C37 RID: 11319
	private static bool requiresBinding;

	// Token: 0x04002C38 RID: 11320
	private static bool focusSetInOnGUI;

	// Token: 0x04002C39 RID: 11321
	private static Vector2 lastMousePosition = new Vector2(-100f, -100f);

	// Token: 0x04002C3A RID: 11322
	private static int blankID;

	// Token: 0x04002C3B RID: 11323
	private static bool inOnGUI;

	// Token: 0x02000901 RID: 2305
	private static class LateLoaded
	{
		// Token: 0x06004EBB RID: 20155 RVA: 0x00146280 File Offset: 0x00144480
		static LateLoaded()
		{
			global::UIUnityEvents.LateLoaded.mTextBlockStyle.alignment = 0;
			global::UIUnityEvents.LateLoaded.mTextBlockStyle.border = new RectOffset(0, 0, 0, 0);
			global::UIUnityEvents.LateLoaded.mTextBlockStyle.clipping = 0;
			global::UIUnityEvents.LateLoaded.mTextBlockStyle.contentOffset = default(Vector2);
			global::UIUnityEvents.LateLoaded.mTextBlockStyle.fixedWidth = -1f;
			global::UIUnityEvents.LateLoaded.mTextBlockStyle.fixedHeight = -1f;
			global::UIUnityEvents.LateLoaded.mTextBlockStyle.imagePosition = 3;
			global::UIUnityEvents.LateLoaded.mTextBlockStyle.margin = new RectOffset(0, 0, 0, 0);
			global::UIUnityEvents.LateLoaded.mTextBlockStyle.name = "BLOCK STYLE";
			global::UIUnityEvents.LateLoaded.mTextBlockStyle.overflow = new RectOffset(0, 0, 0, 0);
			global::UIUnityEvents.LateLoaded.mTextBlockStyle.padding = new RectOffset(0, 0, 0, 0);
			global::UIUnityEvents.LateLoaded.mTextBlockStyle.stretchHeight = false;
			global::UIUnityEvents.LateLoaded.mTextBlockStyle.stretchWidth = false;
			global::UIUnityEvents.LateLoaded.mTextBlockStyle.wordWrap = false;
			GUIStyleState guistyleState = new GUIStyleState();
			guistyleState.background = null;
			guistyleState.textColor = Color.clear;
			global::UIUnityEvents.LateLoaded.mTextBlockStyle.active = guistyleState;
			global::UIUnityEvents.LateLoaded.mTextBlockStyle.focused = guistyleState;
			global::UIUnityEvents.LateLoaded.mTextBlockStyle.hover = guistyleState;
			global::UIUnityEvents.LateLoaded.mTextBlockStyle.normal = guistyleState;
			global::UIUnityEvents.LateLoaded.mTextBlockStyle.onActive = guistyleState;
			global::UIUnityEvents.LateLoaded.mTextBlockStyle.onFocused = guistyleState;
			global::UIUnityEvents.LateLoaded.mTextBlockStyle.onHover = guistyleState;
			global::UIUnityEvents.LateLoaded.mTextBlockStyle.onNormal = guistyleState;
			global::UIUnityEvents.LateLoaded._textFieldInput = typeof(GUIUtility).GetProperty("textFieldInput", BindingFlags.Static | BindingFlags.NonPublic);
			if (global::UIUnityEvents.LateLoaded._textFieldInput == null)
			{
				global::UIUnityEvents.LateLoaded.failedInvokeTextFieldInputGet = true;
				global::UIUnityEvents.LateLoaded.failedInvokeTextFieldInputSet = true;
				Debug.LogError("Unity has changed. no bool property textFieldInput in GUIUtility");
			}
			GameObject gameObject = new GameObject("__UIUnityEvents", new Type[]
			{
				typeof(global::UIUnityEvents)
			});
			global::UIUnityEvents.LateLoaded.singleton = gameObject.GetComponent<global::UIUnityEvents>();
			global::UIUnityEvents.madeSingleton = true;
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
					global::UIUnityEvents.LateLoaded.Keyactions = (Hashtable)obj;
				}
				else
				{
					if (!(obj is IDictionary))
					{
						throw new MethodAccessException("Unity has changed. no s_Keyactions member in TextEditor");
					}
					global::UIUnityEvents.LateLoaded.Keyactions = new Hashtable(obj as IDictionary);
				}
			}
			catch (MethodAccessException arg)
			{
				Debug.Log("Caught exception \r\n" + arg + "\r\nManually building keyactions.");
				global::UIUnityEvents.LateLoaded.Keyactions = new Hashtable();
				global::UIUnityEvents.LateLoaded.MapKey("left", global::UIUnityEvents.TextEditOp.MoveLeft);
				global::UIUnityEvents.LateLoaded.MapKey("right", global::UIUnityEvents.TextEditOp.MoveRight);
				global::UIUnityEvents.LateLoaded.MapKey("up", global::UIUnityEvents.TextEditOp.MoveUp);
				global::UIUnityEvents.LateLoaded.MapKey("down", global::UIUnityEvents.TextEditOp.MoveDown);
				global::UIUnityEvents.LateLoaded.MapKey("#left", global::UIUnityEvents.TextEditOp.SelectLeft);
				global::UIUnityEvents.LateLoaded.MapKey("#right", global::UIUnityEvents.TextEditOp.SelectRight);
				global::UIUnityEvents.LateLoaded.MapKey("#up", global::UIUnityEvents.TextEditOp.SelectUp);
				global::UIUnityEvents.LateLoaded.MapKey("#down", global::UIUnityEvents.TextEditOp.SelectDown);
				global::UIUnityEvents.LateLoaded.MapKey("delete", global::UIUnityEvents.TextEditOp.Delete);
				global::UIUnityEvents.LateLoaded.MapKey("backspace", global::UIUnityEvents.TextEditOp.Backspace);
				global::UIUnityEvents.LateLoaded.MapKey("#backspace", global::UIUnityEvents.TextEditOp.Backspace);
				if (Application.platform != 2 && Application.platform != 5 && Application.platform != 7)
				{
					global::UIUnityEvents.LateLoaded.MapKey("^left", global::UIUnityEvents.TextEditOp.MoveGraphicalLineStart);
					global::UIUnityEvents.LateLoaded.MapKey("^right", global::UIUnityEvents.TextEditOp.MoveGraphicalLineEnd);
					global::UIUnityEvents.LateLoaded.MapKey("&left", global::UIUnityEvents.TextEditOp.MoveWordLeft);
					global::UIUnityEvents.LateLoaded.MapKey("&right", global::UIUnityEvents.TextEditOp.MoveWordRight);
					global::UIUnityEvents.LateLoaded.MapKey("&up", global::UIUnityEvents.TextEditOp.MoveParagraphBackward);
					global::UIUnityEvents.LateLoaded.MapKey("&down", global::UIUnityEvents.TextEditOp.MoveParagraphForward);
					global::UIUnityEvents.LateLoaded.MapKey("%left", global::UIUnityEvents.TextEditOp.MoveGraphicalLineStart);
					global::UIUnityEvents.LateLoaded.MapKey("%right", global::UIUnityEvents.TextEditOp.MoveGraphicalLineEnd);
					global::UIUnityEvents.LateLoaded.MapKey("%up", global::UIUnityEvents.TextEditOp.MoveTextStart);
					global::UIUnityEvents.LateLoaded.MapKey("%down", global::UIUnityEvents.TextEditOp.MoveTextEnd);
					global::UIUnityEvents.LateLoaded.MapKey("#home", global::UIUnityEvents.TextEditOp.SelectTextStart);
					global::UIUnityEvents.LateLoaded.MapKey("#end", global::UIUnityEvents.TextEditOp.SelectTextEnd);
					global::UIUnityEvents.LateLoaded.MapKey("#^left", global::UIUnityEvents.TextEditOp.ExpandSelectGraphicalLineStart);
					global::UIUnityEvents.LateLoaded.MapKey("#^right", global::UIUnityEvents.TextEditOp.ExpandSelectGraphicalLineEnd);
					global::UIUnityEvents.LateLoaded.MapKey("#^up", global::UIUnityEvents.TextEditOp.SelectParagraphBackward);
					global::UIUnityEvents.LateLoaded.MapKey("#^down", global::UIUnityEvents.TextEditOp.SelectParagraphForward);
					global::UIUnityEvents.LateLoaded.MapKey("#&left", global::UIUnityEvents.TextEditOp.SelectWordLeft);
					global::UIUnityEvents.LateLoaded.MapKey("#&right", global::UIUnityEvents.TextEditOp.SelectWordRight);
					global::UIUnityEvents.LateLoaded.MapKey("#&up", global::UIUnityEvents.TextEditOp.SelectParagraphBackward);
					global::UIUnityEvents.LateLoaded.MapKey("#&down", global::UIUnityEvents.TextEditOp.SelectParagraphForward);
					global::UIUnityEvents.LateLoaded.MapKey("#%left", global::UIUnityEvents.TextEditOp.ExpandSelectGraphicalLineStart);
					global::UIUnityEvents.LateLoaded.MapKey("#%right", global::UIUnityEvents.TextEditOp.ExpandSelectGraphicalLineEnd);
					global::UIUnityEvents.LateLoaded.MapKey("#%up", global::UIUnityEvents.TextEditOp.SelectTextStart);
					global::UIUnityEvents.LateLoaded.MapKey("#%down", global::UIUnityEvents.TextEditOp.SelectTextEnd);
					global::UIUnityEvents.LateLoaded.MapKey("%a", global::UIUnityEvents.TextEditOp.SelectAll);
					global::UIUnityEvents.LateLoaded.MapKey("%x", global::UIUnityEvents.TextEditOp.Cut);
					global::UIUnityEvents.LateLoaded.MapKey("%c", global::UIUnityEvents.TextEditOp.Copy);
					global::UIUnityEvents.LateLoaded.MapKey("%v", global::UIUnityEvents.TextEditOp.Paste);
					global::UIUnityEvents.LateLoaded.MapKey("^d", global::UIUnityEvents.TextEditOp.Delete);
					global::UIUnityEvents.LateLoaded.MapKey("^h", global::UIUnityEvents.TextEditOp.Backspace);
					global::UIUnityEvents.LateLoaded.MapKey("^b", global::UIUnityEvents.TextEditOp.MoveLeft);
					global::UIUnityEvents.LateLoaded.MapKey("^f", global::UIUnityEvents.TextEditOp.MoveRight);
					global::UIUnityEvents.LateLoaded.MapKey("^a", global::UIUnityEvents.TextEditOp.MoveLineStart);
					global::UIUnityEvents.LateLoaded.MapKey("^e", global::UIUnityEvents.TextEditOp.MoveLineEnd);
					global::UIUnityEvents.LateLoaded.MapKey("&delete", global::UIUnityEvents.TextEditOp.DeleteWordForward);
					global::UIUnityEvents.LateLoaded.MapKey("&backspace", global::UIUnityEvents.TextEditOp.DeleteWordBack);
				}
				else
				{
					global::UIUnityEvents.LateLoaded.MapKey("home", global::UIUnityEvents.TextEditOp.MoveGraphicalLineStart);
					global::UIUnityEvents.LateLoaded.MapKey("end", global::UIUnityEvents.TextEditOp.MoveGraphicalLineEnd);
					global::UIUnityEvents.LateLoaded.MapKey("%left", global::UIUnityEvents.TextEditOp.MoveWordLeft);
					global::UIUnityEvents.LateLoaded.MapKey("%right", global::UIUnityEvents.TextEditOp.MoveWordRight);
					global::UIUnityEvents.LateLoaded.MapKey("%up", global::UIUnityEvents.TextEditOp.MoveParagraphBackward);
					global::UIUnityEvents.LateLoaded.MapKey("%down", global::UIUnityEvents.TextEditOp.MoveParagraphForward);
					global::UIUnityEvents.LateLoaded.MapKey("^left", global::UIUnityEvents.TextEditOp.MoveToEndOfPreviousWord);
					global::UIUnityEvents.LateLoaded.MapKey("^right", global::UIUnityEvents.TextEditOp.MoveToStartOfNextWord);
					global::UIUnityEvents.LateLoaded.MapKey("^up", global::UIUnityEvents.TextEditOp.MoveParagraphBackward);
					global::UIUnityEvents.LateLoaded.MapKey("^down", global::UIUnityEvents.TextEditOp.MoveParagraphForward);
					global::UIUnityEvents.LateLoaded.MapKey("#^left", global::UIUnityEvents.TextEditOp.SelectToEndOfPreviousWord);
					global::UIUnityEvents.LateLoaded.MapKey("#^right", global::UIUnityEvents.TextEditOp.SelectToStartOfNextWord);
					global::UIUnityEvents.LateLoaded.MapKey("#^up", global::UIUnityEvents.TextEditOp.SelectParagraphBackward);
					global::UIUnityEvents.LateLoaded.MapKey("#^down", global::UIUnityEvents.TextEditOp.SelectParagraphForward);
					global::UIUnityEvents.LateLoaded.MapKey("#home", global::UIUnityEvents.TextEditOp.SelectGraphicalLineStart);
					global::UIUnityEvents.LateLoaded.MapKey("#end", global::UIUnityEvents.TextEditOp.SelectGraphicalLineEnd);
					global::UIUnityEvents.LateLoaded.MapKey("^delete", global::UIUnityEvents.TextEditOp.DeleteWordForward);
					global::UIUnityEvents.LateLoaded.MapKey("^backspace", global::UIUnityEvents.TextEditOp.DeleteWordBack);
					global::UIUnityEvents.LateLoaded.MapKey("^a", global::UIUnityEvents.TextEditOp.SelectAll);
					global::UIUnityEvents.LateLoaded.MapKey("^x", global::UIUnityEvents.TextEditOp.Cut);
					global::UIUnityEvents.LateLoaded.MapKey("^c", global::UIUnityEvents.TextEditOp.Copy);
					global::UIUnityEvents.LateLoaded.MapKey("^v", global::UIUnityEvents.TextEditOp.Paste);
					global::UIUnityEvents.LateLoaded.MapKey("#delete", global::UIUnityEvents.TextEditOp.Cut);
					global::UIUnityEvents.LateLoaded.MapKey("^insert", global::UIUnityEvents.TextEditOp.Copy);
					global::UIUnityEvents.LateLoaded.MapKey("#insert", global::UIUnityEvents.TextEditOp.Paste);
				}
			}
		}

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x06004EBC RID: 20156 RVA: 0x001468C4 File Offset: 0x00144AC4
		// (set) Token: 0x06004EBD RID: 20157 RVA: 0x00146934 File Offset: 0x00144B34
		public static bool textFieldInput
		{
			get
			{
				if (!global::UIUnityEvents.LateLoaded.failedInvokeTextFieldInputGet)
				{
					try
					{
						return (bool)global::UIUnityEvents.LateLoaded._textFieldInput.GetValue(null, null);
					}
					catch (MethodAccessException arg)
					{
						global::UIUnityEvents.LateLoaded.failedInvokeTextFieldInputGet = true;
						Debug.Log("Can not get GUIUtility.textFieldInput\r\n" + arg);
					}
					return false;
				}
				return false;
			}
			set
			{
				if (!global::UIUnityEvents.LateLoaded.failedInvokeTextFieldInputSet)
				{
					try
					{
						global::UIUnityEvents.LateLoaded._textFieldInput.SetValue(null, value, null);
					}
					catch (MethodAccessException arg)
					{
						global::UIUnityEvents.LateLoaded.failedInvokeTextFieldInputSet = true;
						Debug.Log("Can not set GUIUtility.textFieldInput\r\n" + arg);
					}
				}
			}
		}

		// Token: 0x06004EBE RID: 20158 RVA: 0x0014699C File Offset: 0x00144B9C
		private static void MapKey(string key, global::UIUnityEvents.TextEditOp action)
		{
			global::UIUnityEvents.LateLoaded.Keyactions[UnityEngine.Event.KeyboardEvent(key)] = action;
		}

		// Token: 0x04002C3C RID: 11324
		public static readonly GUIStyle mTextBlockStyle = new GUIStyle();

		// Token: 0x04002C3D RID: 11325
		private static readonly PropertyInfo _textFieldInput;

		// Token: 0x04002C3E RID: 11326
		public static global::UIUnityEvents singleton;

		// Token: 0x04002C3F RID: 11327
		public static Hashtable Keyactions;

		// Token: 0x04002C40 RID: 11328
		private static bool failedInvokeTextFieldInputGet;

		// Token: 0x04002C41 RID: 11329
		private static bool failedInvokeTextFieldInputSet;
	}

	// Token: 0x02000902 RID: 2306
	private enum TextEditOp
	{
		// Token: 0x04002C43 RID: 11331
		MoveLeft,
		// Token: 0x04002C44 RID: 11332
		MoveRight,
		// Token: 0x04002C45 RID: 11333
		MoveUp,
		// Token: 0x04002C46 RID: 11334
		MoveDown,
		// Token: 0x04002C47 RID: 11335
		MoveLineStart,
		// Token: 0x04002C48 RID: 11336
		MoveLineEnd,
		// Token: 0x04002C49 RID: 11337
		MoveTextStart,
		// Token: 0x04002C4A RID: 11338
		MoveTextEnd,
		// Token: 0x04002C4B RID: 11339
		MovePageUp,
		// Token: 0x04002C4C RID: 11340
		MovePageDown,
		// Token: 0x04002C4D RID: 11341
		MoveGraphicalLineStart,
		// Token: 0x04002C4E RID: 11342
		MoveGraphicalLineEnd,
		// Token: 0x04002C4F RID: 11343
		MoveWordLeft,
		// Token: 0x04002C50 RID: 11344
		MoveWordRight,
		// Token: 0x04002C51 RID: 11345
		MoveParagraphForward,
		// Token: 0x04002C52 RID: 11346
		MoveParagraphBackward,
		// Token: 0x04002C53 RID: 11347
		MoveToStartOfNextWord,
		// Token: 0x04002C54 RID: 11348
		MoveToEndOfPreviousWord,
		// Token: 0x04002C55 RID: 11349
		SelectLeft,
		// Token: 0x04002C56 RID: 11350
		SelectRight,
		// Token: 0x04002C57 RID: 11351
		SelectUp,
		// Token: 0x04002C58 RID: 11352
		SelectDown,
		// Token: 0x04002C59 RID: 11353
		SelectTextStart,
		// Token: 0x04002C5A RID: 11354
		SelectTextEnd,
		// Token: 0x04002C5B RID: 11355
		SelectPageUp,
		// Token: 0x04002C5C RID: 11356
		SelectPageDown,
		// Token: 0x04002C5D RID: 11357
		ExpandSelectGraphicalLineStart,
		// Token: 0x04002C5E RID: 11358
		ExpandSelectGraphicalLineEnd,
		// Token: 0x04002C5F RID: 11359
		SelectGraphicalLineStart,
		// Token: 0x04002C60 RID: 11360
		SelectGraphicalLineEnd,
		// Token: 0x04002C61 RID: 11361
		SelectWordLeft,
		// Token: 0x04002C62 RID: 11362
		SelectWordRight,
		// Token: 0x04002C63 RID: 11363
		SelectToEndOfPreviousWord,
		// Token: 0x04002C64 RID: 11364
		SelectToStartOfNextWord,
		// Token: 0x04002C65 RID: 11365
		SelectParagraphBackward,
		// Token: 0x04002C66 RID: 11366
		SelectParagraphForward,
		// Token: 0x04002C67 RID: 11367
		Delete,
		// Token: 0x04002C68 RID: 11368
		Backspace,
		// Token: 0x04002C69 RID: 11369
		DeleteWordBack,
		// Token: 0x04002C6A RID: 11370
		DeleteWordForward,
		// Token: 0x04002C6B RID: 11371
		Cut,
		// Token: 0x04002C6C RID: 11372
		Copy,
		// Token: 0x04002C6D RID: 11373
		Paste,
		// Token: 0x04002C6E RID: 11374
		SelectAll,
		// Token: 0x04002C6F RID: 11375
		SelectNone,
		// Token: 0x04002C70 RID: 11376
		ScrollStart,
		// Token: 0x04002C71 RID: 11377
		ScrollEnd,
		// Token: 0x04002C72 RID: 11378
		ScrollPageUp,
		// Token: 0x04002C73 RID: 11379
		ScrollPageDown
	}
}
