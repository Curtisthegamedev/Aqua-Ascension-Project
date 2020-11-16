﻿using System.Collections;
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
    Vector3 AB;
    Vector3 AO;
    Vector3 O = new Vector3(0, 0, 0); 

private void Awake()
{
    ObjectA = GameObject.FindGameObjectWithTag("ParOne");
    ObjectB = GameObject.FindGameObjectWithTag("ParTwo");
}
    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {



        FirstPoint();
        SecondPoint(); 
    }

    private void FirstPoint()
    {
        bool Ap1 = false; bool Ap2 = false; bool Ap3 = false; bool Ap4 = false; bool Ap5 = false;
        bool Bp1 = false; bool Bp2 = false; bool Bp3 = false; bool Bp4 = false; bool Bp5 = false;

        float X1 = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS.x + ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS.x +
            ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS.x + ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS.x +
           ObjectA.GetComponent<PrintVertexPositions>().vertexPos5WS.x) / 4;

        Debug.Log("x1 is: " + X1); 

        float Y1 = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS.y + ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS.y +
            ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS.y + ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS.y +
           ObjectA.GetComponent<PrintVertexPositions>().vertexPos5WS.y) / 4;

        float Z1 = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS.z + ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS.z +
            ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS.z + ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS.z +
           ObjectA.GetComponent<PrintVertexPositions>().vertexPos5WS.z) / 4;



        float X2 = (ObjectA.GetComponent<PrintVertexB>().vertexPosB1WS.x + ObjectA.GetComponent<PrintVertexB>().vertexPosB2WS.x +
                   ObjectA.GetComponent<PrintVertexB>().vertexPosB3WS.x + ObjectA.GetComponent<PrintVertexB>().vertexPosB4WS.x +
                  ObjectA.GetComponent<PrintVertexB>().vertexPosB5WS.x) / 4;

        float Y2 = (ObjectA.GetComponent<PrintVertexB>().vertexPosB1WS.y + ObjectA.GetComponent<PrintVertexB>().vertexPosB2WS.y +
            ObjectA.GetComponent<PrintVertexB>().vertexPosB3WS.y + ObjectA.GetComponent<PrintVertexB>().vertexPosB4WS.y +
           ObjectA.GetComponent<PrintVertexB>().vertexPosB5WS.y) / 4;

        float Z2 = (ObjectA.GetComponent<PrintVertexB>().vertexPosB1WS.z + ObjectA.GetComponent<PrintVertexB>().vertexPosB2WS.z +
            ObjectA.GetComponent<PrintVertexB>().vertexPosB3WS.z + ObjectA.GetComponent<PrintVertexB>().vertexPosB4WS.z +
           ObjectA.GetComponent<PrintVertexB>().vertexPosB5WS.z) / 4;

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

        float AD5 = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos5WS.x * D.x) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos5WS.y * D.y) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos5WS.z * D.z);

        //find the frist point
        if (AD1 > AD2 && AD1 > AD3 && AD1 > AD4 && AD1 > AD5)
        {
            Ap1 = true;

        }
        if (AD2 > AD1 && AD2 > AD3 && AD2 > AD4 && AD2 > AD5)
        {
            Ap2 = true;

        }
        if (AD3 > AD2 && AD3 > AD1 && AD3 > AD4 && AD3 > AD5)
        {
            Ap3 = true;

        }
        if (AD4 > AD2 && AD4 > AD3 && AD4 > AD1 && AD4 > AD5)
        {
            Ap4 = true;

        }
        if (AD5 > AD2 && AD5 > AD3 && AD5 > AD4 && AD5 > AD1)
        {
            Ap5 = true;

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

        float BD5 = (ObjectB.GetComponent<PrintVertexB>().vertexPosB5WS.x * D.x) +
            (ObjectB.GetComponent<PrintVertexB>().vertexPosB5WS.y * D.y) +
            (ObjectB.GetComponent<PrintVertexB>().vertexPosB5WS.z * D.z);


        //find second point
        if (BD1 < BD2 && BD1 < BD3 && BD1 < BD4 && BD1 < BD5)
        {
            Bp1 = true;

        }
        if (BD2 < BD1 && BD2 < BD3 && BD2 < BD4 && BD2 < BD5)
        {
            Bp2 = true;

        }
        if (AD3 < BD2 && BD3 < BD1 && BD3 < BD4 && BD3 < BD5)
        {
            Bp3 = true;

        }
        if (BD4 < BD2 && BD4 < BD3 && BD4 < BD1 && BD4 < BD5)
        {
            Bp4 = true;

        }
        if (BD5 < BD2 && BD5 < BD3 && BD5 < BD4 && BD5 < BD1)
        {
            Bp5 = true;

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
        if (Ap1 == true && Bp5 == true)
        {
            A = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB5WS;

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
        if (Ap2 == true && Bp5 == true)
        {
            A = ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB5WS;

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
        if (Ap3 == true && Bp5 == true)
        {
            A = ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB5WS;

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
        if (Ap4 == true && Bp5 == true)
        {
            A = ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB5WS;

        }
        if (Ap5 == true && Bp1 == true)
        {
            A = ObjectA.GetComponent<PrintVertexPositions>().vertexPos5WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS;

        }
        if (Ap5 == true && Bp1 == true)
        {
            A = ObjectA.GetComponent<PrintVertexPositions>().vertexPos5WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS;

        }
        if (Ap5 == true && Bp2 == true)
        {
            A = ObjectA.GetComponent<PrintVertexPositions>().vertexPos5WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS;

        }
        if (Ap5 == true && Bp3 == true)
        {
            A = ObjectA.GetComponent<PrintVertexPositions>().vertexPos5WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;

        }
        if (Ap5 == true && Bp4 == true)
        {
            A = ObjectA.GetComponent<PrintVertexPositions>().vertexPos5WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS;

        }
        if (Ap5 == true && Bp5 == true)
        {
            A = ObjectA.GetComponent<PrintVertexPositions>().vertexPos5WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB5WS;
        }
    }

    //Use the oposite direction
    private void SecondPoint()
    {
        bool Ap1 = false; bool Ap2 = false; bool Ap3 = false; bool Ap4 = false; bool Ap5 = false;
        bool Bp1 = false; bool Bp2 = false; bool Bp3 = false; bool Bp4 = false; bool Bp5 = false;

        float AD1 = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS.x * -D.x) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS.y * -D.y) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS.z * -D.z);

        float AD2 = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS.x * -D.x) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS.y * -D.y) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS.z * -D.z);

        float AD3 = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS.x * -D.x) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS.y * -D.y) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS.z * -D.z);

        float AD4 = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS.x * -D.x) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS.y * -D.y) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS.z * -D.z);

        float AD5 = (ObjectA.GetComponent<PrintVertexPositions>().vertexPos5WS.x * -D.x) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos5WS.y * -D.y) +
            (ObjectA.GetComponent<PrintVertexPositions>().vertexPos5WS.z * -D.z);

        //find the frist point
        if (AD1 > AD2 && AD1 > AD3 && AD1 > AD4 && AD1 > AD5)
        {
            Ap1 = true;

        }
        if (AD2 > AD1 && AD2 > AD3 && AD2 > AD4 && AD2 > AD5)
        {
            Ap2 = true;

        }
        if (AD3 > AD2 && AD3 > AD1 && AD3 > AD4 && AD3 > AD5)
        {
            Ap3 = true;

        }
        if (AD4 > AD2 && AD4 > AD3 && AD4 > AD1 && AD4 > AD5)
        {
            Ap4 = true;

        }
        if (AD5 > AD2 && AD5 > AD3 && AD5 > AD4 && AD5 > AD1)
        {
            Ap5 = true;

        }
        //dot product with dir for object b vertexs
        float BD1 = (ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS.x * -D.x) +
           (ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS.y * -D.y) +
           (ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS.z * -D.z);

        float BD2 = (ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS.x * -D.x) +
            (ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS.y * -D.y) +
            (ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS.z * -D.z);

        float BD3 = (ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS.x * -D.x) +
            (ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS.y * -D.y) +
            (ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS.z * -D.z);

        float BD4 = (ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS.x * -D.x) +
            (ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS.y * -D.y) +
            (ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS.z * -D.z);

        float BD5 = (ObjectB.GetComponent<PrintVertexB>().vertexPosB5WS.x * -D.x) +
            (ObjectB.GetComponent<PrintVertexB>().vertexPosB5WS.y * -D.y) +
            (ObjectB.GetComponent<PrintVertexB>().vertexPosB5WS.z * -D.z);


        //find second point
        if (BD1 < BD2 && BD1 < BD3 && BD1 < BD4 && BD1 < BD5)
        {
            Bp1 = true;

        }
        if (BD2 < BD1 && BD2 < BD3 && BD2 < BD4 && BD2 < BD5)
        {
            Bp2 = true;

        }
        if (AD3 < BD2 && BD3 < BD1 && BD3 < BD4 && BD3 < BD5)
        {
            Bp3 = true;

        }
        if (BD4 < BD2 && BD4 < BD3 && BD4 < BD1 && BD4 < BD5)
        {
            Bp4 = true;

        }
        if (BD5 < BD2 && BD5 < BD3 && BD5 < BD4 && BD5 < BD1)
        {
            Bp5 = true;

        }

        if (Ap1 == true && Bp1 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
            ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS;

        }
        if (Ap1 == true && Bp2 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS;

        }
        if (Ap1 == true && Bp3 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;

        }
        if (Ap1 == true && Bp3 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;

        }

        if (Ap1 == true && Bp4 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS;

        }
        if (Ap1 == true && Bp5 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB5WS;

        }
        if (Ap2 == true && Bp1 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS;

        }
        if (Ap2 == true && Bp2 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS;

        }
        if (Ap2 == true && Bp3 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;

        }
        if (Ap2 == true && Bp4 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS;

        }
        if (Ap2 == true && Bp5 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB5WS;

        }

        if (Ap3 == true && Bp1 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS;

        }
        if (Ap3 == true && Bp2 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS;

        }

        if (Ap3 == true && Bp3 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;

        }
        if (Ap3 == true && Bp4 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS;

        }
        if (Ap3 == true && Bp5 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB5WS;

        }
        if (Ap4 == true && Bp1 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS;

        }
        if (Ap4 == true && Bp2 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS;

        }
        if (Ap4 == true && Bp3 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;

        }
        if (Ap4 == true && Bp4 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS;

        }
        if (Ap4 == true && Bp5 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB5WS;

        }
        if (Ap5 == true && Bp1 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos5WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS;

        }
        if (Ap5 == true && Bp1 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos5WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS;

        }
        if (Ap5 == true && Bp2 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos5WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS;

        }
        if (Ap5 == true && Bp3 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos5WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;

        }
        if (Ap5 == true && Bp4 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos5WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS;

        }
        if (Ap5 == true && Bp5 == true)
        {
            B = ObjectA.GetComponent<PrintVertexPositions>().vertexPos5WS -
                        ObjectB.GetComponent<PrintVertexB>().vertexPosB5WS;
        }
    }

    private void ThirdPoint()
    {
        AB = B - A;
        AO = A - O;
        Vector3 temp = new Vector3(AB.x * AO.x, AB.y * AO.y, AB.z * AO.z);
        dirTwo = new Vector3(temp.x * AB.x, temp.y * AB.y, temp.z * AB.z); 
    }
}
