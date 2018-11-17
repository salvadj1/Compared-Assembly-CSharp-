using System;
using UnityEngine;

// Token: 0x0200035A RID: 858
public class ReverseSurfaceShader : ScriptableObject
{
	// Token: 0x04000F79 RID: 3961
	public Shader inputShader;

	// Token: 0x04000F7A RID: 3962
	public Shader outputShader;

	// Token: 0x04000F7B RID: 3963
	public string outputShaderName;

	// Token: 0x04000F7C RID: 3964
	public bool pragmaDebug;

	// Token: 0x04000F7D RID: 3965
	public ShaderMod[] mods;
}
