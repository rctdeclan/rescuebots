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
        public Field()
        {
            InitializeComponent();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
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
                }
            }
        }


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

        Bitmap EAST_TO_SOUTH_OUT = new Bitmap(Rescuebots.Properties.Resources.EAST_TO_SOUTH_OUT);
        Bitmap EAST_TO_WEST_OUT = new Bitmap(Rescuebots.Properties.Resources.EAST_TO_WEST_OUT);
        Bitmap NORTH_TO_EAST_OUT = new Bitmap(Rescuebots.Properties.Resources.NORTH_TO_EAST_OUT);
        Bitmap NORTH_TO_SOUTH_OUT = new Bitmap(Rescuebots.Properties.Resources.NORTH_TO_SOUTH_OUT);
        Bitmap NOT_ENTERED_ZONE_OUT = new Bitmap(Rescuebots.Properties.Resources.NOT_ENTERED_ZONE_OUT);
        Bitmap SOUTH_TO_WEST_OUT = new Bitmap(Rescuebots.Properties.Resources.SOUTH_TO_WEST_OUT);
        Bitmap WEST_TO_NORTH_OUT = new Bitmap(Rescuebots.Properties.Resources.WEST_TO_NORTH_OUT);


        TransparentPictureBox[,] borderPBs = new TransparentPictureBox[10, 10];
        TransparentPictureBox[,] routePBs = new TransparentPictureBox[10, 10];

        int[,] borderIndexes = new int[10, 10] 
        { 
        { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 }, 
        { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
        { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
        { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
        { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
        { 1, 2, 3, 8, 5, 6, 7, 8, 9, 0 }, 
        { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
        { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
        { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
        };

        int[,] routeIndexes = new int[10, 10] 
        { 
       { 0, 0, 3, 4, 5, 0, 0, 0, 0, 0 },
       { 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
       { 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
       { 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
       { 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
       { 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
       { 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
       { 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
       { 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
       { 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
        };

        public void FillWithValues()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    int caseSwitch = borderIndexes[i, j];
                    TransparentPictureBox pb = borderPBs[i, j];
                    switch (caseSwitch)
                    {
                        case 0:
                            pb.Image = ALL_OPEN;
                            break;
                        case 1:
                            pb.Image = EAST_CLOSED;
                            break;
                        case 2:
                            pb.Image = EAST_SOUTH_CLOSED;
                            break;
                        case 3:
                            pb.Image = EAST_SOUTH_WEST_CLOSED;
                            break;
                        case 4:
                            pb.Image = EAST_WEST_CLOSED;
                            break;
                        case 5:
                            pb.Image = NORTH_CLOSED;
                            break;
                        case 6:
                            pb.Image = NORTH_EAST_CLOSED;
                            break;
                        case 7:
                            pb.Image = NORTH_EAST_SOUTH_CLOSED;
                            break;
                        case 8:
                            pb.Image = NORTH_SOUTH_CLOSED;
                            break;
                        case 9:
                            pb.Image = SOUTH_CLOSED;
                            break;
                        case 10:
                            pb.Image = SOUTH_WEST_CLOSED;
                            break;
                        case 11:
                            pb.Image = SOUTH_WEST_NORTH_CLOSED;
                            break;
                        case 12:
                            pb.Image = WEST_CLOSED;
                            break;
                        case 13:
                            pb.Image = WEST_NORTH_CLOSED;
                            break;
                        case 14:
                            pb.Image = WEST_NORTH_EAST_CLOSED;
                            break;
                        default:
                            pb.Image = ALL_OPEN;

                            break;
                    }
                }
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    int caseSwitch2 = routeIndexes[i, j];
                    TransparentPictureBox pb = routePBs[i, j];
                    switch (caseSwitch2)
                    {
                        case 0:
                            pb.Image = NOT_ENTERED_ZONE_IN;
                            break;
                        case 1:
                            pb.Image = EAST_TO_SOUTH_IN;
                            break;
                        case 2:
                            pb.Image = EAST_TO_WEST_IN;
                            break;
                        case 3:
                            pb.Image = NORTH_TO_EAST_IN;
                            break;
                        case 4:
                            pb.Image = NORTH_TO_SOUTH_IN;
                            break;
                        case 5:
                            pb.Image = SOUTH_TO_WEST_IN;
                            break;
                        case 6:
                            pb.Image = WEST_TO_NORTH_IN;
                            break;
                        default:
                            pb.Image = NOT_ENTERED_ZONE_IN;

                            break;
                    }
                }
            }
        }
    }
}
