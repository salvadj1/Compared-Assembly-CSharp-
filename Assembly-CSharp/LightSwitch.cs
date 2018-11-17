using System;
using System.IO;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x02000660 RID: 1632
[NGCAutoAddScript]
public class LightSwitch : NetBehaviour, IActivatable, IActivatableToggle, IContextRequestable, IContextRequestableQuick, IContextRequestableText, IContextRequestablePointText, IComponentInterface<IActivatable, MonoBehaviour, Activatable>, IComponentInterface<IActivatable, MonoBehaviour>, IComponentInterface<IActivatable>, IComponentInterface<IContextRequestable, MonoBehaviour, Contextual>, IComponentInterface<IContextRequestable, MonoBehaviour>, IComponentInterface<IContextRequestable>
{
	// Token: 0x17000B1E RID: 2846
	// (get) Token: 0x060038C4 RID: 14532 RVA: 0x000D07FC File Offset: 0x000CE9FC
	protected int randSeed
	{
		get
		{
			return this._randSeed;
		}
	}

	// Token: 0x17000B1F RID: 2847
	// (get) Token: 0x060038C5 RID: 14533 RVA: 0x000D0804 File Offset: 0x000CEA04
	// (set) Token: 0x060038C6 RID: 14534 RVA: 0x000D080C File Offset: 0x000CEA0C
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

	// Token: 0x060038C7 RID: 14535 RVA: 0x000D0818 File Offset: 0x000CEA18
	private static void DefaultArray(string test, ref LightStyle[] array)
	{
		if (array == null)
		{
			LightStyle lightStyle = test;
			if (lightStyle)
			{
				array = new LightStyle[]
				{
					lightStyle
				};
			}
			else
			{
				array = new LightStyle[0];
			}
		}
		else if (array.Length == 0)
		{
			LightStyle lightStyle2 = test;
			if (lightStyle2)
			{
				array = new LightStyle[]
				{
					lightStyle2
				};
			}
		}
	}

	// Token: 0x060038C8 RID: 14536 RVA: 0x000D0884 File Offset: 0x000CEA84
	private void Reset()
	{
		this._randSeed = Random.Range(0, int.MaxValue);
		if (this.stylists == null)
		{
			this.stylists = new LightStylist[0];
		}
		LightSwitch.DefaultArray("on", ref this.randOn);
		LightSwitch.DefaultArray("off", ref this.randOff);
	}

