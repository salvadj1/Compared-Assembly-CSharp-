using System;
using System.Reflection;
using UnityEngine;

// Token: 0x02000705 RID: 1797
[AddComponentMenu("Daikon Forge/Data Binding/Key Binding")]
[Serializable]
public class dfControlKeyBinding : MonoBehaviour, IDataBindingComponent
{
	// Token: 0x17000CE8 RID: 3304
	// (get) Token: 0x0600417B RID: 16763 RVA: 0x000FCD60 File Offset: 0x000FAF60
	// (set) Token: 0x0600417C RID: 16764 RVA: 0x000FCD68 File Offset: 0x000FAF68
	public dfControl Control
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

	// Token: 0x17000CE9 RID: 3305
	// (get) Token: 0x0600417D RID: 16765 RVA: 0x000FCD84 File Offset: 0x000FAF84
	// (set) Token: 0x0600417E RID: 16766 RVA: 0x000FCD8C File Offset: 0x000FAF8C
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

	// Token: 0x17000CEA RID: 3306
	// (get) Token: 0x0600417F RID: 16767 RVA: 0x000FCD98 File Offset: 0x000FAF98
	// (set) Token: 0x06004180 RID: 16768 RVA: 0x000FCDA0 File Offset: 0x000FAFA0
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

	// Token: 0x17000CEB RID: 3307
	// (get) Token: 0x06004181 RID: 16769 RVA: 0x000FCDAC File Offset: 0x000FAFAC
	// (set) Token: 0x06004182 RID: 16770 RVA: 0x000FCDB4 File Offset: 0x000FAFB4
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

	// Token: 0x17000CEC RID: 3308
	// (get) Token: 0x06004183 RID: 16771 RVA: 0x000FCDC0 File Offset: 0x000FAFC0
	// (set) Token: 0x06004184 RID: 16772 RVA: 0x000FCDC8 File Offset: 0x000FAFC8
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

	// Token: 0x17000CED RID: 3309
	// (get) Token: 0x06004185 RID: 16773 RVA: 0x000FCDD4 File Offset: 0x000FAFD4
	// (set) Token: 0x06004186 RID: 16774 RVA: 0x000FCDDC File Offset: 0x000FAFDC
	public dfComponentMemberInfo Target
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

	// Token: 0x06004187 RID: 16775 RVA: 0x000FCDF8 File Offset: 0x000FAFF8
	public void Awake()
	{
	}

	// Token: 0x06004188 RID: 16776 RVA: 0x000FCDFC File Offset: 0x000FAFFC
	public void OnEnable()
	{
	}

	// Token: 0x06004189 RID: 16777 RVA: 0x000FCE00 File Offset: 0x000FB000
	public void Start()
	{
		if (this.control != null && this.target.IsValid)
		{
			this.Bind();
		}
	}

	// Token: 0x0600418A RID: 16778 RVA: 0x000FCE2C File Offset: 0x000FB02C
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

	// Token: 0x0600418B RID: 16779 RVA: 0x000FCE7C File Offset: 0x000FB07C
	public void Unbind()
	{
		if (this.control != null)
		{
			this.control.KeyDown -= this.eventSource_KeyDown;
		}
		this.isBound = false;
	}

	// Token: 0x0600418C RID: 16780 RVA: 0x000FCEB0 File Offset: 0x000FB0B0
	private void eventSource_KeyDown(dfControl control, dfKeyEventArgs args)
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

	// Token: 0x0400228E RID: 8846
	[SerializeField]
	protected dfControl control;

	// Token: 0x0400228F RID: 8847
	[SerializeField]
	protected KeyCode keyCode;

	// Token: 0x04002290 RID: 8848
	[SerializeField]
	protected bool shiftPressed;

	// Token: 0x04002291 RID: 8849
	[SerializeField]
	protected bool altPressed;

	// Token: 0x04002292 RID: 8850
	[SerializeField]
	protected bool controlPressed;

	// Token: 0x04002293 RID: 8851
	[SerializeField]
	protected dfComponentMemberInfo target;

	// Token: 0x04002294 RID: 8852
	private bool isBound;
}
