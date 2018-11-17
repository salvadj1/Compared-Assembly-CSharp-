using System;
using UnityEngine;

// Token: 0x02000796 RID: 1942
[AddComponentMenu("Daikon Forge/User Interface/GUI Camera")]
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
[Serializable]
public class dfGUICamera : MonoBehaviour
{
	// Token: 0x06004131 RID: 16689 RVA: 0x000EEC14 File Offset: 0x000ECE14
	public void Awake()
	{
	}

	// Token: 0x06004132 RID: 16690 RVA: 0x000EEC18 File Offset: 0x000ECE18
	public void OnEnable()
	{
	}

	// Token: 0x06004133 RID: 16691 RVA: 0x000EEC1C File Offset: 0x000ECE1C
	public void Start()
	{
		base.camera.transparencySortMode = 2;
		base.camera.useOcclusionCulling = false;
		base.camera.eventMask &= ~base.camera.cullingMask;
	}
}
