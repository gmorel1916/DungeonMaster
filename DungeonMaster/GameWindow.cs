using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DungeonMaster
{
    public partial class GameWindow : Form
    {
        public GameWindow()
        {
            InitializeComponent();
        }
        TileEngine Dungeon = new TileEngine();
        bool GameClose = false;
        private void GameWindow_Load(object sender, EventArgs e)
        {
            this.Show();

            //Init KeyDown events
            this.KeyDown += new KeyEventHandler(GameWindow_KeyDown);

            Dungeon.Init(DungeonWindow, 100, 100);

            //Main Game Loop
            while (GameClose == false)
            {
                Dungeon.Draw();
                Application.DoEvents();
                System.Threading.Thread.Sleep(30);
            }

            //Close our game when our gameloop has finished.
            this.Close();
        }
        private void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.Left:
                    if (Dungeon.Board[Dungeon.Player.X - 1, Dungeon.Player.Y].TileType == 1)
                        Dungeon.Player.X--;
                    break;

                case Keys.Right:
                    if (Dungeon.Board[Dungeon.Player.X + 1, Dungeon.Player.Y].TileType == 1)
                        Dungeon.Player.X++;
                    break;

                case Keys.Up:
                    if (Dungeon.Board[Dungeon.Player.X, Dungeon.Player.Y - 1].TileType == 1)
                        Dungeon.Player.Y--;
                    break;

                case Keys.Down:
                    if (Dungeon.Board[Dungeon.Player.X, Dungeon.Player.Y + 1].TileType == 1)
                        Dungeon.Player.Y++;
                    break;
                case Keys.A:
                    if (Dungeon.Board[Dungeon.Player.X - 1, Dungeon.Player.Y].TileType == 1)
                        Dungeon.Player.X--;
                    break;

                case Keys.D:
                    if (Dungeon.Board[Dungeon.Player.X + 1, Dungeon.Player.Y].TileType == 1)
                        Dungeon.Player.X++;
                    break;

                case Keys.W:
                    if (Dungeon.Board[Dungeon.Player.X, Dungeon.Player.Y - 1].TileType == 1)
                        Dungeon.Player.Y--;                        
                    break;

                case Keys.S:
                    if (Dungeon.Board[Dungeon.Player.X, Dungeon.Player.Y + 1].TileType == 1)
                        Dungeon.Player.Y++;
                    break;

                case Keys.T:
                    Dungeon.LightingOn = !Dungeon.LightingOn;
                    break;

                case Keys.Escape:
                    GameClose = true;
                    break;

                default:
                    break;
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            GameClose = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            GameClose = true;
        }
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        private void DungeonWindow_Click(object sender, EventArgs e)
        {

        }
    }
}
