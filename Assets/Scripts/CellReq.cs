using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellReq : MonoBehaviour
{
    public enum ReqType
    {
        Undefined,
        NotBlocked,
        Road,
        Blocks
    }

    public ReqType req;
}
