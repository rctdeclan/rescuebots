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

       // int[,] borderIndexes = new int[10, 10] 
       // { 
       //{ 1, 0, 3, 4, 5, 0, 0, 0, 0, 0 },
       //{ 11,2, 3, 4, 5, 6, 5, 4, 3, 0 },
       //{ 11,2, 3, 4, 5, 6, 5, 4, 3, 0 },
       //{ 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
       //{ 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
       //{ 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
       //{ 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
       //{ 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
       //{ 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
       //{ 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
       // };

       // int[,] routeIndexes = new int[10, 10] 
       // { 
       //{ 1, 0, 3, 4, 5, 0, 0, 0, 0, 0 },
       //{ 11,2, 3, 4, 5, 6, 5, 4, 3, 0 },
       //{ 11,2, 3, 4, 5, 6, 5, 4, 3, 0 },
       //{ 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
       //{ 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
       //{ 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
       //{ 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
       //{ 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
       //{ 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
       //{ 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
       // };

       // int[,] invRouteIndexes = new int[10, 10] 
       // { 
       // { 1, 9, 9, 9, 9, 9, 9, 9, 9, 9 }, 
       // { 10,2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
       // { 10,2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
       // { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
       // { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
       // { 1, 2, 3, 8, 5, 6, 7, 8, 9, 0 }, 
       // { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
       // { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
       // { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
       // { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
       // };

        public void FillBorders(List<cell> cells)
        {
            for (int i = 0; i< cells.Count; i++)
            {
                TransparentPictureBox pb = borderPBs[cells[i].x, cells[i].y];
                int index = ((cells[i].north ? 0x1 : 0) + (cells[i].east ? 0x2 : 0) + (cells[i].south ? 0x4 : 0) + (cells[i].west ? 0x8 : 0));
                switch (index)
                {
                    case 0x0:
                        pb.Image = ALL_OPEN;
                        break;
                    case 0x1:
                        pb.Image = NORTH_CLOSED;
                        break;
                    case 0x2:
                        pb.Image = EAST_CLOSED;
                        break;
                    case 0x3:
                        pb.Image = NORTH_EAST_CLOSED;
                        break;
                    case 0x4:
                        pb.Image = SOUTH_CLOSED;
                        break;
                    case 0x5:
                        pb.Image = NORTH_SOUTH_CLOSED;
                        break;
                    case 0x6:
                        pb.Image = EAST_SOUTH_CLOSED;
                        break;
                    case 0x7:
                        pb.Image = NORTH_EAST_SOUTH_CLOSED;
                        break;
                    case 0x8:
                        pb.Image = WEST_CLOSED;
                        break;
                    case 0x9:
                        pb.Image = WEST_NORTH_CLOSED;
                        break;
                    case 0x10:
                        pb.Image = EAST_WEST_CLOSED;
                        break;
                    case 0x11:
                        pb.Image = WEST_NORTH_EAST_CLOSED;
                        break;
                    case 0x12:
                        pb.Image = SOUTH_WEST_CLOSED;
                        break;
                    case 0x13:
                        pb.Image = SOUTH_WEST_NORTH_CLOSED;
                        break;
                    case 0x14:
                        pb.Image = EAST_SOUTH_WEST_CLOSED;
                        break;
                    default:
                        pb.Image = ALL_OPEN;
                        break;
                }

            }
        }

        public void FillRoute()
        {
        }

        public void FillInvRoute()
        {
        }

        //public void FillWithValues()
        //{
        //    for (int i = 0; i < 10; i++)
        //    {
        //        for (int j = 0; j < 10; j++)
        //        {
        //            byte caseSwitch = borderIndexes[i, j];
        //            TransparentPictureBox pb = borderPBs[i, j];
        //            switch (caseSwitch)
        //            {
        //                case 0x0:
        //                    pb.Image = ALL_OPEN;
        //                    break;
        //                case 1:
        //                    pb.Image = EAST_CLOSED;
        //                    break;
        //                case 2:
        //                    pb.Image = EAST_SOUTH_CLOSED;
        //                    break;
        //                case 3:
        //                    pb.Image = EAST_SOUTH_WEST_CLOSED;
        //                    break;
        //                case 4:
        //                    pb.Image = EAST_WEST_CLOSED;
        //                    break;
        //                case 5:
        //                    pb.Image = NORTH_CLOSED;
        //                    break;
        //                case 6:
        //                    pb.Image = NORTH_EAST_CLOSED;
        //                    break;
        //                case 7:
        //                    pb.Image = NORTH_EAST_SOUTH_CLOSED;
        //                    break;
        //                case 8:
        //                    pb.Image = NORTH_SOUTH_CLOSED;
        //                    break;
        //                case 9:
        //                    pb.Image = SOUTH_CLOSED;
        //                    break;
        //                case 10:
        //                    pb.Image = SOUTH_WEST_CLOSED;
        //                    break;
        //                case 11:
        //                    pb.Image = SOUTH_WEST_NORTH_CLOSED;
        //                    break;
        //                case 12:
        //                    pb.Image = WEST_CLOSED;
        //                    break;
        //                case 13:
        //                    pb.Image = WEST_NORTH_CLOSED;
        //                    break;
        //                case 14:
        //                    pb.Image = WEST_NORTH_EAST_CLOSED;
        //                    break;
        //                default:
        //                    pb.Image = ALL_OPEN;

        //                    break;
        //            }
        //            caseSwitch = routeIndexes[i, j];
        //            pb = routePBs[i, j];
        //            switch (caseSwitch)
        //            {
        //                case 0:
        //                    pb.Image = NOT_ENTERED_ZONE_IN;
        //                    break;
        //                case 1:
        //                    pb.Image = EAST_TO_SOUTH_IN;
        //                    break;
        //                case 2:
        //                    pb.Image = EAST_TO_WEST_IN;
        //                    break;
        //                case 3:
        //                    pb.Image = NORTH_TO_EAST_IN;
        //                    break;
        //                case 4:
        //                    pb.Image = NORTH_TO_SOUTH_IN;
        //                    break;
        //                case 5:
        //                    pb.Image = SOUTH_TO_WEST_IN;
        //                    break;
        //                case 6:
        //                    pb.Image = WEST_TO_NORTH_IN;
        //                    break;
        //                case 7:
        //                    pb.Image = NORTH_EAST_WEST_IN;
        //                    break;
        //                case 8:
        //                    pb.Image = EAST_SOUTH_WEST_IN;
        //                    break;
        //                case 9:
        //                    pb.Image = NORTH_SOUTH_WEST_IN;
        //                    break;
        //                case 10:
        //                    pb.Image = NORTH_EAST_WEST_IN;
        //                    break;
        //                case 11:
        //                    pb.Image = NORTH_EAST_SOUTH_WEST_IN;
        //                    break;
        //                default:
        //                    pb.Image = NOT_ENTERED_ZONE_IN;

        //                    break;
        //            }
        //            caseSwitch = invRouteIndexes[i, j];
        //            pb = invRoutePBs[i, j];
        //            switch (caseSwitch)
        //            {
        //                case 0:
        //                    pb.Image = NOT_ENTERED_ZONE_OUT;
        //                    break;
        //                case 1:
        //                    pb.Image = EAST_TO_SOUTH_OUT;
        //                    break;
        //                case 2:
        //                    pb.Image = EAST_TO_WEST_OUT;
        //                    break;
        //                case 3:
        //                    pb.Image = NORTH_TO_EAST_OUT;
        //                    break;
        //                case 4:
        //                    pb.Image = NORTH_TO_SOUTH_OUT;
        //                    break;
        //                case 5:
        //                    pb.Image = SOUTH_TO_WEST_OUT;
        //                    break;
        //                case 6:
        //                    pb.Image = WEST_TO_NORTH_OUT;
        //                    break;
        //                case 7:
        //                    pb.Image = NORTH_EAST_WEST_OUT;
        //                    break;
        //                case 8:
        //                    pb.Image = EAST_SOUTH_WEST_OUT;
        //                    break;
        //                case 9:
        //                    pb.Image = NORTH_SOUTH_WEST_OUT;
        //                    break;
        //                case 10:
        //                    pb.Image = NORTH_EAST_WEST_OUT;
        //                    break;
        //                case 11:
        //                    pb.Image = NORTH_EAST_SOUTH_WEST_OUT;
        //                    break;
        //                default:
        //                    pb.Image = NOT_ENTERED_ZONE_OUT;

        //                    break;
        //            }
        //        }
        //    }
        //}
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
        }
    }
}
