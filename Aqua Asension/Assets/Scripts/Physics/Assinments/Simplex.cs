using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 


public class Simplex : MonoBehaviour
{
    private GameObject ObjectA;
    private GameObject ObjectB;
    Vector3 D;
    Vector3 dirTwo;

    Vector3 A;
    Vector3 B;
    Vector3 C;
    Vector3 DD;

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
        ThirdPoint();
        Forthpoint();
        Final(); 
    }

    public void FirstPoint()
    {
        bool Ap1 = false; bool Ap2 = false; bool Ap3 = false; bool Ap4 = false; bool Ap5 = false;
        bool Bp1 = false; bool Bp2 = false; bool Bp3 = false; bool Bp4 = false; bool Bp5 = false;
        //get x y z for dir in object a. 

        List<float> ObjectAValues = new List<float>() { 0, 0, 0 }; 
        float X1 = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS.x + ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS.x +
            ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS.x + ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS.x) / 4; 

        float Y1 = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS.y + ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS.y +
            ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS.y + ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS.y) / 4;

        float Z1 = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS.z + ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS.z +
            ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS.z + ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS.z) / 4;
        

        //get x y z for dir in object b.
        float X2 = (ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS.x + ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS.x +
                   ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS.x + ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS.x) / 4;

        float Y2 = (ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS.y + ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS.y +
            ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS.y + ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS.y) / 4;

        float Z2 = (ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS.z + ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS.z +
            ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS.z + ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS.z) / 4;

        //The direction
        D = new Vector3(X2 - X1, Y2 - Y1, Z2 - Z1);
        Debug.Log("direction is " + D);

        //find dot product for each vertex with direction. 
        float AD1 = DotPoduct(ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS.x,
            ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS.y,
            ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS.z, D); 

        float AD2 = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS.x * D.x) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS.y * D.y) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS.z * D.z);

        float AD3 = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS.x * D.x) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS.y * D.y) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS.z * D.z);

        float AD4 = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS.x * D.x) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS.y * D.y) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS.z * D.z);

        /*for(int i = 1; i <= 4; i++)
        {
            List<float> VertexDotProducts[i] = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS.x * D.x) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS.y * D.y) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS.z * D.z);
        }*/

        Debug.Log("ad1 " + AD1 + "Ad2 " + AD2 + "ad3 " + AD3 + "ad4 " + AD4);
        //find the frist point by calculation Minkowski difference 
        float[] ADValues = { AD1, AD2, AD3, AD4 };  
        
        /*for(int i = 0; i < 4; i++)
        {
            ADValues[i]
        }*/
        //use arry to return maximum and minimum
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

        List<float> BDValues = new List<float>() { BD1, BD2, BD3, BD4 };
        float BD = BDValues.Max(); 
        //Debug.Log("bd1 " + BD1 + "bd2" + BD2 + "bd3" + BD3 + "bd4" + BD4); 
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
        if (BD1 == BD2 && BD3 == BD4 && BD1 > BD3 && BD1 > BD4)
        {
            Bp1 = true;
            
        }

        //finding minkowski difference. 
        if (Ap1 == true && Bp1 == true)
        {
            A = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
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
        //temp note all of these are evaluating false. 
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



//Finding new direction of D
public void Forthpoint()
{




    Vector3 N;
    Vector3 N1;
    Vector3 N2;
    Vector3 T;
    Vector3 TO;
    Vector3 DiT;

    bool APt1 = false;
    bool APt2 = false;
    bool APt3 = false;
    bool APt4 = false;

    bool BPt1 = false;
    bool BPt2 = false;
    bool BPt3 = false;
    bool BPt4 = false;

    AC = C - -A;
    //find the normal of AB , AC.  (A,B,and C are the three other points from above)
    N.x = AB.x * AC.x;
    N.y = AB.y * AC.y;
    N.z = AB.z * AC.z;
    N1 = N;
    N2 = -N;


    T.x = (A.x + B.x + C.x) / 3;
    T.y = (A.y + B.y + C.y) / 3;
    T.z = (A.z + B.z + C.z) / 3;
    TO = -T;

    //DiT is equal to the closest point to the origin
    float N1T;
    float N2T;

    N1T = (N1.x * TO.x) + (N1.y * TO.y) + (N1.z * TO.z);
    N2T = (N2.x * TO.x) + (N2.y * TO.y) + (N2.z * TO.z);

    if (N1T < 0 && N1T < N2T)
    {

        DiT = N1;

    }
    else
    {

        DiT = N2;

    }

    //Find p1

    float AT1 = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS.x * DiT.x) +
        (ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS.y * DiT.y) +
        (ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS.z * DiT.z);

    float AT2 = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS.x * DiT.x) +
        (ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS.y * DiT.y) +
        (ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS.z * DiT.z);

    float AT3 = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS.x * DiT.x) +
        (ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS.y * DiT.y) +
        (ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS.z * DiT.z);

    float AT4 = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS.x * DiT.x) +
        (ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS.y * DiT.y) +
        (ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS.z * DiT.z);

    if (AT1 > AT2 && AT1 > AT3 && AT1 > AT4)
    {
        APt1 = true;
    }
    if (AT2 > AT1 && AT2 > AT3 && AT2 > AT4)
    {
        APt2 = true;
    }

    if (AT3 > AT2 && AT3 > AT1 && AT3 > AT4)
    {
        APt3 = true;
    }

    if (AT4 > AT2 && AT4 > AT3 && AT4 > AT1)
    {
        APt4 = true;
    }




    //find p2
    float BT1 = (ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS.x * DiT.x) +
      (ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS.y * DiT.y) +
      (ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS.z * DiT.z);

    float BT2 = (ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS.x * DiT.x) +
        (ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS.y * DiT.y) +
        (ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS.z * DiT.z);

    float BT3 = (ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS.x * DiT.x) +
        (ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS.y * DiT.y) +
        (ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS.z * DiT.z);

    float BT4 = (ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS.x * DiT.x) +
        (ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS.y * DiT.y) +
        (ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS.z * DiT.z);

    if (BT1 < BT2 && BT1 < BT3 && BT1 < BT4)
    {
        BPt1 = true;
    }
    if (BT2 < BT1 && BT2 < BT3 && BT2 < BT4)
    {
        BPt2 = true;
    }

    if (BT3 < BT2 && BT3 < BT1 && BT3 < BT4)
    {
        BPt3 = true;
    }

    if (BT4 < BT2 && BT4 < BT3 && BT4 < BT1)
    {
        BPt4 = true;
    }


    if (APt1 == true && BPt1 == true)
    {
        DD = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
        ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS;

    }
    if (APt1 == true && BPt2 == true)
    {
        DD = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
                    ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS;

    }
    if (APt1 == true && BPt3 == true)
    {
        DD = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
                    ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;

    }
    if (APt1 == true && BPt4 == true)
    {
        DD = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
                    ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS;

    }
    if (APt2 == true && BPt1 == true)
    {
        DD = ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS -
                    ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS;

    }
    if (APt2 == true && BPt2 == true)
    {
        DD = ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS -
                    ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS;

    }
    if (APt2 == true && BPt3 == true)
    {
        DD = ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS -
                    ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;

    }
    if (APt2 == true && BPt4 == true)
    {
        DD = ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS -
                    ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS;

    }
    if (APt3 == true && BPt1 == true)
    {
        DD = ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS -
                    ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS;

    }
    if (APt3 == true && BPt2 == true)
    {
        DD = ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS -
                    ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS;

    }

    if (APt3 == true && BPt3 == true)
    {
        DD = ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS -
                    ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;

    }
    if (APt3 == true && BPt4 == true)
    {
        DD = ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS -
                    ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS;

    }
    if (APt4 == true && BPt1 == true)
    {
        DD = ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS -
                    ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS;

    }
    if (APt4 == true && BPt2 == true)
    {
        DD = ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS -
                    ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS;

    }
    if (APt4 == true && BPt3 == true)
    {
        DD = ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS -
                    ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;

    }
    if (APt4 == true && BPt4 == true)
    {
        DD = ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS -
                    ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS;

    }



}

    //matracies good Luck
    public void Final()
    {
        float DV0 = (A.x * (B.y * ((C.z * 1) - (1 * DD.z)) - (B.z * ((C.y * 1) - (1 * DD.y))) + (1 * ((C.y * DD.z) - (DD.y * C.z))))
            - (A.y * (B.x * ((C.z * 1) - (DD.z * 1)) - B.z * (C.x * 1 - DD.x * 1) + (1 * (DD.x * C.z - DD.z * C.x))))
            + (A.z * (B.x * ((C.y * 1) - (DD.y * 1)) - (B.y * ((C.x * 1) - (DD.x * 1))) + 1 * ((C.x * DD.y) - (C.y * DD.x))))
            - (1 * (B.x * ((C.y * DD.z) - (C.z * DD.y)) - (B.y * ((C.x * DD.z) - (C.z * DD.x))) + (B.z * ((C.x * DD.y) - (C.y * DD.x))))));

        float DVA = (0 * (B.y * ((C.z * 1) - (1 * DD.z)) - (B.z * ((C.y * 1) - (1 * DD.y))) + (1 * ((C.y * DD.z) - (DD.y * C.z))))
         - (0 * (B.x * ((C.z * 1) - (DD.z * 1)) - B.z * (C.x * 1 - DD.x * 1) + (1 * (DD.x * C.z - DD.z * C.x))))
         + (0 * (B.x * ((C.y * 1) - (DD.y * 1)) - (B.y * ((C.x * 1) - (DD.x * 1))) + 1 * ((C.x * DD.y) - (C.y * DD.x))))
         - (1 * (B.x * ((C.y * DD.z) - (C.z * DD.y)) - (B.y * ((C.x * DD.z) - (C.z * DD.x))) + (B.z * ((C.x * DD.y) - (C.y * DD.x))))));


        float DVB = (A.x * (0 * ((C.z * 1) - (1 * DD.z)) - (0 * ((C.y * 1) - (1 * DD.y))) + (1 * ((C.y * DD.z) - (DD.y * C.z))))
           - (A.y * (0 * ((C.z * 1) - (DD.z * 1)) - 0 * (C.x * 1 - DD.x * 1) + (1 * (DD.x * C.z - DD.z * C.x))))
           + (A.z * (0 * ((C.y * 1) - (DD.y * 1)) - (0 * ((C.x * 1) - (DD.x * 1))) + 1 * ((C.x * DD.y) - (C.y * DD.x))))
           - (1 * (0 * ((C.y * DD.z) - (C.z * DD.y)) - (0 * ((C.x * DD.z) - (C.z * DD.x))) + (0 * ((C.x * DD.y) - (C.y * DD.x))))));


        float DVC = (A.x * (B.y * ((0 * 1) - (1 * DD.z)) - (B.z * ((0 * 1) - (1 * DD.y))) + (1 * ((0 * DD.z) - (DD.y * 0))))
           - (A.y * (B.x * ((0 * 1) - (DD.z * 1)) - B.z * (0 * 1 - DD.x * 1) + (1 * (DD.x * 0 - DD.z * 0))))
           + (A.z * (B.x * ((0 * 1) - (DD.y * 1)) - (B.y * ((0 * 1) - (DD.x * 1))) + 1 * ((0 * DD.y) - (0 * DD.x))))
           - (1 * (B.x * ((0 * DD.z) - (0 * DD.y)) - (B.y * ((0 * DD.z) - (0 * DD.x))) + (B.z * ((0 * DD.y) - (0 * DD.x))))));

        float DVD = (A.x * (B.y * ((C.z * 1) - (1 * 0)) - (B.z * ((C.y * 1) - (1 * 0))) + (1 * ((C.y * 0) - (0 * C.z))))
           - (A.y * (B.x * ((C.z * 1) - (0 * 1)) - B.z * (C.x * 1 - 0 * 1) + (1 * (0 * C.z - 0 * C.x))))
           + (A.z * (B.x * ((C.y * 1) - (0 * 1)) - (B.y * ((C.x * 1) - (0 * 1))) + 1 * ((C.x * 0) - (C.y * 0))))
           - (1 * (B.x * ((C.y * 0) - (C.z * 0)) - (B.y * ((C.x * 0) - (C.z * 0))) + (B.z * ((C.x * 0) - (C.y * 0))))));

        //Debug.Log("float DV0 " + DV0);
        //Debug.Log("float DVA " + DVA);
        //Debug.Log("float DVB " + DVB);
        //Debug.Log("float DVC " + DVC);
        //Debug.Log("float DVD " + DVD); 

        if (DV0 < 0)
        {
            if (DVA < 0 && DVB > 0 && DVC > 0 && DVD > 0)
            {
                Debug.Log("point of origin is inside the boundury of 0 and A");
            }

            if (DVA > 0 && DVB < 0 && DVC > 0 && DVD > 0)
            {
                Debug.Log("point of origin is inside the boundury of 0 and B");
            }
            if (DVA > 0 && DVB > 0 && DVC < 0 && DVD > 0)
            {
                Debug.Log("point of origin is inside the boundury of 0 and C");
            }
            if (DVA > 0 && DVB > 0 && DVC > 0 && DVD < 0)
            {
                Debug.Log("point of origin is inside the boundury of 0 and D");
            }



            if (DVA < 0 && DVB < 0 && DVC > 0 && DVD > 0)
            {
                Debug.Log("point of origin is inside the boundury of 0, A, and B");
            }
            if (DVA < 0 && DVB > 0 && DVC < 0 && DVD > 0)
            {
                Debug.Log("point of origin is inside the boundury of 0, A and C");
            }

            if (DVA < 0 && DVB > 0 && DVC > 0 && DVD < 0)
            {
                Debug.Log("point of origin is inside the boundury of 0, A,D");
            }

            if (DVA > 0 && DVB < 0 && DVC > 0 && DVD < 0)
            {
                Debug.Log("point of origin is inside the boundury of 0, B,D");
            }
            if (DVA > 0 && DVB < 0 && DVC < 0 && DVD > 0)
            {
                Debug.Log("point of origin is inside the boundury of 0, B,C");
            }
            if (DVA > 0 && DVB > 0 && DVC < 0 && DVD < 0)
            {
                Debug.Log("point of origin is inside the boundury of 0, C,D");
            }




            if (DVA < 0 && DVB < 0 && DVC < 0 && DVD > 0)
            {
                Debug.Log("point of origin is inside the boundury of 0, A, B, C");
            }
            if (DVA < 0 && DVB < 0 && DVC > 0 && DVD < 0)
            {
                Debug.Log("point of origin is inside the boundury of 0, A, B, D");
            }
            if (DVA < 0 && DVB > 0 && DVC < 0 && DVD < 0)
            {
                Debug.Log("point of origin is inside the boundury of 0, A, D, C");
            }
            if (DVA > 0 && DVB < 0 && DVC < 0 && DVD < 0)
            {
                Debug.Log("point of origin is inside the boundury of 0, D, B, C");
            }

            if (DVA < 0 && DVB < 0 && DVC < 0 && DVD < 0)
            {
                Debug.Log("point of origin is inside the boundury of all points");
            }

            if (DVA > 0 && DVB > 0 && DVC > 0 && DVD > 0)
            {
                Debug.Log("point of oragin is point " + DV0);
            }
        }
        else
        {

            if (DVA > 0 && DVB < 0 && DVC < 0 && DVD < 0)
            {
                Debug.Log("point of origin is inside the boundury of 0 and A");
            }

            if (DVA < 0 && DVB > 0 && DVC < 0 && DVD < 0)
            {
                Debug.Log("point of origin is inside the boundury of 0 and B");
            }
            if (DVA < 0 && DVB < 0 && DVC > 0 && DVD < 0)
            {
                Debug.Log("point of origin is inside the boundury of 0 and C");
            }
            if (DVA < 0 && DVB < 0 && DVC < 0 && DVD > 0)
            {
                Debug.Log("point of origin is inside the boundury of 0 and D");
            }



            if (DVA < 0 && DVB < 0 && DVC > 0 && DVD > 0)
            {
                Debug.Log("point of origin is inside the boundury of 0, C, and D");
            }
            if (DVA < 0 && DVB > 0 && DVC < 0 && DVD > 0)
            {
                Debug.Log("point of origin is inside the boundury of 0, B and D");
            }

            if (DVA < 0 && DVB > 0 && DVC > 0 && DVD < 0)
            {
                Debug.Log("point of origin is inside the boundury of 0, C,B");
            }

            if (DVA > 0 && DVB < 0 && DVC > 0 && DVD < 0)
            {
                Debug.Log("point of origin is inside the boundury of 0, A,C");
            }
            if (DVA > 0 && DVB < 0 && DVC < 0 && DVD > 0)
            {
                Debug.Log("point of origin is inside the boundury of 0, A,D");
            }
            if (DVA > 0 && DVB > 0 && DVC < 0 && DVD < 0)
            {
                Debug.Log("point of origin is inside the boundury of 0, A,B");
            }




            if (DVA > 0 && DVB > 0 && DVC > 0 && DVD < 0)
            {
                Debug.Log("point of origin is inside the boundury of 0, A, B, C");
            }
            if (DVA > 0 && DVB > 0 && DVC < 0 && DVD > 0)
            {
                Debug.Log("point of origin is inside the boundury of 0, A, B, D");
            }
            if (DVA > 0 && DVB < 0 && DVC > 0 && DVD > 0)
            {
                Debug.Log("point of origin is inside the boundury of 0, A, D, C");
            }
            if (DVA < 0 && DVB > 0 && DVC > 0 && DVD > 0)
            {
                Debug.Log("point of origin is inside the boundury of 0, D, B, C");
            }

            if (DVA > 0 && DVB > 0 && DVC > 0 && DVD > 0)
            {
                Debug.Log("point of origin is inside the boundury of all points");
            }
            if(DVA < 0 && DVB < 0 && DVC < 0 && DVD < 0)
            {
                Debug.Log("point of origin is point " + DV0); 
            }

        }

    }

    private float DotPoduct(float x, float y, float z, Vector3 VectorToDot)
    {
        float Result = (x * VectorToDot.x) + (y * VectorToDot.y) + (z * VectorToDot.z);

        return Result; 
    }

}


