using System;
using System.IO;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x02000724 RID: 1828
[global::NGCAutoAddScript]
public class LightSwitch : NetBehaviour, global::IActivatable, global::IActivatableToggle, global::IContextRequestable, global::IContextRequestableQuick, global::IContextRequestableText, global::IContextRequestablePointText, global::IComponentInterface<global::IActivatable, MonoBehaviour, global::Activatable>, global::IComponentInterface<global::IActivatable, MonoBehaviour>, global::IComponentInterface<global::IActivatable>, global::IComponentInterface<global::IContextRequestable, MonoBehaviour, global::Contextual>, global::IComponentInterface<global::IContextRequestable, MonoBehaviour>, global::IComponentInterface<global::IContextRequestable>
{
	// Token: 0x17000BA0 RID: 2976
	// (get) Token: 0x06003CB8 RID: 15544 RVA: 0x000D91DC File Offset: 0x000D73DC
	protected int randSeed
	{
		get
		{
			return this._randSeed;
		}
	}

	// Token: 0x17000BA1 RID: 2977
	// (get) Token: 0x06003CB9 RID: 15545 RVA: 0x000D91E4 File Offset: 0x000D73E4
	// (set) Token: 0x06003CBA RID: 15546 RVA: 0x000D91EC File Offset: 0x000D73EC
	private protected bool startsOn
	{
		protected get
		{
			return this._startsOn;
		}
		private set
		{
			this._startsOn = value;
		}
	}

	// Token: 0x06003CBB RID: 15547 RVA: 0x000D91F8 File Offset: 0x000D73F8
	private static void DefaultArray(string test, ref global::LightStyle[] array)
	{
		if (array == null)
		{
			global::LightStyle lightStyle = test;
			if (lightStyle)
			{
				array = new global::LightStyle[]
				{
					lightStyle
				};
			}
			else
			{
				array = new global::LightStyle[0];
			}
		}
		else if (array.Length == 0)
		{
			global::LightStyle lightStyle2 = test;
			if (lightStyle2)
			{
				array = new global::LightStyle[]
				{
					lightStyle2
				};
			}
		}
	}

	// Token: 0x06003CBC RID: 15548 RVA: 0x000D9264 File Offset: 0x000D7464
	private void Reset()
	{
		this._randSeed = Random.Range(0, int.MaxValue);
		if (this.stylists == null)
		{
			this.stylists = new global::LightStylist[0];
		}
		global::LightSwitch.DefaultArray("on", ref this.randOn);
		global::LightSwitch.DefaultArray("off", ref this.randOff);
	}

	// Token: 0x06003CBD RID: 15549 RVA: 0x000D92BC File Offset: 0x000D74BC
	private static bool MakeCTX(ref global::LightStylist[] stylists, ref global::LightSwitch.StylistCTX[] ctx)
	{
		int num;
		if (stylists == null)
		{
			num = 0;
		}
		else
		{
			num = stylists.Length;
		}
		Array.Resize<global::LightSwitch.StylistCTX>(ref ctx, num);
		return num > 0;
	}

	// Token: 0x06003CBE RID: 15550 RVA: 0x000D92E8 File Offset: 0x000D74E8
	private void Awake()
	{
		this.rand = new global::SeededRandom(this.randSeed);
		global::LightSwitch.MakeCTX(ref this.stylists, ref this.stylistCTX);
		if (this.stylists != null)
		{
			for (int i = 0; i < this.stylists.Length; i++)
			{
				if (this.stylists[i])
				{
					this.stylists[i] = this.stylists[i].ensuredAwake;
				}
			}
		}
	}

	// Token: 0x06003CBF RID: 15551 RVA: 0x000D9364 File Offset: 0x000D7564
	private void TurnOn()
	{
		if (this.randOn == null || this.randOn.Length == 0)
		{
			Debug.LogError("Theres no light styles in randOn", this);
		}
		else
		{
			int length = this.randOn.Length;
			for (int i = 0; i < this.stylistCTX.Length; i++)
			{
				this.stylistCTX[i].lastOnStyle = (sbyte)this.rand.RandomIndex(length);
				if (this.stylists[i])
				{
					this.stylists[i].CrossFade(this.randOn[(int)this.stylistCTX[i].lastOnStyle], Random.Range(this.minOnFadeDuration, this.maxOnFadeDuration));
				}
			}
		}
	}

