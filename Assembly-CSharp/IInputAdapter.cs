using System;
using UnityEngine;

// Token: 0x020007A9 RID: 1961
public interface IInputAdapter
{
	// Token: 0x060041F9 RID: 16889
	bool GetKeyDown(KeyCode key);

	// Token: 0x060041FA RID: 16890
	bool GetKeyUp(KeyCode key);

	// Token: 0x060041FB RID: 16891
	float GetAxis(string axisName);

	// Token: 0x060041FC RID: 16892
	Vector2 GetMousePosition();

	// Token: 0x060041FD RID: 16893
	bool GetMouseButton(int button);

	// Token: 0x060041FE RID: 16894
	bool GetMouseButtonDown(int button);

	// Token: 0x060041FF RID: 16895
	bool GetMouseButtonUp(int button);
}