	// Token: 0x060038C9 RID: 14537 RVA: 0x000D08DC File Offset: 0x000CEADC
	private static bool MakeCTX(ref LightStylist[] stylists, ref LightSwitch.StylistCTX[] ctx)
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
		Array.Resize<LightSwitch.StylistCTX>(ref ctx, num);
		return num > 0;
	}

	// Token: 0x060038CA RID: 14538 RVA: 0x000D0908 File Offset: 0x000CEB08
	private void Awake()
	{
		this.rand = new SeededRandom(this.randSeed);
		LightSwitch.MakeCTX(ref this.stylists, ref this.stylistCTX);
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

	// Token: 0x060038CB RID: 14539 RVA: 0x000D0984 File Offset: 0x000CEB84
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

	// Token: 0x060038CC RID: 14540 RVA: 0x000D0A44 File Offset: 0x000CEC44
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

	// Token: 0x060038CD RID: 14541 RVA: 0x000D0B04 File Offset: 0x000CED04
	[RPC]
	protected void ReadState(bool on, NetworkMessageInfo info)
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

	// Token: 0x060038CE RID: 14542 RVA: 0x000D0B40 File Offset: 0x000CED40
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
		NetCull.RPC<bool>(this, "ReadState", 1, this.on);
	}

	// Token: 0x060038CF RID: 14543 RVA: 0x000D0BA0 File Offset: 0x000CEDA0
	public string ContextText(Controllable localControllable)
	{
		if (this.on)
		{
			return this.textTurnOff;
		}
		return this.textTurnOn;
	}

	// Token: 0x060038D0 RID: 14544 RVA: 0x000D0BBC File Offset: 0x000CEDBC
	public ContextExecution ContextQuery(Controllable controllable, ulong timestamp)
	{
		return ContextExecution.Quick;
	}

	// Token: 0x060038D1 RID: 14545 RVA: 0x000D0BC0 File Offset: 0x000CEDC0
	public ContextResponse ContextRespondQuick(Controllable controllable, ulong timestamp)
	{
		this.ServerToggle(timestamp);
		return ContextResponse.DoneBreak;
	}

	// Token: 0x060038D2 RID: 14546 RVA: 0x000D0BCC File Offset: 0x000CEDCC
	public bool ContextTextPoint(out Vector3 worldPoint)
	{
		worldPoint = default(Vector3);
		return false;
	}

	// Token: 0x060038D3 RID: 14547 RVA: 0x000D0BEC File Offset: 0x000CEDEC
	private void OnDestroy()
	{
		if (this.registeredConnectCallback)
		{
			GameEvent.PlayerConnected -= this.PlayerConnected;
			this.registeredConnectCallback = false;
		}
	}

	// Token: 0x060038D4 RID: 14548 RVA: 0x000D0C14 File Offset: 0x000CEE14
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

	// Token: 0x060038D5 RID: 14549 RVA: 0x000D0CA0 File Offset: 0x000CEEA0
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
		Array.Resize<LightSwitch.StylistCTX>(ref this.stylistCTX, (int)b);
		Array.Resize<LightStylist>(ref this.stylists, (int)b);
		for (int i = 0; i < (int)b; i++)
		{
			this.stylistCTX[i].Read(reader);
		}
		if (num != this.rand.Seed)
		{
			this._randSeed = num;
			this.rand = new SeededRandom(num);
		}
		this.rand.PositionData = positionData;
		this.JumpUpdate();
	}

	// Token: 0x060038D6 RID: 14550 RVA: 0x000D0D70 File Offset: 0x000CEF70
	private void JumpUpdate()
	{
		double time = NetCull.time - this.lastChangeTime;
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

	// Token: 0x17000B20 RID: 2848
	// (get) Token: 0x060038D7 RID: 14551 RVA: 0x000D0F50 File Offset: 0x000CF150
	private int StreamSize
	{
		get
		{
			return 17 + this.stylistCTX.Length * 2;
		}
	}

	// Token: 0x060038D8 RID: 14552 RVA: 0x000D0F60 File Offset: 0x000CF160
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

	// Token: 0x060038D9 RID: 14553 RVA: 0x000D0FDC File Offset: 0x000CF1DC
	public void PlayerConnected(PlayerClient player)
	{
		byte[] array = new byte[this.StreamSize];
		using (MemoryStream memoryStream = new MemoryStream(array))
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
			{
				this.Write(binaryWriter);
			}
		}
		NetCull.RPC<byte[]>(this, "ConnectSetup", player.netPlayer, array);
	}

	// Token: 0x060038DA RID: 14554 RVA: 0x000D1078 File Offset: 0x000CF278
	public ActivationResult ActTrigger(Character instigator, ActivationToggleState toggleTarget, ulong timestamp)
	{
		if (toggleTarget != ActivationToggleState.On)
		{
			if (toggleTarget != ActivationToggleState.Off)
			{
				return ActivationResult.Fail_BadToggle;
			}
			if (!this.on)
			{
				return ActivationResult.Fail_Redundant;
			}
			this.ServerToggle(timestamp);
			return (!this.on) ? ActivationResult.Success : ActivationResult.Fail_Busy;
		}
		else
		{
			if (this.on)
			{
				return ActivationResult.Fail_Redundant;
			}
			this.ServerToggle(timestamp);
			return (!this.on) ? ActivationResult.Fail_Busy : ActivationResult.Success;
		}
	}

	// Token: 0x060038DB RID: 14555 RVA: 0x000D10EC File Offset: 0x000CF2EC
	public ActivationToggleState ActGetToggleState()
	{
		return (!this.on) ? ActivationToggleState.Off : ActivationToggleState.On;
	}

	// Token: 0x060038DC RID: 14556 RVA: 0x000D1100 File Offset: 0x000CF300
	public ActivationResult ActTrigger(Character instigator, ulong timestamp)
	{
		return this.ActTrigger(instigator, (!this.on) ? ActivationToggleState.On : ActivationToggleState.Off, timestamp);
	}

	// Token: 0x04001CF3 RID: 7411
	[SerializeField]
	protected LightStylist[] stylists;

	// Token: 0x04001CF4 RID: 7412
	private LightSwitch.StylistCTX[] stylistCTX;

	// Token: 0x04001CF5 RID: 7413
	private double lastChangeTime;

	// Token: 0x04001CF6 RID: 7414
	[SerializeField]
	protected LightStyle[] randOn;

	// Token: 0x04001CF7 RID: 7415
	[SerializeField]
	protected LightStyle[] randOff;

	// Token: 0x04001CF8 RID: 7416
	[SerializeField]
	private int _randSeed;

	// Token: 0x04001CF9 RID: 7417
	[SerializeField]
	protected float minOnFadeDuration;

	// Token: 0x04001CFA RID: 7418
	[SerializeField]
	protected float maxOnFadeDuration;

	// Token: 0x04001CFB RID: 7419
	[SerializeField]
	protected float minOffFadeDuration;

	// Token: 0x04001CFC RID: 7420
	[SerializeField]
	protected float maxOffFadeDuration;

	// Token: 0x04001CFD RID: 7421
	[SerializeField]
	private bool _startsOn;

	// Token: 0x04001CFE RID: 7422
	private sbyte lastPickedOn;

	// Token: 0x04001CFF RID: 7423
	private sbyte lastPickedOff;

	// Token: 0x04001D00 RID: 7424
	private SeededRandom rand;

	// Token: 0x04001D01 RID: 7425
	private bool on;

	// Token: 0x04001D02 RID: 7426
	[SerializeField]
	protected string textTurnOn = "Flick Up";

	// Token: 0x04001D03 RID: 7427
	[SerializeField]
	protected string textTurnOff = "Flick Down";

	// Token: 0x04001D04 RID: 7428
	private bool registeredConnectCallback;

	// Token: 0x02000661 RID: 1633
	private struct StylistCTX
	{
		// Token: 0x060038DD RID: 14557 RVA: 0x000D111C File Offset: 0x000CF31C
		public void Write(BinaryWriter writer)
		{
			writer.Write(this.lastOnStyle);
			writer.Write(this.lastOffStyle);
		}

		// Token: 0x060038DE RID: 14558 RVA: 0x000D1138 File Offset: 0x000CF338
		public void Read(BinaryReader reader)
		{
			this.lastOnStyle = reader.ReadSByte();
			this.lastOffStyle = reader.ReadSByte();
		}

		// Token: 0x04001D05 RID: 7429
		public const int SIZE = 2;

		// Token: 0x04001D06 RID: 7430
		public sbyte lastOnStyle;

		// Token: 0x04001D07 RID: 7431
		public sbyte lastOffStyle;
	}
}
