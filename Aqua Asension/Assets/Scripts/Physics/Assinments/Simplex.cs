using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Simplex : MonoBehaviour
{
private GameObject ObjectA;
private GameObject ObjectB;
    Vector3 D;
    Vector3 dirTwo; 

    Vector3 A;
    Vector3 B;
    Vector3 C; 
    Vector3 AB;
    Vector3 AC;
    Vector3 AO;
    Vector3 O = new Vector3(0, 0, 0); 

private void Awake()
{
    ObjectA = GameObject.FindGameObjectWithTag("ParOne");
    ObjectB = GameObject.FindGameObjectWithTag("ParTwo");
        Debug.Log("First object is: " + ObjectA);
        Debug.Log("Second object is: " + ObjectB); 
}
    void Update()
    {



        FirstPoint();
        SecondPoint(); 
    }

    public void FirstPoint()
    {
        bool Ap1 = false; bool Ap2 = false; bool Ap3 = false; bool Ap4 = false; bool Ap5 = false;
        bool Bp1 = false; bool Bp2 = false; bool Bp3 = false; bool Bp4 = false; bool Bp5 = false;

        float X1 = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS.x + ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS.x +
            ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS.x + ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS.x) / 4;

        //Debug.Log("x1 is: " + X1); 

        float Y1 = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS.y + ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS.y +
            ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS.y + ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS.y) / 4;

        float Z1 = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS.z + ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS.z +
            ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS.z + ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS.z) / 4;



        float X2 = (ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS.x + ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS.x +
                   ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS.x + ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS.x) / 4;

        float Y2 = (ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS.y + ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS.y +
            ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS.y + ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS.y) / 4;

        float Z2 = (ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS.z + ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS.z +
            ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS.z + ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS.z) / 4;

        //The direction
        D = new Vector3(X2 - X1, Y2 - Y1, Z2 - Z1);

        //find dot product for each vertex with direction. 
        float AD1 = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS.x * D.x) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS.y * D.y) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS.z * D.z);

        float AD2 = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS.x * D.x) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS.y * D.y) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS.z * D.z);

        float AD3 = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS.x * D.x) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS.y * D.y) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS.z * D.z);

        float AD4 = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS.x * D.x) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS.y * D.y) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS.z * D.z);


        //find the frist point by calculation Minkowski difference
        if (AD1 > AD2 && AD1 > AD3 && AD1 > AD4)
        {
            Ap1 = true;

        }
        if (AD2 > AD1 && AD2 > AD3 && AD2 > AD4)
        {
            Ap2 = true;

        }
        if (AD3 > AD2 && AD3 > AD1 && AD3 > AD4)
        {
            Ap3 = true;

        }
        if (AD4 > AD2 && AD4 > AD3 && AD4 > AD1)
        {
            Ap4 = true;

        }
        //dot product with dir for object b vertexs
        float BD1 = (ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS.x * D.x) +
           (ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS.y * D.y) +
           (ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS.z * D.z);

        float BD2 = (ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS.x * D.x) +
            (ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS.y * D.y) +
            (ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS.z * D.z);

        float BD3 = (ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS.x * D.x) +
            (ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS.y * D.y) +
            (ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS.z * D.z);

        float BD4 = (ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS.x * D.x) +
            (ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS.y * D.y) +
            (ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS.z * D.z);

        //find second point
        if (BD1 < BD2 && BD1 < BD3 && BD1 < BD4)
        {
            Bp1 = true;

        }
        if (BD2 < BD1 && BD2 < BD3 && BD2 < BD4)
        {
            Bp2 = true;

        }
        if (AD3 < BD2 && BD3 < BD1 && BD3 < BD4)
        {
            Bp3 = true;

        }
        if (BD4 < BD2 && BD4 < BD3 && BD4 < BD1)
        {
            Bp4 = true;

        }

        //finding minkowski difference. 
        if(Ap1 == true && Bp1 == true)
        {
            A =  ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
            ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS;

        }
        if (Ap1 == true && Bp2 == true)
        {
            A = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS;

        }
        if (Ap1 == true && Bp3 == true)
        {
            A = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;

        }
        if (Ap1 == true && Bp3 == true)
        {
            A = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;

        }

        if (Ap1 == true && Bp4 == true)
        {
            A = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS;

        }
        if (Ap2 == true && Bp1 == true)
        {
            A = ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS;

        }
        if (Ap2 == true && Bp2 == true)
        {
            A = ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS;

        }
        if (Ap2 == true && Bp3 == true)
        {
            A = ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;

        }
        if (Ap2 == true && Bp4 == true)
        {
            A = ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS;

        }
        if (Ap3 == true && Bp1 == true)
        {
            A = ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS;

        }
        if (Ap3 == true && Bp2 == true)
        {
            A = ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS;

        }

        if (Ap3 == true && Bp3 == true)
        {
            A = ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;

        }
        if (Ap3 == true && Bp4 == true)
        {
            A = ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS;

        }
        if (Ap4 == true && Bp1 == true)
        {
            A = ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS;

        }
        if (Ap4 == true && Bp2 == true)
        {
            A = ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS;

        }
        if (Ap4 == true && Bp3 == true)
        {
            A = ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;

        }
        if (Ap4 == true && Bp4 == true)
        {
            A = ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS;

        }
        
    }

    //Use the oposite direction
    public void SecondPoint()
    {
        bool Apf1 = false; bool Apf2 = false; bool Apf3 = false; bool Apf4 = false;
        bool Bpf1 = false; bool Bpf2 = false; bool Bpf3 = false; bool Bpf4 = false;

        float ADf1 = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS.x * -D.x) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS.y * -D.y) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS.z * -D.z);

        float ADf2 = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS.x * -D.x) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS.y * -D.y) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS.z * -D.z);

        float ADf3 = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS.x * -D.x) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS.y * -D.y) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS.z * -D.z);

        float ADf4 = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS.x * -D.x) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS.y * -D.y) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS.z * -D.z);

        //find the frist point
        if (ADf1 > ADf2 && ADf1 > ADf3 && ADf1 > ADf4)
        {
            Apf1 = true;

        }
        if (ADf2 > ADf1 && ADf2 > ADf3 && ADf2 > ADf4)
        {
            Apf2 = true;

        }
        if (ADf3 > ADf2 && ADf3 > ADf1 && ADf3 > ADf4)
        {
            Apf3 = true;

        }
        if (ADf4 > ADf2 && ADf4 > ADf3 && ADf4 > ADf1)
        {
            Apf4 = true;

        }
        //dot product with dir for object b vertexs
        float BDf1 = (ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS.x * -D.x) +
           (ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS.y * -D.y) +
           (ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS.z * -D.z);

        float BDf2 = (ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS.x * -D.x) +
            (ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS.y * -D.y) +
            (ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS.z * -D.z);

        float BDf3 = (ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS.x * -D.x) +
            (ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS.y * -D.y) +
            (ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS.z * -D.z);

        float BDf4 = (ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS.x * -D.x) +
            (ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS.y * -D.y) +
            (ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS.z * -D.z);


        //find second point
        if (BDf1 < BDf2 && BDf1 < BDf3 && BDf1 < BDf4)
        {
            Bpf1 = true;

        }
        if (BDf2 < BDf1 && BDf2 < BDf3 && BDf2 < BDf4)
        {
            Bpf2 = true;

        }
        if (ADf3 < BDf2 && BDf3 < BDf1 && BDf3 < BDf4)
        {
            Bpf3 = true;

        }
        if (BDf4 < BDf2 && BDf4 < BDf3 && BDf4 < BDf1)
        {
            Bpf4 = true;

        }

        if (Apf1 == true && Bpf1 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
            ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS;

        }
        if (Apf1 == true && Bpf2 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS;

        }
        if (Apf1 == true && Bpf3 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;

        }
        if (Apf1 == true && Bpf3 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;

        }

        if (Apf1 == true && Bpf4 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS;

        }
        if (Apf2 == true && Bpf1 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS;

        }
        if (Apf2 == true && Bpf2 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS;

        }
        if (Apf2 == true && Bpf3 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;

        }
        if (Apf2 == true && Bpf4 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS;

        }
        if (Apf3 == true && Bpf1 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS;

        }
        if (Apf3 == true && Bpf2 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS;

        }

        if (Apf3 == true && Bpf3 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;

        }
        if (Apf3 == true && Bpf4 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS;

        }
        if (Apf4 == true && Bpf1 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS;

        }
        if (Apf4 == true && Bpf2 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS;

        }
        if (Apf4 == true && Bpf3 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;

        }
        if (Apf4 == true && Bpf4 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS;

        }
    }

    public void ThirdPoint()
    {
        bool Ap1q = false; bool Ap2q = false; bool Ap3q = false; bool Ap4q = false; bool Ap5q = false;
        bool Bp1q = false; bool Bp2q = false; bool Bp3q = false; bool Bp4q = false; bool Bp5q = false;

        AB = B - -A;
        AO = A - -O;

        Vector3 dir3 = Vector3.Cross(AB, AO);
        dir3 = Vector3.Cross(dir3, AB);

        float AD1q = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS.x * dir3.x) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS.y * dir3.y) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS.z * dir3.z);

        float AD2q = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS.x * dir3.x) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS.y * dir3.y) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS.z * dir3.z);

        float AD3q = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS.x * dir3.x) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS.y * dir3.y) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS.z * dir3.z);

        float AD4q = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS.x * dir3.x) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS.y * dir3.y) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS.z * dir3.z);

        if (AD1q > AD2q && AD1q > AD3q && AD1q > AD4q)
        {
            Ap1q = true;

        }
        if (AD2q > AD1q && AD2q > AD3q && AD2q > AD4q)
        {
            Ap2q = true;

        }
        if (AD3q > AD2q && AD3q > AD1q && AD3q > AD4q)
        {
            Ap3q = true;

        }
        if (AD4q > AD2q && AD4q > AD3q && AD4q > AD1q)
        {
            Ap4q = true;

        }

        //dot product with dir for object b vertexs
        float BD1q = (ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS.x * dir3.x) +
           (ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS.y * dir3.y) +
           (ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS.z * dir3.z);

        float BD2q = (ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS.x * dir3.x) +
            (ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS.y * dir3.y) +
            (ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS.z * dir3.z);

        float BD3q = (ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS.x * dir3.x) +
            (ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS.y * dir3.y) +
            (ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS.z * dir3.z);

        float BD4q = (ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS.x * dir3.x) +
            (ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS.y * dir3.y) +
            (ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS.z * dir3.z);
        if (BD1q < BD2q && BD1q < BD3q && BD1q < BD4q)
        {
            Bp1q = true;

        }
        if (BD2q < BD1q && BD2q < BD3q && BD2q < BD4q)
        {
            Bp2q = true;

        }
        if (AD3q < BD2q && BD3q < BD1q && BD3q < BD4q)
        {
            Bp3q = true;

        }
        if (BD4q < BD2q && BD4q < BD3q && BD4q < BD1q)
        {
            Bp4q = true;

        }

        if (Ap1q == true && Bp1q == true)
        {
            C = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
            ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS;

        }
        if (Ap1q == true && Bp2q == true)
        {
            C = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS;

        }
        if (Ap1q == true && Bp3q == true)
        {
            C = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;

        }
        if (Ap1q == true && Bp3q == true)
        {
            C = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;
        }

        if (Ap1q == true && Bp4q == true)
        {
            C = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS;
        }
        if (Ap2q == true && Bp1q == true)
        {
            C = ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS;
        }
        if (Ap2q == true && Bp2q == true)
        {
            C = ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS;
        }
        if (Ap2q == true && Bp3q == true)
        {
            C = ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;

        }
        if (Ap2q == true && Bp4q == true)
        {
            C = ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS;

        }
        if (Ap3q == true && Bp1q == true)
        {
            C = ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS;

        }
        if (Ap3q == true && Bp2q == true)
        {
            C = ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS;

        }

        if (Ap3q == true && Bp3q == true)
        {
            C = ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;

        }
        if (Ap3q == true && Bp4q == true)
        {
            C = ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS;

        }
        if (Ap4q == true && Bp1q == true)
        {
            C = ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS;

        }
        if (Ap4q == true && Bp2q == true)
        {
            C = ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS;

        }
        if (Ap4q == true && Bp3q == true)
        {
            C = ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;

        }
        if (Ap4q == true && Bp4q == true)
        {
            C = ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS;

        }

        Vector3 AC = C - -A;
        Vector3 Normal = Vector3.Cross(AB, AC);
        Vector3 Normal2 = -Normal; 
    }
}
