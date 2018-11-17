using System;

// Token: 0x0200030A RID: 778
public enum NetError
{
	// Token: 0x04000E32 RID: 3634
	AlreadyConnectedToAnotherServer = -1,
	// Token: 0x04000E33 RID: 3635
	ApprovalDenied = 81,
	// Token: 0x04000E34 RID: 3636
	ConnectionBanned = 21,
	// Token: 0x04000E35 RID: 3637
	ConnectionFailed = 14,
	// Token: 0x04000E36 RID: 3638
	ConnectionTimeout = 70,
	// Token: 0x04000E37 RID: 3639
	CreateSocketOrThreadFailure = -2,
	// Token: 0x04000E38 RID: 3640
	DetectedDuplicatePlayerID = 23,
	// Token: 0x04000E39 RID: 3641
	EmptyConnectTarget = -4,
	// Token: 0x04000E3A RID: 3642
	IncompatibleVersions = 64,
	// Token: 0x04000E3B RID: 3643
	IncorrectParameters = -3,
	// Token: 0x04000E3C RID: 3644
	InternalDirectConnectFailed = -5,
	// Token: 0x04000E3D RID: 3645
	InvalidPassword = 22,
	// Token: 0x04000E3E RID: 3646
	IsAuthoritativeServer = 80,
	// Token: 0x04000E3F RID: 3647
	LimitedPlayers = 71,
	// Token: 0x04000E40 RID: 3648
	NATPunchthroughFailed = 63,
	// Token: 0x04000E41 RID: 3649
	NATTargetConnectionLost = 62,
	// Token: 0x04000E42 RID: 3650
	NATTargetNotConnected = 61,
	// Token: 0x04000E43 RID: 3651
	NoError = 0,
	// Token: 0x04000E44 RID: 3652
	ProxyServerNotEnabled = 92,
	// Token: 0x04000E45 RID: 3653
	ProxyServerOutOfPorts,
	// Token: 0x04000E46 RID: 3654
	ProxyTargetNotConnected = 90,
	// Token: 0x04000E47 RID: 3655
	ProxyTargetNotRegistered,
	// Token: 0x04000E48 RID: 3656
	RSAPublicKeyMismatch = 20,
	// Token: 0x04000E49 RID: 3657
	TooManyConnectedPlayers = 17,
	// Token: 0x04000E4A RID: 3658
	Facepunch_Kick_ServerRestarting = 128,
	// Token: 0x04000E4B RID: 3659
	Facepunch_Approval_Closed,
	// Token: 0x04000E4C RID: 3660
	Facepunch_Approval_TooManyConnectedPlayersNow,
	// Token: 0x04000E4D RID: 3661
	Facepunch_Approval_ConnectorAuthorizeException,
	// Token: 0x04000E4E RID: 3662
	Facepunch_Approval_ConnectorAuthorizeExecution,
	// Token: 0x04000E4F RID: 3663
	Facepunch_Approval_ConnectorDidNothing,
	// Token: 0x04000E50 RID: 3664
	Facepunch_Approval_ConnectorCreateFailure,
	// Token: 0x04000E51 RID: 3665
	Facepunch_Approval_ServerDoesNotSupportConnector,
	// Token: 0x04000E52 RID: 3666
	Facepunch_Approval_MissingServerManagement,
	// Token: 0x04000E53 RID: 3667
	Facepunch_Approval_ServerLoginException,
	// Token: 0x04000E54 RID: 3668
	Facepunch_Approval_DisposedWait,
	// Token: 0x04000E55 RID: 3669
	Facepunch_Approval_DisposedLimbo,
	// Token: 0x04000E56 RID: 3670
	Facepunch_Kick_MultipleConnections,
	// Token: 0x04000E57 RID: 3671
	Facepunch_Kick_Violation,
	// Token: 0x04000E58 RID: 3672
	Facepunch_Kick_RCON,
	// Token: 0x04000E59 RID: 3673
	Facepunch_Kick_Ban,
	// Token: 0x04000E5A RID: 3674
	Facepunch_Kick_BadName,
	// Token: 0x04000E5B RID: 3675
	Facepunch_Connector_InLimboState,
	// Token: 0x04000E5C RID: 3676
	Facepunch_Connector_WaitedLimbo,
	// Token: 0x04000E5D RID: 3677
	Facepunch_Connector_RoutineMoveException,
	// Token: 0x04000E5E RID: 3678
	Facepunch_Connector_RoutineYieldException,
	// Token: 0x04000E5F RID: 3679
	Facepunch_Connector_MissingFeatureImplementation,
	// Token: 0x04000E60 RID: 3680
	Facepunch_Connector_Cancelled,
	// Token: 0x04000E61 RID: 3681
	Facepunch_Connector_AuthFailure,
	// Token: 0x04000E62 RID: 3682
	Facepunch_Connector_AuthException,
	// Token: 0x04000E63 RID: 3683
	Facepunch_Connector_MultipleAttempts,
	// Token: 0x04000E64 RID: 3684
	Facepunch_Connector_VAC_Banned,
	// Token: 0x04000E65 RID: 3685
	Facepunch_Connector_AuthTimeout,
	// Token: 0x04000E66 RID: 3686
	Facepunch_Connector_Old,
	// Token: 0x04000E67 RID: 3687
	Facepunch_Connector_NoConnect,
	// Token: 0x04000E68 RID: 3688
	Facepunch_Connector_Invalid,
	// Token: 0x04000E69 RID: 3689
	Facepunch_Connector_Expired,
	// Token: 0x04000E6A RID: 3690
	Facepunch_Connector_ConnectedElsewhere,
	// Token: 0x04000E6B RID: 3691
	Facepunch_API_Failure,
	// Token: 0x04000E6C RID: 3692
	Facepunch_Whitelist_Failure
}
