using System;
using System.Diagnostics;
using Facepunch.Cursor;
using UnityEngine;

// Token: 0x02000541 RID: 1345
internal class ContextUI : MonoBehaviour
{
	// Token: 0x170009BF RID: 2495
	// (get) Token: 0x06002CD6 RID: 11478 RVA: 0x000A7BD8 File Offset: 0x000A5DD8
	internal ContextClientState clientState
	{
		get
		{
			return this._clientState;
		}
	}

	// Token: 0x06002CD7 RID: 11479 RVA: 0x000A7BE0 File Offset: 0x000A5DE0
	[Conditional("CLIENT_POPUP_LOG")]
	private static void LOG(string shorthand, Object contextual)
	{
	}

	// Token: 0x06002CD8 RID: 11480 RVA: 0x000A7BE4 File Offset: 0x000A5DE4
	private void Awake()
	{
		base.useGUILayout = false;
		this.clientUnlock = LockCursorManager.CreateCursorUnlockNode(false, 32, "Context Popup");
	}

	// Token: 0x06002CD9 RID: 11481 RVA: 0x000A7C00 File Offset: 0x000A5E00
	private void OnDestroy()
	{
		this.clientUnlock.Dispose();
		this.clientUnlock = null;
	}

	// Token: 0x06002CDA RID: 11482 RVA: 0x000A7C14 File Offset: 0x000A5E14
	private void OnEnable()
	{
		LockCursorManager.DisableGUICheckOnEnable(this);
	}

	// Token: 0x06002CDB RID: 11483 RVA: 0x000A7C1C File Offset: 0x000A5E1C
	private void OnDisable()
	{
		LockCursorManager.DisableGUICheckOnDisable(this);
	}

	// Token: 0x06002CDC RID: 11484 RVA: 0x000A7C24 File Offset: 0x000A5E24
	private static Rect BoxRect(Vector2 contentSize, GUIStyle box, out int xOffset, out int yOffset)
	{
		Rect rect = box.padding.Add(new Rect(0f, 0f, contentSize.x, contentSize.y));
		int num = Mathf.FloorToInt(((float)Screen.width - rect.width) * 0.5f);
		int num2 = Mathf.FloorToInt(((float)Screen.height - rect.height) * 0.5f);
		rect.x += (float)num;
		rect.y += (float)num2;
		Rect rect2 = box.padding.Remove(rect);
		xOffset = Mathf.FloorToInt(rect2.x);
		yOffset = Mathf.FloorToInt(rect2.y);
		return rect;
	}

	// Token: 0x06002CDD RID: 11485 RVA: 0x000A7CD8 File Offset: 0x000A5ED8
	private static void GUIString(string text, GUIStyle box)
	{
		int num;
		int num2;
		GUI.Box(ContextUI.BoxRect(box.CalcSize(ContextUI.temp), box, out num, out num2), ContextUI.temp, box);
	}

	// Token: 0x06002CDE RID: 11486 RVA: 0x000A7D08 File Offset: 0x000A5F08
	private int GUIOptions(GUIStyle box, GUIStyle button)
	{
		Rect[] array = new Rect[this.clientContext.length];
		int? num = (button.fixedWidth != 0f) ? new int?((int)button.fixedWidth) : null;
		int? num2 = (button.fixedHeight != 0f) ? new int?((int)button.fixedHeight) : null;
		float num3 = float.NegativeInfinity;
		float num4 = 0f;
		for (int i = 0; i < this.clientContext.length; i++)
		{
			ContextUI.temp.text = this.clientContext.option[i].text;
			Vector2 vector = button.CalcSize(ContextUI.temp);
			Rect rect = button.margin.Add(array[i] = button.padding.Add(new Rect(0f, 0f, (float)((num == null) ? Mathf.CeilToInt(vector.x) : num.Value), (float)((num2 == null) ? Mathf.CeilToInt(vector.y) : num2.Value))));
			if (rect.width > num3)
			{
				num3 = rect.width;
			}
			num4 += rect.height;
		}
		int num5;
		int num6;
		GUI.Box(ContextUI.BoxRect(new Vector2(num3, num4), box, out num5, out num6), GUIContent.none, box);
		int result = -1;
		for (int j = 0; j < this.clientContext.length; j++)
		{
			Rect rect2 = button.margin.Add(array[j]);
			rect2.width = num3;
			rect2.x = (float)num5;
			rect2.y = (float)num6;
			num6 = Mathf.FloorToInt(button.margin.Add(rect2).yMax);
			if (GUI.Button(button.margin.Remove(rect2), this.clientContext.option[j].text, button))
			{
				result = j;
			}
		}
		return result;
	}

