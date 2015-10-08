using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DungeonMaster
{
    public class TileEngine
    {
        private const int TileSize = 32;

        public struct Object
        {
            public int X;
            public int Y;
            public Image Sprite;
        }

        public Object Player;
        public Object Stairs;
        public Object Potion;
        public Object Sword;

        public PictureBox DungeonWindow;
        public int Width;
        public int Height;

        private Image[] TileImages;

        public struct Tile
        {
            public int TileType;
        }

        public Tile[,] Board;

        public void Init(PictureBox DungBox, int BoardWidth, int BoardHeight)
        {
            //Set PictureBox 
            DungeonWindow = DungBox;

            //Setup Board & Image Size
            Width = BoardWidth;
            Height = BoardHeight;
            DungeonWindow.Size = new System.Drawing.Size(640, 480);
            DungeonWindow.Image = new Bitmap(DungeonWindow.Size.Width, DungeonWindow.Size.Height);

            Board = new Tile[Width, Height];

            //Set Up Player
            Player = new Object();
            Player.Sprite = Image.FromFile(Application.StartupPath + "\\Player.png");

            //Setup Stairs Example..
            Stairs = new Object();
            Stairs.Sprite = Image.FromFile(Application.StartupPath + "\\Stairs.png");

            //Setup Potion Example..
            Potion = new Object();
            Potion.Sprite = Image.FromFile(Application.StartupPath + "\\Potion.png");

            //Setup Sword Example..
            Sword = new Object();
            Sword.Sprite = Image.FromFile(Application.StartupPath + "\\Sword.png");

            //Setup Lighting Image
            Light = Image.FromFile(Application.StartupPath + "\\Lighting.png");

            //Load Tile Images
            TileImages = new Image[5];
            for (int i = 0; i < 5; i++)
            {
                TileImages[i] = Image.FromFile(Application.StartupPath + "\\Tile" + i.ToString() + ".png");
            }

            //Make A Dungeon with X rooms..
            int DungeonRooms = Utility.Instance.Next(10, 23);
            MakeDungeon(DungeonRooms);

            //Place Player @ Center of First Room.
            Player.X = RoomX[0];
            Player.Y = RoomY[0];

            //Place Stairs in Last Room.
            Stairs.X = RoomX[RoomX.Length - 1];
            Stairs.Y = RoomY[RoomX.Length - 1];

            //Place Potion in Room 3.. 
            Potion.X = RoomX[2];
            Potion.Y = RoomY[2];

            //Place Sword in Room 8.. 
            Sword.X = RoomX[8];
            Sword.Y = RoomY[8];
        }

        public int[] RoomX;
        public int[] RoomY;

        public void MakeDungeon(int NumOfRooms)
        {
            RoomX = new int[NumOfRooms];
            RoomY = new int[NumOfRooms];

            //Clear Dungeon Room
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Board[x, y].TileType = 2;
                }
            }

            for (int i = 0; i < NumOfRooms; i++)
            {
                //Minimum Room size of 3 too keep things in the bounds of the game.
                //This is so we can keep them inside the game board.
                int x1 = Utility.Instance.Next(2, Width - 5);
                int y1 = Utility.Instance.Next(2, Height - 5);

                //The Top right needs to stay inside of the game board
                //Rooms are between 2 and 13 in size.
                int x2 = Utility.Instance.Next(x1 + 1, x1 + 12);
                int y2 = Utility.Instance.Next(y1 + 1, y1 + 12);

                //if x2 or y2 is over the edge of the board.. tell it not to be
                if (x2 >= Width - 3) { x2 = Width - 3; }
                if (y2 >= Height - 3) { y2 = Height - 3; }

                for (int x = x1; x < x2 + 1; x++)
                {
                    for (int y = y1; y < y2 + 1; y++)
                    {
                        //Walkable Floor
                        Board[x, y].TileType = 1;
                    }
                }

                //Find the center of rooms
                RoomX[i] = x1 + ((x2 - x1) / 2);
                RoomY[i] = y1 + ((y2 - y1) / 2);
            }

            //Makes Corridors
            for (int i = 0; i < NumOfRooms - 1; i++)
            {
                int newX = RoomX[i];
                int newY = RoomY[i];
                while (newX != RoomX[i + 1])
                {
                    if (RoomX[i + 1] > newX)
                    {
                        newX += 1;
                    }
                    else if (RoomX[i + 1] < newX)
                    {
                        newX -= 1;
                    }

                    Board[newX, newY].TileType = 1;
                }
                while (newY != RoomY[i + 1])
                {
                    if (RoomY[i + 1] > newY)
                    {
                        newY += 1;
                    }
                    else if (RoomY[i + 1] < newY)
                    {
                        newY -= 1;
                    }

                    Board[newX, newY].TileType = 1;
                }
            }

            //Makes Close Walls Brighter
            for (int x = 1; x < Width - 1; x++)
            {
                for (int y = 1; y < Height - 1; y++)
                {
                    if (Board[x, y].TileType != 2) { continue; }

                    if (Board[x + 1, y].TileType == 1 || Board[x - 1, y].TileType == 1 || Board[x, y + 1].TileType == 1 || Board[x, y - 1].TileType == 1)
                    {
                        Board[x, y].TileType = 0;
                    }
                }
            }

            //Makes further walls darker.
            for (int x = 1; x < Width - 1; x++)
            {
                for (int y = 1; y < Height - 1; y++)
                {
                    // if (Board[x, y].TileType != 2) { continue; }

                    if (Board[x + 1, y].TileType >= 2 && Board[x - 1, y].TileType >= 2 && Board[x, y + 1].TileType >= 2 && Board[x, y - 1].TileType >= 2)
                    {
                        Board[x, y].TileType = 3;
                    }
                }
            }

            //Makes even further walls even darker.
            for (int x = 1; x < Width - 1; x++)
            {
                for (int y = 1; y < Height - 1; y++)
                {
                    // if (Board[x, y].TileType != 2) { continue; }

                    if (Board[x + 1, y].TileType >= 3 && Board[x - 1, y].TileType >= 3 && Board[x, y + 1].TileType >= 3 && Board[x, y - 1].TileType >= 3)
                    {
                        Board[x, y].TileType = 4;
                    }
                }
            }

            //makes Farther Further walls darker.
            for (int x = 1; x < Width - 1; x++)
            {
                for (int y = 1; y < Height - 1; y++)
                {
                    if (Board[x + 1, y].TileType >= 4 && Board[x - 1, y].TileType >= 4 && Board[x, y + 1].TileType >= 4 && Board[x, y - 1].TileType >= 4)
                    {
                        Board[x, y].TileType = 5;
                    }
                }
            }
        }

        // Create font and brush.
        private Font FontDraw = new Font("Consolas", 12);
        private SolidBrush drawBrushRed = new SolidBrush(Color.Red);
        private SolidBrush drawBrushBlue = new SolidBrush(Color.Blue);
        private SolidBrush drawBrushBlack = new SolidBrush(Color.Black);

        private Image Light;
        public bool LightingOn = false;
        public void Draw()
        {
            Graphics G = Graphics.FromImage(DungeonWindow.Image);
            G.Clear(Color.Black);
            Rectangle R;
            int xStart = Player.X - 10;
            int EndX = Player.X + 10;
            int yStart = Player.Y - 8;
            int EndY = Player.Y + 8;
            //Draws the Tiles
            for (int x = xStart; x < EndX; x++)
            {
                if (x < 0) { continue; }
                if (x > Width - 1) { break; }
                for (int y = yStart; y < EndY; y++)
                {
                    if (y < 0) { continue; }
                    if (y > Height - 1) { break; }

                    //Create 32x32 Sized Rectangle for tile images
                    R = new Rectangle(x * TileSize - xStart * TileSize, y * TileSize - yStart * TileSize, TileSize, TileSize);

                    //Draw our tiles.
                    switch (Board[x, y].TileType)
                    {
                        //Wall
                        case 0:
                            G.DrawImage(TileImages[0], R);
                            break;
                        //Floor
                        case 1:
                            G.DrawImage(TileImages[1], R);
                            break;
                        //Dark Wall
                        case 2:
                            G.DrawImage(TileImages[2], R);
                            break;
                        //Very Dark Wall
                        case 3:
                            G.DrawImage(TileImages[3], R);
                            break;

                        //Very Very Dark Wall
                        case 4:
                            G.DrawImage(TileImages[4], R);
                            break;

                        default:
                            break;
                    }
                }
            }

            //Draws Stairs
            int xTemp = Stairs.X * TileSize - xStart * TileSize;
            int yTemp = Stairs.Y * TileSize - yStart * TileSize;
            if (xTemp >= 0 && xTemp <= 640 && yTemp >= 0 && yTemp <= 480)
            {
                R = new Rectangle(xTemp, yTemp, TileSize, TileSize);
                G.DrawImage(Stairs.Sprite, R);

            }

            //Draws Potion
            xTemp = Potion.X * TileSize - xStart * TileSize;
            yTemp = Potion.Y * TileSize - yStart * TileSize;
            if (xTemp >= 0 && xTemp <= 640 && yTemp >= 0 && yTemp <= 480)
            {
                R = new Rectangle(xTemp, yTemp, TileSize, TileSize);
                G.DrawImage(Potion.Sprite, R);
            }

            //Draw Sword
            xTemp = Sword.X * TileSize - xStart * TileSize;
            yTemp = Sword.Y * TileSize - yStart * TileSize;
            if (xTemp >= 0 && xTemp <= 640 && yTemp >= 0 && yTemp <= 480)
            {
                R = new Rectangle(xTemp, yTemp, TileSize, TileSize);
                G.DrawImage(Sword.Sprite, R);
            }

            //Draws Player (x, y, width, height)
            //This scrolls the player through the dungeon and keeps it in the center of the dungeon.
            R = new Rectangle(10 * TileSize, 8 * TileSize, TileSize, TileSize);
            G.DrawImage(Player.Sprite, R);

            //Draw Lighting
            if (LightingOn)
            {
                R = new Rectangle(0, 0, 640, 480);
                G.DrawImage(Light, R);
            }

            G.FillRectangle(drawBrushBlack, 0, 0, 640, 20);
            G.FillRectangle(drawBrushBlack, 0, 460, 640, 20);
            G.DrawString("Health: 100", FontDraw, drawBrushRed, 2, 0);
            G.DrawString("Mana: 100", FontDraw, drawBrushBlue, 630 - ("Mana: 100".Length * 9), 0);
            G.DrawString("RandomDungeon", FontDraw, drawBrushRed, 2, 460);

            string Debug = "Lighting (T): " + LightingOn.ToString() + " - POS: " + Player.X.ToString() + ", " + Player.Y.ToString();

            G.DrawString(Debug, FontDraw, drawBrushBlue, 630 - (Debug.Length * 9), 460);
            
            G.Dispose();
            DungeonWindow.Refresh();
        }
    }
}