	// Token: 0x06003CC0 RID: 15552 RVA: 0x000D9424 File Offset: 0x000D7624
	private void TurnOff()
	{
		if (this.randOff == null || this.randOff.Length == 0)
		{
			Debug.LogError("Theres no light styles in randOn", this);
		}
		else
		{
			int length = this.randOff.Length;
			for (int i = 0; i < this.stylistCTX.Length; i++)
			{
				this.stylistCTX[i].lastOffStyle = (sbyte)this.rand.RandomIndex(length);
				if (this.stylists[i])
				{
					this.stylists[i].CrossFade(this.randOff[(int)this.stylistCTX[i].lastOffStyle], Random.Range(this.minOffFadeDuration, this.maxOffFadeDuration));
				}
			}
		}
	}

	// Token: 0x06003CC1 RID: 15553 RVA: 0x000D94E4 File Offset: 0x000D76E4
	[RPC]
	protected void ReadState(bool on, uLink.NetworkMessageInfo info)
	{
		this.lastChangeTime = info.timestampInMillis;
		this.on = on;
		if (on)
		{
			this.TurnOn();
		}
		else
		{
			this.TurnOff();
		}
	}

	// Token: 0x06003CC2 RID: 15554 RVA: 0x000D9520 File Offset: 0x000D7720
	private void ServerToggle(ulong timestamp)
	{
		this.on = !this.on;
		this.lastChangeTime = timestamp / 1000.0;
		if (this.on)
		{
			this.TurnOn();
		}
		else
		{
			this.TurnOff();
		}
		global::NetCull.RPC<bool>(this, "ReadState", 1, this.on);
	}

	// Token: 0x06003CC3 RID: 15555 RVA: 0x000D9580 File Offset: 0x000D7780
	public string ContextText(global::Controllable localControllable)
	{
		if (this.on)
		{
			return this.textTurnOff;
		}
		return this.textTurnOn;
	}

	// Token: 0x06003CC4 RID: 15556 RVA: 0x000D959C File Offset: 0x000D779C
	public global::ContextExecution ContextQuery(global::Controllable controllable, ulong timestamp)
	{
		return global::ContextExecution.Quick;
	}

	// Token: 0x06003CC5 RID: 15557 RVA: 0x000D95A0 File Offset: 0x000D77A0
	public global::ContextResponse ContextRespondQuick(global::Controllable controllable, ulong timestamp)
	{
		this.ServerToggle(timestamp);
		return global::ContextResponse.DoneBreak;
	}

	// Token: 0x06003CC6 RID: 15558 RVA: 0x000D95AC File Offset: 0x000D77AC
	public bool ContextTextPoint(out Vector3 worldPoint)
	{
		worldPoint = default(Vector3);
		return false;
	}

	// Token: 0x06003CC7 RID: 15559 RVA: 0x000D95CC File Offset: 0x000D77CC
	private void OnDestroy()
	{
		if (this.registeredConnectCallback)
		{
			global::GameEvent.PlayerConnected -= this.PlayerConnected;
			this.registeredConnectCallback = false;
		}
	}

	// Token: 0x06003CC8 RID: 15560 RVA: 0x000D95F4 File Offset: 0x000D77F4
	private void Write(BinaryWriter writer)
	{
		writer.Write((!this.on) ? (-this.lastChangeTime) : this.lastChangeTime);
		writer.Write(this.rand.Seed);
		writer.Write(this.rand.PositionData);
		writer.Write((byte)this.stylistCTX.Length);
		for (int i = 0; i < this.stylistCTX.Length; i++)
		{
			this.stylistCTX[i].Write(writer);
		}
	}

