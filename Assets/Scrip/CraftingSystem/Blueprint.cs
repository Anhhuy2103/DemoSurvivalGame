using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint 
{
    public string BPName;

    public string BpReq1;
    public string BpReq2;

    public int BPReqamount1;
    public int BPReqamount2;

    public int totalOfRequirements; //  check co bao nhieu yeu cau ve nguyen lieu

    // tao construc 
    public Blueprint(string name, int TotalNUM, string R1, int r1NUM, string R2, int R2num)
    {
        BPName = name;
        totalOfRequirements = TotalNUM;

        BpReq1 = R1;
        BpReq2 = R2;

        BPReqamount1 = r1NUM;
        BPReqamount2 = R2num;
    }
}
