using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace SuperKoule
{
    class Map
    {

        Point gridSize;

        int objToDraw;

        List<GameObject> mapObjects;     //all objects
        List<GameObject> movableObjects; //enemies, hero, movable platforms, movable bonuses
        //List<HerniObjekt> staticObjects;  //platforms, bonuses
        GameObject[] platfObjects;   //static brics
        List<GameObject> actionObjects;  //bonuses
        //List<HerniObjekt> decorObjects;   //just decoration, no action needed

        int objCounter       = 0;

        int platfObjCounter  = 0;

        public Map()
        {
            gridSize.X = Constants.GRID_SIZE_X;
            gridSize.Y = Constants.GRID_SIZE_Y;

            mapObjects      = new List<GameObject>();
            movableObjects  = new List<GameObject>();
            //staticObjects   = new List<HerniObjekt>();
            //decorObjects    = new List<HerniObjekt>();
            actionObjects    = new List<GameObject>();
            platfObjects    = new GameObject[(Constants.NbrOfTilesX+1)*(Constants.NbrOfTilesY + 1) ];

            objToDraw = 0;

        }
        /// <summary> 
        ///  Tato metoda vloží herní objekt do mapy
        /// </summary> 
        public void AddObject(GameObject obj)
        {
            if (obj.Type() != typObjektu.empty)
            {
                mapObjects.Add(obj);
                objCounter++;
            }

            switch (obj.Type())
	        {
		        case typObjektu.platform:

                   platfObjects[platfObjCounter] = obj;
                   platfObjCounter++;
                   break;
                case typObjektu.empty:
                   platfObjects[platfObjCounter] = null;
                   platfObjCounter++;

                 break;
                case typObjektu.hero:
                case typObjektu.hazard:
                   movableObjects.Add(obj);                               
                 break;
                case typObjektu.action:
                 actionObjects.Add(obj);
                 break;
                case typObjektu.decoration :

                   mapObjects.Add(obj);
                   objCounter++;
                 break;

                default:
                 break;
	        }


            mapObjects.Sort();
            /*
            Array.Sort(mapObjects,
                delegate(HerniObjekt first, HerniObjekt second) { return first.z.CompareTo(second.z); });*/
        }
        /// <summary> 
        ///  Vymění objekty v poli a na místech i a j.
        /// </summary> 
        private void swapPositions(GameObject[] a, int i, int j)
        {
            GameObject temp = a[i];
            a[i] = a[j];
            a[j] = temp;

        }

        /*
         *  Surrounding indexes
         *  6  1  4
         *  3     2
         *  5  0  7
         * 
         * */
        /// <summary> 
        ///  Vrátí všechny plošiny kolem místa na souřadnicích coords.
        /// </summary> 
        private GameObject[] getSurrounding(Point coords)
        {
            GameObject[] surrounding = new GameObject[9];

            //Console.WriteLine("Surroundings Koala:pos = {0},{1}", coords.X, coords.Y);
            for (int si = 0; si < 9; si++)
            {

                int tilePos;
                int ver = si % 3;
                int hor = si / 3;
                int row = Constants.NbrOfTilesX * (coords.Y + hor - 1); //platforms in rows passed, not nbr of row
                int col = (coords.X) + ver - 1;
                bool inRange = true;
                if (row < 0 ||
                    col < 0 ||
                    coords.Y + hor - 1 >= Constants.NbrOfTilesY - 1||
                    coords.Y == Constants.NbrOfTilesY - 1)
                {
                    row = 0;
                    col = 0;
                    inRange = false;

                }

                tilePos = row + col;
                if (inRange)
                {
                    surrounding[si] = platfObjects[tilePos];
                    /*if (surrounding[si] != null) //debug
                    {
                        Point scords = surrounding[si].GetTileCoordinates(); //TODO nesmi prolitnou spodem a bokem
                    }
                   
                    //Console.WriteLine("x = {0}, y = {1}", scords.X, scords.Y);*/
                }
            }
            surrounding[4] = null;  //collision with center never happens
            swapPositions(surrounding, 0, 7); //straight axis first, then diagonal
            swapPositions(surrounding, 2, 5);

            /*for (int i = 0; i < 9; i++) //debug
            {
                if (surrounding[i] == null)
                {
                    Console.WriteLine(i);
                }
                else
                    Console.WriteLine(surrounding[i].GetTileCoordinates().ToString());
            }*/


            for (int j = 0; j < 9; j++)
                if (surrounding[j] != null)
                    if (surrounding[j].Type() == typObjektu.empty)
                    {
                        surrounding[j] = null;
                    }
                    
            return surrounding;
        }
        /// <summary> 
        ///  Funkce, řešící kolize s plošinami surrounding upravením pozice objektu corrObj. 
        /// </summary> 
        private void solveCollisionWithPlatform(Movable corrObj, GameObject[] surrounding)
        {
            for (int j = 0; j < 9; j++)
            {
                if (surrounding[j] != null)
                {
                    if (corrObj.hitArea.IntersectsWith(surrounding[j].hitArea))
                    {
                        Rectangle intersection = Rectangle.Intersect(
                            corrObj.hitArea,
                            surrounding[j].hitArea);
                        
                        //position correct
                        Vector correction;
                        d direction;
                        switch (j)
                        {
                            case 0: //platform is bellow hero
                                correction = new Vector(0, -intersection.Height);
                                corrObj.onGround = true;
                                direction = d.down;
                                break;

                            case 1: //above
                                correction = new Vector(0, +intersection.Height);
                                direction = d.up;
                                break;
                            case 2: //right
                                correction = new Vector(-intersection.Width,0);
                                direction = d.right;
                                break;
                            case 3: //left
                                correction = new Vector(+intersection.Width,0);
                                direction = d.left;
                                break;
                            default: //one of diagonal platforms
                                if (intersection.Height < intersection.Width)
                                { //collision is from top or bottom
                                    if (j == 7 || j == 5) //above
                                    {
                                        correction = new Vector(0, +intersection.Height);
                                        direction = d.up;
                                    }
                                    else //bellow
                                    {
                                        correction = new Vector(0, -intersection.Height);
                                        corrObj.onGround = true;
                                        direction = d.down;
                                    }
                                }
                                else
                                { //from side
                                    if (j == 5 || j == 8) //right
                                    {
                                        correction = new Vector(-intersection.Width, 0);
                                        direction = d.right;
                                    }
                                    else //left
                                    {
                                        correction = new Vector(+intersection.Width, 0);
                                        direction = d.left;
                                    }
                                }
                                break;
                        }
                        Vector corrected = corrObj.DesiredPos + correction;
                        corrObj.DesiredPos = corrected.toPoint();



                        //if (intersection.Width > intersection.Height)
                        {
                            corrObj.Contact(surrounding[j],direction);
                            surrounding[j].Contact(corrObj, (d) (((int)direction+2) % 4)); //oposite direction for the platform
                        }

                    }

                }
            } //platform collision

            corrObj.Position = corrObj.DesiredPos;
        }
        /// <summary> 
        ///  Metoda vyřeší kolize mezi objekty, včetně správného napozicování.
        /// </summary> 
        public void FindCollisions()
        {
            for (int i = 0; i < movableObjects.Count; i++) //interaction between movables and platforms
            {
                Movable corrObj = (Movable) movableObjects[i]; //object which position i want to correct

                GameObject[] surrounding = getSurrounding(movableObjects[i].GetTileCoordinates());

                solveCollisionWithPlatform(corrObj, surrounding);
            }
            for (int i = 0; i < movableObjects.Count; i++) //movables with each other and points, stars
            {
                for (int j = 0; j < movableObjects.Count; j++)
                {
                    if (i!=j)
                        if (movableObjects[i].hitArea.IntersectsWith(movableObjects[j].hitArea))
                        {
                            d direction = d.up; //TODO test direction for movable
                            {
                                movableObjects[i].Contact(movableObjects[j], direction);
                                movableObjects[j].Contact(movableObjects[i], (d)(((int)direction + 2) % 4)); //oposite direction for the platform
                            }
                        }
                    
                    

                }
                for (int j = 0; j < actionObjects.Count; j++)
                {
                    if (actionObjects[j].destroy == true)
                    {
                        actionObjects.RemoveAt(j);
                    }
                    else
                    {
                        if (movableObjects[i].hitArea.IntersectsWith(actionObjects[j].hitArea))
                        {
                            d direction = d.up; //TODO test direction for actionable
                            {
                                movableObjects[i].Contact(actionObjects[j], direction);
                                actionObjects[j].Contact(movableObjects[i], (d)(((int)direction + 2) % 4)); //oposite direction for the platform
                            }
                        }
                    }
                }
            }

        }
        /// <summary> 
        ///  Nechá každý objekt udělat svoji akci.
        /// </summary> 
        public void MakeActions()
        {
            for (int i = 0; i < mapObjects.Count; i++)
            {
                mapObjects[i].Action();
            }
        }


        /// <summary> 
        ///  Vrátí další objekt k vykreslení
        /// </summary> 
        public GameObject getNextObj()
        {
            bool stop = false;
            while (!stop)
            {
                if (objToDraw < mapObjects.Count)
                {
                    if (mapObjects[objToDraw].visible == false)
                    {
                        objToDraw++;
                    }
                    else
                    {
                        stop = true;
                    }
                }
                else
                {
                    stop = true;
                }   
            }

            int actual = objToDraw;
            objToDraw++;

            if (actual == mapObjects.Count)
            {
                objToDraw = 0;
                return null;
            }
            else
            {
                return mapObjects[actual];
            }
        }
    }


}