	// Token: 0x06003CC9 RID: 15561 RVA: 0x000D9680 File Offset: 0x000D7880
	private void Read(BinaryReader reader)
	{
		this.lastChangeTime = reader.ReadDouble();
		this.on = (this.lastChangeTime > 0.0);
		if (!this.on)
		{
			this.lastChangeTime = -this.lastChangeTime;
		}
		int num = reader.ReadInt32();
		uint positionData = reader.ReadUInt32();
		byte b = reader.ReadByte();
		Array.Resize<global::LightSwitch.StylistCTX>(ref this.stylistCTX, (int)b);
		Array.Resize<global::LightStylist>(ref this.stylists, (int)b);
		for (int i = 0; i < (int)b; i++)
		{
			this.stylistCTX[i].Read(reader);
		}
		if (num != this.rand.Seed)
		{
			this._randSeed = num;
			this.rand = new global::SeededRandom(num);
		}
		this.rand.PositionData = positionData;
		this.JumpUpdate();
	}

	// Token: 0x06003CCA RID: 15562 RVA: 0x000D9750 File Offset: 0x000D7950
	private void JumpUpdate()
	{
		double time = global::NetCull.time - this.lastChangeTime;
		if (this.on)
		{
			int i = 0;
			int num = (this.randOn != null) ? this.randOn.Length : 0;
			while (i < this.stylistCTX.Length)
			{
				if (this.stylists[i] && (int)this.stylistCTX[i].lastOnStyle >= 0 && (int)this.stylistCTX[i].lastOnStyle < num && this.randOn[(int)this.stylistCTX[i].lastOnStyle])
				{
					this.stylists[i].Play(this.randOn[(int)this.stylistCTX[i].lastOnStyle], time);
				}
				else
				{
					Debug.Log("Did not set on " + i, this);
				}
				i++;
			}
		}
		else
		{
			int j = 0;
			int num2 = (this.randOff != null) ? this.randOff.Length : 0;
			while (j < this.stylistCTX.Length)
			{
				if (this.stylists[j] && (int)this.stylistCTX[j].lastOffStyle >= 0 && (int)this.stylistCTX[j].lastOffStyle < num2 && this.randOff[(int)this.stylistCTX[j].lastOffStyle])
				{
					this.stylists[j].Play(this.randOff[(int)this.stylistCTX[j].lastOffStyle], time);
				}
				else
				{
					Debug.Log("Did not set off " + j, this);
				}
				j++;
			}
		}
	}

	// Token: 0x17000BA2 RID: 2978
	// (get) Token: 0x06003CCB RID: 15563 RVA: 0x000D9930 File Offset: 0x000D7B30
	private int StreamSize
	{
		get
		{
			return 17 + this.stylistCTX.Length * 2;
		}
	}

	// Token: 0x06003CCC RID: 15564 RVA: 0x000D9940 File Offset: 0x000D7B40
	[RPC]
	private void ConnectSetup(byte[] data)
	{
		using (MemoryStream memoryStream = new MemoryStream(data))
		{
			using (BinaryReader binaryReader = new BinaryReader(memoryStream))
			{
				this.Read(binaryReader);
			}
		}
	}

