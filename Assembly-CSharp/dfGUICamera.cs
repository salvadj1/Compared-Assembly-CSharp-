using System;
using UnityEngine;

// Token: 0x020006CB RID: 1739
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Daikon Forge/User Interface/GUI Camera")]
[Serializable]
public class dfGUICamera : MonoBehaviour
{
	// Token: 0x06003D27 RID: 15655 RVA: 0x000E60D0 File Offset: 0x000E42D0
	public void Awake()
	{
	}

	// Token: 0x06003D28 RID: 15656 RVA: 0x000E60D4 File Offset: 0x000E42D4
	public void OnEnable()
	{
	}

	// Token: 0x06003D29 RID: 15657 RVA: 0x000E60D8 File Offset: 0x000E42D8
	public void Start()
	{
		base.camera.transparencySortMode = 2;
		base.camera.useOcclusionCulling = false;
		base.camera.eventMask &= ~base.camera.cullingMask;
	}
}
