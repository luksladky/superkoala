using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.ComponentModel;

namespace SuperKoule
{


    class MapLoader
    {
        private Queue<GameObject> objects; 
        public MapLoader(string mapDataText)
        {
            objects = new Queue<GameObject>();
            int iy = 0;
            int tilesx = 0;


            using (StringReader sr = new StringReader(mapDataText))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] objectsOnLine = line.Split(null);
                    int ix = 0;

                    if (line != "")
                    {
                        foreach (string obj in objectsOnLine)
	                    {
                            if (obj != "")
                            {
                                GameObject objToAdd = null;

                                switch (obj[0])
	                            {
                                    case '=': //tile basic green
                                        objToAdd = new Platform(ix * Constants.GRID_SIZE_X, 
                                                          iy * Constants.GRID_SIZE_Y,
                                                          Constants.GRID_SIZE_X,
                                                          Constants.GRID_SIZE_Y);
                                        if (obj.Length > 1)
                                        {
                                            switch (obj[1])
                                            {
                                                case '<':
                                                    objToAdd.SetSpriteByType(sprite.tileLeft);
                                                    break;
                                                case '>':
                                                    objToAdd.SetSpriteByType(sprite.tileRight);
                                                    break;
                                                case '=':
                                                    objToAdd.SetSpriteByType(sprite.tileCenter);
                                                    break;
                                                case 'd':
                                                    objToAdd.SetSpriteByType(sprite.tileDirt);
                                                    break;
        
                                                default:
                                                    break;
                                            }
                                        }
                                        break;
                                    case '_': //half green
                                        objToAdd = new Platform(ix*Constants.GRID_SIZE_X,   //tile basic green half
                                                          iy*Constants.GRID_SIZE_Y,
                                                          Constants.GRID_SIZE_X,
                                                          Constants.GRID_SIZE_Y/2);
                                        objToAdd.SetSpriteByType(sprite.tileHalf);

                                        break;

                                    case '~': //ice
                                        objToAdd = new ActionItem(ix * Constants.GRID_SIZE_X + Constants.GRID_SIZE_X / 4, //tile basic green
                                                          iy * Constants.GRID_SIZE_Y - Constants.GRID_SIZE_Y / 2,
                                                          Constants.GRID_SIZE_X/2,
                                                          Constants.GRID_SIZE_Y/2);
                                        objToAdd.SetSpriteByType(sprite.iceEffect);
                                        objects.Enqueue(objToAdd);

                                        objToAdd = new Platform(ix * Constants.GRID_SIZE_X, 
                                                          iy * Constants.GRID_SIZE_Y,
                                                          Constants.GRID_SIZE_X,
                                                          Constants.GRID_SIZE_Y);

                                        if (obj.Length > 1)
                                        {
                                            switch (obj[1])
                                            {
                                                case '<':
                                                    objToAdd.SetSpriteByType(sprite.iceTileLeft);
                                                    break;
                                                case '>':
                                                    objToAdd.SetSpriteByType(sprite.iceTileRight);
                                                    break;
                                                case '~':
                                                    objToAdd.SetSpriteByType(sprite.iceTileCenter);
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            objToAdd.SetSpriteByType(sprite.iceTile);
                                        }


                                        break;
                                         

                                    case 'w': //water
                                        objToAdd = new EmptySpace(ix * Constants.GRID_SIZE_X,
                                                          iy * Constants.GRID_SIZE_Y,
                                                          Constants.GRID_SIZE_X,
                                                          Constants.GRID_SIZE_Y);
                                        objects.Enqueue(objToAdd);
                                        objToAdd = new ActionItem(ix * Constants.GRID_SIZE_X,   //tile basic green half
                                                          iy * Constants.GRID_SIZE_Y + Constants.GRID_SIZE_Y / 2,
                                                          Constants.GRID_SIZE_X,
                                                          Constants.GRID_SIZE_Y / 2);
                                        objToAdd.SetSpriteByType(sprite.water);

                                        break;

                                    case 'o': //coins
                                        objToAdd = new EmptySpace(ix * Constants.GRID_SIZE_X,
                                                          iy * Constants.GRID_SIZE_Y,
                                                          Constants.GRID_SIZE_X,
                                                          Constants.GRID_SIZE_Y);
                                        objects.Enqueue(objToAdd);
                                        objToAdd = new ActionItem(ix * Constants.GRID_SIZE_X + Constants.GRID_SIZE_X / 4,   //coin is more up
                                                          iy * Constants.GRID_SIZE_Y ,
                                                          Constants.GRID_SIZE_X / 2,
                                                          Constants.GRID_SIZE_Y / 2);
                                        objToAdd.SetSpriteByType(sprite.coin);
                                        if (obj.Length > 1)
                                        {
                                            switch (obj[1])
                                            {
                                                case 'y':
                                                    objToAdd.SetSpriteByType(sprite.coinGold);
                                                    break;
                                            }
                                        }
                                        break;
                                    case '*': //star
                                        objToAdd = new EmptySpace(ix * Constants.GRID_SIZE_X,
                                                          iy * Constants.GRID_SIZE_Y,
                                                          Constants.GRID_SIZE_X,
                                                          Constants.GRID_SIZE_Y);
                                        objects.Enqueue(objToAdd);
                                        objToAdd = new ActionItem(ix * Constants.GRID_SIZE_X + Constants.GRID_SIZE_X / 4, 
                                                          iy * Constants.GRID_SIZE_Y,
                                                          Constants.GRID_SIZE_X / 2,
                                                          Constants.GRID_SIZE_Y / 2);
                                        objToAdd.SetSpriteByType(sprite.star);

                                        break;
                                    case 'k': //keys
                                        objToAdd = new EmptySpace(ix * Constants.GRID_SIZE_X,
                                                          iy * Constants.GRID_SIZE_Y,
                                                          Constants.GRID_SIZE_X,
                                                          Constants.GRID_SIZE_Y);
                                        objects.Enqueue(objToAdd);
                                        objToAdd = new ActionItem(ix * Constants.GRID_SIZE_X,
                                                                  iy * Constants.GRID_SIZE_Y - 10,
                                                                  Constants.GRID_SIZE_X,
                                                                  Constants.GRID_SIZE_Y);
                                        objToAdd.SetSpriteByType(sprite.key);
                                        if (obj.Length > 1)
                                        {
                                            switch (obj[1])
                                            {
                                                case 'r':
                                                    objToAdd.SetSpriteByType(sprite.keyRed);
                                                    break;
                                                case 'g':
                                                    objToAdd.SetSpriteByType(sprite.keyGreen);
                                                    break;
                                                case 'b':
                                                    objToAdd.SetSpriteByType(sprite.keyBlue);
                                                    break;
                                            }
                                        }

                                        break;
                                    case 'D': //water
                                        objToAdd = new EmptySpace(ix * Constants.GRID_SIZE_X,
                                                          iy * Constants.GRID_SIZE_Y,
                                                          Constants.GRID_SIZE_X,
                                                          Constants.GRID_SIZE_Y);
                                        objects.Enqueue(objToAdd);
                                        objToAdd = new ActionItem(ix * Constants.GRID_SIZE_X,   //tile basic green half
                                                          iy * Constants.GRID_SIZE_Y - Constants.GRID_SIZE_Y / 2,
                                                          Constants.GRID_SIZE_X,
                                                          Constants.GRID_SIZE_Y + Constants.GRID_SIZE_Y / 2);
                                        objToAdd.SetSpriteByType(sprite.door);

                                        break;
                                    case 'M': //monster
                                        objToAdd = new EmptySpace(ix * Constants.GRID_SIZE_X,
                                                          iy * Constants.GRID_SIZE_Y,
                                                          Constants.GRID_SIZE_X,
                                                          Constants.GRID_SIZE_Y);
                                        objects.Enqueue(objToAdd);
                                        
                                        int h = 28;
                                        int w = 50;

                                        objToAdd = new Monster(ix * Constants.GRID_SIZE_X,   //default is blob
                                                          iy * Constants.GRID_SIZE_Y,
                                                          w,
                                                          h);
                                       
                                        if (obj.Length > 1)
                                        {
                                            switch (obj[1])
                                            {
                                                case '^':
                                                    objToAdd.SetSpriteByType(sprite.spikes);
                                                    break;
                                                case 'f':
                                                    objToAdd.SetSpriteByType(sprite.fly);
                                                    break;
                                                case 's':
                                                    objToAdd.SetSpriteByType(sprite.snail);
                                                    break;

                                            }

                                        }
                                        else
                                        {
                                            objToAdd.SetSpriteByType(sprite.blob);
                                        }
                                        break;

                                    case 'd': //decoration
                                        objToAdd = new EmptySpace(ix * Constants.GRID_SIZE_X, //only for retrieving right tiles
                                                          iy * Constants.GRID_SIZE_Y,
                                                          Constants.GRID_SIZE_X,
                                                          Constants.GRID_SIZE_Y);
                                        objects.Enqueue(objToAdd);
                                        if (obj.Length > 1)
                                        {
                                            objToAdd = new Decoration(ix * Constants.GRID_SIZE_X, //will be shown
                                                                      iy * Constants.GRID_SIZE_Y,
                                                                      Constants.GRID_SIZE_X,
                                                                      Constants.GRID_SIZE_Y);
                                            switch (obj[1])
                                            {   
                                                case 'F':
                                                    objToAdd.SetSpriteByType(sprite.fence);
                                                    
                                                    break;
                                                case '>':
                                                    objToAdd.SetSpriteByType(sprite.signRight);
                                                    break;
                                                case 'c':
                                                    objToAdd.SetSpriteByType(sprite.cloud);
                                                    break;
                                                case 'p':
                                                    objToAdd.SetSpriteByType(sprite.plant);
                                                    break;
                                                case 'r':
                                                    objToAdd.SetSpriteByType(sprite.rock);
                                                    break;
                                                case 'b':
                                                    objToAdd.SetSpriteByType(sprite.bush);
                                                    break;
                                                case 's':
                                                    objToAdd.SetSpriteByType(sprite.snowHill);
                                                    break;
                                                case 'e':
                                                    objToAdd.SetSpriteByType(sprite.exit);
                                                    break;
                                                case '<':
                                                    objToAdd.SetSpriteByType(sprite.signLeft);
                                                    break;
                                                case 'f':
                                                    objToAdd.SetSpriteByType(sprite.brokenFence);

                                                    break;
                                                default:
                                                    objToAdd.SetSpriteByType(sprite.basic);
                                                    break;
                                            }
                                        }

                                        break;
                                    case '|' :  //monster stopper
                                        objToAdd = new EmptySpace(ix * Constants.GRID_SIZE_X, //only for retrieving right tiles
                                                          iy * Constants.GRID_SIZE_Y,
                                                          Constants.GRID_SIZE_X,
                                                          Constants.GRID_SIZE_Y);
                                        objects.Enqueue(objToAdd);
                                        objToAdd = new ActionItem(ix * Constants.GRID_SIZE_X + Constants.GRID_SIZE_X / 4, //transparent monster stopper
                                                          iy * Constants.GRID_SIZE_Y + Constants.GRID_SIZE_Y / 2,
                                                          Constants.GRID_SIZE_X/2,
                                                          Constants.GRID_SIZE_Y/2);
                                        objToAdd.SetSpriteByType(sprite.monsterStopper);

                                        break;

		                            default:
                                        objToAdd = new EmptySpace(ix * Constants.GRID_SIZE_X,
                                                          iy * Constants.GRID_SIZE_Y,
                                                          Constants.GRID_SIZE_X,
                                                          Constants.GRID_SIZE_Y);
                                        break;

	                            }

                                if (objToAdd != null)
	                            {
		 		                    objects.Enqueue(objToAdd);
	                            }
                                ix++;
                            }
	                    }
                        iy++;
                        if (tilesx == 0)
                        {
                            tilesx = ix;
                        }
                    }

                }
            }
            Constants.NbrOfTilesY = iy + 1;
            Constants.NbrOfTilesX = tilesx;
        }
        public bool ObjectsLeft()
        {
            return objects.Count() > 0 ? true : false;
        }
        public GameObject GetNextObject() {
            if (objects.Count > 0)
            {
                return objects.Dequeue();
            }
            else
            {
                return null;
            }


        }
    }
}

