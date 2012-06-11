using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rescuebots
{
    class ConvertRp6Info
    {
        private List<string> Celinformatie;
        private string Rp6info;

        public List<string> KCelinfo { get { return Celinformatie; }}

        ConvertRp6Info()
        {
            Celinformatie = new List<string>();
            
        }

        public void AddToListCelinfo(string rp6info)
        {
            string a = "";
            Rp6info = rp6info;
            int lengt=(Rp6info.Count()/12);
            for(int b=0;b<=lengt;b++)
            {
                a  =  Rp6info.Substring(0, 12);
                Rp6info.Remove(0,12);
                Celinformatie.Add(a);
            }   
            
        }

        private void ConvertIntTo2dArray(List<string> celinfo)
        {

        }
    }
}
