using System;
using UnityEngine;

// Token: 0x02000407 RID: 1031
public class ReverseSurfaceShader : ScriptableObject
{
	// Token: 0x040010DF RID: 4319
	public Shader inputShader;

	// Token: 0x040010E0 RID: 4320
	public Shader outputShader;

	// Token: 0x040010E1 RID: 4321
	public string outputShaderName;

	// Token: 0x040010E2 RID: 4322
	public bool pragmaDebug;

	// Token: 0x040010E3 RID: 4323
	public global::ShaderMod[] mods;
}
