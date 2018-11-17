using System;
using System.Diagnostics;
using Facepunch.Cursor;
using UnityEngine;

// Token: 0x02000486 RID: 1158
internal class ContextUI : MonoBehaviour
{
	// Token: 0x1700094F RID: 2383
	// (get) Token: 0x06002924 RID: 10532 RVA: 0x000A17DC File Offset: 0x0009F9DC
	internal ContextClientState clientState
	{
		get
		{
			return this._clientState;
		}
	}

	// Token: 0x06002925 RID: 10533 RVA: 0x000A17E4 File Offset: 0x0009F9E4
	[Conditional("CLIENT_POPUP_LOG")]
	private static void LOG(string shorthand, Object contextual)
	{
	}

	// Token: 0x06002926 RID: 10534 RVA: 0x000A17E8 File Offset: 0x0009F9E8
	private void Awake()
	{
		base.useGUILayout = false;
		this.clientUnlock = LockCursorManager.CreateCursorUnlockNode(false, 32, "Context Popup");
	}

	// Token: 0x06002927 RID: 10535 RVA: 0x000A1804 File Offset: 0x0009FA04
	private void OnDestroy()
	{
		this.clientUnlock.Dispose();
		this.clientUnlock = null;
	}

	// Token: 0x06002928 RID: 10536 RVA: 0x000A1818 File Offset: 0x0009FA18
	private void OnEnable()
	{
		LockCursorManager.DisableGUICheckOnEnable(this);
	}

	// Token: 0x06002929 RID: 10537 RVA: 0x000A1820 File Offset: 0x0009FA20
	private void OnDisable()
	{
		LockCursorManager.DisableGUICheckOnDisable(this);
	}

	// Token: 0x0600292A RID: 10538 RVA: 0x000A1828 File Offset: 0x0009FA28
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

	// Token: 0x0600292B RID: 10539 RVA: 0x000A18DC File Offset: 0x0009FADC
	private static void GUIString(string text, GUIStyle box)
	{
		int num;
		int num2;
		GUI.Box(ContextUI.BoxRect(box.CalcSize(ContextUI.temp), box, out num, out num2), ContextUI.temp, box);
	}

	// Token: 0x0600292C RID: 10540 RVA: 0x000A190C File Offset: 0x0009FB0C
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