	// Token: 0x06002CDF RID: 11487 RVA: 0x000A7F48 File Offset: 0x000A6148
	private void OnGUI()
	{
		GUI.depth = 1;
		GUI.skin = this.skin;
		GUIStyle box = "ctxbox";
		GUIStyle button = "ctxbutton";
		int num = -1;
		ContextClientState clientState = this.clientState;
		if (clientState != ContextClientState.Options)
		{
			if (clientState != ContextClientState.Validating)
			{
				return;
			}
			ContextUI.GUIString(this.validatingString, box);
		}
		else
		{
			num = this.GUIOptions(box, button);
			if (num == -1 && global::NetCull.localTimeInMillis - this.clientQueryTime > 300UL && !global::Context.UICommands.IsButtonHeld(false))
			{
				global::Context.EndQuery();
			}
		}
		if (num != -1)
		{
			this.OnClientSelection(num);
		}
	}

	// Token: 0x06002CE0 RID: 11488 RVA: 0x000A7FF4 File Offset: 0x000A61F4
	private void SetContextClientState(ContextClientState state)
	{
		if (this._clientState != state)
		{
			if (this._clientState == ContextClientState.Off)
			{
				this._clientState = state;
				if (ContextUI.clientWorkingCallbacks != null)
				{
					ContextUI.clientWorkingCallbacks(true);
				}
			}
			else if (state == ContextClientState.Off)
			{
				this._clientState = state;
				if (ContextUI.clientWorkingCallbacks != null)
				{
					ContextUI.clientWorkingCallbacks(false);
				}
			}
			else
			{
				this._clientState = state;
			}
		}
	}

	// Token: 0x06002CE1 RID: 11489 RVA: 0x000A8068 File Offset: 0x000A6268
	private void OnClientPromptBegin(global::NetEntityID? useID)
	{
		global::NetEntityID value;
		if (useID != null)
		{
			value = useID.Value;
		}
		else
		{
			global::NetEntityID.Of(this.clientQuery, out value);
		}
		this.clientQueryTime = global::NetCull.localTimeInMillis;
		global::Context.UICommands.Issue_Request(value);
		this.SetContextClientState(ContextClientState.Polling);
	}

	// Token: 0x06002CE2 RID: 11490 RVA: 0x000A80B4 File Offset: 0x000A62B4
	private void OnClientShowMenu()
	{
		this.clientSelection = -1;
		this.clientUnlock.On = true;
		base.enabled = true;
	}

	// Token: 0x06002CE3 RID: 11491 RVA: 0x000A80D0 File Offset: 0x000A62D0
	private void OnClientOptionsMade()
	{
		if (this._clientState == ContextClientState.Validating)
		{
			return;
		}
		ulong num = global::NetCull.localTimeInMillis - this.clientQueryTime;
		if (num > 300UL)
		{
			this.OnClientShowMenu();
		}
		else
		{
			base.Invoke("OnClientShowMenu", (float)((num + 50UL) / 1000.0));
		}
		this.SetContextClientState(ContextClientState.Options);
	}

	// Token: 0x06002CE4 RID: 11492 RVA: 0x000A8134 File Offset: 0x000A6334
	private void OnClientSelection(int i)
	{
		this.clientSelection = i;
		global::Context.UICommands.Issue_Selection(this.clientContext.option[i].name);
		this.validatingString = this.clientContext.option[i].text + "..";
		this.SetContextClientState(ContextClientState.Validating);
	}

	// Token: 0x06002CE5 RID: 11493 RVA: 0x000A8190 File Offset: 0x000A6390
	private void OnClientValidated()
	{
		this.SetContextClientState(ContextClientState.Off);
	}

	// Token: 0x06002CE6 RID: 11494 RVA: 0x000A819C File Offset: 0x000A639C
	private void OnClientOptionsCleared()
	{
		if (this.clientSelection != -1)
		{
			this.clientSelection = -1;
		}
		this.clientContext.length = 0;
	}

	// Token: 0x06002CE7 RID: 11495 RVA: 0x000A81C0 File Offset: 0x000A63C0
	private void OnClientHideMenu()
	{
		base.CancelInvoke("OnClientShowMenu");
		if (this.clientUnlock.TryLock() == 0)
		{
			global::Context.UICommands.IsButtonHeld(true);
			Input.ResetInputAxes();
		}
		base.enabled = false;
	}

