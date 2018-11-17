using System;
using System.Reflection;
using UnityEngine;

// Token: 0x020007D9 RID: 2009
[AddComponentMenu("Daikon Forge/Data Binding/Key Binding")]
[Serializable]
public class dfControlKeyBinding : MonoBehaviour, global::IDataBindingComponent
{
	// Token: 0x17000D70 RID: 3440
	// (get) Token: 0x060045A3 RID: 17827 RVA: 0x00105C14 File Offset: 0x00103E14
	// (set) Token: 0x060045A4 RID: 17828 RVA: 0x00105C1C File Offset: 0x00103E1C
	public global::dfControl Control
	{
		get
		{
			return this.control;
		}
		set
		{
			if (this.isBound)
			{
				this.Unbind();
			}
			this.control = value;
		}
	}

	// Token: 0x17000D71 RID: 3441
	// (get) Token: 0x060045A5 RID: 17829 RVA: 0x00105C38 File Offset: 0x00103E38
	// (set) Token: 0x060045A6 RID: 17830 RVA: 0x00105C40 File Offset: 0x00103E40
	public KeyCode KeyCode
	{
		get
		{
			return this.keyCode;
		}
		set
		{
			this.keyCode = value;
		}
	}

	// Token: 0x17000D72 RID: 3442
	// (get) Token: 0x060045A7 RID: 17831 RVA: 0x00105C4C File Offset: 0x00103E4C
	// (set) Token: 0x060045A8 RID: 17832 RVA: 0x00105C54 File Offset: 0x00103E54
	public bool AltPressed
	{
		get
		{
			return this.altPressed;
		}
		set
		{
			this.altPressed = value;
		}
	}

	// Token: 0x17000D73 RID: 3443
	// (get) Token: 0x060045A9 RID: 17833 RVA: 0x00105C60 File Offset: 0x00103E60
	// (set) Token: 0x060045AA RID: 17834 RVA: 0x00105C68 File Offset: 0x00103E68
	public bool ControlPressed
	{
		get
		{
			return this.controlPressed;
		}
		set
		{
			this.controlPressed = value;
		}
	}

	// Token: 0x17000D74 RID: 3444
	// (get) Token: 0x060045AB RID: 17835 RVA: 0x00105C74 File Offset: 0x00103E74
	// (set) Token: 0x060045AC RID: 17836 RVA: 0x00105C7C File Offset: 0x00103E7C
	public bool ShiftPressed
	{
		get
		{
			return this.shiftPressed;
		}
		set
		{
			this.shiftPressed = value;
		}
	}

	// Token: 0x17000D75 RID: 3445
	// (get) Token: 0x060045AD RID: 17837 RVA: 0x00105C88 File Offset: 0x00103E88
	// (set) Token: 0x060045AE RID: 17838 RVA: 0x00105C90 File Offset: 0x00103E90
	public global::dfComponentMemberInfo Target
	{
		get
		{
			return this.target;
		}
		set
		{
			if (this.isBound)
			{
				this.Unbind();
			}
			this.target = value;
		}
	}

	// Token: 0x060045AF RID: 17839 RVA: 0x00105CAC File Offset: 0x00103EAC
	public void Awake()
	{
	}

	// Token: 0x060045B0 RID: 17840 RVA: 0x00105CB0 File Offset: 0x00103EB0
	public void OnEnable()
	{
	}

	// Token: 0x060045B1 RID: 17841 RVA: 0x00105CB4 File Offset: 0x00103EB4
	public void Start()
	{
		if (this.control != null && this.target.IsValid)
		{
			this.Bind();
		}
	}

	// Token: 0x060045B2 RID: 17842 RVA: 0x00105CE0 File Offset: 0x00103EE0
	public void Bind()
	{
		if (this.isBound)
		{
			this.Unbind();
		}
		if (this.control != null)
		{
			this.control.KeyDown += this.eventSource_KeyDown;
		}
		this.isBound = true;
	}

	// Token: 0x060045B3 RID: 17843 RVA: 0x00105D30 File Offset: 0x00103F30
	public void Unbind()
	{
		if (this.control != null)
		{
			this.control.KeyDown -= this.eventSource_KeyDown;
		}
		this.isBound = false;
	}

	// Token: 0x060045B4 RID: 17844 RVA: 0x00105D64 File Offset: 0x00103F64
	private void eventSource_KeyDown(global::dfControl control, global::dfKeyEventArgs args)
	{
		if (args.KeyCode != this.keyCode)
		{
			return;
		}
		if (args.Shift != this.shiftPressed || args.Control != this.controlPressed || args.Alt != this.altPressed)
		{
			return;
		}
		MethodInfo method = this.target.GetMethod();
		method.Invoke(this.target.Component, null);
	}

	// Token: 0x0400249E RID: 9374
	[SerializeField]
	protected global::dfControl control;

	// Token: 0x0400249F RID: 9375
	[SerializeField]
	protected KeyCode keyCode;

	// Token: 0x040024A0 RID: 9376
	[SerializeField]
	protected bool shiftPressed;

	// Token: 0x040024A1 RID: 9377
	[SerializeField]
	protected bool altPressed;

	// Token: 0x040024A2 RID: 9378
	[SerializeField]
	protected bool controlPressed;

	// Token: 0x040024A3 RID: 9379
	[SerializeField]
	protected global::dfComponentMemberInfo target;

	// Token: 0x040024A4 RID: 9380
	private bool isBound;
}
