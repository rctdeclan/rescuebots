using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Rescuebots
{
    public partial class Field : UserControl
    {
        public static int FIELD_SIZE = 10;
        public Field()
        {
            InitializeComponent();

            for (int j = 0; j < FIELD_SIZE; j++)
            {
                for (int i = 0; i < FIELD_SIZE; i++)
                {
                    borderPBs[i, j] = new TransparentPictureBox();
                    borderPBs[i, j].Image = Rescuebots.Properties.Resources.ALL_OPEN;
                    borderPBs[i, j].Location = new System.Drawing.Point(i * 70, j * 70);
                    borderPBs[i, j].Size = new System.Drawing.Size(70, 70);
                    this.Controls.Add(borderPBs[i, j]);

                    routePBs[i, j] = new TransparentPictureBox();
                    routePBs[i, j].Image = Rescuebots.Properties.Resources.WEST_TO_NORTH_IN;
                    routePBs[i, j].Location = new System.Drawing.Point(i * 70, j * 70);
                    routePBs[i, j].Size = new System.Drawing.Size(70, 70);
                    this.Controls.Add(routePBs[i, j]);

                    invRoutePBs[i, j] = new TransparentPictureBox();
                    invRoutePBs[i, j].Image = Rescuebots.Properties.Resources.WEST_TO_NORTH_OUT;
                    invRoutePBs[i, j].Location = new System.Drawing.Point(i * 70, j * 70);
                    invRoutePBs[i, j].Size = new System.Drawing.Size(70, 70);
                    this.Controls.Add(invRoutePBs[i, j]);
                }
            }

            startPB = new TransparentPictureBox();
            startPB.Image = Rescuebots.Properties.Resources.STARTPOSITION;
            startPB.Size = new System.Drawing.Size(70, 70);

            victimPB = new TransparentPictureBox();
            victimPB.Image = Rescuebots.Properties.Resources.VICTIM;
            victimPB.Size = new System.Drawing.Size(70, 70);

            abductedPB = new TransparentPictureBox();
            abductedPB.Image = Rescuebots.Properties.Resources.NO_STARTPOSITION_OR_VICTIM;
            abductedPB.Size = new System.Drawing.Size(70, 70);

            ResetField();
        }


        public bool softEndRoute = true;
        public bool softEndInvRoute = true;

        Bitmap ALL_OPEN = new Bitmap(Rescuebots.Properties.Resources.ALL_OPEN1);
        Bitmap EAST_CLOSED = new Bitmap(Rescuebots.Properties.Resources.EAST_CLOSED1);
        Bitmap EAST_SOUTH_CLOSED = new Bitmap(Rescuebots.Properties.Resources.EAST_SOUTH_CLOSED1);
        Bitmap EAST_SOUTH_WEST_CLOSED = new Bitmap(Rescuebots.Properties.Resources.EAST_SOUTH_WEST_CLOSED1);
        Bitmap EAST_WEST_CLOSED = new Bitmap(Rescuebots.Properties.Resources.EAST_WEST_CLOSED1);
        Bitmap NORTH_CLOSED = new Bitmap(Rescuebots.Properties.Resources.NORTH_CLOSED1);
        Bitmap NORTH_EAST_CLOSED = new Bitmap(Rescuebots.Properties.Resources.NORTH_EAST_CLOSED1);
        Bitmap NORTH_EAST_SOUTH_CLOSED = new Bitmap(Rescuebots.Properties.Resources.NORTH_EAST_SOUTH_CLOSED1);
        Bitmap NORTH_SOUTH_CLOSED = new Bitmap(Rescuebots.Properties.Resources.NORTH_SOUTH_CLOSED1);
        Bitmap SOUTH_CLOSED = new Bitmap(Rescuebots.Properties.Resources.SOUTH_CLOSED1);
        Bitmap SOUTH_WEST_CLOSED = new Bitmap(Rescuebots.Properties.Resources.SOUTH_WEST_CLOSED1);
        Bitmap SOUTH_WEST_NORTH_CLOSED = new Bitmap(Rescuebots.Properties.Resources.SOUTH_WEST_NORTH_CLOSED1);
        Bitmap WEST_CLOSED = new Bitmap(Rescuebots.Properties.Resources.WEST_CLOSED1);
        Bitmap WEST_NORTH_CLOSED = new Bitmap(Rescuebots.Properties.Resources.WEST_NORTH_CLOSED1);
        Bitmap WEST_NORTH_EAST_CLOSED = new Bitmap(Rescuebots.Properties.Resources.WEST_NORTH_EAST_CLOSED1);

        Bitmap NO_STARTPOSITION_OR_VICTIM = new Bitmap(Rescuebots.Properties.Resources.NO_STARTPOSITION_OR_VICTIM);
        Bitmap STARTPOSITION = new Bitmap(Rescuebots.Properties.Resources.STARTPOSITION);
        Bitmap VICTIM = new Bitmap(Rescuebots.Properties.Resources.VICTIM);

        Bitmap EAST_TO_SOUTH_IN = new Bitmap(Rescuebots.Properties.Resources.EAST_TO_SOUTH_IN);
        Bitmap EAST_TO_WEST_IN = new Bitmap(Rescuebots.Properties.Resources.EAST_TO_WEST_IN);
        Bitmap NORTH_TO_EAST_IN = new Bitmap(Rescuebots.Properties.Resources.NORTH_TO_EAST_IN);
        Bitmap NORTH_TO_SOUTH_IN = new Bitmap(Rescuebots.Properties.Resources.NORTH_TO_SOUTH_IN);
        Bitmap NOT_ENTERED_ZONE_IN = new Bitmap(Rescuebots.Properties.Resources.NOT_ENTERED_ZONE_IN);
        Bitmap SOUTH_TO_WEST_IN = new Bitmap(Rescuebots.Properties.Resources.SOUTH_TO_WEST_IN);
        Bitmap WEST_TO_NORTH_IN = new Bitmap(Rescuebots.Properties.Resources.WEST_TO_NORTH_IN);
        Bitmap NORTH_EAST_SOUTH_IN = new Bitmap(Rescuebots.Properties.Resources.NORTH_EAST_SOUTH_IN);
        Bitmap EAST_SOUTH_WEST_IN = new Bitmap(Rescuebots.Properties.Resources.EAST_SOUTH_WEST_IN);
        Bitmap NORTH_SOUTH_WEST_IN = new Bitmap(Rescuebots.Properties.Resources.NORTH_SOUTH_WEST_IN);
        Bitmap NORTH_EAST_WEST_IN = new Bitmap(Rescuebots.Properties.Resources.NORTH_EAST_WEST_IN);
        Bitmap NORTH_EAST_SOUTH_WEST_IN = new Bitmap(Rescuebots.Properties.Resources.NORTH_EAST_SOUTH_WEST_IN);

        Bitmap EAST_TO_SOUTH_OUT = new Bitmap(Rescuebots.Properties.Resources.EAST_TO_SOUTH_OUT);
        Bitmap EAST_TO_WEST_OUT = new Bitmap(Rescuebots.Properties.Resources.EAST_TO_WEST_OUT);
        Bitmap NORTH_TO_EAST_OUT = new Bitmap(Rescuebots.Properties.Resources.NORTH_TO_EAST_OUT);
        Bitmap NORTH_TO_SOUTH_OUT = new Bitmap(Rescuebots.Properties.Resources.NORTH_TO_SOUTH_OUT);
        Bitmap NOT_ENTERED_ZONE_OUT = new Bitmap(Rescuebots.Properties.Resources.NOT_ENTERED_ZONE_OUT);
        Bitmap SOUTH_TO_WEST_OUT = new Bitmap(Rescuebots.Properties.Resources.SOUTH_TO_WEST_OUT);
        Bitmap WEST_TO_NORTH_OUT = new Bitmap(Rescuebots.Properties.Resources.WEST_TO_NORTH_OUT);
        Bitmap NORTH_EAST_SOUTH_OUT = new Bitmap(Rescuebots.Properties.Resources.NORTH_EAST_SOUTH_OUT);
        Bitmap EAST_SOUTH_WEST_OUT = new Bitmap(Rescuebots.Properties.Resources.EAST_SOUTH_WEST_OUT);
        Bitmap NORTH_SOUTH_WEST_OUT = new Bitmap(Rescuebots.Properties.Resources.NORTH_SOUTH_WEST_OUT);
        Bitmap NORTH_EAST_WEST_OUT = new Bitmap(Rescuebots.Properties.Resources.NORTH_EAST_WEST_OUT);
        Bitmap NORTH_EAST_SOUTH_WEST_OUT = new Bitmap(Rescuebots.Properties.Resources.NORTH_EAST_SOUTH_WEST_OUT);

        TransparentPictureBox[,] borderPBs = new TransparentPictureBox[FIELD_SIZE, FIELD_SIZE];
        TransparentPictureBox[,] routePBs = new TransparentPictureBox[FIELD_SIZE, FIELD_SIZE];
        TransparentPictureBox[,] invRoutePBs = new TransparentPictureBox[FIELD_SIZE, FIELD_SIZE];

        TransparentPictureBox startPB;
        TransparentPictureBox victimPB;
        TransparentPictureBox abductedPB;

        int[,] borderIndexes = new int[FIELD_SIZE, FIELD_SIZE]; 

        int[,] routeIndexes = new int[FIELD_SIZE,FIELD_SIZE];
        int[,] invRouteIndexes = new int[FIELD_SIZE, FIELD_SIZE];

        public void FillBorders(List<cell> cells)
        {
            for (int i = 0; i< cells.Count; i++)
            {
                int x = cells[i].x;
                int y = cells[i].y;
                TransparentPictureBox pb = borderPBs[x,y];
                int index = borderIndexes[x,y] | ((cells[i].north ? 0x1 : 0) + (cells[i].east ? 0x2 : 0) + (cells[i].south ? 0x4 : 0) + (cells[i].west ? 0x8 : 0));
                borderIndexes[x, y] = index;
                switch (index)
                {
                    case 0:
                        pb.Image = ALL_OPEN;
                        break;
                    case 1:
                        pb.Image = NORTH_CLOSED;
                        break;
                    case 2:
                        pb.Image = EAST_CLOSED;
                        break;
                    case 3:
                        pb.Image = NORTH_EAST_CLOSED;
                        break;
                    case 4:
                        pb.Image = SOUTH_CLOSED;
                        break;
                    case 5:
                        pb.Image = NORTH_SOUTH_CLOSED;
                        break;
                    case 6:
                        pb.Image = EAST_SOUTH_CLOSED;
                        break;
                    case 7:
                        pb.Image = NORTH_EAST_SOUTH_CLOSED;
                        break;
                    case 8:
                        pb.Image = WEST_CLOSED;
                        break;
                    case 9:
                        pb.Image = WEST_NORTH_CLOSED;
                        break;
                    case 10:
                        pb.Image = EAST_WEST_CLOSED;
                        break;
                    case 11:
                        pb.Image = WEST_NORTH_EAST_CLOSED;
                        break;
                    case 12:
                        pb.Image = SOUTH_WEST_CLOSED;
                        break;
                    case 13:
                        pb.Image = SOUTH_WEST_NORTH_CLOSED;
                        break;
                    case 14:
                        pb.Image = EAST_SOUTH_WEST_CLOSED;
                        break;
                    default:
                        pb.Image = ALL_OPEN;
                        break;
                }

            }
        }

        public void FillRoute(List<cell> cells)
        {

            for (int i = 0; i < cells.Count; i++)
            {
                int x = cells[i].x;
                int y = cells[i].y;
                TransparentPictureBox pb = invRoutePBs[x, y];
                int index = invRouteIndexes[x,y];
                if (i > 0) 
                {
                    if (cells[i - 1].x - x == -1) index |= 0x8;
                    if (cells[i - 1].x - x == 1) index |= 0x2;
                    if (cells[i - 1].y - y == -1) index |= 0x1;
                    if (cells[i - 1].y - y == 1) index |= 0x4;
                }
                if (i < cells.Count - 1) 
                {
                    if (cells[i + 1].x - x == -1) index |= 0x8;
                    if (cells[i + 1].x - x == 1) index |= 0x2;
                    if (cells[i + 1].y - y == -1) index |= 0x1;
                    if (cells[i + 1].y - y == 1) index |= 0x4;
                }
                switch (index)
                {
                    case 0:
                        pb.Image = NOT_ENTERED_ZONE_IN;
                        break;
                    case 1: //single
                        pb.Image = NORTH_TO_SOUTH_IN;
                        break;
                    case 2: //single
                        pb.Image = EAST_TO_WEST_IN;
                        break;
                    case 3:
                        pb.Image = NORTH_TO_EAST_IN;
                        break;
                    case 4: //single
                        pb.Image = NORTH_TO_SOUTH_IN;
                        break;
                    case 5:
                        pb.Image = NORTH_TO_SOUTH_IN;
                        break;
                    case 6:
                        pb.Image = EAST_TO_SOUTH_IN;
                        break;
                    case 7:
                        pb.Image = NORTH_EAST_SOUTH_IN;
                        break;
                    case 8://single
                        pb.Image = EAST_TO_WEST_IN;
                        break;
                    case 9:
                        pb.Image = WEST_TO_NORTH_IN;
                        break;
                    case 10:
                        pb.Image = EAST_TO_WEST_IN;
                        break;
                    case 11:
                        pb.Image = NORTH_EAST_WEST_IN;
                        break;
                    case 12:
                        pb.Image = SOUTH_TO_WEST_IN;
                        break;
                    case 13:
                        pb.Image = NORTH_SOUTH_WEST_IN;
                        break;
                    case 14:
                        pb.Image = EAST_SOUTH_WEST_IN;
                        break;
                    case 15:
                        pb.Image = NORTH_SOUTH_WEST_IN;
                        break;
                    case 16:
                        pb.Image = NORTH_EAST_SOUTH_WEST_IN;
                        break;
                    default:
                        pb.Image = NOT_ENTERED_ZONE_IN;
                        break;
                }
            }

            //mark startposition
            startPB.Location = new System.Drawing.Point(cells[0].x * 70, cells[0].y * 70);
            this.Controls.Add(startPB);

            //mark abduction point
            if (!softEndRoute)
            {
                abductedPB.Location = new System.Drawing.Point(cells[cells.Count - 1].x * 70, cells[cells.Count - 1].y * 70);
                this.Controls.Add(abductedPB);
            }
            else
            {
                victimPB.Location = new System.Drawing.Point(cells[cells.Count - 1].x * 70, cells[cells.Count - 1].y * 70);
                this.Controls.Add(victimPB);
            }

        }

        public void FillInvRoute(List<cell> invCells)
        {
            for (int i = 0; i < invCells.Count; i++)
            {
                int x = invCells[i].x;
                int y = invCells[i].y;
                TransparentPictureBox pb = routePBs[x, y];
                int index = routeIndexes[x, y];
                if (i > 0)
                {
                    if (invCells[i - 1].x - x == -1) index |= 0x8;
                    if (invCells[i - 1].x - x == 1) index |= 0x2;
                    if (invCells[i - 1].y - y == -1) index |= 0x1;
                    if (invCells[i - 1].y - y == 1) index |= 0x4;
                }
                if (i < invCells.Count -1)
                {
                    if (invCells[i + 1].x - x == -1) index |= 0x8;
                    if (invCells[i + 1].x - x == 1) index |= 0x2;
                    if (invCells[i + 1].y - y == -1) index |= 0x1;
                    if (invCells[i + 1].y - y == 1) index |= 0x4;
                }
                switch (index)
                {
                    case 0:
                        pb.Image = NOT_ENTERED_ZONE_OUT;
                        break;
                    case 1: //single
                        pb.Image = NORTH_TO_SOUTH_OUT;
                        break;
                    case 2: //single
                        pb.Image = EAST_TO_WEST_OUT;
                        break;
                    case 3:
                        pb.Image = NORTH_TO_EAST_OUT;
                        break;
                    case 4: //single
                        pb.Image = NORTH_TO_SOUTH_OUT;
                        break;
                    case 5:
                        pb.Image = NORTH_TO_SOUTH_OUT;
                        break;
                    case 6:
                        pb.Image = EAST_TO_SOUTH_OUT;
                        break;
                    case 7:
                        pb.Image = NORTH_EAST_SOUTH_OUT;
                        break;
                    case 8://single
                        pb.Image = EAST_TO_WEST_OUT;
                        break;
                    case 9:
                        pb.Image = WEST_TO_NORTH_OUT;
                        break;
                    case 10:
                        pb.Image = EAST_TO_WEST_OUT;
                        break;
                    case 11:
                        pb.Image = NORTH_EAST_WEST_OUT;
                        break;
                    case 12:
                        pb.Image = SOUTH_TO_WEST_OUT;
                        break;
                    case 13:
                        pb.Image = NORTH_SOUTH_WEST_OUT;
                        break;
                    case 14:
                        pb.Image = EAST_SOUTH_WEST_OUT;
                        break;
                    case 15:
                        pb.Image = NORTH_SOUTH_WEST_OUT;
                        break;
                    case 16:
                        pb.Image = NORTH_EAST_SOUTH_WEST_OUT;
                        break;
                    default:
                        pb.Image = NOT_ENTERED_ZONE_OUT;
                        break;
                }
            }

            //mark abduction point
            if (!softEndInvRoute)
            {
                abductedPB.Location = new System.Drawing.Point(invCells[invCells.Count - 1].x * 70, invCells[invCells.Count - 1].y * 70);
                this.Controls.Add(abductedPB);
            }
        }

        public void ResetField()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    borderPBs[i, j].Image = ALL_OPEN;
                    routePBs[i, j].Image = NOT_ENTERED_ZONE_IN;
                    invRoutePBs[i, j].Image = NOT_ENTERED_ZONE_OUT;
                }
            }
            if (Controls.Contains(startPB)) Controls.Remove(startPB);
            if (Controls.Contains(victimPB)) Controls.Remove(victimPB);
            if (Controls.Contains(abductedPB)) Controls.Remove(abductedPB);

        }
    }
}