	// Token: 0x06003CCD RID: 15565 RVA: 0x000D99BC File Offset: 0x000D7BBC
	public void PlayerConnected(global::PlayerClient player)
	{
		byte[] array = new byte[this.StreamSize];
		using (MemoryStream memoryStream = new MemoryStream(array))
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
			{
				this.Write(binaryWriter);
			}
		}
		global::NetCull.RPC<byte[]>(this, "ConnectSetup", player.netPlayer, array);
	}

	// Token: 0x06003CCE RID: 15566 RVA: 0x000D9A58 File Offset: 0x000D7C58
	public global::ActivationResult ActTrigger(global::Character instigator, global::ActivationToggleState toggleTarget, ulong timestamp)
	{
		if (toggleTarget != global::ActivationToggleState.On)
		{
			if (toggleTarget != global::ActivationToggleState.Off)
			{
				return global::ActivationResult.Fail_BadToggle;
			}
			if (!this.on)
			{
				return global::ActivationResult.Fail_Redundant;
			}
			this.ServerToggle(timestamp);
			return (!this.on) ? global::ActivationResult.Success : global::ActivationResult.Fail_Busy;
		}
		else
		{
			if (this.on)
			{
				return global::ActivationResult.Fail_Redundant;
			}
			this.ServerToggle(timestamp);
			return (!this.on) ? global::ActivationResult.Fail_Busy : global::ActivationResult.Success;
		}
	}

	// Token: 0x06003CCF RID: 15567 RVA: 0x000D9ACC File Offset: 0x000D7CCC
	public global::ActivationToggleState ActGetToggleState()
	{
		return (!this.on) ? global::ActivationToggleState.Off : global::ActivationToggleState.On;
	}

	// Token: 0x06003CD0 RID: 15568 RVA: 0x000D9AE0 File Offset: 0x000D7CE0
	public global::ActivationResult ActTrigger(global::Character instigator, ulong timestamp)
	{
		return this.ActTrigger(instigator, (!this.on) ? global::ActivationToggleState.On : global::ActivationToggleState.Off, timestamp);
	}

	// Token: 0x04001EEB RID: 7915
	[SerializeField]
	protected global::LightStylist[] stylists;

	// Token: 0x04001EEC RID: 7916
	private global::LightSwitch.StylistCTX[] stylistCTX;

	// Token: 0x04001EED RID: 7917
	private double lastChangeTime;

	// Token: 0x04001EEE RID: 7918
	[SerializeField]
	protected global::LightStyle[] randOn;

	// Token: 0x04001EEF RID: 7919
	[SerializeField]
	protected global::LightStyle[] randOff;

	// Token: 0x04001EF0 RID: 7920
	[SerializeField]
	private int _randSeed;

	// Token: 0x04001EF1 RID: 7921
	[SerializeField]
	protected float minOnFadeDuration;

	// Token: 0x04001EF2 RID: 7922
	[SerializeField]
	protected float maxOnFadeDuration;

	// Token: 0x04001EF3 RID: 7923
	[SerializeField]
	protected float minOffFadeDuration;

	// Token: 0x04001EF4 RID: 7924
	[SerializeField]
	protected float maxOffFadeDuration;

	// Token: 0x04001EF5 RID: 7925
	[SerializeField]
	private bool _startsOn;

	// Token: 0x04001EF6 RID: 7926
	private sbyte lastPickedOn;

	// Token: 0x04001EF7 RID: 7927
	private sbyte lastPickedOff;

	// Token: 0x04001EF8 RID: 7928
	private global::SeededRandom rand;

	// Token: 0x04001EF9 RID: 7929
	private bool on;

	// Token: 0x04001EFA RID: 7930
	[SerializeField]
	protected string textTurnOn = "Flick Up";

	// Token: 0x04001EFB RID: 7931
	[SerializeField]
	protected string textTurnOff = "Flick Down";

	// Token: 0x04001EFC RID: 7932
	private bool registeredConnectCallback;

	// Token: 0x02000725 RID: 1829
	private struct StylistCTX
	{
		// Token: 0x06003CD1 RID: 15569 RVA: 0x000D9AFC File Offset: 0x000D7CFC
		public void Write(BinaryWriter writer)
		{
			writer.Write(this.lastOnStyle);
			writer.Write(this.lastOffStyle);
		}

		// Token: 0x06003CD2 RID: 15570 RVA: 0x000D9B18 File Offset: 0x000D7D18
		public void Read(BinaryReader reader)
		{
			this.lastOnStyle = reader.ReadSByte();
			this.lastOffStyle = reader.ReadSByte();
		}

		// Token: 0x04001EFD RID: 7933
		public const int SIZE = 2;

		// Token: 0x04001EFE RID: 7934
		public sbyte lastOnStyle;

		// Token: 0x04001EFF RID: 7935
		public sbyte lastOffStyle;
	}
}