	// Token: 0x0600292D RID: 10541 RVA: 0x000A1B4C File Offset: 0x0009FD4C
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
			if (num == -1 && NetCull.localTimeInMillis - this.clientQueryTime > 300UL && !Context.UICommands.IsButtonHeld(false))
			{
				Context.EndQuery();
			}
		}
		if (num != -1)
		{
			this.OnClientSelection(num);
		}
	}

	// Token: 0x0600292E RID: 10542 RVA: 0x000A1BF8 File Offset: 0x0009FDF8
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

	// Token: 0x0600292F RID: 10543 RVA: 0x000A1C6C File Offset: 0x0009FE6C
	private void OnClientPromptBegin(NetEntityID? useID)
	{
		NetEntityID value;
		if (useID != null)
		{
			value = useID.Value;
		}
		else
		{
			NetEntityID.Of(this.clientQuery, out value);
		}
		this.clientQueryTime = NetCull.localTimeInMillis;
		Context.UICommands.Issue_Request(value);
		this.SetContextClientState(ContextClientState.Polling);
	}

	// Token: 0x06002930 RID: 10544 RVA: 0x000A1CB8 File Offset: 0x0009FEB8
	private void OnClientShowMenu()
	{
		this.clientSelection = -1;
		this.clientUnlock.On = true;
		base.enabled = true;
	}

	// Token: 0x06002931 RID: 10545 RVA: 0x000A1CD4 File Offset: 0x0009FED4
	private void OnClientOptionsMade()
	{
		if (this._clientState == ContextClientState.Validating)
		{
			return;
		}
		ulong num = NetCull.localTimeInMillis - this.clientQueryTime;
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

	// Token: 0x06002932 RID: 10546 RVA: 0x000A1D38 File Offset: 0x0009FF38
	private void OnClientSelection(int i)
	{
		this.clientSelection = i;
		Context.UICommands.Issue_Selection(this.clientContext.option[i].name);
		this.validatingString = this.clientContext.option[i].text + "..";
		this.SetContextClientState(ContextClientState.Validating);
	}

	// Token: 0x06002933 RID: 10547 RVA: 0x000A1D94 File Offset: 0x0009FF94
	private void OnClientValidated()
	{
		this.SetContextClientState(ContextClientState.Off);
	}

	// Token: 0x06002934 RID: 10548 RVA: 0x000A1DA0 File Offset: 0x0009FFA0
	private void OnClientOptionsCleared()
	{
		if (this.clientSelection != -1)
		{
			this.clientSelection = -1;
		}
		this.clientContext.length = 0;
	}

	// Token: 0x06002935 RID: 10549 RVA: 0x000A1DC4 File Offset: 0x0009FFC4
	private void OnClientHideMenu()
	{
		base.CancelInvoke("OnClientShowMenu");
		if (this.clientUnlock.TryLock() == 0)
		{
			Context.UICommands.IsButtonHeld(true);
			Input.ResetInputAxes();
		}
		base.enabled = false;
	}

	// Token: 0x06002936 RID: 10550 RVA: 0x000A1E00 File Offset: 0x000A0000
	private void OnClientPromptEnd()
	{
		this.OnClientHideMenu();
		this.SetContextClientState(ContextClientState.Off);
	}

	// Token: 0x06002937 RID: 10551 RVA: 0x000A1E10 File Offset: 0x000A0010
	internal void OnServerQuerySent(MonoBehaviour script, NetEntityID entID)
	{
		this.clientQuery = script;
		this.OnClientPromptBegin(new NetEntityID?(entID));
	}

	// Token: 0x06002938 RID: 10552 RVA: 0x000A1E28 File Offset: 0x000A0028
	internal void OnServerQuickTapSent()
	{
		Context.UICommands.Issue_QuickTap();
		if (this._clientState == ContextClientState.Options)
		{
			this.OnClientHideMenu();
		}
		this.SetContextClientState(ContextClientState.Validating);
	}

	// Token: 0x06002939 RID: 10553 RVA: 0x000A1E48 File Offset: 0x000A0048
	internal void OnServerCancelSent()
	{
		Context.UICommands.Issue_Cancel();
		if (this._clientState == ContextClientState.Options)
		{
			this.OnClientHideMenu();
		}
		this.SetContextClientState(ContextClientState.Validating);
	}

	// Token: 0x0600293A RID: 10554 RVA: 0x000A1E68 File Offset: 0x000A0068
	internal void OnServerMenu(ContextMenuData menu)
	{
		this.clientContext.Set(menu);
		this.OnClientOptionsMade();
	}

	// Token: 0x0600293B RID: 10555 RVA: 0x000A1E7C File Offset: 0x000A007C
	internal void OnServerNoOp()
	{
		this.clientContext.length = 0;
		this.OnClientPromptEnd();
	}

	// Token: 0x0600293C RID: 10556 RVA: 0x000A1E90 File Offset: 0x000A0090
	internal void OnServerCancel()
	{
		this.OnClientPromptEnd();
	}

	// Token: 0x0600293D RID: 10557 RVA: 0x000A1E98 File Offset: 0x000A0098
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

	// Token: 0x0600293E RID: 10558 RVA: 0x000A1EC0 File Offset: 0x000A00C0
	internal void OnServerSelectionStale()
	{
		this.OnClientPromptEnd();
	}

	// Token: 0x0600293F RID: 10559 RVA: 0x000A1EC8 File Offset: 0x000A00C8
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

	// Token: 0x06002940 RID: 10560 RVA: 0x000A1EF0 File Offset: 0x000A00F0
	internal void OnServerRestartPolling()
	{
		this.OnClientOptionsCleared();
		this.SetContextClientState(ContextClientState.Polling);
		this.OnClientOptionsMade();
	}

	// Token: 0x0400152E RID: 5422
	[SerializeField]
	private GUISkin skin;

	// Token: 0x0400152F RID: 5423
	[NonSerialized]
	internal UnlockCursorNode clientUnlock;

	// Token: 0x04001530 RID: 5424
	[NonSerialized]
	internal ContextClientStage clientContext;

	// Token: 0x04001531 RID: 5425
	[NonSerialized]
	internal MonoBehaviour clientQuery;

	// Token: 0x04001532 RID: 5426
	[NonSerialized]
	internal ContextClientState _clientState;

	// Token: 0x04001533 RID: 5427
	[NonSerialized]
	internal ulong clientQueryTime;

	// Token: 0x04001534 RID: 5428
	[NonSerialized]
	internal string validatingString;

	// Token: 0x04001535 RID: 5429
	[NonSerialized]
	internal int clientSelection;

	// Token: 0x04001536 RID: 5430
	[NonSerialized]
	internal static ContextClientWorkingCallback clientWorkingCallbacks;

	// Token: 0x04001537 RID: 5431
	private static GUIContent temp = new GUIContent();
}