	// Token: 0x06002CE8 RID: 11496 RVA: 0x000A81FC File Offset: 0x000A63FC
	private void OnClientPromptEnd()
	{
		this.OnClientHideMenu();
		this.SetContextClientState(ContextClientState.Off);
	}

	// Token: 0x06002CE9 RID: 11497 RVA: 0x000A820C File Offset: 0x000A640C
	internal void OnServerQuerySent(MonoBehaviour script, global::NetEntityID entID)
	{
		this.clientQuery = script;
		this.OnClientPromptBegin(new global::NetEntityID?(entID));
	}

	// Token: 0x06002CEA RID: 11498 RVA: 0x000A8224 File Offset: 0x000A6424
	internal void OnServerQuickTapSent()
	{
		global::Context.UICommands.Issue_QuickTap();
		if (this._clientState == ContextClientState.Options)
		{
			this.OnClientHideMenu();
		}
		this.SetContextClientState(ContextClientState.Validating);
	}

	// Token: 0x06002CEB RID: 11499 RVA: 0x000A8244 File Offset: 0x000A6444
	internal void OnServerCancelSent()
	{
		global::Context.UICommands.Issue_Cancel();
		if (this._clientState == ContextClientState.Options)
		{
			this.OnClientHideMenu();
		}
		this.SetContextClientState(ContextClientState.Validating);
	}

	// Token: 0x06002CEC RID: 11500 RVA: 0x000A8264 File Offset: 0x000A6464
	internal void OnServerMenu(ContextMenuData menu)
	{
		this.clientContext.Set(menu);
		this.OnClientOptionsMade();
	}

	// Token: 0x06002CED RID: 11501 RVA: 0x000A8278 File Offset: 0x000A6478
	internal void OnServerNoOp()
	{
		this.clientContext.length = 0;
		this.OnClientPromptEnd();
	}

	// Token: 0x06002CEE RID: 11502 RVA: 0x000A828C File Offset: 0x000A648C
	internal void OnServerCancel()
	{
		this.OnClientPromptEnd();
	}

	// Token: 0x06002CEF RID: 11503 RVA: 0x000A8294 File Offset: 0x000A6494
	internal void OnServerSelection(bool success)
	{
		if (success)
		{
			this.OnClientValidated();
			this.OnClientOptionsCleared();
			this.OnClientPromptEnd();
		}
		else
		{
			this.OnClientPromptEnd();
		}
	}

	// Token: 0x06002CF0 RID: 11504 RVA: 0x000A82BC File Offset: 0x000A64BC
	internal void OnServerSelectionStale()
	{
		this.OnClientPromptEnd();
	}

	// Token: 0x06002CF1 RID: 11505 RVA: 0x000A82C4 File Offset: 0x000A64C4
	internal void OnServerImmediate(bool success)
	{
		if (success)
		{
			this.OnClientValidated();
			this.OnClientOptionsCleared();
			this.OnClientPromptEnd();
		}
		else
		{
			this.OnClientPromptEnd();
		}
	}

	// Token: 0x06002CF2 RID: 11506 RVA: 0x000A82EC File Offset: 0x000A64EC
	internal void OnServerRestartPolling()
	{
		this.OnClientOptionsCleared();
		this.SetContextClientState(ContextClientState.Polling);
		this.OnClientOptionsMade();
	}

	// Token: 0x040016C4 RID: 5828
	[SerializeField]
	private GUISkin skin;

	// Token: 0x040016C5 RID: 5829
	[NonSerialized]
	internal UnlockCursorNode clientUnlock;

	// Token: 0x040016C6 RID: 5830
	[NonSerialized]
	internal ContextClientStage clientContext;

	// Token: 0x040016C7 RID: 5831
	[NonSerialized]
	internal MonoBehaviour clientQuery;

	// Token: 0x040016C8 RID: 5832
	[NonSerialized]
	internal ContextClientState _clientState;

	// Token: 0x040016C9 RID: 5833
	[NonSerialized]
	internal ulong clientQueryTime;

	// Token: 0x040016CA RID: 5834
	[NonSerialized]
	internal string validatingString;

	// Token: 0x040016CB RID: 5835
	[NonSerialized]
	internal int clientSelection;

	// Token: 0x040016CC RID: 5836
	[NonSerialized]
	internal static global::ContextClientWorkingCallback clientWorkingCallbacks;

	// Token: 0x040016CD RID: 5837
	private static GUIContent temp = new GUIContent();
}
